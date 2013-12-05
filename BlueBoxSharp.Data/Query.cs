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
using System.Text;
using BlueBoxSharp.Data.Converters;
using BlueBoxSharp.Data.Expressions;

namespace BlueBoxSharp.Data
{
    internal class Query<T> : IOrderedQueryable<T>, IInternalQuery
    {
        public Query(IQueryProvider provider, DataContext context)
        {
            Provider = provider;
            Expression = Expression.Constant(this);
            Context = context;
        }

        public Query(IQueryProvider provider, Expression expression)
        {
            Provider = provider;
            Expression = expression ?? Expression.Constant(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            ExpressionConverter converter = new ExpressionConverter(this.Expression);
            QueryExpression queryExpression = (QueryExpression)converter.Convert();

            return queryExpression.Context.Execute<T>(queryExpression).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public Expression Expression { get; private set; }
        public IQueryProvider Provider { get; private set; }
        public DataContext Context { get; private set; }
    }
}
