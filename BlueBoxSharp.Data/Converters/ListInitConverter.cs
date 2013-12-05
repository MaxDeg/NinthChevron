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

namespace BlueBoxSharp.Data.Converters
{
    internal class ListInitConverter : BaseConverter<ListInitExpression>
    {
        public ListInitConverter() : base(ExpressionType.ListInit) { }

        public override Expression Convert(ExpressionConverter converter, ListInitExpression expression)
        {
            bool notOnlyConstant = false;
            List<ElementInit> initializers = new List<ElementInit>();
            List<Expression> arguments;

            foreach (ElementInit element in expression.Initializers)
            {
                arguments = new List<Expression>();
                foreach (Expression arg in element.Arguments)
                {
                    Expression exp = converter.Convert(arg);
                    if (!(exp is ConstantExpression))
                        notOnlyConstant = true;

                    arguments.Add(exp);
                }

                initializers.Add(element.Update(arguments));
            }

            NewExpression constructExpr = (NewExpression)converter.Convert(expression.NewExpression);
            arguments = new List<Expression>();

            foreach (Expression arg in expression.NewExpression.Arguments)
            {
                Expression exp = converter.Convert(arg);
                if (!(exp is ConstantExpression))
                    notOnlyConstant = true;

                arguments.Add(exp);
            }
            constructExpr = constructExpr.Update(arguments);

            // If only constant arguments transform to constant
            if (!notOnlyConstant)
            {
                object list = constructExpr.Constructor.Invoke(
                    constructExpr.Arguments.Select(a => ((ConstantExpression)a).Value).ToArray()
                    );

                foreach (ElementInit element in initializers)
                    element.AddMethod.Invoke(list, element.Arguments.Select(a => ((ConstantExpression)a).Value).ToArray());

                return Expression.Constant(list);
            }


            return expression.Update(constructExpr, initializers);
        }
    }
}
