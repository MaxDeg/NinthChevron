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
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BlueBoxSharp.Data.Metadata
{
    public class SqlServerMetadata : IDatabaseMetadata
    {
        public SqlServerMetadata(string connectionString, params string[] schemas)
        {
            if (connectionString.IndexOf("initial catalog") < 0)
                throw new ArgumentException("'initial catalog' option missing in connection string");

            string schemaClause = string.Empty;
            if (schemas.Length > 0)
                schemaClause = " AND columns.table_schema IN (" + string.Join(",", schemas.Select(s => "'" + s + "'")) + ") ";

            Tables = new List<ITableMetadata>();
            Dictionary<string, ITableMetadata> tables = new Dictionary<string, ITableMetadata>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"​SELECT DISTINCT
                                                                columns.table_catalog,
                                                                CASE WHEN columns.table_schema = 'dbo' THEN '' ELSE columns.table_schema END,
														        columns.table_name, 
														        columns.column_name, 
														        columns.data_type, 
														        CASE WHEN columns.is_nullable = 'YES' THEN 1 ELSE 0 END AS IsNullable,  
														        CASE WHEN constraints.Constraint_type = 'PRIMARY KEY' THEN 1 ELSE 0 END AS IsPK,
				                                                COLUMNPROPERTY(object_id(columns.table_name), columns.column_name, 'IsIdentity') AS IsIdentity,
                                                                columns.ordinal_position
											        FROM		information_schema.columns columns
											        LEFT JOIN	information_schema.constraint_column_usage usage
											        ON			columns.table_catalog = usage.table_catalog
													        AND columns.table_schema = usage.table_schema
													        AND columns.table_name = usage.table_name
													        AND columns.column_name = usage.column_name
											        LEFT JOIN	information_schema.table_constraints constraints
											        ON			usage.table_catalog = constraints.table_catalog
													        AND usage.table_schema = constraints.table_schema
													        AND usage.table_name = constraints.table_name
													        AND usage.constraint_name = constraints.constraint_name
                                                    INNER JOIN information_schema.tables tables
                                                    ON			columns.table_catalog = tables.table_catalog
		                                                    AND columns.table_schema = tables.table_schema
		                                                    AND columns.table_name = tables.table_name
											        WHERE		columns.table_name NOT IN ('dtproperties', 'sysconstraints', 'syssegments', 'sysdiagrams')
		                                                    AND tables.table_type = 'BASE TABLE'" + schemaClause + 
											        @"ORDER BY	columns.table_name, columns.ordinal_position", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ITableMetadata currentTable = null;

                    while (reader.Read())
                    {
                        this.Name = reader.GetString(0);
                        string schema = reader.GetString(1);
                        string tableName = reader.GetString(2);

                        if (currentTable == null || currentTable.Name != tableName || currentTable.Schema != schema)
                        {
                            currentTable = new TableMetadata(this.Name, schema, tableName);
                            Tables.Add(currentTable);
                            tables.Add(schema + "/" + tableName, currentTable);
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
                cmd = new SqlCommand(@"SELECT		usage.table_schema,
							                        usage.table_name,
							                        usage.column_name,
			                                        CASE WHEN columns.is_nullable = 'YES' THEN 1 ELSE 0 END AS IsNullable,
							                        fkusage.table_schema AS ReferencedTable,
							                        fkusage.table_name AS ReferencedTable,
							                        fkusage.column_name AS ReferencedColumn

				                        FROM		information_schema.constraint_column_usage usage

				                        INNER JOIN	information_schema.table_constraints constraints
				                        ON			usage.table_catalog = constraints.table_catalog
						                        AND usage.table_schema = constraints.table_schema
						                        AND usage.table_name = constraints.table_name
						                        AND usage.constraint_name = constraints.constraint_name
						                        AND constraints.Constraint_type = 'FOREIGN KEY'

				                        INNER JOIN	information_schema.referential_constraints rconstraint
				                        ON			rconstraint.constraint_catalog = usage.constraint_catalog  
						                        AND rconstraint.constraint_schema = usage.constraint_schema
						                        AND rconstraint.constraint_name = usage.constraint_name

				                        INNER JOIN	information_schema.constraint_column_usage fkusage
				                        ON			rconstraint.unique_constraint_catalog = fkusage.constraint_catalog  
						                        AND rconstraint.unique_constraint_schema = fkusage.constraint_schema
						                        AND rconstraint.unique_constraint_name = fkusage.constraint_name

                                        INNER JOIN  information_schema.columns columns
                                        ON			columns.table_catalog = fkusage.table_catalog
		                                        AND columns.table_schema = fkusage.table_schema
		                                        AND columns.table_name = fkusage.table_name
		                                        AND columns.column_name = fkusage.column_name

                                        INNER JOIN information_schema.tables tables
                                        ON			columns.table_catalog = tables.table_catalog
		                                        AND columns.table_schema = tables.table_schema
		                                        AND columns.table_name = tables.table_name
										WHERE		columns.table_name NOT IN ('dtproperties', 'sysconstraints', 'syssegments', 'sysdiagrams')
		                                        AND tables.table_type = 'BASE TABLE'" + schemaClause + 

                                      @"ORDER BY	columns.table_schema, columns.table_name, columns.ordinal_position", connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
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


                // Get Procedures
                cmd = new SqlCommand(@"SELECT 
                                                    routines.routine_schema, 
			                                        routines.routine_name, 
			                                        parameters.parameter_name, 
			                                        parameters.data_type,
			                                        parameters.parameter_mode,
			                                        ISNULL(parameters.character_maximum_length, parameters.numeric_precision)
                                        FROM		information_schema.routines AS routines
                                        LEFT JOIN	information_schema.parameters AS parameters 
                                        ON			parameters.specific_name = routines.routine_name
                                        INNER JOIN	sysobjects 
                                        ON			sysobjects.type = 'P' 
		                                        AND	sysobjects.category <> 2 
		                                        AND sysobjects.name = routines.routine_name
                                        WHERE		routine_type = 'PROCEDURE'
                                        ORDER BY	routines.routine_name, parameters.ordinal_position", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<SqlServerProcedureMetadata> procedures = new List<SqlServerProcedureMetadata>();
                    SqlServerProcedureMetadata current = null;

                    while (reader.Read())
                    {
                        string schema = reader.GetString(0);
                        string name = reader.GetString(1);
                        if (current == null || current.Name != name || current.Schema != schema)
                        {
                            current = new SqlServerProcedureMetadata(this, schema, name);
                            procedures.Add(current);
                        }

                        if (!reader.IsDBNull(2))
                        {
                            int? precision = null;
                            if (!reader.IsDBNull(5))
                                precision = reader.GetInt32(5);

                            current.Parameters.Add(new ProcedureParameterMetadata(reader.GetString(2), reader.GetString(3), reader.GetString(4), precision));
                        }
                    }

                    this.Procedures = procedures;
                }
            }
        }

        public string Name { get; private set; }
        public List<ITableMetadata> Tables { get; private set; }
        public IEnumerable<IProcedureMetadata> Procedures { get; private set; }
        
        public Type GetType(string dbType, bool isNullable)
        {
            switch (dbType)
            {
                case "varchar":
                case "nvarchar":
                case "char":
                case "nchar":
                case "text":
                case "ntext":
                    return typeof(string);

                case "datetime":
                case "smalldatetime":
                    return isNullable ? typeof(DateTime?) : typeof(DateTime);

                case "int":
                    return isNullable ? typeof(int?) : typeof(int);

                case "smallint":
                    return isNullable ? typeof(short?) : typeof(short);

                case "decimal":
                    return isNullable ? typeof(decimal?) : typeof(decimal);

                case "float":
                    return isNullable ? typeof(double?) : typeof(double);

                case "real":
                    return isNullable ? typeof(float?) : typeof(float);

                case "bigint":
                case "timestamp":
                    return isNullable ? typeof(long?) : typeof(long);

                case "bit":
                    return isNullable ? typeof(bool?) : typeof(bool);

                case "tinyint":
                    return isNullable ? typeof(byte?) : typeof(byte);

                case "uniqueidentifier":
                    return typeof(Guid);

                case "varbinary":
                    return typeof(byte[]);

                case "image":
                default:
                    return typeof(object);
            }
        }
    }
}
