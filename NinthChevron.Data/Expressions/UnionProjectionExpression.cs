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
using System.Reflection;
using System.Text;

namespace NinthChevron.Data.Expressions
{
    public class UnionProjectionExpression : ProjectionExpression
    {
        public UnionProjectionExpression(ProjectionExpression expression)
            : base(expression)
        {
            this.Fields = expression.Fields.ToList();
        }

        public UnionProjectionExpression(Expression expression) : base(expression) { }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.UnionProjection; }
        }

        protected override IEnumerable<ProjectionItem> FindProjection(Expression expression, MemberInfo member)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
                return FindProjection(expression as MemberExpression, member).ToList();
            else if (expression.NodeType == ExpressionType.New)
                return FindProjection(expression as NewExpression, member).ToList();
            else if (expression.NodeType == ExpressionType.MemberInit)
                return FindProjection(expression as MemberInitExpression, member).ToList();
            else if (expression.NodeType == ExpressionType.Call)
                return FindProjection(expression as MethodCallExpression, member).ToList();
            else if (expression is BinaryExpression)
                return FindProjection(expression as BinaryExpression, member).ToList();
            else if (expression.NodeType == ExpressionType.Convert)
                return FindProjection(expression as UnaryExpression, member).ToList();

            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.Projection)
                return FindProjection((ProjectionExpression)expression, member).ToList();
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.Entity)
                return FindProjection((EntityExpression)expression, member).ToList();
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.Exists)
                return FindProjection((ExistsExpression)expression, member).ToList();
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.AliasedExpression)
                return new ProjectionItem[] { new ProjectionItem(expression, member, ((AliasedExpression)expression).Alias) };

            else
                return new ProjectionItem[] { new ProjectionItem(new AliasedExpression(expression, "Extent" + this._index), member, "Extent" + this._index++) };
        }

        protected override IEnumerable<ProjectionItem> FindProjection(ExistsExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(
                new AliasedExpression(Expression.Condition(expression, Expression.Constant(1), Expression.Constant(0)), "Extent" + this._index),
                member, "Extent" + this._index++);
        }
    }
}
