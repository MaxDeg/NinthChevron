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
    public class ProcedureParameterMetadata
    {
        public ProcedureParameterMetadata(string name, string sqlType, string mode, int? precision)
        {
            this.Name = name;
            this.SqlType = sqlType;
            this.Precision = precision;
            this.Mode = mode;
        }

        public string Name { get; private set; }
        public string SqlType { get; private set; }
        public string Mode { get; private set; }
        public int? Precision { get; private set; }
    }
}
