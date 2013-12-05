using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace Sandbox.Entities.Evoconcept
{
	[Table("lut_country_cny", "", "evoconcept")]
	public partial class Country : Entity<Country>
	{
		static Country()
		{
			Join<Userinformation>(t => t.UserinformationCountry, (t, f) => t.Code == f.CountryCode); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("cny_code", true, false, false)]
        public string Code { get; set; }
		
        [NotifyPropertyChanged, Column("cny_name", false, false, false)]
        public string Name { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("ipa_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ipa_ip", false, false, false)]
        public string Ip { get; set; }
		
        [NotifyPropertyChanged, Column("ipa_ipt_code", false, false, false)]
        public string IptCode { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("ofr_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ofr_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_price", false, false, false)]
        public decimal Price { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_locked", false, false, false)]
        public sbyte Locked { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_trial_period", false, false, false)]
        public int TrialPeriod { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_type", false, false, false)]
        public string Type { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("red_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("red_ofr_id", false, false, true)]
        public System.Nullable<int> OfferId { get; set; }
		
        [NotifyPropertyChanged, Column("red_quantity", false, false, false)]
        public int Quantity { get; set; }
		
        [NotifyPropertyChanged, Column("red_percentage", false, false, false)]
        public int Percentage { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("scc_code", true, false, false)]
        public string Code { get; set; }
		
        [NotifyPropertyChanged, Column("scc_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("scc_order", false, false, false)]
        public int Order { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("spv_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("spv_value", false, false, false)]
        public int Value { get; set; }
		
        [NotifyPropertyChanged, Column("spv_custom", false, false, false)]
        public sbyte Custom { get; set; }
		
        [NotifyPropertyChanged, Column("spv_spc_code", false, false, false)]
        public string SpecificationCode { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("spc_code", true, false, false)]
        public string Code { get; set; }
		
        [NotifyPropertyChanged, Column("spc_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("spc_period", false, false, false)]
        public int Period { get; set; }
		
        [NotifyPropertyChanged, Column("spc_description", false, false, false)]
        public string Description { get; set; }
		
        [NotifyPropertyChanged, Column("spc_unit", false, false, true)]
        public string Unit { get; set; }
		
        [NotifyPropertyChanged, Column("spc_scc_code", false, false, false)]
        public string SpecificationcategoryCode { get; set; }
		
        [NotifyPropertyChanged, Column("spc_show_0", false, false, false)]
        public sbyte Show0 { get; set; }
		
        [NotifyPropertyChanged, Column("spc_add_s", false, false, false)]
        public sbyte AddS { get; set; }
		
        [NotifyPropertyChanged, Column("spc_order", false, false, false)]
        public int Order { get; set; }
		
        [NotifyPropertyChanged, Column("spc_image", false, false, false)]
        public sbyte Image { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("als_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("als_dom_id", false, false, false)]
        public int DomainId { get; set; }
		
        [NotifyPropertyChanged, Column("als_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("als_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("als_local_part", false, false, false)]
        public string LocalPart { get; set; }
		
        [NotifyPropertyChanged, Column("als_rcpt", false, false, false)]
        public string Rcpt { get; set; }
		
        [NotifyPropertyChanged, Column("als_derived_ebx_id", false, false, false)]
        public int DerivedEvoboxId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("apa_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("apa_app_id", false, false, false)]
        public int ApplicationId { get; set; }
		
        [NotifyPropertyChanged, Column("apa_value", false, false, false)]
        public string Value { get; set; }
		
        [NotifyPropertyChanged, Column("apa_aat_code", false, false, false)]
        public string AatCode { get; set; }
	
		
        [InnerJoinColumn]
        public Application Application { get; set; }
	}

	[Table("tbl_applicationip_aip", "", "evoconcept")]
	public partial class Applicationip : Entity<Applicationip>
	{
		static Applicationip()
		{
		}

		
        [NotifyPropertyChanged, Column("aip_ip", false, false, false)]
        public string Ip { get; set; }
		
        [NotifyPropertyChanged, Column("aip_app_id", false, false, true)]
        public System.Nullable<int> ApplicationId { get; set; }
	
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

		
        [NotifyPropertyChanged, Column("app_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("app_ebx_id", false, false, false)]
        public int EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("app_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("app_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("app_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("app_svc_id", false, false, false)]
        public int ServiceId { get; set; }
		
        [NotifyPropertyChanged, Column("app_apt_code", false, false, false)]
        public string AptCode { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("bll_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("bll_tsn_id", false, false, false)]
        public int TransactionId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("cgv_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("cgv_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("cgv_date", false, false, true)]
        public System.Nullable<System.DateTime> Date { get; set; }
		
        [NotifyPropertyChanged, Column("cgv_ubx_id", false, false, true)]
        public System.Nullable<int> UsrEbxId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("crn_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("crn_ebx_id", false, false, false)]
        public int EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("crn_url", false, false, false)]
        public string Url { get; set; }
		
        [NotifyPropertyChanged, Column("crn_month", false, false, false)]
        public int Month { get; set; }
		
        [NotifyPropertyChanged, Column("crn_day", false, false, false)]
        public int Day { get; set; }
		
        [NotifyPropertyChanged, Column("crn_hour", false, false, false)]
        public int Hour { get; set; }
		
        [NotifyPropertyChanged, Column("crn_minute", false, false, false)]
        public long Minute { get; set; }
		
        [NotifyPropertyChanged, Column("crn_email_notification", false, false, true)]
        public string EmailNotification { get; set; }
		
        [NotifyPropertyChanged, Column("crn_last_executed", false, false, true)]
        public System.Nullable<System.DateTime> LastExecuted { get; set; }
		
        [NotifyPropertyChanged, Column("crn_last_execution_time", false, false, true)]
        public System.Nullable<int> LastExecutionTime { get; set; }
		
        [NotifyPropertyChanged, Column("crn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("crn_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("crn_svc_id", false, false, false)]
        public int ServiceId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("dbl_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dbl_login", false, false, false)]
        public string Login { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_ebx_id", false, false, false)]
        public int EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_svc_id", false, false, false)]
        public int ServiceId { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_password", false, false, true)]
        public string Password { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("dtb_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dtb_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_ebx_id", false, false, false)]
        public int EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_svc_id", false, false, false)]
        public int ServiceId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("dns_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dns_dom_id", false, false, false)]
        public int DomainId { get; set; }
		
        [NotifyPropertyChanged, Column("dns_name", false, false, true)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("dns_priority", false, false, true)]
        public System.Nullable<int> Priority { get; set; }
		
        [NotifyPropertyChanged, Column("dns_to", false, false, false)]
        public string To { get; set; }
		
        [NotifyPropertyChanged, Column("dns_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("dns_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dns_dnt_code", false, false, false)]
        public string DntCode { get; set; }
		
        [NotifyPropertyChanged, Column("dns_sdm_id", false, false, true)]
        public System.Nullable<int> SdmId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("dom_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dom_ebx_id", false, false, true)]
        public System.Nullable<int> EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("dom_parent_dom_id", false, false, true)]
        public System.Nullable<int> ParentDomainId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_hostdom", false, false, false)]
        public sbyte Hostdom { get; set; }
		
        [NotifyPropertyChanged, Column("dom_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("dom_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dom_primary_svc_id", false, false, true)]
        public System.Nullable<int> PrimaryServiceId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_secondary_svc_id", false, false, true)]
        public System.Nullable<int> SecondaryServiceId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_fullname", false, false, false)]
        public string Fullname { get; set; }
		
        [NotifyPropertyChanged, Column("dom_dnsmanaged", false, false, true)]
        public System.Nullable<sbyte> Dnsmanaged { get; set; }
		
        [NotifyPropertyChanged, Column("dom_status", false, false, false)]
        public string Status { get; set; }
		
        [NotifyPropertyChanged, Column("dom_forceremote", false, false, true)]
        public System.Nullable<sbyte> Forceremote { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("eml_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("eml_dom_id", false, false, false)]
        public int DomainId { get; set; }
		
        [NotifyPropertyChanged, Column("eml_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("eml_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("eml_home_dir", false, false, false)]
        public string HomeDir { get; set; }
		
        [NotifyPropertyChanged, Column("eml_catch_all", false, false, false)]
        public sbyte CatchAll { get; set; }
		
        [NotifyPropertyChanged, Column("eml_away", false, false, true)]
        public System.Nullable<sbyte> Away { get; set; }
		
        [NotifyPropertyChanged, Column("eml_message", false, false, true)]
        public string Message { get; set; }
		
        [NotifyPropertyChanged, Column("eml_password", false, false, false)]
        public string Password { get; set; }
		
        [NotifyPropertyChanged, Column("eml_quota_usage", false, false, false)]
        public int QuotaUsage { get; set; }
		
        [NotifyPropertyChanged, Column("eml_local_part", false, false, false)]
        public string LocalPart { get; set; }
		
        [NotifyPropertyChanged, Column("eml_derived_ebx_id", false, false, false)]
        public int DerivedEvoboxId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("ebx_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ebx_name", false, false, false)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_startdate", false, false, true)]
        public System.Nullable<System.DateTime> Startdate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_enddate", false, false, true)]
        public System.Nullable<System.DateTime> Enddate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_ofr_id", false, false, false)]
        public int OfferId { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_quota", false, false, false)]
        public int Quota { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_stats_service_id", false, false, true)]
        public System.Nullable<int> StatsServiceId { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_homedir", false, false, true)]
        public string Homedir { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_status", false, false, false)]
        public string Status { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_nfs_service_id", false, false, true)]
        public System.Nullable<int> NfsServiceId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("fqc_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("fqc_title", false, false, false)]
        public string Title { get; set; }
		
        [NotifyPropertyChanged, Column("fqc_order", false, false, false)]
        public int Order { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("faq_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("faq_fqc_id", false, false, false)]
        public int FaqcategoryId { get; set; }
		
        [NotifyPropertyChanged, Column("faq_question", false, false, false)]
        public string Question { get; set; }
		
        [NotifyPropertyChanged, Column("faq_answer", false, false, false)]
        public string Answer { get; set; }
		
        [NotifyPropertyChanged, Column("faq_order", false, false, false)]
        public int Order { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("ftp_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ftp_ebx_id", false, false, false)]
        public int EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_login", false, false, false)]
        public string Login { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_password", false, false, false)]
        public string Password { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_homedir", false, false, false)]
        public string Homedir { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_count", false, false, false)]
        public int Count { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_enabled", false, false, false)]
        public sbyte Enabled { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_accessed", false, false, true)]
        public System.Nullable<System.DateTime> Accessed { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_modified", false, false, true)]
        public System.Nullable<System.DateTime> Modified { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_svc_id", false, false, false)]
        public int ServiceId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("nws_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("nws_title", false, false, false)]
        public string Title { get; set; }
		
        [NotifyPropertyChanged, Column("nws_content", false, false, false)]
        public string Content { get; set; }
		
        [NotifyPropertyChanged, Column("nws_author", false, false, false)]
        public string Author { get; set; }
		
        [NotifyPropertyChanged, Column("nws_date", false, false, true)]
        public System.Nullable<System.DateTime> Date { get; set; }
	
	}

	[Table("tbl_ofr_spv_osv", "", "evoconcept")]
	public partial class OfrSpv : Entity<OfrSpv>
	{
		static OfrSpv()
		{
			Join<Specificationvalue>(t => t.Specificationvalue, (t, f) => t.SpecificationvalueId == f.Id); // Relation
			Join<Offer>(t => t.Offer, (t, f) => t.OfferId == f.Id); // Relation
		}

		
        [NotifyPropertyChanged, Column("osv_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("osv_ofr_id", false, false, false)]
        public int OfferId { get; set; }
		
        [NotifyPropertyChanged, Column("osv_spv_id", false, false, false)]
        public int SpecificationvalueId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("ord_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ord_tsn_id", false, false, false)]
        public int TransactionId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("sip_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("sip_srv_id", false, false, false)]
        public int ServerId { get; set; }
		
        [NotifyPropertyChanged, Column("sip_ipa_id", false, false, false)]
        public int IpaddressId { get; set; }
		
        [NotifyPropertyChanged, Column("sip_status", false, false, false)]
        public int Status { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("srv_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("srv_name", false, false, true)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("srv_status", false, false, false)]
        public int Status { get; set; }
		
        [NotifyPropertyChanged, Column("srv_type", false, false, false)]
        public string Type { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("sva_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("sva_key", false, false, false)]
        public string Key { get; set; }
		
        [NotifyPropertyChanged, Column("sva_strvalue", false, false, true)]
        public string Strvalue { get; set; }
		
        [NotifyPropertyChanged, Column("sva_intvalue", false, false, true)]
        public System.Nullable<int> Intvalue { get; set; }
		
		
        [NotifyPropertyChanged, Column("sva_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> Dtvalue { get; set; }
		
        [NotifyPropertyChanged, Column("sva_svc_id", false, false, false)]
        public int ServiceId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("sin_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("sin_svc1_id", false, false, false)]
        public int Svc1Id { get; set; }
		
        [NotifyPropertyChanged, Column("sin_svc2_id", false, false, false)]
        public int Svc2Id { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("svc_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("svc_name", false, false, true)]
        public string Name { get; set; }
		
        [NotifyPropertyChanged, Column("svc_description", false, false, true)]
        public string Description { get; set; }
		
        [NotifyPropertyChanged, Column("svc_srv_id", false, false, false)]
        public int ServerId { get; set; }
		
        [NotifyPropertyChanged, Column("svc_svt_code", false, false, false)]
        public string SvtCode { get; set; }
		
        [NotifyPropertyChanged, Column("svc_capacity", false, false, true)]
        public System.Nullable<int> Capacity { get; set; }
		
        [NotifyPropertyChanged, Column("svc_domain", false, false, true)]
        public string Domain { get; set; }
		
        [NotifyPropertyChanged, Column("svc_backup_svc_id", false, false, true)]
        public System.Nullable<int> BackupServiceId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("ssv_host_id", true, false, false)]
        public int HostId { get; set; }
		
        [NotifyPropertyChanged, Column("ssv_guest_id", true, false, false)]
        public int GuestId { get; set; }
		
        [NotifyPropertyChanged, Column("ssv_active", false, false, false)]
        public sbyte Active { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("tka_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tka_key", false, false, false)]
        public string Key { get; set; }
		
        [NotifyPropertyChanged, Column("tka_strvalue", false, false, true)]
        public string Strvalue { get; set; }
		
        [NotifyPropertyChanged, Column("tka_intvalue", false, false, true)]
        public System.Nullable<int> Intvalue { get; set; }
		
		
        [NotifyPropertyChanged, Column("tka_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> Dtvalue { get; set; }
		
        [NotifyPropertyChanged, Column("tka_tsk_id", false, false, false)]
        public int TaskId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("tsk_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tsk_type", false, false, false)]
        public string Type { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_ref_type", false, false, true)]
        public string RefType { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_ref_id", false, false, true)]
        public System.Nullable<int> RefId { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_status", false, false, false)]
        public string Status { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_percentage", false, false, false)]
        public int Percentage { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_svc_id", false, false, true)]
        public System.Nullable<int> ServiceId { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_error", false, false, true)]
        public string Error { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_created_at", false, false, true)]
        public System.Nullable<System.DateTime> CreatedAt { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_executed_at", false, false, true)]
        public System.Nullable<System.DateTime> ExecutedAt { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_finished_at", false, false, true)]
        public System.Nullable<System.DateTime> FinishedAt { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("tsa_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tsa_tsn_id", false, false, false)]
        public int TransactionId { get; set; }
		
        [NotifyPropertyChanged, Column("tsa_key", false, false, false)]
        public string Key { get; set; }
		
        [NotifyPropertyChanged, Column("tsa_value", false, false, false)]
        public string Value { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("tsn_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tsn_ebx_id", false, false, true)]
        public System.Nullable<int> EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_usr_id", false, false, false)]
        public int UserId { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_price", false, false, false)]
        public decimal Price { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_status", false, false, false)]
        public string Status { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_amount_payed", false, false, true)]
        public System.Nullable<decimal> AmountPayed { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_usage", false, false, false)]
        public string Usage { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_description", false, false, true)]
        public string Description { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_pmm_code", false, false, false)]
        public string PmmCode { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("uak_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("uak_activation_key", false, false, true)]
        public string ActivationKey { get; set; }
		
        [NotifyPropertyChanged, Column("uak_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("uin_usr_id", true, false, false)]
        public int UserId { get; set; }
		
        [NotifyPropertyChanged, Column("uin_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("uin_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("uin_lastname", false, false, false)]
        public string Lastname { get; set; }
		
        [NotifyPropertyChanged, Column("uin_firstname", false, false, false)]
        public string Firstname { get; set; }
		
        [NotifyPropertyChanged, Column("uin_city", false, false, false)]
        public string City { get; set; }
		
        [NotifyPropertyChanged, Column("uin_cny_code", false, false, true)]
        public string CountryCode { get; set; }
		
        [NotifyPropertyChanged, Column("uin_post_code", false, false, false)]
        public string PostCode { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address1", false, false, false)]
        public string Address1 { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address2", false, false, true)]
        public string Address2 { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address3", false, false, true)]
        public string Address3 { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address4", false, false, true)]
        public string Address4 { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("usr_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("usr_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("usr_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("usr_password", false, false, false)]
        public string Password { get; set; }
		
        [NotifyPropertyChanged, Column("usr_email", false, false, false)]
        public string Email { get; set; }
		
        [NotifyPropertyChanged, Column("usr_login", false, false, true)]
        public string Login { get; set; }
		
        [NotifyPropertyChanged, Column("usr_locked", false, false, false)]
        public sbyte Locked { get; set; }
		
        [NotifyPropertyChanged, Column("usr_enabled", false, false, false)]
        public sbyte Enabled { get; set; }
		
        [NotifyPropertyChanged, Column("usr_admin", false, false, false)]
        public sbyte Admin { get; set; }
		
        [NotifyPropertyChanged, Column("usr_credit", false, false, false)]
        public int Credit { get; set; }
		
        [NotifyPropertyChanged, Column("usr_uak_id", false, false, true)]
        public System.Nullable<int> UseractivationkeyId { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("ubx_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ubx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_ebx_id", false, false, false)]
        public int EvoboxId { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_usr_id", false, false, false)]
        public int UserId { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_owner", false, false, true)]
        public System.Nullable<sbyte> Owner { get; set; }
	
		
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

		
        [NotifyPropertyChanged, Column("web_id", true, true, false)]
        public int Id
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("web_createdate", false, false, true)]
        public System.Nullable<System.DateTime> Createdate { get; set; }
		
        [NotifyPropertyChanged, Column("web_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> Updatedate { get; set; }
		
        [NotifyPropertyChanged, Column("web_dom_id", false, false, false)]
        public int DomainId { get; set; }
		
        [NotifyPropertyChanged, Column("web_app_id", false, false, false)]
        public int ApplicationId { get; set; }
	
		
        [InnerJoinColumn]
        public Domain Domain { get; set; }
		
        [InnerJoinColumn]
        public Application Application { get; set; }
	}


}

