using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Helpers
{
    public static class ContractHelper
    {
        public static void NotNull(string param, object value)
        {
            if (value == null) throw new ArgumentNullException(param);
        }

        public static void NotEmpty(string param, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(param);
        }
    }
}
