using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

using BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012;
using BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.HumanResourcesSchema;
using BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.ProductionSchema;
using BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.PurchasingSchema;
using BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.SalesSchema;

namespace BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.PersonSchema
{
	[Table("Address", "Person", "AdventureWorks2012")]
	public partial class Address : Entity<Address>
	{
		static Address()
		{
			Join<BusinessEntityAddress>(t => t.AddressBusinessEntityAddress, (t, f) => t.AddressID == f.AddressID); // Reverse Relation
			Join<SalesOrderHeader>(t => t.BillToAddressSalesOrderHeader, (t, f) => t.AddressID == f.BillToAddressID); // Reverse Relation
			Join<SalesOrderHeader>(t => t.ShipToAddressSalesOrderHeader, (t, f) => t.AddressID == f.ShipToAddressID); // Reverse Relation
			Join<StateProvince>(t => t.StateProvinceStateProvince, (t, f) => t.StateProvinceID == f.StateProvinceID); // Relation
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
        public BusinessEntityAddress AddressBusinessEntityAddress { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader BillToAddressSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader ShipToAddressSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public StateProvince StateProvinceStateProvince { get; set; }
	}

	[Table("AddressType", "Person", "AdventureWorks2012")]
	public partial class AddressType : Entity<AddressType>
	{
		static AddressType()
		{
			Join<BusinessEntityAddress>(t => t.AddressTypeBusinessEntityAddress, (t, f) => t.AddressTypeID == f.AddressTypeID); // Reverse Relation
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
        public BusinessEntityAddress AddressTypeBusinessEntityAddress { get; set; }
	}

	[Table("BusinessEntity", "Person", "AdventureWorks2012")]
	public partial class BusinessEntity : Entity<BusinessEntity>
	{
		static BusinessEntity()
		{
			Join<BusinessEntityAddress>(t => t.BusinessEntityBusinessEntityAddress, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<BusinessEntityContact>(t => t.BusinessEntityBusinessEntityContact, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Person>(t => t.BusinessEntityPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Vendor>(t => t.BusinessEntityVendor, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Store>(t => t.BusinessEntityStore, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
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
        public BusinessEntityAddress BusinessEntityBusinessEntityAddress { get; set; }
		
        [InnerJoinColumn]
        public BusinessEntityContact BusinessEntityBusinessEntityContact { get; set; }
		
        [InnerJoinColumn]
        public Person BusinessEntityPerson { get; set; }
		
        [InnerJoinColumn]
        public Vendor BusinessEntityVendor { get; set; }
		
        [InnerJoinColumn]
        public Store BusinessEntityStore { get; set; }
	}

	[Table("BusinessEntityAddress", "Person", "AdventureWorks2012")]
	public partial class BusinessEntityAddress : Entity<BusinessEntityAddress>
	{
		static BusinessEntityAddress()
		{
			Join<Address>(t => t.AddressAddress, (t, f) => t.AddressID == f.AddressID); // Relation
			Join<AddressType>(t => t.AddressTypeAddressType, (t, f) => t.AddressTypeID == f.AddressTypeID); // Relation
			Join<BusinessEntity>(t => t.BusinessEntityBusinessEntity, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
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
        public Address AddressAddress { get; set; }
		
        [InnerJoinColumn]
        public AddressType AddressTypeAddressType { get; set; }
		
        [InnerJoinColumn]
        public BusinessEntity BusinessEntityBusinessEntity { get; set; }
	}

	[Table("BusinessEntityContact", "Person", "AdventureWorks2012")]
	public partial class BusinessEntityContact : Entity<BusinessEntityContact>
	{
		static BusinessEntityContact()
		{
			Join<BusinessEntity>(t => t.BusinessEntityBusinessEntity, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<ContactType>(t => t.ContactTypeContactType, (t, f) => t.ContactTypeID == f.ContactTypeID); // Relation
			Join<Person>(t => t.PersonPerson, (t, f) => t.PersonID == f.BusinessEntityID); // Relation
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
        public BusinessEntity BusinessEntityBusinessEntity { get; set; }
		
        [InnerJoinColumn]
        public ContactType ContactTypeContactType { get; set; }
		
        [InnerJoinColumn]
        public Person PersonPerson { get; set; }
	}

	[Table("ContactType", "Person", "AdventureWorks2012")]
	public partial class ContactType : Entity<ContactType>
	{
		static ContactType()
		{
			Join<BusinessEntityContact>(t => t.ContactTypeBusinessEntityContact, (t, f) => t.ContactTypeID == f.ContactTypeID); // Reverse Relation
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
        public BusinessEntityContact ContactTypeBusinessEntityContact { get; set; }
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
			Join<Person>(t => t.BusinessEntityPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
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
        public string EmailAddress_ { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Person BusinessEntityPerson { get; set; }
	}

	[Table("Password", "Person", "AdventureWorks2012")]
	public partial class Password : Entity<Password>
	{
		static Password()
		{
			Join<Person>(t => t.BusinessEntityPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
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
        public Person BusinessEntityPerson { get; set; }
	}

	[Table("Person", "Person", "AdventureWorks2012")]
	public partial class Person : Entity<Person>
	{
		static Person()
		{
			Join<BusinessEntity>(t => t.BusinessEntityBusinessEntity, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<Customer>(t => t.PersonCustomer, (t, f) => t.BusinessEntityID == f.PersonID); // Reverse Relation
			Join<PersonCreditCard>(t => t.BusinessEntityPersonCreditCard, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<PersonPhone>(t => t.BusinessEntityPersonPhone, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<BusinessEntityContact>(t => t.PersonBusinessEntityContact, (t, f) => t.BusinessEntityID == f.PersonID); // Reverse Relation
			Join<EmailAddress>(t => t.BusinessEntityEmailAddress, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Password>(t => t.BusinessEntityPassword, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Employee>(t => t.BusinessEntityEmployee, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
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
        public BusinessEntity BusinessEntityBusinessEntity { get; set; }
		
        [InnerJoinColumn]
        public Customer PersonCustomer { get; set; }
		
        [InnerJoinColumn]
        public PersonCreditCard BusinessEntityPersonCreditCard { get; set; }
		
        [InnerJoinColumn]
        public PersonPhone BusinessEntityPersonPhone { get; set; }
		
        [InnerJoinColumn]
        public BusinessEntityContact PersonBusinessEntityContact { get; set; }
		
        [InnerJoinColumn]
        public EmailAddress BusinessEntityEmailAddress { get; set; }
		
        [InnerJoinColumn]
        public Password BusinessEntityPassword { get; set; }
		
        [InnerJoinColumn]
        public Employee BusinessEntityEmployee { get; set; }
	}

	[Table("PersonPhone", "Person", "AdventureWorks2012")]
	public partial class PersonPhone : Entity<PersonPhone>
	{
		static PersonPhone()
		{
			Join<Person>(t => t.BusinessEntityPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<PhoneNumberType>(t => t.PhoneNumberTypePhoneNumberType, (t, f) => t.PhoneNumberTypeID == f.PhoneNumberTypeID); // Relation
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
        public Person BusinessEntityPerson { get; set; }
		
        [InnerJoinColumn]
        public PhoneNumberType PhoneNumberTypePhoneNumberType { get; set; }
	}

	[Table("PhoneNumberType", "Person", "AdventureWorks2012")]
	public partial class PhoneNumberType : Entity<PhoneNumberType>
	{
		static PhoneNumberType()
		{
			Join<PersonPhone>(t => t.PhoneNumberTypePersonPhone, (t, f) => t.PhoneNumberTypeID == f.PhoneNumberTypeID); // Reverse Relation
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
        public PersonPhone PhoneNumberTypePersonPhone { get; set; }
	}

	[Table("StateProvince", "Person", "AdventureWorks2012")]
	public partial class StateProvince : Entity<StateProvince>
	{
		static StateProvince()
		{
			Join<CountryRegion>(t => t.CountryRegionCodeCountryRegion, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Relation
			Join<Address>(t => t.StateProvinceAddress, (t, f) => t.StateProvinceID == f.StateProvinceID); // Reverse Relation
			Join<SalesTaxRate>(t => t.StateProvinceSalesTaxRate, (t, f) => t.StateProvinceID == f.StateProvinceID); // Reverse Relation
			Join<SalesTerritory>(t => t.TerritorySalesTerritory, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
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
        public CountryRegion CountryRegionCodeCountryRegion { get; set; }
		
        [InnerJoinColumn]
        public Address StateProvinceAddress { get; set; }
		
        [InnerJoinColumn]
        public SalesTaxRate StateProvinceSalesTaxRate { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritorySalesTerritory { get; set; }
	}


}

