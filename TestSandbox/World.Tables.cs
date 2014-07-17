using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;


namespace TestSandbox.World
{
	[Table("city", "", "world")]
	public partial class City : Entity<City>
	{
		static City()
		{
		}

		
        [Column("ID", true, true, false)]
        public int ID
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _name;
        [Column("Name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private string _countryCode;
        [Column("CountryCode", false, false, false)]
        public string CountryCode { get { return this._countryCode; } set { this.__Set(ref this._countryCode, value); } }
		
        private string _district;
        [Column("District", false, false, false)]
        public string District { get { return this._district; } set { this.__Set(ref this._district, value); } }
		
        private int _population;
        [Column("Population", false, false, false)]
        public int Population { get { return this._population; } set { this.__Set(ref this._population, value); } }
	
	}

	[Table("country", "", "world")]
	public partial class Country : Entity<Country>
	{
		static Country()
		{
		}

		
        private string _code;
        [Column("Code", true, false, false)]
        public string Code { get { return this._code; } set { this.__Set(ref this._code, value); } }
		
        private string _name;
        [Column("Name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private int _continent;
        [Column("Continent", false, false, false)]
        public int Continent { get { return this._continent; } set { this.__Set(ref this._continent, value); } }
		
        private string _region;
        [Column("Region", false, false, false)]
        public string Region { get { return this._region; } set { this.__Set(ref this._region, value); } }
		
        private double _surfaceArea;
        [Column("SurfaceArea", false, false, false)]
        public double SurfaceArea { get { return this._surfaceArea; } set { this.__Set(ref this._surfaceArea, value); } }
		
        private System.Nullable<short> _indepYear;
        [Column("IndepYear", false, false, true)]
        public System.Nullable<short> IndepYear { get { return this._indepYear; } set { this.__Set(ref this._indepYear, value); } }
		
        private int _population;
        [Column("Population", false, false, false)]
        public int Population { get { return this._population; } set { this.__Set(ref this._population, value); } }
		
        private System.Nullable<double> _lifeExpectancy;
        [Column("LifeExpectancy", false, false, true)]
        public System.Nullable<double> LifeExpectancy { get { return this._lifeExpectancy; } set { this.__Set(ref this._lifeExpectancy, value); } }
		
        private System.Nullable<double> _gNP;
        [Column("GNP", false, false, true)]
        public System.Nullable<double> GNP { get { return this._gNP; } set { this.__Set(ref this._gNP, value); } }
		
        private System.Nullable<double> _gNPOld;
        [Column("GNPOld", false, false, true)]
        public System.Nullable<double> GNPOld { get { return this._gNPOld; } set { this.__Set(ref this._gNPOld, value); } }
		
        private string _localName;
        [Column("LocalName", false, false, false)]
        public string LocalName { get { return this._localName; } set { this.__Set(ref this._localName, value); } }
		
        private string _governmentForm;
        [Column("GovernmentForm", false, false, false)]
        public string GovernmentForm { get { return this._governmentForm; } set { this.__Set(ref this._governmentForm, value); } }
		
        private string _headOfState;
        [Column("HeadOfState", false, false, true)]
        public string HeadOfState { get { return this._headOfState; } set { this.__Set(ref this._headOfState, value); } }
		
        private System.Nullable<int> _capital;
        [Column("Capital", false, false, true)]
        public System.Nullable<int> Capital { get { return this._capital; } set { this.__Set(ref this._capital, value); } }
		
        private string _code2;
        [Column("Code2", false, false, false)]
        public string Code2 { get { return this._code2; } set { this.__Set(ref this._code2, value); } }
	
	}

	[Table("countrylanguage", "", "world")]
	public partial class Countrylanguage : Entity<Countrylanguage>
	{
		static Countrylanguage()
		{
		}

		
        private string _countryCode;
        [Column("CountryCode", true, false, false)]
        public string CountryCode { get { return this._countryCode; } set { this.__Set(ref this._countryCode, value); } }
		
        private string _language;
        [Column("Language", true, false, false)]
        public string Language { get { return this._language; } set { this.__Set(ref this._language, value); } }
		
        private int _isOfficial;
        [Column("IsOfficial", false, false, false)]
        public int IsOfficial { get { return this._isOfficial; } set { this.__Set(ref this._isOfficial, value); } }
		
        private double _percentage;
        [Column("Percentage", false, false, false)]
        public double Percentage { get { return this._percentage; } set { this.__Set(ref this._percentage, value); } }
	
	}


}

