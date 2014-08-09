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
using System.Text;

namespace NinthChevron.Data.Metadata
{
    public class RelationColumnMetadata : IRelationColumnMetadata
    {
        public RelationColumnMetadata(bool isReverse, ITableMetadata table, string name, bool isNullable, ITableMetadata foreignTable, string foreignColumn)
        {
            this.IsReverseRelation = isReverse;
            this.Table = table;
            this.Name = name;
            this.IsNullable = isNullable;
            this.ForeignTable = foreignTable;
            this.ForeignColumn = foreignColumn;
        }

        public bool IsReverseRelation { get; private set; }

        public ITableMetadata Table { get; private set; }
        public string Name { get; private set; }
        public bool IsNullable { get; private set; }

        public ITableMetadata ForeignTable { get; private set; }
        public string ForeignColumn { get; private set; }
    }
}
