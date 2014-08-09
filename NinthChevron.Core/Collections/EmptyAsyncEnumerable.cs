using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinthChevron.Collections
{
#if !NET40

    internal class EmptyAsyncEnumerable<TClass> : IAsyncEnumerable<TClass>
    {
        public IAsyncEnumerator<TClass> GetAsyncEnumerator()
        {
            return new Enumerator();
        }

        public struct Enumerator : IAsyncEnumerator<TClass>
        {
            public TClass Current { get { throw new InvalidOperationException(); } }

            public Task<bool> MoveNextAsync()
            {
                return Task.FromResult(false);
            }

            public Task ResetAsync()
            {
                throw new InvalidOperationException();
            }

            public void Dispose() { }
        }
    }

#endif
}
