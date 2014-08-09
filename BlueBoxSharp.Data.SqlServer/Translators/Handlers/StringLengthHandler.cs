using BlueBoxSharp.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Data.SqlServer.Translators.Handlers
{
    [NativeMethod(typeof(string), "Length")]
    public class StringLengthHandler : IMethodHandler
    {
        public string Translate(Type realType, string objectExpression, params string[] parametersExpressions)
        {
            return string.Format("LEN({0})", parametersExpressions[0]);
        }
    }
}
