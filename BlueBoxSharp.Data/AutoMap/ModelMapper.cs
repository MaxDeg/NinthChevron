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
using System.Text;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;

namespace BlueBoxSharp.Data.AutoMap
{
    internal class ModelMapper<TEntity>
    {
        private TEntity _entity; 
        private bool _isPreLoading;

        private ModelMapper(TEntity entity, bool isPreloading)
        {
            this._entity = entity;
            this._isPreLoading = isPreloading;
        }

        private void EnterModel(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext)
        {
            context.Data = true;

            if (parentContext != null)
                context.Model = property.GetValue(parentContext.Model);
        }

        private void ExitModel(PropertyDescriptor property, IModelVisitorContext context, IModelVisitorContext parentContext) { }

        private void VisitProperty(PropertyDescriptor property, IModelVisitorContext context)
        {
            if (!(bool)context.Data)
                return;

            PropertyDescriptor entityProp = null;
            object entityObject = this._entity;


            EntityMapAttribute entityMapAttr = property.Attributes.OfType<EntityMapAttribute>().FirstOrDefault();
            EntityUpdateAttribute entityUpdateAttr = property.Attributes.OfType<EntityUpdateAttribute>().FirstOrDefault();

            if (entityMapAttr == null && entityUpdateAttr == null)
                return;

            List<string> properties;
            if (entityMapAttr != null && (!entityMapAttr.IsReadOnly || this._isPreLoading))
                properties = entityMapAttr.Properties.ToList();
            else
                properties = new List<string>();

            if (entityMapAttr != null && entityMapAttr.Properties.Count() == 0 && (!entityMapAttr.IsReadOnly || this._isPreLoading))
                properties.Add(property.Name);
            if (entityUpdateAttr != null)
                properties.AddRange(entityUpdateAttr.Properties);

            // If the property is entity mapped with multiple we cannot map it back to an entity!!
            if (properties.Count > 1)
                return;

            foreach (IEnumerable<PropertyDescriptor> propertyList in properties.Select(e => TypeDescriptorHelper.GetProperty<TEntity>(e)))
            {
                foreach (PropertyDescriptor propDescriptor in propertyList)
                {
                    object tempEntity = propDescriptor.GetValue(entityObject);
                    if (tempEntity == null && !TypeHelper.IsBaseType(propDescriptor.PropertyType))
                    {
                        tempEntity = TypeDescriptor.CreateInstance(null, propDescriptor.PropertyType, null, null);
                        propDescriptor.SetValue(entityObject, tempEntity);
                    }

                    if (!TypeHelper.IsBaseType(propDescriptor.PropertyType))
                        entityObject = tempEntity;

                    entityProp = propDescriptor;
                }

                ColumnAttribute columnAttr = entityProp.Attributes.OfType<ColumnAttribute>().FirstOrDefault();
                if (columnAttr == null || (columnAttr.IsIdentity || columnAttr.IsPrimaryKey) && !this._isPreLoading) return;

                if (property.PropertyType == entityProp.PropertyType)
                    entityProp.SetValue(entityObject, property.GetValue(context.Model));
                else
                {
                    if (TypeHelper.NonNullableType(property.PropertyType) == typeof(bool) && TypeHelper.NonNullableType(entityProp.PropertyType) == typeof(int))
                        entityProp.SetValue(entityObject, (bool)(property.GetValue(context.Model) ?? false) ? 1 : 0);
                    else if (property.PropertyType.IsSubclassOf(typeof(Enum)) && TypeHelper.IsBaseType(entityProp.PropertyType))
                        entityProp.SetValue(entityObject, Convert.ChangeType(property.GetValue(context.Model), entityProp.PropertyType));
                    else
                    {
                        object value = property.GetValue(context.Model);
                        if (value != null)
                            entityProp.SetValue(entityObject, Convert.ChangeType(value, entityProp.PropertyType));
                    }
                }
            }
        }

        public static void Visit<TModel>(TEntity entity, object model, bool isPreloading = false)
        {
            ModelMapper<TEntity> merger = new ModelMapper<TEntity>(entity, isPreloading);
            IModelVisitor visitor = ModelVisitorProvider.Default;

            visitor.OnEnterModel += merger.EnterModel;
            visitor.OnExitModel += merger.ExitModel;
            visitor.OnVisitProperty += merger.VisitProperty;

            visitor.Visit<TModel>(model);
        }
    }
}
