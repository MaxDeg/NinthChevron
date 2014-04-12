/**
 *   Copyright 2013
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BlueBoxSharp.Data.Converters;
using BlueBoxSharp.Data.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BlueBoxSharp.Collections;

namespace BlueBoxSharp.Data
{
    internal class QueryProvider : IQueryProvider
#if !NET40
, IAsyncQueryProvider
#endif
    {
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Query<TElement>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new Query<object>(this, expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            ExpressionConverter converter = new ExpressionConverter(expression);
            CommandExpression query = (CommandExpression)converter.Convert();

            if (query is QueryExpression)
                return ExecuteQueryExpression<TResult>(query as QueryExpression);
            else
                query.Context.ExecuteCommand<object>(query, null);

            return default(TResult);
        }

        internal IEnumerable<TResult> ExecuteEnum<TResult>(Expression expression)
        {
            ExpressionConverter converter = new ExpressionConverter(expression);
            QueryExpression query = (QueryExpression)converter.Convert();

            return query.Context.Execute<TResult>(query);
        }

        public object Execute(Expression expression)
        {
            return Execute<object>(expression);
        }

        private TResult ExecuteQueryExpression<TResult>(QueryExpression query)
        {
            IEnumerable<TResult> collection = query.Context.Execute<TResult>(query);

            if (query.ResultType == QueryReturnType.Single || query.ResultType == QueryReturnType.Aggregate)
                try
                {
                    return (TResult)collection.Single();
                }
                catch (InvalidOperationException)
                {
                    throw new RecordNotfoundException();
                }

            else if (query.ResultType == QueryReturnType.SingleOrDefault)
                return (TResult)collection.SingleOrDefault();

            else if (query.ResultType == QueryReturnType.First)
                try
                {
                    return (TResult)collection.First();
                }
                catch (InvalidOperationException)
                {
                    throw new RecordNotfoundException();
                }

            else if (query.ResultType == QueryReturnType.FirstOrDefault)
                return (TResult)collection.FirstOrDefault();

            return default(TResult);
        }

#if !NET40

        public Task ExecuteAsync(Expression expression, CancellationToken token)
        {
            return ExecuteAsync<object>(expression, token);
        }

        async public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken token)
        {
            ExpressionConverter converter = new ExpressionConverter(expression);
            QueryExpression query = (QueryExpression)converter.Convert();

            token.ThrowIfCancellationRequested();

            IAsyncEnumerable<TResult> collection = query.Context.AsyncExecute<TResult>(query);

            token.ThrowIfCancellationRequested();

            if (query.ResultType == QueryReturnType.Single || query.ResultType == QueryReturnType.Aggregate)
                try
                {
                    return (TResult)await collection.SingleAsync();
                }
                catch (InvalidOperationException)
                {
                    throw new RecordNotfoundException();
                }

            else if (query.ResultType == QueryReturnType.SingleOrDefault)
                return (TResult)await collection.SingleOrDefaultAsync();

            else if (query.ResultType == QueryReturnType.First)
                try
                {
                    return (TResult)await collection.FirstAsync();
                }
                catch (InvalidOperationException)
                {
                    throw new RecordNotfoundException();
                }

            else if (query.ResultType == QueryReturnType.FirstOrDefault)
                return (TResult)await collection.FirstOrDefaultAsync();

            return default(TResult);
        }

#endif
    }
}
