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
	public class ValidationTable : DataTable, System.Collections.IEnumerable
	{
		//
		// Header constants
		// 
		/// <value>The constant used for  table. </value>
		public const String TBL_VALIDATION = "validation";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_PKID = "validation_id";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_FORM_SECTION_TYPE_ID = "form_section_type_id";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_FORM_SECTION_NUMBER = "form_section_number";				
		
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_CATALOG_ITEM_CODE = "catalog_item_code";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_DELIVERY_DATE = "delivery_date";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_DELIVERY_METHOD_ID = "delivery_method_id";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_NB_DAY_LEAD_TIME = "nb_day_lead_time";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_CATALOG_ITEM_NB_DAY_LEAD_TIME = "catalog_item_nb_day_lead_time";				
		
        //-------Product All Section Fields------------
        /// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_TOTAL_QUANTITY = "total_quantity";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_TOTAL_CD_QUANTITY = "total_cd_quantity"; //cookie dough quantity
        /// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_TOTAL_AMOUNT = "total_amount";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_MIN_LINE_ITEM_QUANTITY = FormPropertyName.MIN_LINE_ITEM_QUANTITY;
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_MIN_TOTAL_QUANTITY = FormPropertyName.MIN_TOTAL_QUANTITY;
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_MAX_TOTAL_QUANTITY = FormPropertyName.MIN_TOTAL_QUANTITY;
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_MIN_TOTAL_AMOUNT = FormPropertyName.MIN_TOTAL_AMOUNT;
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_MAX_TOTAL_AMOUNT = FormPropertyName.MAX_TOTAL_AMOUNT;
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_MIN_NB_DAY_LEAD_TIME = FormPropertyName.MIN_NB_DAY_LEAD_TIME;
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_ORDER_MAX_MIN_NB_DAY_LEAD_TIME = "order_max_min_nb_day_lead_time";
        
        /// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_IS_NEW_ACCOUNT = "is_new_account";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_IS_NEW_ORDER = "is_new_order";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_IS_FIRST_TIME_PROCESS = "is_first_time_process";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_IS_NEW_CREDIT_APP = "is_new_credit_application";		
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_IS_CREDIT_APP_APPROVED = "is_credit_approved";		
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_CREDIT_APPLICATION_TYPE_ID = "credit_application_type_id";		
		
   
        
        public const String FLD_ORDER_TYPE_ID = "order_type_id";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_ORDER_STATUS_ID = "order_status_id";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_ORDER_STATUS_CATEGORY_ID = "order_status_category_id";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_ORG_TYPE_ID = "organization_type_id";		
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_NB_INACTIVE_MONTH = "nb_inactive_month";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_PROGRAM_TYPE_ID = "program_type_id";	
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_ACCOUNT_STATUS_ID = "account_status_id";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_ACCOUNT_FM_ID = "account_fm_id";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_ORDER_FM_ID = "order_fm_id";

		/// <value>The constant used for PKId field in the  table. </value>		
		public const String FLD_IS_TAX_EXEMPTED = "is_tax_exempted";
		/// <value>The constant used for PKId field in the  table. </value>		
		public const String FLD_IS_TAX_EXEMPTION_NO_ENTERED = "is_tax_exemption_no_entered";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_IS_TAX_EXEMPTION_FORM_RECEIVED = "is_tax_exemption_form_received";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_IS_CREDIT_APPLICATION_FORM_RECEIVED = "is_credit_application_form_received";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_ACCOUNT_HISTORY_INTERVAL_NB_DAY = FormPropertyName.ACCOUNT_HISTORY_INTERVAL_NB_DAY;
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_ACCOUNT_HISTORY_INTERVAL_TOTAL_AMOUNT = "accout_history_interval_total_amount";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_ACCOUNT_HISTORY_INTERVAL_MAX_TOTAL_AMOUNT = "account_history_interval_max_total_amount";	
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_ACCOUNT_HISTORY_TOTAL_AMOUNT = "account_history_total_amount";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_ACCOUNT_HISTORY_MIN_TOTAL_AMOUNT = FormPropertyName.ACCOUNT_HISTORY_MIN_TOTAL_AMOUNT;
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_ACCOUNT_HAS_PRE_SALES_ESTIMATE = "account_has_pre_sales_estimate";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_ACCOUNT_HAS_PROGRAM_AGREEMENT = "account_has_program_agreement";


        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_IS_NEW_PROGRAM_AGREEMENT = "is_new_program_agreement";
        /// <value>The constant used for PKId field in the  table. </value>
        public const String FLD_PROGRAM_AGREEMENT_STATUS_ID = "program_agreement_status_id";
        
		

		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_EXPRESSION = "biz_rule_expression";
		/// <value>The constant used for the order "Description" field in the  table. </value>
		public const String FLD_FEES_VALUE_AMOUNT = "fees_value_amount";

		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_IS_VALID = "IsValid";
		/// <value>The constant used for PKId field in the  table. </value>
		public const String FLD_MESSAGE = "Message";
		
		
		/// <value>The constant used for IsSystem Extended properties in the  table. </value>
		private const String IS_SYSTEM = "IsSystem";

	
		public ValidationTable() : 
                base(TBL_VALIDATION) {
            this.InitClass();
        }
		    
        public ValidationTable(DataTable table) : 
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
            ValidationTable cln = ((ValidationTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new ValidationTable();
        }
        
        internal void InitVars() {
            
        }

        public DataRow GetValidationRow(int FormSectionTypeID, int FormSectionNumber)
        {

            DataRow row;
            DataView dv = new DataView(this);
            string sFilter = "";
            sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",0) = " + FormSectionTypeID.ToString();
            sFilter = sFilter + "AND ISNULL(" + FLD_FORM_SECTION_NUMBER + ",0) = " + FormSectionNumber.ToString();
            dv.RowFilter = sFilter;
            if (dv.Count > 0)
            {
                row = dv[0].Row;
                return row;
            }
            else
            {
                return null;
            }


        }

		private void SetColumnSystem(DataColumn col, bool IsSystem)
		{
			col.ExtendedProperties.Add(IS_SYSTEM, IsSystem);
		}

		private void SetColumnSystem(DataColumn col)
		{
			SetColumnSystem(col, true);
		}

		public bool IsColumnSystem(DataColumn col)
		{
			bool IsSystem = false;
			if (col.ExtendedProperties.Contains(IS_SYSTEM))
				IsSystem = Convert.ToBoolean(col.ExtendedProperties[IS_SYSTEM]);
			return IsSystem;
		}
        
        private void InitClass() {	
			//
			// Create the table
			//
			this.TableName = TBL_VALIDATION;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;

            columns.Add(FLD_FORM_SECTION_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_FORM_SECTION_NUMBER, typeof(System.Int32));
            
			columns.Add(FLD_CATALOG_ITEM_CODE, typeof(System.String));			
			columns.Add(FLD_NB_DAY_LEAD_TIME, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_CATALOG_ITEM_NB_DAY_LEAD_TIME, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_ORDER_MAX_MIN_NB_DAY_LEAD_TIME, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_DELIVERY_DATE, typeof(System.DateTime));
			columns.Add(FLD_DELIVERY_METHOD_ID, typeof(System.Int32));
			
            //---Product All Section
            columns.Add(FLD_MIN_NB_DAY_LEAD_TIME, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_TOTAL_QUANTITY, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_TOTAL_CD_QUANTITY, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_MIN_TOTAL_QUANTITY, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_TOTAL_AMOUNT, typeof(System.Decimal)).DefaultValue = 0;
            columns.Add(FLD_MIN_TOTAL_AMOUNT, typeof(System.Decimal)).DefaultValue = 0;
            columns.Add(FLD_MIN_LINE_ITEM_QUANTITY, typeof(System.Int32)).DefaultValue = 0;
            
            columns.Add(FLD_CREDIT_APPLICATION_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_STATUS_ID, typeof(System.Int32));
            columns.Add(FLD_ORDER_STATUS_CATEGORY_ID, typeof(System.Int32));
			columns.Add(FLD_PROGRAM_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORG_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ACCOUNT_STATUS_ID, typeof(System.Int32));
			columns.Add(FLD_NB_INACTIVE_MONTH, typeof(System.Int32));			
			columns.Add(FLD_ACCOUNT_HISTORY_INTERVAL_NB_DAY, typeof(System.Int32));
			columns.Add(FLD_ACCOUNT_HISTORY_INTERVAL_TOTAL_AMOUNT, typeof(System.Decimal));
			columns.Add(FLD_ACCOUNT_HISTORY_TOTAL_AMOUNT, typeof(System.Decimal));
			columns.Add(FLD_ACCOUNT_HISTORY_INTERVAL_MAX_TOTAL_AMOUNT, typeof(System.Decimal));
            columns.Add(FLD_ACCOUNT_HISTORY_MIN_TOTAL_AMOUNT, typeof(System.Decimal));
            columns.Add(FLD_ACCOUNT_HAS_PRE_SALES_ESTIMATE, typeof(System.Decimal));
            columns.Add(FLD_ACCOUNT_FM_ID, typeof(System.String));
            columns.Add(FLD_ORDER_FM_ID, typeof(System.String));
            columns.Add(FLD_PROGRAM_AGREEMENT_STATUS_ID, typeof(System.Int32));
            

            //System Column
			Column = columns.Add(FLD_IS_NEW_ACCOUNT, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);

			Column = columns.Add(FLD_IS_NEW_ORDER, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);

            Column = columns.Add(FLD_IS_FIRST_TIME_PROCESS, typeof(System.Boolean));
            Column.DefaultValue = 0;
            SetColumnSystem(Column);
			
			Column = columns.Add(FLD_IS_NEW_CREDIT_APP, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);

            Column = columns.Add(FLD_IS_NEW_PROGRAM_AGREEMENT, typeof(System.Boolean));
            Column.DefaultValue = 0;
            SetColumnSystem(Column);
			
			Column = columns.Add(FLD_IS_CREDIT_APP_APPROVED, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);
			
			Column = columns.Add(FLD_IS_TAX_EXEMPTED, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);

			Column = columns.Add(FLD_IS_TAX_EXEMPTION_NO_ENTERED, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);

			Column = columns.Add(FLD_IS_TAX_EXEMPTION_FORM_RECEIVED, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);

			Column = columns.Add(FLD_IS_CREDIT_APPLICATION_FORM_RECEIVED, typeof(System.Boolean));
			Column.DefaultValue = 0;
			SetColumnSystem(Column);

			//Utility Helper Column
			columns.Add(FLD_EXPRESSION, typeof(System.String)).DefaultValue = "";
			columns.Add(FLD_FEES_VALUE_AMOUNT, typeof(System.Decimal));
			columns.Add(FLD_IS_VALID, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_MESSAGE, typeof(System.String)).DefaultValue = 0;
				
		}

	}
}
