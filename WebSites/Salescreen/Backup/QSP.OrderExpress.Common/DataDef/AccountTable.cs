using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of AccountTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type AccountTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class AccountTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_ACCOUNT = "account";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_PKID = "account_id";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_ORG_ID = "organization_id";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_IS_RENEW = "is_renew";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_CUSTOMER_ID = "customer_id";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ACCOUNT_TYPE_ID = "account_type_id";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ACCOUNT_TYPE_NAME = "account_type_name";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ACCOUNT_STATUS_ID = "account_status_id";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ACCOUNT_STATUS_NAME = "account_status_name";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION = "account_status_short_description";
        /// <value>The constant used for Account Type ID field in the Account table. </value>
        public const String FLD_ACCOUNT_STATUS_DESCRIPTION = "account_status_description";
        /// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ACCOUNT_STATUS_COLOR_CODE = "color_code";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_NAME = "account_name";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_FULF_ACCOUNT_ID = "fulf_account_id";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FM_ID = "fm_id";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FM_NAME = "fm_name";
		/// <value>The constant used for Tax Exemption Number field in the Account table. </value>
		public const String FLD_TAX_EXEMPTION_NO = "tax_exemption_number";
		/// <value>The constant used for Tax Exemption Expiration Date field in the Account table. </value>
		public const String FLD_TAX_EXEMPTION_EXP_DATE = "tax_exemption_expiration_date";
		/// <value>The constant used for credit limit field in the Account table. </value>
		public const String FLD_CREDIT_LIMIT = "credit_limit";
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_COMMENTS = "comments";	
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_LAST_ORDER_DATE = "last_order_date";	
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_LAST_CAMPAIGN_ID = "last_campaign_id";
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_IS_VALIDATION_PERFORMED = "is_validation_performed";
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_INFO_STATUS = "info_status";
        /// <value>The constant used for comments field in the Order table. </value>
        public const String FLD_ACCOUNT_COLLECTION_DATE = "account_collection_date";
        /// <value>The constant used for comments field in the Order table. </value>
        public const String FLD_ACCOUNT_COLLECTION_AMOUNT = "account_collection_amount";
         
         

		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
        public const String FLD_CREATE_DATE = "create_date";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_LAST_NAME = "create_last_name";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_FIRST_NAME = "create_first_name";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public AccountTable() : 
                base(TBL_ACCOUNT) {
            this.InitClass();
        }
		    
        public AccountTable(DataTable table) : 
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
            AccountTable cln = ((AccountTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new AccountTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_ACCOUNT;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_IS_RENEW, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_ORG_ID, typeof(System.Int32));
			columns.Add(FLD_CUSTOMER_ID, typeof(System.Int32));
			columns.Add(FLD_ACCOUNT_STATUS_ID, typeof(System.Int32));	
			columns.Add(FLD_ACCOUNT_STATUS_NAME, typeof(System.String));
			columns.Add(FLD_ACCOUNT_STATUS_SHORT_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_ACCOUNT_STATUS_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_ACCOUNT_STATUS_COLOR_CODE, typeof(System.String));
            columns.Add(FLD_ACCOUNT_TYPE_ID, typeof(System.Int32));	
			columns.Add(FLD_ACCOUNT_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));	
			columns.Add(FLD_FULF_ACCOUNT_ID, typeof(System.Int32));
			columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_FM_NAME, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_EXP_DATE, typeof(System.DateTime));
			columns.Add(FLD_CREDIT_LIMIT, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_COMMENTS, typeof(System.String));			
			columns.Add(FLD_LAST_ORDER_DATE, typeof(System.DateTime));
			columns.Add(FLD_LAST_CAMPAIGN_ID, typeof(System.Int32));
			columns.Add(FLD_IS_VALIDATION_PERFORMED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_INFO_STATUS, typeof(System.Int32)).DefaultValue = InfoStatus.NONE;
            columns.Add(FLD_ACCOUNT_COLLECTION_DATE, typeof(System.DateTime));
            columns.Add(FLD_ACCOUNT_COLLECTION_AMOUNT, typeof(System.Decimal)).DefaultValue = 0;

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
        			
		}
	}
}
