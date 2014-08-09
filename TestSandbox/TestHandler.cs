using BlueBoxSharp.Data.Translators.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSandbox
{
    [NativeMethod(typeof(object), "Translate")]
    [NativeMethod(typeof(object), "Translate", typeof(int))]
    [NativeMethod(typeof(object), "Translate", typeof(int), typeof(int))]
    public class TestHandler : IMethodHandler
    {
        public string Translate(string objectExpression, params string[] parametersExpressions)
        {
            return "TestHandler.Translate";
        }
    }
}
