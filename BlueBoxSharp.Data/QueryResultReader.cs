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
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Data.Expressions;
using BlueBoxSharp.Helpers;

namespace BlueBoxSharp.Data
{
    internal class QueryResultReader
    {
        private IEnumerable<ProjectionItem> _items;

        internal QueryResultReader(ProjectionExpression projection)
        {
            this._items = projection.Fields;
        }

        internal object Read(object[] values)
        {
            int index = 0;
            ProjectionItem item = this._items.First();

            object result = VisitProjection(null, item.Expression, values, ref index);
            if (result is IInternalEntity)
                ((IInternalEntity)result).ResetChangeTracker();

            return result;
        }

        private object VisitProjection(object model, Expression expression, object[] values, ref int index)
        {
            if (expression.NodeType == ExpressionType.New)
                return ConstructNewObject((NewExpression)expression, values, ref index);
            else if (expression.NodeType == ExpressionType.MemberInit)
                return InitNewObjectMember(model, (MemberInitExpression)expression, values, ref index);
            else if (expression.NodeType == ExpressionType.Call)
                return InitMethodCall((MethodCallExpression)expression, values, ref index);
            else if (expression.NodeType == ExpressionType.Convert)
                return VisitProjection(model, ((UnaryExpression)expression).Operand, values, ref index);
            else
            {
                object value = values[index++];
                if (value == DBNull.Value)
                    return null;

                return value;
            }
        }

        private object ConstructNewObject(NewExpression expression, object[] values, ref int index)
        {
            int i = 0;
            object[] parameters = new object[expression.Arguments.Count];

            foreach (Expression arg in expression.Arguments)
            {
                object value = VisitProjection(null, arg, values, ref index);

                if (value != null && TypeHelper.NonNullableType(arg.Type) == typeof(bool) && TypeHelper.NonNullableType(value.GetType()) == typeof(int))
                    parameters[i++] = (int)value == 1;
                else if (value != null && TypeHelper.NonNullableType(value.GetType()) != TypeHelper.NonNullableType(arg.Type)
                            && !arg.Type.IsSubclassOf(typeof(Enum)) && !arg.Type.IsAssignableFrom(value.GetType()))
                    parameters[i++] = Convert.ChangeType(value, TypeHelper.NonNullableType(arg.Type));
                else
                    parameters[i++] = value;
            }

            return expression.Constructor.Invoke(parameters);
        }

        private object InitNewObjectMember(object model, MemberInitExpression expression, object[] values, ref int index)
        {
            object obj;
            if (model == null)
                obj = ConstructNewObject(expression.NewExpression, values, ref index);
            else
                obj = model;

            foreach (MemberBinding binding in expression.Bindings)
            {
                if (binding.BindingType != MemberBindingType.Assignment) 
                    continue;

                MemberAssignment assignment = (MemberAssignment)binding;
                PropertyInfo property = (PropertyInfo)assignment.Member;
                object tmp = property.GetValue(obj, null);
                object value = VisitProjection(tmp, assignment.Expression, values, ref index);

                SetValue(property, obj, value);
            }

            return obj;
        }

        private object InitMethodCall(MethodCallExpression expression, object[] values, ref int index)
        {
            object target = null;
            if (expression.Object != null && expression.Object.NodeType != ExpressionType.Constant)
                target = VisitProjection(null, expression.Object, values, ref index);
            else if (expression.Object != null)
                target = ((ConstantExpression)expression.Object).Value;

            int i = 0;
            object[] parameters = new object[expression.Arguments.Count];

            foreach (Expression arg in expression.Arguments)
                parameters[i++] = VisitProjection(null, arg, values, ref index);

            return expression.Method.Invoke(target, parameters);
        }

        private void SetValue(PropertyInfo property, object model, object value)
        {
            if (value != null && TypeHelper.NonNullableType(property.PropertyType) == typeof(bool) && TypeHelper.NonNullableType(value.GetType()) == typeof(int))
                property.SetValue(model, (int)value == 1, null);
            else if (value != null && TypeHelper.NonNullableType(value.GetType()) != TypeHelper.NonNullableType(property.PropertyType) 
                        && !property.PropertyType.IsSubclassOf(typeof(Enum)) && !property.PropertyType.IsAssignableFrom(value.GetType()))
                property.SetValue(model, Convert.ChangeType(value, TypeHelper.NonNullableType(property.PropertyType)), null);
            else
                property.SetValue(model, value, null);
        }
    }
}