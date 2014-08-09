using System;
using System.Collections.Generic;
using NinthChevron.Data;
using NinthChevron.Data.Entity;
using NinthChevron.Helpers;
using NinthChevron.ComponentModel.DataAnnotations;

using NinthChevron.Data.SqlServer.Test.AdventureWorks2012;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.HumanResourcesSchema;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.PersonSchema;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.ProductionSchema;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.PurchasingSchema;

namespace NinthChevron.Data.SqlServer.Test.AdventureWorks2012.SalesSchema
{
	[Table("CountryRegionCurrency", "Sales", "AdventureWorks2012")]
	public partial class CountryRegionCurrency : Entity<CountryRegionCurrency>
	{
		static CountryRegionCurrency()
		{
			Join<CountryRegion>(t => t.CountryRegionCodeCountryRegion, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Relation
			Join<Currency>(t => t.CurrencyCodeCurrency, (t, f) => t.CurrencyCode == f.CurrencyCode); // Relation
		}

		
        [NotifyPropertyChanged, Column("CountryRegionCode", true, false, false)]
        public string CountryRegionCode { get; set; }
		
        [NotifyPropertyChanged, Column("CurrencyCode", true, false, false)]
        public string CurrencyCode { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public CountryRegion CountryRegionCodeCountryRegion { get; set; }
		
        [InnerJoinColumn]
        public Currency CurrencyCodeCurrency { get; set; }
	}

	[Table("CreditCard", "Sales", "AdventureWorks2012")]
	public partial class CreditCard : Entity<CreditCard>
	{
		static CreditCard()
		{
			Join<PersonCreditCard>(t => t.CreditCardPersonCreditCard, (t, f) => t.CreditCardID == f.CreditCardID); // Reverse Relation
			Join<SalesOrderHeader>(t => t.CreditCardSalesOrderHeader, (t, f) => t.CreditCardID == f.CreditCardID); // Reverse Relation
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
        public PersonCreditCard CreditCardPersonCreditCard { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader CreditCardSalesOrderHeader { get; set; }
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
			Join<Currency>(t => t.FromCurrencyCodeCurrency, (t, f) => t.FromCurrencyCode == f.CurrencyCode); // Relation
			Join<Currency>(t => t.ToCurrencyCodeCurrency, (t, f) => t.ToCurrencyCode == f.CurrencyCode); // Relation
			Join<SalesOrderHeader>(t => t.CurrencyRateSalesOrderHeader, (t, f) => t.CurrencyRateID == f.CurrencyRateID); // Reverse Relation
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
        public Currency FromCurrencyCodeCurrency { get; set; }
		
        [InnerJoinColumn]
        public Currency ToCurrencyCodeCurrency { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader CurrencyRateSalesOrderHeader { get; set; }
	}

	[Table("Customer", "Sales", "AdventureWorks2012")]
	public partial class Customer : Entity<Customer>
	{
		static Customer()
		{
			Join<Person>(t => t.PersonPerson, (t, f) => t.PersonID == f.BusinessEntityID); // Relation
			Join<SalesOrderHeader>(t => t.CustomerSalesOrderHeader, (t, f) => t.CustomerID == f.CustomerID); // Reverse Relation
			Join<SalesTerritory>(t => t.TerritorySalesTerritory, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
			Join<Store>(t => t.StoreStore, (t, f) => t.StoreID == f.BusinessEntityID); // Relation
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
        public Person PersonPerson { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader CustomerSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritorySalesTerritory { get; set; }
		
        [InnerJoinColumn]
        public Store StoreStore { get; set; }
	}

	[Table("PersonCreditCard", "Sales", "AdventureWorks2012")]
	public partial class PersonCreditCard : Entity<PersonCreditCard>
	{
		static PersonCreditCard()
		{
			Join<Person>(t => t.BusinessEntityPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<CreditCard>(t => t.CreditCardCreditCard, (t, f) => t.CreditCardID == f.CreditCardID); // Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("CreditCardID", true, false, false)]
        public int CreditCardID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Person BusinessEntityPerson { get; set; }
		
        [InnerJoinColumn]
        public CreditCard CreditCardCreditCard { get; set; }
	}

	[Table("SalesOrderDetail", "Sales", "AdventureWorks2012")]
	public partial class SalesOrderDetail : Entity<SalesOrderDetail>
	{
		static SalesOrderDetail()
		{
			Join<SalesOrderHeader>(t => t.SalesOrderSalesOrderHeader, (t, f) => t.SalesOrderID == f.SalesOrderID); // Relation
			Join<SpecialOfferProduct>(t => t.ProductSpecialOfferProduct, (t, f) => t.ProductID == f.SpecialOfferID); // Relation
			Join<SpecialOfferProduct>(t => t.SpecialOfferSpecialOfferProduct, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Relation
			Join<SpecialOfferProduct>(t => t.SpecialOfferSpecialOfferProduct, (t, f) => t.SpecialOfferID == f.ProductID); // Relation
			Join<SpecialOfferProduct>(t => t.ProductSpecialOfferProduct, (t, f) => t.ProductID == f.ProductID); // Relation
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
        public SalesOrderHeader SalesOrderSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct ProductSpecialOfferProduct { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct SpecialOfferSpecialOfferProduct { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct SpecialOfferSpecialOfferProduct { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct ProductSpecialOfferProduct { get; set; }
	}

	[Table("SalesOrderHeader", "Sales", "AdventureWorks2012")]
	public partial class SalesOrderHeader : Entity<SalesOrderHeader>
	{
		static SalesOrderHeader()
		{
			Join<Address>(t => t.BillToAddressAddress, (t, f) => t.BillToAddressID == f.AddressID); // Relation
			Join<Address>(t => t.ShipToAddressAddress, (t, f) => t.ShipToAddressID == f.AddressID); // Relation
			Join<ShipMethod>(t => t.ShipMethodShipMethod, (t, f) => t.ShipMethodID == f.ShipMethodID); // Relation
			Join<CreditCard>(t => t.CreditCardCreditCard, (t, f) => t.CreditCardID == f.CreditCardID); // Relation
			Join<CurrencyRate>(t => t.CurrencyRateCurrencyRate, (t, f) => t.CurrencyRateID == f.CurrencyRateID); // Relation
			Join<Customer>(t => t.CustomerCustomer, (t, f) => t.CustomerID == f.CustomerID); // Relation
			Join<SalesOrderHeaderSalesReason>(t => t.SalesOrderSalesOrderHeaderSalesReason, (t, f) => t.SalesOrderID == f.SalesOrderID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.SalesOrderSalesOrderDetail, (t, f) => t.SalesOrderID == f.SalesOrderID); // Reverse Relation
			Join<SalesPerson>(t => t.SalesPersonSalesPerson, (t, f) => t.SalesPersonID == f.BusinessEntityID); // Relation
			Join<SalesTerritory>(t => t.TerritorySalesTerritory, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
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
        public Address BillToAddressAddress { get; set; }
		
        [InnerJoinColumn]
        public Address ShipToAddressAddress { get; set; }
		
        [InnerJoinColumn]
        public ShipMethod ShipMethodShipMethod { get; set; }
		
        [InnerJoinColumn]
        public CreditCard CreditCardCreditCard { get; set; }
		
        [InnerJoinColumn]
        public CurrencyRate CurrencyRateCurrencyRate { get; set; }
		
        [InnerJoinColumn]
        public Customer CustomerCustomer { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeaderSalesReason SalesOrderSalesOrderHeaderSalesReason { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail SalesOrderSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesPerson SalesPersonSalesPerson { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritorySalesTerritory { get; set; }
	}

	[Table("SalesOrderHeaderSalesReason", "Sales", "AdventureWorks2012")]
	public partial class SalesOrderHeaderSalesReason : Entity<SalesOrderHeaderSalesReason>
	{
		static SalesOrderHeaderSalesReason()
		{
			Join<SalesOrderHeader>(t => t.SalesOrderSalesOrderHeader, (t, f) => t.SalesOrderID == f.SalesOrderID); // Relation
			Join<SalesReason>(t => t.SalesReasonSalesReason, (t, f) => t.SalesReasonID == f.SalesReasonID); // Relation
		}

		
        [NotifyPropertyChanged, Column("SalesOrderID", true, false, false)]
        public int SalesOrderID { get; set; }
		
        [NotifyPropertyChanged, Column("SalesReasonID", true, false, false)]
        public int SalesReasonID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public SalesOrderHeader SalesOrderSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesReason SalesReasonSalesReason { get; set; }
	}

	[Table("SalesPerson", "Sales", "AdventureWorks2012")]
	public partial class SalesPerson : Entity<SalesPerson>
	{
		static SalesPerson()
		{
			Join<Employee>(t => t.BusinessEntityEmployee, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<SalesOrderHeader>(t => t.SalesPersonSalesOrderHeader, (t, f) => t.BusinessEntityID == f.SalesPersonID); // Reverse Relation
			Join<SalesTerritoryHistory>(t => t.BusinessEntitySalesTerritoryHistory, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<SalesPersonQuotaHistory>(t => t.BusinessEntitySalesPersonQuotaHistory, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
			Join<Store>(t => t.SalesPersonStore, (t, f) => t.BusinessEntityID == f.SalesPersonID); // Reverse Relation
			Join<SalesTerritory>(t => t.TerritorySalesTerritory, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
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
        public Employee BusinessEntityEmployee { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader SalesPersonSalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritoryHistory BusinessEntitySalesTerritoryHistory { get; set; }
		
        [InnerJoinColumn]
        public SalesPersonQuotaHistory BusinessEntitySalesPersonQuotaHistory { get; set; }
		
        [InnerJoinColumn]
        public Store SalesPersonStore { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritorySalesTerritory { get; set; }
	}

	[Table("SalesPersonQuotaHistory", "Sales", "AdventureWorks2012")]
	public partial class SalesPersonQuotaHistory : Entity<SalesPersonQuotaHistory>
	{
		static SalesPersonQuotaHistory()
		{
			Join<SalesPerson>(t => t.BusinessEntitySalesPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
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
        public SalesPerson BusinessEntitySalesPerson { get; set; }
	}

	[Table("SalesReason", "Sales", "AdventureWorks2012")]
	public partial class SalesReason : Entity<SalesReason>
	{
		static SalesReason()
		{
			Join<SalesOrderHeaderSalesReason>(t => t.SalesReasonSalesOrderHeaderSalesReason, (t, f) => t.SalesReasonID == f.SalesReasonID); // Reverse Relation
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
        public SalesOrderHeaderSalesReason SalesReasonSalesOrderHeaderSalesReason { get; set; }
	}

	[Table("SalesTaxRate", "Sales", "AdventureWorks2012")]
	public partial class SalesTaxRate : Entity<SalesTaxRate>
	{
		static SalesTaxRate()
		{
			Join<StateProvince>(t => t.StateProvinceStateProvince, (t, f) => t.StateProvinceID == f.StateProvinceID); // Relation
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
        public StateProvince StateProvinceStateProvince { get; set; }
	}

	[Table("SalesTerritory", "Sales", "AdventureWorks2012")]
	public partial class SalesTerritory : Entity<SalesTerritory>
	{
		static SalesTerritory()
		{
			Join<CountryRegion>(t => t.CountryRegionCodeCountryRegion, (t, f) => t.CountryRegionCode == f.CountryRegionCode); // Relation
			Join<SalesOrderHeader>(t => t.TerritorySalesOrderHeader, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<Customer>(t => t.TerritoryCustomer, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<StateProvince>(t => t.TerritoryStateProvince, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<SalesPerson>(t => t.TerritorySalesPerson, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
			Join<SalesTerritoryHistory>(t => t.TerritorySalesTerritoryHistory, (t, f) => t.TerritoryID == f.TerritoryID); // Reverse Relation
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
        public CountryRegion CountryRegionCodeCountryRegion { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader TerritorySalesOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public Customer TerritoryCustomer { get; set; }
		
        [InnerJoinColumn]
        public StateProvince TerritoryStateProvince { get; set; }
		
        [InnerJoinColumn]
        public SalesPerson TerritorySalesPerson { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritoryHistory TerritorySalesTerritoryHistory { get; set; }
	}

	[Table("SalesTerritoryHistory", "Sales", "AdventureWorks2012")]
	public partial class SalesTerritoryHistory : Entity<SalesTerritoryHistory>
	{
		static SalesTerritoryHistory()
		{
			Join<SalesPerson>(t => t.BusinessEntitySalesPerson, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<SalesTerritory>(t => t.TerritorySalesTerritory, (t, f) => t.TerritoryID == f.TerritoryID); // Relation
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
        public SalesPerson BusinessEntitySalesPerson { get; set; }
		
        [InnerJoinColumn]
        public SalesTerritory TerritorySalesTerritory { get; set; }
	}

	[Table("ShoppingCartItem", "Sales", "AdventureWorks2012")]
	public partial class ShoppingCartItem : Entity<ShoppingCartItem>
	{
		static ShoppingCartItem()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
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
        public Product ProductProduct { get; set; }
	}

	[Table("SpecialOffer", "Sales", "AdventureWorks2012")]
	public partial class SpecialOffer : Entity<SpecialOffer>
	{
		static SpecialOffer()
		{
			Join<SpecialOfferProduct>(t => t.SpecialOfferSpecialOfferProduct, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Reverse Relation
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
        public SpecialOfferProduct SpecialOfferSpecialOfferProduct { get; set; }
	}

	[Table("SpecialOfferProduct", "Sales", "AdventureWorks2012")]
	public partial class SpecialOfferProduct : Entity<SpecialOfferProduct>
	{
		static SpecialOfferProduct()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
			Join<SpecialOffer>(t => t.SpecialOfferSpecialOffer, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Relation
			Join<SalesOrderDetail>(t => t.ProductSalesOrderDetail, (t, f) => t.SpecialOfferID == f.ProductID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.SpecialOfferSalesOrderDetail, (t, f) => t.SpecialOfferID == f.SpecialOfferID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.SpecialOfferSalesOrderDetail, (t, f) => t.ProductID == f.SpecialOfferID); // Reverse Relation
			Join<SalesOrderDetail>(t => t.ProductSalesOrderDetail, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
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
        public Product ProductProduct { get; set; }
		
        [InnerJoinColumn]
        public SpecialOffer SpecialOfferSpecialOffer { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail ProductSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail SpecialOfferSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail SpecialOfferSalesOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderDetail ProductSalesOrderDetail { get; set; }
	}

	[Table("Store", "Sales", "AdventureWorks2012")]
	public partial class Store : Entity<Store>
	{
		static Store()
		{
			Join<BusinessEntity>(t => t.BusinessEntityBusinessEntity, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<SalesPerson>(t => t.SalesPersonSalesPerson, (t, f) => t.SalesPersonID == f.BusinessEntityID); // Relation
			Join<Customer>(t => t.StoreCustomer, (t, f) => t.BusinessEntityID == f.StoreID); // Reverse Relation
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
        public BusinessEntity BusinessEntityBusinessEntity { get; set; }
		
        [InnerJoinColumn]
        public SalesPerson SalesPersonSalesPerson { get; set; }
		
        [InnerJoinColumn]
        public Customer StoreCustomer { get; set; }
	}


}

