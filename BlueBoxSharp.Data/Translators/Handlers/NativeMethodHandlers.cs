using BlueBoxSharp.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Data.Translators.Handlers
{
    public static class NativeMethodHandlers
    {
        private struct Key : IEquatable<Key>
        {
            private Type _objectType;
            private string _name;
            private Type[] _parametersTypes;

            public Key(Type objectType, string name, Type[] parametersTypes)
            {
                this._objectType = objectType;
                this._name = name;
                this._parametersTypes = parametersTypes;
            }

            public bool Equals(Key other)
            {
                return this._objectType == other._objectType && this._name == other._name &&
                        this._parametersTypes.Count() == other._parametersTypes.Count() && this._parametersTypes.Except(other._parametersTypes).Count() == 0;
            }
        }

        private static ConcurrentDictionary<Key, Type> handlers = new ConcurrentDictionary<Key, Type>();

        static NativeMethodHandlers()
        {
            // Load all the NativeHandlers -> NativeHandlerAttribute
            var types = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                        from type in assembly.GetTypes()
                        where Attribute.IsDefined(type, typeof(NativeMethodAttribute))
                        select type;

            foreach (Type type in types)
            {
                foreach (NativeMethodAttribute attribute in type.GetCustomAttributes(typeof(NativeMethodAttribute), true))
                {
                    handlers.TryAdd(new Key(attribute.ObjectType, attribute.Name, attribute.ParametersTypes), type);
                }
            }
        }

        public static IMethodHandler GetHandler(Type objectType, string name, params Type[] parametersTypes)
        {
            Type handlerType;
            if (handlers.TryGetValue(new Key(objectType, name, parametersTypes), out handlerType))
            {
                return (IMethodHandler)Activator.CreateInstance(handlerType);
            }
            else
            {
                foreach (var subTypes in TypeHelper.GetTypeHierarchy(objectType))
                {
                    foreach (var type in subTypes)
                    {
                        if (handlers.TryGetValue(new Key(type.IsGenericType ? type.GetGenericTypeDefinition() : type, name, parametersTypes), out handlerType))
                        {
                            return (IMethodHandler)Activator.CreateInstance(handlerType);
                        }
                    }
                }

                return null;
            }
        }
    }
}
