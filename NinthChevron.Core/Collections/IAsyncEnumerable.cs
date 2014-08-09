using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Collections
{
#if !NET40

    public interface IAsyncEnumerable
    {
        IAsyncEnumerator GetEnumerator();
    }

#endif
}
