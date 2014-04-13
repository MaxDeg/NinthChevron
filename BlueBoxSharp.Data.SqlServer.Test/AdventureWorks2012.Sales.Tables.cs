using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.Sales
{
	[Table("CountryRegionCurrency", "Sales", "AdventureWorks2012")]
	public partial class CountryRegionCurrency : Entity<CountryRegionCurrency>
	{
		static CountryRegionCurrency()
		{
			Join<CountryRegion>(t => t.CountryRegionCode, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Relation
			Join<Currency>(t => t.CurrencyCode, (t, f) => t.CurrencyCode == f.CurrencyCode); // Relation
		}

		
        [NotifyPropertyChanged, Column("CountryRegionCode", true, false, false)]
        public string CountryRegionCode { get; set; }
		
        [NotifyPropertyChanged, Column("CurrencyCode", true, false, false)]
        public string CurrencyCode { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public CountryRegion CountryRegionCode { get; set; }
		
        [InnerJoinColumn]
        public Currency CurrencyCode { get; set; }
	}

	[Table("CreditCard", "Sales", "AdventureWorks2012")]
	public partial class CreditCard : Entity<CreditCard>
	{
		static CreditCard()
		{
			Join<PersonCreditCard>(t => t.CreditCardIDPersonCreditCard, (t, f) => t.CreditCardID == f.CreditCardID); // Reverse Relation
			Join<SalesOrderHeader>(t => t.CreditCardIDSalesOrderHeader, (t, f) => t.CreditCardID == f.CreditCardID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("CreditCardID", true, true, false)]
        public int CreditCardID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("CardType", false, false, false)]
        public string CardType { get; set; }
		
        [NotifyPropertyChanged, Column("CardNumber", false, false, false)]
        public string CardNumber { get; set; }
		
        [NotifyPropertyChanged, Column("ExpMonth", false, false, false)]
        public byte ExpMonth { get; set; }
		
        [NotifyPropertyChanged, Column("ExpYear", false, false, false)]
        public short ExpYear { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public PersonCreditCard CreditCardIDPersonCreditCard { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader CreditCardIDSalesOrderHeader { get; set; }
	}

	[Table("Currency", "Sales", "AdventureWorks2012")]
	public partial class Currency : Entity<Currency>
	{
		static Currency()
		{
			Join<CountryRegionCurrency>(t => t.CurrencyCodeCountryRegionCurrency, (t, f) => t.CurrencyCode == f.CurrencyCode); // Reverse Relation
			Join<CurrencyRate>(t => t.FromCurrencyCodeCurrencyRate, (t, f) => t.CurrencyCode == f.FromCurrencyCode); // Reverse Relation
			Join<CurrencyRate>(t => t.ToCurrencyCodeCurrencyRate, (t, f) => t.CurrencyCode == f.ToCurrencyCode); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("CurrencyCode", true, false, false)]
        public string CurrencyCode { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public CountryRegionCurrency CurrencyCodeCountryRegionCurrency { get; set; }
		
        [InnerJoinColumn]
        public CurrencyRate FromCurrencyCodeCurrencyRate { get; set; }
		
        [InnerJoinColumn]
        public CurrencyRate ToCurrencyCodeCurrencyRate { get; set; }
	}

	[Table("CurrencyRate", "Sales", "AdventureWorks2012")]
	public partial class CurrencyRate : Entity<CurrencyRate>
	{
		static CurrencyRate()
		{
			Join<Currency>(t => t.FromCurrencyCode, (t, f) => t.FromCurrencyCode == f.CurrencyCode); // Relation
			Join<Currency>(t => t.ToCurrencyCode, (t, f) => t.ToCurrencyCode == f.CurrencyCode); // Relation
			Join<SalesOrderHeader>(t => t.CurrencyRateIDSalesOrderHeader, (t, f) => t.CurrencyRateID == f.CurrencyRateID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("CurrencyRateID", true, true, false)]
        public int CurrencyRateID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("CurrencyRateDate", false, false, false)]
        public System.DateTime CurrencyRateDate { get; set; }
		
        [NotifyPropertyChanged, Column("FromCurrencyCode", true, false, false)]
        public string FromCurrencyCode { get; set; }
		
        [NotifyPropertyChanged, Column("ToCurrencyCode", true, false, false)]
        public string ToCurrencyCode { get; set; }
		
        [NotifyPropertyChanged, Column("AverageRate", false, false, false)]
        public object AverageRate { get; set; }
		
        [NotifyPropertyChanged, Column("EndOfDayRate", false, false, false)]
        public object EndOfDayRate { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Currency FromCurrencyCode { get; set; }
		
        [InnerJoinColumn]
        public Currency ToCurrencyCode { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader CurrencyRateIDSalesOrderHeader { get; set; }
	}

	[Table("Customer", "Sales", "AdventureWorks2012")]
	public partial class Customer : Entity<Customer>
	{
		static Customer()
		{
			Join<Person>(t => t.PersonID, (t, f) => t.PersonID == f.BusinessEntityID); // Relation
			Join<SalesOrderHeader>(t => t.CustomerIDSalesOrderHeader, (t, f) => t.CustomerID == f.CustomerID); // Reverse Relation
			Join<SalesTerritory>(t => t.TerritoryID, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
			Join<Store>(t => t.StoreID, (t, f) => t.StoreID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("CustomerID", true, true, false)]
        public int CustomerID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("PersonID", true, false, true)]
        public System.Nullable<int> PersonID { get; set; }
		
        [NotifyPropertyChanged, Column("StoreID", true, false, true)]
        public System.Nullable<int> StoreID { get; set; }
		
        [NotifyPropertyChanged, Column("TerritoryID", true, false, true)]
        public System.Nullable<int> TerritoryID { get; set; }
		
        [NotifyPropertyChanged, Column("AccountNumber", false, false, false)]
        public string AccountNumber { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Person PersonID { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader CustomerIDSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritoryID { get; set; }
		
        [InnerJoinColumn]
        public Store StoreID { get; set; }
	}

	[Table("PersonCreditCard", "Sales", "AdventureWorks2012")]
	public partial class PersonCreditCard : Entity<PersonCreditCard>
	{
		static PersonCreditCard()
		{
			Join<Person>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<CreditCard>(t => t.CreditCardID, (t, f) => t.CreditCardID == f.CreditCardID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("CreditCardID", true, false, false)]
        public int CreditCardID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Person BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public CreditCard CreditCardID { get; set; }
	}

	[Table("SalesOrderDetail", "Sales", "AdventureWorks2012")]
	public partial class SalesOrderDetail : Entity<SalesOrderDetail>
	{
		static SalesOrderDetail()
		{
			Join<SalesOrderHeader>(t => t.SalesOrderID, (t, f) => t.SalesOrderID == f.SalesOrderID); // Relation
			Join<SpecialOfferProduct>(t => t.ProductID, (t, f) => t.ProductID == f.SpecialOfferID); // Relation
			Join<SpecialOfferProduct>(t => t.SpecialOfferID, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Relation
			Join<SpecialOfferProduct>(t => t.SpecialOfferID, (t, f) => t.SpecialOfferID == f.ProductID); // Relation
			Join<SpecialOfferProduct>(t => t.ProductID, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("SalesOrderID", true, false, false)]
        public int SalesOrderID { get; set; }
		
        [NotifyPropertyChanged, Column("SalesOrderDetailID", true, true, false)]
        public int SalesOrderDetailID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("CarrierTrackingNumber", false, false, true)]
        public string CarrierTrackingNumber { get; set; }
		
        [NotifyPropertyChanged, Column("OrderQty", true, false, false)]
        public short OrderQty { get; set; }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("SpecialOfferID", true, false, false)]
        public int SpecialOfferID { get; set; }
		
        [NotifyPropertyChanged, Column("UnitPrice", true, false, false)]
        public object UnitPrice { get; set; }
		
        [NotifyPropertyChanged, Column("UnitPriceDiscount", true, false, false)]
        public object UnitPriceDiscount { get; set; }
		
        [NotifyPropertyChanged, Column("LineTotal", false, false, false)]
        public object LineTotal { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public SalesOrderHeader SalesOrderID { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct ProductID { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct SpecialOfferID { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct SpecialOfferID { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct ProductID { get; set; }
	}

	[Table("SalesOrderHeader", "Sales", "AdventureWorks2012")]
	public partial class SalesOrderHeader : Entity<SalesOrderHeader>
	{
		static SalesOrderHeader()
		{
			Join<Address>(t => t.BillToAddressID, (t, f) => t.BillToAddressID == f.AddressID); // Relation
			Join<Address>(t => t.ShipToAddressID, (t, f) => t.ShipToAddressID == f.AddressID); // Relation
			Join<ShipMethod>(t => t.ShipMethodID, (t, f) => t.ShipMethodID == f.ShipMethodID); // Relation
			Join<CreditCard>(t => t.CreditCardID, (t, f) => t.CreditCardID == f.CreditCardID); // Relation
			Join<CurrencyRate>(t => t.CurrencyRateID, (t, f) => t.CurrencyRateID == f.CurrencyRateID); // Relation
			Join<Customer>(t => t.CustomerID, (t, f) => t.CustomerID == f.CustomerID); // Relation
			Join<SalesOrderHeaderSalesReason>(t => t.SalesOrderIDSalesOrderHeaderSalesReason, (t, f) => t.SalesOrderID == f.SalesOrderID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.SalesOrderIDSalesOrderDetail, (t, f) => t.SalesOrderID == f.SalesOrderID); // Reverse Relation
			Join<SalesPerson>(t => t.SalesPersonID, (t, f) => t.SalesPersonID == f.BusinessEntityID); // Relation
			Join<SalesTerritory>(t => t.TerritoryID, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
		}

		
        [NotifyPropertyChanged, Column("SalesOrderID", true, true, false)]
        public int SalesOrderID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("RevisionNumber", false, false, false)]
        public byte RevisionNumber { get; set; }
		
        [NotifyPropertyChanged, Column("OrderDate", true, false, false)]
        public System.DateTime OrderDate { get; set; }
		
        [NotifyPropertyChanged, Column("DueDate", true, false, false)]
        public System.DateTime DueDate { get; set; }
		
        [NotifyPropertyChanged, Column("ShipDate", true, false, true)]
        public System.Nullable<System.DateTime> ShipDate { get; set; }
		
        [NotifyPropertyChanged, Column("Status", true, false, false)]
        public byte Status { get; set; }
		
        [NotifyPropertyChanged, Column("OnlineOrderFlag", false, false, false)]
        public bool OnlineOrderFlag { get; set; }
		
        [NotifyPropertyChanged, Column("SalesOrderNumber", false, false, false)]
        public string SalesOrderNumber { get; set; }
		
        [NotifyPropertyChanged, Column("PurchaseOrderNumber", false, false, true)]
        public string PurchaseOrderNumber { get; set; }
		
        [NotifyPropertyChanged, Column("AccountNumber", false, false, true)]
        public string AccountNumber { get; set; }
		
        [NotifyPropertyChanged, Column("CustomerID", true, false, false)]
        public int CustomerID { get; set; }
		
        [NotifyPropertyChanged, Column("SalesPersonID", true, false, true)]
        public System.Nullable<int> SalesPersonID { get; set; }
		
        [NotifyPropertyChanged, Column("TerritoryID", true, false, true)]
        public System.Nullable<int> TerritoryID { get; set; }
		
        [NotifyPropertyChanged, Column("BillToAddressID", true, false, false)]
        public int BillToAddressID { get; set; }
		
        [NotifyPropertyChanged, Column("ShipToAddressID", true, false, false)]
        public int ShipToAddressID { get; set; }
		
        [NotifyPropertyChanged, Column("ShipMethodID", true, false, false)]
        public int ShipMethodID { get; set; }
		
        [NotifyPropertyChanged, Column("CreditCardID", true, false, true)]
        public System.Nullable<int> CreditCardID { get; set; }
		
        [NotifyPropertyChanged, Column("CreditCardApprovalCode", false, false, true)]
        public string CreditCardApprovalCode { get; set; }
		
        [NotifyPropertyChanged, Column("CurrencyRateID", true, false, true)]
        public System.Nullable<int> CurrencyRateID { get; set; }
		
        [NotifyPropertyChanged, Column("SubTotal", true, false, false)]
        public object SubTotal { get; set; }
		
        [NotifyPropertyChanged, Column("TaxAmt", true, false, false)]
        public object TaxAmt { get; set; }
		
        [NotifyPropertyChanged, Column("Freight", true, false, false)]
        public object Freight { get; set; }
		
        [NotifyPropertyChanged, Column("TotalDue", false, false, false)]
        public object TotalDue { get; set; }
		
        [NotifyPropertyChanged, Column("Comment", false, false, true)]
        public string Comment { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Address BillToAddressID { get; set; }
		
        [InnerJoinColumn]
        public Address ShipToAddressID { get; set; }
		
        [InnerJoinColumn]
        public ShipMethod ShipMethodID { get; set; }
		
        [InnerJoinColumn]
        public CreditCard CreditCardID { get; set; }
		
        [InnerJoinColumn]
        public CurrencyRate CurrencyRateID { get; set; }
		
        [InnerJoinColumn]
        public Customer CustomerID { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeaderSalesReason SalesOrderIDSalesOrderHeaderSalesReason { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail SalesOrderIDSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesPerson SalesPersonID { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritoryID { get; set; }
	}

	[Table("SalesOrderHeaderSalesReason", "Sales", "AdventureWorks2012")]
	public partial class SalesOrderHeaderSalesReason : Entity<SalesOrderHeaderSalesReason>
	{
		static SalesOrderHeaderSalesReason()
		{
			Join<SalesOrderHeader>(t => t.SalesOrderID, (t, f) => t.SalesOrderID == f.SalesOrderID); // Relation
			Join<SalesReason>(t => t.SalesReasonID, (t, f) => t.SalesReasonID == f.SalesReasonID); // Relation
		}

		
        [NotifyPropertyChanged, Column("SalesOrderID", true, false, false)]
        public int SalesOrderID { get; set; }
		
        [NotifyPropertyChanged, Column("SalesReasonID", true, false, false)]
        public int SalesReasonID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public SalesOrderHeader SalesOrderID { get; set; }
		
        [InnerJoinColumn]
        public SalesReason SalesReasonID { get; set; }
	}

	[Table("SalesPerson", "Sales", "AdventureWorks2012")]
	public partial class SalesPerson : Entity<SalesPerson>
	{
		static SalesPerson()
		{
			Join<Employee>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<SalesOrderHeader>(t => t.SalesPersonIDSalesOrderHeader, (t, f) => t.BusinessEntityID == f.SalesPersonID); // Reverse Relation
			Join<SalesTerritoryHistory>(t => t.BusinessEntityIDSalesTerritoryHistory, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<SalesPersonQuotaHistory>(t => t.BusinessEntityIDSalesPersonQuotaHistory, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Store>(t => t.SalesPersonIDStore, (t, f) => t.BusinessEntityID == f.SalesPersonID); // Reverse Relation
			Join<SalesTerritory>(t => t.TerritoryID, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("TerritoryID", true, false, true)]
        public System.Nullable<int> TerritoryID { get; set; }
		
        [NotifyPropertyChanged, Column("SalesQuota", true, false, true)]
        public object SalesQuota { get; set; }
		
        [NotifyPropertyChanged, Column("Bonus", true, false, false)]
        public object Bonus { get; set; }
		
        [NotifyPropertyChanged, Column("CommissionPct", true, false, false)]
        public object CommissionPct { get; set; }
		
        [NotifyPropertyChanged, Column("SalesYTD", true, false, false)]
        public object SalesYTD { get; set; }
		
        [NotifyPropertyChanged, Column("SalesLastYear", true, false, false)]
        public object SalesLastYear { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Employee BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader SalesPersonIDSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritoryHistory BusinessEntityIDSalesTerritoryHistory { get; set; }
		
        [InnerJoinColumn]
        public SalesPersonQuotaHistory BusinessEntityIDSalesPersonQuotaHistory { get; set; }
		
        [InnerJoinColumn]
        public Store SalesPersonIDStore { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritoryID { get; set; }
	}

	[Table("SalesPersonQuotaHistory", "Sales", "AdventureWorks2012")]
	public partial class SalesPersonQuotaHistory : Entity<SalesPersonQuotaHistory>
	{
		static SalesPersonQuotaHistory()
		{
			Join<SalesPerson>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("QuotaDate", true, false, false)]
        public System.DateTime QuotaDate { get; set; }
		
        [NotifyPropertyChanged, Column("SalesQuota", true, false, false)]
        public object SalesQuota { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public SalesPerson BusinessEntityID { get; set; }
	}

	[Table("SalesReason", "Sales", "AdventureWorks2012")]
	public partial class SalesReason : Entity<SalesReason>
	{
		static SalesReason()
		{
			Join<SalesOrderHeaderSalesReason>(t => t.SalesReasonIDSalesOrderHeaderSalesReason, (t, f) => t.SalesReasonID == f.SalesReasonID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("SalesReasonID", true, true, false)]
        public int SalesReasonID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ReasonType", false, false, false)]
        public string ReasonType { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public SalesOrderHeaderSalesReason SalesReasonIDSalesOrderHeaderSalesReason { get; set; }
	}

	[Table("SalesTaxRate", "Sales", "AdventureWorks2012")]
	public partial class SalesTaxRate : Entity<SalesTaxRate>
	{
		static SalesTaxRate()
		{
			Join<StateProvince>(t => t.StateProvinceID, (t, f) => t.StateProvinceID == f.StateProvinceID); // Relation
		}

		
        [NotifyPropertyChanged, Column("SalesTaxRateID", true, true, false)]
        public int SalesTaxRateID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("StateProvinceID", true, false, false)]
        public int StateProvinceID { get; set; }
		
        [NotifyPropertyChanged, Column("TaxType", true, false, false)]
        public byte TaxType { get; set; }
		
        [NotifyPropertyChanged, Column("TaxRate", false, false, false)]
        public object TaxRate { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public StateProvince StateProvinceID { get; set; }
	}

	[Table("SalesTerritory", "Sales", "AdventureWorks2012")]
	public partial class SalesTerritory : Entity<SalesTerritory>
	{
		static SalesTerritory()
		{
			Join<CountryRegion>(t => t.CountryRegionCode, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Relation
			Join<SalesOrderHeader>(t => t.TerritoryIDSalesOrderHeader, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<Customer>(t => t.TerritoryIDCustomer, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<StateProvince>(t => t.TerritoryIDStateProvince, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<SalesPerson>(t => t.TerritoryIDSalesPerson, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<SalesTerritoryHistory>(t => t.TerritoryIDSalesTerritoryHistory, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("TerritoryID", true, true, false)]
        public int TerritoryID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("CountryRegionCode", true, false, false)]
        public string CountryRegionCode { get; set; }
		
        [NotifyPropertyChanged, Column("Group", false, false, false)]
        public string Group { get; set; }
		
        [NotifyPropertyChanged, Column("SalesYTD", true, false, false)]
        public object SalesYTD { get; set; }
		
        [NotifyPropertyChanged, Column("SalesLastYear", true, false, false)]
        public object SalesLastYear { get; set; }
		
        [NotifyPropertyChanged, Column("CostYTD", true, false, false)]
        public object CostYTD { get; set; }
		
        [NotifyPropertyChanged, Column("CostLastYear", true, false, false)]
        public object CostLastYear { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public CountryRegion CountryRegionCode { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader TerritoryIDSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public Customer TerritoryIDCustomer { get; set; }
		
        [InnerJoinColumn]
        public StateProvince TerritoryIDStateProvince { get; set; }
		
        [InnerJoinColumn]
        public SalesPerson TerritoryIDSalesPerson { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritoryHistory TerritoryIDSalesTerritoryHistory { get; set; }
	}

	[Table("SalesTerritoryHistory", "Sales", "AdventureWorks2012")]
	public partial class SalesTerritoryHistory : Entity<SalesTerritoryHistory>
	{
		static SalesTerritoryHistory()
		{
			Join<SalesPerson>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<SalesTerritory>(t => t.TerritoryID, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("TerritoryID", true, false, false)]
        public int TerritoryID { get; set; }
		
        [NotifyPropertyChanged, Column("StartDate", true, false, false)]
        public System.DateTime StartDate { get; set; }
		
        [NotifyPropertyChanged, Column("EndDate", true, false, true)]
        public System.Nullable<System.DateTime> EndDate { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public SalesPerson BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritoryID { get; set; }
	}

	[Table("ShoppingCartItem", "Sales", "AdventureWorks2012")]
	public partial class ShoppingCartItem : Entity<ShoppingCartItem>
	{
		static ShoppingCartItem()
		{
			Join<Product>(t => t.ProductID, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ShoppingCartItemID", true, true, false)]
        public int ShoppingCartItemID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ShoppingCartID", false, false, false)]
        public string ShoppingCartID { get; set; }
		
        [NotifyPropertyChanged, Column("Quantity", true, false, false)]
        public int Quantity { get; set; }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("DateCreated", false, false, false)]
        public System.DateTime DateCreated { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductID { get; set; }
	}

	[Table("SpecialOffer", "Sales", "AdventureWorks2012")]
	public partial class SpecialOffer : Entity<SpecialOffer>
	{
		static SpecialOffer()
		{
			Join<SpecialOfferProduct>(t => t.SpecialOfferIDSpecialOfferProduct, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("SpecialOfferID", true, true, false)]
        public int SpecialOfferID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Description", false, false, false)]
        public string Description { get; set; }
		
        [NotifyPropertyChanged, Column("DiscountPct", true, false, false)]
        public object DiscountPct { get; set; }
		
        [NotifyPropertyChanged, Column("Type", false, false, false)]
        public string Type { get; set; }
		
        [NotifyPropertyChanged, Column("Category", false, false, false)]
        public string Category { get; set; }
		
        [NotifyPropertyChanged, Column("StartDate", true, false, false)]
        public System.DateTime StartDate { get; set; }
		
        [NotifyPropertyChanged, Column("EndDate", true, false, false)]
        public System.DateTime EndDate { get; set; }
		
        [NotifyPropertyChanged, Column("MinQty", true, false, false)]
        public int MinQty { get; set; }
		
        [NotifyPropertyChanged, Column("MaxQty", true, false, true)]
        public System.Nullable<int> MaxQty { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public SpecialOfferProduct SpecialOfferIDSpecialOfferProduct { get; set; }
	}

	[Table("SpecialOfferProduct", "Sales", "AdventureWorks2012")]
	public partial class SpecialOfferProduct : Entity<SpecialOfferProduct>
	{
		static SpecialOfferProduct()
		{
			Join<Product>(t => t.ProductID, (t, f) => t.ProductID == f.ProductID); // Relation
			Join<SpecialOffer>(t => t.SpecialOfferID, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Relation
			Join<SalesOrderDetail>(t => t.ProductIDSalesOrderDetail, (t, f) => t.SpecialOfferID == f.ProductID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.SpecialOfferIDSalesOrderDetail, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.SpecialOfferIDSalesOrderDetail, (t, f) => t.ProductID == f.SpecialOfferID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.ProductIDSalesOrderDetail, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("SpecialOfferID", true, false, false)]
        public int SpecialOfferID { get; set; }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductID { get; set; }
		
        [InnerJoinColumn]
        public SpecialOffer SpecialOfferID { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail ProductIDSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail SpecialOfferIDSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail SpecialOfferIDSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail ProductIDSalesOrderDetail { get; set; }
	}

	[Table("Store", "Sales", "AdventureWorks2012")]
	public partial class Store : Entity<Store>
	{
		static Store()
		{
			Join<BusinessEntity>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<SalesPerson>(t => t.SalesPersonID, (t, f) => t.SalesPersonID == f.BusinessEntityID); // Relation
			Join<Customer>(t => t.StoreIDCustomer, (t, f) => t.BusinessEntityID == f.StoreID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("SalesPersonID", true, false, true)]
        public System.Nullable<int> SalesPersonID { get; set; }
		
        [NotifyPropertyChanged, Column("Demographics", false, false, true)]
        public object Demographics { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntity BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public SalesPerson SalesPersonID { get; set; }
		
        [InnerJoinColumn]
        public Customer StoreIDCustomer { get; set; }
	}


}

