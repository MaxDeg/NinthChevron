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
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using BlueBoxSharp.Data.Converters;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.Data.Metadata;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Data.AutoMap;

namespace BlueBoxSharp.Data
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> WithIndex<TSource, TKey>(this IQueryable<TSource> enumerable, Expression<Func<TSource, TKey>> propertySelector, params string[] indexes)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (propertySelector == null)
                throw new ArgumentNullException("propertySelector");
            if (indexes == null)
                throw new ArgumentNullException("indexes");

            return enumerable.Provider.CreateQuery<TSource>(
                Expression.Call(
                    null,
                    typeof(EntityQueryable).GetMethod("WithIndex").MakeGenericMethod(typeof(TSource), typeof(TKey)),
                    new Expression[] { enumerable.Expression, propertySelector, Expression.Constant(indexes.Select(i => Expression.Constant(i))) }
                    ));
        }
        
        public static IQueryable<TResult> SelectWith<TSource, TResult>(this IQueryable<TSource> enumerable, Expression<Func<TSource, TResult>> selector)
            where TSource : IEntity
        {
            SelectModelMapper<TSource, TResult> visitor = SelectModelMapper<TSource, TResult>.Visit(selector);

            return Queryable.Select(enumerable, visitor.ResultExpression);
        }

        public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> enumerable)
            where TSource : IEntity
        {
            SelectModelMapper<TSource, TResult> visitor = SelectModelMapper<TSource, TResult>.Visit(null);

            return Queryable.Select(enumerable, visitor.ResultExpression);
        }

        public static IQueryable<TSource> Where<TSource, TModel>(this IQueryable<TSource> enumerable, TModel model)
            where TSource : IEntity
        {
            WhereClauseMapper<TSource> visitor = WhereClauseMapper<TSource>.Visit<TModel>(model);

            if (visitor.ResultExpression != null)
                return Queryable.Where(enumerable, visitor.ResultExpression);
            else
                return enumerable;
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> enumerable, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return Queryable.Where(enumerable, predicate);
            else
                return enumerable;
        }

        public static IQueryable<TSource> Join<TSource, TKey>(this IQueryable<TSource> enumerable, Expression<Func<TSource, TKey>> propertySelector)
            where TKey : Entity<TKey>, new()
        {
            ExpressionConverter converter = new ExpressionConverter(null);
            LambdaExpression lambda = converter.GetLambdaExpression(propertySelector);

            if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpr = (MemberExpression)lambda.Body;
                DataContext context = ((IInternalQuery)enumerable).Context;
                TableMetadata meta = MappingProvider.GetMetadata(memberExpr.Expression.Type);
                Expression innerKeySelector;

                if (meta.TryGetJoin(memberExpr.Member.Name, out innerKeySelector) != JoinType.None)
                    return Queryable.Join<TSource, TKey, TKey, TSource>(enumerable, (IQueryable<TKey>)context.Query<TKey>(), propertySelector, i => i, (o, i) => o);
            }

            return enumerable;
        }

        public static IQueryable<TProjection> LeftJoin<TSource, TJoinSource, TProjection>(
                        this IQueryable<TSource> enumerable,
                        IQueryable<TJoinSource> joinEnumerable,
                        Expression<Func<TSource, TJoinSource, bool>> clause,
                        Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector)
            where TSource : Entity<TSource>, new()
        {
            return Join(enumerable, JoinType.Left, p => p, joinEnumerable, clause, projectionSelector);
        }

        public static IQueryable<TProjection> RightJoin<TSource, TJoinSource, TProjection>(
                        this IQueryable<TSource> enumerable,
                        IQueryable<TJoinSource> joinEnumerable,
                        Expression<Func<TSource, TJoinSource, bool>> clause,
                        Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector)
            where TSource : Entity<TSource>, new()
        {
            return Join(enumerable, JoinType.Right, p => p, joinEnumerable, clause, projectionSelector);
        }

        public static IQueryable<TProjection> InnerJoin<TSource, TJoinSource, TProjection>(
                        this IQueryable<TSource> enumerable,
                        IQueryable<TJoinSource> joinEnumerable,
                        Expression<Func<TSource, TJoinSource, bool>> clause,
                        Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector)
            where TSource : Entity<TSource>, new()
        {
            return Join(enumerable, JoinType.Inner, p => p, joinEnumerable, clause, projectionSelector);
        }

        public static IQueryable<TProjection> LeftJoin<TSource, TKey, TJoinSource, TProjection>(
                        this IQueryable<TSource> enumerable,
                        Expression<Func<TSource, TKey>> propertySelector,
                        IQueryable<TJoinSource> joinEnumerable,
                        Expression<Func<TKey, TJoinSource, bool>> clause,
                        Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector)
            where TKey : Entity<TKey>, new()
        {
            return Join(enumerable, JoinType.Left, propertySelector, joinEnumerable, clause, projectionSelector);
        }

        public static IQueryable<TProjection> RightJoin<TSource, TKey, TJoinSource, TProjection>(
                        this IQueryable<TSource> enumerable,
                        Expression<Func<TSource, TKey>> propertySelector,
                        IQueryable<TJoinSource> joinEnumerable,
                        Expression<Func<TKey, TJoinSource, bool>> clause,
                        Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector)
            where TKey : Entity<TKey>, new()
        {
            return Join(enumerable, JoinType.Right, propertySelector, joinEnumerable, clause, projectionSelector);
        }

        public static IQueryable<TProjection> InnerJoin<TSource, TKey, TJoinSource, TProjection>(
                        this IQueryable<TSource> enumerable,
                        Expression<Func<TSource, TKey>> propertySelector,
                        IQueryable<TJoinSource> joinEnumerable,
                        Expression<Func<TKey, TJoinSource, bool>> clause,
                        Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector)
            where TKey : Entity<TKey>, new()
        {
            return Join(enumerable, JoinType.Inner, propertySelector, joinEnumerable, clause, projectionSelector);
        }

        private static IQueryable<TProjection> Join<TSource, TKey, TJoinSource, TProjection>(
                        this IQueryable<TSource> enumerable,
                        JoinType type,
                        Expression<Func<TSource, TKey>> propertySelector,
                        IQueryable<TJoinSource> joinEnumerable,
                        Expression<Func<TKey, TJoinSource, bool>> clause,
                        Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector)
            where TKey : Entity<TKey>, new()
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");
            if (joinEnumerable == null)
                throw new ArgumentNullException("joinEnumerable");
            if (clause == null)
                throw new ArgumentNullException("clause");
            if (propertySelector == null)
                throw new ArgumentNullException("propertySelector");

            return enumerable.Provider.CreateQuery<TProjection>(
                Expression.Call(
                    null,
                    typeof(EntityQueryable).GetMethod("DynamicJoin").MakeGenericMethod(typeof(TSource), typeof(TKey), typeof(TJoinSource), typeof(TProjection)),
                    new Expression[] { enumerable.Expression, Expression.Constant(type), propertySelector, joinEnumerable.Expression, clause, projectionSelector }
                    ));
        }

        public static IQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> enumerable, bool condition, Expression<Func<TSource, TKey>> keySelector)
        {
            if (condition)
                return Queryable.OrderBy(enumerable, keySelector);
            else
                return enumerable;
        }

        public static IQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> enumerable, bool condition, Expression<Func<TSource, TKey>> keySelector)
        {
            if (condition)
                return Queryable.OrderByDescending(enumerable, keySelector);
            else
                return enumerable;
        }

        public static IQueryable<TSource> OrderBy<TSource, TModel>(this IQueryable<TSource> enumerable, bool condition, string sortProperty)
        {
            if (condition && !string.IsNullOrEmpty(sortProperty))
                return Queryable.OrderBy(enumerable, VisitOrderBy<TModel, TSource>(sortProperty));
            else
                return enumerable;
        }

        public static IQueryable<TSource> OrderByDescending<TSource, TModel>(this IQueryable<TSource> enumerable, bool condition, string sortProperty)
        {
            if (condition && !string.IsNullOrEmpty(sortProperty))
                return Queryable.OrderByDescending(enumerable, VisitOrderBy<TModel, TSource>(sortProperty));
            else
                return enumerable;
        }

        public static IQueryable<TSource> OrderBy<TSource, TModel>(this IQueryable<TSource> enumerable, string sortProperty)
        {
            Expression<Func<TSource, object>> exp = VisitOrderBy<TModel, TSource>(sortProperty);

            if (exp != null)
                return Queryable.OrderBy(enumerable, VisitOrderBy<TModel, TSource>(sortProperty));
            else
                return enumerable;
        }

        public static IQueryable<TSource> OrderByDescending<TSource, TModel>(this IQueryable<TSource> enumerable, string sortProperty)
        {
            Expression<Func<TSource, object>> exp = VisitOrderBy<TModel, TSource>(sortProperty);

            if (exp != null)
                return Queryable.OrderByDescending(enumerable, exp);
            else
                return enumerable;
        }

        private static Expression<Func<TSource, object>> VisitOrderBy<TModel, TSource>(string sortProperty)
        {
            if (string.IsNullOrEmpty(sortProperty))
                return null;

            PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(TModel));
            PropertyDescriptor property = collection.Find(sortProperty, true);
            if (property == null) property = collection.Find("Id", true);

            try
            {
                EntityMapAttribute entityMapAttr = property.Attributes.OfType<EntityMapAttribute>().FirstOrDefault();

                if (entityMapAttr == null)
                    throw new Exception("OrderBy must be used on EntityMapped properties only!");

                List<string> properties = new List<string>();
                if (!string.IsNullOrEmpty(entityMapAttr.SortKey))
                    properties.Add(entityMapAttr.SortKey);
                else if (entityMapAttr.Properties.Count() > 0)
                    properties = entityMapAttr.Properties.ToList();
                else
                    properties.Add(property.Name);

                ParameterExpression parameterExpr = Expression.Parameter(typeof(TSource), "e");
                Expression expression = null;

                if (typeof(IEntity).IsAssignableFrom(typeof(TSource)))
                {
                    foreach (IEnumerable<PropertyDescriptor> descriptorPipeline in properties.Select(e => TypeDescriptorHelper.GetProperty<TSource>(e)))
                    {
                        Expression propExpr = null;

                        foreach (PropertyDescriptor descriptor in descriptorPipeline)
                        {
                            if (propExpr == null)
                                propExpr = Expression.Property(parameterExpr, descriptor.Name);
                            else
                                propExpr = Expression.Property(propExpr, descriptor.Name);
                        }

                        if (expression == null)
                            expression = propExpr;
                        else
                        {
                            MethodInfo concatMethod = typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string), typeof(string) });
                            expression = Expression.Call(concatMethod, expression, Expression.Constant(" "), propExpr);
                        }
                    }
                }
                else
                    expression = Expression.Property(parameterExpr, typeof(TModel).GetProperty(property.Name));


                return Expression.Lambda<Func<TSource, object>>(
                    Expression.Convert(expression, typeof(object)), new ParameterExpression[] { parameterExpr }
                    );
            }
            catch (Exception e)
            {
                throw new Exception("Property: " + property.Name, e);
            }
        }


        public static void Insert<TSource, TModel>(this IQueryable<TSource> enumerable, Expression<Func<TSource, TModel>> selector)
            where TSource : IEntity
        {
            enumerable.Provider.Execute<TSource>(
                Expression.Call(
                    null,
                    ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(typeof(TSource), typeof(TModel)),
                    new Expression[] { enumerable.Expression, selector }
                    ));
        }

        public static void Insert<TSource>(this IQueryable<TSource> enumerable)
            where TSource : IEntity
        {
            enumerable.Provider.Execute<TSource>(
                Expression.Call(
                    null,
                    ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(typeof(TSource)),
                    new Expression[] { enumerable.Expression }
                    ));
        }

        public static void Delete<TSource>(this IQueryable<TSource> enumerable)
            where TSource : IEntity
        {
            enumerable.Provider.Execute<TSource>(
                Expression.Call(
                    null,
                    ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(typeof(TSource)),
                    new Expression[] { enumerable.Expression }
                    ));
        }

        public static void Update<TSource>(this IQueryable<TSource> enumerable, Expression<Func<TSource, TSource>> selector)
            where TSource : IEntity
        {
            enumerable.Provider.Execute<TSource>(
                Expression.Call(
                    null,
                    ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(typeof(TSource)),
                    new Expression[] { enumerable.Expression, selector }
                    ));
        }
    }
}
