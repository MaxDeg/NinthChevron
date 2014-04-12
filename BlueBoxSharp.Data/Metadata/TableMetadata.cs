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
using System.Runtime.CompilerServices;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;

namespace BlueBoxSharp.Data.Metadata
{
    public enum JoinType { Left, Right, Inner, None }

    public class TableMetadata : ITableMetadata
    {
        #region Static/Relations
        
        private static Dictionary<MemberInfo, Expression> _clauses = new Dictionary<MemberInfo, Expression>();

        internal static void AddClause(MemberInfo member, Expression clause)
        {
            _clauses.Add(member, clause);
        }

        internal static Expression GetClause(MemberInfo member)
        {
            Expression clause;

            if (_clauses.TryGetValue(member, out clause))
                return clause;

            return null;
        }

        #endregion

        private string _identityColumnName;
        private List<string> _primaryKeyColumnNames;

        public TableMetadata(string database, string schema, string name)
        {
            this.Database = database;
            this.Schema = schema;
            this.Name = name;
            this.Columns = new Dictionary<string, IColumnMetadata>();
            this.Relations = new List<IRelationColumnMetadata>();
            this._primaryKeyColumnNames = new List<string>();
        }

        internal TableMetadata(TableAttribute tableAttr, Type mappingClass)
        {
            this.Database = tableAttr.Database;
            this.Schema = tableAttr.Schema;
            this.Name = tableAttr.Name;
            this.Columns = new Dictionary<string, IColumnMetadata>();
            this.Relations = new List<IRelationColumnMetadata>();
            this.LeftJoins = new Dictionary<string, Expression>();
            this.InnerJoins = new Dictionary<string, Expression>();
            this.AutoJoins = new List<string>();
            this._primaryKeyColumnNames = new List<string>();

            foreach (PropertyInfo prop in mappingClass.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                ColumnAttribute fieldAttr = (ColumnAttribute)prop.GetCustomAttributes(typeof(ColumnAttribute), true).SingleOrDefault();
                if (fieldAttr != null)
                {
                    if (fieldAttr.IsIdentity) 
                        this._identityColumnName = prop.Name;
                    if (fieldAttr.IsPrimaryKey)
                        this._primaryKeyColumnNames.Add(prop.Name);

                    this.Columns.Add(prop.Name, new ColumnMetadata(prop, fieldAttr));
                    continue;
                }

                LeftJoinColumnAttribute leftJoinAttr = (LeftJoinColumnAttribute)prop.GetCustomAttributes(typeof(LeftJoinColumnAttribute), true).SingleOrDefault();
                if (leftJoinAttr != null)
                {
                    Expression expr = GetClause(prop);
                    if (expr == null) throw new Exception("Join clause not found!");

                    this.LeftJoins.Add(prop.Name, expr);
                    if (leftJoinAttr.AutoJoin) this.AutoJoins.Add(prop.Name);

                    continue;
                }

                InnerJoinColumnAttribute innerJoinAttr = (InnerJoinColumnAttribute)prop.GetCustomAttributes(typeof(InnerJoinColumnAttribute), true).SingleOrDefault();
                if (innerJoinAttr != null)
                {
                    Expression expr = GetClause(prop);
                    if (expr == null) throw new Exception("Join clause not found!");

                    this.InnerJoins.Add(prop.Name, expr);
                    if (innerJoinAttr.AutoJoin) this.AutoJoins.Add(prop.Name);

                    continue;
                }
            }
        }

        public string Database { get; private set; }
        public string Schema { get; private set; }
        public string Name { get; private set; }
        public IDictionary<string, IColumnMetadata> Columns { get; private set; }
        public IList<IRelationColumnMetadata> Relations { get; private set; }

        public IColumnMetadata IdentityColumn { get { return this.Columns[this._identityColumnName]; } }

        public Dictionary<string, Expression> LeftJoins { get; private set; }
        public Dictionary<string, Expression> InnerJoins { get; private set; }
        public List<string> AutoJoins { get; private set; }
        
        internal Tuple<string, object> GetIdentityValue(IEntity entity)
        {
            if (this._identityColumnName == null)
                return null;

            object value = TypeDescriptor.GetProperties(entity).Find(this._identityColumnName, true).GetValue(entity);

            if (TypeHelper.IsDefault(value))
                return null;

            return Tuple.Create(this._identityColumnName, value);
        }

        internal IEnumerable<Tuple<string, object>> GetPrimaryKeyValues(IEntity entity)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entity);

            foreach (string pKey in this._primaryKeyColumnNames)
                yield return Tuple.Create(pKey, properties.Find(pKey, true).GetValue(entity));
        }

        internal bool HasIdentityOrPrimaryKeyDefined(IEntity entity)
        {
            Tuple<string, object> identity = this.GetIdentityValue(entity);
            if (identity != null) 
                return true;

            foreach (Tuple<string, object> value in this.GetPrimaryKeyValues(entity))
            {
                if (!TypeHelper.IsDefault(value.Item2))
                    return true;
            }

            return false;
        }

        internal JoinType TryGetJoin(string property, out Expression clause)
        {
            clause = null;

            if (this.LeftJoins.TryGetValue(property, out clause))
                return JoinType.Left;
            else if (this.InnerJoins.TryGetValue(property, out clause))
                return JoinType.Inner;

            return JoinType.None;
        }


        public bool IsMappingTable()
        {
            IEnumerable<IColumnMetadata> data = this.Columns.Values;
            IEnumerable<IRelationColumnMetadata> originalRelation = this.Relations.Where(r => !r.IsReverseRelation);

            if (!string.IsNullOrEmpty(this._identityColumnName))
                data = data.Where(c => c.Name != this._identityColumnName);

            data = data.Where(c => !originalRelation.Any(r => r.Name == c.Name));

            return data.Count() == 0;
        }
    }
}
