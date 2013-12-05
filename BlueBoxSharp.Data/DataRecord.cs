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
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace BlueBoxSharp.Data
{
    public class DataRecord
    {
        private object[] _values;
        private int _fieldCount;

        public DataRecord(IDataRecord reader)
        {
            this._values = new object[reader.FieldCount];
            this._fieldCount = reader.GetValues(this._values);
        }

        public TValue Get<TValue>(int ordinal)
        {
            return Get<TValue>(ordinal, default(TValue));
        }

        public TValue Get<TValue>(int ordinal, TValue defaultValue)
        {
            if (ordinal >= this._fieldCount)
                throw new ArgumentOutOfRangeException();

            object value = this._values[ordinal];
            if (value == DBNull.Value)
                return defaultValue;
            else
                return (TValue)value;
        }

        public static IEnumerable<DataRecord> CreateEnumerable(DbDataReader reader)
        {
            foreach (DbDataReader r in reader)
                yield return new DataRecord(r);
        }
    }
}
