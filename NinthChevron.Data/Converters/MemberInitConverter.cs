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
    internal class MemberInitConverter : BaseConverter<MemberInitExpression>
    {
        private static List<ExpressionType> _conditionClause = new List<ExpressionType> 
        {
            ExpressionType.AndAlso,
            ExpressionType.OrElse,
            ExpressionType.Equal,
            ExpressionType.NotEqual,
            ExpressionType.GreaterThan,
            ExpressionType.GreaterThanOrEqual,
            ExpressionType.LessThan,
            ExpressionType.LessThanOrEqual
        };

        internal MemberInitConverter() : base(ExpressionType.MemberInit) { }

        public override Expression Convert(ExpressionConverter converter, MemberInitExpression expression)
        {
            List<MemberBinding> bindings = new List<MemberBinding>();
            foreach (MemberBinding binding in expression.Bindings)
                bindings.Add(ConvertBinding(converter, binding));

            return Expression.MemberInit(
                converter.Convert(expression.NewExpression) as NewExpression, 
                bindings
                );
        }

        private MemberBinding ConvertBinding(ExpressionConverter converter, MemberBinding bind)
        {
            if (bind is MemberAssignment)
                return this.ConvertBinding(converter, bind as MemberAssignment);
            else if (bind is MemberListBinding)
                return this.ConvertBinding(converter, bind as MemberListBinding);
            else if (bind is MemberMemberBinding)
                return this.ConvertBinding(converter, bind as MemberMemberBinding);

            return null;
        }

        private MemberBinding ConvertBinding(ExpressionConverter converter, MemberAssignment bind)
        {
            Expression exp = converter.Convert(bind.Expression);
            if (_conditionClause.Contains(exp.NodeType))
                exp = Expression.Condition(exp, Expression.Constant(true), Expression.Constant(false));

            return Expression.Bind(bind.Member, exp);
        }

        private MemberBinding ConvertBinding(ExpressionConverter converter, MemberListBinding bind)
        {
            throw new NotImplementedException();
        }

        private MemberBinding ConvertBinding(ExpressionConverter converter, MemberMemberBinding bind)
        {
            throw new NotImplementedException();
        }
    }
}
