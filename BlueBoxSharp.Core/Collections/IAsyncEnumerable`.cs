using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Collections
{
#if !NET40

    public interface IAsyncEnumerable<TClass>
    {
        IAsyncEnumerator<TClass> GetAsyncEnumerator();
    }

#endif
}
