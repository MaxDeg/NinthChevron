using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.HumanResources
{
	[Table("Department", "HumanResources", "AdventureWorks2012")]
	public partial class Department : Entity<Department>
	{
		static Department()
		{
			Join<EmployeeDepartmentHistory>(t => t.DepartmentIDEmployeeDepartmentHistory, (t, f) => t.DepartmentID == f.DepartmentID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("DepartmentID", true, true, false)]
        public short DepartmentID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<short>() : (short)Convert.ChangeType(this.EntityIdentity, typeof(short)); }
	        set { this.EntityIdentity = (short)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("GroupName", false, false, false)]
        public string GroupName { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public EmployeeDepartmentHistory DepartmentIDEmployeeDepartmentHistory { get; set; }
	}

	[Table("Employee", "HumanResources", "AdventureWorks2012")]
	public partial class Employee : Entity<Employee>
	{
		static Employee()
		{
			Join<EmployeeDepartmentHistory>(t => t.BusinessEntityIDEmployeeDepartmentHistory, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<EmployeePayHistory>(t => t.BusinessEntityIDEmployeePayHistory, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<JobCandidate>(t => t.BusinessEntityIDJobCandidate, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Document>(t => t.OwnerDocument, (t, f) => t.BusinessEntityID == f.Owner); // Reverse Relation
			Join<PurchaseOrderHeader>(t => t.EmployeeIDPurchaseOrderHeader, (t, f) => t.BusinessEntityID == f.EmployeeID); // Reverse Relation
			Join<SalesPerson>(t => t.BusinessEntityIDSalesPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Person>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("NationalIDNumber", false, false, false)]
        public string NationalIDNumber { get; set; }
		
        [NotifyPropertyChanged, Column("LoginID", false, false, false)]
        public string LoginID { get; set; }
		
        [NotifyPropertyChanged, Column("OrganizationNode", false, false, true)]
        public object OrganizationNode { get; set; }
		
        [NotifyPropertyChanged, Column("OrganizationLevel", false, false, true)]
        public System.Nullable<short> OrganizationLevel { get; set; }
		
        [NotifyPropertyChanged, Column("JobTitle", false, false, false)]
        public string JobTitle { get; set; }
		
        [NotifyPropertyChanged, Column("BirthDate", true, false, false)]
        public object BirthDate { get; set; }
		
        [NotifyPropertyChanged, Column("MaritalStatus", true, false, false)]
        public string MaritalStatus { get; set; }
		
        [NotifyPropertyChanged, Column("Gender", true, false, false)]
        public string Gender { get; set; }
		
        [NotifyPropertyChanged, Column("HireDate", true, false, false)]
        public object HireDate { get; set; }
		
        [NotifyPropertyChanged, Column("SalariedFlag", false, false, false)]
        public bool SalariedFlag { get; set; }
		
        [NotifyPropertyChanged, Column("VacationHours", true, false, false)]
        public short VacationHours { get; set; }
		
        [NotifyPropertyChanged, Column("SickLeaveHours", true, false, false)]
        public short SickLeaveHours { get; set; }
		
        [NotifyPropertyChanged, Column("CurrentFlag", false, false, false)]
        public bool CurrentFlag { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public EmployeeDepartmentHistory BusinessEntityIDEmployeeDepartmentHistory { get; set; }
		
        [InnerJoinColumn]
        public EmployeePayHistory BusinessEntityIDEmployeePayHistory { get; set; }
		
        [InnerJoinColumn]
        public JobCandidate BusinessEntityIDJobCandidate { get; set; }
		
        [InnerJoinColumn]
        public Document OwnerDocument { get; set; }
		
        [InnerJoinColumn]
        public PurchaseOrderHeader EmployeeIDPurchaseOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesPerson BusinessEntityIDSalesPerson { get; set; }
		
        [InnerJoinColumn]
        public Person BusinessEntityID { get; set; }
	}

	[Table("EmployeeDepartmentHistory", "HumanResources", "AdventureWorks2012")]
	public partial class EmployeeDepartmentHistory : Entity<EmployeeDepartmentHistory>
	{
		static EmployeeDepartmentHistory()
		{
			Join<Department>(t => t.DepartmentID, (t, f) => t.DepartmentID == f.DepartmentID); // Relation
			Join<Employee>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<Shift>(t => t.ShiftID, (t, f) => t.ShiftID == f.ShiftID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("DepartmentID", true, false, false)]
        public short DepartmentID { get; set; }
		
        [NotifyPropertyChanged, Column("ShiftID", true, false, false)]
        public byte ShiftID { get; set; }
		
        [NotifyPropertyChanged, Column("StartDate", true, false, false)]
        public object StartDate { get; set; }
		
        [NotifyPropertyChanged, Column("EndDate", true, false, true)]
        public object EndDate { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Department DepartmentID { get; set; }
		
        [InnerJoinColumn]
        public Employee BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public Shift ShiftID { get; set; }
	}

	[Table("EmployeePayHistory", "HumanResources", "AdventureWorks2012")]
	public partial class EmployeePayHistory : Entity<EmployeePayHistory>
	{
		static EmployeePayHistory()
		{
			Join<Employee>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("RateChangeDate", true, false, false)]
        public System.DateTime RateChangeDate { get; set; }
		
        [NotifyPropertyChanged, Column("Rate", true, false, false)]
        public object Rate { get; set; }
		
        [NotifyPropertyChanged, Column("PayFrequency", true, false, false)]
        public byte PayFrequency { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Employee BusinessEntityID { get; set; }
	}

	[Table("JobCandidate", "HumanResources", "AdventureWorks2012")]
	public partial class JobCandidate : Entity<JobCandidate>
	{
		static JobCandidate()
		{
			Join<Employee>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("JobCandidateID", true, true, false)]
        public int JobCandidateID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, true)]
        public System.Nullable<int> BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("Resume", false, false, true)]
        public object Resume { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Employee BusinessEntityID { get; set; }
	}

	[Table("Shift", "HumanResources", "AdventureWorks2012")]
	public partial class Shift : Entity<Shift>
	{
		static Shift()
		{
			Join<EmployeeDepartmentHistory>(t => t.ShiftIDEmployeeDepartmentHistory, (t, f) => t.ShiftID == f.ShiftID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ShiftID", true, true, false)]
        public byte ShiftID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this.EntityIdentity, typeof(byte)); }
	        set { this.EntityIdentity = (byte)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("StartTime", false, false, false)]
        public object StartTime { get; set; }
		
        [NotifyPropertyChanged, Column("EndTime", false, false, false)]
        public object EndTime { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public EmployeeDepartmentHistory ShiftIDEmployeeDepartmentHistory { get; set; }
	}


}

