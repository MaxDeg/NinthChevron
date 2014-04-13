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
using System.Text;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Data.Metadata;

namespace BlueBoxSharp.Data
{
    public static class EntityQueryable
    {
        public static IQueryable<TSource> WithIndex<TSource, TKey>(
            IQueryable<TSource> enumerable, 
            Expression<Func<TSource, TKey>> propertySelector, 
            IEnumerable<ConstantExpression> indexes
            )
        {
            return enumerable;
        }


        public static IQueryable<TProjection> DynamicJoin<TSource, TKey, TJoinSource, TProjection>(
            IQueryable<TSource> enumerable,
            JoinType type,
            Expression<Func<TSource, TKey>> propertySelector,
            IQueryable<TJoinSource> joinEnumerable,
            Expression<Func<TKey, TJoinSource, bool>> clause,
            Expression<Func<TSource, TJoinSource, TProjection>> projectionSelector
            )
        {
            return null;
        }
    }
}
