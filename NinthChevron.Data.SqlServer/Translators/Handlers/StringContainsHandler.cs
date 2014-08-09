using NinthChevron.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Data.SqlServer.Translators.Handlers
{
    [NativeMethod(typeof(string), "Contains")]
    public class StringContainsHandler : IMethodHandler
    {
        public string Translate(Type realType, string objectExpression, params string[] parametersExpressions)
        {
            return string.Format("{0} LIKE {1}", objectExpression, parametersExpressions[0]);
        }
    }
}
