using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of CampaignData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type BusinessNotificationTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class BusinessNotificationTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_BUSINESS_NOTIFICATION = "business_notification";
		/// <value>The constant used for PKId business_notification in the Order table. </value>
		public const String FLD_PKID = "business_notification_id";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_BUSINESS_NOTIFICATION_NAME = "business_notification_name";
		/// <value>The constant used for "Form Code" business_notification in the Order table. </value>
		public const String FLD_BUSINESS_NOTIFICATION_TYPE_ID = "business_notification_type_id";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_BUSINESS_NOTIFICATION_TYPE_NAME = "business_notification_type_name";
		/// <value>The constant used for "Form Code" business_notification in the Order table. </value>
		public const String FLD_SOURCE_ID = "source_id";
		/// <value>The constant used for "Form Code" business_notification in the Order table. </value>
		public const String FLD_BUSINESS_TASK_ID = "business_task_id";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_BUSINESS_TASK_NAME = "business_task_name";
		/// <value>The constant used for "Form Code" business_notification in the Order table. </value>
		public const String FLD_ASSIGNED_USER_ID = "assigned_user_id";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_ASSIGNED_USER_NAME = "assigned_user_name";
		/// <value>The constant used for "Form Code" business_notification in the Order table. </value>
		public const String FLD_ENTITY_ID = "entity_id";
		/// <value>The constant used for "Form Code" business_notification in the Order table. </value>
		public const String FLD_ENTITY_TYPE_ID = "entity_type_id";
		/// <value>The constant used for "Form Code" business_notification in the Order table. </value>
		public const String FLD_ENTITY_TYPE_NAME = "entity_type_name";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_SUBJECT = "subject";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_MESSAGE = "message";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_IS_COMPLETE = "is_complete";
		/// <value>The constant used for the order "Form Name" business_notification in the Order table. </value>
		public const String FLD_COMPLETE_DATE = "complete_date";

        public const String FLD_DELETED = "deleted";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_USER_ID = "create_user_id";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_USER_NAME = "create_user_name";
        /// <value>The constant used for "create date" field in the Collection Days table. </value>
        public const String FLD_CREATE_DATE = "create_date";
        /// <value>The constant used for "update user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_USER_ID = "update_user_id";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_USER_NAME = "update_user_name";
        /// <value>The constant used for "update date" field in the Collection Days table. </value>
        public const String FLD_UPDATE_DATE = "update_date";
    
        
		public BusinessNotificationTable() : 
                base(TBL_BUSINESS_NOTIFICATION) {
            this.InitClass();
        }
		    
        public BusinessNotificationTable(DataTable table) : 
                base(table.TableName) {
            if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                this.CaseSensitive = table.CaseSensitive;
            }
            if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                this.Locale = table.Locale;
            }
            if ((table.Namespace != table.DataSet.Namespace)) {
                this.Namespace = table.Namespace;
            }
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
            this.DisplayExpression = table.DisplayExpression;
        }
        
		protected BusinessNotificationTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            BusinessNotificationTable cln = ((BusinessNotificationTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new BusinessNotificationTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_BUSINESS_NOTIFICATION;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_BUSINESS_NOTIFICATION_NAME, typeof(System.String)).DefaultValue = "New Note";
			columns.Add(FLD_BUSINESS_NOTIFICATION_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_BUSINESS_NOTIFICATION_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_SOURCE_ID, typeof(System.Int32)).DefaultValue = 1;
			columns.Add(FLD_BUSINESS_TASK_ID, typeof(System.Int32));
			columns.Add(FLD_BUSINESS_TASK_NAME, typeof(System.String));
			columns.Add(FLD_ASSIGNED_USER_ID, typeof(System.Int32));
			columns.Add(FLD_ASSIGNED_USER_NAME, typeof(System.String));
			columns.Add(FLD_ENTITY_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_SUBJECT, typeof(System.String));
			columns.Add(FLD_MESSAGE, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_IS_COMPLETE, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_COMPLETE_DATE, typeof(System.DateTime));
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_CREATE_USER_NAME, typeof(System.String));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_UPDATE_USER_NAME, typeof(System.String));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
