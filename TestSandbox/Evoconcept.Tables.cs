


using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace TestSandbox.Evoconcept
{
	[Table("lut_country_cny", "", "evoconcept")]
	public partial class LutCountryCny : Entity<LutCountryCny>
	{
		static LutCountryCny()
		{
			Join<TblUserinformationUin>(t => t.UinCnyTblUserinformationUin, (t, f) => t.CnyCode == f.UinCnyCode); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("cny_code", true, false, false)]
        public string CnyCode { get; set; }
		
        [NotifyPropertyChanged, Column("cny_name", false, false, false)]
        public string CnyName { get; set; }
	
		
        [LeftJoinColumn]
        public TblUserinformationUin UinCnyTblUserinformationUin { get; set; }
	}

	[Table("lut_ipaddress_ipa", "", "evoconcept")]
	public partial class LutIpaddressIpa : Entity<LutIpaddressIpa>
	{
		static LutIpaddressIpa()
		{
			Join<TblServeripSip>(t => t.SipIpaTblServeripSip, (t, f) => t.IpaId == f.SipIpaId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ipa_id", true, true, false)]
        public int IpaId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ipa_ip", false, false, false)]
        public string IpaIp { get; set; }
		
        [NotifyPropertyChanged, Column("ipa_ipt_code", false, false, false)]
        public string IpaIptCode { get; set; }
	
		
        [InnerJoinColumn]
        public TblServeripSip SipIpaTblServeripSip { get; set; }
	}

	[Table("lut_offer_ofr", "", "evoconcept")]
	public partial class LutOfferOfr : Entity<LutOfferOfr>
	{
		static LutOfferOfr()
		{
			Join<LutReductionRed>(t => t.RedOfrLutReductionRed, (t, f) => t.OfrId == f.RedOfrId); // Reverse Relation
			Join<TblEvoboxEbx>(t => t.EbxOfrTblEvoboxEbx, (t, f) => t.OfrId == f.EbxOfrId); // Reverse Relation
			Join<TblOfrSpvOsv>(t => t.OsvOfrTblOfrSpvOsv, (t, f) => t.OfrId == f.OsvOfrId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ofr_id", true, true, false)]
        public int OfrId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ofr_name", false, false, false)]
        public string OfrName { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_price", false, false, false)]
        public decimal OfrPrice { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_locked", false, false, false)]
        public sbyte OfrLocked { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_trial_period", false, false, false)]
        public int OfrTrialPeriod { get; set; }
		
        [NotifyPropertyChanged, Column("ofr_type", false, false, false)]
        public string OfrType { get; set; }
	
		
        [LeftJoinColumn]
        public LutReductionRed RedOfrLutReductionRed { get; set; }
		
        [InnerJoinColumn]
        public TblEvoboxEbx EbxOfrTblEvoboxEbx { get; set; }
		
        [InnerJoinColumn]
        public TblOfrSpvOsv OsvOfrTblOfrSpvOsv { get; set; }
	}

	[Table("lut_reduction_red", "", "evoconcept")]
	public partial class LutReductionRed : Entity<LutReductionRed>
	{
		static LutReductionRed()
		{
			Join<LutOfferOfr>(t => t.RedOfr, (t, f) => t.RedOfrId == f.OfrId); // Relation
		}

		
        [NotifyPropertyChanged, Column("red_id", true, true, false)]
        public int RedId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("red_ofr_id", false, false, true)]
        public System.Nullable<int> RedOfrId { get; set; }
		
        [NotifyPropertyChanged, Column("red_quantity", false, false, false)]
        public int RedQuantity { get; set; }
		
        [NotifyPropertyChanged, Column("red_percentage", false, false, false)]
        public int RedPercentage { get; set; }
	
		
        [LeftJoinColumn]
        public LutOfferOfr RedOfr { get; set; }
	}

	[Table("lut_specificationcategory_scc", "", "evoconcept")]
	public partial class LutSpecificationcategoryScc : Entity<LutSpecificationcategoryScc>
	{
		static LutSpecificationcategoryScc()
		{
			Join<LutSpecificationSpc>(t => t.SpcSccLutSpecificationSpc, (t, f) => t.SccCode == f.SpcSccCode); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("scc_code", true, false, false)]
        public string SccCode { get; set; }
		
        [NotifyPropertyChanged, Column("scc_name", false, false, false)]
        public string SccName { get; set; }
		
        [NotifyPropertyChanged, Column("scc_order", false, false, false)]
        public int SccOrder { get; set; }
	
		
        [InnerJoinColumn]
        public LutSpecificationSpc SpcSccLutSpecificationSpc { get; set; }
	}

	[Table("lut_specificationvalue_spv", "", "evoconcept")]
	public partial class LutSpecificationvalueSpv : Entity<LutSpecificationvalueSpv>
	{
		static LutSpecificationvalueSpv()
		{
			Join<LutSpecificationSpc>(t => t.SpvSpc, (t, f) => t.SpvSpcCode == f.SpcCode); // Relation
			Join<TblOfrSpvOsv>(t => t.OsvSpvTblOfrSpvOsv, (t, f) => t.SpvId == f.OsvSpvId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("spv_id", true, true, false)]
        public int SpvId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("spv_value", false, false, false)]
        public int SpvValue { get; set; }
		
        [NotifyPropertyChanged, Column("spv_custom", false, false, false)]
        public sbyte SpvCustom { get; set; }
		
        [NotifyPropertyChanged, Column("spv_spc_code", false, false, false)]
        public string SpvSpcCode { get; set; }
	
		
        [InnerJoinColumn]
        public LutSpecificationSpc SpvSpc { get; set; }
		
        [InnerJoinColumn]
        public TblOfrSpvOsv OsvSpvTblOfrSpvOsv { get; set; }
	}

	[Table("lut_specification_spc", "", "evoconcept")]
	public partial class LutSpecificationSpc : Entity<LutSpecificationSpc>
	{
		static LutSpecificationSpc()
		{
			Join<LutSpecificationvalueSpv>(t => t.SpvSpcLutSpecificationvalueSpv, (t, f) => t.SpcCode == f.SpvSpcCode); // Reverse Relation
			Join<LutSpecificationcategoryScc>(t => t.SpcScc, (t, f) => t.SpcSccCode == f.SccCode); // Relation
		}

		
        [NotifyPropertyChanged, Column("spc_code", true, false, false)]
        public string SpcCode { get; set; }
		
        [NotifyPropertyChanged, Column("spc_name", false, false, false)]
        public string SpcName { get; set; }
		
        [NotifyPropertyChanged, Column("spc_period", false, false, false)]
        public int SpcPeriod { get; set; }
		
        [NotifyPropertyChanged, Column("spc_description", false, false, false)]
        public string SpcDescription { get; set; }
		
        [NotifyPropertyChanged, Column("spc_unit", false, false, true)]
        public string SpcUnit { get; set; }
		
        [NotifyPropertyChanged, Column("spc_scc_code", false, false, false)]
        public string SpcSccCode { get; set; }
		
        [NotifyPropertyChanged, Column("spc_show_0", false, false, false)]
        public sbyte SpcShow0 { get; set; }
		
        [NotifyPropertyChanged, Column("spc_add_s", false, false, false)]
        public sbyte SpcAddS { get; set; }
		
        [NotifyPropertyChanged, Column("spc_order", false, false, false)]
        public int SpcOrder { get; set; }
		
        [NotifyPropertyChanged, Column("spc_image", false, false, false)]
        public sbyte SpcImage { get; set; }
	
		
        [InnerJoinColumn]
        public LutSpecificationvalueSpv SpvSpcLutSpecificationvalueSpv { get; set; }
		
        [InnerJoinColumn]
        public LutSpecificationcategoryScc SpcScc { get; set; }
	}

	[Table("tbl_alias_als", "", "evoconcept")]
	public partial class TblAliasAls : Entity<TblAliasAls>
	{
		static TblAliasAls()
		{
			Join<TblEvoboxEbx>(t => t.AlsDerivedEbx, (t, f) => t.AlsDerivedEbxId == f.EbxId); // Relation
			Join<TblDomainDom>(t => t.AlsDom, (t, f) => t.AlsDomId == f.DomId); // Relation
		}

		
        [NotifyPropertyChanged, Column("als_id", true, true, false)]
        public int AlsId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("als_dom_id", false, false, false)]
        public int AlsDomId { get; set; }
		
        [NotifyPropertyChanged, Column("als_createdate", false, false, true)]
        public System.Nullable<System.DateTime> AlsCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("als_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> AlsUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("als_local_part", false, false, false)]
        public string AlsLocalPart { get; set; }
		
        [NotifyPropertyChanged, Column("als_rcpt", false, false, false)]
        public string AlsRcpt { get; set; }
		
        [NotifyPropertyChanged, Column("als_derived_ebx_id", false, false, false)]
        public int AlsDerivedEbxId { get; set; }
	
		
        [InnerJoinColumn]
        public TblEvoboxEbx AlsDerivedEbx { get; set; }
		
        [InnerJoinColumn]
        public TblDomainDom AlsDom { get; set; }
	}

	[Table("tbl_applicationattribute_apa", "", "evoconcept")]
	public partial class TblApplicationattributeApa : Entity<TblApplicationattributeApa>
	{
		static TblApplicationattributeApa()
		{
			Join<TblApplicationApp>(t => t.ApaApp, (t, f) => t.ApaAppId == f.AppId); // Relation
		}

		
        [NotifyPropertyChanged, Column("apa_id", true, true, false)]
        public int ApaId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("apa_app_id", false, false, false)]
        public int ApaAppId { get; set; }
		
        [NotifyPropertyChanged, Column("apa_value", false, false, false)]
        public string ApaValue { get; set; }
		
        [NotifyPropertyChanged, Column("apa_aat_code", false, false, false)]
        public string ApaAatCode { get; set; }
	
		
        [InnerJoinColumn]
        public TblApplicationApp ApaApp { get; set; }
	}

	[Table("tbl_applicationip_aip", "", "evoconcept")]
	public partial class TblApplicationipAip : Entity<TblApplicationipAip>
	{
		static TblApplicationipAip()
		{
		}

		
        [NotifyPropertyChanged, Column("aip_ip", false, false, false)]
        public string AipIp { get; set; }
		
        [NotifyPropertyChanged, Column("aip_app_id", false, false, true)]
        public System.Nullable<int> AipAppId { get; set; }
	
	}

	[Table("tbl_application_app", "", "evoconcept")]
	public partial class TblApplicationApp : Entity<TblApplicationApp>
	{
		static TblApplicationApp()
		{
			Join<TblApplicationattributeApa>(t => t.ApaAppTblApplicationattributeApa, (t, f) => t.AppId == f.ApaAppId); // Reverse Relation
			Join<TblEvoboxEbx>(t => t.AppEbx, (t, f) => t.AppEbxId == f.EbxId); // Relation
			Join<TblServiceSvc>(t => t.AppSvc, (t, f) => t.AppSvcId == f.SvcId); // Relation
			Join<TblWebsiteWeb>(t => t.WebAppTblWebsiteWeb, (t, f) => t.AppId == f.WebAppId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("app_id", true, true, false)]
        public int AppId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("app_ebx_id", false, false, false)]
        public int AppEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("app_createdate", false, false, true)]
        public System.Nullable<System.DateTime> AppCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("app_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> AppUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("app_name", false, false, false)]
        public string AppName { get; set; }
		
        [NotifyPropertyChanged, Column("app_svc_id", false, false, false)]
        public int AppSvcId { get; set; }
		
        [NotifyPropertyChanged, Column("app_apt_code", false, false, false)]
        public string AppAptCode { get; set; }
	
		
        [InnerJoinColumn]
        public TblApplicationattributeApa ApaAppTblApplicationattributeApa { get; set; }
		
        [InnerJoinColumn]
        public TblEvoboxEbx AppEbx { get; set; }
		
        [InnerJoinColumn]
        public TblServiceSvc AppSvc { get; set; }
		
        [InnerJoinColumn]
        public TblWebsiteWeb WebAppTblWebsiteWeb { get; set; }
	}

	[Table("tbl_bill_bll", "", "evoconcept")]
	public partial class TblBillBll : Entity<TblBillBll>
	{
		static TblBillBll()
		{
			Join<TblTransactionTsn>(t => t.BllTsn, (t, f) => t.BllTsnId == f.TsnId); // Relation
		}

		
        [NotifyPropertyChanged, Column("bll_id", true, true, false)]
        public int BllId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("bll_tsn_id", false, false, false)]
        public int BllTsnId { get; set; }
	
		
        [InnerJoinColumn]
        public TblTransactionTsn BllTsn { get; set; }
	}

	[Table("tbl_conditions_cgv", "", "evoconcept")]
	public partial class TblConditionsCgv : Entity<TblConditionsCgv>
	{
		static TblConditionsCgv()
		{
			Join<TblUsrEbxUbx>(t => t.CgvUbx, (t, f) => t.CgvUbxId == f.UbxId); // Relation
		}

		
        [NotifyPropertyChanged, Column("cgv_id", true, true, false)]
        public int CgvId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("cgv_name", false, false, false)]
        public string CgvName { get; set; }
		
        [NotifyPropertyChanged, Column("cgv_date", false, false, true)]
        public System.Nullable<System.DateTime> CgvDate { get; set; }
		
        [NotifyPropertyChanged, Column("cgv_ubx_id", false, false, true)]
        public System.Nullable<int> CgvUbxId { get; set; }
	
		
        [LeftJoinColumn]
        public TblUsrEbxUbx CgvUbx { get; set; }
	}

	[Table("tbl_cron_crn", "", "evoconcept")]
	public partial class TblCronCrn : Entity<TblCronCrn>
	{
		static TblCronCrn()
		{
			Join<TblServiceSvc>(t => t.CrnSvc, (t, f) => t.CrnSvcId == f.SvcId); // Relation
			Join<TblEvoboxEbx>(t => t.CrnEbx, (t, f) => t.CrnEbxId == f.EbxId); // Relation
		}

		
        [NotifyPropertyChanged, Column("crn_id", true, true, false)]
        public int CrnId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("crn_ebx_id", false, false, false)]
        public int CrnEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("crn_url", false, false, false)]
        public string CrnUrl { get; set; }
		
        [NotifyPropertyChanged, Column("crn_month", false, false, false)]
        public int CrnMonth { get; set; }
		
        [NotifyPropertyChanged, Column("crn_day", false, false, false)]
        public int CrnDay { get; set; }
		
        [NotifyPropertyChanged, Column("crn_hour", false, false, false)]
        public int CrnHour { get; set; }
		
        [NotifyPropertyChanged, Column("crn_minute", false, false, false)]
        public long CrnMinute { get; set; }
		
        [NotifyPropertyChanged, Column("crn_email_notification", false, false, true)]
        public string CrnEmailNotification { get; set; }
		
        [NotifyPropertyChanged, Column("crn_last_executed", false, false, true)]
        public System.Nullable<System.DateTime> CrnLastExecuted { get; set; }
		
        [NotifyPropertyChanged, Column("crn_last_execution_time", false, false, true)]
        public System.Nullable<int> CrnLastExecutionTime { get; set; }
		
        [NotifyPropertyChanged, Column("crn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> CrnCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("crn_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> CrnUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("crn_svc_id", false, false, false)]
        public int CrnSvcId { get; set; }
	
		
        [InnerJoinColumn]
        public TblServiceSvc CrnSvc { get; set; }
		
        [InnerJoinColumn]
        public TblEvoboxEbx CrnEbx { get; set; }
	}

	[Table("tbl_databaselogin_dbl", "", "evoconcept")]
	public partial class TblDatabaseloginDbl : Entity<TblDatabaseloginDbl>
	{
		static TblDatabaseloginDbl()
		{
			Join<TblEvoboxEbx>(t => t.DblEbx, (t, f) => t.DblEbxId == f.EbxId); // Relation
			Join<TblServiceSvc>(t => t.DblSvc, (t, f) => t.DblSvcId == f.SvcId); // Relation
		}

		
        [NotifyPropertyChanged, Column("dbl_id", true, true, false)]
        public int DblId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dbl_login", false, false, false)]
        public string DblLogin { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_ebx_id", false, false, false)]
        public int DblEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DblCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DblUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_svc_id", false, false, false)]
        public int DblSvcId { get; set; }
		
        [NotifyPropertyChanged, Column("dbl_password", false, false, true)]
        public string DblPassword { get; set; }
	
		
        [InnerJoinColumn]
        public TblEvoboxEbx DblEbx { get; set; }
		
        [InnerJoinColumn]
        public TblServiceSvc DblSvc { get; set; }
	}

	[Table("tbl_database_dtb", "", "evoconcept")]
	public partial class TblDatabaseDtb : Entity<TblDatabaseDtb>
	{
		static TblDatabaseDtb()
		{
			Join<TblEvoboxEbx>(t => t.DtbEbx, (t, f) => t.DtbEbxId == f.EbxId); // Relation
			Join<TblServiceSvc>(t => t.DtbSvc, (t, f) => t.DtbSvcId == f.SvcId); // Relation
		}

		
        [NotifyPropertyChanged, Column("dtb_id", true, true, false)]
        public int DtbId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dtb_name", false, false, false)]
        public string DtbName { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_ebx_id", false, false, false)]
        public int DtbEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DtbCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DtbUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dtb_svc_id", false, false, false)]
        public int DtbSvcId { get; set; }
	
		
        [InnerJoinColumn]
        public TblEvoboxEbx DtbEbx { get; set; }
		
        [InnerJoinColumn]
        public TblServiceSvc DtbSvc { get; set; }
	}

	[Table("tbl_dnsrecord_dns", "", "evoconcept")]
	public partial class TblDnsrecordDns : Entity<TblDnsrecordDns>
	{
		static TblDnsrecordDns()
		{
			Join<TblDomainDom>(t => t.DnsDom, (t, f) => t.DnsDomId == f.DomId); // Relation
			Join<TblDomainDom>(t => t.DnsSdm, (t, f) => t.DnsSdmId == f.DomId); // Relation
		}

		
        [NotifyPropertyChanged, Column("dns_id", true, true, false)]
        public int DnsId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dns_dom_id", false, false, false)]
        public int DnsDomId { get; set; }
		
        [NotifyPropertyChanged, Column("dns_name", false, false, true)]
        public string DnsName { get; set; }
		
        [NotifyPropertyChanged, Column("dns_priority", false, false, true)]
        public System.Nullable<int> DnsPriority { get; set; }
		
        [NotifyPropertyChanged, Column("dns_to", false, false, false)]
        public string DnsTo { get; set; }
		
        [NotifyPropertyChanged, Column("dns_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DnsCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dns_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DnsUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dns_dnt_code", false, false, false)]
        public string DnsDntCode { get; set; }
		
        [NotifyPropertyChanged, Column("dns_sdm_id", false, false, true)]
        public System.Nullable<int> DnsSdmId { get; set; }
	
		
        [InnerJoinColumn]
        public TblDomainDom DnsDom { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DnsSdm { get; set; }
	}

	[Table("tbl_domain_dom", "", "evoconcept")]
	public partial class TblDomainDom : Entity<TblDomainDom>
	{
		static TblDomainDom()
		{
			Join<TblAliasAls>(t => t.AlsDomTblAliasAls, (t, f) => t.DomId == f.AlsDomId); // Reverse Relation
			Join<TblDnsrecordDns>(t => t.DnsDomTblDnsrecordDns, (t, f) => t.DomId == f.DnsDomId); // Reverse Relation
			Join<TblDnsrecordDns>(t => t.DnsSdmTblDnsrecordDns, (t, f) => t.DomId == f.DnsSdmId); // Reverse Relation
			Join<TblServiceSvc>(t => t.DomSecondarySvc, (t, f) => t.DomSecondarySvcId == f.SvcId); // Relation
			Join<TblEvoboxEbx>(t => t.DomEbx, (t, f) => t.DomEbxId == f.EbxId); // Relation
			Join<TblServiceSvc>(t => t.DomPrimarySvc, (t, f) => t.DomPrimarySvcId == f.SvcId); // Relation
			Join<TblDomainDom>(t => t.DomParentDom, (t, f) => t.DomParentDomId == f.DomId); // Relation
			Join<TblDomainDom>(t => t.DomParentDomTblDomainDom, (t, f) => t.DomId == f.DomParentDomId); // Reverse Relation
			Join<TblEmailEml>(t => t.EmlDomTblEmailEml, (t, f) => t.DomId == f.EmlDomId); // Reverse Relation
			Join<TblWebsiteWeb>(t => t.WebDomTblWebsiteWeb, (t, f) => t.DomId == f.WebDomId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("dom_id", true, true, false)]
        public int DomId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("dom_ebx_id", false, false, true)]
        public System.Nullable<int> DomEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_name", false, false, false)]
        public string DomName { get; set; }
		
        [NotifyPropertyChanged, Column("dom_parent_dom_id", false, false, true)]
        public System.Nullable<int> DomParentDomId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_hostdom", false, false, false)]
        public sbyte DomHostdom { get; set; }
		
        [NotifyPropertyChanged, Column("dom_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DomCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dom_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DomUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("dom_primary_svc_id", false, false, true)]
        public System.Nullable<int> DomPrimarySvcId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_secondary_svc_id", false, false, true)]
        public System.Nullable<int> DomSecondarySvcId { get; set; }
		
        [NotifyPropertyChanged, Column("dom_fullname", false, false, false)]
        public string DomFullname { get; set; }
		
        [NotifyPropertyChanged, Column("dom_dnsmanaged", false, false, true)]
        public System.Nullable<sbyte> DomDnsmanaged { get; set; }
		
        [NotifyPropertyChanged, Column("dom_status", false, false, false)]
        public string DomStatus { get; set; }
		
        [NotifyPropertyChanged, Column("dom_forceremote", false, false, true)]
        public System.Nullable<sbyte> DomForceremote { get; set; }
	
		
        [InnerJoinColumn]
        public TblAliasAls AlsDomTblAliasAls { get; set; }
		
        [InnerJoinColumn]
        public TblDnsrecordDns DnsDomTblDnsrecordDns { get; set; }
		
        [LeftJoinColumn]
        public TblDnsrecordDns DnsSdmTblDnsrecordDns { get; set; }
		
        [LeftJoinColumn]
        public TblServiceSvc DomSecondarySvc { get; set; }
		
        [LeftJoinColumn]
        public TblEvoboxEbx DomEbx { get; set; }
		
        [LeftJoinColumn]
        public TblServiceSvc DomPrimarySvc { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DomParentDom { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DomParentDomTblDomainDom { get; set; }
		
        [InnerJoinColumn]
        public TblEmailEml EmlDomTblEmailEml { get; set; }
		
        [InnerJoinColumn]
        public TblWebsiteWeb WebDomTblWebsiteWeb { get; set; }
	}

	[Table("tbl_email_eml", "", "evoconcept")]
	public partial class TblEmailEml : Entity<TblEmailEml>
	{
		static TblEmailEml()
		{
			Join<TblEvoboxEbx>(t => t.EmlDerivedEbx, (t, f) => t.EmlDerivedEbxId == f.EbxId); // Relation
			Join<TblDomainDom>(t => t.EmlDom, (t, f) => t.EmlDomId == f.DomId); // Relation
		}

		
        [NotifyPropertyChanged, Column("eml_id", true, true, false)]
        public int EmlId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("eml_dom_id", false, false, false)]
        public int EmlDomId { get; set; }
		
        [NotifyPropertyChanged, Column("eml_createdate", false, false, true)]
        public System.Nullable<System.DateTime> EmlCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("eml_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> EmlUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("eml_home_dir", false, false, false)]
        public string EmlHomeDir { get; set; }
		
        [NotifyPropertyChanged, Column("eml_catch_all", false, false, false)]
        public sbyte EmlCatchAll { get; set; }
		
        [NotifyPropertyChanged, Column("eml_away", false, false, true)]
        public System.Nullable<sbyte> EmlAway { get; set; }
		
        [NotifyPropertyChanged, Column("eml_message", false, false, true)]
        public string EmlMessage { get; set; }
		
        [NotifyPropertyChanged, Column("eml_password", false, false, false)]
        public string EmlPassword { get; set; }
		
        [NotifyPropertyChanged, Column("eml_quota_usage", false, false, false)]
        public int EmlQuotaUsage { get; set; }
		
        [NotifyPropertyChanged, Column("eml_local_part", false, false, false)]
        public string EmlLocalPart { get; set; }
		
        [NotifyPropertyChanged, Column("eml_derived_ebx_id", false, false, false)]
        public int EmlDerivedEbxId { get; set; }
	
		
        [InnerJoinColumn]
        public TblEvoboxEbx EmlDerivedEbx { get; set; }
		
        [InnerJoinColumn]
        public TblDomainDom EmlDom { get; set; }
	}

	[Table("tbl_evobox_ebx", "", "evoconcept")]
	public partial class TblEvoboxEbx : Entity<TblEvoboxEbx>
	{
		static TblEvoboxEbx()
		{
			Join<TblAliasAls>(t => t.AlsDerivedEbxTblAliasAls, (t, f) => t.EbxId == f.AlsDerivedEbxId); // Reverse Relation
			Join<TblApplicationApp>(t => t.AppEbxTblApplicationApp, (t, f) => t.EbxId == f.AppEbxId); // Reverse Relation
			Join<TblCronCrn>(t => t.CrnEbxTblCronCrn, (t, f) => t.EbxId == f.CrnEbxId); // Reverse Relation
			Join<TblDatabaseloginDbl>(t => t.DblEbxTblDatabaseloginDbl, (t, f) => t.EbxId == f.DblEbxId); // Reverse Relation
			Join<TblDatabaseDtb>(t => t.DtbEbxTblDatabaseDtb, (t, f) => t.EbxId == f.DtbEbxId); // Reverse Relation
			Join<TblDomainDom>(t => t.DomEbxTblDomainDom, (t, f) => t.EbxId == f.DomEbxId); // Reverse Relation
			Join<TblEmailEml>(t => t.EmlDerivedEbxTblEmailEml, (t, f) => t.EbxId == f.EmlDerivedEbxId); // Reverse Relation
			Join<TblServiceSvc>(t => t.EbxStatsService, (t, f) => t.EbxStatsServiceId == f.SvcId); // Relation
			Join<LutOfferOfr>(t => t.EbxOfr, (t, f) => t.EbxOfrId == f.OfrId); // Relation
			Join<TblFtpaccountFtp>(t => t.FtpEbxTblFtpaccountFtp, (t, f) => t.EbxId == f.FtpEbxId); // Reverse Relation
			Join<TblTransactionTsn>(t => t.TsnEbxTblTransactionTsn, (t, f) => t.EbxId == f.TsnEbxId); // Reverse Relation
			Join<TblUsrEbxUbx>(t => t.UbxEbxTblUsrEbxUbx, (t, f) => t.EbxId == f.UbxEbxId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("ebx_id", true, true, false)]
        public int EbxId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ebx_name", false, false, false)]
        public string EbxName { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_startdate", false, false, true)]
        public System.Nullable<System.DateTime> EbxStartdate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_enddate", false, false, true)]
        public System.Nullable<System.DateTime> EbxEnddate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_ofr_id", false, false, false)]
        public int EbxOfrId { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_quota", false, false, false)]
        public int EbxQuota { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> EbxCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> EbxUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_stats_service_id", false, false, true)]
        public System.Nullable<int> EbxStatsServiceId { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_homedir", false, false, true)]
        public string EbxHomedir { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_status", false, false, false)]
        public string EbxStatus { get; set; }
		
        [NotifyPropertyChanged, Column("ebx_nfs_service_id", false, false, true)]
        public System.Nullable<int> EbxNfsServiceId { get; set; }
	
		
        [InnerJoinColumn]
        public TblAliasAls AlsDerivedEbxTblAliasAls { get; set; }
		
        [InnerJoinColumn]
        public TblApplicationApp AppEbxTblApplicationApp { get; set; }
		
        [InnerJoinColumn]
        public TblCronCrn CrnEbxTblCronCrn { get; set; }
		
        [InnerJoinColumn]
        public TblDatabaseloginDbl DblEbxTblDatabaseloginDbl { get; set; }
		
        [InnerJoinColumn]
        public TblDatabaseDtb DtbEbxTblDatabaseDtb { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DomEbxTblDomainDom { get; set; }
		
        [InnerJoinColumn]
        public TblEmailEml EmlDerivedEbxTblEmailEml { get; set; }
		
        [LeftJoinColumn]
        public TblServiceSvc EbxStatsService { get; set; }
		
        [InnerJoinColumn]
        public LutOfferOfr EbxOfr { get; set; }
		
        [InnerJoinColumn]
        public TblFtpaccountFtp FtpEbxTblFtpaccountFtp { get; set; }
		
        [LeftJoinColumn]
        public TblTransactionTsn TsnEbxTblTransactionTsn { get; set; }
		
        [InnerJoinColumn]
        public TblUsrEbxUbx UbxEbxTblUsrEbxUbx { get; set; }
	}

	[Table("tbl_faqcategory_fqc", "", "evoconcept")]
	public partial class TblFaqcategoryFqc : Entity<TblFaqcategoryFqc>
	{
		static TblFaqcategoryFqc()
		{
			Join<TblFaqFaq>(t => t.FaqFqcTblFaqFaq, (t, f) => t.FqcId == f.FaqFqcId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("fqc_id", true, true, false)]
        public int FqcId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("fqc_title", false, false, false)]
        public string FqcTitle { get; set; }
		
        [NotifyPropertyChanged, Column("fqc_order", false, false, false)]
        public int FqcOrder { get; set; }
	
		
        [InnerJoinColumn]
        public TblFaqFaq FaqFqcTblFaqFaq { get; set; }
	}

	[Table("tbl_faq_faq", "", "evoconcept")]
	public partial class TblFaqFaq : Entity<TblFaqFaq>
	{
		static TblFaqFaq()
		{
			Join<TblFaqcategoryFqc>(t => t.FaqFqc, (t, f) => t.FaqFqcId == f.FqcId); // Relation
		}

		
        [NotifyPropertyChanged, Column("faq_id", true, true, false)]
        public int FaqId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("faq_fqc_id", false, false, false)]
        public int FaqFqcId { get; set; }
		
        [NotifyPropertyChanged, Column("faq_question", false, false, false)]
        public string FaqQuestion { get; set; }
		
        [NotifyPropertyChanged, Column("faq_answer", false, false, false)]
        public string FaqAnswer { get; set; }
		
        [NotifyPropertyChanged, Column("faq_order", false, false, false)]
        public int FaqOrder { get; set; }
	
		
        [InnerJoinColumn]
        public TblFaqcategoryFqc FaqFqc { get; set; }
	}

	[Table("tbl_ftpaccount_ftp", "", "evoconcept")]
	public partial class TblFtpaccountFtp : Entity<TblFtpaccountFtp>
	{
		static TblFtpaccountFtp()
		{
			Join<TblServiceSvc>(t => t.FtpSvc, (t, f) => t.FtpSvcId == f.SvcId); // Relation
			Join<TblEvoboxEbx>(t => t.FtpEbx, (t, f) => t.FtpEbxId == f.EbxId); // Relation
		}

		
        [NotifyPropertyChanged, Column("ftp_id", true, true, false)]
        public int FtpId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ftp_ebx_id", false, false, false)]
        public int FtpEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_login", false, false, false)]
        public string FtpLogin { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_password", false, false, false)]
        public string FtpPassword { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_homedir", false, false, false)]
        public string FtpHomedir { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_count", false, false, false)]
        public int FtpCount { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_enabled", false, false, false)]
        public sbyte FtpEnabled { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_createdate", false, false, true)]
        public System.Nullable<System.DateTime> FtpCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> FtpUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_accessed", false, false, true)]
        public System.Nullable<System.DateTime> FtpAccessed { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_modified", false, false, true)]
        public System.Nullable<System.DateTime> FtpModified { get; set; }
		
        [NotifyPropertyChanged, Column("ftp_svc_id", false, false, false)]
        public int FtpSvcId { get; set; }
	
		
        [InnerJoinColumn]
        public TblServiceSvc FtpSvc { get; set; }
		
        [InnerJoinColumn]
        public TblEvoboxEbx FtpEbx { get; set; }
	}

	[Table("tbl_news_nws", "", "evoconcept")]
	public partial class TblNewsNws : Entity<TblNewsNws>
	{
		static TblNewsNws()
		{
		}

		
        [NotifyPropertyChanged, Column("nws_id", true, true, false)]
        public int NwsId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("nws_title", false, false, false)]
        public string NwsTitle { get; set; }
		
        [NotifyPropertyChanged, Column("nws_content", false, false, false)]
        public string NwsContent { get; set; }
		
        [NotifyPropertyChanged, Column("nws_author", false, false, false)]
        public string NwsAuthor { get; set; }
		
        [NotifyPropertyChanged, Column("nws_date", false, false, true)]
        public System.Nullable<System.DateTime> NwsDate { get; set; }
	
	}

	[Table("tbl_ofr_spv_osv", "", "evoconcept")]
	public partial class TblOfrSpvOsv : Entity<TblOfrSpvOsv>
	{
		static TblOfrSpvOsv()
		{
			Join<LutSpecificationvalueSpv>(t => t.OsvSpv, (t, f) => t.OsvSpvId == f.SpvId); // Relation
			Join<LutOfferOfr>(t => t.OsvOfr, (t, f) => t.OsvOfrId == f.OfrId); // Relation
		}

		
        [NotifyPropertyChanged, Column("osv_id", true, true, false)]
        public int OsvId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("osv_ofr_id", false, false, false)]
        public int OsvOfrId { get; set; }
		
        [NotifyPropertyChanged, Column("osv_spv_id", false, false, false)]
        public int OsvSpvId { get; set; }
	
		
        [InnerJoinColumn]
        public LutSpecificationvalueSpv OsvSpv { get; set; }
		
        [InnerJoinColumn]
        public LutOfferOfr OsvOfr { get; set; }
	}

	[Table("tbl_order_ord", "", "evoconcept")]
	public partial class TblOrderOrd : Entity<TblOrderOrd>
	{
		static TblOrderOrd()
		{
			Join<TblTransactionTsn>(t => t.OrdTsn, (t, f) => t.OrdTsnId == f.TsnId); // Relation
		}

		
        [NotifyPropertyChanged, Column("ord_id", true, true, false)]
        public int OrdId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ord_tsn_id", false, false, false)]
        public int OrdTsnId { get; set; }
	
		
        [InnerJoinColumn]
        public TblTransactionTsn OrdTsn { get; set; }
	}

	[Table("tbl_serverip_sip", "", "evoconcept")]
	public partial class TblServeripSip : Entity<TblServeripSip>
	{
		static TblServeripSip()
		{
			Join<TblServerSrv>(t => t.SipSrv, (t, f) => t.SipSrvId == f.SrvId); // Relation
			Join<LutIpaddressIpa>(t => t.SipIpa, (t, f) => t.SipIpaId == f.IpaId); // Relation
		}

		
        [NotifyPropertyChanged, Column("sip_id", true, true, false)]
        public int SipId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("sip_srv_id", false, false, false)]
        public int SipSrvId { get; set; }
		
        [NotifyPropertyChanged, Column("sip_ipa_id", false, false, false)]
        public int SipIpaId { get; set; }
		
        [NotifyPropertyChanged, Column("sip_status", false, false, false)]
        public int SipStatus { get; set; }
	
		
        [InnerJoinColumn]
        public TblServerSrv SipSrv { get; set; }
		
        [InnerJoinColumn]
        public LutIpaddressIpa SipIpa { get; set; }
	}

	[Table("tbl_server_srv", "", "evoconcept")]
	public partial class TblServerSrv : Entity<TblServerSrv>
	{
		static TblServerSrv()
		{
			Join<TblServeripSip>(t => t.SipSrvTblServeripSip, (t, f) => t.SrvId == f.SipSrvId); // Reverse Relation
			Join<TblServiceSvc>(t => t.SvcSrvTblServiceSvc, (t, f) => t.SrvId == f.SvcSrvId); // Reverse Relation
			Join<TblSrvSrvSsv>(t => t.SsvGuestTblSrvSrvSsv, (t, f) => t.SrvId == f.SsvGuestId); // Reverse Relation
			Join<TblSrvSrvSsv>(t => t.SsvHostTblSrvSrvSsv, (t, f) => t.SrvId == f.SsvHostId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("srv_id", true, true, false)]
        public int SrvId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("srv_name", false, false, true)]
        public string SrvName { get; set; }
		
        [NotifyPropertyChanged, Column("srv_status", false, false, false)]
        public int SrvStatus { get; set; }
		
        [NotifyPropertyChanged, Column("srv_type", false, false, false)]
        public string SrvType { get; set; }
	
		
        [InnerJoinColumn]
        public TblServeripSip SipSrvTblServeripSip { get; set; }
		
        [InnerJoinColumn]
        public TblServiceSvc SvcSrvTblServiceSvc { get; set; }
		
        [InnerJoinColumn]
        public TblSrvSrvSsv SsvGuestTblSrvSrvSsv { get; set; }
		
        [InnerJoinColumn]
        public TblSrvSrvSsv SsvHostTblSrvSrvSsv { get; set; }
	}

	[Table("tbl_serviceattribute_sva", "", "evoconcept")]
	public partial class TblServiceattributeSva : Entity<TblServiceattributeSva>
	{
		static TblServiceattributeSva()
		{
			Join<TblServiceSvc>(t => t.SvaSvc, (t, f) => t.SvaSvcId == f.SvcId); // Relation
		}

		
        [NotifyPropertyChanged, Column("sva_id", true, true, false)]
        public int SvaId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("sva_key", false, false, false)]
        public string SvaKey { get; set; }
		
        [NotifyPropertyChanged, Column("sva_strvalue", false, false, true)]
        public string SvaStrvalue { get; set; }
		
        [NotifyPropertyChanged, Column("sva_intvalue", false, false, true)]
        public System.Nullable<int> SvaIntvalue { get; set; }
		
		
        [NotifyPropertyChanged, Column("sva_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> SvaDtvalue { get; set; }
		
        [NotifyPropertyChanged, Column("sva_svc_id", false, false, false)]
        public int SvaSvcId { get; set; }
	
		
        [InnerJoinColumn]
        public TblServiceSvc SvaSvc { get; set; }
	}

	[Table("tbl_serviceinvoker_sin", "", "evoconcept")]
	public partial class TblServiceinvokerSin : Entity<TblServiceinvokerSin>
	{
		static TblServiceinvokerSin()
		{
			Join<TblServiceSvc>(t => t.SinSvc2, (t, f) => t.SinSvc2Id == f.SvcId); // Relation
			Join<TblServiceSvc>(t => t.SinSvc1, (t, f) => t.SinSvc1Id == f.SvcId); // Relation
		}

		
        [NotifyPropertyChanged, Column("sin_id", true, true, false)]
        public int SinId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("sin_svc1_id", false, false, false)]
        public int SinSvc1Id { get; set; }
		
        [NotifyPropertyChanged, Column("sin_svc2_id", false, false, false)]
        public int SinSvc2Id { get; set; }
	
		
        [InnerJoinColumn]
        public TblServiceSvc SinSvc2 { get; set; }
		
        [InnerJoinColumn]
        public TblServiceSvc SinSvc1 { get; set; }
	}

	[Table("tbl_service_svc", "", "evoconcept")]
	public partial class TblServiceSvc : Entity<TblServiceSvc>
	{
		static TblServiceSvc()
		{
			Join<TblApplicationApp>(t => t.AppSvcTblApplicationApp, (t, f) => t.SvcId == f.AppSvcId); // Reverse Relation
			Join<TblCronCrn>(t => t.CrnSvcTblCronCrn, (t, f) => t.SvcId == f.CrnSvcId); // Reverse Relation
			Join<TblDatabaseloginDbl>(t => t.DblSvcTblDatabaseloginDbl, (t, f) => t.SvcId == f.DblSvcId); // Reverse Relation
			Join<TblDatabaseDtb>(t => t.DtbSvcTblDatabaseDtb, (t, f) => t.SvcId == f.DtbSvcId); // Reverse Relation
			Join<TblDomainDom>(t => t.DomSecondarySvcTblDomainDom, (t, f) => t.SvcId == f.DomSecondarySvcId); // Reverse Relation
			Join<TblDomainDom>(t => t.DomPrimarySvcTblDomainDom, (t, f) => t.SvcId == f.DomPrimarySvcId); // Reverse Relation
			Join<TblEvoboxEbx>(t => t.EbxStatsServiceTblEvoboxEbx, (t, f) => t.SvcId == f.EbxStatsServiceId); // Reverse Relation
			Join<TblFtpaccountFtp>(t => t.FtpSvcTblFtpaccountFtp, (t, f) => t.SvcId == f.FtpSvcId); // Reverse Relation
			Join<TblServiceattributeSva>(t => t.SvaSvcTblServiceattributeSva, (t, f) => t.SvcId == f.SvaSvcId); // Reverse Relation
			Join<TblServiceinvokerSin>(t => t.SinSvc2TblServiceinvokerSin, (t, f) => t.SvcId == f.SinSvc2Id); // Reverse Relation
			Join<TblServiceinvokerSin>(t => t.SinSvc1TblServiceinvokerSin, (t, f) => t.SvcId == f.SinSvc1Id); // Reverse Relation
			Join<TblServerSrv>(t => t.SvcSrv, (t, f) => t.SvcSrvId == f.SrvId); // Relation
		}

		
        [NotifyPropertyChanged, Column("svc_id", true, true, false)]
        public int SvcId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("svc_name", false, false, true)]
        public string SvcName { get; set; }
		
        [NotifyPropertyChanged, Column("svc_description", false, false, true)]
        public string SvcDescription { get; set; }
		
        [NotifyPropertyChanged, Column("svc_srv_id", false, false, false)]
        public int SvcSrvId { get; set; }
		
        [NotifyPropertyChanged, Column("svc_svt_code", false, false, false)]
        public string SvcSvtCode { get; set; }
		
        [NotifyPropertyChanged, Column("svc_capacity", false, false, true)]
        public System.Nullable<int> SvcCapacity { get; set; }
		
        [NotifyPropertyChanged, Column("svc_domain", false, false, true)]
        public string SvcDomain { get; set; }
		
        [NotifyPropertyChanged, Column("svc_backup_svc_id", false, false, true)]
        public System.Nullable<int> SvcBackupSvcId { get; set; }
	
		
        [InnerJoinColumn]
        public TblApplicationApp AppSvcTblApplicationApp { get; set; }
		
        [InnerJoinColumn]
        public TblCronCrn CrnSvcTblCronCrn { get; set; }
		
        [InnerJoinColumn]
        public TblDatabaseloginDbl DblSvcTblDatabaseloginDbl { get; set; }
		
        [InnerJoinColumn]
        public TblDatabaseDtb DtbSvcTblDatabaseDtb { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DomSecondarySvcTblDomainDom { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DomPrimarySvcTblDomainDom { get; set; }
		
        [LeftJoinColumn]
        public TblEvoboxEbx EbxStatsServiceTblEvoboxEbx { get; set; }
		
        [InnerJoinColumn]
        public TblFtpaccountFtp FtpSvcTblFtpaccountFtp { get; set; }
		
        [InnerJoinColumn]
        public TblServiceattributeSva SvaSvcTblServiceattributeSva { get; set; }
		
        [InnerJoinColumn]
        public TblServiceinvokerSin SinSvc2TblServiceinvokerSin { get; set; }
		
        [InnerJoinColumn]
        public TblServiceinvokerSin SinSvc1TblServiceinvokerSin { get; set; }
		
        [InnerJoinColumn]
        public TblServerSrv SvcSrv { get; set; }
	}

	[Table("tbl_srv_srv_ssv", "", "evoconcept")]
	public partial class TblSrvSrvSsv : Entity<TblSrvSrvSsv>
	{
		static TblSrvSrvSsv()
		{
			Join<TblServerSrv>(t => t.SsvGuest, (t, f) => t.SsvGuestId == f.SrvId); // Relation
			Join<TblServerSrv>(t => t.SsvHost, (t, f) => t.SsvHostId == f.SrvId); // Relation
		}

		
        [NotifyPropertyChanged, Column("ssv_host_id", true, false, false)]
        public int SsvHostId { get; set; }
		
        [NotifyPropertyChanged, Column("ssv_guest_id", true, false, false)]
        public int SsvGuestId { get; set; }
		
        [NotifyPropertyChanged, Column("ssv_active", false, false, false)]
        public sbyte SsvActive { get; set; }
	
		
        [InnerJoinColumn]
        public TblServerSrv SsvGuest { get; set; }
		
        [InnerJoinColumn]
        public TblServerSrv SsvHost { get; set; }
	}

	[Table("tbl_taskattribute_tka", "", "evoconcept")]
	public partial class TblTaskattributeTka : Entity<TblTaskattributeTka>
	{
		static TblTaskattributeTka()
		{
			Join<TblTaskTsk>(t => t.TkaTsk, (t, f) => t.TkaTskId == f.TskId); // Relation
		}

		
        [NotifyPropertyChanged, Column("tka_id", true, true, false)]
        public int TkaId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tka_key", false, false, false)]
        public string TkaKey { get; set; }
		
        [NotifyPropertyChanged, Column("tka_strvalue", false, false, true)]
        public string TkaStrvalue { get; set; }
		
        [NotifyPropertyChanged, Column("tka_intvalue", false, false, true)]
        public System.Nullable<int> TkaIntvalue { get; set; }
		
		
        [NotifyPropertyChanged, Column("tka_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> TkaDtvalue { get; set; }
		
        [NotifyPropertyChanged, Column("tka_tsk_id", false, false, false)]
        public int TkaTskId { get; set; }
	
		
        [InnerJoinColumn]
        public TblTaskTsk TkaTsk { get; set; }
	}

	[Table("tbl_task_tsk", "", "evoconcept")]
	public partial class TblTaskTsk : Entity<TblTaskTsk>
	{
		static TblTaskTsk()
		{
			Join<TblTaskattributeTka>(t => t.TkaTskTblTaskattributeTka, (t, f) => t.TskId == f.TkaTskId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("tsk_id", true, true, false)]
        public int TskId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tsk_type", false, false, false)]
        public string TskType { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_ref_type", false, false, true)]
        public string TskRefType { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_ref_id", false, false, true)]
        public System.Nullable<int> TskRefId { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_status", false, false, false)]
        public string TskStatus { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_percentage", false, false, false)]
        public int TskPercentage { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_svc_id", false, false, true)]
        public System.Nullable<int> TskSvcId { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_error", false, false, true)]
        public string TskError { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_created_at", false, false, true)]
        public System.Nullable<System.DateTime> TskCreatedAt { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_executed_at", false, false, true)]
        public System.Nullable<System.DateTime> TskExecutedAt { get; set; }
		
        [NotifyPropertyChanged, Column("tsk_finished_at", false, false, true)]
        public System.Nullable<System.DateTime> TskFinishedAt { get; set; }
	
		
        [InnerJoinColumn]
        public TblTaskattributeTka TkaTskTblTaskattributeTka { get; set; }
	}

	[Table("tbl_transactionattribute_tsa", "", "evoconcept")]
	public partial class TblTransactionattributeTsa : Entity<TblTransactionattributeTsa>
	{
		static TblTransactionattributeTsa()
		{
			Join<TblTransactionTsn>(t => t.TsaTsn, (t, f) => t.TsaTsnId == f.TsnId); // Relation
		}

		
        [NotifyPropertyChanged, Column("tsa_id", true, true, false)]
        public int TsaId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tsa_tsn_id", false, false, false)]
        public int TsaTsnId { get; set; }
		
        [NotifyPropertyChanged, Column("tsa_key", false, false, false)]
        public string TsaKey { get; set; }
		
        [NotifyPropertyChanged, Column("tsa_value", false, false, false)]
        public string TsaValue { get; set; }
	
		
        [InnerJoinColumn]
        public TblTransactionTsn TsaTsn { get; set; }
	}

	[Table("tbl_transaction_tsn", "", "evoconcept")]
	public partial class TblTransactionTsn : Entity<TblTransactionTsn>
	{
		static TblTransactionTsn()
		{
			Join<TblBillBll>(t => t.BllTsnTblBillBll, (t, f) => t.TsnId == f.BllTsnId); // Reverse Relation
			Join<TblOrderOrd>(t => t.OrdTsnTblOrderOrd, (t, f) => t.TsnId == f.OrdTsnId); // Reverse Relation
			Join<TblTransactionattributeTsa>(t => t.TsaTsnTblTransactionattributeTsa, (t, f) => t.TsnId == f.TsaTsnId); // Reverse Relation
			Join<TblEvoboxEbx>(t => t.TsnEbx, (t, f) => t.TsnEbxId == f.EbxId); // Relation
			Join<TblUserUsr>(t => t.TsnUsr, (t, f) => t.TsnUsrId == f.UsrId); // Relation
		}

		
        [NotifyPropertyChanged, Column("tsn_id", true, true, false)]
        public int TsnId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("tsn_ebx_id", false, false, true)]
        public System.Nullable<int> TsnEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_usr_id", false, false, false)]
        public int TsnUsrId { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_price", false, false, false)]
        public decimal TsnPrice { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_status", false, false, false)]
        public string TsnStatus { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_amount_payed", false, false, true)]
        public System.Nullable<decimal> TsnAmountPayed { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_usage", false, false, false)]
        public string TsnUsage { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_description", false, false, true)]
        public string TsnDescription { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> TsnCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("tsn_pmm_code", false, false, false)]
        public string TsnPmmCode { get; set; }
	
		
        [InnerJoinColumn]
        public TblBillBll BllTsnTblBillBll { get; set; }
		
        [InnerJoinColumn]
        public TblOrderOrd OrdTsnTblOrderOrd { get; set; }
		
        [InnerJoinColumn]
        public TblTransactionattributeTsa TsaTsnTblTransactionattributeTsa { get; set; }
		
        [LeftJoinColumn]
        public TblEvoboxEbx TsnEbx { get; set; }
		
        [InnerJoinColumn]
        public TblUserUsr TsnUsr { get; set; }
	}

	[Table("tbl_useractivationkey_uak", "", "evoconcept")]
	public partial class TblUseractivationkeyUak : Entity<TblUseractivationkeyUak>
	{
		static TblUseractivationkeyUak()
		{
			Join<TblUserUsr>(t => t.UsrUakTblUserUsr, (t, f) => t.UakId == f.UsrUakId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("uak_id", true, true, false)]
        public int UakId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("uak_activation_key", false, false, true)]
        public string UakActivationKey { get; set; }
		
        [NotifyPropertyChanged, Column("uak_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UakCreatedate { get; set; }
	
		
        [LeftJoinColumn]
        public TblUserUsr UsrUakTblUserUsr { get; set; }
	}

	[Table("tbl_userinformation_uin", "", "evoconcept")]
	public partial class TblUserinformationUin : Entity<TblUserinformationUin>
	{
		static TblUserinformationUin()
		{
			Join<LutCountryCny>(t => t.UinCny, (t, f) => t.UinCnyCode == f.CnyCode); // Relation
			Join<TblUserUsr>(t => t.UinUsr, (t, f) => t.UinUsrId == f.UsrId); // Relation
		}

		
        [NotifyPropertyChanged, Column("uin_usr_id", true, false, false)]
        public int UinUsrId { get; set; }
		
        [NotifyPropertyChanged, Column("uin_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UinCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("uin_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> UinUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("uin_lastname", false, false, false)]
        public string UinLastname { get; set; }
		
        [NotifyPropertyChanged, Column("uin_firstname", false, false, false)]
        public string UinFirstname { get; set; }
		
        [NotifyPropertyChanged, Column("uin_city", false, false, false)]
        public string UinCity { get; set; }
		
        [NotifyPropertyChanged, Column("uin_cny_code", false, false, true)]
        public string UinCnyCode { get; set; }
		
        [NotifyPropertyChanged, Column("uin_post_code", false, false, false)]
        public string UinPostCode { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address1", false, false, false)]
        public string UinAddress1 { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address2", false, false, true)]
        public string UinAddress2 { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address3", false, false, true)]
        public string UinAddress3 { get; set; }
		
        [NotifyPropertyChanged, Column("uin_address4", false, false, true)]
        public string UinAddress4 { get; set; }
	
		
        [LeftJoinColumn]
        public LutCountryCny UinCny { get; set; }
		
        [InnerJoinColumn]
        public TblUserUsr UinUsr { get; set; }
	}

	[Table("tbl_user_usr", "", "evoconcept")]
	public partial class TblUserUsr : Entity<TblUserUsr>
	{
		static TblUserUsr()
		{
			Join<TblTransactionTsn>(t => t.TsnUsrTblTransactionTsn, (t, f) => t.UsrId == f.TsnUsrId); // Reverse Relation
			Join<TblUserinformationUin>(t => t.UinUsrTblUserinformationUin, (t, f) => t.UsrId == f.UinUsrId); // Reverse Relation
			Join<TblUseractivationkeyUak>(t => t.UsrUak, (t, f) => t.UsrUakId == f.UakId); // Relation
			Join<TblUsrEbxUbx>(t => t.UbxUsrTblUsrEbxUbx, (t, f) => t.UsrId == f.UbxUsrId); // Reverse Relation
		}

		
        [NotifyPropertyChanged, Column("usr_id", true, true, false)]
        public int UsrId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("usr_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UsrCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("usr_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> UsrUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("usr_password", false, false, false)]
        public string UsrPassword { get; set; }
		
        [NotifyPropertyChanged, Column("usr_email", false, false, false)]
        public string UsrEmail { get; set; }
		
        [NotifyPropertyChanged, Column("usr_login", false, false, true)]
        public string UsrLogin { get; set; }
		
        [NotifyPropertyChanged, Column("usr_locked", false, false, false)]
        public sbyte UsrLocked { get; set; }
		
        [NotifyPropertyChanged, Column("usr_enabled", false, false, false)]
        public sbyte UsrEnabled { get; set; }
		
        [NotifyPropertyChanged, Column("usr_admin", false, false, false)]
        public sbyte UsrAdmin { get; set; }
		
        [NotifyPropertyChanged, Column("usr_credit", false, false, false)]
        public int UsrCredit { get; set; }
		
        [NotifyPropertyChanged, Column("usr_uak_id", false, false, true)]
        public System.Nullable<int> UsrUakId { get; set; }
	
		
        [InnerJoinColumn]
        public TblTransactionTsn TsnUsrTblTransactionTsn { get; set; }
		
        [InnerJoinColumn]
        public TblUserinformationUin UinUsrTblUserinformationUin { get; set; }
		
        [LeftJoinColumn]
        public TblUseractivationkeyUak UsrUak { get; set; }
		
        [InnerJoinColumn]
        public TblUsrEbxUbx UbxUsrTblUsrEbxUbx { get; set; }
	}

	[Table("tbl_usr_ebx_ubx", "", "evoconcept")]
	public partial class TblUsrEbxUbx : Entity<TblUsrEbxUbx>
	{
		static TblUsrEbxUbx()
		{
			Join<TblConditionsCgv>(t => t.CgvUbxTblConditionsCgv, (t, f) => t.UbxId == f.CgvUbxId); // Reverse Relation
			Join<TblUserUsr>(t => t.UbxUsr, (t, f) => t.UbxUsrId == f.UsrId); // Relation
			Join<TblEvoboxEbx>(t => t.UbxEbx, (t, f) => t.UbxEbxId == f.EbxId); // Relation
		}

		
        [NotifyPropertyChanged, Column("ubx_id", true, true, false)]
        public int UbxId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ubx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UbxCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> UbxUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_ebx_id", false, false, false)]
        public int UbxEbxId { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_usr_id", false, false, false)]
        public int UbxUsrId { get; set; }
		
        [NotifyPropertyChanged, Column("ubx_owner", false, false, true)]
        public System.Nullable<sbyte> UbxOwner { get; set; }
	
		
        [LeftJoinColumn]
        public TblConditionsCgv CgvUbxTblConditionsCgv { get; set; }
		
        [InnerJoinColumn]
        public TblUserUsr UbxUsr { get; set; }
		
        [InnerJoinColumn]
        public TblEvoboxEbx UbxEbx { get; set; }
	}

	[Table("tbl_website_web", "", "evoconcept")]
	public partial class TblWebsiteWeb : Entity<TblWebsiteWeb>
	{
		static TblWebsiteWeb()
		{
			Join<TblApplicationApp>(t => t.WebApp, (t, f) => t.WebAppId == f.AppId); // Relation
			Join<TblDomainDom>(t => t.WebDom, (t, f) => t.WebDomId == f.DomId); // Relation
		}

		
        [NotifyPropertyChanged, Column("web_id", true, true, false)]
        public int WebId
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("web_createdate", false, false, true)]
        public System.Nullable<System.DateTime> WebCreatedate { get; set; }
		
        [NotifyPropertyChanged, Column("web_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> WebUpdatedate { get; set; }
		
        [NotifyPropertyChanged, Column("web_dom_id", false, false, false)]
        public int WebDomId { get; set; }
		
        [NotifyPropertyChanged, Column("web_app_id", false, false, false)]
        public int WebAppId { get; set; }
	
		
        [InnerJoinColumn]
        public TblApplicationApp WebApp { get; set; }
		
        [InnerJoinColumn]
        public TblDomainDom WebDom { get; set; }
	}


}

