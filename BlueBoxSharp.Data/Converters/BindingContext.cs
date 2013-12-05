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

namespace BlueBoxSharp.Data.Converters
{
    internal class Binding
    {
        internal Expression Alias { get; private set; }
        internal Expression Expression { get; private set; }

        public Binding(Expression alias, Expression expression)
        {
            this.Alias = alias;
            this.Expression = expression;
        }
    }

    internal class BindingContext
    {
        private Stack<Binding> _scopes;

        internal BindingContext()
        {
            this._scopes = new Stack<Binding>();
        }

        internal void ScopeBegin(params Binding[] bindings)
        {
            foreach (Binding binding in bindings)
            {
                this._scopes.Push(binding);
            }
        }

        internal void ScopeEnd()
        {
            this._scopes.Pop();
        }

        internal bool TryGetBoundExpression(Expression alias, out Expression expression)
        {
            expression = this._scopes
                .Where(b => b.Alias.Equals(alias))
                .Select(b => b.Expression)
                .FirstOrDefault();

            return expression != null;
        }
    }
}
