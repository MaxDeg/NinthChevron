using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012
{
	public class Procedures
	{
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @StartProductID   int(10)</para>
        /// <para>[IN] @CheckDate   datetime</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspGetBillOfMaterials(DataContext context, System.Nullable<int> inStartProductID, System.Nullable<System.DateTime> inCheckDate) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[dbo].[uspGetBillOfMaterials]", inStartProductID, inCheckDate);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @BusinessEntityID   int(10)</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspGetEmployeeManagers(DataContext context, System.Nullable<int> inBusinessEntityID) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[dbo].[uspGetEmployeeManagers]", inBusinessEntityID);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @BusinessEntityID   int(10)</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspGetManagerEmployees(DataContext context, System.Nullable<int> inBusinessEntityID) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[dbo].[uspGetManagerEmployees]", inBusinessEntityID);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @StartProductID   int(10)</para>
        /// <para>[IN] @CheckDate   datetime</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspGetWhereUsedProductID(DataContext context, System.Nullable<int> inStartProductID, System.Nullable<System.DateTime> inCheckDate) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[dbo].[uspGetWhereUsedProductID]", inStartProductID, inCheckDate);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[INOUT] @ErrorLogID   int(10)</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspLogError(DataContext context, System.Nullable<int> inoutErrorLogID) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[dbo].[uspLogError]", inoutErrorLogID);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// </summary>
        public static IEnumerable<DataRecord> UspPrintError(DataContext context) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[dbo].[uspPrintError]");
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @searchString   nvarchar(1000)</para>
        /// <para>[IN] @useInflectional   bit</para>
        /// <para>[IN] @useThesaurus   bit</para>
        /// <para>[IN] @language   int(10)</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspSearchCandidateResumes(DataContext context, string inSearchString, System.Nullable<bool> inUseInflectional, System.Nullable<bool> inUseThesaurus, System.Nullable<int> inLanguage) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[dbo].[uspSearchCandidateResumes]", inSearchString, inUseInflectional, inUseThesaurus, inLanguage);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @BusinessEntityID   int(10)</para>
        /// <para>[IN] @JobTitle   nvarchar(50)</para>
        /// <para>[IN] @HireDate   datetime</para>
        /// <para>[IN] @RateChangeDate   datetime</para>
        /// <para>[IN] @Rate   money(19)</para>
        /// <para>[IN] @PayFrequency   tinyint(3)</para>
        /// <para>[IN] @CurrentFlag   bit</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspUpdateEmployeeHireInfo(DataContext context, System.Nullable<int> inBusinessEntityID, string inJobTitle, System.Nullable<System.DateTime> inHireDate, System.Nullable<System.DateTime> inRateChangeDate, object inRate, System.Nullable<byte> inPayFrequency, System.Nullable<bool> inCurrentFlag) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[HumanResources].[uspUpdateEmployeeHireInfo]", inBusinessEntityID, inJobTitle, inHireDate, inRateChangeDate, inRate, inPayFrequency, inCurrentFlag);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @BusinessEntityID   int(10)</para>
        /// <para>[IN] @OrganizationNode   hierarchyid(892)</para>
        /// <para>[IN] @LoginID   nvarchar(256)</para>
        /// <para>[IN] @JobTitle   nvarchar(50)</para>
        /// <para>[IN] @HireDate   datetime</para>
        /// <para>[IN] @CurrentFlag   bit</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspUpdateEmployeeLogin(DataContext context, System.Nullable<int> inBusinessEntityID, object inOrganizationNode, string inLoginID, string inJobTitle, System.Nullable<System.DateTime> inHireDate, System.Nullable<bool> inCurrentFlag) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[HumanResources].[uspUpdateEmployeeLogin]", inBusinessEntityID, inOrganizationNode, inLoginID, inJobTitle, inHireDate, inCurrentFlag);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] @BusinessEntityID   int(10)</para>
        /// <para>[IN] @NationalIDNumber   nvarchar(15)</para>
        /// <para>[IN] @BirthDate   datetime</para>
        /// <para>[IN] @MaritalStatus   nchar(1)</para>
        /// <para>[IN] @Gender   nchar(1)</para>
        /// </summary>
        public static IEnumerable<DataRecord> UspUpdateEmployeePersonalInfo(DataContext context, System.Nullable<int> inBusinessEntityID, string inNationalIDNumber, System.Nullable<System.DateTime> inBirthDate, string inMaritalStatus, string inGender) 
        { 
            return context.ExecuteProcedure("[AdventureWorks2012]..[HumanResources].[uspUpdateEmployeePersonalInfo]", inBusinessEntityID, inNationalIDNumber, inBirthDate, inMaritalStatus, inGender);
        }
	}
}
