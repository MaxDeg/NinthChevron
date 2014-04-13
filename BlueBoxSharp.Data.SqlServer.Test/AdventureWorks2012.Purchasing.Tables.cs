using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012.Purchasing
{
	[Table("ProductVendor", "Purchasing", "AdventureWorks2012")]
	public partial class ProductVendor : Entity<ProductVendor>
	{
		static ProductVendor()
		{
			Join<Product>(t => t.ProductID, (t, f) => t.ProductID == f.ProductID); // Relation
			Join<UnitMeasure>(t => t.UnitMeasureCode, (t, f) => t.UnitMeasureCode == f.UnitMeasureCode); // Relation
			Join<Vendor>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("AverageLeadTime", true, false, false)]
        public int AverageLeadTime { get; set; }
		
        [NotifyPropertyChanged, Column("StandardPrice", true, false, false)]
        public object StandardPrice { get; set; }
		
        [NotifyPropertyChanged, Column("LastReceiptCost", true, false, true)]
        public object LastReceiptCost { get; set; }
		
        [NotifyPropertyChanged, Column("LastReceiptDate", false, false, true)]
        public System.Nullable<System.DateTime> LastReceiptDate { get; set; }
		
        [NotifyPropertyChanged, Column("MinOrderQty", true, false, false)]
        public int MinOrderQty { get; set; }
		
        [NotifyPropertyChanged, Column("MaxOrderQty", true, false, false)]
        public int MaxOrderQty { get; set; }
		
        [NotifyPropertyChanged, Column("OnOrderQty", true, false, true)]
        public System.Nullable<int> OnOrderQty { get; set; }
		
        [NotifyPropertyChanged, Column("UnitMeasureCode", true, false, false)]
        public string UnitMeasureCode { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductID { get; set; }
		
        [InnerJoinColumn]
        public UnitMeasure UnitMeasureCode { get; set; }
		
        [InnerJoinColumn]
        public Vendor BusinessEntityID { get; set; }
	}

	[Table("PurchaseOrderDetail", "Purchasing", "AdventureWorks2012")]
	public partial class PurchaseOrderDetail : Entity<PurchaseOrderDetail>
	{
		static PurchaseOrderDetail()
		{
			Join<Product>(t => t.ProductID, (t, f) => t.ProductID == f.ProductID); // Relation
			Join<PurchaseOrderHeader>(t => t.PurchaseOrderID, (t, f) => t.PurchaseOrderID == f.PurchaseOrderID); // Relation
		}

		
        [NotifyPropertyChanged, Column("PurchaseOrderID", true, false, false)]
        public int PurchaseOrderID { get; set; }
		
        [NotifyPropertyChanged, Column("PurchaseOrderDetailID", true, true, false)]
        public int PurchaseOrderDetailID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("DueDate", false, false, false)]
        public System.DateTime DueDate { get; set; }
		
        [NotifyPropertyChanged, Column("OrderQty", true, false, false)]
        public short OrderQty { get; set; }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("UnitPrice", true, false, false)]
        public object UnitPrice { get; set; }
		
        [NotifyPropertyChanged, Column("LineTotal", false, false, false)]
        public object LineTotal { get; set; }
		
        [NotifyPropertyChanged, Column("ReceivedQty", true, false, false)]
        public decimal ReceivedQty { get; set; }
		
        [NotifyPropertyChanged, Column("RejectedQty", true, false, false)]
        public decimal RejectedQty { get; set; }
		
        [NotifyPropertyChanged, Column("StockedQty", false, false, false)]
        public decimal StockedQty { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductID { get; set; }
		
        [InnerJoinColumn]
        public PurchaseOrderHeader PurchaseOrderID { get; set; }
	}

	[Table("PurchaseOrderHeader", "Purchasing", "AdventureWorks2012")]
	public partial class PurchaseOrderHeader : Entity<PurchaseOrderHeader>
	{
		static PurchaseOrderHeader()
		{
			Join<Employee>(t => t.EmployeeID, (t, f) => t.EmployeeID == f.BusinessEntityID); // Relation
			Join<PurchaseOrderDetail>(t => t.PurchaseOrderIDPurchaseOrderDetail, (t, f) => t.PurchaseOrderID == f.PurchaseOrderID); // Reverse Relation
			Join<ShipMethod>(t => t.ShipMethodID, (t, f) => t.ShipMethodID == f.ShipMethodID); // Relation
			Join<Vendor>(t => t.VendorID, (t, f) => t.VendorID == f.BusinessEntityID); // Relation
		}

		
        [NotifyPropertyChanged, Column("PurchaseOrderID", true, true, false)]
        public int PurchaseOrderID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("RevisionNumber", false, false, false)]
        public byte RevisionNumber { get; set; }
		
        [NotifyPropertyChanged, Column("Status", true, false, false)]
        public byte Status { get; set; }
		
        [NotifyPropertyChanged, Column("EmployeeID", true, false, false)]
        public int EmployeeID { get; set; }
		
        [NotifyPropertyChanged, Column("VendorID", true, false, false)]
        public int VendorID { get; set; }
		
        [NotifyPropertyChanged, Column("ShipMethodID", true, false, false)]
        public int ShipMethodID { get; set; }
		
        [NotifyPropertyChanged, Column("OrderDate", true, false, false)]
        public System.DateTime OrderDate { get; set; }
		
        [NotifyPropertyChanged, Column("ShipDate", true, false, true)]
        public System.Nullable<System.DateTime> ShipDate { get; set; }
		
        [NotifyPropertyChanged, Column("SubTotal", true, false, false)]
        public object SubTotal { get; set; }
		
        [NotifyPropertyChanged, Column("TaxAmt", true, false, false)]
        public object TaxAmt { get; set; }
		
        [NotifyPropertyChanged, Column("Freight", true, false, false)]
        public object Freight { get; set; }
		
        [NotifyPropertyChanged, Column("TotalDue", false, false, false)]
        public object TotalDue { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Employee EmployeeID { get; set; }
		
        [InnerJoinColumn]
        public PurchaseOrderDetail PurchaseOrderIDPurchaseOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public ShipMethod ShipMethodID { get; set; }
		
        [InnerJoinColumn]
        public Vendor VendorID { get; set; }
	}

	[Table("ShipMethod", "Purchasing", "AdventureWorks2012")]
	public partial class ShipMethod : Entity<ShipMethod>
	{
		static ShipMethod()
		{
			Join<PurchaseOrderHeader>(t => t.ShipMethodIDPurchaseOrderHeader, (t, f) => t.ShipMethodID == f.ShipMethodID); // Reverse Relation
			Join<SalesOrderHeader>(t => t.ShipMethodIDSalesOrderHeader, (t, f) => t.ShipMethodID == f.ShipMethodID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ShipMethodID", true, true, false)]
        public int ShipMethodID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ShipBase", true, false, false)]
        public object ShipBase { get; set; }
		
        [NotifyPropertyChanged, Column("ShipRate", true, false, false)]
        public object ShipRate { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public PurchaseOrderHeader ShipMethodIDPurchaseOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public SalesOrderHeader ShipMethodIDSalesOrderHeader { get; set; }
	}

	[Table("Vendor", "Purchasing", "AdventureWorks2012")]
	public partial class Vendor : Entity<Vendor>
	{
		static Vendor()
		{
			Join<BusinessEntity>(t => t.BusinessEntityID, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Relation
			Join<PurchaseOrderHeader>(t => t.VendorIDPurchaseOrderHeader, (t, f) => t.BusinessEntityID == f.VendorID); // Reverse Relation
			Join<ProductVendor>(t => t.BusinessEntityIDProductVendor, (t, f) => t.BusinessEntityID == f.BusinessEntityID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("BusinessEntityID", true, false, false)]
        public int BusinessEntityID { get; set; }
		
        [NotifyPropertyChanged, Column("AccountNumber", false, false, false)]
        public string AccountNumber { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("CreditRating", true, false, false)]
        public byte CreditRating { get; set; }
		
        [NotifyPropertyChanged, Column("PreferredVendorStatus", false, false, false)]
        public bool PreferredVendorStatus { get; set; }
		
        [NotifyPropertyChanged, Column("ActiveFlag", false, false, false)]
        public bool ActiveFlag { get; set; }
		
        [NotifyPropertyChanged, Column("PurchasingWebServiceURL", false, false, true)]
        public string PurchasingWebServiceURL { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public BusinessEntity BusinessEntityID { get; set; }
		
        [InnerJoinColumn]
        public PurchaseOrderHeader VendorIDPurchaseOrderHeader { get; set; }
		
        [InnerJoinColumn]
        public ProductVendor BusinessEntityIDProductVendor { get; set; }
	}


}

