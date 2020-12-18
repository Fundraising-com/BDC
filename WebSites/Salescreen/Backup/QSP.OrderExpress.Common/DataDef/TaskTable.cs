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
	///         The serializale constructor allows objects of type TaskTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class TaskTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_TASK = "task";
		/// <value>The constant used for PKId task in the Order table. </value>
		public const String FLD_PKID = "task_id";
		/// <value>The constant used for the order "Form Name" task in the Order table. </value>
		public const String FLD_TASK_NAME = "task_name";
		/// <value>The constant used for "Form Code" task in the Order table. </value>
		public const String FLD_TASK_TYPE_ID = "task_type_id";
		/// <value>The constant used for the order "Form Name" task in the Order table. </value>
		public const String FLD_TASK_TYPE_NAME = "task_type_name";
		/// <value>The constant used for "Form Code" task in the Order table. </value>
		public const String FLD_TEMPLATE_EMAIL_ID = "template_email_id";
		/// <value>The constant used for the order "Form Name" task in the Order table. </value>
		public const String FLD_TEMPLATE_EMAIL_NAME = "template_email_name";
		/// <value>The constant used for the order "Description" task in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
        /// <value>The constant used for "Form Code" task in the Order table. </value>
        public const String FLD_BUSINESS_NOTIFICATION_TYPE_ID = "business_notification_type_id";
        /// <value>The constant used for the order "Form Name" task in the Order table. </value>
        public const String FLD_BUSINESS_NOTIFICATION_TYPE_NAME = "business_notification_type_name";
        /// <value>The constant used for the order "Description" template_email in the Order table. </value>
		public const String FLD_TASK_SP = "task_sp";
		/// <value>The constant used for the order "Description" template_email in the Order table. </value>
		public const String FLD_PARAMETER_NAME = "parameter_name";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" task in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" task in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" task in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public TaskTable() : 
                base(TBL_TASK) {
            this.InitClass();
        }
		    
        public TaskTable(DataTable table) : 
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
        
		protected TaskTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            TaskTable cln = ((TaskTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new TaskTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_TASK;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_TASK_NAME, typeof(System.String));
			columns.Add(FLD_TASK_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_TASK_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_TEMPLATE_EMAIL_ID, typeof(System.Int32));
			columns.Add(FLD_TEMPLATE_EMAIL_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_BUSINESS_NOTIFICATION_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_BUSINESS_NOTIFICATION_TYPE_NAME, typeof(System.String));
            columns.Add(FLD_TASK_SP, typeof(System.String));
			columns.Add(FLD_PARAMETER_NAME, typeof(System.String));
			
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
