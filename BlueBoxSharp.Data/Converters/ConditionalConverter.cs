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

namespace BlueBoxSharp.Data.Converters
{
    internal class ConditionalConverter : BaseConverter<ConditionalExpression>
    {
        internal ConditionalConverter() : base(ExpressionType.Conditional) { }

        public override Expression Convert(ExpressionConverter converter, ConditionalExpression expression)
        {
            Expression test = converter.Convert(expression.Test);
            if (test.NodeType == ExpressionType.Convert)
                test = ((UnaryExpression)test).Operand;

            if (test is ConstantExpression)
            {
                if ((bool)((ConstantExpression)test).Value)
                    return converter.Convert(expression.IfTrue);
                else
                    return converter.Convert(expression.IfFalse);
            }
            else
                return Expression.Condition(
                    test,
                    converter.Convert(expression.IfTrue),
                    converter.Convert(expression.IfFalse)
                    );
        }
    }
}
