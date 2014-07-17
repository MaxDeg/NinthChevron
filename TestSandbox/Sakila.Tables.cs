using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;


namespace TestSandbox.Sakila
{
	[Table("actor", "", "sakila")]
	public partial class Actor : Entity<Actor>
	{
		static Actor()
		{
			Join<FilmActor>(t => t.ActorFilmActor, (t, f) => t.ActorId == f.ActorId); // Reverse Relation
		}

		
        [Column("actor_id", true, true, false)]
        public ushort ActorId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this._entityIdentity, typeof(ushort)); }
	        set { this.__Set(ref this._entityIdentity, (ushort)value); }
        }
		
        private string _firstName;
        [Column("first_name", false, false, false)]
        public string FirstName { get { return this._firstName; } set { this.__Set(ref this._firstName, value); } }
		
        private string _lastName;
        [Column("last_name", false, false, false)]
        public string LastName { get { return this._lastName; } set { this.__Set(ref this._lastName, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
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

		
        [Column("address_id", true, true, false)]
        public ushort AddressId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this._entityIdentity, typeof(ushort)); }
	        set { this.__Set(ref this._entityIdentity, (ushort)value); }
        }
		
        private string __address;
        [Column("_address", false, false, true)]
        public string _Address { get { return this.__address; } set { this.__Set(ref this.__address, value); } }
		
        private string _address2;
        [Column("address2", false, false, true)]
        public string Address2 { get { return this._address2; } set { this.__Set(ref this._address2, value); } }
		
        private string _district;
        [Column("district", false, false, false)]
        public string District { get { return this._district; } set { this.__Set(ref this._district, value); } }
		
        private ushort _cityId;
        [Column("city_id", false, false, false)]
        public ushort CityId { get { return this._cityId; } set { this.__Set(ref this._cityId, value); } }
		
        private string _postalCode;
        [Column("postal_code", false, false, true)]
        public string PostalCode { get { return this._postalCode; } set { this.__Set(ref this._postalCode, value); } }
		
        private string _phone;
        [Column("phone", false, false, false)]
        public string Phone { get { return this._phone; } set { this.__Set(ref this._phone, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
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

		
        [Column("category_id", true, true, false)]
        public byte CategoryId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this._entityIdentity, typeof(byte)); }
	        set { this.__Set(ref this._entityIdentity, (byte)value); }
        }
		
        private string _name;
        [Column("name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
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

		
        [Column("city_id", true, true, false)]
        public ushort CityId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this._entityIdentity, typeof(ushort)); }
	        set { this.__Set(ref this._entityIdentity, (ushort)value); }
        }
		
        private string _name;
        [Column("name", false, false, true)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private ushort _countryId;
        [Column("country_id", false, false, false)]
        public ushort CountryId { get { return this._countryId; } set { this.__Set(ref this._countryId, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
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

		
        [Column("country_id", true, true, false)]
        public ushort CountryId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this._entityIdentity, typeof(ushort)); }
	        set { this.__Set(ref this._entityIdentity, (ushort)value); }
        }
		
        private string _name;
        [Column("name", false, false, true)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public City CountryCity { get; set; }
	}

	[Table("customer", "", "sakila")]
	public partial class Customer : Entity<Customer>
	{
		static Customer()
		{
			Join<Store>(t => t.Store, (t, f) => t.StoreId == f.StoreId); // Relation
			Join<Address>(t => t.Address, (t, f) => t.AddressId == f.AddressId); // Relation
			Join<Payment>(t => t.CustomerPayment, (t, f) => t.CustomerId == f.CustomerId); // Reverse Relation
			Join<Rental>(t => t.CustomerRental, (t, f) => t.CustomerId == f.CustomerId); // Reverse Relation
		}

		
        [Column("customer_id", true, true, false)]
        public ushort CustomerId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this._entityIdentity, typeof(ushort)); }
	        set { this.__Set(ref this._entityIdentity, (ushort)value); }
        }
		
        private byte _storeId;
        [Column("store_id", false, false, false)]
        public byte StoreId { get { return this._storeId; } set { this.__Set(ref this._storeId, value); } }
		
        private string _firstName;
        [Column("first_name", false, false, false)]
        public string FirstName { get { return this._firstName; } set { this.__Set(ref this._firstName, value); } }
		
        private string _lastName;
        [Column("last_name", false, false, false)]
        public string LastName { get { return this._lastName; } set { this.__Set(ref this._lastName, value); } }
		
        private string _email;
        [Column("email", false, false, true)]
        public string Email { get { return this._email; } set { this.__Set(ref this._email, value); } }
		
        private ushort _addressId;
        [Column("address_id", false, false, false)]
        public ushort AddressId { get { return this._addressId; } set { this.__Set(ref this._addressId, value); } }
		
        private sbyte _active;
        [Column("active", false, false, false)]
        public sbyte Active { get { return this._active; } set { this.__Set(ref this._active, value); } }
		
        private System.DateTime _createDate;
        [Column("create_date", false, false, false)]
        public System.DateTime CreateDate { get { return this._createDate; } set { this.__Set(ref this._createDate, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public Store Store { get; set; }
		
        [InnerJoinColumn]
        public Address Address { get; set; }
		
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
			Join<Language>(t => t.Language, (t, f) => t.LanguageId == f.LanguageId); // Relation
			Join<Language>(t => t.OriginalLanguage, (t, f) => t.OriginalLanguageId == f.LanguageId); // Relation
			Join<FilmActor>(t => t.FilmFilmActor, (t, f) => t.FilmId == f.FilmId); // Reverse Relation
			Join<FilmCategory>(t => t.FilmFilmCategory, (t, f) => t.FilmId == f.FilmId); // Reverse Relation
			Join<Inventory>(t => t.FilmInventory, (t, f) => t.FilmId == f.FilmId); // Reverse Relation
		}

		
        [Column("film_id", true, true, false)]
        public ushort FilmId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this._entityIdentity, typeof(ushort)); }
	        set { this.__Set(ref this._entityIdentity, (ushort)value); }
        }
		
        private string _title;
        [Column("title", false, false, false)]
        public string Title { get { return this._title; } set { this.__Set(ref this._title, value); } }
		
        private string _description;
        [Column("description", false, false, true)]
        public string Description { get { return this._description; } set { this.__Set(ref this._description, value); } }
		
        private System.Nullable<short> _releaseYear;
        [Column("release_year", false, false, true)]
        public System.Nullable<short> ReleaseYear { get { return this._releaseYear; } set { this.__Set(ref this._releaseYear, value); } }
		
        private byte _languageId;
        [Column("language_id", false, false, false)]
        public byte LanguageId { get { return this._languageId; } set { this.__Set(ref this._languageId, value); } }
		
        private System.Nullable<byte> _originalLanguageId;
        [Column("original_language_id", false, false, true)]
        public System.Nullable<byte> OriginalLanguageId { get { return this._originalLanguageId; } set { this.__Set(ref this._originalLanguageId, value); } }
		
        private byte _rentalDuration;
        [Column("rental_duration", false, false, false)]
        public byte RentalDuration { get { return this._rentalDuration; } set { this.__Set(ref this._rentalDuration, value); } }
		
        private decimal _rentalRate;
        [Column("rental_rate", false, false, false)]
        public decimal RentalRate { get { return this._rentalRate; } set { this.__Set(ref this._rentalRate, value); } }
		
        private System.Nullable<ushort> _length;
        [Column("length", false, false, true)]
        public System.Nullable<ushort> Length { get { return this._length; } set { this.__Set(ref this._length, value); } }
		
        private decimal _replacementCost;
        [Column("replacement_cost", false, false, false)]
        public decimal ReplacementCost { get { return this._replacementCost; } set { this.__Set(ref this._replacementCost, value); } }
		
        private System.Nullable<int> _rating;
        [Column("rating", false, false, true)]
        public System.Nullable<int> Rating { get { return this._rating; } set { this.__Set(ref this._rating, value); } }
		
        private object _specialFeatures;
        [Column("special_features", false, false, true)]
        public object SpecialFeatures { get { return this._specialFeatures; } set { this.__Set(ref this._specialFeatures, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public Language Language { get; set; }
		
        [LeftJoinColumn]
        public Language OriginalLanguage { get; set; }
		
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

		
        private ushort _actorId;
        [Column("actor_id", true, false, false)]
        public ushort ActorId { get { return this._actorId; } set { this.__Set(ref this._actorId, value); } }
		
        private ushort _filmId;
        [Column("film_id", true, false, false)]
        public ushort FilmId { get { return this._filmId; } set { this.__Set(ref this._filmId, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
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
			Join<Category>(t => t.Category, (t, f) => t.CategoryId == f.CategoryId); // Relation
			Join<Film>(t => t.Film, (t, f) => t.FilmId == f.FilmId); // Relation
		}

		
        private ushort _filmId;
        [Column("film_id", true, false, false)]
        public ushort FilmId { get { return this._filmId; } set { this.__Set(ref this._filmId, value); } }
		
        private byte _categoryId;
        [Column("category_id", true, false, false)]
        public byte CategoryId { get { return this._categoryId; } set { this.__Set(ref this._categoryId, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public Category Category { get; set; }
		
        [InnerJoinColumn]
        public Film Film { get; set; }
	}

	[Table("film_text", "", "sakila")]
	public partial class FilmText : Entity<FilmText>
	{
		static FilmText()
		{
		}

		
        private short _filmId;
        [Column("film_id", true, false, false)]
        public short FilmId { get { return this._filmId; } set { this.__Set(ref this._filmId, value); } }
		
        private string _title;
        [Column("title", false, false, false)]
        public string Title { get { return this._title; } set { this.__Set(ref this._title, value); } }
		
        private string _description;
        [Column("description", false, false, true)]
        public string Description { get { return this._description; } set { this.__Set(ref this._description, value); } }
	
	}

	[Table("inventory", "", "sakila")]
	public partial class Inventory : Entity<Inventory>
	{
		static Inventory()
		{
			Join<Store>(t => t.Store, (t, f) => t.StoreId == f.StoreId); // Relation
			Join<Film>(t => t.Film, (t, f) => t.FilmId == f.FilmId); // Relation
			Join<Rental>(t => t.InventoryRental, (t, f) => t.InventoryId == f.InventoryId); // Reverse Relation
		}

		
        [Column("inventory_id", true, true, false)]
        public uint InventoryId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<uint>() : (uint)Convert.ChangeType(this._entityIdentity, typeof(uint)); }
	        set { this.__Set(ref this._entityIdentity, (uint)value); }
        }
		
        private ushort _filmId;
        [Column("film_id", false, false, false)]
        public ushort FilmId { get { return this._filmId; } set { this.__Set(ref this._filmId, value); } }
		
        private byte _storeId;
        [Column("store_id", false, false, false)]
        public byte StoreId { get { return this._storeId; } set { this.__Set(ref this._storeId, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public Store Store { get; set; }
		
        [InnerJoinColumn]
        public Film Film { get; set; }
		
        [InnerJoinColumn]
        public Rental InventoryRental { get; set; }
	}

	[Table("language", "", "sakila")]
	public partial class Language : Entity<Language>
	{
		static Language()
		{
			Join<Film>(t => t.LanguageFilm, (t, f) => t.LanguageId == f.LanguageId); // Reverse Relation
			Join<Film>(t => t.OriginalLanguageFilm, (t, f) => t.LanguageId == f.OriginalLanguageId); // Reverse Relation
		}

		
        [Column("language_id", true, true, false)]
        public byte LanguageId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this._entityIdentity, typeof(byte)); }
	        set { this.__Set(ref this._entityIdentity, (byte)value); }
        }
		
        private string _name;
        [Column("name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public Film LanguageFilm { get; set; }
		
        [LeftJoinColumn]
        public Film OriginalLanguageFilm { get; set; }
	}

	[Table("payment", "", "sakila")]
	public partial class Payment : Entity<Payment>
	{
		static Payment()
		{
			Join<Rental>(t => t.Rental, (t, f) => t.RentalId == f.RentalId); // Relation
			Join<Staff>(t => t.Staff, (t, f) => t.StaffId == f.StaffId); // Relation
			Join<Customer>(t => t.Customer, (t, f) => t.CustomerId == f.CustomerId); // Relation
		}

		
        [Column("payment_id", true, true, false)]
        public ushort PaymentId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<ushort>() : (ushort)Convert.ChangeType(this._entityIdentity, typeof(ushort)); }
	        set { this.__Set(ref this._entityIdentity, (ushort)value); }
        }
		
        private ushort _customerId;
        [Column("customer_id", false, false, false)]
        public ushort CustomerId { get { return this._customerId; } set { this.__Set(ref this._customerId, value); } }
		
        private byte _staffId;
        [Column("staff_id", false, false, false)]
        public byte StaffId { get { return this._staffId; } set { this.__Set(ref this._staffId, value); } }
		
        private System.Nullable<int> _rentalId;
        [Column("rental_id", false, false, true)]
        public System.Nullable<int> RentalId { get { return this._rentalId; } set { this.__Set(ref this._rentalId, value); } }
		
        private decimal _amount;
        [Column("amount", false, false, false)]
        public decimal Amount { get { return this._amount; } set { this.__Set(ref this._amount, value); } }
		
        private System.DateTime _paymentDate;
        [Column("payment_date", false, false, false)]
        public System.DateTime PaymentDate { get { return this._paymentDate; } set { this.__Set(ref this._paymentDate, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [LeftJoinColumn]
        public Rental Rental { get; set; }
		
        [InnerJoinColumn]
        public Staff Staff { get; set; }
		
        [InnerJoinColumn]
        public Customer Customer { get; set; }
	}

	[Table("rental", "", "sakila")]
	public partial class Rental : Entity<Rental>
	{
		static Rental()
		{
			Join<Payment>(t => t.RentalPayment, (t, f) => t.RentalId == f.RentalId); // Reverse Relation
			Join<Customer>(t => t.Customer, (t, f) => t.CustomerId == f.CustomerId); // Relation
			Join<Inventory>(t => t.Inventory, (t, f) => t.InventoryId == f.InventoryId); // Relation
			Join<Staff>(t => t.Staff, (t, f) => t.StaffId == f.StaffId); // Relation
		}

		
        [Column("rental_id", true, true, false)]
        public int RentalId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.DateTime _rentalDate;
        [Column("rental_date", false, false, false)]
        public System.DateTime RentalDate { get { return this._rentalDate; } set { this.__Set(ref this._rentalDate, value); } }
		
        private uint _inventoryId;
        [Column("inventory_id", false, false, false)]
        public uint InventoryId { get { return this._inventoryId; } set { this.__Set(ref this._inventoryId, value); } }
		
        private ushort _customerId;
        [Column("customer_id", false, false, false)]
        public ushort CustomerId { get { return this._customerId; } set { this.__Set(ref this._customerId, value); } }
		
        private System.Nullable<System.DateTime> _returnDate;
        [Column("return_date", false, false, true)]
        public System.Nullable<System.DateTime> ReturnDate { get { return this._returnDate; } set { this.__Set(ref this._returnDate, value); } }
		
        private byte _staffId;
        [Column("staff_id", false, false, false)]
        public byte StaffId { get { return this._staffId; } set { this.__Set(ref this._staffId, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [LeftJoinColumn]
        public Payment RentalPayment { get; set; }
		
        [InnerJoinColumn]
        public Customer Customer { get; set; }
		
        [InnerJoinColumn]
        public Inventory Inventory { get; set; }
		
        [InnerJoinColumn]
        public Staff Staff { get; set; }
	}

	[Table("staff", "", "sakila")]
	public partial class Staff : Entity<Staff>
	{
		static Staff()
		{
			Join<Payment>(t => t.StaffPayment, (t, f) => t.StaffId == f.StaffId); // Reverse Relation
			Join<Rental>(t => t.StaffRental, (t, f) => t.StaffId == f.StaffId); // Reverse Relation
			Join<Address>(t => t.Address, (t, f) => t.AddressId == f.AddressId); // Relation
			Join<Store>(t => t.Store, (t, f) => t.StoreId == f.StoreId); // Relation
			Join<Store>(t => t.ManagerStaffStore, (t, f) => t.StaffId == f.ManagerStaffId); // Reverse Relation
		}

		
        [Column("staff_id", true, true, false)]
        public byte StaffId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this._entityIdentity, typeof(byte)); }
	        set { this.__Set(ref this._entityIdentity, (byte)value); }
        }
		
        private string _firstName;
        [Column("first_name", false, false, false)]
        public string FirstName { get { return this._firstName; } set { this.__Set(ref this._firstName, value); } }
		
        private string _lastName;
        [Column("last_name", false, false, false)]
        public string LastName { get { return this._lastName; } set { this.__Set(ref this._lastName, value); } }
		
        private ushort _addressId;
        [Column("address_id", false, false, false)]
        public ushort AddressId { get { return this._addressId; } set { this.__Set(ref this._addressId, value); } }
		
        private byte[] _picture;
        [Column("picture", false, false, true)]
        public byte[] Picture { get { return this._picture; } set { this.__Set(ref this._picture, value); } }
		
        private string _email;
        [Column("email", false, false, true)]
        public string Email { get { return this._email; } set { this.__Set(ref this._email, value); } }
		
        private byte _storeId;
        [Column("store_id", false, false, false)]
        public byte StoreId { get { return this._storeId; } set { this.__Set(ref this._storeId, value); } }
		
        private sbyte _active;
        [Column("active", false, false, false)]
        public sbyte Active { get { return this._active; } set { this.__Set(ref this._active, value); } }
		
        private string _username;
        [Column("username", false, false, false)]
        public string Username { get { return this._username; } set { this.__Set(ref this._username, value); } }
		
        private string _password;
        [Column("password", false, false, true)]
        public string Password { get { return this._password; } set { this.__Set(ref this._password, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public Payment StaffPayment { get; set; }
		
        [InnerJoinColumn]
        public Rental StaffRental { get; set; }
		
        [InnerJoinColumn]
        public Address Address { get; set; }
		
        [InnerJoinColumn]
        public Store Store { get; set; }
		
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
			Join<Staff>(t => t.ManagerStaff, (t, f) => t.ManagerStaffId == f.StaffId); // Relation
			Join<Address>(t => t.Address, (t, f) => t.AddressId == f.AddressId); // Relation
		}

		
        [Column("store_id", true, true, false)]
        public byte StoreId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this._entityIdentity, typeof(byte)); }
	        set { this.__Set(ref this._entityIdentity, (byte)value); }
        }
		
        private byte _managerStaffId;
        [Column("manager_staff_id", false, false, false)]
        public byte ManagerStaffId { get { return this._managerStaffId; } set { this.__Set(ref this._managerStaffId, value); } }
		
        private ushort _addressId;
        [Column("address_id", false, false, false)]
        public ushort AddressId { get { return this._addressId; } set { this.__Set(ref this._addressId, value); } }
		
        private System.DateTime _lastUpdate;
        [Column("last_update", false, false, false)]
        public System.DateTime LastUpdate { get { return this._lastUpdate; } set { this.__Set(ref this._lastUpdate, value); } }
	
		
        [InnerJoinColumn]
        public Customer StoreCustomer { get; set; }
		
        [InnerJoinColumn]
        public Inventory StoreInventory { get; set; }
		
        [InnerJoinColumn]
        public Staff StoreStaff { get; set; }
		
        [InnerJoinColumn]
        public Staff ManagerStaff { get; set; }
		
        [InnerJoinColumn]
        public Address Address { get; set; }
	}

	[Table("tbl_billinginfo_bli", "", "sakila")]
	public partial class TblBillinginfoBli : Entity<TblBillinginfoBli>
	{
		static TblBillinginfoBli()
		{
			Join<TblBillBll>(t => t.BliBll, (t, f) => t.BliBllId == f.BLLId); // Relation
		}

		
        [Column("bli_Id", true, true, false)]
        public int BliId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _bliBllId;
        [Column("bli_bll_Id", false, false, true)]
        public System.Nullable<int> BliBllId { get { return this._bliBllId; } set { this.__Set(ref this._bliBllId, value); } }
	
		
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

		
        [Column("BLL_Id", true, true, false)]
        public int BLLId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _bLLTSNId;
        [Column("BLL_TSN_Id", false, false, true)]
        public System.Nullable<int> BLLTSNId { get { return this._bLLTSNId; } set { this.__Set(ref this._bLLTSNId, value); } }
	
		
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

		
        [Column("TSN_Id", true, true, false)]
        public int TSNId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
	
		
        [LeftJoinColumn]
        public TblBillBll BLLTSNTblBillBll { get; set; }
	}


}

