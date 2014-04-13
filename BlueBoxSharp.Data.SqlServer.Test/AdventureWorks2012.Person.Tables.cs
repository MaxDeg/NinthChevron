using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.Person
{
	[Table("Address", "Person", "AdventureWorks2012")]
	public partial class Address : Entity<Address>
	{
		static Address()
		{
			Join<BusinessEntityAddress>(t => t.AddressIDBusinessEntityAddress, (t, f) => t.AddressID == f.AddressID); // Reverse Relation
			Join<SalesOrderHeader>(t => t.BillToAddressIDSalesOrderHeader, (t, f) => t.AddressID == f.BillToAddressID); // Reverse Relation
			Join<SalesOrderHeader>(t => t.ShipToAddressIDSalesOrderHeader, (t, f) => t.AddressID == f.ShipToAddressID); // Reverse Relation
			Join<StateProvince>(t => t.StateProvinceID, (t, f) => t.StateProvinceID == f.StateProvinceID); // Relation
		}

		
        [NotifyPropertyChanged, Column("AddressID", true, true, false)]
        public int AddressID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("AddressLine1", false, false, false)]
        public string AddressLine1 { get; set; }
		
        [NotifyPropertyChanged, Column("AddressLine2", false, false, true)]
        public string AddressLine2 { get; set; }
		
        [NotifyPropertyChanged, Column("City", false, false, false)]
        public string City { get; set; }
		
        [NotifyPropertyChanged, Column("StateProvinceID", true, false, false)]
        public int StateProvinceID { get; set; }
		
        [NotifyPropertyChanged, Column("PostalCode", false, false, false)]
        public string PostalCode { get; set; }
		
        [NotifyPropertyChanged, Column("SpatialLocation", false, false, true)]
        public object SpatialLocation { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntityAddress AddressIDBusinessEntityAddress { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader BillToAddressIDSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader ShipToAddressIDSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public StateProvince StateProvinceID { get; set; }
	}

	[Table("AddressType", "Person", "AdventureWorks2012")]
	public partial class AddressType : Entity<AddressType>
	{
		static AddressType()
		{
			Join<BusinessEntityAddress>(t => t.AddressTypeIDBusinessEntityAddress, (t, f) => t.AddressTypeID == f.AddressTypeID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("AddressTypeID", true, true, false)]
        public int AddressTypeID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntityAddress AddressTypeIDBusinessEntityAddress { get; set; }
	}

	[Table("BusinessEntity", "Person", "AdventureWorks2012")]
	public partial class BusinessEntity : Entity<BusinessEntity>
	{
		static BusinessEntity()
		{
			Join<BusinessEntityAddress>(t => t.BusinessEntityIDBusinessEntityAddress, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<BusinessEntityContact>(t => t.BusinessEntityIDBusinessEntityContact, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Person>(t => t.BusinessEntityIDPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Vendor>(t => t.BusinessEntityIDVendor, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Store>(t => t.BusinessEntityIDStore, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, true, false)]
        public int BusinessEntityID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntityAddress BusinessEntityIDBusinessEntityAddress { get; set; }
		
        [InnerJoinColumn]
        public BusinessEntityContact BusinessEntityIDBusinessEntityContact { get; set; }
		
        [InnerJoinColumn]
        public Person BusinessEntityIDPerson { get; set; }
		
        [InnerJoinColumn]
        public Vendor BusinessEntityIDVendor { get; set; }
		
        [InnerJoinColumn]
        public Store BusinessEntityIDStore { get; set; }
	}

	[Table("BusinessEntityAddress", "Person", "AdventureWorks2012")]
	public partial class BusinessEntityAddress : Entity<BusinessEntityAddress>
	{
		static BusinessEntityAddress()
		{
			Join<Address>(t => t.AddressID, (t, f) => t.AddressID == f.AddressID); // Relation
			Join<AddressType>(t => t.AddressTypeID, (t, f) => t.AddressTypeID == f.AddressTypeID); // Relation
			Join<BusinessEntity>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("AddressID", true, false, false)]
        public int AddressID { get; set; }
		
        [NotifyPropertyChanged, Column("AddressTypeID", true, false, false)]
        public int AddressTypeID { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Address AddressID { get; set; }
		
        [InnerJoinColumn]
        public AddressType AddressTypeID { get; set; }
		
        [InnerJoinColumn]
        public BusinessEntity BusinessEntityID { get; set; }
	}

	[Table("BusinessEntityContact", "Person", "AdventureWorks2012")]
	public partial class BusinessEntityContact : Entity<BusinessEntityContact>
	{
		static BusinessEntityContact()
		{
			Join<BusinessEntity>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<ContactType>(t => t.ContactTypeID, (t, f) => t.ContactTypeID == f.ContactTypeID); // Relation
			Join<Person>(t => t.PersonID, (t, f) => t.PersonID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("PersonID", true, false, false)]
        public int PersonID { get; set; }
		
        [NotifyPropertyChanged, Column("ContactTypeID", true, false, false)]
        public int ContactTypeID { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntity BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public ContactType ContactTypeID { get; set; }
		
        [InnerJoinColumn]
        public Person PersonID { get; set; }
	}

	[Table("ContactType", "Person", "AdventureWorks2012")]
	public partial class ContactType : Entity<ContactType>
	{
		static ContactType()
		{
			Join<BusinessEntityContact>(t => t.ContactTypeIDBusinessEntityContact, (t, f) => t.ContactTypeID == f.ContactTypeID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ContactTypeID", true, true, false)]
        public int ContactTypeID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntityContact ContactTypeIDBusinessEntityContact { get; set; }
	}

	[Table("CountryRegion", "Person", "AdventureWorks2012")]
	public partial class CountryRegion : Entity<CountryRegion>
	{
		static CountryRegion()
		{
			Join<StateProvince>(t => t.CountryRegionCodeStateProvince, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Reverse Relation
			Join<CountryRegionCurrency>(t => t.CountryRegionCodeCountryRegionCurrency, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Reverse Relation
			Join<SalesTerritory>(t => t.CountryRegionCodeSalesTerritory, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("CountryRegionCode", true, false, false)]
        public string CountryRegionCode { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public StateProvince CountryRegionCodeStateProvince { get; set; }
		
        [InnerJoinColumn]
        public CountryRegionCurrency CountryRegionCodeCountryRegionCurrency { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory CountryRegionCodeSalesTerritory { get; set; }
	}

	[Table("EmailAddress", "Person", "AdventureWorks2012")]
	public partial class EmailAddress : Entity<EmailAddress>
	{
		static EmailAddress()
		{
			Join<Person>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("EmailAddressID", true, true, false)]
        public int EmailAddressID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("EmailAddress", false, false, true)]
        public string EmailAddress { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Person BusinessEntityID { get; set; }
	}

	[Table("Password", "Person", "AdventureWorks2012")]
	public partial class Password : Entity<Password>
	{
		static Password()
		{
			Join<Person>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("PasswordHash", false, false, false)]
        public string PasswordHash { get; set; }
		
        [NotifyPropertyChanged, Column("PasswordSalt", false, false, false)]
        public string PasswordSalt { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Person BusinessEntityID { get; set; }
	}

	[Table("Person", "Person", "AdventureWorks2012")]
	public partial class Person : Entity<Person>
	{
		static Person()
		{
			Join<BusinessEntity>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<Customer>(t => t.PersonIDCustomer, (t, f) => t.BusinessEntityID == f.PersonID); // Reverse Relation
			Join<PersonCreditCard>(t => t.BusinessEntityIDPersonCreditCard, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<PersonPhone>(t => t.BusinessEntityIDPersonPhone, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<BusinessEntityContact>(t => t.PersonIDBusinessEntityContact, (t, f) => t.BusinessEntityID == f.PersonID); // Reverse Relation
			Join<EmailAddress>(t => t.BusinessEntityIDEmailAddress, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Password>(t => t.BusinessEntityIDPassword, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Employee>(t => t.BusinessEntityIDEmployee, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("PersonType", true, false, false)]
        public string PersonType { get; set; }
		
        [NotifyPropertyChanged, Column("NameStyle", false, false, false)]
        public bool NameStyle { get; set; }
		
        [NotifyPropertyChanged, Column("Title", false, false, true)]
        public string Title { get; set; }
		
        [NotifyPropertyChanged, Column("FirstName", false, false, false)]
        public string FirstName { get; set; }
		
        [NotifyPropertyChanged, Column("MiddleName", false, false, true)]
        public string MiddleName { get; set; }
		
        [NotifyPropertyChanged, Column("LastName", false, false, false)]
        public string LastName { get; set; }
		
        [NotifyPropertyChanged, Column("Suffix", false, false, true)]
        public string Suffix { get; set; }
		
        [NotifyPropertyChanged, Column("EmailPromotion", true, false, false)]
        public int EmailPromotion { get; set; }
		
        [NotifyPropertyChanged, Column("AdditionalContactInfo", false, false, true)]
        public object AdditionalContactInfo { get; set; }
		
        [NotifyPropertyChanged, Column("Demographics", false, false, true)]
        public object Demographics { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntity BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public Customer PersonIDCustomer { get; set; }
		
        [InnerJoinColumn]
        public PersonCreditCard BusinessEntityIDPersonCreditCard { get; set; }
		
        [InnerJoinColumn]
        public PersonPhone BusinessEntityIDPersonPhone { get; set; }
		
        [InnerJoinColumn]
        public BusinessEntityContact PersonIDBusinessEntityContact { get; set; }
		
        [InnerJoinColumn]
        public EmailAddress BusinessEntityIDEmailAddress { get; set; }
		
        [InnerJoinColumn]
        public Password BusinessEntityIDPassword { get; set; }
		
        [InnerJoinColumn]
        public Employee BusinessEntityIDEmployee { get; set; }
	}

	[Table("PersonPhone", "Person", "AdventureWorks2012")]
	public partial class PersonPhone : Entity<PersonPhone>
	{
		static PersonPhone()
		{
			Join<Person>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<PhoneNumberType>(t => t.PhoneNumberTypeID, (t, f) => t.PhoneNumberTypeID == f.PhoneNumberTypeID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("PhoneNumber", true, false, false)]
        public string PhoneNumber { get; set; }
		
        [NotifyPropertyChanged, Column("PhoneNumberTypeID", true, false, false)]
        public int PhoneNumberTypeID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Person BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public PhoneNumberType PhoneNumberTypeID { get; set; }
	}

	[Table("PhoneNumberType", "Person", "AdventureWorks2012")]
	public partial class PhoneNumberType : Entity<PhoneNumberType>
	{
		static PhoneNumberType()
		{
			Join<PersonPhone>(t => t.PhoneNumberTypeIDPersonPhone, (t, f) => t.PhoneNumberTypeID == f.PhoneNumberTypeID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("PhoneNumberTypeID", true, true, false)]
        public int PhoneNumberTypeID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public PersonPhone PhoneNumberTypeIDPersonPhone { get; set; }
	}

	[Table("StateProvince", "Person", "AdventureWorks2012")]
	public partial class StateProvince : Entity<StateProvince>
	{
		static StateProvince()
		{
			Join<CountryRegion>(t => t.CountryRegionCode, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Relation
			Join<Address>(t => t.StateProvinceIDAddress, (t, f) => t.StateProvinceID == f.StateProvinceID); // Reverse Relation
			Join<SalesTaxRate>(t => t.StateProvinceIDSalesTaxRate, (t, f) => t.StateProvinceID == f.StateProvinceID); // Reverse Relation
			Join<SalesTerritory>(t => t.TerritoryID, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
		}

		
        [NotifyPropertyChanged, Column("StateProvinceID", true, true, false)]
        public int StateProvinceID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("StateProvinceCode", false, false, false)]
        public string StateProvinceCode { get; set; }
		
        [NotifyPropertyChanged, Column("CountryRegionCode", true, false, false)]
        public string CountryRegionCode { get; set; }
		
        [NotifyPropertyChanged, Column("IsOnlyStateProvinceFlag", false, false, false)]
        public bool IsOnlyStateProvinceFlag { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("TerritoryID", true, false, false)]
        public int TerritoryID { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public CountryRegion CountryRegionCode { get; set; }
		
        [InnerJoinColumn]
        public Address StateProvinceIDAddress { get; set; }
		
        [InnerJoinColumn]
        public SalesTaxRate StateProvinceIDSalesTaxRate { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritoryID { get; set; }
	}


}

