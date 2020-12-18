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
	public class CampaignTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Campaign constants
		// 
		/// <value>The constant used for Campaigns table. </value>
		public const String TBL_CAMPAIGNS = "campaign";
		/// <value>The constant used for PKId field in the Campaigns table. </value>
		public const String FLD_PKID = "campaign_id";
		/// <value>The constant used for "Campaign Type ID" field in the Campaigns table. </value>
		public const String FLD_PROG_TYPE_ID = "program_type_id";
		/// <value>The constant used for "Campaign Type ID" field in the Campaigns table. </value>
		public const String FLD_PROG_TYPE_NAME = "program_type_name";
		/// <value>The constant used for "Account ID" field in the Campaigns table. </value>
		public const String FLD_ACCOUNT_ID = "account_id";
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_TRADE_CLASS_ID = "trade_class_id";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_TRADE_CLASS_NAME = "trade_class_name";		
		/// <value>The constant used for "Warehouse ID" field in the Campaigns table. </value>
		public const String FLD_WAREHOUSE_ID = "warehouse_id";
		/// <value>The constant used for "Warehouse ID" field in the Campaigns table. </value>
		public const String FLD_FULF_WAREHOUSE_ID = "fulf_warehouse_id";
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_WAREHOUSE_NAME = "warehouse_name";		
		/// <value>The constant used for FM ID field in the AccountOwnership table. </value>
		public const String FLD_FM_ID = "fm_id";		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FM_NAME = "fm_name";
		/// <value>The constant used for Start Date field in the Campaigns table. </value>
		public const String FLD_NAME  = "campaign_name";
		/// <value>The constant used for Campaign Name field in the Campaigns table. </value>
		public const String FLD_START_DATE  = "start_date";
		/// <value>The constant used for End Date field in the Campaigns table. </value>
		public const String FLD_END_DATE = "end_date";
		/// <value>The constant used for End Date field in the Campaigns table. </value>
		public const String FLD_FISCAL_YEAR = "fiscal_year";
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_FORM_ID = "form_id";		
		/// <value>The constant used for Tax Exemption Number field in the Campaigns table. </value>
		public const String FLD_TAX_EXEMPTION_NO = "tax_exemption_number";
		/// <value>The constant used for Tax Exemption Expiration Date field in the Account table. </value>
		public const String FLD_TAX_EXEMPTION_EXP_DATE = "tax_exemption_expiration_date";
		/// <value>The constant used for Enrollment field in the Campaigns table. </value>
		public const String FLD_ENROLLMENT = "enrollment";
		/// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
		public const String FLD_GOAL_ESTIMATED_GROSS = "goal_estimated_gross";
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_COMMENTS = "comments";

        //public const String FLD_PROFIT_RATE = "profit_rate";
        //move only at order level
	
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
		

		public CampaignTable() : 
                base(TBL_CAMPAIGNS) {
            this.InitClass();
        }
		    
        public CampaignTable(DataTable table) : 
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
            CampaignTable cln = ((CampaignTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new CampaignTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Campaign table
			//
			this.TableName = TBL_CAMPAIGNS;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_PROG_TYPE_ID, typeof(System.Int32));	
			columns.Add(FLD_PROG_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_ACCOUNT_ID, typeof(System.Int32));
			columns.Add(FLD_TRADE_CLASS_ID, typeof(System.Int32));
			columns.Add(FLD_TRADE_CLASS_NAME, typeof(System.String));
			columns.Add(FLD_WAREHOUSE_ID, typeof(System.Int32));
			columns.Add(FLD_FULF_WAREHOUSE_ID, typeof(System.Int32));
			columns.Add(FLD_WAREHOUSE_NAME, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_FM_NAME, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_EXP_DATE, typeof(System.DateTime));
			columns.Add(FLD_START_DATE, typeof(System.DateTime));
			columns.Add(FLD_END_DATE, typeof(System.DateTime));			
			columns.Add(FLD_FISCAL_YEAR, typeof(System.Int32));
			columns.Add(FLD_FORM_ID, typeof(System.Int32));			
			columns.Add(FLD_ENROLLMENT, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_GOAL_ESTIMATED_GROSS, typeof(System.Decimal)).DefaultValue = 0;			
			columns.Add(FLD_COMMENTS, typeof(System.String));

			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));

            //columns.Add(FLD_PROFIT_RATE, typeof(System.Decimal)).DefaultValue = 0;
            //move only at order level
			
		}

	}
}
