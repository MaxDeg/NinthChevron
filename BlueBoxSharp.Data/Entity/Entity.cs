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
using System.Text;
using BlueBoxSharp.Data.AutoMap;
using BlueBoxSharp.Data.Metadata;

namespace BlueBoxSharp.Data.Entity
{
    public abstract class Entity<TEntity> : IEntity, IInternalEntity, INotifyPropertyChanged where TEntity : Entity<TEntity>, new()
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _propertyChangeScope;

        public EntityChangeTracker ChangeTracker { get; private set; }

        // Could be private ???
        public Entity()
        {
            this.ChangeTracker = new EntityChangeTracker(this);
        }

        public static Entity<TEntity> Create<TModel>(TModel model)
        {
            if (model == null) throw new ArgumentNullException("model");

            TEntity entity = new TEntity();
            ModelMapper<TEntity>.Visit<TModel>(entity, model, true);

            return entity;
        }

        public object EntityIdentity { get; set; }

        public virtual Expression<Func<TEntity, bool>> Filter(DataContext repository) { return null; }

        public void UpdateWith<TModel>(TModel model)
        {
            ModelMapper<TEntity>.Visit<TModel>((TEntity)this, model);
        }
        
        public void ResetChangeTracker()
        {
            this.ChangeTracker = new EntityChangeTracker(this);
        }

        protected void __RaisePropertyChanged(string property, object oldValue, object newValue)
        {
            if (this._propertyChangeScope || PropertyChanged == null)
                return;

            if (oldValue == null && newValue == null) return;
            if (oldValue is string && (string)oldValue == "" && newValue == null) return;

            TableMetadata tableMeta = MappingProvider.GetMetadata(typeof(TEntity));
            IColumnMetadata columnMeta = tableMeta.Columns[property];

            if (newValue == null && !columnMeta.IsNullable)
            {
                if (columnMeta.Type == typeof(string))
                    newValue = string.Empty;
                else if (columnMeta.Type.IsValueType)
                    newValue = Activator.CreateInstance(columnMeta.Type);
                else
                    throw new NullReferenceException(string.Format("{0} is not nullable", property));
            }
            
            EntityPropertyChangedEventArgs args = new EntityPropertyChangedEventArgs(property);
            args.OldValue = oldValue;
            args.NewValue = newValue;

            // Call PropertyChanged (! avoid infinite loop)
            this._propertyChangeScope = true;
            PropertyChanged(this, args);
            this._propertyChangeScope = false;
        }
        
        ////////////////////////////////////////
        // Static Methods
        ////////////////////////////////////////

        protected static void Join<TOtherEntity>(Expression<Func<TEntity, TOtherEntity>> property, Expression<Func<TEntity, TOtherEntity, bool>> predicate)
            where TOtherEntity : Entity<TOtherEntity>, new()
        {
            MemberExpression member = property.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("property must select a property");

            TableMetadata.AddClause(member.Member, predicate);
        }
    }
}
