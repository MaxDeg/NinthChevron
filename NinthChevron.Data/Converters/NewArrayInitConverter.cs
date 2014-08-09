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

namespace NinthChevron.Data.Converters
{
    internal class NewArrayInitConverter : BaseConverter<NewArrayExpression>
    {
        public NewArrayInitConverter() : base(ExpressionType.NewArrayInit) { }

        public override Expression Convert(ExpressionConverter converter, NewArrayExpression expression)
        {
            bool notOnlyConstant = false;
            List<Expression> expressions = new List<Expression>();

            foreach (Expression element in expression.Expressions)
            {
                Expression exp = converter.Convert(element);
                expressions.Add(exp);

                if (exp.NodeType != ExpressionType.Constant)
                    notOnlyConstant = true;
            }

            // If only constant arguments transform to constant
            if (!notOnlyConstant)
            {
                Array array = Array.CreateInstance(expression.Type.GetElementType(), expressions.Count);
                for (int i = 0; i < expressions.Count; i++)
                    array.SetValue(((ConstantExpression)expressions[i]).Value, i);

                return Expression.Constant(array, expression.Type);
            }

            return expression.Update(expressions);
        }
    }
}
