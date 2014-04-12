using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBoxSharp.Collections
{
#if !NET40

    public interface IAsyncEnumerator<TClass> : IDisposable
    {
        TClass Current { get; }

        Task<bool> MoveNextAsync();
        Task ResetAsync();
    }

#endif
}
