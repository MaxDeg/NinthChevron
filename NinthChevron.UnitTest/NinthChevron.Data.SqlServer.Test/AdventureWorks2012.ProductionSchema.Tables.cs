using System;
using System.Collections.Generic;
using NinthChevron.Data;
using NinthChevron.Data.Entity;
using NinthChevron.Helpers;
using NinthChevron.ComponentModel.DataAnnotations;

using NinthChevron.Data.SqlServer.Test.AdventureWorks2012;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.HumanResourcesSchema;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.PersonSchema;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.PurchasingSchema;
using NinthChevron.Data.SqlServer.Test.AdventureWorks2012.SalesSchema;

namespace NinthChevron.Data.SqlServer.Test.AdventureWorks2012.ProductionSchema
{
	[Table("BillOfMaterials", "Production", "AdventureWorks2012")]
	public partial class BillOfMaterials : Entity<BillOfMaterials>
	{
		static BillOfMaterials()
		{
			Join<Product>(t => t.ComponentProduct, (t, f) => t.ComponentID == f.ProductID); // Relation
			Join<Product>(t => t.ProductAssemblyProduct, (t, f) => t.ProductAssemblyID == f.ProductID); // Relation
			Join<UnitMeasure>(t => t.UnitMeasureCodeUnitMeasure, (t, f) => t.UnitMeasureCode == f.UnitMeasureCode); // Relation
		}

		
        [NotifyPropertyChanged, Column("BillOfMaterialsID", true, true, false)]
        public int BillOfMaterialsID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ProductAssemblyID", true, false, true)]
        public System.Nullable<int> ProductAssemblyID { get; set; }
		
        [NotifyPropertyChanged, Column("ComponentID", true, false, false)]
        public int ComponentID { get; set; }
		
        [NotifyPropertyChanged, Column("StartDate", true, false, false)]
        public System.DateTime StartDate { get; set; }
		
        [NotifyPropertyChanged, Column("EndDate", true, false, true)]
        public System.Nullable<System.DateTime> EndDate { get; set; }
		
        [NotifyPropertyChanged, Column("UnitMeasureCode", true, false, false)]
        public string UnitMeasureCode { get; set; }
		
        [NotifyPropertyChanged, Column("BOMLevel", true, false, false)]
        public short BOMLevel { get; set; }
		
        [NotifyPropertyChanged, Column("PerAssemblyQty", true, false, false)]
        public decimal PerAssemblyQty { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ComponentProduct { get; set; }
		
        [InnerJoinColumn]
        public Product ProductAssemblyProduct { get; set; }
		
        [InnerJoinColumn]
        public UnitMeasure UnitMeasureCodeUnitMeasure { get; set; }
	}

