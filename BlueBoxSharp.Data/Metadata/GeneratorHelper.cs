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
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;
using BlueBoxSharp.Extensions;
using System.IO;

namespace BlueBoxSharp.Data.Metadata
{
    public static class GeneratorHelper
    {
        public static string CreateProperty(IColumnMetadata meta)
        {
            if (meta.Type == null) return string.Empty;

            CSharpCodeProvider provider = new CSharpCodeProvider();

            if (meta.IsIdentity)
                return string.Format(@"
        [NotifyPropertyChanged, Column(""{0}"", {1}, {2}, {3})]
        public {4} {5}
        {{
	        get {{ return this.EntityIdentity == null ? TypeHelper.GetDefault<{4}>() : ({4})Convert.ChangeType(this.EntityIdentity, typeof({4})); }}
	        set {{ this.EntityIdentity = ({4})value; }}
        }}",
                meta.Name,
                meta.IsPrimaryKey ? "true" : "false",
                meta.IsIdentity ? "true" : "false",
                meta.IsNullable ? "true" : "false",
                provider.GetTypeOutput(new CodeTypeReference(meta.Type)),
                ToCSharpName(meta.Name, NameType.Column));

            return string.Format(@"
        [NotifyPropertyChanged, Column(""{0}"", {1}, {2}, {3})]
        public {4} {5} {{ get; set; }}",
                meta.Name,
                meta.IsPrimaryKey ? "true" : "false",
                meta.IsIdentity ? "true" : "false",
                meta.IsNullable ? "true" : "false",
                provider.GetTypeOutput(new CodeTypeReference(meta.Type)),
                ToCSharpName(meta.Name, NameType.Column));
        }

        public static string CreateRelationProperty(IRelationColumnMetadata meta)
        {
            return string.Format(@"
        [{0}JoinColumn]
        {1} {2} {3} {{ get; set; }}",
                            meta.IsNullable ? "Left" : "Inner",
                            meta.ForeignTable.IsMappingTable() ? "private" : "public",
                            ToCSharpName(meta.ForeignTable.Name, NameType.Table),
                            meta.IsReverseRelation ? ToCSharpName(meta.ForeignColumn, NameType.ReverseRelationColumn, meta.ForeignTable) 
                                                    : ToCSharpName(meta.Name, NameType.RelationColumn, meta.ForeignTable));
        }

        public static string CreateRelationDefinition(IRelationColumnMetadata meta)
        {
            return string.Format(@"Join<{0}>(t => t.{1}, (t, f) => t.{2} == f.{3});",
                ToCSharpName(meta.ForeignTable.Name, NameType.Table),
                meta.IsReverseRelation ? ToCSharpName(meta.ForeignColumn, NameType.ReverseRelationColumn, meta.ForeignTable)
                                        : ToCSharpName(meta.Name, NameType.RelationColumn, meta.ForeignTable),
                ToCSharpName(meta.Name, NameType.Column),
                ToCSharpName(meta.ForeignColumn, NameType.Column));
        }

        public static string CreateConstant(IProcedureMetadata meta)
        {
            return string.Format(@"
        /// <summary>
        /// Procedure Parameters:{2}
        /// </summary>
        public const string {0} = ""{1}"";",
                ToCSharpName(meta.Name, NameType.None),
                meta,
                string.Join("",
                    meta.Parameters
                    .Select(p => string.Format(@"
        /// <para>[{0}] {1}   {2}{3}</para>", p.Mode, p.Name, p.SqlType, p.Precision.HasValue ? "(" + p.Precision.Value + ")" : ""))
                ));
        }
        
        public static string CreateDelegate(IProcedureMetadata meta)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            
            string commentParameters = string.Join("",
                    meta.Parameters
                    .Select(p => string.Format(@"
        /// <para>[{0}] {1}   {2}{3}</para>", p.Mode, p.Name, p.SqlType, p.Precision.HasValue ? "(" + p.Precision.Value + ")" : "")));

            string parameters = string.Join(", ", meta.Parameters.Select(p => 
                                                        string.Format("{0} {1}",
                                                            provider.GetTypeOutput(new CodeTypeReference(meta.Database.GetType(p.SqlType, true))),
                                                            p.Mode.ToLower() + ToCSharpName(p.Name.Replace("@", ""), NameType.None)
                                                            )
                                                        )
                                                    );

            return string.Format(@"
        /// <summary>
        /// Procedure Parameters:{0}
        /// </summary>
        public static IEnumerable<DataRecord> {1}(DataContext context{2}) 
        {{ 
            return context.ExecuteProcedure(""{3}""{4});
        }}", 
                    commentParameters,
                    ToCSharpName(meta.Name, NameType.Procedure),
                    meta.Parameters.Count > 0 ? ", " + parameters : "",
                    meta,
                    meta.Parameters.Count > 0 ? ", " + string.Join(", ", meta.Parameters.Select(p => p.Mode.ToLower() + ToCSharpName(p.Name.Replace("@", ""), NameType.None))) : ""
                    );
        }

        public static string ToCSharpName(string name, NameType nameType, ITableMetadata foreign = null)
        {
            string result = string.Empty;

            switch (nameType)
            {
                case NameType.Schema:
                    result = NamingRulesProvider.Current.SchemaName(name);
                    break;

                case NameType.Table:
                    result = NamingRulesProvider.Current.TableName(name);
                    break;

                case NameType.Column:
                    result = NamingRulesProvider.Current.ColumnName(name);
                    break;

                case NameType.RelationColumn:
                    result = NamingRulesProvider.Current.RelationColumnName(foreign, name, false);
                    break;

                case NameType.ReverseRelationColumn:
                    result = NamingRulesProvider.Current.RelationColumnName(foreign, name, true);
                    break;

                case NameType.Procedure:
                    result = NamingRulesProvider.Current.ProcedureName(name);
                    break;

                case NameType.None:
                    result = name.ToUpperCamelCase().ConvertToCSharpName();
                    break;
            }

            return result.ToUpperCamelCase().ConvertToCSharpName();
        }
    }
}
