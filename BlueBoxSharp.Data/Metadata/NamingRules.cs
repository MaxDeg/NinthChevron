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

namespace BlueBoxSharp.Data.Metadata
{
    public class NamingRules
    {
        public NamingRules()
        {
            SchemaName = name => name;
            TableName = name => name;
            ColumnName = (table, name) => table.Name == name ? name + "_" : name;
            RelationColumnName = (foreignTable, name, isReverse) =>
            {
                string result;
                string[] parts = name.Split('_');

                if (parts.Length > 1)
                    result = string.Join("_", parts.Take(parts.Length - 1));
                else
                    result = name;

                if (isReverse) 
                    return result + "_" + TableName(foreignTable.Name);

                return result;
            };
            ProcedureName = name => name;
        }

        public Func<string, string> SchemaName { get; set; }
        public Func<string, string> TableName { get; set; }
        public Func<ITableMetadata, string, string> ColumnName { get; set; }
        public Func<ITableMetadata, string, bool, string> RelationColumnName { get; set; }
        public Func<string, string> ProcedureName { get; set; }
    }
}
