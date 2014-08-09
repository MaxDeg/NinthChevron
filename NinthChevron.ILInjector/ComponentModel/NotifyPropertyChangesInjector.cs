/**
 *   Copyright 2013
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using NinthChevron.ComponentModel.DataAnnotations;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Pdb;
using System.CodeDom.Compiler;
using Mono.Collections.Generic;

namespace NinthChevron.ILInjector.Injectors
{
    public class NotifyPropertyChangesInjector
    {
        #region Static

        private static readonly System.Reflection.MethodBase combineMethodBase;
        private static readonly System.Reflection.MethodBase invokeMethodBase;
        private static readonly System.Reflection.MethodBase constructorMethodBase;
        private static readonly System.Reflection.MethodBase removeMethodBase;
        private static readonly System.Reflection.MethodBase equalsMethodBase;

        private static readonly string EventName = "PropertyChanged";
        private static readonly string RaiseMethodName = "__RaisePropertyChanged";
        
        static NotifyPropertyChangesInjector()
        {
            var type = typeof(Delegate);
            combineMethodBase = type.GetMethod("Combine", new[] { type, type });
            removeMethodBase = type.GetMethod("Remove");

            // --------------------------------------------------------------------------

            type = typeof(PropertyChangedEventHandler);
            invokeMethodBase = type.GetMethod("Invoke");

            // --------------------------------------------------------------------------

            type = typeof(PropertyChangedEventArgs);
            constructorMethodBase = type.GetConstructor(new Type[] { typeof(string) });

            // --------------------------------------------------------------------------

            type = typeof(object);
            equalsMethodBase = type.GetMethod("Equals", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        }

        #endregion

        private string _assemblyPath;
        private AssemblyDefinition _assembly;

        public NotifyPropertyChangesInjector(string assemblyPath)
        {
            this._assemblyPath = assemblyPath;
        }

        public void Run()
        {
            DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(Path.GetDirectoryName(this._assemblyPath));

            this._assembly = AssemblyDefinition.ReadAssembly(this._assemblyPath, new ReaderParameters { ReadSymbols = true, AssemblyResolver = resolver });

            foreach (ModuleDefinition moduleDefinition in this._assembly.Modules)
            {
                foreach (TypeDefinition type in GetTypesToWeave(moduleDefinition))
                {
                    MethodReference method = InjectINotifyPropertyChanged(moduleDefinition, type);
                    InjectEventTriggers(moduleDefinition, type, method);
                }
            }
            
            this._assembly.Write(this._assemblyPath, new WriterParameters
            {
                WriteSymbols = true,
                SymbolWriterProvider = new PdbWriterProvider()
            });
        }

        #region Private methods

        private IEnumerable<TypeDefinition> GetTypesToWeave(ModuleDefinition moduleDefinition)
        {
            TypeReference notifyAttrReference = GetReference<NotifyPropertyChangedAttribute>(moduleDefinition);

            return moduleDefinition.Types.Where(t => t.Properties.Any(p => p.CustomAttributes.Any(a => a.AttributeType == notifyAttrReference)));
        }

        private MethodReference InjectINotifyPropertyChanged(ModuleDefinition moduleDefinition, TypeDefinition type)
        {
            // Try to find the INotifyPropertyChanged interface in the object hierarchy
            TypeReference iNotifyPropertyType = type.GetHierarchy().FirstOrDefault(t => t.Resolve().Interfaces.Any(i => i.Name == "INotifyPropertyChanged"));
            FieldReference eventField = null;

            // If we found the interface, we get the event field required by this interface
            if (iNotifyPropertyType != null)
                eventField = iNotifyPropertyType.Resolve().Fields.FirstOrDefault(f => f.Name == EventName);
            else
            {
                // Otherwise we add the interface on this object and create the Event
                type.Interfaces.Add(GetReference<INotifyPropertyChanged>(moduleDefinition));
                eventField = InjectEvent(moduleDefinition, type);
            }

            return InjectRaiseEventHelper(moduleDefinition, eventField, type);
        }

        private FieldReference InjectEvent(ModuleDefinition moduleDefinition, TypeDefinition type)
        {
            TypeReference handlerTypeRef = GetReference<PropertyChangedEventHandler>(moduleDefinition);

            MethodReference combineMethodRef = moduleDefinition.Import(combineMethodBase);
            MethodReference removeMethodRef = moduleDefinition.Import(removeMethodBase);

            // Internally an Event is represented by a private field and 2 methods to add and remove event handler
            FieldDefinition eventField = new FieldDefinition(EventName, FieldAttributes.Private, handlerTypeRef);
            type.Fields.Add(eventField);

            EventDefinition eventDefinition = new EventDefinition(EventName, EventAttributes.None, handlerTypeRef)
            {
                AddMethod = CreateEventMethod(moduleDefinition, "add_" + EventName, combineMethodRef, eventField),
                RemoveMethod = CreateEventMethod(moduleDefinition, "remove_" + EventName, removeMethodRef, eventField)
            };

            type.Events.Add(eventDefinition);
            type.Methods.Add(eventDefinition.AddMethod);
            type.Methods.Add(eventDefinition.RemoveMethod);

            return eventField;
        }

        private MethodDefinition CreateEventMethod(ModuleDefinition moduleDefinition, string methodName, MethodReference delegateMethodReference, FieldReference eventField)
        {
            const MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Final |
                                                MethodAttributes.SpecialName | MethodAttributes.NewSlot | MethodAttributes.Virtual;

            TypeReference eventHandlerTypeRef = GetReference<PropertyChangedEventHandler>(moduleDefinition);

            MethodDefinition methodDef = new MethodDefinition(methodName, attributes, moduleDefinition.TypeSystem.Void);
            methodDef.Parameters.Add(new ParameterDefinition(eventHandlerTypeRef));

            ILProcessor ilProcessor = methodDef.Body.GetILProcessor();

            // IL Code
            // --------------------------------------------------------------------------
            
            ilProcessor.Emit(OpCodes.Ldarg_0);                          // Loads the argument at index 0 onto the evaluation stack.
            ilProcessor.Emit(OpCodes.Ldarg_0);                          // Loads the argument at index 0 onto the evaluation stack.
            ilProcessor.Emit(OpCodes.Ldfld, eventField);                // Finds the value of a field in the object whose reference is currently on the evaluation stack.
            ilProcessor.Emit(OpCodes.Ldarg_1);                          // Loads the argument at index 1 onto the evaluation stack.
            ilProcessor.Emit(OpCodes.Call, delegateMethodReference);    // Calls the method indicated by the passed method descriptor.
            ilProcessor.Emit(OpCodes.Castclass, eventHandlerTypeRef);   // Attempts to cast an object passed by reference to the specified class.
            ilProcessor.Emit(OpCodes.Stfld, eventField);                // Replaces the value stored in the field of an object reference or pointer with a new value.
            ilProcessor.Emit(OpCodes.Ret);                              // Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.
            
            // --------------------------------------------------------------------------
            
            return methodDef;
        }

        private MethodReference InjectRaiseEventHelper(ModuleDefinition moduleDefinition, FieldReference eventField, TypeDefinition type)
        {
            // Try to find raise method in parent hierarchy
            MethodDefinition raisePropertyChanged = type.GetHierarchy().SelectMany(t => t.Resolve().Methods)
                                                            .Where(m => m.Name == RaiseMethodName && m.Parameters.Count == 3)
                                                            .Where(m => m.Parameters[0].ParameterType.MetadataType == MetadataType.String)
                                                            .Where(m => m.Parameters[1].ParameterType.MetadataType == MetadataType.Object)
                                                            .Where(m => m.Parameters[2].ParameterType.MetadataType == MetadataType.Object)
                                                            .SingleOrDefault();

            if (raisePropertyChanged != null)
            {
                // If we found a raise method we resolve it (loading reference in this module)
                raisePropertyChanged = raisePropertyChanged.Resolve();

                // If this declaring type is a generic object. When need to make this call generic
                if (raisePropertyChanged.DeclaringType.HasGenericParameters)
                    return raisePropertyChanged.MakeGeneric(type);

                return raisePropertyChanged;
            }

            TypeReference typeReference = type;
            MethodReference invokeMethodRef = moduleDefinition.Import(invokeMethodBase);
            MethodReference argsCtorMethodRef = moduleDefinition.Import(constructorMethodBase);
            MethodReference equalsMethodRef = moduleDefinition.Import(equalsMethodBase);

            if (eventField.DeclaringType != typeReference)
                typeReference = eventField.DeclaringType;

            raisePropertyChanged = new MethodDefinition(RaiseMethodName, MethodAttributes.Family, typeReference);
            raisePropertyChanged.ReturnType = moduleDefinition.TypeSystem.Void;

            raisePropertyChanged.Parameters.Add(new ParameterDefinition("propertyName", ParameterAttributes.None, moduleDefinition.TypeSystem.String));
            raisePropertyChanged.Parameters.Add(new ParameterDefinition("oldValue", ParameterAttributes.None, moduleDefinition.TypeSystem.Object));
            raisePropertyChanged.Parameters.Add(new ParameterDefinition("newValue", ParameterAttributes.None, moduleDefinition.TypeSystem.Object));
            
            ILProcessor ilProcessor = raisePropertyChanged.Body.GetILProcessor();

            Instruction returnInstruction = ilProcessor.Create(OpCodes.Ret);

            // IL Code
            // --------------------------------------------------------------------------
            
            // Check if event has no subscriber return from methods
            ilProcessor.Emit(OpCodes.Ldarg_0);
            ilProcessor.Emit(OpCodes.Ldfld, eventField);
            ilProcessor.Emit(OpCodes.Brfalse_S, returnInstruction);

            // Check if oldValue and newValue are the same
            // object.Equals
            ilProcessor.Emit(OpCodes.Ldarg_2);
            ilProcessor.Emit(OpCodes.Ldarg_3);
            ilProcessor.Emit(OpCodes.Call, equalsMethodRef);
            ilProcessor.Emit(OpCodes.Ldc_I4_0);
            ilProcessor.Emit(OpCodes.Ceq);
            ilProcessor.Emit(OpCodes.Brfalse_S, returnInstruction);
            
            ilProcessor.Emit(OpCodes.Ldarg_0);
            ilProcessor.Emit(OpCodes.Ldfld, eventField);
            ilProcessor.Emit(OpCodes.Ldarg_0);
            ilProcessor.Emit(OpCodes.Ldarg_1);
            ilProcessor.Emit(OpCodes.Newobj, argsCtorMethodRef);
            ilProcessor.Emit(OpCodes.Callvirt, invokeMethodRef);

            // --------------------------------------------------------------------------

            ilProcessor.Append(returnInstruction);
            
            type.Methods.Add(raisePropertyChanged);

            if (raisePropertyChanged.DeclaringType.HasGenericParameters)
                return raisePropertyChanged.MakeGeneric(type);

            return raisePropertyChanged;
        }

        private void InjectEventTriggers(ModuleDefinition moduleDefinition, TypeDefinition type, MethodReference generatedRaiseMethod)
        {
            TypeReference notifyAttrReference = GetReference<NotifyPropertyChangedAttribute>(moduleDefinition);
            TypeDefinition generatedCodeReference = GetReference<GeneratedCodeAttribute>(moduleDefinition).Resolve();
            MethodReference generatedAttrCtor = moduleDefinition.Import(generatedCodeReference.Methods.First(m => m.IsConstructor && m.Parameters.Count == 2));


            foreach (PropertyDefinition property in type.Properties.Where(p => p.CustomAttributes.Any(a => a.AttributeType == notifyAttrReference)))
            {
                MethodDefinition method = property.SetMethod;
                TypeReference propertyType = property.PropertyType;

                if (method.CustomAttributes.Any(c => c.AttributeType.Name == generatedCodeReference.Name && (string)c.ConstructorArguments[0].Value == "NinthChevronGenerated"))
                    continue;
                if (property.SetMethod == null || property.GetMethod == null) 
                    continue;


                MethodReference raiseMethod = GetRaiseEventMethod(moduleDefinition, type, property, generatedRaiseMethod);
                ILProcessor ilProcessor = method.Body.GetILProcessor();

                // IL Code
                // --------------------------------------------------------------------------


                // Adding Code at the beginning of the setter
                // -------------------------------------------------------------------------
                Instruction retInstruction = method.Body.Instructions.First();

                method.Body.Variables.Add(new VariableDefinition("__oldValue", propertyType));
                int localVarLocation = method.Body.Variables.Count - 1;

                MethodReference getMethodReference = property.GetMethod.Resolve();
                if (property.GetMethod.DeclaringType.HasGenericParameters)
                    getMethodReference = property.GetMethod.MakeGeneric(type); 

                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Ldarg_0));
                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Call, getMethodReference));
                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Stloc, localVarLocation));


                // Adding Code at the end of the setter
                // -------------------------------------------------------------------------
                retInstruction = method.Body.Instructions.Last();


                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Ldarg_0));
                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Ldstr, property.Name));
                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Ldloc, localVarLocation));

                // Converts a value type to an object reference
                if (propertyType.IsValueType)
                    ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Box, propertyType));

                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Ldarg_1));
                
                // Converts a value type to an object reference
                if (propertyType.IsValueType)
                    ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Box, propertyType));

                if (raiseMethod.DeclaringType.IsGenericInstance)
                {
                    foreach (TypeReference arg in ((GenericInstanceType)raiseMethod.DeclaringType).GenericArguments)
                        ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Box, arg));
                }

                ilProcessor.InsertBefore(retInstruction, ilProcessor.Create(OpCodes.Call, raiseMethod));

                // -------------------------------------------------------------------------

                CustomAttribute generatedAttribute = new CustomAttribute(generatedAttrCtor);
                generatedAttribute.ConstructorArguments.Add(new CustomAttributeArgument(moduleDefinition.TypeSystem.String, "NinthChevronGenerated"));
                generatedAttribute.ConstructorArguments.Add(new CustomAttributeArgument(moduleDefinition.TypeSystem.String, "1.0"));
                method.CustomAttributes.Add(generatedAttribute);
            }
        }

        private MethodReference GetRaiseEventMethod(ModuleDefinition moduleDefinition, TypeDefinition type, PropertyDefinition property, MethodReference generatedRaiseMethod)
        {
            TypeReference notifyAttrReference = GetReference<NotifyPropertyChangedAttribute>(moduleDefinition);

            string argumentValue = property.CustomAttributes.Single(a => a.AttributeType == notifyAttrReference)
                                                .ConstructorArguments.Where(a => a.Value != null)
                                                .Select(a => (string)a.Value)
                                                .SingleOrDefault();

            MethodReference overridedRaiseMethod = type.GetHierarchy().SelectMany(t => t.Resolve().Methods)
                                                            .Where(m => m.Name == argumentValue && m.Parameters.Count == 3)
                                                            .Where(m => m.Parameters[0].ParameterType.MetadataType == MetadataType.String)
                                                            .Where(m => m.Parameters[1].ParameterType.MetadataType == MetadataType.Object)
                                                            .Where(m => m.Parameters[2].ParameterType.MetadataType == MetadataType.Object)
                                                            .SingleOrDefault();
                                    
            generatedRaiseMethod = overridedRaiseMethod ?? generatedRaiseMethod;

            if (generatedRaiseMethod.DeclaringType.HasGenericParameters)
                return generatedRaiseMethod.MakeGeneric(type); 

            return moduleDefinition.Import(generatedRaiseMethod);
        }

        private TypeReference GetReference(ModuleDefinition moduleDefinition, Type type)
        {
            TypeReference reference;
            if (!moduleDefinition.TryGetTypeReference(type.FullName, out reference))
                return moduleDefinition.Import(type);
            
            return reference;
        }

        private TypeReference GetReference<TType>(ModuleDefinition moduleDefinition)
        {
            return GetReference(moduleDefinition, typeof(TType));
        }

        #endregion
    }
}
