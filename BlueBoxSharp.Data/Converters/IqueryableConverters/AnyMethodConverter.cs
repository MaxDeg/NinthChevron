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
using BlueBoxSharp.Data.Expressions;

namespace BlueBoxSharp.Data.Converters.IqueryableConverters
{
    // Equivalent of EXISTS in SQL
    internal class AnyMethodConverter : BaseIQueryableMethodConverter
    {
        public AnyMethodConverter() : base("Any") { }

        public override Expression Call(ExpressionConverter converter, MethodCallExpression expression)
        {
            QueryExpression context = (QueryExpression)converter.Convert(expression.Arguments[0]);

            if (expression.Arguments.Count == 2)
            {
                LambdaExpression lambda = converter.GetLambdaExpression(expression.Arguments[1]);
                WhereMethodConverter.ConvertPredicate(converter, context, lambda);
            }

            context.Project(null);
            return new ExistsExpression(context);
        }
    }
}
