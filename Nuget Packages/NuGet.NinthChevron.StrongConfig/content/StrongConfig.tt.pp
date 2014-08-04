<#@ template debug="false" hostspecific="true" language="C#" #>
<#@ assembly name="System.Core" #>
<#@ assembly name="System.Configuration.dll" #>
<#@ assembly name="$(NCStrongConfigLibPath)BlueBoxSharp.Core.dll" #>
<#@ import namespace="System.Linq" #>
<#@ import namespace="System.Text" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ import namespace="System.Configuration" #>
<#@ import namespace="BlueBoxSharp.Extensions" #>
<#@ output extension=".cs" #>
<#
	string path = Host.ResolvePath(@"App.config");
	Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap { ExeConfigFilename = path }, ConfigurationUserLevel.None);
#>
using System;
using System.Configuration;

namespace $rootnamespace$
{
	public static class AppSettings
	{
	<# 
	foreach (string key in configuration.AppSettings.Settings.AllKeys)
	{
	#>
	public static string <#= key.ToUpperCamelCase() #> { get { return ConfigurationManager.AppSettings["<#= key #>"]; } }
	<#
	}
	#>
}
	
	public static class ConnectionStrings
	{
	<# 
	foreach (ConnectionStringSettings obj in configuration.ConnectionStrings.ConnectionStrings)
	{
	#>
	public static string <#= obj.Name.ToUpperCamelCase() #> { get { return ConfigurationManager.ConnectionStrings["<#= obj.Name #>"].ConnectionString; } }
	<#
	}
	#>
}
}