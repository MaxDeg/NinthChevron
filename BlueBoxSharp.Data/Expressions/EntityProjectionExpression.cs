/**
 *   Copyright 2014
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

using BlueBoxSharp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BlueBoxSharp.Data.Expressions
{
    public class EntityProjectionExpression : ProjectionExpression
    {
        public EntityExpression Entity { get; private set; }

        public EntityProjectionExpression(EntityExpression entity)
        {
            this.Entity = entity;
            this.Fields = FindProjection(Expression.MemberInit(
                                            Expression.New(entity.Type),
                                            entity.Type.GetProperties()
                                                .Where(p => !typeof(IEntity).IsAssignableFrom(p.PropertyType) && p.GetCustomAttributes(typeof(ColumnAttribute), true).Any())
                                                .Select(p => Expression.Bind(p, Expression.MakeMemberAccess(new EntityRefExpression(entity), p)))
                                            ), null).ToList();
        }

        public override Type Type
        {
            get { return this.Entity.Type; }
        }

        public override bool TryFindMember(MemberInfo member, out AliasedExpression result)
        {
            result = null;

            EntityRefExpression entityRef = new EntityRefExpression(this.Entity);
            if (member.ReflectedType != this.Type)
                return false;

            var property = this.Entity.Type.GetProperties()
                                            .Select((p, i) => new { Property = p, Index = i })
                                            .Where(p => p.Property.Name == member.Name).FirstOrDefault();
            if (property == null)
                return false;

            result = new AliasedExpression(Expression.MakeMemberAccess(entityRef, property.Property), "Extent" + property.Index);
            return true;
        }
    }
}
