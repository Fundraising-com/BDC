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
	///         The serializale constructor allows objects of type CampaignData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CreditApplicationTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Campaign constants
		// 
		/// <value>The constant used for Campaigns table. </value>
		public const String TBL_CREDIT_APPLICATIONS = "credit_application";
		/// <value>The constant used for PKId field in the Campaigns table. </value>
		public const String FLD_PKID = "credit_application_id";
		/// <value>The constant used for "Campaign Type ID" field in the Campaigns table. </value>
		public const String FLD_TYPE_ID = "credit_application_type_id";
		/// <value>The constant used for "Campaign Type ID" field in the Campaigns table. </value>
		public const String FLD_TYPE_NAME = "credit_application_type_name";
		/// <value>The constant used for "Account ID" field in the Campaigns table. </value>
		public const String FLD_ACCOUNT_ID = "account_id";
		/// <value>The constant used for "Warehouse ID" field in the Campaigns table. </value>
		public const String FLD_CUSTOMER_ID = "customer_id";
		/// <value>The constant used for FM ID field in the AccountOwnership table. </value>
		public const String FLD_CREDIT_CARD_ID = "credit_card_id";		
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_FORM_ID = "form_id";
		/// <value>The constant used for "Campaign Type ID" field in the Campaigns table. </value>
		public const String FLD_OFFICER_NAME = "officer_name";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SOCIAL_SECURITY_NUMBER = "social_security_number";
		/// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
		public const String FLD_CREDIT_LIMIT = "credit_limit";
		/// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
		public const String FLD_POSTAL_ADDRESS_ID = "postal_address_id";
		/// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
		public const String FLD_PHONE_NUMBER_ID = "phone_number_id";
		/// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
		public const String FLD_HOME_PHONE_NUMBER_ID = "home_phone_number_id";
		/// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
		public const String FLD_APPROVED = "approved";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_APPROVE_USER_ID = "approve_user_id";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_APPROVE_USER_NAME = "approve_user_name";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_APPROVE_DATE = "approve_date";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_APPROVE_CODE = "approve_code";
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_IS_VALIDATION_PERFORMED = "is_validation_performed";
				
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
		

		public CreditApplicationTable() : 
                base(TBL_CREDIT_APPLICATIONS) {
            this.InitClass();
        }
		    
        public CreditApplicationTable(DataTable table) : 
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
            CreditApplicationTable cln = ((CreditApplicationTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new CreditApplicationTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Campaign table
			//
			this.TableName = TBL_CREDIT_APPLICATIONS;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_TYPE_ID, typeof(System.Int32));	
			columns.Add(FLD_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_ACCOUNT_ID, typeof(System.Int32));
			columns.Add(FLD_CUSTOMER_ID, typeof(System.Int32));
			columns.Add(FLD_CREDIT_CARD_ID, typeof(System.Int32));
			columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_OFFICER_NAME, typeof(System.String));
			columns.Add(FLD_SOCIAL_SECURITY_NUMBER, typeof(System.String));
			columns.Add(FLD_CREDIT_LIMIT, typeof(System.Decimal)).DefaultValue = 0;				
			columns.Add(FLD_POSTAL_ADDRESS_ID, typeof(System.Int32));
			columns.Add(FLD_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_HOME_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_APPROVED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_APPROVE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_APPROVE_USER_NAME, typeof(System.String));
			columns.Add(FLD_APPROVE_DATE, typeof(System.DateTime));
			columns.Add(FLD_APPROVE_CODE, typeof(System.String));
			columns.Add(FLD_IS_VALIDATION_PERFORMED, typeof(System.Boolean)).DefaultValue = 0;
						
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
