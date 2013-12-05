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
using System.Reflection;
using System.Text;
using BlueBoxSharp.Data.Entity;

namespace BlueBoxSharp.Data.Metadata
{
    public class ColumnMetadata : IColumnMetadata
    {
        public ColumnMetadata(IDatabaseMetadata db, ITableMetadata table, string name, string type, bool isPrimaryKey, bool isIdentity, bool isNullable)
        {
            this.Name = name;
            this.IsIdentity = isIdentity;
            this.IsPrimaryKey = isPrimaryKey;
            this.IsNullable = isNullable;
            this.SqlType = this.IsNullable ? type + "_nullable" : type;

            if (db.TypeMapping.ContainsKey(this.SqlType))
                this.Type = Type.GetType(db.TypeMapping[this.SqlType], true, true);
        }

        public ColumnMetadata(PropertyInfo property, ColumnAttribute attr)
        {
            this.Name = attr.Name;
            this.IsIdentity = attr.IsIdentity;
            this.IsNullable = attr.IsNullable;
            this.IsPrimaryKey = attr.IsPrimaryKey;
            this.Type = property.PropertyType;
        }

        public string Name { get; private set; }
        public bool IsPrimaryKey { get; private set; }
        public bool IsIdentity { get; private set; }
        public bool IsNullable { get; private set; }
        public Type Type { get; private set; }
        public string SqlType { get; private set; }
    }
}
