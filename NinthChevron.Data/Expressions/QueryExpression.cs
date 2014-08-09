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
using NinthChevron.Data.Converters;
using NinthChevron.Data.Metadata;

namespace NinthChevron.Data.Expressions
{
    public enum QueryReturnType { Enumerable, First, FirstOrDefault, Single, SingleOrDefault, Aggregate }

    public class QueryExpression : CommandExpression
    {
        private QueryExpression _parentQuery;
        private List<QueryExpression> _subQueries;

        public bool Distinct { get; internal set; }
        public virtual int SkipCount { get; set; }
        public virtual int RowCount { get; set; }
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
            this.Projection = new EntityProjectionExpression((EntityExpression)this.From);
        }

        protected QueryExpression(DataContext context)
            : base(context)
        {
            this._subQueries = new List<QueryExpression>();
            this.OrderBy = new List<OrderByExpression>();
            this.IsDefaultProjection = true;
            this.ResultType = QueryReturnType.Enumerable;
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
            this.Projection = projection;
            this.IsDefaultProjection = false;
        }

        internal virtual void Project(ExpressionConverter converter, LambdaExpression lambda)
        {
            Expression projection = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], this.Projection));

            if (projection.NodeType == (ExpressionType)ExtendedExpressionType.Projection)
                this.Project(projection as ProjectionExpression);
            else
                this.Project(new ProjectionExpression(projection));
        }

        internal virtual void OrderByAsc(ExpressionConverter converter, LambdaExpression lambda)
        {
            Expression orderByExpr = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], this.Projection));

            if (orderByExpr.NodeType == ExpressionType.Convert)
                orderByExpr = ((UnaryExpression)orderByExpr).Operand;

            OrderByExpression orderByClause = new OrderByExpression(orderByExpr, OrderByDirection.Ascending);
            this.OrderBy.Add(orderByClause);
        }

        internal virtual void OrderByDesc(ExpressionConverter converter, LambdaExpression lambda)
        {
            Expression orderByExpr = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], this.Projection));

            if (orderByExpr.NodeType == ExpressionType.Convert)
                orderByExpr = ((UnaryExpression)orderByExpr).Operand;

            OrderByExpression orderByClause = new OrderByExpression(orderByExpr, OrderByDirection.Descending);
            this.OrderBy.Add(orderByClause);
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
