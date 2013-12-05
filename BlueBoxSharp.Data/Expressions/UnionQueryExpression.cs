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
    public class UnionQueryExpression : QueryExpression
    {
        public QueryExpression LeftQuery { get; internal set; }
        public QueryExpression RightQuery { get; internal set; }
        public string Alias { get; private set; }

        internal UnionQueryExpression(QueryExpression left, QueryExpression right)
            : base(left.Context)
        {
            if (left.Context != right.Context)
                throw new InvalidOperationException("Cannot create an union between DataContext");

            this.RightQuery = right;
            this.LeftQuery = left;

            this.Alias = this.GetNewTableAlias();
            this.Projection = new UnionProjectionExpression(left.Projection);
            this.IsDefaultProjection = left.IsDefaultProjection;
        }

        internal override void Project(ExpressionConverter converter, LambdaExpression lambda)
        {
            if (this.LeftQuery.IsDefaultProjection)
                this.LeftQuery.Project(converter, lambda);

            if (this.RightQuery.IsDefaultProjection)
                this.RightQuery.Project(converter, lambda);
            
            this.Projection = new UnionProjectionExpression(converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], this)));
            this.IsDefaultProjection = false;


            foreach (OrderByExpression orderBy in this.OrderBy)
            {
                ProjectionItem item;
                if (this.Projection.TryFindMember(orderBy.Expression, out item))
                    orderBy.Alias = item.Alias;
            }
        }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.Union; }
        }

    }
}
