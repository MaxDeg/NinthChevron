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

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueBoxSharp.Data.Metadata
{
    public class MySqlMetadata : IDatabaseMetadata
    {
        public MySqlMetadata(string connectionString, params string[] schemas)
        {
            if (connectionString.IndexOf("Database") < 0)
                throw new ArgumentException("'Database' option missing in connection string");

            string schemaClause = string.Empty;
            if (schemas.Length > 0)
                schemaClause = " AND columns.table_schema IN (" + string.Join(",", schemas.Select(s => "'" + s + "'")) + ") ";

            this.Tables = new List<ITableMetadata>();
            Dictionary<string, ITableMetadata> tables = new Dictionary<string, ITableMetadata>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(@"SELECT DISTINCT
                                                                columns.table_catalog,
			                                                    columns.table_schema,
			                                                    columns.table_name, 
			                                                    columns.column_name, 
			                                                    CONCAT(IF(columns.column_type LIKE '%unsigned', 'u', ''), columns.data_type), 
			                                                    CASE WHEN columns.is_nullable = 'YES' THEN 1 ELSE 0 END AS IsNullable,  
			                                                    CASE WHEN columns.column_key = 'PRI' THEN 1 ELSE 0 END AS IsPK,
			                                                    CASE WHEN columns.extra LIKE '%auto_increment%' THEN 1 ELSE 0 END AS IsIdentity,
			                                                    columns.ordinal_position
                                                    FROM		information_schema.columns columns
                                                    INNER JOIN information_schema.tables tables
                                                    ON			columns.table_catalog = tables.table_catalog
		                                                    AND columns.table_schema = tables.table_schema
		                                                    AND columns.table_name = tables.table_name
                                                    WHERE		columns.table_schema NOT IN ('mysql', 'performance_schema')
		                                                    AND tables.table_type = 'BASE TABLE'" + schemaClause +
                                                    @"ORDER BY	columns.table_schema, columns.table_name, columns.ordinal_position", connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    ITableMetadata currentTable = null;

                    while (reader.Read())
                    {
                        string schema = string.Empty;
                        this.Name = reader.GetString(1);
                        string tableName = reader.GetString(2);

                        if (currentTable == null || currentTable.Name != tableName || currentTable.Schema != schema)
                        {
                            currentTable = new TableMetadata(this.Name, schema, tableName);
                            Tables.Add(currentTable);
                            tables.Add(this.Name + "/" + tableName, currentTable);
                        }

                        currentTable.Columns.Add(reader.GetString(3), new ColumnMetadata(
                            this,
                            currentTable,
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetInt32(6) == 1 ? true : false,
                            reader.GetInt32(7) == 1 ? true : false,
                            reader.GetInt32(5) == 1 ? true : false
                            ));
                    }
                }

                // Found Relation Column
                cmd = new MySqlCommand(@"SELECT DISTINCT
			                                        column_usage.table_schema,
			                                        column_usage.table_name,
			                                        column_usage.column_name,
			                                        CASE WHEN columns.is_nullable = 'YES' THEN 1 ELSE 0 END AS IsNullable,
			                                        column_usage.referenced_table_schema,
			                                        column_usage.referenced_table_name,
			                                        column_usage.referenced_column_name
                                        FROM		information_schema.KEY_COLUMN_USAGE column_usage
                                        INNER JOIN information_schema.tables tables
                                        ON			column_usage.table_catalog = tables.table_catalog
		                                        AND column_usage.table_schema = tables.table_schema
		                                        AND column_usage.table_name = tables.table_name
                                        INNER JOIN 	information_schema.columns columns
                                        ON			columns.table_schema = column_usage.table_schema
		                                        AND	columns.table_name = column_usage.table_name
		                                        AND	columns.column_name = column_usage.column_name
                                        WHERE		column_usage.table_schema NOT IN ('mysql', 'performance_schema')
		                                        AND tables.table_type = 'BASE TABLE'" + schemaClause +
                                              @"AND column_usage.referenced_table_name IS NOT NULL
                                        ORDER BY	column_usage.table_schema, column_usage.table_name", connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string sourceSchema = reader.GetString(0);
                        string sourceTableName = reader.GetString(1);
                        string sourceColumn = reader.GetString(2);
                        bool isNullable = reader.GetBoolean(3);
                        string foreignSchema = reader.GetString(4);
                        string foreignTableName = reader.GetString(5);
                        string foreignColumn = reader.GetString(6);

                        ITableMetadata table = tables[sourceSchema + "/" + sourceTableName];
                        ITableMetadata foreignTable = tables[foreignSchema + "/" + foreignTableName];

                        table.Relations.Add(new RelationColumnMetadata(false, table, sourceColumn, isNullable, foreignTable, foreignColumn));
                        foreignTable.Relations.Add(new RelationColumnMetadata(true, foreignTable, foreignColumn, isNullable, table, sourceColumn));
                    }
                }


                // Found Procedures
                cmd = new MySqlCommand(@"SELECT 
                                                    routines.db,
			                                        routines.name, 
			                                        routines.param_list
                                        FROM		mysql.proc AS routines
                                        WHERE		routines.type = 'PROCEDURE'
                                        ORDER BY	routines.name", connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<MySqlProcedure> procedures = new List<MySqlProcedure>();

                    while (reader.Read())
                    {
                        MySqlProcedure proc = new MySqlProcedure(this, reader.GetString(0), reader.GetString(1));

                        if (!reader.IsDBNull(2))
                        {
                            string parameters = reader.GetString(2).Trim();

                            if (!string.IsNullOrEmpty(parameters))
                            {
                                do
                                {
                                    var data = ReadProcParameter(parameters, out parameters);
                                    proc.Parameters.Add(new ProcedureParameterMetadata(data.Item1, data.Item2, data.Item3, data.Item4));
                                }
                                while (parameters.Length > 0);
                            }
                        }

                        procedures.Add(proc);
                    }

                    this.Procedures = procedures;
                }
            }
        }

        private Tuple<string, string, string, int?> ReadProcParameter(string parameter, out string parametersLeft)
        {
            int? precision = null;
            string name, mode, type;

            int firstIdx = parameter.IndexOf(' ');
            mode = parameter.Substring(0, firstIdx).Trim();

            int secondIdx;
            if (mode != "IN" && mode != "OUT" && mode != "INOUT")
            {
                name = mode;
                mode = "IN";
                secondIdx = firstIdx;
            }
            else
            {
                secondIdx = parameter.IndexOf(' ', firstIdx + 1);
                name = parameter.Substring(firstIdx, secondIdx - firstIdx).Trim();
            }

            int comaIdx = parameter.IndexOf(',', firstIdx + 1);
            int parenthesisIdx = parameter.IndexOf('(', firstIdx + 1);
            if (parenthesisIdx > 0 && parenthesisIdx < comaIdx)
                comaIdx = parameter.IndexOf(',', comaIdx + 1);
            if (comaIdx < 0)
                comaIdx = parameter.Length;

            type = parameter.Substring(secondIdx, comaIdx - secondIdx).Trim().ToLower();
            if (type.Contains("unsigned"))
                type = "u" + type.Replace(" unsigned", "");

            firstIdx = type.IndexOf('(');
            if (firstIdx > 0)
            {
                secondIdx = Math.Min(type.IndexOf(',', firstIdx + 1), type.IndexOf(')', firstIdx + 1));
                precision = int.Parse(type.Substring(firstIdx + 1, secondIdx - firstIdx - 1).Trim());

                type = type.Substring(0, firstIdx);
            }

            parametersLeft = parameter.Substring(comaIdx == parameter.Length ? comaIdx : comaIdx + 1).Trim();

            return Tuple.Create(name, type, mode, precision);
        }

        public string Name { get; private set; }
        public List<ITableMetadata> Tables { get; private set; }
        public IEnumerable<IProcedureMetadata> Procedures { get; private set; }
        
        public Type GetType(string dbType, bool isNullable)
        {
            switch (dbType)
            {
                case "varchar":
                case "char":
                case "text":
                    return typeof(string);

                case "date":
                case "datetime":
                case "smalldatetime":
                case "timestamp":
                    return isNullable ? typeof(DateTime?) : typeof(DateTime);

                case "int":
                case "integer":
                case "mediumint":
                case "enum":
                    return isNullable ? typeof(int?) : typeof(int);

                case "uint":
                case "uinteger":
                case "umediumint":
                    return isNullable ? typeof(uint?) : typeof(uint);

                case "year":
                case "smallint":
                    return isNullable ? typeof(short?) : typeof(short);

                case "usmallint":
                    return isNullable ? typeof(ushort?) : typeof(ushort);

                case "decimal":
                case "udecimal":
                    return isNullable ? typeof(decimal?) : typeof(decimal);

                case "float":
                    return isNullable ? typeof(double?) : typeof(double);

                case "real":
                    return isNullable ? typeof(float?) : typeof(float);

                case "bigint":
                case "serial":
                    return isNullable ? typeof(long?) : typeof(long);

                case "bit":
                case "boolean":
                    return isNullable ? typeof(bool?) : typeof(bool);

                case "tinyint":
                    return isNullable ? typeof(sbyte?) : typeof(sbyte);

                case "utinyint":
                    return isNullable ? typeof(byte?) : typeof(byte);

                case "varbinary":
                case "blob":
                    return typeof(byte[]);

                default:
                    return typeof(object);
            }
        }
    }
}
