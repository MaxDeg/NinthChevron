using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NinthChevron.Data
{
#if !NET40

    internal interface IAsyncQueryProvider
    {
        Task ExecuteAsync(Expression expression, CancellationToken token);
        Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken token);
    }

#endif
}
