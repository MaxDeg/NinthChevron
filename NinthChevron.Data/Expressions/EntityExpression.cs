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
using System.Runtime.CompilerServices;
using System.Text;
using NinthChevron.Data.Converters;
using NinthChevron.Data.Entity;
using NinthChevron.Data.Metadata;

namespace NinthChevron.Data.Expressions
{
    public class EntityExpression : Expression
    {
        private Type _type;
        private CommandExpression _query;
        private Dictionary<string, JoinExpression> _joins;
        private List<string> _indexes;

        internal EntityExpression(CommandExpression expression, Type type)
        {
            this._type = type;
            this.Alias = expression.GetNewTableAlias();
            this._query = expression;
            this._joins = new Dictionary<string, JoinExpression>();
            this._indexes = new List<string>();

            if (typeof(IEntity).IsAssignableFrom(this._type))
            {
                ExpressionConverter converter = new ExpressionConverter(null);
                object entity = Activator.CreateInstance(type);
                MethodInfo filterMethod = this._type.GetMethod("Filter");

                if (filterMethod != null)
                {
                    Expression filterExp = (Expression)filterMethod.Invoke(entity, new object[] { this._query.Context });

                    if (filterExp != null)
                    {
                        LambdaExpression lambda = converter.GetLambdaExpression(filterExp);

                        Expression whereExp = converter.Convert(lambda.Body, new Binding(lambda.Parameters[0], this));
                        this.Where = whereExp;
                    }
                }

                // AUTO JOIN
                TableMetadata meta = MappingProvider.GetMetadata(this._type);
                if (meta != null)
                {
                    foreach (string property in meta.AutoJoins)
                    {
                        PropertyInfo info = this._type.GetProperty(property);
                        Join(converter, info, info.PropertyType);
                    }
                }
            }
        }

        public string Alias { get; private set; }
        public IEnumerable<JoinExpression> Joins { get { return this._joins.Values; } }
        public IEnumerable<string> Indexes { get { return this._indexes; } }
        public Expression Where { get; private set; }

        public override ExpressionType NodeType
        {
            get { return (ExpressionType)ExtendedExpressionType.Entity; }
        }
        public override Type Type
        {
            get { return this._type; }
        }

        public override string ToString()
        {
            return string.Format("[{0} {1}]", this._type, this.Alias);
        }
                    
        internal EntityProjectionExpression Join(ExpressionConverter converter, MemberInfo member, Type type)
        {
            if (!typeof(IEntity).IsAssignableFrom(type))
                throw new ArgumentException("Type doesn't implements IEntity");

            JoinExpression expression;
            if (!this._joins.TryGetValue(member.Name, out expression))
            {
                EntityExpression entityRef = new EntityExpression(this._query, type);
                Expression clause;
                JoinType joinType = MappingProvider.GetMetadata(this._type).TryGetJoin(member.Name, out clause);

                if (clause != null && joinType != JoinType.None)
                {
                    LambdaExpression lambda = converter.GetLambdaExpression(clause);
                    
                    clause = converter.Convert(
                        lambda.Body,
                        new Binding(lambda.Parameters[0], this),
                        new Binding(lambda.Parameters[1], entityRef)
                        );

                    if (entityRef.Where != null)
                        clause = Expression.AndAlso(clause, entityRef.Where);

                    expression = new JoinExpression(member.Name, entityRef, joinType, clause);
                    this._joins.Add(member.Name, expression);
                }
                else
                    throw new Exception("No Join clause found");
            }

            return new EntityProjectionExpression(expression.Entity);
        }

        internal EntityProjectionExpression Join(ExpressionConverter converter, EntityExpression entity, JoinExpression join)
        {
            JoinExpression joinExpression;
            Expression clause;
            JoinType joinType = MappingProvider.GetMetadata(entity._type).TryGetJoin(join.Key, out clause);

            if (clause != null && joinType != JoinType.None)
            {
                LambdaExpression lambda = converter.GetLambdaExpression(clause);

                clause = converter.Convert(
                    lambda.Body,
                    new Binding(lambda.Parameters[0], entity),
                    new Binding(lambda.Parameters[1], this)
                    );

                if (entity.Where != null)
                    clause = Expression.AndAlso(clause, entity.Where);

                joinExpression = new JoinExpression(join.Key, entity, joinType, clause);
                this._joins.Add(join.Key, joinExpression);
            }
            else
                throw new Exception("No Join clause found");

            //return joinExpression;
            return new EntityProjectionExpression(joinExpression.Entity);
        }

        internal EntityProjectionExpression Join(ExpressionConverter converter, JoinExpression join)
        {
            this._joins.Add(join.Key, join);

            //return join;
            return new EntityProjectionExpression(join.Entity);
        }
        
        internal void AddIndexes(List<string> indexes)
        {
            this._indexes.AddRange(indexes);
        }
    }
}
