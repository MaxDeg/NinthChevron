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
using BlueBoxSharp.Helpers;

namespace BlueBoxSharp.Data.Converters
{
    internal class UnaryConverter : BaseConverter<UnaryExpression>
    {
        internal UnaryConverter() : base(ExpressionType.Convert, ExpressionType.Negate, ExpressionType.Not) { }

        public override Expression Convert(ExpressionConverter converter, UnaryExpression expression)
        {
            Expression operand = converter.Convert(expression.Operand);

            if (operand is ConstantExpression && expression.NodeType == ExpressionType.Convert)
            {
                ConstantExpression constant = operand as ConstantExpression;

                if (!TypeHelper.IsNullable(expression.Type))
                    return Expression.Constant(System.Convert.ChangeType(constant.Value , expression.Type));
            }
            else if (expression.NodeType == ExpressionType.Not)
                if (TypeHelper.NonNullableType(operand.Type) == typeof(bool) && (operand is MemberExpression || operand is ConstantExpression))
                    return Expression.Equal(operand, Expression.Constant(false));

            return expression.Update(operand);
        }
    }
}
