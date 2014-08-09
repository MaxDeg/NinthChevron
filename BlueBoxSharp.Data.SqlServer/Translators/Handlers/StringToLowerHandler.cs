using BlueBoxSharp.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Data.SqlServer.Translators.Handlers
{
    [NativeMethod(typeof(string), "ToLower")]
    [NativeMethod(typeof(char), "ToLower")]
    public class StringToLowerHandler : IMethodHandler
    {
        public string Translate(Type realType, string objectExpression, params string[] parametersExpressions)
        {
            if (realType == typeof(string))
            {
                return string.Format("LOWER({0})", objectExpression);
            }
            else if (realType == typeof(char))
            {
                return string.Format("LOWER({0})", parametersExpressions[0]);
            }
            else
            {
                // Should never happens
                throw new InvalidOperationException();
            }
        }
    }
}
