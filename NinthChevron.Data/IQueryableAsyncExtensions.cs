using NinthChevron.Collections;
using NinthChevron.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NinthChevron.Data
{
#if !NET40

    public static class IQueryableAsyncExtensions
    {
        #region Method definitions

        private static readonly MethodInfo _first = GetMethod(
            "First", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _first_Predicate = GetMethod(
            "First", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _firstOrDefault = GetMethod(
            "FirstOrDefault", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _firstOrDefault_Predicate = GetMethod(
            "FirstOrDefault", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _single = GetMethod(
            "Single", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _single_Predicate = GetMethod(
            "Single", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _singleOrDefault = GetMethod(
            "SingleOrDefault", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _singleOrDefault_Predicate = GetMethod(
            "SingleOrDefault", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _contains = GetMethod(
            "Contains", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    T
                });

        private static readonly MethodInfo _any = GetMethod(
            "Any", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _any_Predicate = GetMethod(
            "Any", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _all_Predicate = GetMethod(
            "All", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _count = GetMethod(
            "Count", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _count_Predicate = GetMethod(
            "Count", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _longCount = GetMethod(
            "LongCount", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _longCount_Predicate = GetMethod(
            "LongCount", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, typeof(bool)))
                });

        private static readonly MethodInfo _min = GetMethod(
            "Min", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _min_Selector = GetMethod(
            "Min", (T, U) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, U))
                });

        private static readonly MethodInfo _max = GetMethod(
            "Max", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _max_Selector = GetMethod(
            "Max", (T, U) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, U))
                });

        private static readonly MethodInfo _sum = GetMethod(
            "Sum", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _sum_Selector = GetMethod(
            "Sum", (T, R) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, R))
                });

        private static readonly MethodInfo _average = GetMethod(
            "Average", (T) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T)
                });

        private static readonly MethodInfo _average_Selector = GetMethod(
            "Average", (T, R) => new[]
                {
                    typeof(IQueryable<>).MakeGenericType(T),
                    typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(T, R))
                });


        private static MethodInfo GetMethod(string methodName, Func<Type[]> getParameterTypes)
        {
            return GetMethod(methodName, getParameterTypes.Method, 0);
        }

        private static MethodInfo GetMethod(string methodName, Func<Type, Type, Type[]> getParameterTypes)
        {
            return GetMethod(methodName, getParameterTypes.Method, 2);
        }

        private static MethodInfo GetMethod(string methodName, Func<Type, Type[]> getParameterTypes)
        {
            return GetMethod(methodName, getParameterTypes.Method, 1);
        }

        private static MethodInfo GetMethod(string methodName, MethodInfo getParameterTypesMethod, int genericArgumentsCount)
        {
            var candidates = typeof(Queryable).GetMethods().Where(m => m.Name == methodName);

            foreach (MethodInfo candidate in candidates)
            {
                var genericArguments = candidate.GetGenericArguments();
                if (genericArguments.Length == genericArgumentsCount
                    && Matches(candidate, (Type[])getParameterTypesMethod.Invoke(null, genericArguments)))
                {
                    return candidate;
                }
            }

            return null;
        }

        private static bool Matches(MethodInfo methodInfo, Type[] parameterTypes)
        {
            return methodInfo.GetParameters().Select(p => p.ParameterType).SequenceEqual(parameterTypes);
        }

        #endregion

        private static IAsyncEnumerable AsDbAsyncEnumerable(this IQueryable source)
        {
            ContractHelper.NotNull("source", source);

            var enumerable = source as IAsyncEnumerable;
            if (enumerable != null)
                return enumerable;
            else
                throw new Exception("Provider is not Async");
        }

        private static IAsyncEnumerable<T> AsDbAsyncEnumerable<T>(this IQueryable<T> source)
        {
            ContractHelper.NotNull("source", source);

            var enumerable = source as IAsyncEnumerable<T>;
            if (enumerable != null)
                return enumerable;
            else
                throw new Exception("Provider is not Async");
        }

        async public static Task ForEachAsync<TSource>(this IQueryable<TSource> source, Action<TSource> action, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var enumerator = source.AsDbAsyncEnumerable().GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                cancellationToken.Value.ThrowIfCancellationRequested();
                action(enumerator.Current);
            }
        }

        public static Task<List<TSource>> ToListAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return source.AsDbAsyncEnumerable().ToListAsync(cancellationToken);
        }

        public static Task<TSource[]> ToArrayAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return source.AsDbAsyncEnumerable().ToArrayAsync(cancellationToken);
        }

        public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
            this IQueryable<TSource> source, Func<TSource, TKey> keySelector, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return source.AsDbAsyncEnumerable().ToDictionaryAsync(keySelector, cancellationToken);
        }

        public static Task<Dictionary<TKey, TSource>> ToDictionaryAsync<TSource, TKey>(
            this IQueryable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer,
            CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return source.AsDbAsyncEnumerable().ToDictionaryAsync(keySelector, comparer, cancellationToken);
        }

        public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
            this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return source.AsDbAsyncEnumerable().ToDictionaryAsync(keySelector, elementSelector, cancellationToken);
        }

        public static Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TSource, TKey, TElement>(
            this IQueryable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            return source.AsDbAsyncEnumerable().ToDictionaryAsync(keySelector, elementSelector, comparer, cancellationToken);
        }



        public static Task<TSource> FirstAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _first.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value
                    );
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> FirstAsync<TSource>(
            this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _first_Predicate.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(predicate) }
                        ),
                    cancellationToken.Value);
            }
            else
            {
                throw new Exception("Provider is not Async");
            }
        }

        public static Task<TSource> FirstOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _firstOrDefault.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _firstOrDefault_Predicate.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(predicate) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> SingleAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _single.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> SingleAsync<TSource>(
            this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _single_Predicate.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(predicate) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> SingleOrDefaultAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _singleOrDefault.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> SingleOrDefaultAsync<TSource>(
            this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _singleOrDefault_Predicate.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(predicate) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<bool>(
                    Expression.Call(
                        null,
                        _any.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<bool> AnyAsync<TSource>(
            this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<bool>(
                    Expression.Call(
                        null,
                        _any_Predicate.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(predicate) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<int> CountAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<int>(
                    Expression.Call(
                        null,
                        _count.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<int> CountAsync<TSource>(
            this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<int>(
                    Expression.Call(
                        null,
                        _count_Predicate.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(predicate) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<long> LongCountAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<long>(
                    Expression.Call(
                        null,
                        _longCount.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<long> LongCountAsync<TSource>(
            this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<long>(
                    Expression.Call(
                        null,
                        _longCount_Predicate.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(predicate) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> MinAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _min.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TResult> MinAsync<TSource, TResult>(
            this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TResult>(
                    Expression.Call(
                        null,
                        _min_Selector.MakeGenericMethod(typeof(TSource), typeof(TResult)),
                        new[] { source.Expression, Expression.Quote(selector) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TSource> MaxAsync<TSource>(this IQueryable<TSource> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TSource>(
                    Expression.Call(
                        null,
                        _max.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TResult> MaxAsync<TSource, TResult>(
            this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TResult>(
                    Expression.Call(
                        null,
                        _max_Selector.MakeGenericMethod(typeof(TSource), typeof(TResult)),
                        new[] { source.Expression, Expression.Quote(selector) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TResult> SumAsync<TResult>(this IQueryable<TResult> source, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TResult>(
                    Expression.Call(
                        null,
                        _sum,
                        new[] { source.Expression }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }

        public static Task<TResult> AverageAsync<TSource, TResult>(
            this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, CancellationToken? cancellationToken = null)
        {
            if (cancellationToken == null) cancellationToken = CancellationToken.None;
            else cancellationToken.Value.ThrowIfCancellationRequested();

            var provider = source.Provider as IAsyncQueryProvider;
            if (provider != null)
            {
                return provider.ExecuteAsync<TResult>(
                    Expression.Call(
                        null,
                        _average_Selector.MakeGenericMethod(typeof(TSource)),
                        new[] { source.Expression, Expression.Quote(selector) }
                        ),
                    cancellationToken.Value);
            }
            else
                throw new Exception("Provider is not Async");
        }
    }

#endif
}
