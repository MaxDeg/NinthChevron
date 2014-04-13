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
using BlueBoxSharp.Data.Expressions;

namespace BlueBoxSharp.Data.SqlServer.Translators
{
    public class TSqlTranslator2005 : TSqlTranslator
    {
        public TSqlTranslator2005(DataContext context) : base(context) { }

        public override string Visit(QueryExpression node)
        {
            int rowCount = node.SkipCount + node.RowCount;

            if (node.IsSubQuery || rowCount <= 1 || node.OrderBy.Count == 0)
                return base.Visit(node);

            string fromAlias = string.Empty;

            if (node.From.NodeType == (ExpressionType)ExtendedExpressionType.Query || node.From.NodeType == (ExpressionType)ExtendedExpressionType.Union)
                fromAlias = " AS [" + node.GetNewTableAlias() + "]";
            
            string query = "SELECT * FROM (" +
                                "SELECT " + (node.Distinct ? "DISTINCT " : "") +
                                    (Visit((Expression)node.Projection) ?? "*") +
                                    ", ROW_NUMBER() OVER (ORDER BY " + string.Join(", ", node.OrderBy.Select(o => Visit(o))) + ") AS [RowNumber]" +
                                " FROM " + Visit(node.From) + fromAlias + " " +
                                (node.Where != null ? " WHERE " + Visit(node.Where) : "") +
                                (node.GroupBy != null ? " GROUP BY " + Visit(node.GroupBy) : "") +
                            ") AS [Paging] " +
                            "WHERE [RowNumber] BETWEEN " + (node.SkipCount +1) + " AND " + rowCount +
                            " ORDER BY [RowNumber]";

            node.SkipCount = 0;

            return query;
        }

        public override string Visit(OrderByExpression node)
        {
            return  Visit(node.Expression) + (node.Direction == OrderByDirection.Ascending ? " ASC" : " DESC");
        }
    }
}
