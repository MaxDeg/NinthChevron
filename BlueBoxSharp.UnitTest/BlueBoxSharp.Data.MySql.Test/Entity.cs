using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace BlueBoxSharp.Data.MySql.Test.Sakila
{
	[Table("actor", "", "sakila")]
	public partial class Actor : Entity<Actor>
	{
		static Actor()
		{
			Join<FilmActor>(t => t.ActorFilmActor, (t, f) => t.ActorId == f.ActorId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("actor_id", true, true, false)]
        public ushort ActorId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this.EntityIdentity, typeof(ushort)); }
	        set { this.EntityIdentity = (ushort)value; }
        }
		
        [NotifyPropertyChanged, Column("first_name", false, false, false)]
        public string FirstName { get; set; }
		
        [NotifyPropertyChanged, Column("last_name", false, false, false)]
        public string LastName { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public FilmActor ActorFilmActor { get; set; }
	}

	[Table("address", "", "sakila")]
	public partial class Address : Entity<Address>
	{
		static Address()
		{
			Join<City>(t => t.City, (t, f) => t.CityId == f.CityId); // Relation
			Join<Customer>(t => t.AddressCustomer, (t, f) => t.AddressId == f.AddressId); // Reverse Relation
			Join<Staff>(t => t.AddressStaff, (t, f) => t.AddressId == f.AddressId); // Reverse Relation
			Join<Store>(t => t.AddressStore, (t, f) => t.AddressId == f.AddressId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("address_id", true, true, false)]
        public ushort AddressId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this.EntityIdentity, typeof(ushort)); }
	        set { this.EntityIdentity = (ushort)value; }
        }
		
        [NotifyPropertyChanged, Column("_address", false, false, true)]
        public string _Address { get; set; }
		
        [NotifyPropertyChanged, Column("address2", false, false, true)]
        public string Address2 { get; set; }
		
        [NotifyPropertyChanged, Column("district", false, false, false)]
        public string District { get; set; }
		
        [NotifyPropertyChanged, Column("city_id", false, false, false)]
        public ushort CityId { get; set; }
		
        [NotifyPropertyChanged, Column("postal_code", false, false, true)]
        public string PostalCode { get; set; }
		
        [NotifyPropertyChanged, Column("phone", false, false, false)]
        public string Phone { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public City City { get; set; }
		
        [InnerJoinColumn]
        public Customer AddressCustomer { get; set; }
		
        [InnerJoinColumn]
        public Staff AddressStaff { get; set; }
		
        [InnerJoinColumn]
        public Store AddressStore { get; set; }
	}

	[Table("category", "", "sakila")]
	public partial class Category : Entity<Category>
	{
		static Category()
		{
			Join<FilmCategory>(t => t.CategoryFilmCategory, (t, f) => t.CategoryId == f.CategoryId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("category_id", true, true, false)]
        public byte CategoryId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this.EntityIdentity, typeof(byte)); }
	        set { this.EntityIdentity = (byte)value; }
        }
		
        [NotifyPropertyChanged, Column("name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public FilmCategory CategoryFilmCategory { get; set; }
	}

	[Table("city", "", "sakila")]
	public partial class City : Entity<City>
	{
		static City()
		{
			Join<Address>(t => t.CityAddress, (t, f) => t.CityId == f.CityId); // Reverse Relation
			Join<Country>(t => t.Country, (t, f) => t.CountryId == f.CountryId); // Relation
		}

		
        [NotifyPropertyChanged, Column("city_id", true, true, false)]
        public ushort CityId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this.EntityIdentity, typeof(ushort)); }
	        set { this.EntityIdentity = (ushort)value; }
        }
		
        [NotifyPropertyChanged, Column("name", false, false, true)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("country_id", false, false, false)]
        public ushort CountryId { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Address CityAddress { get; set; }
		
        [InnerJoinColumn]
        public Country Country { get; set; }
	}

	[Table("country", "", "sakila")]
	public partial class Country : Entity<Country>
	{
		static Country()
		{
			Join<City>(t => t.CountryCity, (t, f) => t.CountryId == f.CountryId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("country_id", true, true, false)]
        public ushort CountryId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this.EntityIdentity, typeof(ushort)); }
	        set { this.EntityIdentity = (ushort)value; }
        }
		
        [NotifyPropertyChanged, Column("name", false, false, true)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public City CountryCity { get; set; }
	}

	[Table("customer", "", "sakila")]
	public partial class Customer : Entity<Customer>
	{
		static Customer()
		{
			Join<Address>(t => t.Address, (t, f) => t.AddressId == f.AddressId); // Relation
			Join<Store>(t => t.Store, (t, f) => t.StoreId == f.StoreId); // Relation
			Join<Payment>(t => t.CustomerPayment, (t, f) => t.CustomerId == f.CustomerId); // Reverse Relation
			Join<Rental>(t => t.CustomerRental, (t, f) => t.CustomerId == f.CustomerId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("customer_id", true, true, false)]
        public ushort CustomerId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this.EntityIdentity, typeof(ushort)); }
	        set { this.EntityIdentity = (ushort)value; }
        }
		
        [NotifyPropertyChanged, Column("store_id", false, false, false)]
        public byte StoreId { get; set; }
		
        [NotifyPropertyChanged, Column("first_name", false, false, false)]
        public string FirstName { get; set; }
		
        [NotifyPropertyChanged, Column("last_name", false, false, false)]
        public string LastName { get; set; }
		
        [NotifyPropertyChanged, Column("email", false, false, true)]
        public string Email { get; set; }
		
        [NotifyPropertyChanged, Column("address_id", false, false, false)]
        public ushort AddressId { get; set; }
		
        [NotifyPropertyChanged, Column("active", false, false, false)]
        public sbyte Active { get; set; }
		
        [NotifyPropertyChanged, Column("create_date", false, false, false)]
        public System.DateTime CreateDate { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Address Address { get; set; }
		
        [InnerJoinColumn]
        public Store Store { get; set; }
		
        [InnerJoinColumn]
        public Payment CustomerPayment { get; set; }
		
        [InnerJoinColumn]
        public Rental CustomerRental { get; set; }
	}

	[Table("film", "", "sakila")]
	public partial class Film : Entity<Film>
	{
		static Film()
		{
			Join<Language>(t => t.OriginalLanguage, (t, f) => t.OriginalLanguageId == f.LanguageId); // Relation
			Join<Language>(t => t.Language, (t, f) => t.LanguageId == f.LanguageId); // Relation
			Join<FilmActor>(t => t.FilmFilmActor, (t, f) => t.FilmId == f.FilmId); // Reverse Relation
			Join<FilmCategory>(t => t.FilmFilmCategory, (t, f) => t.FilmId == f.FilmId); // Reverse Relation
			Join<Inventory>(t => t.FilmInventory, (t, f) => t.FilmId == f.FilmId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("film_id", true, true, false)]
        public ushort FilmId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this.EntityIdentity, typeof(ushort)); }
	        set { this.EntityIdentity = (ushort)value; }
        }
		
        [NotifyPropertyChanged, Column("title", false, false, false)]
        public string Title { get; set; }
		
        [NotifyPropertyChanged, Column("description", false, false, true)]
        public string Description { get; set; }
		
        [NotifyPropertyChanged, Column("release_year", false, false, true)]
        public System.Nullable<short> ReleaseYear { get; set; }
		
        [NotifyPropertyChanged, Column("language_id", false, false, false)]
        public byte LanguageId { get; set; }
		
        [NotifyPropertyChanged, Column("original_language_id", false, false, true)]
        public System.Nullable<byte> OriginalLanguageId { get; set; }
		
        [NotifyPropertyChanged, Column("rental_duration", false, false, false)]
        public byte RentalDuration { get; set; }
		
        [NotifyPropertyChanged, Column("rental_rate", false, false, false)]
        public decimal RentalRate { get; set; }
		
        [NotifyPropertyChanged, Column("length", false, false, true)]
        public System.Nullable<ushort> Length { get; set; }
		
        [NotifyPropertyChanged, Column("replacement_cost", false, false, false)]
        public decimal ReplacementCost { get; set; }
		
        [NotifyPropertyChanged, Column("rating", false, false, true)]
        public System.Nullable<int> Rating { get; set; }
		
        [NotifyPropertyChanged, Column("special_features", false, false, true)]
        public object SpecialFeatures { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [LeftJoinColumn]
        public Language OriginalLanguage { get; set; }
		
        [InnerJoinColumn]
        public Language Language { get; set; }
		
        [InnerJoinColumn]
        public FilmActor FilmFilmActor { get; set; }
		
        [InnerJoinColumn]
        public FilmCategory FilmFilmCategory { get; set; }
		
        [InnerJoinColumn]
        public Inventory FilmInventory { get; set; }
	}

	[Table("film_actor", "", "sakila")]
	public partial class FilmActor : Entity<FilmActor>
	{
		static FilmActor()
		{
			Join<Film>(t => t.Film, (t, f) => t.FilmId == f.FilmId); // Relation
			Join<Actor>(t => t.Actor, (t, f) => t.ActorId == f.ActorId); // Relation
		}

		
        [NotifyPropertyChanged, Column("actor_id", true, false, false)]
        public ushort ActorId { get; set; }
		
        [NotifyPropertyChanged, Column("film_id", true, false, false)]
        public ushort FilmId { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Film Film { get; set; }
		
        [InnerJoinColumn]
        public Actor Actor { get; set; }
	}

	[Table("film_category", "", "sakila")]
	public partial class FilmCategory : Entity<FilmCategory>
	{
		static FilmCategory()
		{
			Join<Film>(t => t.Film, (t, f) => t.FilmId == f.FilmId); // Relation
			Join<Category>(t => t.Category, (t, f) => t.CategoryId == f.CategoryId); // Relation
		}

		
        [NotifyPropertyChanged, Column("film_id", true, false, false)]
        public ushort FilmId { get; set; }
		
        [NotifyPropertyChanged, Column("category_id", true, false, false)]
        public byte CategoryId { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Film Film { get; set; }
		
        [InnerJoinColumn]
        public Category Category { get; set; }
	}

	[Table("film_text", "", "sakila")]
	public partial class FilmText : Entity<FilmText>
	{
		static FilmText()
		{
		}

		
        [NotifyPropertyChanged, Column("film_id", true, false, false)]
        public short FilmId { get; set; }
		
        [NotifyPropertyChanged, Column("title", false, false, false)]
        public string Title { get; set; }
		
        [NotifyPropertyChanged, Column("description", false, false, true)]
        public string Description { get; set; }
	
	}

	[Table("inventory", "", "sakila")]
	public partial class Inventory : Entity<Inventory>
	{
		static Inventory()
		{
			Join<Film>(t => t.Film, (t, f) => t.FilmId == f.FilmId); // Relation
			Join<Store>(t => t.Store, (t, f) => t.StoreId == f.StoreId); // Relation
			Join<Rental>(t => t.InventoryRental, (t, f) => t.InventoryId == f.InventoryId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("inventory_id", true, true, false)]
        public uint InventoryId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<uint>() : (uint)Convert.ChangeType(this.EntityIdentity, typeof(uint)); }
	        set { this.EntityIdentity = (uint)value; }
        }
		
        [NotifyPropertyChanged, Column("film_id", false, false, false)]
        public ushort FilmId { get; set; }
		
        [NotifyPropertyChanged, Column("store_id", false, false, false)]
        public byte StoreId { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Film Film { get; set; }
		
        [InnerJoinColumn]
        public Store Store { get; set; }
		
        [InnerJoinColumn]
        public Rental InventoryRental { get; set; }
	}

	[Table("language", "", "sakila")]
	public partial class Language : Entity<Language>
	{
		static Language()
		{
			Join<Film>(t => t.OriginalLanguageFilm, (t, f) => t.LanguageId == f.OriginalLanguageId); // Reverse Relation
			Join<Film>(t => t.LanguageFilm, (t, f) => t.LanguageId == f.LanguageId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("language_id", true, true, false)]
        public byte LanguageId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this.EntityIdentity, typeof(byte)); }
	        set { this.EntityIdentity = (byte)value; }
        }
		
        [NotifyPropertyChanged, Column("name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [LeftJoinColumn]
        public Film OriginalLanguageFilm { get; set; }
		
        [InnerJoinColumn]
        public Film LanguageFilm { get; set; }
	}

	[Table("payment", "", "sakila")]
	public partial class Payment : Entity<Payment>
	{
		static Payment()
		{
			Join<Customer>(t => t.Customer, (t, f) => t.CustomerId == f.CustomerId); // Relation
			Join<Rental>(t => t.Rental, (t, f) => t.RentalId == f.RentalId); // Relation
			Join<Staff>(t => t.Staff, (t, f) => t.StaffId == f.StaffId); // Relation
		}

		
        [NotifyPropertyChanged, Column("payment_id", true, true, false)]
        public ushort PaymentId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this.EntityIdentity, typeof(ushort)); }
	        set { this.EntityIdentity = (ushort)value; }
        }
		
        [NotifyPropertyChanged, Column("customer_id", false, false, false)]
        public ushort CustomerId { get; set; }
		
        [NotifyPropertyChanged, Column("staff_id", false, false, false)]
        public byte StaffId { get; set; }
		
        [NotifyPropertyChanged, Column("rental_id", false, false, true)]
        public System.Nullable<int> RentalId { get; set; }
		
        [NotifyPropertyChanged, Column("amount", false, false, false)]
        public decimal Amount { get; set; }
		
        [NotifyPropertyChanged, Column("payment_date", false, false, false)]
        public System.DateTime PaymentDate { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Customer Customer { get; set; }
		
        [LeftJoinColumn]
        public Rental Rental { get; set; }
		
        [InnerJoinColumn]
        public Staff Staff { get; set; }
	}

	[Table("rental", "", "sakila")]
	public partial class Rental : Entity<Rental>
	{
		static Rental()
		{
			Join<Payment>(t => t.RentalPayment, (t, f) => t.RentalId == f.RentalId); // Reverse Relation
			Join<Staff>(t => t.Staff, (t, f) => t.StaffId == f.StaffId); // Relation
			Join<Customer>(t => t.Customer, (t, f) => t.CustomerId == f.CustomerId); // Relation
			Join<Inventory>(t => t.Inventory, (t, f) => t.InventoryId == f.InventoryId); // Relation
		}

		
        [NotifyPropertyChanged, Column("rental_id", true, true, false)]
        public int RentalId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("rental_date", false, false, false)]
        public System.DateTime RentalDate { get; set; }
		
        [NotifyPropertyChanged, Column("inventory_id", false, false, false)]
        public uint InventoryId { get; set; }
		
        [NotifyPropertyChanged, Column("customer_id", false, false, false)]
        public ushort CustomerId { get; set; }
		
        [NotifyPropertyChanged, Column("return_date", false, false, true)]
        public System.Nullable<System.DateTime> ReturnDate { get; set; }
		
        [NotifyPropertyChanged, Column("staff_id", false, false, false)]
        public byte StaffId { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [LeftJoinColumn]
        public Payment RentalPayment { get; set; }
		
        [InnerJoinColumn]
        public Staff Staff { get; set; }
		
        [InnerJoinColumn]
        public Customer Customer { get; set; }
		
        [InnerJoinColumn]
        public Inventory Inventory { get; set; }
	}

	[Table("staff", "", "sakila")]
	public partial class Staff : Entity<Staff>
	{
		static Staff()
		{
			Join<Payment>(t => t.StaffPayment, (t, f) => t.StaffId == f.StaffId); // Reverse Relation
			Join<Rental>(t => t.StaffRental, (t, f) => t.StaffId == f.StaffId); // Reverse Relation
			Join<Store>(t => t.Store, (t, f) => t.StoreId == f.StoreId); // Relation
			Join<Address>(t => t.Address, (t, f) => t.AddressId == f.AddressId); // Relation
			Join<Store>(t => t.ManagerStaffStore, (t, f) => t.StaffId == f.ManagerStaffId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("staff_id", true, true, false)]
        public byte StaffId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this.EntityIdentity, typeof(byte)); }
	        set { this.EntityIdentity = (byte)value; }
        }
		
        [NotifyPropertyChanged, Column("first_name", false, false, false)]
        public string FirstName { get; set; }
		
        [NotifyPropertyChanged, Column("last_name", false, false, false)]
        public string LastName { get; set; }
		
        [NotifyPropertyChanged, Column("address_id", false, false, false)]
        public ushort AddressId { get; set; }
		
        [NotifyPropertyChanged, Column("picture", false, false, true)]
        public byte[] Picture { get; set; }
		
        [NotifyPropertyChanged, Column("email", false, false, true)]
        public string Email { get; set; }
		
        [NotifyPropertyChanged, Column("store_id", false, false, false)]
        public byte StoreId { get; set; }
		
        [NotifyPropertyChanged, Column("active", false, false, false)]
        public sbyte Active { get; set; }
		
        [NotifyPropertyChanged, Column("username", false, false, false)]
        public string Username { get; set; }
		
        [NotifyPropertyChanged, Column("password", false, false, true)]
        public string Password { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Payment StaffPayment { get; set; }
		
        [InnerJoinColumn]
        public Rental StaffRental { get; set; }
		
        [InnerJoinColumn]
        public Store Store { get; set; }
		
        [InnerJoinColumn]
        public Address Address { get; set; }
		
        [InnerJoinColumn]
        public Store ManagerStaffStore { get; set; }
	}

	[Table("store", "", "sakila")]
	public partial class Store : Entity<Store>
	{
		static Store()
		{
			Join<Customer>(t => t.StoreCustomer, (t, f) => t.StoreId == f.StoreId); // Reverse Relation
			Join<Inventory>(t => t.StoreInventory, (t, f) => t.StoreId == f.StoreId); // Reverse Relation
			Join<Staff>(t => t.StoreStaff, (t, f) => t.StoreId == f.StoreId); // Reverse Relation
			Join<Address>(t => t.Address, (t, f) => t.AddressId == f.AddressId); // Relation
			Join<Staff>(t => t.ManagerStaff, (t, f) => t.ManagerStaffId == f.StaffId); // Relation
		}

		
        [NotifyPropertyChanged, Column("store_id", true, true, false)]
        public byte StoreId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this.EntityIdentity, typeof(byte)); }
	        set { this.EntityIdentity = (byte)value; }
        }
		
        [NotifyPropertyChanged, Column("manager_staff_id", false, false, false)]
        public byte ManagerStaffId { get; set; }
		
        [NotifyPropertyChanged, Column("address_id", false, false, false)]
        public ushort AddressId { get; set; }
		
        [NotifyPropertyChanged, Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get; set; }
	
		
        [InnerJoinColumn]
        public Customer StoreCustomer { get; set; }
		
        [InnerJoinColumn]
        public Inventory StoreInventory { get; set; }
		
        [InnerJoinColumn]
        public Staff StoreStaff { get; set; }
		
        [InnerJoinColumn]
        public Address Address { get; set; }
		
        [InnerJoinColumn]
        public Staff ManagerStaff { get; set; }
	}

	[Table("tbl_billinginfo_bli", "", "sakila")]
	public partial class TblBillinginfoBli : Entity<TblBillinginfoBli>
	{
		static TblBillinginfoBli()
		{
			Join<TblBillBll>(t => t.BliBll, (t, f) => t.BliBllId == f.BLLId); // Relation
		}

		
        [NotifyPropertyChanged, Column("bli_Id", true, true, false)]
        public int BliId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("bli_bll_Id", false, false, true)]
        public System.Nullable<int> BliBllId { get; set; }
	
		
        [LeftJoinColumn]
        public TblBillBll BliBll { get; set; }
	}

	[Table("tbl_bill_bll", "", "sakila")]
	public partial class TblBillBll : Entity<TblBillBll>
	{
		static TblBillBll()
		{
			Join<TblBillinginfoBli>(t => t.BliBllTblBillinginfoBli, (t, f) => t.BLLId == f.BliBllId); // Reverse Relation
			Join<TblTransactionTsn>(t => t.BLLTSN, (t, f) => t.BLLTSNId == f.TSNId); // Relation
		}

		
        [NotifyPropertyChanged, Column("BLL_Id", true, true, false)]
        public int BLLId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("BLL_TSN_Id", false, false, true)]
        public System.Nullable<int> BLLTSNId { get; set; }
	
		
        [LeftJoinColumn]
        public TblBillinginfoBli BliBllTblBillinginfoBli { get; set; }
		
        [LeftJoinColumn]
        public TblTransactionTsn BLLTSN { get; set; }
	}

	[Table("tbl_transaction_tsn", "", "sakila")]
	public partial class TblTransactionTsn : Entity<TblTransactionTsn>
	{
		static TblTransactionTsn()
		{
			Join<TblBillBll>(t => t.BLLTSNTblBillBll, (t, f) => t.TSNId == f.BLLTSNId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("TSN_Id", true, true, false)]
        public int TSNId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
	
		
        [LeftJoinColumn]
        public TblBillBll BLLTSNTblBillBll { get; set; }
	}


	public class Procedures
	{
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] p_film_id   int</para>
        /// <para>[IN] p_store_id   int</para>
        /// <para>[OUT] p_film_count   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> FilmInStock(DataContext context, System.Nullable<int> inPFilmId, System.Nullable<int> inPStoreId, System.Nullable<int> outPFilmCount) 
        { 
            return context.ExecuteProcedure("`sakila`.`film_in_stock`", inPFilmId, inPStoreId, outPFilmCount);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] p_film_id   int</para>
        /// <para>[IN] p_store_id   int</para>
        /// <para>[OUT] p_film_count   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> FilmNotInStock(DataContext context, System.Nullable<int> inPFilmId, System.Nullable<int> inPStoreId, System.Nullable<int> outPFilmCount) 
        { 
            return context.ExecuteProcedure("`sakila`.`film_not_in_stock`", inPFilmId, inPStoreId, outPFilmCount);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] domId   int</para>
        /// <para>[OUT] boxId   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> GetDomBoxId(DataContext context, System.Nullable<int> inDomId, System.Nullable<int> outBoxId) 
        { 
            return context.ExecuteProcedure("`evoconcept`.`get_dom_box_id`", inDomId, outBoxId);
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// </summary>
        public static IEnumerable<DataRecord> NewProcedure(DataContext context) 
        { 
            return context.ExecuteProcedure("`world`.`new_procedure`");
        }
	
        /// <summary>
        /// Procedure Parameters:
        /// <para>[IN] min_monthly_purchases   utinyint</para>
        /// <para>[IN] min_dollar_amount_purchased   udecimal(10)</para>
        /// <para>[OUT] count_rewardees   int</para>
        /// </summary>
        public static IEnumerable<DataRecord> RewardsReport(DataContext context, System.Nullable<byte> inMinMonthlyPurchases, System.Nullable<decimal> inMinDollarAmountPurchased, System.Nullable<int> outCountRewardees) 
        { 
            return context.ExecuteProcedure("`sakila`.`rewards_report`", inMinMonthlyPurchases, inMinDollarAmountPurchased, outCountRewardees);
        }
	}
}