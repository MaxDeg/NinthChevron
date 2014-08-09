using BlueBoxSharp.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Data.SqlServer.Translators.Handlers
{
    [NativeMethod(typeof(object), "ToString")]
    internal class ObjectToStringHandler : IMethodHandler
    {
        public string Translate(Type realType, string objectExpression, params string[] parametersExpressions)
        {
            return string.Format("CONVERT(NVARCHAR, {0})", objectExpression);
        }
    }
}
