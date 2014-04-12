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
using BlueBoxSharp.Data.Entity;

namespace BlueBoxSharp.Data.Expressions
{
    /*
     * TODO: Refactoring: FindProjection more generic way
     */
    public class ProjectionExpression : Expression
    {
        private Type _type;
        protected int _index = 0;

        protected ProjectionExpression() { }

        public ProjectionExpression(Expression expression)
        {
            this._type = expression.Type;
            this.Fields = FindProjection(expression, null).ToList();
        }
        
        public List<ProjectionItem> Fields { get; protected set; }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.Projection; }
        }

        public override Type Type
        {
            get { return this._type; }
        }

        public virtual bool TryFindMember(MemberInfo member, out AliasedExpression result)
        {
            result = Fields
                .Where(f => f.Type == ProjectionItemType.Projection)
                .Where(f => f.Expression.Expression is MemberExpression
                    && ((MemberExpression)f.Expression.Expression).Member == member || (f.Member != null && f.Member.Name == member.Name && f.Member.DeclaringType == member.DeclaringType))
                .Select(f => f.Expression)
                .FirstOrDefault();

            if (result == null)
                result = Fields
                    .Where(f => f.Type == ProjectionItemType.Projection)
                    .Where(f => f.Member != null && f.Member.Name == member.Name)
                    .Select(f => f.Expression)
                    .FirstOrDefault();

            return result != null;
        }

        public override string ToString()
        {
            return string.Join(", \r\n", this.Fields.Where(f => f.Type == ProjectionItemType.Projection));
        }



        protected virtual IEnumerable<ProjectionItem> FindProjection(Expression expression, MemberInfo member)
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
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.GroupByProjection)
                return FindProjection((GroupByProjectionExpression)expression, member).ToList();
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.Entity)
                return FindProjection((EntityExpression)expression, member).ToList();
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.Join)
                return FindProjection((JoinExpression)expression, member).ToList();
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.Exists)
                return FindProjection((ExistsExpression)expression, member).ToList();
            else if (expression.NodeType == (ExpressionType)ExtendedExpressionType.AliasedExpression)
                return new ProjectionItem[] { new ProjectionItem(expression, member, ((AliasedExpression)expression).Alias) };

            else
                return new ProjectionItem[] { new ProjectionItem(expression, member, "Extent" + this._index++) };
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(EntityExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(new EntityProjectionExpression((EntityExpression)expression), member, "Extent" + this._index++);
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(JoinExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(new EntityProjectionExpression(((JoinExpression)expression).Entity), member, "Extent" + this._index++);
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(MemberExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(expression, member ?? expression.Member, "Extent" + this._index++);
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(NewExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(expression, member);

            for (int i = 0; i < expression.Arguments.Count; i++)
            {
                Expression arg = expression.Arguments[i];
                MemberInfo info = null;

                if(expression.Members != null)
                    info = expression.Members[i];

                foreach (ProjectionItem innerProj in FindProjection(arg, info))
                    yield return innerProj;
            }
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(MemberInitExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(expression, member);

            foreach (ProjectionItem item in FindProjection(expression.NewExpression, member).Skip(1))
                yield return item;

            foreach (MemberAssignment arg in expression.Bindings)
                foreach (ProjectionItem innerProj in FindProjection(arg.Expression, arg.Member))
                    yield return innerProj;
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(MethodCallExpression expression, MemberInfo member)
        {
            var customAttr = expression.Method.GetCustomAttributes(typeof(SqlFunctionAttribute), false);
            if (customAttr != null && customAttr.Length > 0)
                yield return new ProjectionItem(expression, member, "Extent" + this._index++);
            else
            {
                if (expression.Object != null && expression.Object.NodeType != ExpressionType.Constant)
                    yield return new ProjectionItem(expression.Object, member, "Extent" + this._index++);

                foreach (Expression expr in expression.Arguments)
                    yield return new ProjectionItem(expr, member, "Extent" + this._index++);
            }
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(ExistsExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(Expression.Condition(expression, Expression.Constant(1), Expression.Constant(0)), member, "Extent" + this._index++);
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(BinaryExpression expression, MemberInfo member)
        {
            yield return new ProjectionItem(expression, member, "Extent" + this._index++);
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(UnaryExpression expression, MemberInfo member)
        {
            return FindProjection(expression.Operand, member);
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(ProjectionExpression expression, MemberInfo member)
        {
            if (expression is EntityProjectionExpression)
            {
                yield return new ProjectionItem(expression, member, "Extent" + this._index++);
            }
            else
            {
                foreach (ProjectionItem item in expression.Fields)
                {
                    foreach (ProjectionItem innerProj in FindProjection(item.Expression.Expression, item.Member))
                        yield return innerProj;
                }
            }
        }

        protected virtual IEnumerable<ProjectionItem> FindProjection(GroupByProjectionExpression expression, MemberInfo member)
        {
            foreach (ProjectionItem item in expression.Fields)
            {
                foreach (ProjectionItem innerProj in FindProjection(item.Expression.Expression, member))
                    yield return innerProj;
            }
        }
    }
}
