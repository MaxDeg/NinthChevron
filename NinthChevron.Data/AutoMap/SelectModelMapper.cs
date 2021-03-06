﻿/**
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
    internal class SelectModelMapper<TEntity, TModel>
    {
        private ParameterExpression _parameter;
        private MethodInfo _stringConcatMethod;
        private MemberInitExpression _baseBindings;

        public SelectModelMapper() : this(null) { }

        public SelectModelMapper(Expression<Func<TEntity, TModel>> baseBindings)
        {
            if (baseBindings == null)
                this._parameter = Expression.Parameter(typeof(TEntity), "e");
            else
            {
                this._parameter = baseBindings.Parameters[0];
                this._baseBindings = (MemberInitExpression)baseBindings.Body;
            }

            this._stringConcatMethod = typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string), typeof(string) });
        }

        public Expression<Func<TEntity, TModel>> ResultExpression { get; private set; }


        private void EnterModel(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext)
        {
            ContextData data = new ContextData
            {
                Bindings = new Dictionary<string, MemberBinding>(),
                BaseBindings = parentContext == null ? this._baseBindings : ((ContextData)parentContext.Data).BaseBindings
            };
            context.Data = data;

            Type modelType = typeof(TModel);
            if (property != null) modelType = property.PropertyType;

            if (data.BaseBindings != null)
            {
                foreach (MemberBinding bind in data.BaseBindings.Bindings)
                {
                    Expression bindExpression = ((MemberAssignment)bind).Expression;

                    if (property != null && bind.Member.Name == property.Name && bindExpression is MemberInitExpression)
                        data.BaseBindings = (MemberInitExpression)bindExpression;

                    PropertyInfo entityProperty = modelType.GetProperty(bind.Member.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (entityProperty != null)
                        data.Bindings.Add(entityProperty.Name, Expression.Bind(entityProperty, bindExpression));
                }
            }
        }

        private void ExitModel(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext)
        {
            ContextData data = (ContextData)context.Data;

            if (property != null && !((ContextData)parentContext.Data).Bindings.ContainsKey(property.Name))
            {
                if (property.PropertyType.GetConstructor(Type.EmptyTypes) == null)
                    return;
                MemberInitExpression memberInit = Expression.MemberInit(
                    Expression.New(property.PropertyType),
                    data.Bindings.Values
                    );

                ((ContextData)parentContext.Data).Bindings.Add(
                    property.Name,
                    Expression.Bind(property.ComponentType.GetProperty(property.Name), memberInit)
                    );
            }
            else if (property == null && parentContext == null)
                this.ResultExpression = Expression.Lambda<Func<TEntity, TModel>>(
                    Expression.MemberInit(
                        Expression.New(typeof(TModel)),
                        data.Bindings.Values
                    ),
                    new ParameterExpression[] { this._parameter }
                    );
        }

        private void VisitProperty(PropertyDescriptor property, IModelVisitorContext context)
        {
            if (property.PropertyType == typeof(IEnumerable<int>)) return;

            ContextData data = (ContextData)context.Data;
            if (data.Bindings.ContainsKey(property.Name)) return;

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


                if (property.PropertyType != entityProp.PropertyType && !entityProp.PropertyType.IsSubclassOf(property.PropertyType))
                {
                    bool isBoolProperty = TypeHelper.NonNullableType(property.PropertyType) == typeof(bool);
                    bool isIntProperty = TypeHelper.NonNullableType(entityProp.PropertyType) == typeof(int);

                    if (isBoolProperty && isIntProperty && !TypeHelper.IsNullable(entityProp.PropertyType))
                    {
                        Expression value = Expression.Constant(1);
                        propExpr = Expression.Equal(propExpr, value);
                    }
                    else if (isBoolProperty && isIntProperty)
                    {
                        Expression value = Expression.Constant(1);

                        MemberInfo valueMember = entityProp.PropertyType.GetMember("Value")[0];
                        propExpr = Expression.Equal(Expression.MakeMemberAccess(propExpr, valueMember), value);
                    }
                    else
                        propExpr = Expression.Convert(propExpr, property.PropertyType);
                }

                if (expression == null)
                    expression = propExpr;
                else
                    expression = Expression.Call(this._stringConcatMethod, expression, Expression.Constant(" "), propExpr);
            }

            data.Bindings.Add(property.Name, Expression.Bind(property.ComponentType.GetProperty(property.Name), expression));
        }


        public static SelectModelMapper<TEntity, TModel> Visit(Expression<Func<TEntity, TModel>> baseBindings)
        {
            SelectModelMapper<TEntity, TModel> merger = new SelectModelMapper<TEntity, TModel>(baseBindings);
            IModelVisitor visitor = ModelVisitorProvider.Default;

            visitor.OnEnterModel += merger.EnterModel;
            visitor.OnExitModel += merger.ExitModel;
            visitor.OnVisitProperty += merger.VisitProperty;

            visitor.Visit<TModel>(null);
            return merger;
        }


        private class ContextData
        {
            public Dictionary<string, MemberBinding> Bindings { get; set; }
            public MemberInitExpression BaseBindings { get; set; }
        }
    }
}
