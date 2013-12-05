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
using BlueBoxSharp.Data.Metadata;

namespace BlueBoxSharp.Data.Expressions
{
    public enum QueryReturnType { Enumerable, First, FirstOrDefault, Single, SingleOrDefault, Aggregate }

    public class QueryExpression : CommandExpression
    {
        private QueryExpression _parentQuery;
        private List<QueryExpression> _subQueries;

        public bool Distinct { get; internal set; }
        public virtual int SkipCount { get; internal set; }
        public virtual int RowCount { get; internal set; }
        public QueryReturnType ResultType { get; internal set; }
        public bool IsDefaultProjection { get; protected set; }
        public bool IsSubQuery { get { return this._parentQuery != null; } }

        public ProjectionExpression Projection { get; protected set; }
        public Expression GroupBy { get; internal set; }
        public List<OrderByExpression> OrderBy { get; internal set; }

        internal QueryExpression(DataContext context, Type type) : this(context, null, type) { }

        private QueryExpression(DataContext context, QueryExpression parent, Type type)
            : base(context) // Use this base constructor to assign From property after parent
        {
            if (parent != null)
            {
                parent._subQueries.Add(this);
                this._parentQuery = parent;
            }

            this._type = type;
            this._subQueries = new List<QueryExpression>();
            this.OrderBy = new List<OrderByExpression>();
            this.IsDefaultProjection = true;
            this.ResultType = QueryReturnType.Enumerable;

            EntityExpression entity = new EntityExpression(this, type);
            if (entity.Where != null) this.Where = entity.Where;

            this.From = entity;
            this.Projection = new ProjectionExpression(this);
        }

        protected QueryExpression(DataContext context)
            : base(context)
        {
            this._subQueries = new List<QueryExpression>();
            this.OrderBy = new List<OrderByExpression>();
            this.IsDefaultProjection = true;
            this.ResultType = QueryReturnType.Enumerable;
        }

        protected QueryExpression(QueryExpression expression)
            : base(expression)
        {
            this._subQueries = expression._subQueries;
            this.OrderBy = expression.OrderBy;
            this.Projection = expression.Projection;
            this.IsDefaultProjection = expression.IsDefaultProjection;
            this.ResultType = expression.ResultType;
        }

        internal QueryExpression SubQuery(Type type)
        {
            QueryExpression q = new QueryExpression(this.Context, this, type);
            return q;
        }

        internal QueryExpression WrapQuery(Type type)
        {
            this.OrderBy.Clear();
            this.SkipCount = 0;
            this.RowCount = 0;

            QueryExpression q = new QueryExpression(this.Context, type);
            q._subQueries.Add(this);
            this._parentQuery = q;

            return q;
        }

        internal virtual void Project(ProjectionExpression projection)
        {
            if (projection != null && projection.Fields.Count == 0)
                throw new ArgumentException("Empty Projection is not allowed");

            this.Projection = projection;
            this.IsDefaultProjection = false;

            foreach (OrderByExpression expr in this.OrderBy)
            {
                ProjectionItem item;
                if (this.Projection.TryFindMember(expr.Expression, out item))
                    expr.Alias = item.Alias;
                else
                    expr.Alias = null;
            }
        }

        internal virtual void Project(ExpressionConverter converter, LambdaExpression lambda)
        {
            Expression exp = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], this));

            if (exp is QueryExpression)
                this.Project(new ProjectionExpression((QueryExpression)exp));
            else if (exp is JoinExpression)
                this.Project(new ProjectionExpression((JoinExpression)exp));
            else
                this.Project(new ProjectionExpression(exp));
        }

        public override string ToString()
        {
            return string.Format("SELECT:\r\n {0}\r\n\r\nFROM:\r\n {1}\r\n\r\nWHERE:\r\n {2}\r\n\r\nGROUP BY:\r\n {3}", this.Projection, this.From, this.Where, this.GroupBy);
        }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.Query; }
        }

        public override Type Type
        {
            get
            {
                Type type = this.Projection != null ? this.Projection.Type : this._type;

                if (ResultType == QueryReturnType.Enumerable)
                    return typeof(IQueryable<>).MakeGenericType(type);

                return type;
            }
        }

        public Type InnerType
        {
            get { return this.Projection != null ? this.Projection.Type : this._type; }
        }


        public override string GetNewTableAlias()
        {
            if (this._parentQuery != null)
                return this._parentQuery.GetNewTableAlias();

            return "T" + this._tableAliasIdx++;
        }
    }
}
