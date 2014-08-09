using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Data.Translators.Handlers
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NativeMethodAttribute : Attribute
    {
        public Type ObjectType { get; private set; }
        public string Name { get; private set; }
        public Type[] ParametersTypes { get; private set; }

        public NativeMethodAttribute(Type objectType, string name, params Type[] parametersTypes)
        {
            this.ObjectType = objectType;
            this.Name = name;
            this.ParametersTypes = parametersTypes;
        }
    }
}
