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
using BlueBoxSharp.Data.Expressions;

namespace BlueBoxSharp.Data.Converters.IqueryableConverters
{
    internal class CountMethodConverter : BaseIQueryableMethodConverter
    {
        public CountMethodConverter() : base("Count") { }

        public override Expression Call(ExpressionConverter converter, MethodCallExpression expression)
        {
            QueryExpression context = (QueryExpression)converter.Convert(expression.Arguments[0]);

            if (expression.Arguments.Count == 2)
            {
                LambdaExpression lambda = converter.GetLambdaExpression(expression.Arguments[1]);
                WhereMethodConverter.ConvertPredicate(converter, context, lambda);
            }

            if (context.IsDefaultProjection)
            {
                context.Project(new ProjectionExpression(typeof(int), new AggregateExpression(typeof(int), ExtendedExpressionType.Count, null)));
                context.ResultType = QueryReturnType.Aggregate;

                return context;
            }
            else
            {
                QueryExpression countQuery = context.WrapQuery(typeof(int));
                countQuery.ResultType = QueryReturnType.Aggregate;
                countQuery.From = context;
                countQuery.Project(new ProjectionExpression(typeof(int), new AggregateExpression(typeof(int), ExtendedExpressionType.Count, null)));

                return countQuery;
            }
        }
    }
}
