

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

		
        private string _cnyCode;
        [Column("cny_code", true, false, false)]
        public string CnyCode { get { return this._cnyCode; } set { this.__Set(ref this._cnyCode, value); } }
		
        private string _cnyName;
        [Column("cny_name", false, false, false)]
        public string CnyName { get { return this._cnyName; } set { this.__Set(ref this._cnyName, value); } }
	
		
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

		
        [Column("ipa_id", true, true, false)]
        public int IpaId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _ipaIp;
        [Column("ipa_ip", false, false, false)]
        public string IpaIp { get { return this._ipaIp; } set { this.__Set(ref this._ipaIp, value); } }
		
        private string _ipaIptCode;
        [Column("ipa_ipt_code", false, false, false)]
        public string IpaIptCode { get { return this._ipaIptCode; } set { this.__Set(ref this._ipaIptCode, value); } }
	
		
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

		
        [Column("ofr_id", true, true, false)]
        public int OfrId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _ofrName;
        [Column("ofr_name", false, false, false)]
        public string OfrName { get { return this._ofrName; } set { this.__Set(ref this._ofrName, value); } }
		
        private decimal _ofrPrice;
        [Column("ofr_price", false, false, false)]
        public decimal OfrPrice { get { return this._ofrPrice; } set { this.__Set(ref this._ofrPrice, value); } }
		
        private sbyte _ofrLocked;
        [Column("ofr_locked", false, false, false)]
        public sbyte OfrLocked { get { return this._ofrLocked; } set { this.__Set(ref this._ofrLocked, value); } }
		
        private int _ofrTrialPeriod;
        [Column("ofr_trial_period", false, false, false)]
        public int OfrTrialPeriod { get { return this._ofrTrialPeriod; } set { this.__Set(ref this._ofrTrialPeriod, value); } }
		
        private string _ofrType;
        [Column("ofr_type", false, false, false)]
        public string OfrType { get { return this._ofrType; } set { this.__Set(ref this._ofrType, value); } }
	
		
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

		
        [Column("red_id", true, true, false)]
        public int RedId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _redOfrId;
        [Column("red_ofr_id", false, false, true)]
        public System.Nullable<int> RedOfrId { get { return this._redOfrId; } set { this.__Set(ref this._redOfrId, value); } }
		
        private int _redQuantity;
        [Column("red_quantity", false, false, false)]
        public int RedQuantity { get { return this._redQuantity; } set { this.__Set(ref this._redQuantity, value); } }
		
        private int _redPercentage;
        [Column("red_percentage", false, false, false)]
        public int RedPercentage { get { return this._redPercentage; } set { this.__Set(ref this._redPercentage, value); } }
	
		
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

		
        private string _sccCode;
        [Column("scc_code", true, false, false)]
        public string SccCode { get { return this._sccCode; } set { this.__Set(ref this._sccCode, value); } }
		
        private string _sccName;
        [Column("scc_name", false, false, false)]
        public string SccName { get { return this._sccName; } set { this.__Set(ref this._sccName, value); } }
		
        private int _sccOrder;
        [Column("scc_order", false, false, false)]
        public int SccOrder { get { return this._sccOrder; } set { this.__Set(ref this._sccOrder, value); } }
	
		
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

		
        [Column("spv_id", true, true, false)]
        public int SpvId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _spvValue;
        [Column("spv_value", false, false, false)]
        public int SpvValue { get { return this._spvValue; } set { this.__Set(ref this._spvValue, value); } }
		
        private sbyte _spvCustom;
        [Column("spv_custom", false, false, false)]
        public sbyte SpvCustom { get { return this._spvCustom; } set { this.__Set(ref this._spvCustom, value); } }
		
        private string _spvSpcCode;
        [Column("spv_spc_code", false, false, false)]
        public string SpvSpcCode { get { return this._spvSpcCode; } set { this.__Set(ref this._spvSpcCode, value); } }
	
		
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

		
        private string _spcCode;
        [Column("spc_code", true, false, false)]
        public string SpcCode { get { return this._spcCode; } set { this.__Set(ref this._spcCode, value); } }
		
        private string _spcName;
        [Column("spc_name", false, false, false)]
        public string SpcName { get { return this._spcName; } set { this.__Set(ref this._spcName, value); } }
		
        private int _spcPeriod;
        [Column("spc_period", false, false, false)]
        public int SpcPeriod { get { return this._spcPeriod; } set { this.__Set(ref this._spcPeriod, value); } }
		
        private string _spcDescription;
        [Column("spc_description", false, false, false)]
        public string SpcDescription { get { return this._spcDescription; } set { this.__Set(ref this._spcDescription, value); } }
		
        private string _spcUnit;
        [Column("spc_unit", false, false, true)]
        public string SpcUnit { get { return this._spcUnit; } set { this.__Set(ref this._spcUnit, value); } }
		
        private string _spcSccCode;
        [Column("spc_scc_code", false, false, false)]
        public string SpcSccCode { get { return this._spcSccCode; } set { this.__Set(ref this._spcSccCode, value); } }
		
        private sbyte _spcShow0;
        [Column("spc_show_0", false, false, false)]
        public sbyte SpcShow0 { get { return this._spcShow0; } set { this.__Set(ref this._spcShow0, value); } }
		
        private sbyte _spcAddS;
        [Column("spc_add_s", false, false, false)]
        public sbyte SpcAddS { get { return this._spcAddS; } set { this.__Set(ref this._spcAddS, value); } }
		
        private int _spcOrder;
        [Column("spc_order", false, false, false)]
        public int SpcOrder { get { return this._spcOrder; } set { this.__Set(ref this._spcOrder, value); } }
		
        private sbyte _spcImage;
        [Column("spc_image", false, false, false)]
        public sbyte SpcImage { get { return this._spcImage; } set { this.__Set(ref this._spcImage, value); } }
	
		
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
			Join<TblDomainDom>(t => t.AlsDom, (t, f) => t.AlsDomId == f.DomId); // Relation
			Join<TblEvoboxEbx>(t => t.AlsDerivedEbx, (t, f) => t.AlsDerivedEbxId == f.EbxId); // Relation
		}

		
        [Column("als_id", true, true, false)]
        public int AlsId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _alsDomId;
        [Column("als_dom_id", false, false, false)]
        public int AlsDomId { get { return this._alsDomId; } set { this.__Set(ref this._alsDomId, value); } }
		
        private System.Nullable<System.DateTime> _alsCreatedate;
        [Column("als_createdate", false, false, true)]
        public System.Nullable<System.DateTime> AlsCreatedate { get { return this._alsCreatedate; } set { this.__Set(ref this._alsCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _alsUpdatedate;
        [Column("als_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> AlsUpdatedate { get { return this._alsUpdatedate; } set { this.__Set(ref this._alsUpdatedate, value); } }
		
        private string _alsLocalPart;
        [Column("als_local_part", false, false, false)]
        public string AlsLocalPart { get { return this._alsLocalPart; } set { this.__Set(ref this._alsLocalPart, value); } }
		
        private string _alsRcpt;
        [Column("als_rcpt", false, false, false)]
        public string AlsRcpt { get { return this._alsRcpt; } set { this.__Set(ref this._alsRcpt, value); } }
		
        private int _alsDerivedEbxId;
        [Column("als_derived_ebx_id", false, false, false)]
        public int AlsDerivedEbxId { get { return this._alsDerivedEbxId; } set { this.__Set(ref this._alsDerivedEbxId, value); } }
	
		
        [InnerJoinColumn]
        public TblDomainDom AlsDom { get; set; }
		
        [InnerJoinColumn]
        public TblEvoboxEbx AlsDerivedEbx { get; set; }
	}

	[Table("tbl_applicationattribute_apa", "", "evoconcept")]
	public partial class TblApplicationattributeApa : Entity<TblApplicationattributeApa>
	{
		static TblApplicationattributeApa()
		{
			Join<TblApplicationApp>(t => t.ApaApp, (t, f) => t.ApaAppId == f.AppId); // Relation
		}

		
        [Column("apa_id", true, true, false)]
        public int ApaId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _apaAppId;
        [Column("apa_app_id", false, false, false)]
        public int ApaAppId { get { return this._apaAppId; } set { this.__Set(ref this._apaAppId, value); } }
		
        private string _apaValue;
        [Column("apa_value", false, false, false)]
        public string ApaValue { get { return this._apaValue; } set { this.__Set(ref this._apaValue, value); } }
		
        private string _apaAatCode;
        [Column("apa_aat_code", false, false, false)]
        public string ApaAatCode { get { return this._apaAatCode; } set { this.__Set(ref this._apaAatCode, value); } }
	
		
        [InnerJoinColumn]
        public TblApplicationApp ApaApp { get; set; }
	}

	[Table("tbl_applicationip_aip", "", "evoconcept")]
	public partial class TblApplicationipAip : Entity<TblApplicationipAip>
	{
		static TblApplicationipAip()
		{
		}

		
        private string _aipIp;
        [Column("aip_ip", false, false, false)]
        public string AipIp { get { return this._aipIp; } set { this.__Set(ref this._aipIp, value); } }
		
        private System.Nullable<int> _aipAppId;
        [Column("aip_app_id", false, false, true)]
        public System.Nullable<int> AipAppId { get { return this._aipAppId; } set { this.__Set(ref this._aipAppId, value); } }
	
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

		
        [Column("app_id", true, true, false)]
        public int AppId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _appEbxId;
        [Column("app_ebx_id", false, false, false)]
        public int AppEbxId { get { return this._appEbxId; } set { this.__Set(ref this._appEbxId, value); } }
		
        private System.Nullable<System.DateTime> _appCreatedate;
        [Column("app_createdate", false, false, true)]
        public System.Nullable<System.DateTime> AppCreatedate { get { return this._appCreatedate; } set { this.__Set(ref this._appCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _appUpdatedate;
        [Column("app_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> AppUpdatedate { get { return this._appUpdatedate; } set { this.__Set(ref this._appUpdatedate, value); } }
		
        private string _appName;
        [Column("app_name", false, false, false)]
        public string AppName { get { return this._appName; } set { this.__Set(ref this._appName, value); } }
		
        private int _appSvcId;
        [Column("app_svc_id", false, false, false)]
        public int AppSvcId { get { return this._appSvcId; } set { this.__Set(ref this._appSvcId, value); } }
		
        private string _appAptCode;
        [Column("app_apt_code", false, false, false)]
        public string AppAptCode { get { return this._appAptCode; } set { this.__Set(ref this._appAptCode, value); } }
	
		
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

		
        [Column("bll_id", true, true, false)]
        public int BllId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _bllTsnId;
        [Column("bll_tsn_id", false, false, false)]
        public int BllTsnId { get { return this._bllTsnId; } set { this.__Set(ref this._bllTsnId, value); } }
	
		
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

		
        [Column("cgv_id", true, true, false)]
        public int CgvId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _cgvName;
        [Column("cgv_name", false, false, false)]
        public string CgvName { get { return this._cgvName; } set { this.__Set(ref this._cgvName, value); } }
		
        private System.Nullable<System.DateTime> _cgvDate;
        [Column("cgv_date", false, false, true)]
        public System.Nullable<System.DateTime> CgvDate { get { return this._cgvDate; } set { this.__Set(ref this._cgvDate, value); } }
		
        private System.Nullable<int> _cgvUbxId;
        [Column("cgv_ubx_id", false, false, true)]
        public System.Nullable<int> CgvUbxId { get { return this._cgvUbxId; } set { this.__Set(ref this._cgvUbxId, value); } }
	
		
        [LeftJoinColumn]
        public TblUsrEbxUbx CgvUbx { get; set; }
	}

	[Table("tbl_cron_crn", "", "evoconcept")]
	public partial class TblCronCrn : Entity<TblCronCrn>
	{
		static TblCronCrn()
		{
			Join<TblEvoboxEbx>(t => t.CrnEbx, (t, f) => t.CrnEbxId == f.EbxId); // Relation
			Join<TblServiceSvc>(t => t.CrnSvc, (t, f) => t.CrnSvcId == f.SvcId); // Relation
		}

		
        [Column("crn_id", true, true, false)]
        public int CrnId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _crnEbxId;
        [Column("crn_ebx_id", false, false, false)]
        public int CrnEbxId { get { return this._crnEbxId; } set { this.__Set(ref this._crnEbxId, value); } }
		
        private string _crnUrl;
        [Column("crn_url", false, false, false)]
        public string CrnUrl { get { return this._crnUrl; } set { this.__Set(ref this._crnUrl, value); } }
		
        private int _crnMonth;
        [Column("crn_month", false, false, false)]
        public int CrnMonth { get { return this._crnMonth; } set { this.__Set(ref this._crnMonth, value); } }
		
        private int _crnDay;
        [Column("crn_day", false, false, false)]
        public int CrnDay { get { return this._crnDay; } set { this.__Set(ref this._crnDay, value); } }
		
        private int _crnHour;
        [Column("crn_hour", false, false, false)]
        public int CrnHour { get { return this._crnHour; } set { this.__Set(ref this._crnHour, value); } }
		
        private long _crnMinute;
        [Column("crn_minute", false, false, false)]
        public long CrnMinute { get { return this._crnMinute; } set { this.__Set(ref this._crnMinute, value); } }
		
        private string _crnEmailNotification;
        [Column("crn_email_notification", false, false, true)]
        public string CrnEmailNotification { get { return this._crnEmailNotification; } set { this.__Set(ref this._crnEmailNotification, value); } }
		
        private System.Nullable<System.DateTime> _crnLastExecuted;
        [Column("crn_last_executed", false, false, true)]
        public System.Nullable<System.DateTime> CrnLastExecuted { get { return this._crnLastExecuted; } set { this.__Set(ref this._crnLastExecuted, value); } }
		
        private System.Nullable<int> _crnLastExecutionTime;
        [Column("crn_last_execution_time", false, false, true)]
        public System.Nullable<int> CrnLastExecutionTime { get { return this._crnLastExecutionTime; } set { this.__Set(ref this._crnLastExecutionTime, value); } }
		
        private System.Nullable<System.DateTime> _crnCreatedate;
        [Column("crn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> CrnCreatedate { get { return this._crnCreatedate; } set { this.__Set(ref this._crnCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _crnUpdatedate;
        [Column("crn_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> CrnUpdatedate { get { return this._crnUpdatedate; } set { this.__Set(ref this._crnUpdatedate, value); } }
		
        private int _crnSvcId;
        [Column("crn_svc_id", false, false, false)]
        public int CrnSvcId { get { return this._crnSvcId; } set { this.__Set(ref this._crnSvcId, value); } }
	
		
        [InnerJoinColumn]
        public TblEvoboxEbx CrnEbx { get; set; }
		
        [InnerJoinColumn]
        public TblServiceSvc CrnSvc { get; set; }
	}

	[Table("tbl_databaselogin_dbl", "", "evoconcept")]
	public partial class TblDatabaseloginDbl : Entity<TblDatabaseloginDbl>
	{
		static TblDatabaseloginDbl()
		{
			Join<TblEvoboxEbx>(t => t.DblEbx, (t, f) => t.DblEbxId == f.EbxId); // Relation
			Join<TblServiceSvc>(t => t.DblSvc, (t, f) => t.DblSvcId == f.SvcId); // Relation
		}

		
        [Column("dbl_id", true, true, false)]
        public int DblId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _dblLogin;
        [Column("dbl_login", false, false, false)]
        public string DblLogin { get { return this._dblLogin; } set { this.__Set(ref this._dblLogin, value); } }
		
        private int _dblEbxId;
        [Column("dbl_ebx_id", false, false, false)]
        public int DblEbxId { get { return this._dblEbxId; } set { this.__Set(ref this._dblEbxId, value); } }
		
        private System.Nullable<System.DateTime> _dblCreatedate;
        [Column("dbl_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DblCreatedate { get { return this._dblCreatedate; } set { this.__Set(ref this._dblCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _dblUpdatedate;
        [Column("dbl_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DblUpdatedate { get { return this._dblUpdatedate; } set { this.__Set(ref this._dblUpdatedate, value); } }
		
        private int _dblSvcId;
        [Column("dbl_svc_id", false, false, false)]
        public int DblSvcId { get { return this._dblSvcId; } set { this.__Set(ref this._dblSvcId, value); } }
		
        private string _dblPassword;
        [Column("dbl_password", false, false, true)]
        public string DblPassword { get { return this._dblPassword; } set { this.__Set(ref this._dblPassword, value); } }
	
		
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

		
        [Column("dtb_id", true, true, false)]
        public int DtbId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _dtbName;
        [Column("dtb_name", false, false, false)]
        public string DtbName { get { return this._dtbName; } set { this.__Set(ref this._dtbName, value); } }
		
        private int _dtbEbxId;
        [Column("dtb_ebx_id", false, false, false)]
        public int DtbEbxId { get { return this._dtbEbxId; } set { this.__Set(ref this._dtbEbxId, value); } }
		
        private System.Nullable<System.DateTime> _dtbCreatedate;
        [Column("dtb_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DtbCreatedate { get { return this._dtbCreatedate; } set { this.__Set(ref this._dtbCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _dtbUpdatedate;
        [Column("dtb_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DtbUpdatedate { get { return this._dtbUpdatedate; } set { this.__Set(ref this._dtbUpdatedate, value); } }
		
        private int _dtbSvcId;
        [Column("dtb_svc_id", false, false, false)]
        public int DtbSvcId { get { return this._dtbSvcId; } set { this.__Set(ref this._dtbSvcId, value); } }
	
		
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

		
        [Column("dns_id", true, true, false)]
        public int DnsId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _dnsDomId;
        [Column("dns_dom_id", false, false, false)]
        public int DnsDomId { get { return this._dnsDomId; } set { this.__Set(ref this._dnsDomId, value); } }
		
        private string _dnsName;
        [Column("dns_name", false, false, true)]
        public string DnsName { get { return this._dnsName; } set { this.__Set(ref this._dnsName, value); } }
		
        private System.Nullable<int> _dnsPriority;
        [Column("dns_priority", false, false, true)]
        public System.Nullable<int> DnsPriority { get { return this._dnsPriority; } set { this.__Set(ref this._dnsPriority, value); } }
		
        private string _dnsTo;
        [Column("dns_to", false, false, false)]
        public string DnsTo { get { return this._dnsTo; } set { this.__Set(ref this._dnsTo, value); } }
		
        private System.Nullable<System.DateTime> _dnsCreatedate;
        [Column("dns_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DnsCreatedate { get { return this._dnsCreatedate; } set { this.__Set(ref this._dnsCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _dnsUpdatedate;
        [Column("dns_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DnsUpdatedate { get { return this._dnsUpdatedate; } set { this.__Set(ref this._dnsUpdatedate, value); } }
		
        private string _dnsDntCode;
        [Column("dns_dnt_code", false, false, false)]
        public string DnsDntCode { get { return this._dnsDntCode; } set { this.__Set(ref this._dnsDntCode, value); } }
		
        private System.Nullable<int> _dnsSdmId;
        [Column("dns_sdm_id", false, false, true)]
        public System.Nullable<int> DnsSdmId { get { return this._dnsSdmId; } set { this.__Set(ref this._dnsSdmId, value); } }
	
		
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
			Join<TblDomainDom>(t => t.DomParentDom, (t, f) => t.DomParentDomId == f.DomId); // Relation
			Join<TblDomainDom>(t => t.DomParentDomTblDomainDom, (t, f) => t.DomId == f.DomParentDomId); // Reverse Relation
			Join<TblServiceSvc>(t => t.DomSecondarySvc, (t, f) => t.DomSecondarySvcId == f.SvcId); // Relation
			Join<TblEvoboxEbx>(t => t.DomEbx, (t, f) => t.DomEbxId == f.EbxId); // Relation
			Join<TblServiceSvc>(t => t.DomPrimarySvc, (t, f) => t.DomPrimarySvcId == f.SvcId); // Relation
			Join<TblEmailEml>(t => t.EmlDomTblEmailEml, (t, f) => t.DomId == f.EmlDomId); // Reverse Relation
			Join<TblWebsiteWeb>(t => t.WebDomTblWebsiteWeb, (t, f) => t.DomId == f.WebDomId); // Reverse Relation
		}

		
        [Column("dom_id", true, true, false)]
        public int DomId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _domEbxId;
        [Column("dom_ebx_id", false, false, true)]
        public System.Nullable<int> DomEbxId { get { return this._domEbxId; } set { this.__Set(ref this._domEbxId, value); } }
		
        private string _domName;
        [Column("dom_name", false, false, false)]
        public string DomName { get { return this._domName; } set { this.__Set(ref this._domName, value); } }
		
        private System.Nullable<int> _domParentDomId;
        [Column("dom_parent_dom_id", false, false, true)]
        public System.Nullable<int> DomParentDomId { get { return this._domParentDomId; } set { this.__Set(ref this._domParentDomId, value); } }
		
        private sbyte _domHostdom;
        [Column("dom_hostdom", false, false, false)]
        public sbyte DomHostdom { get { return this._domHostdom; } set { this.__Set(ref this._domHostdom, value); } }
		
        private System.Nullable<System.DateTime> _domCreatedate;
        [Column("dom_createdate", false, false, true)]
        public System.Nullable<System.DateTime> DomCreatedate { get { return this._domCreatedate; } set { this.__Set(ref this._domCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _domUpdatedate;
        [Column("dom_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> DomUpdatedate { get { return this._domUpdatedate; } set { this.__Set(ref this._domUpdatedate, value); } }
		
        private System.Nullable<int> _domPrimarySvcId;
        [Column("dom_primary_svc_id", false, false, true)]
        public System.Nullable<int> DomPrimarySvcId { get { return this._domPrimarySvcId; } set { this.__Set(ref this._domPrimarySvcId, value); } }
		
        private System.Nullable<int> _domSecondarySvcId;
        [Column("dom_secondary_svc_id", false, false, true)]
        public System.Nullable<int> DomSecondarySvcId { get { return this._domSecondarySvcId; } set { this.__Set(ref this._domSecondarySvcId, value); } }
		
        private string _domFullname;
        [Column("dom_fullname", false, false, false)]
        public string DomFullname { get { return this._domFullname; } set { this.__Set(ref this._domFullname, value); } }
		
        private System.Nullable<sbyte> _domDnsmanaged;
        [Column("dom_dnsmanaged", false, false, true)]
        public System.Nullable<sbyte> DomDnsmanaged { get { return this._domDnsmanaged; } set { this.__Set(ref this._domDnsmanaged, value); } }
		
        private string _domStatus;
        [Column("dom_status", false, false, false)]
        public string DomStatus { get { return this._domStatus; } set { this.__Set(ref this._domStatus, value); } }
		
        private System.Nullable<sbyte> _domForceremote;
        [Column("dom_forceremote", false, false, true)]
        public System.Nullable<sbyte> DomForceremote { get { return this._domForceremote; } set { this.__Set(ref this._domForceremote, value); } }
	
		
        [InnerJoinColumn]
        public TblAliasAls AlsDomTblAliasAls { get; set; }
		
        [InnerJoinColumn]
        public TblDnsrecordDns DnsDomTblDnsrecordDns { get; set; }
		
        [LeftJoinColumn]
        public TblDnsrecordDns DnsSdmTblDnsrecordDns { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DomParentDom { get; set; }
		
        [LeftJoinColumn]
        public TblDomainDom DomParentDomTblDomainDom { get; set; }
		
        [LeftJoinColumn]
        public TblServiceSvc DomSecondarySvc { get; set; }
		
        [LeftJoinColumn]
        public TblEvoboxEbx DomEbx { get; set; }
		
        [LeftJoinColumn]
        public TblServiceSvc DomPrimarySvc { get; set; }
		
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

		
        [Column("eml_id", true, true, false)]
        public int EmlId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _emlDomId;
        [Column("eml_dom_id", false, false, false)]
        public int EmlDomId { get { return this._emlDomId; } set { this.__Set(ref this._emlDomId, value); } }
		
        private System.Nullable<System.DateTime> _emlCreatedate;
        [Column("eml_createdate", false, false, true)]
        public System.Nullable<System.DateTime> EmlCreatedate { get { return this._emlCreatedate; } set { this.__Set(ref this._emlCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _emlUpdatedate;
        [Column("eml_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> EmlUpdatedate { get { return this._emlUpdatedate; } set { this.__Set(ref this._emlUpdatedate, value); } }
		
        private string _emlHomeDir;
        [Column("eml_home_dir", false, false, false)]
        public string EmlHomeDir { get { return this._emlHomeDir; } set { this.__Set(ref this._emlHomeDir, value); } }
		
        private sbyte _emlCatchAll;
        [Column("eml_catch_all", false, false, false)]
        public sbyte EmlCatchAll { get { return this._emlCatchAll; } set { this.__Set(ref this._emlCatchAll, value); } }
		
        private System.Nullable<sbyte> _emlAway;
        [Column("eml_away", false, false, true)]
        public System.Nullable<sbyte> EmlAway { get { return this._emlAway; } set { this.__Set(ref this._emlAway, value); } }
		
        private string _emlMessage;
        [Column("eml_message", false, false, true)]
        public string EmlMessage { get { return this._emlMessage; } set { this.__Set(ref this._emlMessage, value); } }
		
        private string _emlPassword;
        [Column("eml_password", false, false, false)]
        public string EmlPassword { get { return this._emlPassword; } set { this.__Set(ref this._emlPassword, value); } }
		
        private int _emlQuotaUsage;
        [Column("eml_quota_usage", false, false, false)]
        public int EmlQuotaUsage { get { return this._emlQuotaUsage; } set { this.__Set(ref this._emlQuotaUsage, value); } }
		
        private string _emlLocalPart;
        [Column("eml_local_part", false, false, false)]
        public string EmlLocalPart { get { return this._emlLocalPart; } set { this.__Set(ref this._emlLocalPart, value); } }
		
        private int _emlDerivedEbxId;
        [Column("eml_derived_ebx_id", false, false, false)]
        public int EmlDerivedEbxId { get { return this._emlDerivedEbxId; } set { this.__Set(ref this._emlDerivedEbxId, value); } }
	
		
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

		
        [Column("ebx_id", true, true, false)]
        public int EbxId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _ebxName;
        [Column("ebx_name", false, false, false)]
        public string EbxName { get { return this._ebxName; } set { this.__Set(ref this._ebxName, value); } }
		
        private System.Nullable<System.DateTime> _ebxStartdate;
        [Column("ebx_startdate", false, false, true)]
        public System.Nullable<System.DateTime> EbxStartdate { get { return this._ebxStartdate; } set { this.__Set(ref this._ebxStartdate, value); } }
		
        private System.Nullable<System.DateTime> _ebxEnddate;
        [Column("ebx_enddate", false, false, true)]
        public System.Nullable<System.DateTime> EbxEnddate { get { return this._ebxEnddate; } set { this.__Set(ref this._ebxEnddate, value); } }
		
        private int _ebxOfrId;
        [Column("ebx_ofr_id", false, false, false)]
        public int EbxOfrId { get { return this._ebxOfrId; } set { this.__Set(ref this._ebxOfrId, value); } }
		
        private int _ebxQuota;
        [Column("ebx_quota", false, false, false)]
        public int EbxQuota { get { return this._ebxQuota; } set { this.__Set(ref this._ebxQuota, value); } }
		
        private System.Nullable<System.DateTime> _ebxCreatedate;
        [Column("ebx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> EbxCreatedate { get { return this._ebxCreatedate; } set { this.__Set(ref this._ebxCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _ebxUpdatedate;
        [Column("ebx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> EbxUpdatedate { get { return this._ebxUpdatedate; } set { this.__Set(ref this._ebxUpdatedate, value); } }
		
        private System.Nullable<int> _ebxStatsServiceId;
        [Column("ebx_stats_service_id", false, false, true)]
        public System.Nullable<int> EbxStatsServiceId { get { return this._ebxStatsServiceId; } set { this.__Set(ref this._ebxStatsServiceId, value); } }
		
        private string _ebxHomedir;
        [Column("ebx_homedir", false, false, true)]
        public string EbxHomedir { get { return this._ebxHomedir; } set { this.__Set(ref this._ebxHomedir, value); } }
		
        private string _ebxStatus;
        [Column("ebx_status", false, false, false)]
        public string EbxStatus { get { return this._ebxStatus; } set { this.__Set(ref this._ebxStatus, value); } }
		
        private System.Nullable<int> _ebxNfsServiceId;
        [Column("ebx_nfs_service_id", false, false, true)]
        public System.Nullable<int> EbxNfsServiceId { get { return this._ebxNfsServiceId; } set { this.__Set(ref this._ebxNfsServiceId, value); } }
	
		
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

		
        [Column("fqc_id", true, true, false)]
        public int FqcId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _fqcTitle;
        [Column("fqc_title", false, false, false)]
        public string FqcTitle { get { return this._fqcTitle; } set { this.__Set(ref this._fqcTitle, value); } }
		
        private int _fqcOrder;
        [Column("fqc_order", false, false, false)]
        public int FqcOrder { get { return this._fqcOrder; } set { this.__Set(ref this._fqcOrder, value); } }
	
		
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

		
        [Column("faq_id", true, true, false)]
        public int FaqId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _faqFqcId;
        [Column("faq_fqc_id", false, false, false)]
        public int FaqFqcId { get { return this._faqFqcId; } set { this.__Set(ref this._faqFqcId, value); } }
		
        private string _faqQuestion;
        [Column("faq_question", false, false, false)]
        public string FaqQuestion { get { return this._faqQuestion; } set { this.__Set(ref this._faqQuestion, value); } }
		
        private string _faqAnswer;
        [Column("faq_answer", false, false, false)]
        public string FaqAnswer { get { return this._faqAnswer; } set { this.__Set(ref this._faqAnswer, value); } }
		
        private int _faqOrder;
        [Column("faq_order", false, false, false)]
        public int FaqOrder { get { return this._faqOrder; } set { this.__Set(ref this._faqOrder, value); } }
	
		
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

		
        [Column("ftp_id", true, true, false)]
        public int FtpId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _ftpEbxId;
        [Column("ftp_ebx_id", false, false, false)]
        public int FtpEbxId { get { return this._ftpEbxId; } set { this.__Set(ref this._ftpEbxId, value); } }
		
        private string _ftpLogin;
        [Column("ftp_login", false, false, false)]
        public string FtpLogin { get { return this._ftpLogin; } set { this.__Set(ref this._ftpLogin, value); } }
		
        private string _ftpPassword;
        [Column("ftp_password", false, false, false)]
        public string FtpPassword { get { return this._ftpPassword; } set { this.__Set(ref this._ftpPassword, value); } }
		
        private string _ftpHomedir;
        [Column("ftp_homedir", false, false, false)]
        public string FtpHomedir { get { return this._ftpHomedir; } set { this.__Set(ref this._ftpHomedir, value); } }
		
        private int _ftpCount;
        [Column("ftp_count", false, false, false)]
        public int FtpCount { get { return this._ftpCount; } set { this.__Set(ref this._ftpCount, value); } }
		
        private sbyte _ftpEnabled;
        [Column("ftp_enabled", false, false, false)]
        public sbyte FtpEnabled { get { return this._ftpEnabled; } set { this.__Set(ref this._ftpEnabled, value); } }
		
        private System.Nullable<System.DateTime> _ftpCreatedate;
        [Column("ftp_createdate", false, false, true)]
        public System.Nullable<System.DateTime> FtpCreatedate { get { return this._ftpCreatedate; } set { this.__Set(ref this._ftpCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _ftpUpdatedate;
        [Column("ftp_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> FtpUpdatedate { get { return this._ftpUpdatedate; } set { this.__Set(ref this._ftpUpdatedate, value); } }
		
        private System.Nullable<System.DateTime> _ftpAccessed;
        [Column("ftp_accessed", false, false, true)]
        public System.Nullable<System.DateTime> FtpAccessed { get { return this._ftpAccessed; } set { this.__Set(ref this._ftpAccessed, value); } }
		
        private System.Nullable<System.DateTime> _ftpModified;
        [Column("ftp_modified", false, false, true)]
        public System.Nullable<System.DateTime> FtpModified { get { return this._ftpModified; } set { this.__Set(ref this._ftpModified, value); } }
		
        private int _ftpSvcId;
        [Column("ftp_svc_id", false, false, false)]
        public int FtpSvcId { get { return this._ftpSvcId; } set { this.__Set(ref this._ftpSvcId, value); } }
	
		
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

		
        [Column("nws_id", true, true, false)]
        public int NwsId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _nwsTitle;
        [Column("nws_title", false, false, false)]
        public string NwsTitle { get { return this._nwsTitle; } set { this.__Set(ref this._nwsTitle, value); } }
		
        private string _nwsContent;
        [Column("nws_content", false, false, false)]
        public string NwsContent { get { return this._nwsContent; } set { this.__Set(ref this._nwsContent, value); } }
		
        private string _nwsAuthor;
        [Column("nws_author", false, false, false)]
        public string NwsAuthor { get { return this._nwsAuthor; } set { this.__Set(ref this._nwsAuthor, value); } }
		
        private System.Nullable<System.DateTime> _nwsDate;
        [Column("nws_date", false, false, true)]
        public System.Nullable<System.DateTime> NwsDate { get { return this._nwsDate; } set { this.__Set(ref this._nwsDate, value); } }
	
	}

	[Table("tbl_ofr_spv_osv", "", "evoconcept")]
	public partial class TblOfrSpvOsv : Entity<TblOfrSpvOsv>
	{
		static TblOfrSpvOsv()
		{
			Join<LutSpecificationvalueSpv>(t => t.OsvSpv, (t, f) => t.OsvSpvId == f.SpvId); // Relation
			Join<LutOfferOfr>(t => t.OsvOfr, (t, f) => t.OsvOfrId == f.OfrId); // Relation
		}

		
        [Column("osv_id", true, true, false)]
        public int OsvId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _osvOfrId;
        [Column("osv_ofr_id", false, false, false)]
        public int OsvOfrId { get { return this._osvOfrId; } set { this.__Set(ref this._osvOfrId, value); } }
		
        private int _osvSpvId;
        [Column("osv_spv_id", false, false, false)]
        public int OsvSpvId { get { return this._osvSpvId; } set { this.__Set(ref this._osvSpvId, value); } }
	
		
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

		
        [Column("ord_id", true, true, false)]
        public int OrdId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _ordTsnId;
        [Column("ord_tsn_id", false, false, false)]
        public int OrdTsnId { get { return this._ordTsnId; } set { this.__Set(ref this._ordTsnId, value); } }
	
		
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

		
        [Column("sip_id", true, true, false)]
        public int SipId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _sipSrvId;
        [Column("sip_srv_id", false, false, false)]
        public int SipSrvId { get { return this._sipSrvId; } set { this.__Set(ref this._sipSrvId, value); } }
		
        private int _sipIpaId;
        [Column("sip_ipa_id", false, false, false)]
        public int SipIpaId { get { return this._sipIpaId; } set { this.__Set(ref this._sipIpaId, value); } }
		
        private int _sipStatus;
        [Column("sip_status", false, false, false)]
        public int SipStatus { get { return this._sipStatus; } set { this.__Set(ref this._sipStatus, value); } }
	
		
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

		
        [Column("srv_id", true, true, false)]
        public int SrvId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _srvName;
        [Column("srv_name", false, false, true)]
        public string SrvName { get { return this._srvName; } set { this.__Set(ref this._srvName, value); } }
		
        private int _srvStatus;
        [Column("srv_status", false, false, false)]
        public int SrvStatus { get { return this._srvStatus; } set { this.__Set(ref this._srvStatus, value); } }
		
        private string _srvType;
        [Column("srv_type", false, false, false)]
        public string SrvType { get { return this._srvType; } set { this.__Set(ref this._srvType, value); } }
	
		
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

		
        [Column("sva_id", true, true, false)]
        public int SvaId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _svaKey;
        [Column("sva_key", false, false, false)]
        public string SvaKey { get { return this._svaKey; } set { this.__Set(ref this._svaKey, value); } }
		
        private string _svaStrvalue;
        [Column("sva_strvalue", false, false, true)]
        public string SvaStrvalue { get { return this._svaStrvalue; } set { this.__Set(ref this._svaStrvalue, value); } }
		
        private System.Nullable<int> _svaIntvalue;
        [Column("sva_intvalue", false, false, true)]
        public System.Nullable<int> SvaIntvalue { get { return this._svaIntvalue; } set { this.__Set(ref this._svaIntvalue, value); } }
		
        private object _svaDblvalue;
        [Column("sva_dblvalue", false, false, true)]
        public object SvaDblvalue { get { return this._svaDblvalue; } set { this.__Set(ref this._svaDblvalue, value); } }
		
        private System.Nullable<System.DateTime> _svaDtvalue;
        [Column("sva_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> SvaDtvalue { get { return this._svaDtvalue; } set { this.__Set(ref this._svaDtvalue, value); } }
		
        private int _svaSvcId;
        [Column("sva_svc_id", false, false, false)]
        public int SvaSvcId { get { return this._svaSvcId; } set { this.__Set(ref this._svaSvcId, value); } }
	
		
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

		
        [Column("sin_id", true, true, false)]
        public int SinId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _sinSvc1Id;
        [Column("sin_svc1_id", false, false, false)]
        public int SinSvc1Id { get { return this._sinSvc1Id; } set { this.__Set(ref this._sinSvc1Id, value); } }
		
        private int _sinSvc2Id;
        [Column("sin_svc2_id", false, false, false)]
        public int SinSvc2Id { get { return this._sinSvc2Id; } set { this.__Set(ref this._sinSvc2Id, value); } }
	
		
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

		
        [Column("svc_id", true, true, false)]
        public int SvcId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _svcName;
        [Column("svc_name", false, false, true)]
        public string SvcName { get { return this._svcName; } set { this.__Set(ref this._svcName, value); } }
		
        private string _svcDescription;
        [Column("svc_description", false, false, true)]
        public string SvcDescription { get { return this._svcDescription; } set { this.__Set(ref this._svcDescription, value); } }
		
        private int _svcSrvId;
        [Column("svc_srv_id", false, false, false)]
        public int SvcSrvId { get { return this._svcSrvId; } set { this.__Set(ref this._svcSrvId, value); } }
		
        private string _svcSvtCode;
        [Column("svc_svt_code", false, false, false)]
        public string SvcSvtCode { get { return this._svcSvtCode; } set { this.__Set(ref this._svcSvtCode, value); } }
		
        private System.Nullable<int> _svcCapacity;
        [Column("svc_capacity", false, false, true)]
        public System.Nullable<int> SvcCapacity { get { return this._svcCapacity; } set { this.__Set(ref this._svcCapacity, value); } }
		
        private string _svcDomain;
        [Column("svc_domain", false, false, true)]
        public string SvcDomain { get { return this._svcDomain; } set { this.__Set(ref this._svcDomain, value); } }
		
        private System.Nullable<int> _svcBackupSvcId;
        [Column("svc_backup_svc_id", false, false, true)]
        public System.Nullable<int> SvcBackupSvcId { get { return this._svcBackupSvcId; } set { this.__Set(ref this._svcBackupSvcId, value); } }
	
		
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

		
        private int _ssvHostId;
        [Column("ssv_host_id", true, false, false)]
        public int SsvHostId { get { return this._ssvHostId; } set { this.__Set(ref this._ssvHostId, value); } }
		
        private int _ssvGuestId;
        [Column("ssv_guest_id", true, false, false)]
        public int SsvGuestId { get { return this._ssvGuestId; } set { this.__Set(ref this._ssvGuestId, value); } }
		
        private sbyte _ssvActive;
        [Column("ssv_active", false, false, false)]
        public sbyte SsvActive { get { return this._ssvActive; } set { this.__Set(ref this._ssvActive, value); } }
	
		
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

		
        [Column("tka_id", true, true, false)]
        public int TkaId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _tkaKey;
        [Column("tka_key", false, false, false)]
        public string TkaKey { get { return this._tkaKey; } set { this.__Set(ref this._tkaKey, value); } }
		
        private string _tkaStrvalue;
        [Column("tka_strvalue", false, false, true)]
        public string TkaStrvalue { get { return this._tkaStrvalue; } set { this.__Set(ref this._tkaStrvalue, value); } }
		
        private System.Nullable<int> _tkaIntvalue;
        [Column("tka_intvalue", false, false, true)]
        public System.Nullable<int> TkaIntvalue { get { return this._tkaIntvalue; } set { this.__Set(ref this._tkaIntvalue, value); } }
		
        private object _tkaDblvalue;
        [Column("tka_dblvalue", false, false, true)]
        public object TkaDblvalue { get { return this._tkaDblvalue; } set { this.__Set(ref this._tkaDblvalue, value); } }
		
        private System.Nullable<System.DateTime> _tkaDtvalue;
        [Column("tka_dtvalue", false, false, true)]
        public System.Nullable<System.DateTime> TkaDtvalue { get { return this._tkaDtvalue; } set { this.__Set(ref this._tkaDtvalue, value); } }
		
        private int _tkaTskId;
        [Column("tka_tsk_id", false, false, false)]
        public int TkaTskId { get { return this._tkaTskId; } set { this.__Set(ref this._tkaTskId, value); } }
	
		
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

		
        [Column("tsk_id", true, true, false)]
        public int TskId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _tskType;
        [Column("tsk_type", false, false, false)]
        public string TskType { get { return this._tskType; } set { this.__Set(ref this._tskType, value); } }
		
        private string _tskRefType;
        [Column("tsk_ref_type", false, false, true)]
        public string TskRefType { get { return this._tskRefType; } set { this.__Set(ref this._tskRefType, value); } }
		
        private System.Nullable<int> _tskRefId;
        [Column("tsk_ref_id", false, false, true)]
        public System.Nullable<int> TskRefId { get { return this._tskRefId; } set { this.__Set(ref this._tskRefId, value); } }
		
        private string _tskStatus;
        [Column("tsk_status", false, false, false)]
        public string TskStatus { get { return this._tskStatus; } set { this.__Set(ref this._tskStatus, value); } }
		
        private int _tskPercentage;
        [Column("tsk_percentage", false, false, false)]
        public int TskPercentage { get { return this._tskPercentage; } set { this.__Set(ref this._tskPercentage, value); } }
		
        private System.Nullable<int> _tskSvcId;
        [Column("tsk_svc_id", false, false, true)]
        public System.Nullable<int> TskSvcId { get { return this._tskSvcId; } set { this.__Set(ref this._tskSvcId, value); } }
		
        private string _tskError;
        [Column("tsk_error", false, false, true)]
        public string TskError { get { return this._tskError; } set { this.__Set(ref this._tskError, value); } }
		
        private System.Nullable<System.DateTime> _tskCreatedAt;
        [Column("tsk_created_at", false, false, true)]
        public System.Nullable<System.DateTime> TskCreatedAt { get { return this._tskCreatedAt; } set { this.__Set(ref this._tskCreatedAt, value); } }
		
        private System.Nullable<System.DateTime> _tskExecutedAt;
        [Column("tsk_executed_at", false, false, true)]
        public System.Nullable<System.DateTime> TskExecutedAt { get { return this._tskExecutedAt; } set { this.__Set(ref this._tskExecutedAt, value); } }
		
        private System.Nullable<System.DateTime> _tskFinishedAt;
        [Column("tsk_finished_at", false, false, true)]
        public System.Nullable<System.DateTime> TskFinishedAt { get { return this._tskFinishedAt; } set { this.__Set(ref this._tskFinishedAt, value); } }
	
		
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

		
        [Column("tsa_id", true, true, false)]
        public int TsaId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private int _tsaTsnId;
        [Column("tsa_tsn_id", false, false, false)]
        public int TsaTsnId { get { return this._tsaTsnId; } set { this.__Set(ref this._tsaTsnId, value); } }
		
        private string _tsaKey;
        [Column("tsa_key", false, false, false)]
        public string TsaKey { get { return this._tsaKey; } set { this.__Set(ref this._tsaKey, value); } }
		
        private string _tsaValue;
        [Column("tsa_value", false, false, false)]
        public string TsaValue { get { return this._tsaValue; } set { this.__Set(ref this._tsaValue, value); } }
	
		
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
			Join<TblUserUsr>(t => t.TsnUsr, (t, f) => t.TsnUsrId == f.UsrId); // Relation
			Join<TblEvoboxEbx>(t => t.TsnEbx, (t, f) => t.TsnEbxId == f.EbxId); // Relation
		}

		
        [Column("tsn_id", true, true, false)]
        public int TsnId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<int> _tsnEbxId;
        [Column("tsn_ebx_id", false, false, true)]
        public System.Nullable<int> TsnEbxId { get { return this._tsnEbxId; } set { this.__Set(ref this._tsnEbxId, value); } }
		
        private int _tsnUsrId;
        [Column("tsn_usr_id", false, false, false)]
        public int TsnUsrId { get { return this._tsnUsrId; } set { this.__Set(ref this._tsnUsrId, value); } }
		
        private decimal _tsnPrice;
        [Column("tsn_price", false, false, false)]
        public decimal TsnPrice { get { return this._tsnPrice; } set { this.__Set(ref this._tsnPrice, value); } }
		
        private string _tsnStatus;
        [Column("tsn_status", false, false, false)]
        public string TsnStatus { get { return this._tsnStatus; } set { this.__Set(ref this._tsnStatus, value); } }
		
        private System.Nullable<decimal> _tsnAmountPayed;
        [Column("tsn_amount_payed", false, false, true)]
        public System.Nullable<decimal> TsnAmountPayed { get { return this._tsnAmountPayed; } set { this.__Set(ref this._tsnAmountPayed, value); } }
		
        private string _tsnUsage;
        [Column("tsn_usage", false, false, false)]
        public string TsnUsage { get { return this._tsnUsage; } set { this.__Set(ref this._tsnUsage, value); } }
		
        private string _tsnDescription;
        [Column("tsn_description", false, false, true)]
        public string TsnDescription { get { return this._tsnDescription; } set { this.__Set(ref this._tsnDescription, value); } }
		
        private System.Nullable<System.DateTime> _tsnCreatedate;
        [Column("tsn_createdate", false, false, true)]
        public System.Nullable<System.DateTime> TsnCreatedate { get { return this._tsnCreatedate; } set { this.__Set(ref this._tsnCreatedate, value); } }
		
        private string _tsnPmmCode;
        [Column("tsn_pmm_code", false, false, false)]
        public string TsnPmmCode { get { return this._tsnPmmCode; } set { this.__Set(ref this._tsnPmmCode, value); } }
	
		
        [InnerJoinColumn]
        public TblBillBll BllTsnTblBillBll { get; set; }
		
        [InnerJoinColumn]
        public TblOrderOrd OrdTsnTblOrderOrd { get; set; }
		
        [InnerJoinColumn]
        public TblTransactionattributeTsa TsaTsnTblTransactionattributeTsa { get; set; }
		
        [InnerJoinColumn]
        public TblUserUsr TsnUsr { get; set; }
		
        [LeftJoinColumn]
        public TblEvoboxEbx TsnEbx { get; set; }
	}

	[Table("tbl_useractivationkey_uak", "", "evoconcept")]
	public partial class TblUseractivationkeyUak : Entity<TblUseractivationkeyUak>
	{
		static TblUseractivationkeyUak()
		{
			Join<TblUserUsr>(t => t.UsrUakTblUserUsr, (t, f) => t.UakId == f.UsrUakId); // Reverse Relation
		}

		
        [Column("uak_id", true, true, false)]
        public int UakId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private string _uakActivationKey;
        [Column("uak_activation_key", false, false, true)]
        public string UakActivationKey { get { return this._uakActivationKey; } set { this.__Set(ref this._uakActivationKey, value); } }
		
        private System.Nullable<System.DateTime> _uakCreatedate;
        [Column("uak_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UakCreatedate { get { return this._uakCreatedate; } set { this.__Set(ref this._uakCreatedate, value); } }
	
		
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

		
        private int _uinUsrId;
        [Column("uin_usr_id", true, false, false)]
        public int UinUsrId { get { return this._uinUsrId; } set { this.__Set(ref this._uinUsrId, value); } }
		
        private System.Nullable<System.DateTime> _uinCreatedate;
        [Column("uin_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UinCreatedate { get { return this._uinCreatedate; } set { this.__Set(ref this._uinCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _uinUpdatedate;
        [Column("uin_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> UinUpdatedate { get { return this._uinUpdatedate; } set { this.__Set(ref this._uinUpdatedate, value); } }
		
        private string _uinLastname;
        [Column("uin_lastname", false, false, false)]
        public string UinLastname { get { return this._uinLastname; } set { this.__Set(ref this._uinLastname, value); } }
		
        private string _uinFirstname;
        [Column("uin_firstname", false, false, false)]
        public string UinFirstname { get { return this._uinFirstname; } set { this.__Set(ref this._uinFirstname, value); } }
		
        private string _uinCity;
        [Column("uin_city", false, false, false)]
        public string UinCity { get { return this._uinCity; } set { this.__Set(ref this._uinCity, value); } }
		
        private string _uinCnyCode;
        [Column("uin_cny_code", false, false, true)]
        public string UinCnyCode { get { return this._uinCnyCode; } set { this.__Set(ref this._uinCnyCode, value); } }
		
        private string _uinPostCode;
        [Column("uin_post_code", false, false, false)]
        public string UinPostCode { get { return this._uinPostCode; } set { this.__Set(ref this._uinPostCode, value); } }
		
        private string _uinAddress1;
        [Column("uin_address1", false, false, false)]
        public string UinAddress1 { get { return this._uinAddress1; } set { this.__Set(ref this._uinAddress1, value); } }
		
        private string _uinAddress2;
        [Column("uin_address2", false, false, true)]
        public string UinAddress2 { get { return this._uinAddress2; } set { this.__Set(ref this._uinAddress2, value); } }
		
        private string _uinAddress3;
        [Column("uin_address3", false, false, true)]
        public string UinAddress3 { get { return this._uinAddress3; } set { this.__Set(ref this._uinAddress3, value); } }
		
        private string _uinAddress4;
        [Column("uin_address4", false, false, true)]
        public string UinAddress4 { get { return this._uinAddress4; } set { this.__Set(ref this._uinAddress4, value); } }
	
		
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

		
        [Column("usr_id", true, true, false)]
        public int UsrId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<System.DateTime> _usrCreatedate;
        [Column("usr_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UsrCreatedate { get { return this._usrCreatedate; } set { this.__Set(ref this._usrCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _usrUpdatedate;
        [Column("usr_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> UsrUpdatedate { get { return this._usrUpdatedate; } set { this.__Set(ref this._usrUpdatedate, value); } }
		
        private string _usrPassword;
        [Column("usr_password", false, false, false)]
        public string UsrPassword { get { return this._usrPassword; } set { this.__Set(ref this._usrPassword, value); } }
		
        private string _usrEmail;
        [Column("usr_email", false, false, false)]
        public string UsrEmail { get { return this._usrEmail; } set { this.__Set(ref this._usrEmail, value); } }
		
        private string _usrLogin;
        [Column("usr_login", false, false, true)]
        public string UsrLogin { get { return this._usrLogin; } set { this.__Set(ref this._usrLogin, value); } }
		
        private sbyte _usrLocked;
        [Column("usr_locked", false, false, false)]
        public sbyte UsrLocked { get { return this._usrLocked; } set { this.__Set(ref this._usrLocked, value); } }
		
        private sbyte _usrEnabled;
        [Column("usr_enabled", false, false, false)]
        public sbyte UsrEnabled { get { return this._usrEnabled; } set { this.__Set(ref this._usrEnabled, value); } }
		
        private sbyte _usrAdmin;
        [Column("usr_admin", false, false, false)]
        public sbyte UsrAdmin { get { return this._usrAdmin; } set { this.__Set(ref this._usrAdmin, value); } }
		
        private int _usrCredit;
        [Column("usr_credit", false, false, false)]
        public int UsrCredit { get { return this._usrCredit; } set { this.__Set(ref this._usrCredit, value); } }
		
        private System.Nullable<int> _usrUakId;
        [Column("usr_uak_id", false, false, true)]
        public System.Nullable<int> UsrUakId { get { return this._usrUakId; } set { this.__Set(ref this._usrUakId, value); } }
	
		
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

		
        [Column("ubx_id", true, true, false)]
        public int UbxId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<System.DateTime> _ubxCreatedate;
        [Column("ubx_createdate", false, false, true)]
        public System.Nullable<System.DateTime> UbxCreatedate { get { return this._ubxCreatedate; } set { this.__Set(ref this._ubxCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _ubxUpdatedate;
        [Column("ubx_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> UbxUpdatedate { get { return this._ubxUpdatedate; } set { this.__Set(ref this._ubxUpdatedate, value); } }
		
        private int _ubxEbxId;
        [Column("ubx_ebx_id", false, false, false)]
        public int UbxEbxId { get { return this._ubxEbxId; } set { this.__Set(ref this._ubxEbxId, value); } }
		
        private int _ubxUsrId;
        [Column("ubx_usr_id", false, false, false)]
        public int UbxUsrId { get { return this._ubxUsrId; } set { this.__Set(ref this._ubxUsrId, value); } }
		
        private System.Nullable<sbyte> _ubxOwner;
        [Column("ubx_owner", false, false, true)]
        public System.Nullable<sbyte> UbxOwner { get { return this._ubxOwner; } set { this.__Set(ref this._ubxOwner, value); } }
	
		
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

		
        [Column("web_id", true, true, false)]
        public int WebId
        {
	        get { return this._entityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this._entityIdentity, typeof(int)); }
	        set { this.__Set(ref this._entityIdentity, (int)value); }
        }
		
        private System.Nullable<System.DateTime> _webCreatedate;
        [Column("web_createdate", false, false, true)]
        public System.Nullable<System.DateTime> WebCreatedate { get { return this._webCreatedate; } set { this.__Set(ref this._webCreatedate, value); } }
		
        private System.Nullable<System.DateTime> _webUpdatedate;
        [Column("web_updatedate", false, false, true)]
        public System.Nullable<System.DateTime> WebUpdatedate { get { return this._webUpdatedate; } set { this.__Set(ref this._webUpdatedate, value); } }
		
        private int _webDomId;
        [Column("web_dom_id", false, false, false)]
        public int WebDomId { get { return this._webDomId; } set { this.__Set(ref this._webDomId, value); } }
		
        private int _webAppId;
        [Column("web_app_id", false, false, false)]
        public int WebAppId { get { return this._webAppId; } set { this.__Set(ref this._webAppId, value); } }
	
		
        [InnerJoinColumn]
        public TblApplicationApp WebApp { get; set; }
		
        [InnerJoinColumn]
        public TblDomainDom WebDom { get; set; }
	}


}

