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
using BlueBoxSharp.Helpers;

namespace BlueBoxSharp.Data.Translators
{
    public abstract class SqlExpressionVisitor
    {
        public string Visit(Expression node)
        {
            if (node == null) return null;

            switch (node.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Divide:
                case ExpressionType.AndAlso:
                case ExpressionType.Modulo:
                case ExpressionType.Multiply:
                case ExpressionType.OrElse:
                case ExpressionType.Assign:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Coalesce:
                    return Visit((BinaryExpression)node);
                case ExpressionType.Call:
                    return Visit((MethodCallExpression)node);
                case ExpressionType.Conditional:
                    return Visit((ConditionalExpression)node);
                case ExpressionType.Constant:
                    return Visit((ConstantExpression)node);
                case ExpressionType.Parameter:
                    return Visit((ParameterExpression)node);
                case ExpressionType.MemberAccess:
                    MemberExpression expr = (MemberExpression)node;
                    // If we try to access Value member of Nullable just skip it
                    if (TypeHelper.IsNullable(expr.Member.DeclaringType) && expr.Member.Name == "Value")
                        return Visit(expr.Expression);
                    else
                        return Visit(expr);
                case ExpressionType.Convert:
                case ExpressionType.Not:
                    return Visit((UnaryExpression)node);
                case ExpressionType.ListInit:
                    return Visit((ListInitExpression)node);
                case ExpressionType.MemberInit:
                    return Visit((MemberInitExpression)node);
                case ExpressionType.New:
                    return Visit((NewExpression)node);
                /* 
                 ExtendedExpressionType
                 */
                case (ExpressionType)ExtendedExpressionType.Query:
                    return Visit((QueryExpression)node);
                case (ExpressionType)ExtendedExpressionType.Insert:
                    return Visit((InsertExpression)node);
                case (ExpressionType)ExtendedExpressionType.Update:
                    return Visit((UpdateExpression)node);
                case (ExpressionType)ExtendedExpressionType.Delete:
                    return Visit((DeleteExpression)node);
                case (ExpressionType)ExtendedExpressionType.Union:
                    return Visit((UnionQueryExpression)node);
                case (ExpressionType)ExtendedExpressionType.Count:
                case (ExpressionType)ExtendedExpressionType.Max:
                case (ExpressionType)ExtendedExpressionType.Min:
                case (ExpressionType)ExtendedExpressionType.Sum:
                case (ExpressionType)ExtendedExpressionType.Average:
                    return Visit((AggregateExpression)node);
                case (ExpressionType)ExtendedExpressionType.Projection:
                    return Visit((ProjectionExpression)node);
                case (ExpressionType)ExtendedExpressionType.GroupByProjection:
                    return Visit((GroupByProjectionExpression)node);
                case (ExpressionType)ExtendedExpressionType.UnionProjection:
                    return Visit((UnionProjectionExpression)node);
                case (ExpressionType)ExtendedExpressionType.OrderBy:
                    return Visit((OrderByExpression)node);
                case (ExpressionType)ExtendedExpressionType.Entity:
                    return Visit((EntityExpression)node);
                case (ExpressionType)ExtendedExpressionType.EntityReference:
                    return Visit((EntityRefExpression)node);
                case (ExpressionType)ExtendedExpressionType.Join:
                    return Visit((JoinExpression)node);
                case (ExpressionType)ExtendedExpressionType.Exists:
                    return Visit((ExistsExpression)node);
                case (ExpressionType)ExtendedExpressionType.AliasedExpression:
                    return Visit((AliasedExpression)node);
                                    
                default:
                    return string.Empty;
            }
        }

        public abstract string Visit(BinaryExpression node);
        public abstract string Visit(UnaryExpression node);
        public abstract string Visit(MethodCallExpression node);
        public abstract string Visit(ConditionalExpression node);
        public abstract string Visit(ConstantExpression node);
        public abstract string Visit(ParameterExpression node);
        public abstract string Visit(MemberExpression node);
        public abstract string Visit(ListInitExpression node);
        public abstract string Visit(MemberInitExpression node);
        public abstract string Visit(NewExpression node);

        /* 
         ExtendedExpressionType
         */
        public abstract string Visit(QueryExpression node);
        public abstract string Visit(DeleteExpression node);
        public abstract string Visit(InsertExpression node);
        public abstract string Visit(UpdateExpression node);
        public abstract string Visit(UnionQueryExpression node);
        public abstract string Visit(AggregateExpression node);
        public abstract string Visit(ProjectionExpression node);
        public abstract string Visit(GroupByProjectionExpression node);
        public abstract string Visit(UnionProjectionExpression node);
        public abstract string Visit(OrderByExpression node);
        public abstract string Visit(EntityExpression node);
        public abstract string Visit(EntityRefExpression node);
        public abstract string Visit(JoinExpression node);
        public abstract string Visit(ExistsExpression node);
        public abstract string Visit(AliasedExpression node);
    }
}
