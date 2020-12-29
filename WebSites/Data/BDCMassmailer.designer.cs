#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GA.BDC.Data
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="rd_mailer")]
	public partial class BDCMassmailerDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void Insertemail_queue(email_queue instance);
    partial void Updateemail_queue(email_queue instance);
    partial void Deleteemail_queue(email_queue instance);
    partial void Insertemail_activity(email_activity instance);
    partial void Updateemail_activity(email_activity instance);
    partial void Deleteemail_activity(email_activity instance);
    #endregion
		
		public BDCMassmailerDataContext() : 
				base(global::GA.BDC.Data.Properties.Settings.Default.RD_MailerConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public BDCMassmailerDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BDCMassmailerDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BDCMassmailerDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BDCMassmailerDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<email_queue> email_queues
		{
			get
			{
				return this.GetTable<email_queue>();
			}
		}
		
		public System.Data.Linq.Table<email_activity> email_activities
		{
			get
			{
				return this.GetTable<email_activity>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.bdc_get_email_queue")]
		public ISingleResult<bdc_get_email_queueResult1> bdc_get_email_queue()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<bdc_get_email_queueResult1>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.bdc_get_latest_email_status")]
		public ISingleResult<bdc_get_latest_email_statusResult> bdc_get_latest_email_status([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="SmallInt")] System.Nullable<short> email_status, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="SmallInt")] System.Nullable<short> project_id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="SmallInt")] System.Nullable<short> retention)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), email_status, project_id, retention);
			return ((ISingleResult<bdc_get_latest_email_statusResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.bdc_get_latest_email_activity")]
		public ISingleResult<bdc_get_latest_email_activityResult> bdc_get_latest_email_activity([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> touch_id, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="SmallInt")] System.Nullable<short> project_id)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), touch_id, project_id);
			return ((ISingleResult<bdc_get_latest_email_activityResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.bdc_update_email_status_by_external_id")]
		public ISingleResult<bdc_update_email_status_by_external_idResult> bdc_update_email_status_by_external_id([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> externalId, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> newStatus)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), externalId, newStatus);
			return ((ISingleResult<bdc_update_email_status_by_external_idResult>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.email_queue")]
	public partial class email_queue : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _queue_id;
		
		private int _source_id;
		
		private short _project_id;
		
		private string _reply_to_name;
		
		private string _reply_to_email;
		
		private string _to_name;
		
		private string _to_email;
		
		private string _from_email;
		
		private string _from_name;
		
		private string _bounce_email;
		
		private string _bounce_name;
		
		private string _cc_email;
		
		private string _cc_name;
		
		private string _bcc_email;
		
		private string _bcc_name;
		
		private string _subject;
		
		private string _bodytxt;
		
		private string _bodyhtml;
		
		private byte _sent_status_id;
		
		private string _error_message;
		
		private System.DateTime _datestamp;
		
		private byte _priority;
		
		private System.Nullable<int> _komunik_return_value_id;
		
		private string _extra_info;
		
		private System.Nullable<System.DateTime> _timesent;
		
		private System.Nullable<int> _ext_email_id;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onqueue_idChanging(int value);
    partial void Onqueue_idChanged();
    partial void Onsource_idChanging(int value);
    partial void Onsource_idChanged();
    partial void Onproject_idChanging(short value);
    partial void Onproject_idChanged();
    partial void Onreply_to_nameChanging(string value);
    partial void Onreply_to_nameChanged();
    partial void Onreply_to_emailChanging(string value);
    partial void Onreply_to_emailChanged();
    partial void Onto_nameChanging(string value);
    partial void Onto_nameChanged();
    partial void Onto_emailChanging(string value);
    partial void Onto_emailChanged();
    partial void Onfrom_emailChanging(string value);
    partial void Onfrom_emailChanged();
    partial void Onfrom_nameChanging(string value);
    partial void Onfrom_nameChanged();
    partial void Onbounce_emailChanging(string value);
    partial void Onbounce_emailChanged();
    partial void Onbounce_nameChanging(string value);
    partial void Onbounce_nameChanged();
    partial void Oncc_emailChanging(string value);
    partial void Oncc_emailChanged();
    partial void Oncc_nameChanging(string value);
    partial void Oncc_nameChanged();
    partial void Onbcc_emailChanging(string value);
    partial void Onbcc_emailChanged();
    partial void Onbcc_nameChanging(string value);
    partial void Onbcc_nameChanged();
    partial void OnsubjectChanging(string value);
    partial void OnsubjectChanged();
    partial void OnbodytxtChanging(string value);
    partial void OnbodytxtChanged();
    partial void OnbodyhtmlChanging(string value);
    partial void OnbodyhtmlChanged();
    partial void Onsent_status_idChanging(byte value);
    partial void Onsent_status_idChanged();
    partial void Onerror_messageChanging(string value);
    partial void Onerror_messageChanged();
    partial void OndatestampChanging(System.DateTime value);
    partial void OndatestampChanged();
    partial void OnpriorityChanging(byte value);
    partial void OnpriorityChanged();
    partial void Onkomunik_return_value_idChanging(System.Nullable<int> value);
    partial void Onkomunik_return_value_idChanged();
    partial void Onextra_infoChanging(string value);
    partial void Onextra_infoChanged();
    partial void OntimesentChanging(System.Nullable<System.DateTime> value);
    partial void OntimesentChanged();
    partial void Onext_email_idChanging(System.Nullable<int> value);
    partial void Onext_email_idChanged();
    #endregion
		
		public email_queue()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_queue_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int queue_id
		{
			get
			{
				return this._queue_id;
			}
			set
			{
				if ((this._queue_id != value))
				{
					this.Onqueue_idChanging(value);
					this.SendPropertyChanging();
					this._queue_id = value;
					this.SendPropertyChanged("queue_id");
					this.Onqueue_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_source_id", DbType="Int NOT NULL")]
		public int source_id
		{
			get
			{
				return this._source_id;
			}
			set
			{
				if ((this._source_id != value))
				{
					this.Onsource_idChanging(value);
					this.SendPropertyChanging();
					this._source_id = value;
					this.SendPropertyChanged("source_id");
					this.Onsource_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_project_id", DbType="SmallInt NOT NULL")]
		public short project_id
		{
			get
			{
				return this._project_id;
			}
			set
			{
				if ((this._project_id != value))
				{
					this.Onproject_idChanging(value);
					this.SendPropertyChanging();
					this._project_id = value;
					this.SendPropertyChanged("project_id");
					this.Onproject_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reply_to_name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string reply_to_name
		{
			get
			{
				return this._reply_to_name;
			}
			set
			{
				if ((this._reply_to_name != value))
				{
					this.Onreply_to_nameChanging(value);
					this.SendPropertyChanging();
					this._reply_to_name = value;
					this.SendPropertyChanged("reply_to_name");
					this.Onreply_to_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reply_to_email", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string reply_to_email
		{
			get
			{
				return this._reply_to_email;
			}
			set
			{
				if ((this._reply_to_email != value))
				{
					this.Onreply_to_emailChanging(value);
					this.SendPropertyChanging();
					this._reply_to_email = value;
					this.SendPropertyChanged("reply_to_email");
					this.Onreply_to_emailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_to_name", DbType="VarChar(100)")]
		public string to_name
		{
			get
			{
				return this._to_name;
			}
			set
			{
				if ((this._to_name != value))
				{
					this.Onto_nameChanging(value);
					this.SendPropertyChanging();
					this._to_name = value;
					this.SendPropertyChanged("to_name");
					this.Onto_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_to_email", DbType="VarChar(100)")]
		public string to_email
		{
			get
			{
				return this._to_email;
			}
			set
			{
				if ((this._to_email != value))
				{
					this.Onto_emailChanging(value);
					this.SendPropertyChanging();
					this._to_email = value;
					this.SendPropertyChanged("to_email");
					this.Onto_emailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_from_email", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string from_email
		{
			get
			{
				return this._from_email;
			}
			set
			{
				if ((this._from_email != value))
				{
					this.Onfrom_emailChanging(value);
					this.SendPropertyChanging();
					this._from_email = value;
					this.SendPropertyChanged("from_email");
					this.Onfrom_emailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_from_name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string from_name
		{
			get
			{
				return this._from_name;
			}
			set
			{
				if ((this._from_name != value))
				{
					this.Onfrom_nameChanging(value);
					this.SendPropertyChanging();
					this._from_name = value;
					this.SendPropertyChanged("from_name");
					this.Onfrom_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bounce_email", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string bounce_email
		{
			get
			{
				return this._bounce_email;
			}
			set
			{
				if ((this._bounce_email != value))
				{
					this.Onbounce_emailChanging(value);
					this.SendPropertyChanging();
					this._bounce_email = value;
					this.SendPropertyChanged("bounce_email");
					this.Onbounce_emailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bounce_name", DbType="VarChar(100)")]
		public string bounce_name
		{
			get
			{
				return this._bounce_name;
			}
			set
			{
				if ((this._bounce_name != value))
				{
					this.Onbounce_nameChanging(value);
					this.SendPropertyChanging();
					this._bounce_name = value;
					this.SendPropertyChanged("bounce_name");
					this.Onbounce_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cc_email", DbType="VarChar(100)")]
		public string cc_email
		{
			get
			{
				return this._cc_email;
			}
			set
			{
				if ((this._cc_email != value))
				{
					this.Oncc_emailChanging(value);
					this.SendPropertyChanging();
					this._cc_email = value;
					this.SendPropertyChanged("cc_email");
					this.Oncc_emailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cc_name", DbType="VarChar(100)")]
		public string cc_name
		{
			get
			{
				return this._cc_name;
			}
			set
			{
				if ((this._cc_name != value))
				{
					this.Oncc_nameChanging(value);
					this.SendPropertyChanging();
					this._cc_name = value;
					this.SendPropertyChanged("cc_name");
					this.Oncc_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bcc_email", DbType="VarChar(100)")]
		public string bcc_email
		{
			get
			{
				return this._bcc_email;
			}
			set
			{
				if ((this._bcc_email != value))
				{
					this.Onbcc_emailChanging(value);
					this.SendPropertyChanging();
					this._bcc_email = value;
					this.SendPropertyChanged("bcc_email");
					this.Onbcc_emailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bcc_name", DbType="VarChar(100)")]
		public string bcc_name
		{
			get
			{
				return this._bcc_name;
			}
			set
			{
				if ((this._bcc_name != value))
				{
					this.Onbcc_nameChanging(value);
					this.SendPropertyChanging();
					this._bcc_name = value;
					this.SendPropertyChanged("bcc_name");
					this.Onbcc_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_subject", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string subject
		{
			get
			{
				return this._subject;
			}
			set
			{
				if ((this._subject != value))
				{
					this.OnsubjectChanging(value);
					this.SendPropertyChanging();
					this._subject = value;
					this.SendPropertyChanged("subject");
					this.OnsubjectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bodytxt", DbType="Text NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string bodytxt
		{
			get
			{
				return this._bodytxt;
			}
			set
			{
				if ((this._bodytxt != value))
				{
					this.OnbodytxtChanging(value);
					this.SendPropertyChanging();
					this._bodytxt = value;
					this.SendPropertyChanged("bodytxt");
					this.OnbodytxtChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bodyhtml", DbType="Text NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string bodyhtml
		{
			get
			{
				return this._bodyhtml;
			}
			set
			{
				if ((this._bodyhtml != value))
				{
					this.OnbodyhtmlChanging(value);
					this.SendPropertyChanging();
					this._bodyhtml = value;
					this.SendPropertyChanged("bodyhtml");
					this.OnbodyhtmlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sent_status_id", DbType="TinyInt NOT NULL")]
		public byte sent_status_id
		{
			get
			{
				return this._sent_status_id;
			}
			set
			{
				if ((this._sent_status_id != value))
				{
					this.Onsent_status_idChanging(value);
					this.SendPropertyChanging();
					this._sent_status_id = value;
					this.SendPropertyChanged("sent_status_id");
					this.Onsent_status_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_error_message", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string error_message
		{
			get
			{
				return this._error_message;
			}
			set
			{
				if ((this._error_message != value))
				{
					this.Onerror_messageChanging(value);
					this.SendPropertyChanging();
					this._error_message = value;
					this.SendPropertyChanged("error_message");
					this.Onerror_messageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_datestamp", DbType="DateTime NOT NULL")]
		public System.DateTime datestamp
		{
			get
			{
				return this._datestamp;
			}
			set
			{
				if ((this._datestamp != value))
				{
					this.OndatestampChanging(value);
					this.SendPropertyChanging();
					this._datestamp = value;
					this.SendPropertyChanged("datestamp");
					this.OndatestampChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_priority", DbType="TinyInt NOT NULL")]
		public byte priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				if ((this._priority != value))
				{
					this.OnpriorityChanging(value);
					this.SendPropertyChanging();
					this._priority = value;
					this.SendPropertyChanged("priority");
					this.OnpriorityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_komunik_return_value_id", DbType="Int")]
		public System.Nullable<int> komunik_return_value_id
		{
			get
			{
				return this._komunik_return_value_id;
			}
			set
			{
				if ((this._komunik_return_value_id != value))
				{
					this.Onkomunik_return_value_idChanging(value);
					this.SendPropertyChanging();
					this._komunik_return_value_id = value;
					this.SendPropertyChanged("komunik_return_value_id");
					this.Onkomunik_return_value_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_extra_info", DbType="VarChar(2000)")]
		public string extra_info
		{
			get
			{
				return this._extra_info;
			}
			set
			{
				if ((this._extra_info != value))
				{
					this.Onextra_infoChanging(value);
					this.SendPropertyChanging();
					this._extra_info = value;
					this.SendPropertyChanged("extra_info");
					this.Onextra_infoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_timesent", DbType="DateTime")]
		public System.Nullable<System.DateTime> timesent
		{
			get
			{
				return this._timesent;
			}
			set
			{
				if ((this._timesent != value))
				{
					this.OntimesentChanging(value);
					this.SendPropertyChanging();
					this._timesent = value;
					this.SendPropertyChanged("timesent");
					this.OntimesentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ext_email_id", DbType="Int")]
		public System.Nullable<int> ext_email_id
		{
			get
			{
				return this._ext_email_id;
			}
			set
			{
				if ((this._ext_email_id != value))
				{
					this.Onext_email_idChanging(value);
					this.SendPropertyChanging();
					this._ext_email_id = value;
					this.SendPropertyChanged("ext_email_id");
					this.Onext_email_idChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.email_activity")]
	public partial class email_activity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _email_activity_id;
		
		private int _touch_id;
		
		private int _project_id;
		
		private int _email_template_id;
		
		private System.DateTime _email_activity_date;
		
		private int _action_id;
		
		private string _action_desc;
		
		private int _batch_id;
		
		private System.DateTime _create_date;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onemail_activity_idChanging(int value);
    partial void Onemail_activity_idChanged();
    partial void Ontouch_idChanging(int value);
    partial void Ontouch_idChanged();
    partial void Onproject_idChanging(int value);
    partial void Onproject_idChanged();
    partial void Onemail_template_idChanging(int value);
    partial void Onemail_template_idChanged();
    partial void Onemail_activity_dateChanging(System.DateTime value);
    partial void Onemail_activity_dateChanged();
    partial void Onaction_idChanging(int value);
    partial void Onaction_idChanged();
    partial void Onaction_descChanging(string value);
    partial void Onaction_descChanged();
    partial void Onbatch_idChanging(int value);
    partial void Onbatch_idChanged();
    partial void Oncreate_dateChanging(System.DateTime value);
    partial void Oncreate_dateChanged();
    #endregion
		
		public email_activity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email_activity_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int email_activity_id
		{
			get
			{
				return this._email_activity_id;
			}
			set
			{
				if ((this._email_activity_id != value))
				{
					this.Onemail_activity_idChanging(value);
					this.SendPropertyChanging();
					this._email_activity_id = value;
					this.SendPropertyChanged("email_activity_id");
					this.Onemail_activity_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_touch_id", DbType="Int NOT NULL")]
		public int touch_id
		{
			get
			{
				return this._touch_id;
			}
			set
			{
				if ((this._touch_id != value))
				{
					this.Ontouch_idChanging(value);
					this.SendPropertyChanging();
					this._touch_id = value;
					this.SendPropertyChanged("touch_id");
					this.Ontouch_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_project_id", DbType="Int NOT NULL")]
		public int project_id
		{
			get
			{
				return this._project_id;
			}
			set
			{
				if ((this._project_id != value))
				{
					this.Onproject_idChanging(value);
					this.SendPropertyChanging();
					this._project_id = value;
					this.SendPropertyChanged("project_id");
					this.Onproject_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email_template_id", DbType="Int NOT NULL")]
		public int email_template_id
		{
			get
			{
				return this._email_template_id;
			}
			set
			{
				if ((this._email_template_id != value))
				{
					this.Onemail_template_idChanging(value);
					this.SendPropertyChanging();
					this._email_template_id = value;
					this.SendPropertyChanged("email_template_id");
					this.Onemail_template_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_email_activity_date", DbType="DateTime NOT NULL")]
		public System.DateTime email_activity_date
		{
			get
			{
				return this._email_activity_date;
			}
			set
			{
				if ((this._email_activity_date != value))
				{
					this.Onemail_activity_dateChanging(value);
					this.SendPropertyChanging();
					this._email_activity_date = value;
					this.SendPropertyChanged("email_activity_date");
					this.Onemail_activity_dateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_action_id", DbType="Int NOT NULL")]
		public int action_id
		{
			get
			{
				return this._action_id;
			}
			set
			{
				if ((this._action_id != value))
				{
					this.Onaction_idChanging(value);
					this.SendPropertyChanging();
					this._action_id = value;
					this.SendPropertyChanged("action_id");
					this.Onaction_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_action_desc", DbType="VarChar(255)")]
		public string action_desc
		{
			get
			{
				return this._action_desc;
			}
			set
			{
				if ((this._action_desc != value))
				{
					this.Onaction_descChanging(value);
					this.SendPropertyChanging();
					this._action_desc = value;
					this.SendPropertyChanged("action_desc");
					this.Onaction_descChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_batch_id", DbType="Int NOT NULL")]
		public int batch_id
		{
			get
			{
				return this._batch_id;
			}
			set
			{
				if ((this._batch_id != value))
				{
					this.Onbatch_idChanging(value);
					this.SendPropertyChanging();
					this._batch_id = value;
					this.SendPropertyChanged("batch_id");
					this.Onbatch_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_create_date", DbType="DateTime NOT NULL")]
		public System.DateTime create_date
		{
			get
			{
				return this._create_date;
			}
			set
			{
				if ((this._create_date != value))
				{
					this.Oncreate_dateChanging(value);
					this.SendPropertyChanging();
					this._create_date = value;
					this.SendPropertyChanged("create_date");
					this.Oncreate_dateChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	public partial class bdc_get_email_queueResult1
	{
		
		private int _queue_id;
		
		private int _source_id;
		
		private int _project_id;
		
		private string _reply_to_name;
		
		private string _reply_to_email;
		
		private string _to_name;
		
		private string _to_email;
		
		private string _from_email;
		
		private string _from_name;
		
		private string _bounce_email;
		
		private string _bounce_name;
		
		private string _cc_email;
		
		private string _cc_name;
		
		private string _bcc_email;
		
		private string _bcc_name;
		
		private string _subject;
		
		private string _bodytxt;
		
		private string _bodyhtml;
		
		private string _extra_info;
		
		private byte _sent_status_id;
		
		private System.DateTime _datestamp;
		
		private byte _priority;
		
		private System.Nullable<int> _komunik_return_value_id;
		
		public bdc_get_email_queueResult1()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_queue_id", DbType="Int NOT NULL")]
		public int queue_id
		{
			get
			{
				return this._queue_id;
			}
			set
			{
				if ((this._queue_id != value))
				{
					this._queue_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_source_id", DbType="Int NOT NULL")]
		public int source_id
		{
			get
			{
				return this._source_id;
			}
			set
			{
				if ((this._source_id != value))
				{
					this._source_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_project_id", DbType="Int NOT NULL")]
		public int project_id
		{
			get
			{
				return this._project_id;
			}
			set
			{
				if ((this._project_id != value))
				{
					this._project_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reply_to_name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string reply_to_name
		{
			get
			{
				return this._reply_to_name;
			}
			set
			{
				if ((this._reply_to_name != value))
				{
					this._reply_to_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_reply_to_email", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string reply_to_email
		{
			get
			{
				return this._reply_to_email;
			}
			set
			{
				if ((this._reply_to_email != value))
				{
					this._reply_to_email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_to_name", DbType="VarChar(100)")]
		public string to_name
		{
			get
			{
				return this._to_name;
			}
			set
			{
				if ((this._to_name != value))
				{
					this._to_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_to_email", DbType="VarChar(100)")]
		public string to_email
		{
			get
			{
				return this._to_email;
			}
			set
			{
				if ((this._to_email != value))
				{
					this._to_email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_from_email", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string from_email
		{
			get
			{
				return this._from_email;
			}
			set
			{
				if ((this._from_email != value))
				{
					this._from_email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_from_name", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string from_name
		{
			get
			{
				return this._from_name;
			}
			set
			{
				if ((this._from_name != value))
				{
					this._from_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bounce_email", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string bounce_email
		{
			get
			{
				return this._bounce_email;
			}
			set
			{
				if ((this._bounce_email != value))
				{
					this._bounce_email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bounce_name", DbType="VarChar(100)")]
		public string bounce_name
		{
			get
			{
				return this._bounce_name;
			}
			set
			{
				if ((this._bounce_name != value))
				{
					this._bounce_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cc_email", DbType="VarChar(100)")]
		public string cc_email
		{
			get
			{
				return this._cc_email;
			}
			set
			{
				if ((this._cc_email != value))
				{
					this._cc_email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_cc_name", DbType="VarChar(100)")]
		public string cc_name
		{
			get
			{
				return this._cc_name;
			}
			set
			{
				if ((this._cc_name != value))
				{
					this._cc_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bcc_email", DbType="VarChar(100)")]
		public string bcc_email
		{
			get
			{
				return this._bcc_email;
			}
			set
			{
				if ((this._bcc_email != value))
				{
					this._bcc_email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bcc_name", DbType="VarChar(100)")]
		public string bcc_name
		{
			get
			{
				return this._bcc_name;
			}
			set
			{
				if ((this._bcc_name != value))
				{
					this._bcc_name = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_subject", DbType="VarChar(255) NOT NULL", CanBeNull=false)]
		public string subject
		{
			get
			{
				return this._subject;
			}
			set
			{
				if ((this._subject != value))
				{
					this._subject = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bodytxt", DbType="Text NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string bodytxt
		{
			get
			{
				return this._bodytxt;
			}
			set
			{
				if ((this._bodytxt != value))
				{
					this._bodytxt = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bodyhtml", DbType="Text NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string bodyhtml
		{
			get
			{
				return this._bodyhtml;
			}
			set
			{
				if ((this._bodyhtml != value))
				{
					this._bodyhtml = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_extra_info", DbType="VarChar(2000)")]
		public string extra_info
		{
			get
			{
				return this._extra_info;
			}
			set
			{
				if ((this._extra_info != value))
				{
					this._extra_info = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sent_status_id", DbType="TinyInt NOT NULL")]
		public byte sent_status_id
		{
			get
			{
				return this._sent_status_id;
			}
			set
			{
				if ((this._sent_status_id != value))
				{
					this._sent_status_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_datestamp", DbType="DateTime NOT NULL")]
		public System.DateTime datestamp
		{
			get
			{
				return this._datestamp;
			}
			set
			{
				if ((this._datestamp != value))
				{
					this._datestamp = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_priority", DbType="TinyInt NOT NULL")]
		public byte priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				if ((this._priority != value))
				{
					this._priority = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_komunik_return_value_id", DbType="Int")]
		public System.Nullable<int> komunik_return_value_id
		{
			get
			{
				return this._komunik_return_value_id;
			}
			set
			{
				if ((this._komunik_return_value_id != value))
				{
					this._komunik_return_value_id = value;
				}
			}
		}
	}
	
	public partial class bdc_get_latest_email_statusResult
	{
		
		private int _touch_id;
		
		public bdc_get_latest_email_statusResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_touch_id", DbType="Int NOT NULL")]
		public int touch_id
		{
			get
			{
				return this._touch_id;
			}
			set
			{
				if ((this._touch_id != value))
				{
					this._touch_id = value;
				}
			}
		}
	}
	
	public partial class bdc_get_latest_email_activityResult
	{
		
		private int _action_id;
		
		public bdc_get_latest_email_activityResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_action_id", DbType="Int NOT NULL")]
		public int action_id
		{
			get
			{
				return this._action_id;
			}
			set
			{
				if ((this._action_id != value))
				{
					this._action_id = value;
				}
			}
		}
	}
	
	public partial class bdc_update_email_status_by_external_idResult
	{
		
		private int _queue_id;
		
		private int _source_id;
		
		private short _project_id;
		
		private byte _sent_status_id;
		
		private System.DateTime _datestamp;
		
		public bdc_update_email_status_by_external_idResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_queue_id", DbType="Int NOT NULL")]
		public int queue_id
		{
			get
			{
				return this._queue_id;
			}
			set
			{
				if ((this._queue_id != value))
				{
					this._queue_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_source_id", DbType="Int NOT NULL")]
		public int source_id
		{
			get
			{
				return this._source_id;
			}
			set
			{
				if ((this._source_id != value))
				{
					this._source_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_project_id", DbType="SmallInt NOT NULL")]
		public short project_id
		{
			get
			{
				return this._project_id;
			}
			set
			{
				if ((this._project_id != value))
				{
					this._project_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_sent_status_id", DbType="TinyInt NOT NULL")]
		public byte sent_status_id
		{
			get
			{
				return this._sent_status_id;
			}
			set
			{
				if ((this._sent_status_id != value))
				{
					this._sent_status_id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_datestamp", DbType="DateTime NOT NULL")]
		public System.DateTime datestamp
		{
			get
			{
				return this._datestamp;
			}
			set
			{
				if ((this._datestamp != value))
				{
					this._datestamp = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
