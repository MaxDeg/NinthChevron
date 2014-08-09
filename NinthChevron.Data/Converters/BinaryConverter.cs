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
using NinthChevron.Helpers;

namespace NinthChevron.Data.Converters
{
    internal class BinaryConverter : BaseConverter<BinaryExpression>
    {
        internal BinaryConverter() 
            : base(ExpressionType.Equal, ExpressionType.NotEqual, 
                    ExpressionType.GreaterThan, ExpressionType.GreaterThanOrEqual, 
                    ExpressionType.LessThan, ExpressionType.LessThanOrEqual,
                    ExpressionType.AndAlso, ExpressionType.OrElse,
                    ExpressionType.Add, ExpressionType.Subtract, ExpressionType.Multiply, ExpressionType.Divide,
                    ExpressionType.Coalesce,
                    ExpressionType.Assign)
        { }

        public override Expression Convert(ExpressionConverter converter, BinaryExpression expression)
        {
            Expression left = converter.Convert(expression.Left);
            Expression right = converter.Convert(expression.Right);

            if (expression.NodeType == ExpressionType.AndAlso || expression.NodeType == ExpressionType.OrElse)
            {
                if (TypeHelper.NonNullableType(left.Type) == typeof(bool) && (left is MemberExpression || left is ConstantExpression))
                    left = Expression.Equal(left, Expression.Constant(true));

                if (TypeHelper.NonNullableType(right.Type) == typeof(bool) && (right is MemberExpression || right is ConstantExpression))
                    right = Expression.Equal(right, Expression.Constant(true));
            }

            if (left.Type != right.Type)
                right = Expression.Convert(right, left.Type);
                        
            return expression.Update(left, expression.Conversion, right);
        }
    }
}
