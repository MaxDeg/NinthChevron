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

namespace NinthChevron.Data.Converters.IQueryableConverters
{
    internal class JoinMethodConverter : BaseIQueryableMethodConverter
    {
        public JoinMethodConverter() : base("Join") { }

        public override Expression Call(ExpressionConverter converter, MethodCallExpression expression)
        {
            QueryExpression context = (QueryExpression)converter.Convert(expression.Arguments[0]);
            LambdaExpression lambda;

            lambda = converter.GetLambdaExpression(expression.Arguments[2]);
            Expression outerRef = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], context.Projection));

            lambda = converter.GetLambdaExpression(expression.Arguments[3]);
            Expression innerRef = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], (ConstantExpression)expression.Arguments[1]));

            if (outerRef.NodeType == (ExpressionType)ExtendedExpressionType.Query)
            {
                QueryExpression outerExp = (QueryExpression)outerRef;
                JoinExpression innerExp = (JoinExpression)innerRef;
                EntityExpression entity = new EntityExpression(context, expression.Arguments[1].Type.GetGenericArguments()[0]);
                
                ((EntityExpression)outerExp.From).Join(converter, entity, innerExp);
            }
                // Inner & Outer are JoinExpression
            else if (outerRef.NodeType == (ExpressionType)ExtendedExpressionType.Join && innerRef.NodeType == (ExpressionType)ExtendedExpressionType.Join)
            {
                JoinExpression outerExp = (JoinExpression)outerRef;
                JoinExpression innerExp = (JoinExpression)innerRef;
                EntityExpression entity = new EntityExpression(context, expression.Arguments[1].Type.GetGenericArguments()[0]);

                outerExp.Entity.Join(converter, entity, innerExp);
            }

            lambda = converter.GetLambdaExpression(expression.Arguments[4]);
            Expression projection = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], context), new Binding(lambda.Parameters[1], innerRef));

            context.Project(converter, lambda);

            return context;
        }
    }
}
