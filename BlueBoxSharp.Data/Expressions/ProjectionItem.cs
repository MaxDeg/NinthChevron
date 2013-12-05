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

namespace BlueBoxSharp.Data.Expressions
{
    public enum ProjectionItemType { Projection, New, MemberInit }

    public class ProjectionItem
    {
        public Expression Expression { get; internal set; }
        public string Alias { get; internal set; }
        public MemberInfo Member { get; internal set; }
        public int Count { get; internal set; }
        public ProjectionItemType Type { get; internal set; }

        internal ProjectionItem() { }

        public ProjectionItem(Expression exp, MemberInfo memb, string alias)
        {
            this.Expression = exp;
            this.Member = memb;
            this.Alias = alias;
            this.Type = ProjectionItemType.Projection;
        }

        public ProjectionItem(NewExpression exp, MemberInfo memb)
        {
            this.Expression = exp;
            this.Count = exp.Arguments.Count;
            this.Member = memb;
            this.Type = ProjectionItemType.New;
        }

        public ProjectionItem(MemberInitExpression exp, MemberInfo memb)
        {
            this.Expression = exp;
            this.Member = memb;
            this.Count = exp.Bindings.Count;
            this.Type = ProjectionItemType.MemberInit;
        }

        public override string ToString()
        {
            if (this.Member.MemberType == MemberTypes.Property)
                return string.Format("[{0} {1} ({2})]", ((PropertyInfo)this.Member).PropertyType, this.Member.Name, this.Alias);

            return string.Format("[{0} ({2})]", this.Member.Name, this.Alias);
        }
    }
}
