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
using NinthChevron.Data.Expressions;

namespace NinthChevron.Data.Converters
{
    internal class ParameterConverter : BaseConverter<ParameterExpression>
    {
        internal ParameterConverter() : base(ExpressionType.Parameter) { }

        public override Expression Convert(ExpressionConverter converter, ParameterExpression expression)
        {
            Expression resultExpression;
            if (converter.Context.TryGetBoundExpression(expression, out resultExpression))
            {
                if (resultExpression.NodeType == (ExpressionType)ExtendedExpressionType.Entity)
                    return new EntityRefExpression((EntityExpression)resultExpression);
                else if (typeof(IInternalQuery).IsAssignableFrom(resultExpression.Type) && resultExpression.NodeType == ExpressionType.Constant)
                {
                    // An EntitySet passed as a Constant in parameter
                    // Create a new Query expression
                    IInternalQuery set = (IInternalQuery)((ConstantExpression)resultExpression).Value;
                    return new QueryExpression(set.Context, resultExpression.Type.GetGenericArguments()[0]);
                }
                else
                    return resultExpression;
            }

            throw new InvalidOperationException("No parameter found");
        }
    }
}
