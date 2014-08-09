using BlueBoxSharp.Data.Translators.Handlers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Data.SqlServer.Translators.Handlers
{
    [NativeMethod(typeof(IEnumerable<>), "Contains")]
    public class IEnumerableContainsHandler : IMethodHandler
    {
        public string Translate(Type realType, string objectExpression, params string[] parametersExpressions)
        {
            string list = objectExpression != null ? objectExpression : parametersExpressions[0];
            string argument = objectExpression != null ? parametersExpressions[0] : parametersExpressions[1];

            if (list == "()")
            {
                return string.Format("{0} <> {0}", argument);
            }
            else
            {
                return string.Format("{0} IN {1}", argument, list);
            }
        }
    }
}
