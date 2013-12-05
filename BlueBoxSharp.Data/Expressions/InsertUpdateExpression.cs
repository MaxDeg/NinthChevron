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
using BlueBoxSharp.Data.Converters;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Data.Metadata;

namespace BlueBoxSharp.Data.Expressions
{
    public abstract class InsertUpdateExpression : QueryExpression
    {
        private TableMetadata _metadata;
        private ExtendedExpressionType _nodeType;

        internal InsertUpdateExpression(ExtendedExpressionType type, QueryExpression expression)
            : base(expression)
        {
            if (type != ExtendedExpressionType.Update && type != ExtendedExpressionType.Insert)
                throw new ArgumentException("type");

            this.Columns = new List<Tuple<string, object>>();
            this._metadata = MappingProvider.GetMetadata(this.Type);
            this._nodeType = type;

            //foreach (var change in entity.ChangeTracker.Changes)
            //    this.AddChange(change);
        }

        internal InsertUpdateExpression(ExtendedExpressionType type, DataContext context, IInternalEntity entity)
            : base(context, entity.GetType())
        {
            if (type != ExtendedExpressionType.Update && type != ExtendedExpressionType.Insert)
                throw new ArgumentException("type");

            this.Columns = new List<Tuple<string, object>>();
            this._metadata = MappingProvider.GetMetadata(entity.GetType());
            this._nodeType = type;

            foreach (var change in entity.ChangeTracker.Changes)
                this.AddChange(change);
        }

        public List<Tuple<string, object>> Columns { get; internal set; }
        public override ExpressionType NodeType
        {
            get { return (ExpressionType)this._nodeType; }
        }
        
        private void AddChange(EntityPropertyChange change)
        {
            IColumnMetadata columnMeta;
            if (!this._metadata.Columns.TryGetValue(change.Property, out columnMeta)) return;
            if (change.NewValue == null && !columnMeta.IsNullable)
            {
                if (columnMeta.Type == typeof(string))
                    change.NewValue = string.Empty;
                else if (columnMeta.Type.IsValueType)
                    change.NewValue = Activator.CreateInstance(columnMeta.Type);
                else
                    throw new NullReferenceException(string.Format("Property {0} cannot be null in the database.", change.Property));
            }

            if (change.OldValue == null && change.NewValue == null) return;
            if (change.OldValue is string && (string)change.OldValue == "" && change.NewValue == null) return;
            if (change.OldValue != null && change.OldValue.Equals(change.NewValue)) return;

            Tuple<string, object> entry = this.Columns.Where(c => c.Item1 == change.Property).SingleOrDefault();
            if (entry != null) this.Columns.Remove(entry);

            this.Columns.Add(Tuple.Create(change.Property, change.NewValue));
        }
    }
}
