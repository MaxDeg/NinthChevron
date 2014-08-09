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

using NinthChevron.Data.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinthChevron.Data.SqlServer.Metadata
{
    public class SqlServerProcedureMetadata : IProcedureMetadata
    {
        public SqlServerProcedureMetadata(IDatabaseMetadata dbMetadata, string schema, string name)
        {
            this.Database = dbMetadata;
            this.Schema = schema;
            this.Name = name;
            this.Parameters = new List<ProcedureParameterMetadata>();
        }

        public IDatabaseMetadata Database { get; private set; }
        public string Schema { get; private set; }
        public string Name { get; private set; }
        public List<ProcedureParameterMetadata> Parameters { get; private set; }
        
        public override string ToString()
        {
            return string.Format("[{0}]..[{1}].[{2}]", this.Database.Name, this.Schema, this.Name);
        }
    }
}
