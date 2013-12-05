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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using BlueBoxSharp.Data.Entity;

namespace BlueBoxSharp.Data.Metadata
{
    public static class MappingProvider
    {
        private static ConcurrentDictionary<Type, TableMetadata> _tableCache = new ConcurrentDictionary<Type, TableMetadata>();
        private static object _lockObject = new object();

        public static TableMetadata GetMetadata(Type index)
        {
            TableMetadata meta;
            if (!_tableCache.TryGetValue(index, out meta))
                lock (_lockObject)
                    meta = LoadMetadata(index);

            return meta;
        }

        public static TableMetadata GetMetadata(string table)
        {
            return _tableCache.Values.Where(t => t.Name == table).FirstOrDefault();
        }

        private static TableMetadata LoadMetadata(Type type)
        {
            if (_tableCache.ContainsKey(type))
                return _tableCache[type];

            // Add in the cache -- prevent recursive call
            _tableCache.TryAdd(type, null);
            
            // To force the loading of the class in memory so the static constructor is executed.
            RuntimeHelpers.RunClassConstructor(type.TypeHandle);

            TableAttribute tableAttr = (TableAttribute)type.GetCustomAttributes(typeof(TableAttribute), true).SingleOrDefault();
            if (tableAttr == null)
                return null;            
            
            TableMetadata meta = new TableMetadata(tableAttr, type);
            _tableCache.TryUpdate(type, meta, null);

            return meta;
        }
    }
}
