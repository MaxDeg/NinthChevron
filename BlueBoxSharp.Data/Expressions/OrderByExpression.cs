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

namespace BlueBoxSharp.Data.Expressions
{
    public class OrderByExpression : Expression
    {
        public Expression Expression { get; internal set; }
        public OrderByDirection Direction { get; internal set; }
        public string Alias { get; set; }

        internal OrderByExpression(Expression expression, OrderByDirection direction)
        {
            this.Expression = expression;
            this.Direction = direction;
        }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.OrderBy; }
        }

        public override Type Type
        {
            get { return null; }
        }
    }
}
