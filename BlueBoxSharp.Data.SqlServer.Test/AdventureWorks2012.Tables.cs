

using System;
using System.Collections.Generic;
using BlueBoxSharp.Data;
using BlueBoxSharp.Data.Entity;
using BlueBoxSharp.Helpers;
using BlueBoxSharp.ComponentModel.DataAnnotations;

namespace BlueBoxSharp.Data.SqlServer.Test.AdventureWorks2012
{
	[Table("AWBuildVersion", "", "AdventureWorks2012")]
	public partial class AWBuildVersion : Entity<AWBuildVersion>
	{
		static AWBuildVersion()
		{
		}

		
        [NotifyPropertyChanged, Column("SystemInformationID", true, true, false)]
        public byte SystemInformationID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<byte>() : (byte)Convert.ChangeType(this.EntityIdentity, typeof(byte)); }
	        set { this.EntityIdentity = (byte)value; }
        }
		
        [NotifyPropertyChanged, Column("Database Version", false, false, false)]
        public string DatabaseVersion { get; set; }
		
        [NotifyPropertyChanged, Column("VersionDate", false, false, false)]
        public System.DateTime VersionDate { get; set; }
		
        [NotifyPropertyChanged, Column("ModifiedDate", false, false, false)]
        public System.DateTime ModifiedDate { get; set; }
	
	}

	[Table("DatabaseLog", "", "AdventureWorks2012")]
	public partial class DatabaseLog : Entity<DatabaseLog>
	{
		static DatabaseLog()
		{
		}

		
        [NotifyPropertyChanged, Column("DatabaseLogID", true, true, false)]
        public int DatabaseLogID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("PostTime", false, false, false)]
        public System.DateTime PostTime { get; set; }
		
        [NotifyPropertyChanged, Column("DatabaseUser", false, false, false)]
        public string DatabaseUser { get; set; }
		
        [NotifyPropertyChanged, Column("Event", false, false, false)]
        public string Event { get; set; }
		
        [NotifyPropertyChanged, Column("Schema", false, false, true)]
        public string Schema { get; set; }
		
        [NotifyPropertyChanged, Column("Object", false, false, true)]
        public string Object { get; set; }
		
        [NotifyPropertyChanged, Column("TSQL", false, false, false)]
        public string TSQL { get; set; }
		
        [NotifyPropertyChanged, Column("XmlEvent", false, false, false)]
        public object XmlEvent { get; set; }
	
	}

	[Table("ErrorLog", "", "AdventureWorks2012")]
	public partial class ErrorLog : Entity<ErrorLog>
	{
		static ErrorLog()
		{
		}

		
        [NotifyPropertyChanged, Column("ErrorLogID", true, true, false)]
        public int ErrorLogID
        {
	        get { return this.EntityIdentity == null ? TypeHelper.GetDefault<int>() : (int)Convert.ChangeType(this.EntityIdentity, typeof(int)); }
	        set { this.EntityIdentity = (int)value; }
        }
		
        [NotifyPropertyChanged, Column("ErrorTime", false, false, false)]
        public System.DateTime ErrorTime { get; set; }
		
        [NotifyPropertyChanged, Column("UserName", false, false, false)]
        public string UserName { get; set; }
		
        [NotifyPropertyChanged, Column("ErrorNumber", false, false, false)]
        public int ErrorNumber { get; set; }
		
        [NotifyPropertyChanged, Column("ErrorSeverity", false, false, true)]
        public System.Nullable<int> ErrorSeverity { get; set; }
		
        [NotifyPropertyChanged, Column("ErrorState", false, false, true)]
        public System.Nullable<int> ErrorState { get; set; }
		
        [NotifyPropertyChanged, Column("ErrorProcedure", false, false, true)]
        public string ErrorProcedure { get; set; }
		
        [NotifyPropertyChanged, Column("ErrorLine", false, false, true)]
        public System.Nullable<int> ErrorLine { get; set; }
		
        [NotifyPropertyChanged, Column("ErrorMessage", false, false, false)]
        public string ErrorMessage { get; set; }
	
	}


}

