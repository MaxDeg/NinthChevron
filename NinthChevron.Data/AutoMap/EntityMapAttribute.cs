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

namespace NinthChevron.Data.AutoMap
{
    /// <summary>
    /// Attribute used to Map a property of a view class to an Entity class.
    /// If you don't specify a property the system will use the name of the property on which the attribute is set.
    /// The property name (specified or infered) is NOT case sensitive!
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityMapAttribute : Attribute
    {
        public IEnumerable<string> Properties { get; private set; }

        /// <summary>
        /// Property use to specify if this property is used in INSERT/UPDATE statement.
        /// False by default.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Specify a different key used in the ORDER BY clause.
        /// </summary>
        public string SortKey { get; set; }
        
        public EntityMapAttribute(string property, bool readOnly)
        {
            this.Properties = new List<string>();
            this.IsReadOnly = readOnly;

            if (property != null)
                this.Properties = new List<string> { property };
            else
                this.Properties = new List<string>();
        }

        public EntityMapAttribute(params string[] properties)
        {
            this.Properties = new List<string>(properties);
            this.IsReadOnly = true;
        }

        public EntityMapAttribute(string property) : this(property, false) { }
        public EntityMapAttribute(bool isReadOnly) : this(null, isReadOnly) { }
        public EntityMapAttribute() : this(null, false) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class EntityUpdateAttribute : Attribute
    {
        public IEnumerable<string> Properties { get; private set; }
        
        public EntityUpdateAttribute(params string[] properties)
        {
            this.Properties = new List<string>(properties);
        }
    }
}
