using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Collections
{
#if !NET40

    public static class AsyncEnumerable
    {
        public static IAsyncEnumerable<TClass> Empty<TClass>()
        {
            return new EmptyAsyncEnumerable<TClass>();
        }
    }

#endif
}
