using BlueBoxSharp.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSandbox
{
    [NativeMethod(typeof(IEnumerable<>), "Translate")]
    public class TestHandler : IMethodHandler
    {
        public string Translate(Type t, string objectExpression, params string[] parametersExpressions)
        {
            return "TestHandler.Translate";
        }
    }

    [NativeMethod(typeof(object), "Translate")]
    public class Test2Handler : IMethodHandler
    {
        public string Translate(Type t, string objectExpression, params string[] parametersExpressions)
        {
            return "TestHandler.Translate(object)";
        }
    }

    [NativeMethod(typeof(string), "Translate")]
    public class Test3Handler : IMethodHandler
    {
        public string Translate(Type t, string objectExpression, params string[] parametersExpressions)
        {
            return "TestHandler.Translate(string)";
        }
    }
}
