using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueBoxSharp.Data
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SqlFunctionAttribute : Attribute
    {
        public string CallFormat { get; private set; }

        public SqlFunctionAttribute(string callFormat)
        {
            this.CallFormat = callFormat;
        }
    }

    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public class SqlKeywordAttribute : Attribute
    {
    }
}
