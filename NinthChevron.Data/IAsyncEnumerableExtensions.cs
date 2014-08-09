using NinthChevron.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NinthChevron.Data
{
#if !NET40

    public static class IAsyncEnumerableExtensions
    {
        async public static Task<TSource> FirstAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);
            bool isInit = false;

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    cancellationToken.Value.ThrowIfCancellationRequested();
                    result = enumerator.Current;
                    isInit = true;
                    break;
                }

                if (!isInit)
                    throw new InvalidOperationException("Sequence is empty");

                return result;
            }
        }

        async public static Task<TSource> FirstAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);
            bool isInit = false;

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    cancellationToken.Value.ThrowIfCancellationRequested();
                    if (!predicate(enumerator.Current)) continue;

                    result = enumerator.Current;
                    isInit = true;
                    break;
                }

                if (!isInit)
                    throw new InvalidOperationException("Sequence is empty");

                return result;
            }
        }

        async public static Task<TSource> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    cancellationToken.Value.ThrowIfCancellationRequested();
                    result = enumerator.Current;
                    break;
                }

                return result;
            }
        }

        async public static Task<TSource> FirstOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    cancellationToken.Value.ThrowIfCancellationRequested();
                    if (!predicate(enumerator.Current)) continue;

                    result = enumerator.Current;
                    break;
                }

                return result;
            }
        }

        async public static Task<TSource> SingleAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);
            bool isInit = false;

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    if (isInit)
                        throw new InvalidOperationException("The input sequence contains more than one element.");

                    cancellationToken.Value.ThrowIfCancellationRequested();
                    result = enumerator.Current;
                    isInit = true;
                    break;
                }

                if (!isInit)
                    throw new InvalidOperationException("Sequence is empty");

                return result;
            }
        }

        async public static Task<TSource> SingleAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);
            bool isInit = false;

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    if (!predicate(enumerator.Current)) continue;
                    if (isInit)
                        throw new InvalidOperationException("The input sequence contains more than one element.");

                    cancellationToken.Value.ThrowIfCancellationRequested();
                    result = enumerator.Current;
                    isInit = true;
                    break;
                }

                if (!isInit)
                    throw new InvalidOperationException("Sequence is empty");

                return result;
            }
        }

        async public static Task<TSource> SingleOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);
            bool isInit = false;

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    if (isInit)
                        throw new InvalidOperationException("The input sequence contains more than one element.");

                    cancellationToken.Value.ThrowIfCancellationRequested();
                    result = enumerator.Current;
                    isInit = true;
                    break;
                }

                return result;
            }
        }

        async public static Task<TSource> SingleOrDefaultAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            TSource result = default(TSource);
            bool isInit = false;

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    if (!predicate(enumerator.Current)) continue;
                    if (isInit)
                        throw new InvalidOperationException("The input sequence contains more than one element.");

                    cancellationToken.Value.ThrowIfCancellationRequested();
                    result = enumerator.Current;
                    isInit = true;
                    break;
                }

                return result;
            }
        }


        async public static Task<List<TSource>> ToListAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            List<TSource> list = new List<TSource>();

            using (var enumerator = source.GetAsyncEnumerator())
            {
                while (await enumerator.MoveNextAsync())
                {
                    cancellationToken.Value.ThrowIfCancellationRequested();
                    list.Add(enumerator.Current);
                }

                return list;
            }
        }

        async public static Task<TSource[]> ToArrayAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return (await source.ToListAsync(cancellationToken)).ToArray();
        }

        async public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
            this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return (await source.ToListAsync(cancellationToken)).ToDictionary(keySelector);
        }

        async public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
            this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer,
            CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return (await source.ToListAsync(cancellationToken)).ToDictionary(keySelector, comparer);
        }

        async public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
            this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return (await source.ToListAsync(cancellationToken)).ToDictionary(keySelector, elementSelector);
        }

        async public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
            this IAsyncEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return (await source.ToListAsync(cancellationToken)).ToDictionary(keySelector, elementSelector, comparer);
        }
    }

#endif
}
