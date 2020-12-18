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
	///         The serializale constructor allows objects of type BusinessTaskTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class BusinessTaskTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Tasks Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_BUSINESS_TASK = "business_task";
		/// <value>The constant used for PKId task in the Order table. </value>
		public const String FLD_PKID = "business_task_id";
		/// <value>The constant used for "Form Code" task in the Order table. </value>
		public const String FLD_FORM_ID = "form_id";
		/// <value>The constant used for the order "Form Name" task in the Order table. </value>
		public const String FLD_TASK_ID = "task_id";
		/// <value>The constant used for the order "Form Name" task in the Order table. </value>
		public const String FLD_TASK_NAME = "task_name";
		/// <value>The constant used for the order "Form Name" task in the Order table. </value>
		public const String FLD_TASK_TYPE_ID = "task_type_id";
		/// <value>The constant used for the order "Form Name" task in the Order table. </value>
		public const String FLD_TASK_TYPE_NAME = "task_type_name";
		/// <value>The constant used for the order "Description" task in the Order table. </value>
		public const String FLD_NAME = "business_task_name";
		/// <value>The constant used for the order "Description" task in the Order table. </value>
		public const String FLD_MESSAGE = "message";
		/// <value>The constant used for the order "Description" task in the Order table. </value>
		public const String FLD_EXPRESSION = "task_expression";
		/// <value>The constant used for the order "Description" task in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_ASSIGNMENT_TYPE_ID = "assignment_type_id";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_ASSIGNED_USER_ID = "assigned_user_id";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_ASSIGNED_USER_FIRST_NAME = "first_name";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_ASSIGNED_USER_LAST_NAME = "last_name";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_ASSIGNED_USER_EMAIL = "email";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_ASSIGNED_ROLE_ID = "assigned_role_id";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_ASSIGNED_ROLE_NAME = "role_name";
		/// <value>The constant used for PKId task in the Order table. </value>
		public const String FLD_PARENT_ID = "parent_business_task_id";		
		/// <value>The constant used for the order "IsValid" task in the Order table. </value>
		public const String FLD_IS_VALID = "IsValid";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" task in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" task in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" task in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" task in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public BusinessTaskTable() : 
                base(TBL_BUSINESS_TASK) {
            this.InitClass();
        }
		    
        public BusinessTaskTable(DataTable table) : 
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
        
        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            BusinessTaskTable cln = ((BusinessTaskTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new BusinessTaskTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_BUSINESS_TASK;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_TASK_ID, typeof(System.Int32));
			columns.Add(FLD_TASK_NAME, typeof(System.String));
			columns.Add(FLD_TASK_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_TASK_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String)).DefaultValue = "New Business Tasks";;			
			columns.Add(FLD_MESSAGE, typeof(System.String));
			columns.Add(FLD_EXPRESSION, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_ASSIGNMENT_TYPE_ID, typeof(System.Int32)).DefaultValue = 1; //Specified user
			columns.Add(FLD_ASSIGNED_USER_ID, typeof(System.Int32));
			columns.Add(FLD_ASSIGNED_USER_FIRST_NAME, typeof(System.String));
			columns.Add(FLD_ASSIGNED_USER_LAST_NAME, typeof(System.String));
			columns.Add(FLD_ASSIGNED_USER_EMAIL, typeof(System.String));
			columns.Add(FLD_ASSIGNED_ROLE_ID, typeof(System.Int32));
			columns.Add(FLD_ASSIGNED_ROLE_NAME, typeof(System.String));
			columns.Add(FLD_PARENT_ID, typeof(System.Int32));
			columns.Add(FLD_IS_VALID, typeof(System.Boolean)).DefaultValue = 0;
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
