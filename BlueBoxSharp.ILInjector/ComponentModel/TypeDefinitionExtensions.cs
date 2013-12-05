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
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace BlueBoxSharp.ILInjector.Injectors
{
    internal static class TypeDefinitionExtensions
    {
        public static IEnumerable<TypeReference> GetHierarchy(this TypeReference type)
        {
            TypeDefinition current = type.Resolve();
            yield return type;

            while (current.BaseType != null)
            {
                yield return current.BaseType;
                current = current.BaseType.Resolve();
            }
        }
        
        public static TypeReference MakeGenericType(this TypeReference self, Collection<TypeReference> arguments)
        {
            GenericInstanceType instance = new GenericInstanceType(self);
            foreach (var argument in arguments)
                instance.GenericArguments.Add(argument);

            return instance;
        }
        
        public static MethodReference MakeGeneric(this MethodReference self, TypeReference type)
        {
            TypeReference hierarchyType = type.GetHierarchy().FirstOrDefault(t => t.Resolve().FullName == self.DeclaringType.FullName);

            if (type == hierarchyType)
                return self.MakeGeneric(new Collection<TypeReference>(type.GenericParameters.ToArray()));
            if (!hierarchyType.IsGenericInstance)
                return self;

            GenericInstanceType instance = (GenericInstanceType)hierarchyType;
            if (instance.HasGenericArguments)
                return self.MakeGeneric(instance.GenericArguments);
            else
            {
                instance = new GenericInstanceType(self.DeclaringType);

                if (instance.HasGenericArguments)
                    return self.MakeGeneric(new Collection<TypeReference>(instance.GenericArguments));
                else
                    return self.MakeGeneric(new Collection<TypeReference>(instance.GenericParameters.ToArray()));
            }
        }

        public static MethodReference MakeGeneric(this MethodReference self, Collection<TypeReference> arguments)
        {
            var reference = new MethodReference(self.Name, self.ReturnType)
            {
                DeclaringType = self.DeclaringType.MakeGenericType(arguments),
                HasThis = self.HasThis,
                ExplicitThis = self.ExplicitThis,
                CallingConvention = self.CallingConvention,
            };

            foreach (var parameter in self.Parameters)
                reference.Parameters.Add(new ParameterDefinition(parameter.ParameterType));

            foreach (var generic_parameter in self.GenericParameters)
                reference.GenericParameters.Add(new GenericParameter(generic_parameter.Name, reference));

            return reference;
        }
    }
}
