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
using System.Text;
using BlueBoxSharp.Data.Converters;

namespace BlueBoxSharp.Data.Expressions
{
    public abstract class CommandExpression : Expression
    {
        protected Type _type;

        public Expression From { get; internal set; }
        public Expression Where { get; internal set; }
        
        internal DataContext Context { get; private set; }

        protected CommandExpression(CommandExpression expression)
        {
            this.Context = expression.Context;
            this.From = expression.From;
            this.Where = expression.Where;
            this._type = expression._type;
        }

        protected CommandExpression(DataContext context)
        {
            this.Context = context;
        }

        internal CommandExpression(DataContext context, Type type)
        {
            this.Context = context;
            this._type = type;

            EntityExpression entity = new EntityExpression(this, type);
            if (entity.Where != null) this.Where = entity.Where;

            this.From = entity;
        }
        
        public override Type Type
        {
            get { return this._type; }
        }
        
        protected int _tableAliasIdx = 0;
        public virtual string GetNewTableAlias()
        {
            return "T" + this._tableAliasIdx++;
        }
    }
}
