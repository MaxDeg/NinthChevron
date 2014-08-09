using NinthChevron.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Data.SqlServer.Translators.Handlers
{
    [NativeMethod(typeof(string), "ToUpper")]
    [NativeMethod(typeof(char), "ToUpper")]
    public class ToUpperHandler : IMethodHandler
    {
        public string Translate(Type realType, string objectExpression, params string[] parametersExpressions)
        {
            if (realType == typeof(string))
            {
                return string.Format("UPPER({0})", objectExpression);
            }
            else if (realType == typeof(char))
            {
                return string.Format("UPPER({0})", parametersExpressions[0]);
            }
            else
            {
                // Should never happens
                throw new InvalidOperationException();
            }
        }
    }
}
