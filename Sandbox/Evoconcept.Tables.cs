using System;
using System.Collections.Generic;
using NinthChevron.Data;
using NinthChevron.Data.Entity;
using NinthChevron.Helpers;
using NinthChevron.ComponentModel.DataAnnotations;

namespace Sandbox.Entities.Evoconcept
{
	[Table("lut_country_cny", "", "evoconcept")]
	public partial class Country : Entity<Country>
	{
		static Country()
		{
			Join<Userinformation>(t => t.UserinformationCountry, (t, f) => t.Code == f.CountryCode); // Reverse Relation
		}

		
        private string _code;
        [Column("cny_code", true, false, false)]
        public string Code { get { return this._code; } set { this.__Set(ref this._code, value); } }
		
        private string _name;
        [Column("cny_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
	
		
        [LeftJoinColumn]
        public Userinformation UserinformationCountry { get; set; }
	}

	[Table("lut_ipaddress_ipa", "", "evoconcept")]
	public partial class Ipaddress : Entity<Ipaddress>
	{
		static Ipaddress()
		{
			Join<Serverip>(t => t.ServeripIpaddress, (t, f) => t.Id == f.IpaddressId); // Reverse Relation
		}

		
        [Column("ipa_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _ip;
        [Column("ipa_ip", false, false, false)]
        public string Ip { get { return this._ip; } set { this.__Set(ref this._ip, value); } }
		
        private string _iptCode;
        [Column("ipa_ipt_code", false, false, false)]
        public string IptCode { get { return this._iptCode; } set { this.__Set(ref this._iptCode, value); } }
	
		
        [InnerJoinColumn]
        public Serverip ServeripIpaddress { get; set; }
	}

	[Table("lut_offer_ofr", "", "evoconcept")]
	public partial class Offer : Entity<Offer>
	{
		static Offer()
		{
			Join<Reduction>(t => t.ReductionOffer, (t, f) => t.Id == f.OfferId); // Reverse Relation
			Join<Evobox>(t => t.EvoboxOffer, (t, f) => t.Id == f.OfferId); // Reverse Relation
			Join<OfrSpv>(t => t.OfrSpvOffer, (t, f) => t.Id == f.OfferId); // Reverse Relation
		}

		
        [Column("ofr_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _name;
        [Column("ofr_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private decimal _price;
        [Column("ofr_price", false, false, false)]
        public decimal Price { get { return this._price; } set { this.__Set(ref this._price, value); } }
		
        private sbyte _locked;
        [Column("ofr_locked", false, false, false)]
        public sbyte Locked { get { return this._locked; } set { this.__Set(ref this._locked, value); } }
		
        private int _trialPeriod;
        [Column("ofr_trial_period", false, false, false)]
        public int TrialPeriod { get { return this._trialPeriod; } set { this.__Set(ref this._trialPeriod, value); } }
		
        private string _type;
        [Column("ofr_type", false, false, false)]
        public string Type { get { return this._type; } set { this.__Set(ref this._type, value); } }
	
		
        [LeftJoinColumn]
        public Reduction ReductionOffer { get; set; }
		
        [InnerJoinColumn]
        public Evobox EvoboxOffer { get; set; }
		
        [InnerJoinColumn]
        public OfrSpv OfrSpvOffer { get; set; }
	}

	[Table("lut_reduction_red", "", "evoconcept")]
	public partial class Reduction : Entity<Reduction>
	{
		static Reduction()
		{
			Join<Offer>(t => t.Offer, (t, f) => t.OfferId == f.Id); // Relation
		}

		
        [Column("red_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _offerId;
        [Column("red_ofr_id", false, false, true)]
        public System.Nullable<int> OfferId { get { return this._offerId; } set { this.__Set(ref this._offerId, value); } }
		
        private int _quantity;
        [Column("red_quantity", false, false, false)]
        public int Quantity { get { return this._quantity; } set { this.__Set(ref this._quantity, value); } }
		
        private int _percentage;
        [Column("red_percentage", false, false, false)]
        public int Percentage { get { return this._percentage; } set { this.__Set(ref this._percentage, value); } }
	
		
        [LeftJoinColumn]
        public Offer Offer { get; set; }
	}

	[Table("lut_specificationcategory_scc", "", "evoconcept")]
	public partial class Specificationcategory : Entity<Specificationcategory>
	{
		static Specificationcategory()
		{
			Join<Specification>(t => t.SpecificationSpecificationcategory, (t, f) => t.Code == f.SpecificationcategoryCode); // Reverse Relation
		}

		
        private string _code;
        [Column("scc_code", true, false, false)]
        public string Code { get { return this._code; } set { this.__Set(ref this._code, value); } }
		
        private string _name;
        [Column("scc_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private int _order;
        [Column("scc_order", false, false, false)]
        public int Order { get { return this._order; } set { this.__Set(ref this._order, value); } }
	
		
        [InnerJoinColumn]
        public Specification SpecificationSpecificationcategory { get; set; }
	}

	[Table("lut_specificationvalue_spv", "", "evoconcept")]
	public partial class Specificationvalue : Entity<Specificationvalue>
	{
		static Specificationvalue()
		{
			Join<Specification>(t => t.Specification, (t, f) => t.SpecificationCode == f.Code); // Relation
			Join<OfrSpv>(t => t.OfrSpvSpecificationvalue, (t, f) => t.Id == f.SpecificationvalueId); // Reverse Relation
		}

		
        [Column("spv_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _value;
        [Column("spv_value", false, false, false)]
        public int Value { get { return this._value; } set { this.__Set(ref this._value, value); } }
		
        private sbyte _custom;
        [Column("spv_custom", false, false, false)]
        public sbyte Custom { get { return this._custom; } set { this.__Set(ref this._custom, value); } }
		
        private string _specificationCode;
        [Column("spv_spc_code", false, false, false)]
        public string SpecificationCode { get { return this._specificationCode; } set { this.__Set(ref this._specificationCode, value); } }
	
		
        [InnerJoinColumn]
        public Specification Specification { get; set; }
		
        [InnerJoinColumn]
        public OfrSpv OfrSpvSpecificationvalue { get; set; }
	}

	[Table("lut_specification_spc", "", "evoconcept")]
	public partial class Specification : Entity<Specification>
	{
		static Specification()
		{
			Join<Specificationvalue>(t => t.SpecificationvalueSpecification, (t, f) => t.Code == f.SpecificationCode); // Reverse Relation
			Join<Specificationcategory>(t => t.Specificationcategory, (t, f) => t.SpecificationcategoryCode == f.Code); // Relation
		}

		
        private string _code;
        [Column("spc_code", true, false, false)]
        public string Code { get { return this._code; } set { this.__Set(ref this._code, value); } }
		
        private string _name;
        [Column("spc_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private int _period;
        [Column("spc_period", false, false, false)]
        public int Period { get { return this._period; } set { this.__Set(ref this._period, value); } }
		
        private string _description;
        [Column("spc_description", false, false, false)]
        public string Description { get { return this._description; } set { this.__Set(ref this._description, value); } }
		
        private string _unit;
        [Column("spc_unit", false, false, true)]
        public string Unit { get { return this._unit; } set { this.__Set(ref this._unit, value); } }
		
        private string _specificationcategoryCode;
        [Column("spc_scc_code", false, false, false)]
        public string SpecificationcategoryCode { get { return this._specificationcategoryCode; } set { this.__Set(ref this._specificationcategoryCode, value); } }
		
        private sbyte _show0;
        [Column("spc_show_0", false, false, false)]
        public sbyte Show0 { get { return this._show0; } set { this.__Set(ref this._show0, value); } }
		
        private sbyte _addS;
        [Column("spc_add_s", false, false, false)]
        public sbyte AddS { get { return this._addS; } set { this.__Set(ref this._addS, value); } }
		
        private int _order;
        [Column("spc_order", false, false, false)]
        public int Order { get { return this._order; } set { this.__Set(ref this._order, value); } }
		
        private sbyte _image;
        [Column("spc_image", false, false, false)]
        public sbyte Image { get { return this._image; } set { this.__Set(ref this._image, value); } }
	
		
        [InnerJoinColumn]
        public Specificationvalue SpecificationvalueSpecification { get; set; }
		
        [InnerJoinColumn]
        public Specificationcategory Specificationcategory { get; set; }
	}

	[Table("tbl_alias_als", "", "evoconcept")]
	public partial class Alias : Entity<Alias>
	{
		static Alias()
		{
			Join<Domain>(t => t.Domain, (t, f) => t.DomainId == f.Id); // Relation
			Join<Evobox>(t => t.DerivedEvobox, (t, f) => t.DerivedEvoboxId == f.Id); // Relation
		}

		
        [Column("als_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _domainId;
        [Column("als_dom_id", false, false, false)]
        public int DomainId { get { return this._domainId; } set { this.__Set(ref this._domainId, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("als_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("als_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private string _localPart;
        [Column("als_local_part", false, false, false)]
        public string LocalPart { get { return this._localPart; } set { this.__Set(ref this._localPart, value); } }
		
        private string _rcpt;
        [Column("als_rcpt", false, false, false)]
        public string Rcpt { get { return this._rcpt; } set { this.__Set(ref this._rcpt, value); } }
		
        private int _derivedEvoboxId;
        [Column("als_derived_ebx_id", false, false, false)]
        public int DerivedEvoboxId { get { return this._derivedEvoboxId; } set { this.__Set(ref this._derivedEvoboxId, value); } }
	
		
        [InnerJoinColumn]
        public Domain Domain { get; set; }
		
        [InnerJoinColumn]
        public Evobox DerivedEvobox { get; set; }
	}

	[Table("tbl_applicationattribute_apa", "", "evoconcept")]
	public partial class Applicationattribute : Entity<Applicationattribute>
	{
		static Applicationattribute()
		{
			Join<Application>(t => t.Application, (t, f) => t.ApplicationId == f.Id); // Relation
		}

		
        [Column("apa_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _applicationId;
        [Column("apa_app_id", false, false, false)]
        public int ApplicationId { get { return this._applicationId; } set { this.__Set(ref this._applicationId, value); } }
		
        private string _value;
        [Column("apa_value", false, false, false)]
        public string Value { get { return this._value; } set { this.__Set(ref this._value, value); } }
		
        private string _aatCode;
        [Column("apa_aat_code", false, false, false)]
        public string AatCode { get { return this._aatCode; } set { this.__Set(ref this._aatCode, value); } }
	
		
        [InnerJoinColumn]
        public Application Application { get; set; }
	}

	[Table("tbl_applicationip_aip", "", "evoconcept")]
	public partial class Applicationip : Entity<Applicationip>
	{
		static Applicationip()
		{
		}

		
        private string _ip;
        [Column("aip_ip", false, false, false)]
        public string Ip { get { return this._ip; } set { this.__Set(ref this._ip, value); } }
		
        private System.Nullable<int> _applicationId;
        [Column("aip_app_id", false, false, true)]
        public System.Nullable<int> ApplicationId { get { return this._applicationId; } set { this.__Set(ref this._applicationId, value); } }
	
	}

	[Table("tbl_application_app", "", "evoconcept")]
	public partial class Application : Entity<Application>
	{
		static Application()
		{
			Join<Applicationattribute>(t => t.ApplicationattributeApplication, (t, f) => t.Id == f.ApplicationId); // Reverse Relation
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
			Join<Service>(t => t.Service, (t, f) => t.ServiceId == f.Id); // Relation
			Join<Website>(t => t.WebsiteApplication, (t, f) => t.Id == f.ApplicationId); // Reverse Relation
		}

		
        [Column("app_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _evoboxId;
        [Column("app_ebx_id", false, false, false)]
        public int EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("app_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("app_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private string _name;
        [Column("app_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private int _serviceId;
        [Column("app_svc_id", false, false, false)]
        public int ServiceId { get { return this._serviceId; } set { this.__Set(ref this._serviceId, value); } }
		
        private string _aptCode;
        [Column("app_apt_code", false, false, false)]
        public string AptCode { get { return this._aptCode; } set { this.__Set(ref this._aptCode, value); } }
	
		
        [InnerJoinColumn]
        public Applicationattribute ApplicationattributeApplication { get; set; }
		
        [InnerJoinColumn]
        public Evobox Evobox { get; set; }
		
        [InnerJoinColumn]
        public Service Service { get; set; }
		
        [InnerJoinColumn]
        public Website WebsiteApplication { get; set; }
	}

	[Table("tbl_bill_bll", "", "evoconcept")]
	public partial class Bill : Entity<Bill>
	{
		static Bill()
		{
			Join<Transaction>(t => t.Transaction, (t, f) => t.TransactionId == f.Id); // Relation
		}

		
        [Column("bll_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _transactionId;
        [Column("bll_tsn_id", false, false, false)]
        public int TransactionId { get { return this._transactionId; } set { this.__Set(ref this._transactionId, value); } }
	
		
        [InnerJoinColumn]
        public Transaction Transaction { get; set; }
	}

	[Table("tbl_conditions_cgv", "", "evoconcept")]
	public partial class Conditions : Entity<Conditions>
	{
		static Conditions()
		{
			Join<UsrEbx>(t => t.UsrEbx, (t, f) => t.UsrEbxId == f.Id); // Relation
		}

		
        [Column("cgv_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _name;
        [Column("cgv_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private System.Nullable<System.DateTime> _date;
        [Column("cgv_date", false, false, true)]
        public System.Nullable<System.DateTime> Date { get { return this._date; } set { this.__Set(ref this._date, value); } }
		
        private System.Nullable<int> _usrEbxId;
        [Column("cgv_ubx_id", false, false, true)]
        public System.Nullable<int> UsrEbxId { get { return this._usrEbxId; } set { this.__Set(ref this._usrEbxId, value); } }
	
		
        [LeftJoinColumn]
        public UsrEbx UsrEbx { get; set; }
	}

	[Table("tbl_cron_crn", "", "evoconcept")]
	public partial class Cron : Entity<Cron>
	{
		static Cron()
		{
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
			Join<Service>(t => t.Service, (t, f) => t.ServiceId == f.Id); // Relation
		}

		
        [Column("crn_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _evoboxId;
        [Column("crn_ebx_id", false, false, false)]
        public int EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private string _url;
        [Column("crn_url", false, false, false)]
        public string Url { get { return this._url; } set { this.__Set(ref this._url, value); } }
		
        private int _month;
        [Column("crn_month", false, false, false)]
        public int Month { get { return this._month; } set { this.__Set(ref this._month, value); } }
		
        private int _day;
        [Column("crn_day", false, false, false)]
        public int Day { get { return this._day; } set { this.__Set(ref this._day, value); } }
		
        private int _hour;
        [Column("crn_hour", false, false, false)]
        public int Hour { get { return this._hour; } set { this.__Set(ref this._hour, value); } }
		
        private long _minute;
        [Column("crn_minute", false, false, false)]
        public long Minute { get { return this._minute; } set { this.__Set(ref this._minute, value); } }
		
        private string _emailNotification;
        [Column("crn_email_notification", false, false, true)]
        public string EmailNotification { get { return this._emailNotification; } set { this.__Set(ref this._emailNotification, value); } }
		
        private System.Nullable<System.DateTime> _lastExecuted;
        [Column("crn_last_executed", false, false, true)]
        public System.Nullable<System.DateTime> LastExecuted { get { return this._lastExecuted; } set { this.__Set(ref this._lastExecuted, value); } }
		
        private System.Nullable<int> _lastExecutionTime;
        [Column("crn_last_execution_time", false, false, true)]
        public System.Nullable<int> LastExecutionTime { get { return this._lastExecutionTime; } set { this.__Set(ref this._lastExecutionTime, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("crn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("crn_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private int _serviceId;
        [Column("crn_svc_id", false, false, false)]
        public int ServiceId { get { return this._serviceId; } set { this.__Set(ref this._serviceId, value); } }
	
		
        [InnerJoinColumn]
        public Evobox Evobox { get; set; }
		
        [InnerJoinColumn]
        public Service Service { get; set; }
	}

	[Table("tbl_databaselogin_dbl", "", "evoconcept")]
	public partial class Databaselogin : Entity<Databaselogin>
	{
		static Databaselogin()
		{
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
			Join<Service>(t => t.Service, (t, f) => t.ServiceId == f.Id); // Relation
		}

		
        [Column("dbl_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _login;
        [Column("dbl_login", false, false, false)]
        public string Login { get { return this._login; } set { this.__Set(ref this._login, value); } }
		
        private int _evoboxId;
        [Column("dbl_ebx_id", false, false, false)]
        public int EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("dbl_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("dbl_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private int _serviceId;
        [Column("dbl_svc_id", false, false, false)]
        public int ServiceId { get { return this._serviceId; } set { this.__Set(ref this._serviceId, value); } }
		
        private string _password;
        [Column("dbl_password", false, false, true)]
        public string Password { get { return this._password; } set { this.__Set(ref this._password, value); } }
	
		
        [InnerJoinColumn]
        public Evobox Evobox { get; set; }
		
        [InnerJoinColumn]
        public Service Service { get; set; }
	}

	[Table("tbl_database_dtb", "", "evoconcept")]
	public partial class Database : Entity<Database>
	{
		static Database()
		{
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
			Join<Service>(t => t.Service, (t, f) => t.ServiceId == f.Id); // Relation
		}

		
        [Column("dtb_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _name;
        [Column("dtb_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private int _evoboxId;
        [Column("dtb_ebx_id", false, false, false)]
        public int EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("dtb_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("dtb_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private int _serviceId;
        [Column("dtb_svc_id", false, false, false)]
        public int ServiceId { get { return this._serviceId; } set { this.__Set(ref this._serviceId, value); } }
	
		
        [InnerJoinColumn]
        public Evobox Evobox { get; set; }
		
        [InnerJoinColumn]
        public Service Service { get; set; }
	}

	[Table("tbl_dnsrecord_dns", "", "evoconcept")]
	public partial class Dnsrecord : Entity<Dnsrecord>
	{
		static Dnsrecord()
		{
			Join<Domain>(t => t.Sdm, (t, f) => t.SdmId == f.Id); // Relation
			Join<Domain>(t => t.Domain, (t, f) => t.DomainId == f.Id); // Relation
		}

		
        [Column("dns_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _domainId;
        [Column("dns_dom_id", false, false, false)]
        public int DomainId { get { return this._domainId; } set { this.__Set(ref this._domainId, value); } }
		
        private string _name;
        [Column("dns_name", false, false, true)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private System.Nullable<int> _priority;
        [Column("dns_priority", false, false, true)]
        public System.Nullable<int> Priority { get { return this._priority; } set { this.__Set(ref this._priority, value); } }
		
        private string _to;
        [Column("dns_to", false, false, false)]
        public string To { get { return this._to; } set { this.__Set(ref this._to, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("dns_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("dns_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private string _dntCode;
        [Column("dns_dnt_code", false, false, false)]
        public string DntCode { get { return this._dntCode; } set { this.__Set(ref this._dntCode, value); } }
		
        private System.Nullable<int> _sdmId;
        [Column("dns_sdm_id", false, false, true)]
        public System.Nullable<int> SdmId { get { return this._sdmId; } set { this.__Set(ref this._sdmId, value); } }
	
		
        [LeftJoinColumn]
        public Domain Sdm { get; set; }
		
        [InnerJoinColumn]
        public Domain Domain { get; set; }
	}

	[Table("tbl_domain_dom", "", "evoconcept")]
	public partial class Domain : Entity<Domain>
	{
		static Domain()
		{
			Join<Alias>(t => t.AliasDomain, (t, f) => t.Id == f.DomainId); // Reverse Relation
			Join<Dnsrecord>(t => t.DnsrecordSdm, (t, f) => t.Id == f.SdmId); // Reverse Relation
			Join<Dnsrecord>(t => t.DnsrecordDomain, (t, f) => t.Id == f.DomainId); // Reverse Relation
			Join<Domain>(t => t.ParentDomain, (t, f) => t.ParentDomainId == f.Id); // Relation
			Join<Domain>(t => t.DomainParentDomain, (t, f) => t.Id == f.ParentDomainId); // Reverse Relation
			Join<Service>(t => t.SecondaryService, (t, f) => t.SecondaryServiceId == f.Id); // Relation
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
			Join<Service>(t => t.PrimaryService, (t, f) => t.PrimaryServiceId == f.Id); // Relation
			Join<Email>(t => t.EmailDomain, (t, f) => t.Id == f.DomainId); // Reverse Relation
			Join<Website>(t => t.WebsiteDomain, (t, f) => t.Id == f.DomainId); // Reverse Relation
		}

		
        [Column("dom_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _evoboxId;
        [Column("dom_ebx_id", false, false, true)]
        public System.Nullable<int> EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private string _name;
        [Column("dom_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private System.Nullable<int> _parentDomainId;
        [Column("dom_parent_dom_id", false, false, true)]
        public System.Nullable<int> ParentDomainId { get { return this._parentDomainId; } set { this.__Set(ref this._parentDomainId, value); } }
		
        private sbyte _hostdom;
        [Column("dom_hostdom", false, false, false)]
        public sbyte Hostdom { get { return this._hostdom; } set { this.__Set(ref this._hostdom, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("dom_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("dom_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private System.Nullable<int> _primaryServiceId;
        [Column("dom_primary_svc_id", false, false, true)]
        public System.Nullable<int> PrimaryServiceId { get { return this._primaryServiceId; } set { this.__Set(ref this._primaryServiceId, value); } }
		
        private System.Nullable<int> _secondaryServiceId;
        [Column("dom_secondary_svc_id", false, false, true)]
        public System.Nullable<int> SecondaryServiceId { get { return this._secondaryServiceId; } set { this.__Set(ref this._secondaryServiceId, value); } }
		
        private string _fullname;
        [Column("dom_fullname", false, false, false)]
        public string Fullname { get { return this._fullname; } set { this.__Set(ref this._fullname, value); } }
		
        private System.Nullable<sbyte> _dnsmanaged;
        [Column("dom_dnsmanaged", false, false, true)]
        public System.Nullable<sbyte> Dnsmanaged { get { return this._dnsmanaged; } set { this.__Set(ref this._dnsmanaged, value); } }
		
        private string _status;
        [Column("dom_status", false, false, false)]
        public string Status { get { return this._status; } set { this.__Set(ref this._status, value); } }
		
        private System.Nullable<sbyte> _forceremote;
        [Column("dom_forceremote", false, false, true)]
        public System.Nullable<sbyte> Forceremote { get { return this._forceremote; } set { this.__Set(ref this._forceremote, value); } }
	
		
        [InnerJoinColumn]
        public Alias AliasDomain { get; set; }
		
        [LeftJoinColumn]
        public Dnsrecord DnsrecordSdm { get; set; }
		
        [InnerJoinColumn]
        public Dnsrecord DnsrecordDomain { get; set; }
		
        [LeftJoinColumn]
        public Domain ParentDomain { get; set; }
		
        [LeftJoinColumn]
        public Domain DomainParentDomain { get; set; }
		
        [LeftJoinColumn]
        public Service SecondaryService { get; set; }
		
        [LeftJoinColumn]
        public Evobox Evobox { get; set; }
		
        [LeftJoinColumn]
        public Service PrimaryService { get; set; }
		
        [InnerJoinColumn]
        public Email EmailDomain { get; set; }
		
        [InnerJoinColumn]
        public Website WebsiteDomain { get; set; }
	}

	[Table("tbl_email_eml", "", "evoconcept")]
	public partial class Email : Entity<Email>
	{
		static Email()
		{
			Join<Evobox>(t => t.DerivedEvobox, (t, f) => t.DerivedEvoboxId == f.Id); // Relation
			Join<Domain>(t => t.Domain, (t, f) => t.DomainId == f.Id); // Relation
		}

		
        [Column("eml_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _domainId;
        [Column("eml_dom_id", false, false, false)]
        public int DomainId { get { return this._domainId; } set { this.__Set(ref this._domainId, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("eml_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("eml_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private string _homeDir;
        [Column("eml_home_dir", false, false, false)]
        public string HomeDir { get { return this._homeDir; } set { this.__Set(ref this._homeDir, value); } }
		
        private sbyte _catchAll;
        [Column("eml_catch_all", false, false, false)]
        public sbyte CatchAll { get { return this._catchAll; } set { this.__Set(ref this._catchAll, value); } }
		
        private System.Nullable<sbyte> _away;
        [Column("eml_away", false, false, true)]
        public System.Nullable<sbyte> Away { get { return this._away; } set { this.__Set(ref this._away, value); } }
		
        private string _message;
        [Column("eml_message", false, false, true)]
        public string Message { get { return this._message; } set { this.__Set(ref this._message, value); } }
		
        private string _password;
        [Column("eml_password", false, false, false)]
        public string Password { get { return this._password; } set { this.__Set(ref this._password, value); } }
		
        private int _quotaUsage;
        [Column("eml_quota_usage", false, false, false)]
        public int QuotaUsage { get { return this._quotaUsage; } set { this.__Set(ref this._quotaUsage, value); } }
		
        private string _localPart;
        [Column("eml_local_part", false, false, false)]
        public string LocalPart { get { return this._localPart; } set { this.__Set(ref this._localPart, value); } }
		
        private int _derivedEvoboxId;
        [Column("eml_derived_ebx_id", false, false, false)]
        public int DerivedEvoboxId { get { return this._derivedEvoboxId; } set { this.__Set(ref this._derivedEvoboxId, value); } }
	
		
        [InnerJoinColumn]
        public Evobox DerivedEvobox { get; set; }
		
        [InnerJoinColumn]
        public Domain Domain { get; set; }
	}

	[Table("tbl_evobox_ebx", "", "evoconcept")]
	public partial class Evobox : Entity<Evobox>
	{
		static Evobox()
		{
			Join<Alias>(t => t.AliasDerivedEvobox, (t, f) => t.Id == f.DerivedEvoboxId); // Reverse Relation
			Join<Application>(t => t.ApplicationEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
			Join<Cron>(t => t.CronEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
			Join<Databaselogin>(t => t.DatabaseloginEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
			Join<Database>(t => t.DatabaseEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
			Join<Domain>(t => t.DomainEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
			Join<Email>(t => t.EmailDerivedEvobox, (t, f) => t.Id == f.DerivedEvoboxId); // Reverse Relation
			Join<Service>(t => t.StatsService, (t, f) => t.StatsServiceId == f.Id); // Relation
			Join<Offer>(t => t.Offer, (t, f) => t.OfferId == f.Id); // Relation
			Join<Ftpaccount>(t => t.FtpaccountEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
			Join<Transaction>(t => t.TransactionEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
			Join<UsrEbx>(t => t.UsrEbxEvobox, (t, f) => t.Id == f.EvoboxId); // Reverse Relation
		}

		
        [Column("ebx_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _name;
        [Column("ebx_name", false, false, false)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private System.Nullable<System.DateTime> _startdate;
        [Column("ebx_startdate", false, false, true)]
        public System.Nullable<System.DateTime> Startdate { get { return this._startdate; } set { this.__Set(ref this._startdate, value); } }
		
        private System.Nullable<System.DateTime> _enddate;
        [Column("ebx_enddate", false, false, true)]
        public System.Nullable<System.DateTime> Enddate { get { return this._enddate; } set { this.__Set(ref this._enddate, value); } }
		
        private int _offerId;
        [Column("ebx_ofr_id", false, false, false)]
        public int OfferId { get { return this._offerId; } set { this.__Set(ref this._offerId, value); } }
		
        private int _quota;
        [Column("ebx_quota", false, false, false)]
        public int Quota { get { return this._quota; } set { this.__Set(ref this._quota, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("ebx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("ebx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private System.Nullable<int> _statsServiceId;
        [Column("ebx_stats_service_id", false, false, true)]
        public System.Nullable<int> StatsServiceId { get { return this._statsServiceId; } set { this.__Set(ref this._statsServiceId, value); } }
		
        private string _homedir;
        [Column("ebx_homedir", false, false, true)]
        public string Homedir { get { return this._homedir; } set { this.__Set(ref this._homedir, value); } }
		
        private string _status;
        [Column("ebx_status", false, false, false)]
        public string Status { get { return this._status; } set { this.__Set(ref this._status, value); } }
		
        private System.Nullable<int> _nfsServiceId;
        [Column("ebx_nfs_service_id", false, false, true)]
        public System.Nullable<int> NfsServiceId { get { return this._nfsServiceId; } set { this.__Set(ref this._nfsServiceId, value); } }
	
		
        [InnerJoinColumn]
        public Alias AliasDerivedEvobox { get; set; }
		
        [InnerJoinColumn]
        public Application ApplicationEvobox { get; set; }
		
        [InnerJoinColumn]
        public Cron CronEvobox { get; set; }
		
        [InnerJoinColumn]
        public Databaselogin DatabaseloginEvobox { get; set; }
		
        [InnerJoinColumn]
        public Database DatabaseEvobox { get; set; }
		
        [LeftJoinColumn]
        public Domain DomainEvobox { get; set; }
		
        [InnerJoinColumn]
        public Email EmailDerivedEvobox { get; set; }
		
        [LeftJoinColumn]
        public Service StatsService { get; set; }
		
        [InnerJoinColumn]
        public Offer Offer { get; set; }
		
        [InnerJoinColumn]
        public Ftpaccount FtpaccountEvobox { get; set; }
		
        [LeftJoinColumn]
        public Transaction TransactionEvobox { get; set; }
		
        [InnerJoinColumn]
        public UsrEbx UsrEbxEvobox { get; set; }
	}

	[Table("tbl_faqcategory_fqc", "", "evoconcept")]
	public partial class Faqcategory : Entity<Faqcategory>
	{
		static Faqcategory()
		{
			Join<Faq>(t => t.FaqFaqcategory, (t, f) => t.Id == f.FaqcategoryId); // Reverse Relation
		}

		
        [Column("fqc_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _title;
        [Column("fqc_title", false, false, false)]
        public string Title { get { return this._title; } set { this.__Set(ref this._title, value); } }
		
        private int _order;
        [Column("fqc_order", false, false, false)]
        public int Order { get { return this._order; } set { this.__Set(ref this._order, value); } }
	
		
        [InnerJoinColumn]
        public Faq FaqFaqcategory { get; set; }
	}

	[Table("tbl_faq_faq", "", "evoconcept")]
	public partial class Faq : Entity<Faq>
	{
		static Faq()
		{
			Join<Faqcategory>(t => t.Faqcategory, (t, f) => t.FaqcategoryId == f.Id); // Relation
		}

		
        [Column("faq_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _faqcategoryId;
        [Column("faq_fqc_id", false, false, false)]
        public int FaqcategoryId { get { return this._faqcategoryId; } set { this.__Set(ref this._faqcategoryId, value); } }
		
        private string _question;
        [Column("faq_question", false, false, false)]
        public string Question { get { return this._question; } set { this.__Set(ref this._question, value); } }
		
        private string _answer;
        [Column("faq_answer", false, false, false)]
        public string Answer { get { return this._answer; } set { this.__Set(ref this._answer, value); } }
		
        private int _order;
        [Column("faq_order", false, false, false)]
        public int Order { get { return this._order; } set { this.__Set(ref this._order, value); } }
	
		
        [InnerJoinColumn]
        public Faqcategory Faqcategory { get; set; }
	}

	[Table("tbl_ftpaccount_ftp", "", "evoconcept")]
	public partial class Ftpaccount : Entity<Ftpaccount>
	{
		static Ftpaccount()
		{
			Join<Service>(t => t.Service, (t, f) => t.ServiceId == f.Id); // Relation
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
		}

		
        [Column("ftp_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _evoboxId;
        [Column("ftp_ebx_id", false, false, false)]
        public int EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private string _login;
        [Column("ftp_login", false, false, false)]
        public string Login { get { return this._login; } set { this.__Set(ref this._login, value); } }
		
        private string _password;
        [Column("ftp_password", false, false, false)]
        public string Password { get { return this._password; } set { this.__Set(ref this._password, value); } }
		
        private string _homedir;
        [Column("ftp_homedir", false, false, false)]
        public string Homedir { get { return this._homedir; } set { this.__Set(ref this._homedir, value); } }
		
        private int _count;
        [Column("ftp_count", false, false, false)]
        public int Count { get { return this._count; } set { this.__Set(ref this._count, value); } }
		
        private sbyte _enabled;
        [Column("ftp_enabled", false, false, false)]
        public sbyte Enabled { get { return this._enabled; } set { this.__Set(ref this._enabled, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("ftp_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("ftp_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private System.Nullable<System.DateTime> _accessed;
        [Column("ftp_accessed", false, false, true)]
        public System.Nullable<System.DateTime> Accessed { get { return this._accessed; } set { this.__Set(ref this._accessed, value); } }
		
        private System.Nullable<System.DateTime> _modified;
        [Column("ftp_modified", false, false, true)]
        public System.Nullable<System.DateTime> Modified { get { return this._modified; } set { this.__Set(ref this._modified, value); } }
		
        private int _serviceId;
        [Column("ftp_svc_id", false, false, false)]
        public int ServiceId { get { return this._serviceId; } set { this.__Set(ref this._serviceId, value); } }
	
		
        [InnerJoinColumn]
        public Service Service { get; set; }
		
        [InnerJoinColumn]
        public Evobox Evobox { get; set; }
	}

	[Table("tbl_news_nws", "", "evoconcept")]
	public partial class News : Entity<News>
	{
		static News()
		{
		}

		
        [Column("nws_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _title;
        [Column("nws_title", false, false, false)]
        public string Title { get { return this._title; } set { this.__Set(ref this._title, value); } }
		
        private string _content;
        [Column("nws_content", false, false, false)]
        public string Content { get { return this._content; } set { this.__Set(ref this._content, value); } }
		
        private string _author;
        [Column("nws_author", false, false, false)]
        public string Author { get { return this._author; } set { this.__Set(ref this._author, value); } }
		
        private System.Nullable<System.DateTime> _date;
        [Column("nws_date", false, false, true)]
        public System.Nullable<System.DateTime> Date { get { return this._date; } set { this.__Set(ref this._date, value); } }
	
	}

	[Table("tbl_ofr_spv_osv", "", "evoconcept")]
	public partial class OfrSpv : Entity<OfrSpv>
	{
		static OfrSpv()
		{
			Join<Specificationvalue>(t => t.Specificationvalue, (t, f) => t.SpecificationvalueId == f.Id); // Relation
			Join<Offer>(t => t.Offer, (t, f) => t.OfferId == f.Id); // Relation
		}

		
        [Column("osv_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _offerId;
        [Column("osv_ofr_id", false, false, false)]
        public int OfferId { get { return this._offerId; } set { this.__Set(ref this._offerId, value); } }
		
        private int _specificationvalueId;
        [Column("osv_spv_id", false, false, false)]
        public int SpecificationvalueId { get { return this._specificationvalueId; } set { this.__Set(ref this._specificationvalueId, value); } }
	
		
        [InnerJoinColumn]
        public Specificationvalue Specificationvalue { get; set; }
		
        [InnerJoinColumn]
        public Offer Offer { get; set; }
	}

	[Table("tbl_order_ord", "", "evoconcept")]
	public partial class Order : Entity<Order>
	{
		static Order()
		{
			Join<Transaction>(t => t.Transaction, (t, f) => t.TransactionId == f.Id); // Relation
		}

		
        [Column("ord_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _transactionId;
        [Column("ord_tsn_id", false, false, false)]
        public int TransactionId { get { return this._transactionId; } set { this.__Set(ref this._transactionId, value); } }
	
		
        [InnerJoinColumn]
        public Transaction Transaction { get; set; }
	}

	[Table("tbl_serverip_sip", "", "evoconcept")]
	public partial class Serverip : Entity<Serverip>
	{
		static Serverip()
		{
			Join<Ipaddress>(t => t.Ipaddress, (t, f) => t.IpaddressId == f.Id); // Relation
			Join<Server>(t => t.Server, (t, f) => t.ServerId == f.Id); // Relation
		}

		
        [Column("sip_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _serverId;
        [Column("sip_srv_id", false, false, false)]
        public int ServerId { get { return this._serverId; } set { this.__Set(ref this._serverId, value); } }
		
        private int _ipaddressId;
        [Column("sip_ipa_id", false, false, false)]
        public int IpaddressId { get { return this._ipaddressId; } set { this.__Set(ref this._ipaddressId, value); } }
		
        private int _status;
        [Column("sip_status", false, false, false)]
        public int Status { get { return this._status; } set { this.__Set(ref this._status, value); } }
	
		
        [InnerJoinColumn]
        public Ipaddress Ipaddress { get; set; }
		
        [InnerJoinColumn]
        public Server Server { get; set; }
	}

	[Table("tbl_server_srv", "", "evoconcept")]
	public partial class Server : Entity<Server>
	{
		static Server()
		{
			Join<Serverip>(t => t.ServeripServer, (t, f) => t.Id == f.ServerId); // Reverse Relation
			Join<Service>(t => t.ServiceServer, (t, f) => t.Id == f.ServerId); // Reverse Relation
			Join<SrvSrv>(t => t.SrvSrvGuest, (t, f) => t.Id == f.GuestId); // Reverse Relation
			Join<SrvSrv>(t => t.SrvSrvHost, (t, f) => t.Id == f.HostId); // Reverse Relation
		}

		
        [Column("srv_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _name;
        [Column("srv_name", false, false, true)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private int _status;
        [Column("srv_status", false, false, false)]
        public int Status { get { return this._status; } set { this.__Set(ref this._status, value); } }
		
        private string _type;
        [Column("srv_type", false, false, false)]
        public string Type { get { return this._type; } set { this.__Set(ref this._type, value); } }
	
		
        [InnerJoinColumn]
        public Serverip ServeripServer { get; set; }
		
        [InnerJoinColumn]
        public Service ServiceServer { get; set; }
		
        [InnerJoinColumn]
        public SrvSrv SrvSrvGuest { get; set; }
		
        [InnerJoinColumn]
        public SrvSrv SrvSrvHost { get; set; }
	}

	[Table("tbl_serviceattribute_sva", "", "evoconcept")]
	public partial class Serviceattribute : Entity<Serviceattribute>
	{
		static Serviceattribute()
		{
			Join<Service>(t => t.Service, (t, f) => t.ServiceId == f.Id); // Relation
		}

		
        [Column("sva_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _key;
        [Column("sva_key", false, false, false)]
        public string Key { get { return this._key; } set { this.__Set(ref this._key, value); } }
		
        private string _strvalue;
        [Column("sva_strvalue", false, false, true)]
        public string Strvalue { get { return this._strvalue; } set { this.__Set(ref this._strvalue, value); } }
		
        private System.Nullable<int> _intvalue;
        [Column("sva_intvalue", false, false, true)]
        public System.Nullable<int> Intvalue { get { return this._intvalue; } set { this.__Set(ref this._intvalue, value); } }
		
        private object _dblvalue;
        [Column("sva_dblvalue", false, false, true)]
        public object Dblvalue { get { return this._dblvalue; } set { this.__Set(ref this._dblvalue, value); } }
		
        private System.Nullable<System.DateTime> _dtvalue;
        [Column("sva_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> Dtvalue { get { return this._dtvalue; } set { this.__Set(ref this._dtvalue, value); } }
		
        private int _serviceId;
        [Column("sva_svc_id", false, false, false)]
        public int ServiceId { get { return this._serviceId; } set { this.__Set(ref this._serviceId, value); } }
	
		
        [InnerJoinColumn]
        public Service Service { get; set; }
	}

	[Table("tbl_serviceinvoker_sin", "", "evoconcept")]
	public partial class Serviceinvoker : Entity<Serviceinvoker>
	{
		static Serviceinvoker()
		{
			Join<Service>(t => t.Svc2, (t, f) => t.Svc2Id == f.Id); // Relation
			Join<Service>(t => t.Svc1, (t, f) => t.Svc1Id == f.Id); // Relation
		}

		
        [Column("sin_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _svc1Id;
        [Column("sin_svc1_id", false, false, false)]
        public int Svc1Id { get { return this._svc1Id; } set { this.__Set(ref this._svc1Id, value); } }
		
        private int _svc2Id;
        [Column("sin_svc2_id", false, false, false)]
        public int Svc2Id { get { return this._svc2Id; } set { this.__Set(ref this._svc2Id, value); } }
	
		
        [InnerJoinColumn]
        public Service Svc2 { get; set; }
		
        [InnerJoinColumn]
        public Service Svc1 { get; set; }
	}

	[Table("tbl_service_svc", "", "evoconcept")]
	public partial class Service : Entity<Service>
	{
		static Service()
		{
			Join<Application>(t => t.ApplicationService, (t, f) => t.Id == f.ServiceId); // Reverse Relation
			Join<Cron>(t => t.CronService, (t, f) => t.Id == f.ServiceId); // Reverse Relation
			Join<Databaselogin>(t => t.DatabaseloginService, (t, f) => t.Id == f.ServiceId); // Reverse Relation
			Join<Database>(t => t.DatabaseService, (t, f) => t.Id == f.ServiceId); // Reverse Relation
			Join<Domain>(t => t.DomainSecondaryService, (t, f) => t.Id == f.SecondaryServiceId); // Reverse Relation
			Join<Domain>(t => t.DomainPrimaryService, (t, f) => t.Id == f.PrimaryServiceId); // Reverse Relation
			Join<Evobox>(t => t.EvoboxStatsService, (t, f) => t.Id == f.StatsServiceId); // Reverse Relation
			Join<Ftpaccount>(t => t.FtpaccountService, (t, f) => t.Id == f.ServiceId); // Reverse Relation
			Join<Serviceattribute>(t => t.ServiceattributeService, (t, f) => t.Id == f.ServiceId); // Reverse Relation
			Join<Serviceinvoker>(t => t.ServiceinvokerSvc2, (t, f) => t.Id == f.Svc2Id); // Reverse Relation
			Join<Serviceinvoker>(t => t.ServiceinvokerSvc1, (t, f) => t.Id == f.Svc1Id); // Reverse Relation
			Join<Server>(t => t.Server, (t, f) => t.ServerId == f.Id); // Relation
		}

		
        [Column("svc_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _name;
        [Column("svc_name", false, false, true)]
        public string Name { get { return this._name; } set { this.__Set(ref this._name, value); } }
		
        private string _description;
        [Column("svc_description", false, false, true)]
        public string Description { get { return this._description; } set { this.__Set(ref this._description, value); } }
		
        private int _serverId;
        [Column("svc_srv_id", false, false, false)]
        public int ServerId { get { return this._serverId; } set { this.__Set(ref this._serverId, value); } }
		
        private string _svtCode;
        [Column("svc_svt_code", false, false, false)]
        public string SvtCode { get { return this._svtCode; } set { this.__Set(ref this._svtCode, value); } }
		
        private System.Nullable<int> _capacity;
        [Column("svc_capacity", false, false, true)]
        public System.Nullable<int> Capacity { get { return this._capacity; } set { this.__Set(ref this._capacity, value); } }
		
        private string _domain;
        [Column("svc_domain", false, false, true)]
        public string Domain { get { return this._domain; } set { this.__Set(ref this._domain, value); } }
		
        private System.Nullable<int> _backupServiceId;
        [Column("svc_backup_svc_id", false, false, true)]
        public System.Nullable<int> BackupServiceId { get { return this._backupServiceId; } set { this.__Set(ref this._backupServiceId, value); } }
	
		
        [InnerJoinColumn]
        public Application ApplicationService { get; set; }
		
        [InnerJoinColumn]
        public Cron CronService { get; set; }
		
        [InnerJoinColumn]
        public Databaselogin DatabaseloginService { get; set; }
		
        [InnerJoinColumn]
        public Database DatabaseService { get; set; }
		
        [LeftJoinColumn]
        public Domain DomainSecondaryService { get; set; }
		
        [LeftJoinColumn]
        public Domain DomainPrimaryService { get; set; }
		
        [LeftJoinColumn]
        public Evobox EvoboxStatsService { get; set; }
		
        [InnerJoinColumn]
        public Ftpaccount FtpaccountService { get; set; }
		
        [InnerJoinColumn]
        public Serviceattribute ServiceattributeService { get; set; }
		
        [InnerJoinColumn]
        public Serviceinvoker ServiceinvokerSvc2 { get; set; }
		
        [InnerJoinColumn]
        public Serviceinvoker ServiceinvokerSvc1 { get; set; }
		
        [InnerJoinColumn]
        public Server Server { get; set; }
	}

	[Table("tbl_srv_srv_ssv", "", "evoconcept")]
	public partial class SrvSrv : Entity<SrvSrv>
	{
		static SrvSrv()
		{
			Join<Server>(t => t.Guest, (t, f) => t.GuestId == f.Id); // Relation
			Join<Server>(t => t.Host, (t, f) => t.HostId == f.Id); // Relation
		}

		
        private int _hostId;
        [Column("ssv_host_id", true, false, false)]
        public int HostId { get { return this._hostId; } set { this.__Set(ref this._hostId, value); } }
		
        private int _guestId;
        [Column("ssv_guest_id", true, false, false)]
        public int GuestId { get { return this._guestId; } set { this.__Set(ref this._guestId, value); } }
		
        private sbyte _active;
        [Column("ssv_active", false, false, false)]
        public sbyte Active { get { return this._active; } set { this.__Set(ref this._active, value); } }
	
		
        [InnerJoinColumn]
        public Server Guest { get; set; }
		
        [InnerJoinColumn]
        public Server Host { get; set; }
	}

	[Table("tbl_taskattribute_tka", "", "evoconcept")]
	public partial class Taskattribute : Entity<Taskattribute>
	{
		static Taskattribute()
		{
			Join<Task>(t => t.Task, (t, f) => t.TaskId == f.Id); // Relation
		}

		
        [Column("tka_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _key;
        [Column("tka_key", false, false, false)]
        public string Key { get { return this._key; } set { this.__Set(ref this._key, value); } }
		
        private string _strvalue;
        [Column("tka_strvalue", false, false, true)]
        public string Strvalue { get { return this._strvalue; } set { this.__Set(ref this._strvalue, value); } }
		
        private System.Nullable<int> _intvalue;
        [Column("tka_intvalue", false, false, true)]
        public System.Nullable<int> Intvalue { get { return this._intvalue; } set { this.__Set(ref this._intvalue, value); } }
		
        private object _dblvalue;
        [Column("tka_dblvalue", false, false, true)]
        public object Dblvalue { get { return this._dblvalue; } set { this.__Set(ref this._dblvalue, value); } }
		
        private System.Nullable<System.DateTime> _dtvalue;
        [Column("tka_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> Dtvalue { get { return this._dtvalue; } set { this.__Set(ref this._dtvalue, value); } }
		
        private int _taskId;
        [Column("tka_tsk_id", false, false, false)]
        public int TaskId { get { return this._taskId; } set { this.__Set(ref this._taskId, value); } }
	
		
        [InnerJoinColumn]
        public Task Task { get; set; }
	}

	[Table("tbl_task_tsk", "", "evoconcept")]
	public partial class Task : Entity<Task>
	{
		static Task()
		{
			Join<Taskattribute>(t => t.TaskattributeTask, (t, f) => t.Id == f.TaskId); // Reverse Relation
		}

		
        [Column("tsk_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _type;
        [Column("tsk_type", false, false, false)]
        public string Type { get { return this._type; } set { this.__Set(ref this._type, value); } }
		
        private string _refType;
        [Column("tsk_ref_type", false, false, true)]
        public string RefType { get { return this._refType; } set { this.__Set(ref this._refType, value); } }
		
        private System.Nullable<int> _refId;
        [Column("tsk_ref_id", false, false, true)]
        public System.Nullable<int> RefId { get { return this._refId; } set { this.__Set(ref this._refId, value); } }
		
        private string _status;
        [Column("tsk_status", false, false, false)]
        public string Status { get { return this._status; } set { this.__Set(ref this._status, value); } }
		
        private int _percentage;
        [Column("tsk_percentage", false, false, false)]
        public int Percentage { get { return this._percentage; } set { this.__Set(ref this._percentage, value); } }
		
        private System.Nullable<int> _serviceId;
        [Column("tsk_svc_id", false, false, true)]
        public System.Nullable<int> ServiceId { get { return this._serviceId; } set { this.__Set(ref this._serviceId, value); } }
		
        private string _error;
        [Column("tsk_error", false, false, true)]
        public string Error { get { return this._error; } set { this.__Set(ref this._error, value); } }
		
        private System.Nullable<System.DateTime> _createdAt;
        [Column("tsk_created_at", false, false, true)]
        public System.Nullable<System.DateTime> CreatedAt { get { return this._createdAt; } set { this.__Set(ref this._createdAt, value); } }
		
        private System.Nullable<System.DateTime> _executedAt;
        [Column("tsk_executed_at", false, false, true)]
        public System.Nullable<System.DateTime> ExecutedAt { get { return this._executedAt; } set { this.__Set(ref this._executedAt, value); } }
		
        private System.Nullable<System.DateTime> _finishedAt;
        [Column("tsk_finished_at", false, false, true)]
        public System.Nullable<System.DateTime> FinishedAt { get { return this._finishedAt; } set { this.__Set(ref this._finishedAt, value); } }
	
		
        [InnerJoinColumn]
        public Taskattribute TaskattributeTask { get; set; }
	}

	[Table("tbl_transactionattribute_tsa", "", "evoconcept")]
	public partial class Transactionattribute : Entity<Transactionattribute>
	{
		static Transactionattribute()
		{
			Join<Transaction>(t => t.Transaction, (t, f) => t.TransactionId == f.Id); // Relation
		}

		
        [Column("tsa_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _transactionId;
        [Column("tsa_tsn_id", false, false, false)]
        public int TransactionId { get { return this._transactionId; } set { this.__Set(ref this._transactionId, value); } }
		
        private string _key;
        [Column("tsa_key", false, false, false)]
        public string Key { get { return this._key; } set { this.__Set(ref this._key, value); } }
		
        private string _value;
        [Column("tsa_value", false, false, false)]
        public string Value { get { return this._value; } set { this.__Set(ref this._value, value); } }
	
		
        [InnerJoinColumn]
        public Transaction Transaction { get; set; }
	}

	[Table("tbl_transaction_tsn", "", "evoconcept")]
	public partial class Transaction : Entity<Transaction>
	{
		static Transaction()
		{
			Join<Bill>(t => t.BillTransaction, (t, f) => t.Id == f.TransactionId); // Reverse Relation
			Join<Order>(t => t.OrderTransaction, (t, f) => t.Id == f.TransactionId); // Reverse Relation
			Join<Transactionattribute>(t => t.TransactionattributeTransaction, (t, f) => t.Id == f.TransactionId); // Reverse Relation
			Join<User>(t => t.User, (t, f) => t.UserId == f.Id); // Relation
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
		}

		
        [Column("tsn_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _evoboxId;
        [Column("tsn_ebx_id", false, false, true)]
        public System.Nullable<int> EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private int _userId;
        [Column("tsn_usr_id", false, false, false)]
        public int UserId { get { return this._userId; } set { this.__Set(ref this._userId, value); } }
		
        private decimal _price;
        [Column("tsn_price", false, false, false)]
        public decimal Price { get { return this._price; } set { this.__Set(ref this._price, value); } }
		
        private string _status;
        [Column("tsn_status", false, false, false)]
        public string Status { get { return this._status; } set { this.__Set(ref this._status, value); } }
		
        private System.Nullable<decimal> _amountPayed;
        [Column("tsn_amount_payed", false, false, true)]
        public System.Nullable<decimal> AmountPayed { get { return this._amountPayed; } set { this.__Set(ref this._amountPayed, value); } }
		
        private string _usage;
        [Column("tsn_usage", false, false, false)]
        public string Usage { get { return this._usage; } set { this.__Set(ref this._usage, value); } }
		
        private string _description;
        [Column("tsn_description", false, false, true)]
        public string Description { get { return this._description; } set { this.__Set(ref this._description, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("tsn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private string _pmmCode;
        [Column("tsn_pmm_code", false, false, false)]
        public string PmmCode { get { return this._pmmCode; } set { this.__Set(ref this._pmmCode, value); } }
	
		
        [InnerJoinColumn]
        public Bill BillTransaction { get; set; }
		
        [InnerJoinColumn]
        public Order OrderTransaction { get; set; }
		
        [InnerJoinColumn]
        public Transactionattribute TransactionattributeTransaction { get; set; }
		
        [InnerJoinColumn]
        public User User { get; set; }
		
        [LeftJoinColumn]
        public Evobox Evobox { get; set; }
	}

	[Table("tbl_useractivationkey_uak", "", "evoconcept")]
	public partial class Useractivationkey : Entity<Useractivationkey>
	{
		static Useractivationkey()
		{
			Join<User>(t => t.UserUseractivationkey, (t, f) => t.Id == f.UseractivationkeyId); // Reverse Relation
		}

		
        [Column("uak_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _activationKey;
        [Column("uak_activation_key", false, false, true)]
        public string ActivationKey { get { return this._activationKey; } set { this.__Set(ref this._activationKey, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("uak_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
	
		
        [LeftJoinColumn]
        public User UserUseractivationkey { get; set; }
	}

	[Table("tbl_userinformation_uin", "", "evoconcept")]
	public partial class Userinformation : Entity<Userinformation>
	{
		static Userinformation()
		{
			Join<Country>(t => t.Country, (t, f) => t.CountryCode == f.Code); // Relation
			Join<User>(t => t.User, (t, f) => t.UserId == f.Id); // Relation
		}

		
        private int _userId;
        [Column("uin_usr_id", true, false, false)]
        public int UserId { get { return this._userId; } set { this.__Set(ref this._userId, value); } }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("uin_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("uin_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private string _lastname;
        [Column("uin_lastname", false, false, false)]
        public string Lastname { get { return this._lastname; } set { this.__Set(ref this._lastname, value); } }
		
        private string _firstname;
        [Column("uin_firstname", false, false, false)]
        public string Firstname { get { return this._firstname; } set { this.__Set(ref this._firstname, value); } }
		
        private string _city;
        [Column("uin_city", false, false, false)]
        public string City { get { return this._city; } set { this.__Set(ref this._city, value); } }
		
        private string _countryCode;
        [Column("uin_cny_code", false, false, true)]
        public string CountryCode { get { return this._countryCode; } set { this.__Set(ref this._countryCode, value); } }
		
        private string _postCode;
        [Column("uin_post_code", false, false, false)]
        public string PostCode { get { return this._postCode; } set { this.__Set(ref this._postCode, value); } }
		
        private string _address1;
        [Column("uin_address1", false, false, false)]
        public string Address1 { get { return this._address1; } set { this.__Set(ref this._address1, value); } }
		
        private string _address2;
        [Column("uin_address2", false, false, true)]
        public string Address2 { get { return this._address2; } set { this.__Set(ref this._address2, value); } }
		
        private string _address3;
        [Column("uin_address3", false, false, true)]
        public string Address3 { get { return this._address3; } set { this.__Set(ref this._address3, value); } }
		
        private string _address4;
        [Column("uin_address4", false, false, true)]
        public string Address4 { get { return this._address4; } set { this.__Set(ref this._address4, value); } }
	
		
        [LeftJoinColumn]
        public Country Country { get; set; }
		
        [InnerJoinColumn]
        public User User { get; set; }
	}

	[Table("tbl_user_usr", "", "evoconcept")]
	public partial class User : Entity<User>
	{
		static User()
		{
			Join<Transaction>(t => t.TransactionUser, (t, f) => t.Id == f.UserId); // Reverse Relation
			Join<Userinformation>(t => t.UserinformationUser, (t, f) => t.Id == f.UserId); // Reverse Relation
			Join<Useractivationkey>(t => t.Useractivationkey, (t, f) => t.UseractivationkeyId == f.Id); // Relation
			Join<UsrEbx>(t => t.UsrEbxUser, (t, f) => t.Id == f.UserId); // Reverse Relation
		}

		
        [Column("usr_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("usr_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("usr_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private string _password;
        [Column("usr_password", false, false, false)]
        public string Password { get { return this._password; } set { this.__Set(ref this._password, value); } }
		
        private string _email;
        [Column("usr_email", false, false, false)]
        public string Email { get { return this._email; } set { this.__Set(ref this._email, value); } }
		
        private string _login;
        [Column("usr_login", false, false, true)]
        public string Login { get { return this._login; } set { this.__Set(ref this._login, value); } }
		
        private sbyte _locked;
        [Column("usr_locked", false, false, false)]
        public sbyte Locked { get { return this._locked; } set { this.__Set(ref this._locked, value); } }
		
        private sbyte _enabled;
        [Column("usr_enabled", false, false, false)]
        public sbyte Enabled { get { return this._enabled; } set { this.__Set(ref this._enabled, value); } }
		
        private sbyte _admin;
        [Column("usr_admin", false, false, false)]
        public sbyte Admin { get { return this._admin; } set { this.__Set(ref this._admin, value); } }
		
        private int _credit;
        [Column("usr_credit", false, false, false)]
        public int Credit { get { return this._credit; } set { this.__Set(ref this._credit, value); } }
		
        private System.Nullable<int> _useractivationkeyId;
        [Column("usr_uak_id", false, false, true)]
        public System.Nullable<int> UseractivationkeyId { get { return this._useractivationkeyId; } set { this.__Set(ref this._useractivationkeyId, value); } }
	
		
        [InnerJoinColumn]
        public Transaction TransactionUser { get; set; }
		
        [InnerJoinColumn]
        public Userinformation UserinformationUser { get; set; }
		
        [LeftJoinColumn]
        public Useractivationkey Useractivationkey { get; set; }
		
        [InnerJoinColumn]
        public UsrEbx UsrEbxUser { get; set; }
	}

	[Table("tbl_usr_ebx_ubx", "", "evoconcept")]
	public partial class UsrEbx : Entity<UsrEbx>
	{
		static UsrEbx()
		{
			Join<Conditions>(t => t.ConditionsUsrEbx, (t, f) => t.Id == f.UsrEbxId); // Reverse Relation
			Join<User>(t => t.User, (t, f) => t.UserId == f.Id); // Relation
			Join<Evobox>(t => t.Evobox, (t, f) => t.EvoboxId == f.Id); // Relation
		}

		
        [Column("ubx_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("ubx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("ubx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private int _evoboxId;
        [Column("ubx_ebx_id", false, false, false)]
        public int EvoboxId { get { return this._evoboxId; } set { this.__Set(ref this._evoboxId, value); } }
		
        private int _userId;
        [Column("ubx_usr_id", false, false, false)]
        public int UserId { get { return this._userId; } set { this.__Set(ref this._userId, value); } }
		
        private System.Nullable<sbyte> _owner;
        [Column("ubx_owner", false, false, true)]
        public System.Nullable<sbyte> Owner { get { return this._owner; } set { this.__Set(ref this._owner, value); } }
	
		
        [LeftJoinColumn]
        public Conditions ConditionsUsrEbx { get; set; }
		
        [InnerJoinColumn]
        public User User { get; set; }
		
        [InnerJoinColumn]
        public Evobox Evobox { get; set; }
	}

	[Table("tbl_website_web", "", "evoconcept")]
	public partial class Website : Entity<Website>
	{
		static Website()
		{
			Join<Domain>(t => t.Domain, (t, f) => t.DomainId == f.Id); // Relation
			Join<Application>(t => t.Application, (t, f) => t.ApplicationId == f.Id); // Relation
		}

		
        [Column("web_id", true, true, false)]
        public int Id
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<System.DateTime> _createdate;
        [Column("web_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get { return this._createdate; } set { this.__Set(ref this._createdate, value); } }
		
        private System.Nullable<System.DateTime> _updatedate;
        [Column("web_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get { return this._updatedate; } set { this.__Set(ref this._updatedate, value); } }
		
        private int _domainId;
        [Column("web_dom_id", false, false, false)]
        public int DomainId { get { return this._domainId; } set { this.__Set(ref this._domainId, value); } }
		
        private int _applicationId;
        [Column("web_app_id", false, false, false)]
        public int ApplicationId { get { return this._applicationId; } set { this.__Set(ref this._applicationId, value); } }
	
		
        [InnerJoinColumn]
        public Domain Domain { get; set; }
		
        [InnerJoinColumn]
        public Application Application { get; set; }
	}


}

