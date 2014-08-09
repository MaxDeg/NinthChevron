using NinthChevron.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Data.SqlServer.Translators.Handlers
{
    [NativeMethod(typeof(string), "IsNullOrEmpty")]
    public class StringIsNullOrEmptyHandler : IMethodHandler
    {
        public string Translate(Type realType, string objectExpression, params string[] parametersExpressions)
        {
            return string.Format("ISNULL(LEN({0}), 0) <= 0", parametersExpressions[0]);
        }
    }
}
