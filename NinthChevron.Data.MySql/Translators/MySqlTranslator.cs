﻿/**
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

using NinthChevron.Data.Expressions;
using NinthChevron.Data.Metadata;
using NinthChevron.Data.Translators;
using NinthChevron.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace NinthChevron.Data.MySql.Translators
{
    public class MySqlTranslator : SqlExpressionVisitor, ITranslator
    {
        private DataContext _context;

        public MySqlTranslator(DataContext context)
        {
            this._context = context;
        }

        #region ITranslator implementation

        public string SqlEncode(object value)
        {
            if (value == null)
                return "NULL";

            Type type = value.GetType();

            if (type == typeof(string))
                return "'" + ((string)value).Replace("'", "''") + "'";
            else if (TypeHelper.NonNullableType(type) == typeof(bool))
                return (bool)value ? "1" : "0";
            else if (TypeHelper.NonNullableType(type) == typeof(DateTime))
            {
                DateTime dt = (DateTime)value;
                return "'" + dt.ToString("yyyy/MM/dd HH:mm:ss.fff") + "'";
            }
            else if (TypeHelper.NonNullableType(type) == typeof(int))
                return ((int)value).ToString(CultureInfo.InvariantCulture);
            else if (TypeHelper.NonNullableType(type) == typeof(double))
                return ((double)value).ToString(CultureInfo.InvariantCulture);
            else if (TypeHelper.NonNullableType(type) == typeof(float))
                return ((float)value).ToString(CultureInfo.InvariantCulture);
            else if (TypeHelper.NonNullableType(type) == typeof(long))
                return ((long)value).ToString(CultureInfo.InvariantCulture);
            else if (TypeHelper.NonNullableType(type) == typeof(decimal))
                return ((decimal)value).ToString(CultureInfo.InvariantCulture);

            return "'" + value.ToString().Replace("'", "''") + "'";
        }

        public string Translate(Expression query)
        {
            return this.Visit(query);
        }

        #endregion

        #region SqlExpressionVisitor implementation

        public override string Visit(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    if (TypeHelper.IsNumeric(node.Left.Type) && TypeHelper.IsNumeric(node.Right.Type))
                        return "CONCAT(" + Visit(node.Left) + ", " + Visit(node.Right) + ")";
                    else
                        return Visit(node.Left) + " + " + Visit(node.Right);

                case ExpressionType.Subtract:
                    return Visit(node.Left) + " - " + Visit(node.Right);

                case ExpressionType.Divide:
                    return Visit(node.Left) + " / " + Visit(node.Right);

                case ExpressionType.Multiply:
                    return Visit(node.Left) + " * " + Visit(node.Right);

                case ExpressionType.Modulo:
                    return Visit(node.Left) + " % " + Visit(node.Right);

                case ExpressionType.AndAlso:
                    return Visit(node.Left) + " AND " + Visit(node.Right);

                case ExpressionType.OrElse:
                    return "(" + Visit(node.Left) + " OR " + Visit(node.Right) + ")";

                case ExpressionType.Assign:
                    return Visit(node.Left) + " = " + Visit(node.Right);

                case ExpressionType.Equal:
                    string exp = Visit(node.Right);

                    if (exp != "NULL")
                        return Visit(node.Left) + " = " + exp;

                    return Visit(node.Left) + " IS NULL";

                case ExpressionType.NotEqual:
                    return Visit(node.Left) + " <> " + Visit(node.Right);

                case ExpressionType.GreaterThan:
                    return Visit(node.Left) + " > " + Visit(node.Right);

                case ExpressionType.GreaterThanOrEqual:
                    return Visit(node.Left) + " >= " + Visit(node.Right);

                case ExpressionType.LessThan:
                    return Visit(node.Left) + " < " + Visit(node.Right);

                case ExpressionType.LessThanOrEqual:
                    return Visit(node.Left) + " <= " + Visit(node.Right);

                case ExpressionType.Coalesce:
                    return "COALESCE(" + Visit(node.Left) + ", " + Visit(node.Right) + ")";

                default:
                    return "";
            }
        }

        public override string Visit(UnaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    if (TypeHelper.NonNullableType(node.Operand.Type) == TypeHelper.NonNullableType(node.Type))
                        return Visit(node.Operand);
                    else if (TypeHelper.NonNullableType(node.Type) == typeof(int))
                        return "CAST(" + Visit(node.Operand) + " AS INT)";
                    else if (node.Type == typeof(string))
                        return "CAST(" + Visit(node.Operand) + " AS VARCHAR)";

                    return Visit(node.Operand);

                case ExpressionType.Not:
                    return "NOT(" + Visit(node.Operand) + ")";

                default:
                    return "";
            }
        }

        public override string Visit(MethodCallExpression node)
        {
            SqlFunctionAttribute attribute = node.Method.GetCustomAttributes(typeof(SqlFunctionAttribute), false).OfType<SqlFunctionAttribute>().SingleOrDefault();

            if (attribute != null)
                return string.Format(attribute.CallFormat, node.Arguments.Select(a => Visit(a)).ToArray());
            else
            {
                switch (node.Method.Name)
                {
                    case "ToString":
                        return string.Format("CAST({0} AS VARCHAR)", Visit(node.Object));
                    case "IsNullOrEmpty":
                        return string.Format("IFNULL(LENGTH({0}), 0) <= 0", Visit(node.Arguments[0]));
                    case "StartsWith":
                        return string.Format("{0} LIKE {1}", Visit(node.Object), Visit(node.Arguments[0]));
                    case "ToLower":
                        return string.Format("LOWER({0})", Visit(node.Object));
                    case "ToUpper":
                        return string.Format("UPPER({0})", Visit(node.Object));
                    case "Concat":
                        List<string> expressions = new List<string>();

                        foreach (Expression e in node.Arguments)
                        {
                            if (e is ConstantExpression)
                                expressions.Add(SqlEncode(((ConstantExpression)e).Value));
                            else
                                expressions.Add(Visit(e));
                        }

                        return "CONCAT(" + string.Join(", ", expressions) + ")";
                    case "Contains":
                        if (node.Object != null && node.Object.Type == typeof(string))
                            return string.Format("{0} LIKE {1}", Visit(node.Object), Visit(node.Arguments[0]));

                        string argument;
                        string list;
                        if (node.Object != null)
                        {
                            list = Visit(node.Object);
                            argument = Visit(node.Arguments[0]);
                        }
                        else
                        {
                            list = Visit(node.Arguments[0]);
                            argument = Visit(node.Arguments[1]);
                        }

                        if (list == "()")
                            return string.Format("{0} <> {0}", argument);

                        return string.Format("{0} IN {1}", argument, list);

                    default:
                        return string.Join(",", node.Arguments.Select(a => Visit(a)));
                }
            }
        }

        public override string Visit(ConditionalExpression node)
        {
            return string.Format("CASE WHEN {0} THEN {1} ELSE {2} END",
                    Visit(node.Test), Visit(node.IfTrue), Visit(node.IfFalse));
        }

        public override string Visit(ConstantExpression node)
        {
            if (typeof(IEnumerable).IsAssignableFrom(node.Type) && node.Type != typeof(string))
                return "(" + string.Join(", ", ((IEnumerable)node.Value).Cast<object>().Select(o => SqlEncode(o))) + ")";
            else
                return SqlEncode(node.Value);
        }

        public override string Visit(ParameterExpression node)
        {
            throw new NotImplementedException();
        }

        public override string Visit(MemberExpression node)
        {
            if (node.Expression.NodeType == (ExpressionType)ExtendedExpressionType.GroupByProjection)
                return Visit(node.Expression);
            else if (node.Expression.NodeType == (ExpressionType)ExtendedExpressionType.Projection)
                return Visit(node.Expression);

            TableMetadata meta = MappingProvider.GetMetadata(node.Member.DeclaringType);
            if (meta != null)
                return Visit(node.Expression) + ".`" + meta.Columns[node.Member.Name].Name + "`";

            if (node.Expression.Type == typeof(DateTime))
            {
                if (node.Member.Name == "Date")
                    return "DATEADD(dd, 0, DATEDIFF(dd, 0, " + Visit(node.Expression) + "))";
            }

            throw new Exception("Member Not found in metadata");
        }

        public override string Visit(ListInitExpression node)
        {
            throw new NotImplementedException();
        }

        public override string Visit(MemberInitExpression node)
        {
            return string.Join(", ", node.Bindings.Select(b => (MemberAssignment)b).Select(b => Visit(b.Expression)));
        }

        public override string Visit(NewExpression node)
        {
            return string.Join(", ", node.Arguments.Select(a => Visit(a)));
        }

        public override string Visit(QueryExpression node)
        {
            int rowCount = node.SkipCount + node.RowCount;
            string fromAlias = string.Empty;

            if (node.From.NodeType == (ExpressionType)ExtendedExpressionType.Query || node.From.NodeType == (ExpressionType)ExtendedExpressionType.Union)
                fromAlias = " `" + node.GetNewTableAlias() + "`";

            string query = "SELECT " + (node.Distinct ? "DISTINCT " : "") +
                                (Visit((Expression)node.Projection) ?? "*") +
                            " FROM " + Visit(node.From) + fromAlias +
                            (node.Where != null ? " WHERE " + Visit(node.Where) : "") +
                            (node.GroupBy != null ? " GROUP BY " + Visit(node.GroupBy) : "") +
                            (node.OrderBy.Count > 0 ? " ORDER BY " + string.Join(", ", node.OrderBy.Select(o => Visit(o))) : "") +
                            (node.RowCount > 0 ? " LIMIT " + node.RowCount + (node.SkipCount > 0 ? " OFFSET " + node.SkipCount : "") : "");

            if (node.IsSubQuery)
                return "(" + query + ")";
            else
                return query;
        }

        public override string Visit(DeleteExpression node)
        {
            EntityRefExpression entityRef = new EntityRefExpression((EntityExpression)node.From);

            return "DELETE " + Visit(entityRef) + " FROM " + Visit(node.From) + " WHERE " + Visit(node.Where) + ";";
        }

        public override string Visit(InsertExpression node)
        {
            TableMetadata meta = MappingProvider.GetMetadata(node.From.Type);
            if (meta == null) return string.Empty;

            return "INSERT INTO " + string.Format("`{0}`.`{1}`", meta.Database, meta.Name) +
                        "(" + string.Join(", ", node.Columns.Select(c => meta.Columns[c.Item1].Name)) + ")" +
                        " VALUES(" + string.Join(", ", node.Columns.Select(c => SqlEncode(c.Item2))) + "); SELECT LAST_INSERT_ID();";
        }

        public override string Visit(InsertSelectExpression node)
        {
            TableMetadata meta = MappingProvider.GetMetadata(node.Projection.Type);
            if (meta == null) return string.Empty;

            var fields = node.Projection.Fields.Where(f => f.Type == ProjectionItemType.Projection);

            return "INSERT INTO " + string.Format("`{0}`.`{1}`", meta.Database, meta.Name) +
                        "(" + string.Join(", ", fields.Select(c => meta.Columns[c.Member.Name].Name)) + ") " +
                        Visit((QueryExpression)node) + "; SELECT LAST_INSERT_ID();";
        }

        public override string Visit(UpdateExpression node)
        {
            EntityRefExpression entityRef = new EntityRefExpression((EntityExpression)node.From);
            TableMetadata meta = MappingProvider.GetMetadata(entityRef.Type);
            if (meta == null) return string.Empty;

            return "UPDATE " + Visit(node.From) +
                " SET " +
                string.Join(", ", node.Columns.Select(c => string.Format("{0} = {1}", meta.Columns[c.Item1].Name, SqlEncode(c.Item2)))) +
                " WHERE " + Visit(node.Where);
        }

        public override string Visit(UpdateSelectExpression node)
        {
            EntityRefExpression entityRef = new EntityRefExpression((EntityExpression)node.From);
            TableMetadata meta = MappingProvider.GetMetadata(entityRef.Type);
            if (meta == null) return string.Empty;

            var fields = node.Projection.Fields.Where(f => f.Type == ProjectionItemType.Projection);

            return "UPDATE " + Visit(node.From) +
                " SET " +
                string.Join(", ", fields.Select(c => string.Format("{0} = {1}", meta.Columns[c.Member.Name].Name, Visit(c.Expression)))) +
                (node.Where != null ? " WHERE " + Visit(node.Where) : "");
        }

        public override string Visit(UnionQueryExpression node)
        {
            int rowCount = node.SkipCount + node.RowCount;

            string leftQuery = Visit((Expression)node.LeftQuery);
            string rightQuery = Visit((Expression)node.RightQuery);

            string unionQuery;
            if (rowCount == 0)
                unionQuery = leftQuery + (node.Distinct ? " UNION " : " UNION ALL ") + rightQuery;
            else
                unionQuery = "SELECT " + (rowCount > 0 ? "TOP " + rowCount + " " : "") +
                                Visit((Expression)node.Projection) +
                            " FROM (" +
                                leftQuery + (node.Distinct ? " UNION " : " UNION ALL ") + rightQuery
                            + ") `" + ((EntityExpression)node.From).Alias + "` ";

            string query = unionQuery + " " +
                            (node.Where != null ? " WHERE " + Visit(node.Where) : "") +
                            (node.GroupBy != null ? " GROUP BY " + Visit(node.GroupBy) : "") +
                            (node.OrderBy.Count > 0 ? " ORDER BY " + string.Join(", ", node.OrderBy.Select(o => Visit(o))) : "");

            if (node.IsSubQuery)
                return "(" + query + ")";
            else
                return query;
        }

        public override string Visit(AggregateExpression node)
        {
            switch (node.NodeType)
            {
                case (ExpressionType)ExtendedExpressionType.Count:
                    return "COUNT(*) ";
                case (ExpressionType)ExtendedExpressionType.Max:
                    return "MAX(" + Visit(node.Expression) + ") ";
                case (ExpressionType)ExtendedExpressionType.Min:
                    return "MIN(" + Visit(node.Expression) + ") ";
                case (ExpressionType)ExtendedExpressionType.Sum:
                    return "SUM(" + Visit(node.Expression) + ") ";
                case (ExpressionType)ExtendedExpressionType.Average:
                    return "AVG(" + Visit(node.Expression) + ") ";
                default:
                    return "";
            }
        }

        public override string Visit(ProjectionExpression node)
        {
            List<string> projection = new List<string>();

            foreach (ProjectionItem item in node.Fields.Where(f => f.Type == ProjectionItemType.Projection))
            {
                if (item.Expression.Expression.NodeType == (ExpressionType)ExtendedExpressionType.Projection)
                    projection.Add(Visit(item.Expression.Expression));
                else
                    projection.Add(Visit(item.Expression.Expression) + " `" + item.Expression.Alias + "`");
            }

            return string.Join(", ", projection) + " ";
        }

        public override string Visit(GroupByProjectionExpression node)
        {
            List<string> projection = new List<string>();

            foreach (ProjectionItem item in node.Fields.Where(f => f.Type == ProjectionItemType.Projection))
                projection.Add(Visit(item.Expression.Expression) + " `" + item.Expression.Alias + "`");

            return string.Join(", ", projection) + " ";
        }

        public override string Visit(UnionProjectionExpression node)
        {
            List<string> projection = new List<string>();

            foreach (ProjectionItem item in node.Fields.Where(f => f.Type == ProjectionItemType.Projection))
                projection.Add("`" + item.Expression.Alias + "`");

            return string.Join(", ", projection) + " ";
        }

        public override string Visit(OrderByExpression node)
        {
            return (node.Alias == null ? Visit(node.Expression) : node.Alias) + (node.Direction == OrderByDirection.Ascending ? " ASC" : " DESC");
        }

        public override string Visit(EntityExpression node)
        {
            TableMetadata meta = MappingProvider.GetMetadata(node.Type);

            string withIndexes = string.Empty;
            if (node.Indexes.Count() > 0)
                withIndexes = string.Format(" WITH(INDEX({0}))", string.Join(", ", node.Indexes));

            return string.Format("`{0}`.`{1}` `{2}`{3} {4}", meta.Database, meta.Name, node.Alias, withIndexes, string.Join(" ", node.Joins.Select(j => Visit(j))));
        }

        public override string Visit(EntityRefExpression node)
        {
            return "`" + node.Entity.Alias + "`";
        }

        public override string Visit(JoinExpression node)
        {
            TableMetadata meta = MappingProvider.GetMetadata(node.Entity.Type);

            string withIndexes = string.Empty;
            if (node.Indexes.Count() > 0)
                withIndexes = string.Format(" WITH(INDEX({0}))", string.Join(", ", node.Indexes));

            string from = string.Format("`{0}`.`{1}` `{2}`", meta.Database, meta.Name, node.Entity.Alias);
            string joinKeyword = null;

            if (node.JoinType == JoinType.Left)
                joinKeyword = " LEFT JOIN ";
            else if (node.JoinType == JoinType.Right)
                joinKeyword = " RIGHT JOIN ";
            else
                joinKeyword = " INNER JOIN ";

            return joinKeyword + from + " ON " + Visit(node.JoinClause)
                        + string.Join(" ", node.Entity.Joins.Select(j => Visit(j)));
        }

        public override string Visit(ExistsExpression node)
        {
            return "EXISTS(" + Visit(node.Expression) + ") ";
        }

        public override string Visit(AliasedExpression node)
        {
            return "`" + node.Alias + "`";
        }

        public override string Visit(KeywordExpression node)
        {
            return node.Value.ToString();
        }

        #endregion
    }
}
