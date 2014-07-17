<#@ template debug="false" hostspecific="true" language="C#" #>
<#@ output extension="?" #>
<#@ assembly name="System.Xml" #>
<#@ assembly name="System.Core.dll" #>
<#@ assembly name="System.Configuration.dll" #>
<#@ assembly name="$(BBSharpCoreLibPath)BlueBoxSharp.Core.dll" #>
<#@ assembly name="$(BBSharpDataLibPath)BlueBoxSharp.Data.dll" #>
<#@ assembly name="$(BBSharpDataSqlServerLibPath)BlueBoxSharp.Data.SqlServer.dll" #>
<#@ import namespace="System.IO" #>
<#@ import namespace="System.Linq" #>
<#@ import namespace="System.Text" #>
<#@ import namespace="System.Xml" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ import namespace="BlueBoxSharp.Data" #>
<#@ import namespace="BlueBoxSharp.Data.Metadata" #>
<#@ import namespace="BlueBoxSharp.Data.SqlServer" #>
<#@ import namespace="BlueBoxSharp.Data.SqlServer.Metadata" #>
<#@ include file="$(BBSharpDataBuildPath)T4TemplateHelpers.t4" #>
<#
	string path = Host.ResolvePath(@"App.config");
	System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(
															new System.Configuration.ExeConfigurationFileMap { ExeConfigFilename = path }, 
															System.Configuration.ConfigurationUserLevel.None
														);
		
	SqlServerMetadata meta = new SqlServerMetadata(configuration.ConnectionStrings.ConnectionStrings["T4Connection"].ConnectionString);

foreach (var group in meta.Tables.GroupBy(t => new { Database = t.Database, Schema = t.Schema })) 
{
	string dbName = GeneratorHelper.ToCSharpName(group.Key.Database, NameType.None);
	string schemaName = GeneratorHelper.ToCSharpName(group.Key.Schema, NameType.Schema);
	string namespaceName = dbName + (!string.IsNullOrEmpty(group.Key.Schema) ? "." + schemaName + "Schema" : "");

#>
using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

<#
foreach (var schema in meta.Tables.Where(t => t.Schema != group.Key.Schema).Select(t => new { Database = t.Database, Schema = t.Schema }).Distinct())
{
#>
using $rootnamespace$.<#= dbName + (!string.IsNullOrEmpty(schema.Schema) ? "." + GeneratorHelper.ToCSharpName(schema.Schema, NameType.Schema) + "Schema" : "") #>;
<#
}
#>

namespace $rootnamespace$.<#= namespaceName #>
{
<# 
foreach (ITableMetadata tab in group) 
{
#>
	[Table("<#= tab.Name #>", "<#= tab.Schema #>", "<#= tab.Database #>")]
	public partial class <#= GeneratorHelper.ToCSharpName(tab.Name, NameType.Table) #> : Entity<<#= GeneratorHelper.ToCSharpName(tab.Name, NameType.Table) #>>
	{
		static <#= GeneratorHelper.ToCSharpName(tab.Name, NameType.Table) #>()
		{
	<# 
	foreach (IRelationColumnMetadata c in tab.Relations) 
	{
	#>
		<#= GeneratorHelper.CreateRelationDefinition(c) #> // <#= c.IsReverseRelation ? "Reverse Relation" : "Relation" #>
	<# 
	}
	#>
	}

	<# 
	foreach (IColumnMetadata c in tab.Columns.Values) 
	{
	#>
	<#= GeneratorHelper.CreateProperty(c) #>
	<#
	}
	#>

	<# 
	foreach (IRelationColumnMetadata c in tab.Relations) 
	{
	#>
	<#= GeneratorHelper.CreateRelationProperty(c) #>
	<# 
	}
	#>
}

<# 
}
#>

}

<# 
	SaveOutput(namespaceName + ".Tables.cs");
#>
using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace $rootnamespace$.<#= namespaceName #>
{
	public class Procedures
	{
<# 
foreach (IProcedureMetadata procedure in meta.Procedures.Where(p => p.Database.Name == group.Key.Database))
{ 
#>
	<#= GeneratorHelper.CreateDelegate(procedure) #>
<# 
}
#>
	}
}
<# 
	SaveOutput(namespaceName + ".Procedures.cs");
#>
<#
}

	DeleteOldOutputs();
#>