	[Table("Culture", "Production", "AdventureWorks2012")]
	public partial class Culture : Entity<Culture>
	{
		static Culture()
		{
			Join<ProductModelProductDescriptionCulture>(t => t.CultureProductModelProductDescriptionCulture, (t, f) => t.CultureID == f.CultureID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("CultureID", true, false, false)]
        public string CultureID { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductModelProductDescriptionCulture CultureProductModelProductDescriptionCulture { get; set; }
	}

	[Table("Document", "Production", "AdventureWorks2012")]
	public partial class Document : Entity<Document>
	{
		static Document()
		{
			Join<Employee>(t => t.OwnerEmployee, (t, f) => t.Owner == f.BusinessEntityID); // Relation
			Join<ProductDocument>(t => t.DocumentNodeProductDocument, (t, f) => t.DocumentNode == f.DocumentNode); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("DocumentNode", true, false, false)]
        public object DocumentNode { get; set; }
		
        [NotifyPropertyChanged, Column("DocumentLevel", false, false, true)]
        public System.Nullable<short> DocumentLevel { get; set; }
		
        [NotifyPropertyChanged, Column("Title", false, false, false)]
        public string Title { get; set; }
		
        [NotifyPropertyChanged, Column("Owner", true, false, false)]
        public int Owner { get; set; }
		
        [NotifyPropertyChanged, Column("FolderFlag", false, false, false)]
        public bool FolderFlag { get; set; }
		
        [NotifyPropertyChanged, Column("FileName", false, false, false)]
        public string FileName { get; set; }
		
        [NotifyPropertyChanged, Column("FileExtension", false, false, false)]
        public string FileExtension { get; set; }
		
        [NotifyPropertyChanged, Column("Revision", false, false, false)]
        public string Revision { get; set; }
		
        [NotifyPropertyChanged, Column("ChangeNumber", false, false, false)]
        public int ChangeNumber { get; set; }
		
        [NotifyPropertyChanged, Column("Status", true, false, false)]
        public byte Status { get; set; }
		
        [NotifyPropertyChanged, Column("DocumentSummary", false, false, true)]
        public string DocumentSummary { get; set; }
		
        [NotifyPropertyChanged, Column("Document", false, false, true)]
        public byte[] Document_ { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", true, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Employee OwnerEmployee { get; set; }
		
        [InnerJoinColumn]
        public ProductDocument DocumentNodeProductDocument { get; set; }
	}

	[Table("Illustration", "Production", "AdventureWorks2012")]
	public partial class Illustration : Entity<Illustration>
	{
		static Illustration()
		{
			Join<ProductModelIllustration>(t => t.IllustrationProductModelIllustration, (t, f) => t.IllustrationID == f.IllustrationID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("IllustrationID", true, true, false)]
        public int IllustrationID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Diagram", false, false, true)]
        public object Diagram { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductModelIllustration IllustrationProductModelIllustration { get; set; }
	}

	[Table("Location", "Production", "AdventureWorks2012")]
	public partial class Location : Entity<Location>
	{
		static Location()
		{
			Join<ProductInventory>(t => t.LocationProductInventory, (t, f) => t.LocationID == f.LocationID); // Reverse Relation
			Join<WorkOrderRouting>(t => t.LocationWorkOrderRouting, (t, f) => t.LocationID == f.LocationID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("LocationID", true, true, false)]
        public short LocationID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<short>() : (short)Convert.ChangeType(this.EntityIdentity, typeof(short)); }
	        set { this.EntityIdentity = (short)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("CostRate", true, false, false)]
        public object CostRate { get; set; }
		
        [NotifyPropertyChanged, Column("Availability", true, false, false)]
        public decimal Availability { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductInventory LocationProductInventory { get; set; }
		
        [InnerJoinColumn]
        public WorkOrderRouting LocationWorkOrderRouting { get; set; }
	}

	[Table("Product", "Production", "AdventureWorks2012")]
	public partial class Product : Entity<Product>
	{
		static Product()
		{
			Join<TransactionHistory>(t => t.ProductTransactionHistory, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<WorkOrder>(t => t.ProductWorkOrder, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductProductPhoto>(t => t.ProductProductProductPhoto, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductReview>(t => t.ProductProductReview, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductVendor>(t => t.ProductProductVendor, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<PurchaseOrderDetail>(t => t.ProductPurchaseOrderDetail, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductInventory>(t => t.ProductProductInventory, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductListPriceHistory>(t => t.ProductProductListPriceHistory, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductDocument>(t => t.ProductProductDocument, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductCostHistory>(t => t.ProductProductCostHistory, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<BillOfMaterials>(t => t.ComponentBillOfMaterials, (t, f) => t.ProductID == f.ComponentID); // Reverse Relation
			Join<BillOfMaterials>(t => t.ProductAssemblyBillOfMaterials, (t, f) => t.ProductID == f.ProductAssemblyID); // Reverse Relation
			Join<ShoppingCartItem>(t => t.ProductShoppingCartItem, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<SpecialOfferProduct>(t => t.ProductSpecialOfferProduct, (t, f) => t.ProductID == f.ProductID); // Reverse Relation
			Join<ProductModel>(t => t.ProductModelProductModel, (t, f) => t.ProductModelID == f.ProductModelID); // Relation
			Join<ProductSubcategory>(t => t.ProductSubcategoryProductSubcategory, (t, f) => t.ProductSubcategoryID == f.ProductSubcategoryID); // Relation
			Join<UnitMeasure>(t => t.SizeUnitMeasureCodeUnitMeasure, (t, f) => t.SizeUnitMeasureCode == f.UnitMeasureCode); // Relation
			Join<UnitMeasure>(t => t.WeightUnitMeasureCodeUnitMeasure, (t, f) => t.WeightUnitMeasureCode == f.UnitMeasureCode); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductID", true, true, false)]
        public int ProductID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ProductNumber", false, false, false)]
        public string ProductNumber { get; set; }
		
        [NotifyPropertyChanged, Column("MakeFlag", false, false, false)]
        public bool MakeFlag { get; set; }
		
        [NotifyPropertyChanged, Column("FinishedGoodsFlag", false, false, false)]
        public bool FinishedGoodsFlag { get; set; }
		
        [NotifyPropertyChanged, Column("Color", false, false, true)]
        public string Color { get; set; }
		
        [NotifyPropertyChanged, Column("SafetyStockLevel", true, false, false)]
        public short SafetyStockLevel { get; set; }
		
        [NotifyPropertyChanged, Column("ReorderPoint", true, false, false)]
        public short ReorderPoint { get; set; }
		
        [NotifyPropertyChanged, Column("StandardCost", true, false, false)]
        public object StandardCost { get; set; }
		
        [NotifyPropertyChanged, Column("ListPrice", true, false, false)]
        public object ListPrice { get; set; }
		
        [NotifyPropertyChanged, Column("Size", false, false, true)]
        public string Size { get; set; }
		
        [NotifyPropertyChanged, Column("SizeUnitMeasureCode", true, false, true)]
        public string SizeUnitMeasureCode { get; set; }
		
        [NotifyPropertyChanged, Column("WeightUnitMeasureCode", true, false, true)]
        public string WeightUnitMeasureCode { get; set; }
		
        [NotifyPropertyChanged, Column("Weight", true, false, true)]
        public System.Nullable<decimal> Weight { get; set; }
		
        [NotifyPropertyChanged, Column("DaysToManufacture", true, false, false)]
        public int DaysToManufacture { get; set; }
		
        [NotifyPropertyChanged, Column("ProductLine", true, false, true)]
        public string ProductLine { get; set; }
		
        [NotifyPropertyChanged, Column("Class", true, false, true)]
        public string Class { get; set; }
		
        [NotifyPropertyChanged, Column("Style", true, false, true)]
        public string Style { get; set; }
		
        [NotifyPropertyChanged, Column("ProductSubcategoryID", true, false, true)]
        public System.Nullable<int> ProductSubcategoryID { get; set; }
		
        [NotifyPropertyChanged, Column("ProductModelID", true, false, true)]
        public System.Nullable<int> ProductModelID { get; set; }
		
        [NotifyPropertyChanged, Column("SellStartDate", true, false, false)]
        public System.DateTime SellStartDate { get; set; }
		
        [NotifyPropertyChanged, Column("SellEndDate", true, false, true)]
        public System.Nullable<System.DateTime> SellEndDate { get; set; }
		
        [NotifyPropertyChanged, Column("DiscontinuedDate", false, false, true)]
        public System.Nullable<System.DateTime> DiscontinuedDate { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public TransactionHistory ProductTransactionHistory { get; set; }
		
        [InnerJoinColumn]
        public WorkOrder ProductWorkOrder { get; set; }
		
        [InnerJoinColumn]
        public ProductProductPhoto ProductProductProductPhoto { get; set; }
		
        [InnerJoinColumn]
        public ProductReview ProductProductReview { get; set; }
		
        [InnerJoinColumn]
        public ProductVendor ProductProductVendor { get; set; }
		
        [InnerJoinColumn]
        public PurchaseOrderDetail ProductPurchaseOrderDetail { get; set; }
		
        [InnerJoinColumn]
        public ProductInventory ProductProductInventory { get; set; }
		
        [InnerJoinColumn]
        public ProductListPriceHistory ProductProductListPriceHistory { get; set; }
		
        [InnerJoinColumn]
        public ProductDocument ProductProductDocument { get; set; }
		
        [InnerJoinColumn]
        public ProductCostHistory ProductProductCostHistory { get; set; }
		
        [InnerJoinColumn]
        public BillOfMaterials ComponentBillOfMaterials { get; set; }
		
        [InnerJoinColumn]
        public BillOfMaterials ProductAssemblyBillOfMaterials { get; set; }
		
        [InnerJoinColumn]
        public ShoppingCartItem ProductShoppingCartItem { get; set; }
		
        [InnerJoinColumn]
        public SpecialOfferProduct ProductSpecialOfferProduct { get; set; }
		
        [InnerJoinColumn]
        public ProductModel ProductModelProductModel { get; set; }
		
        [InnerJoinColumn]
        public ProductSubcategory ProductSubcategoryProductSubcategory { get; set; }
		
        [InnerJoinColumn]
        public UnitMeasure SizeUnitMeasureCodeUnitMeasure { get; set; }
		
        [InnerJoinColumn]
        public UnitMeasure WeightUnitMeasureCodeUnitMeasure { get; set; }
	}

	[Table("ProductCategory", "Production", "AdventureWorks2012")]
	public partial class ProductCategory : Entity<ProductCategory>
	{
		static ProductCategory()
		{
			Join<ProductSubcategory>(t => t.ProductCategoryProductSubcategory, (t, f) => t.ProductCategoryID == f.ProductCategoryID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ProductCategoryID", true, true, false)]
        public int ProductCategoryID
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
        public ProductSubcategory ProductCategoryProductSubcategory { get; set; }
	}

	[Table("ProductCostHistory", "Production", "AdventureWorks2012")]
	public partial class ProductCostHistory : Entity<ProductCostHistory>
	{
		static ProductCostHistory()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("StartDate", true, false, false)]
        public System.DateTime StartDate { get; set; }
		
        [NotifyPropertyChanged, Column("EndDate", true, false, true)]
        public System.Nullable<System.DateTime> EndDate { get; set; }
		
        [NotifyPropertyChanged, Column("StandardCost", true, false, false)]
        public object StandardCost { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
	}

	[Table("ProductDescription", "Production", "AdventureWorks2012")]
	public partial class ProductDescription : Entity<ProductDescription>
	{
		static ProductDescription()
		{
			Join<ProductModelProductDescriptionCulture>(t => t.ProductDescriptionProductModelProductDescriptionCulture, (t, f) => t.ProductDescriptionID == f.ProductDescriptionID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ProductDescriptionID", true, true, false)]
        public int ProductDescriptionID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Description", false, false, false)]
        public string Description { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductModelProductDescriptionCulture ProductDescriptionProductModelProductDescriptionCulture { get; set; }
	}

	[Table("ProductDocument", "Production", "AdventureWorks2012")]
	public partial class ProductDocument : Entity<ProductDocument>
	{
		static ProductDocument()
		{
			Join<Document>(t => t.DocumentNodeDocument, (t, f) => t.DocumentNode == f.DocumentNode); // Relation
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("DocumentNode", true, false, false)]
        public object DocumentNode { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Document DocumentNodeDocument { get; set; }
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
	}

	[Table("ProductInventory", "Production", "AdventureWorks2012")]
	public partial class ProductInventory : Entity<ProductInventory>
	{
		static ProductInventory()
		{
			Join<Location>(t => t.LocationLocation, (t, f) => t.LocationID == f.LocationID); // Relation
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("LocationID", true, false, false)]
        public short LocationID { get; set; }
		
        [NotifyPropertyChanged, Column("Shelf", true, false, false)]
        public string Shelf { get; set; }
		
        [NotifyPropertyChanged, Column("Bin", true, false, false)]
        public byte Bin { get; set; }
		
        [NotifyPropertyChanged, Column("Quantity", false, false, false)]
        public short Quantity { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Location LocationLocation { get; set; }
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
	}

	[Table("ProductListPriceHistory", "Production", "AdventureWorks2012")]
	public partial class ProductListPriceHistory : Entity<ProductListPriceHistory>
	{
		static ProductListPriceHistory()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("StartDate", true, false, false)]
        public System.DateTime StartDate { get; set; }
		
        [NotifyPropertyChanged, Column("EndDate", true, false, true)]
        public System.Nullable<System.DateTime> EndDate { get; set; }
		
        [NotifyPropertyChanged, Column("ListPrice", true, false, false)]
        public object ListPrice { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
	}

	[Table("ProductModel", "Production", "AdventureWorks2012")]
	public partial class ProductModel : Entity<ProductModel>
	{
		static ProductModel()
		{
			Join<ProductModelProductDescriptionCulture>(t => t.ProductModelProductModelProductDescriptionCulture, (t, f) => t.ProductModelID == f.ProductModelID); // Reverse Relation
			Join<ProductModelIllustration>(t => t.ProductModelProductModelIllustration, (t, f) => t.ProductModelID == f.ProductModelID); // Reverse Relation
			Join<Product>(t => t.ProductModelProduct, (t, f) => t.ProductModelID == f.ProductModelID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ProductModelID", true, true, false)]
        public int ProductModelID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("CatalogDescription", false, false, true)]
        public object CatalogDescription { get; set; }
		
        [NotifyPropertyChanged, Column("Instructions", false, false, true)]
        public object Instructions { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductModelProductDescriptionCulture ProductModelProductModelProductDescriptionCulture { get; set; }
		
        [InnerJoinColumn]
        public ProductModelIllustration ProductModelProductModelIllustration { get; set; }
		
        [InnerJoinColumn]
        public Product ProductModelProduct { get; set; }
	}

	[Table("ProductModelIllustration", "Production", "AdventureWorks2012")]
	public partial class ProductModelIllustration : Entity<ProductModelIllustration>
	{
		static ProductModelIllustration()
		{
			Join<Illustration>(t => t.IllustrationIllustration, (t, f) => t.IllustrationID == f.IllustrationID); // Relation
			Join<ProductModel>(t => t.ProductModelProductModel, (t, f) => t.ProductModelID == f.ProductModelID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductModelID", true, false, false)]
        public int ProductModelID { get; set; }
		
        [NotifyPropertyChanged, Column("IllustrationID", true, false, false)]
        public int IllustrationID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Illustration IllustrationIllustration { get; set; }
		
        [InnerJoinColumn]
        public ProductModel ProductModelProductModel { get; set; }
	}

	[Table("ProductModelProductDescriptionCulture", "Production", "AdventureWorks2012")]
	public partial class ProductModelProductDescriptionCulture : Entity<ProductModelProductDescriptionCulture>
	{
		static ProductModelProductDescriptionCulture()
		{
			Join<Culture>(t => t.CultureCulture, (t, f) => t.CultureID == f.CultureID); // Relation
			Join<ProductDescription>(t => t.ProductDescriptionProductDescription, (t, f) => t.ProductDescriptionID == f.ProductDescriptionID); // Relation
			Join<ProductModel>(t => t.ProductModelProductModel, (t, f) => t.ProductModelID == f.ProductModelID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductModelID", true, false, false)]
        public int ProductModelID { get; set; }
		
        [NotifyPropertyChanged, Column("ProductDescriptionID", true, false, false)]
        public int ProductDescriptionID { get; set; }
		
        [NotifyPropertyChanged, Column("CultureID", true, false, false)]
        public string CultureID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Culture CultureCulture { get; set; }
		
        [InnerJoinColumn]
        public ProductDescription ProductDescriptionProductDescription { get; set; }
		
        [InnerJoinColumn]
        public ProductModel ProductModelProductModel { get; set; }
	}

	[Table("ProductPhoto", "Production", "AdventureWorks2012")]
	public partial class ProductPhoto : Entity<ProductPhoto>
	{
		static ProductPhoto()
		{
			Join<ProductProductPhoto>(t => t.ProductPhotoProductProductPhoto, (t, f) => t.ProductPhotoID == f.ProductPhotoID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ProductPhotoID", true, true, false)]
        public int ProductPhotoID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ThumbNailPhoto", false, false, true)]
        public byte[] ThumbNailPhoto { get; set; }
		
        [NotifyPropertyChanged, Column("ThumbnailPhotoFileName", false, false, true)]
        public string ThumbnailPhotoFileName { get; set; }
		
        [NotifyPropertyChanged, Column("LargePhoto", false, false, true)]
        public byte[] LargePhoto { get; set; }
		
        [NotifyPropertyChanged, Column("LargePhotoFileName", false, false, true)]
        public string LargePhotoFileName { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductProductPhoto ProductPhotoProductProductPhoto { get; set; }
	}

	[Table("ProductProductPhoto", "Production", "AdventureWorks2012")]
	public partial class ProductProductPhoto : Entity<ProductProductPhoto>
	{
		static ProductProductPhoto()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
			Join<ProductPhoto>(t => t.ProductPhotoProductPhoto, (t, f) => t.ProductPhotoID == f.ProductPhotoID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("ProductPhotoID", true, false, false)]
        public int ProductPhotoID { get; set; }
		
        [NotifyPropertyChanged, Column("Primary", false, false, false)]
        public bool Primary { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
		
        [InnerJoinColumn]
        public ProductPhoto ProductPhotoProductPhoto { get; set; }
	}

	[Table("ProductReview", "Production", "AdventureWorks2012")]
	public partial class ProductReview : Entity<ProductReview>
	{
		static ProductReview()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("ProductReviewID", true, true, false)]
        public int ProductReviewID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("ReviewerName", false, false, false)]
        public string ReviewerName { get; set; }
		
        [NotifyPropertyChanged, Column("ReviewDate", false, false, false)]
        public System.DateTime ReviewDate { get; set; }
		
        [NotifyPropertyChanged, Column("EmailAddress", false, false, false)]
        public string EmailAddress { get; set; }
		
        [NotifyPropertyChanged, Column("Rating", true, false, false)]
        public int Rating { get; set; }
		
        [NotifyPropertyChanged, Column("Comments", false, false, true)]
        public string Comments { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
	}

	[Table("ProductSubcategory", "Production", "AdventureWorks2012")]
	public partial class ProductSubcategory : Entity<ProductSubcategory>
	{
		static ProductSubcategory()
		{
			Join<ProductCategory>(t => t.ProductCategoryProductCategory, (t, f) => t.ProductCategoryID == f.ProductCategoryID); // Relation
			Join<Product>(t => t.ProductSubcategoryProduct, (t, f) => t.ProductSubcategoryID == f.ProductSubcategoryID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ProductSubcategoryID", true, true, false)]
        public int ProductSubcategoryID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ProductCategoryID", true, false, false)]
        public int ProductCategoryID { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("rowguid", false, false, false)]
        public System.Guid Rowguid { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductCategory ProductCategoryProductCategory { get; set; }
		
        [InnerJoinColumn]
        public Product ProductSubcategoryProduct { get; set; }
	}

	[Table("ScrapReason", "Production", "AdventureWorks2012")]
	public partial class ScrapReason : Entity<ScrapReason>
	{
		static ScrapReason()
		{
			Join<WorkOrder>(t => t.ScrapReasonWorkOrder, (t, f) => t.ScrapReasonID == f.ScrapReasonID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ScrapReasonID", true, true, false)]
        public short ScrapReasonID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<short>() : (short)Convert.ChangeType(this.EntityIdentity, typeof(short)); }
	        set { this.EntityIdentity = (short)value; }
        }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public WorkOrder ScrapReasonWorkOrder { get; set; }
	}

	[Table("TransactionHistory", "Production", "AdventureWorks2012")]
	public partial class TransactionHistory : Entity<TransactionHistory>
	{
		static TransactionHistory()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
		}

		
        [NotifyPropertyChanged, Column("TransactionID", true, true, false)]
        public int TransactionID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("ReferenceOrderID", false, false, false)]
        public int ReferenceOrderID { get; set; }
		
        [NotifyPropertyChanged, Column("ReferenceOrderLineID", false, false, false)]
        public int ReferenceOrderLineID { get; set; }
		
        [NotifyPropertyChanged, Column("TransactionDate", false, false, false)]
        public System.DateTime TransactionDate { get; set; }
		
        [NotifyPropertyChanged, Column("TransactionType", true, false, false)]
        public string TransactionType { get; set; }
		
        [NotifyPropertyChanged, Column("Quantity", false, false, false)]
        public int Quantity { get; set; }
		
        [NotifyPropertyChanged, Column("ActualCost", false, false, false)]
        public object ActualCost { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
	}

	[Table("TransactionHistoryArchive", "Production", "AdventureWorks2012")]
	public partial class TransactionHistoryArchive : Entity<TransactionHistoryArchive>
	{
		static TransactionHistoryArchive()
		{
		}

		
        [NotifyPropertyChanged, Column("TransactionID", true, false, false)]
        public int TransactionID { get; set; }
		
        [NotifyPropertyChanged, Column("ProductID", false, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("ReferenceOrderID", false, false, false)]
        public int ReferenceOrderID { get; set; }
		
        [NotifyPropertyChanged, Column("ReferenceOrderLineID", false, false, false)]
        public int ReferenceOrderLineID { get; set; }
		
        [NotifyPropertyChanged, Column("TransactionDate", false, false, false)]
        public System.DateTime TransactionDate { get; set; }
		
        [NotifyPropertyChanged, Column("TransactionType", true, false, false)]
        public string TransactionType { get; set; }
		
        [NotifyPropertyChanged, Column("Quantity", false, false, false)]
        public int Quantity { get; set; }
		
        [NotifyPropertyChanged, Column("ActualCost", false, false, false)]
        public object ActualCost { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
	}

	[Table("UnitMeasure", "Production", "AdventureWorks2012")]
	public partial class UnitMeasure : Entity<UnitMeasure>
	{
		static UnitMeasure()
		{
			Join<ProductVendor>(t => t.UnitMeasureCodeProductVendor, (t, f) => t.UnitMeasureCode == f.UnitMeasureCode); // Reverse Relation
			Join<Product>(t => t.SizeUnitMeasureCodeProduct, (t, f) => t.UnitMeasureCode == f.SizeUnitMeasureCode); // Reverse Relation
			Join<Product>(t => t.WeightUnitMeasureCodeProduct, (t, f) => t.UnitMeasureCode == f.WeightUnitMeasureCode); // Reverse Relation
			Join<BillOfMaterials>(t => t.UnitMeasureCodeBillOfMaterials, (t, f) => t.UnitMeasureCode == f.UnitMeasureCode); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("UnitMeasureCode", true, false, false)]
        public string UnitMeasureCode { get; set; }
		
        [NotifyPropertyChanged, Column("Name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public ProductVendor UnitMeasureCodeProductVendor { get; set; }
		
        [InnerJoinColumn]
        public Product SizeUnitMeasureCodeProduct { get; set; }
		
        [InnerJoinColumn]
        public Product WeightUnitMeasureCodeProduct { get; set; }
		
        [InnerJoinColumn]
        public BillOfMaterials UnitMeasureCodeBillOfMaterials { get; set; }
	}

	[Table("WorkOrder", "Production", "AdventureWorks2012")]
	public partial class WorkOrder : Entity<WorkOrder>
	{
		static WorkOrder()
		{
			Join<Product>(t => t.ProductProduct, (t, f) => t.ProductID == f.ProductID); // Relation
			Join<ScrapReason>(t => t.ScrapReasonScrapReason, (t, f) => t.ScrapReasonID == f.ScrapReasonID); // Relation
			Join<WorkOrderRouting>(t => t.WorkOrderWorkOrderRouting, (t, f) => t.WorkOrderID == f.WorkOrderID); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("WorkOrderID", true, true, false)]
        public int WorkOrderID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("OrderQty", true, false, false)]
        public int OrderQty { get; set; }
		
        [NotifyPropertyChanged, Column("StockedQty", false, false, false)]
        public int StockedQty { get; set; }
		
        [NotifyPropertyChanged, Column("ScrappedQty", true, false, false)]
        public short ScrappedQty { get; set; }
		
        [NotifyPropertyChanged, Column("StartDate", true, false, false)]
        public System.DateTime StartDate { get; set; }
		
        [NotifyPropertyChanged, Column("EndDate", true, false, true)]
        public System.Nullable<System.DateTime> EndDate { get; set; }
		
        [NotifyPropertyChanged, Column("DueDate", false, false, false)]
        public System.DateTime DueDate { get; set; }
		
        [NotifyPropertyChanged, Column("ScrapReasonID", true, false, true)]
        public System.Nullable<short> ScrapReasonID { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Product ProductProduct { get; set; }
		
        [InnerJoinColumn]
        public ScrapReason ScrapReasonScrapReason { get; set; }
		
        [InnerJoinColumn]
        public WorkOrderRouting WorkOrderWorkOrderRouting { get; set; }
	}

	[Table("WorkOrderRouting", "Production", "AdventureWorks2012")]
	public partial class WorkOrderRouting : Entity<WorkOrderRouting>
	{
		static WorkOrderRouting()
		{
			Join<Location>(t => t.LocationLocation, (t, f) => t.LocationID == f.LocationID); // Relation
			Join<WorkOrder>(t => t.WorkOrderWorkOrder, (t, f) => t.WorkOrderID == f.WorkOrderID); // Relation
		}

		
        [NotifyPropertyChanged, Column("WorkOrderID", true, false, false)]
        public int WorkOrderID { get; set; }
		
        [NotifyPropertyChanged, Column("ProductID", true, false, false)]
        public int ProductID { get; set; }
		
        [NotifyPropertyChanged, Column("OperationSequence", true, false, false)]
        public short OperationSequence { get; set; }
		
        [NotifyPropertyChanged, Column("LocationID", true, false, false)]
        public short LocationID { get; set; }
		
        [NotifyPropertyChanged, Column("ScheduledStartDate", true, false, false)]
        public System.DateTime ScheduledStartDate { get; set; }
		
        [NotifyPropertyChanged, Column("ScheduledEndDate", true, false, false)]
        public System.DateTime ScheduledEndDate { get; set; }
		
        [NotifyPropertyChanged, Column("ActualStartDate", true, false, true)]
        public System.Nullable<System.DateTime> ActualStartDate { get; set; }
		
        [NotifyPropertyChanged, Column("ActualEndDate", true, false, true)]
        public System.Nullable<System.DateTime> ActualEndDate { get; set; }
		
        [NotifyPropertyChanged, Column("ActualResourceHrs", true, false, true)]
        public System.Nullable<decimal> ActualResourceHrs { get; set; }
		
        [NotifyPropertyChanged, Column("PlannedCost", true, false, false)]
        public object PlannedCost { get; set; }
		
        [NotifyPropertyChanged, Column("ActualCost", true, false, true)]
        public object ActualCost { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
		
        [InnerJoinColumn]
        public Location LocationLocation { get; set; }
		
        [InnerJoinColumn]
        public WorkOrder WorkOrderWorkOrder { get; set; }
	}


}

