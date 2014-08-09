using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Data.Translators.Handlers
{
    /// <summary>
    /// Map Native .Net methods to SQL equivalent
    /// Annotate the implementation with NativeMethodAttribute to specify the .Net method
    /// </summary>
    public interface IMethodHandler
    {
        /// <summary>
        /// Return the SQL call
        /// </summary>
        /// <param name="objectExpression">.Net object transformed to SQL. Is null for static methods</param>
        /// <param name="parametersExpressions"></param>
        /// <returns></returns>
        string Translate(Type realType, string objectExpression, params string[] parametersExpressions);
    }
}
