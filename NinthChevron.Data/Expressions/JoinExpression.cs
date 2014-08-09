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
using System.Reflection;
using NinthChevron.Data.Metadata;

namespace NinthChevron.Data.Expressions
{
    public class JoinExpression : Expression
    {
        private List<string> _indexes;
        public IEnumerable<string> Indexes { get { return this._indexes; } }

        internal JoinExpression(string key, EntityExpression entityRef, JoinType type, Expression joinClause)
        {
            this.Key = key;
            this.Entity = entityRef;
            this.JoinType = type;
            this.JoinClause = joinClause;
            this._indexes = new List<string>();
        }

        public string Key { get; private set; }
        public EntityExpression Entity { get; private set; }
        public JoinType JoinType { get; private set; }
        public Expression JoinClause { get; private set; }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.Join; }
        }

        public override Type Type
        {
            get { return this.Entity.Type; }
        }

        internal void AddIndexes(List<string> indexes)
        {
            this._indexes.AddRange(indexes);
        }
    }
}
