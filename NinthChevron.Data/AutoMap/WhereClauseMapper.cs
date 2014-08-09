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
using NinthChevron.Helpers;

namespace NinthChevron.Data.AutoMap
{
    internal class WhereClauseMapper<TEntity>
    {
        private ParameterExpression _parameter;
        private MethodInfo _stringConcatMethod;

        public WhereClauseMapper()
        {
            this._parameter = Expression.Parameter(typeof(TEntity), "e");
            this._stringConcatMethod = typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string), typeof(string) });
        }

        public Expression<Func<TEntity, bool>> ResultExpression { get; private set; }

        private void EnterModel(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext)
        {
            if (parentContext != null)
                context.Data = parentContext.Data;
        }

        private void ExitModel(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext)
        {
            if (parentContext != null)
                parentContext.Data = context.Data;
            else if (context.Data != null)
                ResultExpression = Expression.Lambda<Func<TEntity, bool>>((Expression)context.Data, new ParameterExpression[] { this._parameter });
        }

        private void VisitProperty(PropertyDescriptor property, IModelVisitorContext context)
        {
            object propertyValue = property.GetValue(context.Model);
            if (propertyValue == null) return;

            Expression expression = null;
            List<string> properties;
            EntityMapAttribute entityMapAttr = property.Attributes.OfType<EntityMapAttribute>().FirstOrDefault();

            if (entityMapAttr == null) return;
            
            properties = entityMapAttr.Properties.ToList();

            if (entityMapAttr.Properties.Count() == 0)
                properties.Add(property.Name);

            // Get All properties mapped with the current one
            foreach (IEnumerable<PropertyDescriptor> mappedProperties in properties.Select(e => TypeDescriptorHelper.GetProperty<TEntity>(e)))
            {
                Expression propExpr = null;
                PropertyDescriptor entityProp = null;

                // Access member: . notation
                foreach (PropertyDescriptor descriptor in mappedProperties)
                {
                    if (propExpr == null)
                        propExpr = Expression.Property(this._parameter, descriptor.Name);
                    else
                        propExpr = Expression.Property(propExpr, descriptor.Name);

                    entityProp = descriptor;
                }

                if (expression == null)
                    expression = propExpr;
                else
                    expression = Expression.Call(this._stringConcatMethod, expression, Expression.Constant(" "), propExpr);
            }


            if (TypeHelper.IsNullable(expression.Type))
                expression = Expression.Property(expression, "Value");

            if (property.PropertyType == typeof(string))
            {
                Expression value = Expression.Constant(((string)propertyValue).Trim() + "%");

                MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                expression = Expression.Call(expression, containsMethod, value);
            }
            else if (property.PropertyType == typeof(IEnumerable<int>))
            {
                Expression value = Expression.Constant(propertyValue);

                MethodInfo containsMethod = typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static)
                                                .Where(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                                                .SingleOrDefault();
                expression = Expression.Call(null, containsMethod.MakeGenericMethod(typeof(int)), value, expression);
            }
            else if (TypeHelper.NonNullableType(property.PropertyType) == typeof(bool) && TypeHelper.NonNullableType(expression.Type) == typeof(int))
            {
                Expression value = Expression.Constant((bool)propertyValue ? 1 : 0);
                expression = Expression.Equal(expression, value);
            }
            else if (property.PropertyType.IsSubclassOf(typeof(Enum)) && TypeHelper.IsBaseType(expression.Type))
                expression = Expression.Equal(expression, Expression.Constant(Convert.ChangeType(propertyValue, expression.Type)));
            else
            {
                Expression value = Expression.Constant(propertyValue);
                expression = Expression.Equal(expression, value);
            }

            if (context.Data == null)
                context.Data = expression;
            else
                context.Data = Expression.MakeBinary(ExpressionType.AndAlso, (Expression)context.Data, expression);
        }

        public static WhereClauseMapper<TEntity> Visit<TModel>(object model)
        {
            WhereClauseMapper<TEntity> merger = new WhereClauseMapper<TEntity>();
            IModelVisitor visitor = ModelVisitorProvider.Default;

            visitor.OnEnterModel += merger.EnterModel;
            visitor.OnExitModel += merger.ExitModel;
            visitor.OnVisitProperty += merger.VisitProperty;

            visitor.Visit<TModel>(model);

            return merger;
        }
    }
}
