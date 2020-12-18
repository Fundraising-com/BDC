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
	///         The serializale constructor allows objects of type OrderHeaderTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class OrderHeaderTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_ORDERS = "order";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "order_id";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_FULF_ORDER_ID = "fulf_order_id";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_SUPPLY_ORDER_ID = "supply_order_id";
		/// <value>The constant used for "Campaign ID" field in the Order table. </value>
		public const String FLD_CAMPAIGN_ID = "campaign_id";
		/// <value>The constant used for the order "Order Group ID" field in the Order table. </value>
		public const String FLD_ORDER_GROUP_ID = "order_group_id";
		/// <value>The constant used for the order "Order Type ID" field in the Order table. </value>
		public const String FLD_ORDER_TYPE_ID = "order_type_id";
		/// <value>The constant used for the order "Order Type ID" field in the Order table. </value>
		public const String FLD_ORDER_TYPE_NAME = "order_type_name";
		/// <value>The constant used for the order "Status ID" field in the Order table. </value>
		public const String FLD_ORDER_STATUS_ID = "order_status_id";
		/// <value>The constant used for the order "Status ID" field in the Order table. </value>
		public const String FLD_ORDER_STATUS_NAME = "order_status_name";
		/// <value>The constant used for the order "Status ID" field in the Order table. </value>
		public const String FLD_ORDER_STATUS_SHORT_DESCRIPTION = "order_status_short_description";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
        public const String FLD_ORDER_STATUS_DESCRIPTION = "order_status_description";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
		public const String FLD_ORDER_STATUS_COLOR_CODE = "color_code";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
        public const String FLD_ORDER_STATUS_CATEGORY_ID = "status_category_id";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
        public const String FLD_ORDER_STATUS_CATEGORY_NAME = "status_category_name";
        /// <value>The constant used for the order "Source ID" field in the Order table. </value>
		public const String FLD_SOURCE_ID = "source_id";
		/// <value>The constant used for the order "Status Reason ID" field in the Order table. </value>
		public const String FLD_STATUS_REASON_ID = "status_reason_id";
		/// <value>The constant used for the order "Status Reason ID" field in the Order table. </value>
		public const String FLD_STATUS_REASON_NAME = "status_reason_name";
		/// <value>The constant used for the order "Billing Postal Address ID" field in the Order table. </value>
		public const String FLD_BILLING_POSTAL_ADDRESS_ID = "billing_postal_address_id";
		/// <value>The constant used for the order "Billing Phone Number ID" field in the Order table. </value>
		public const String FLD_BILLING_PHONE_NUMBER_ID = "billing_phone_number_id";
		/// <value>The constant used for the order "Billing Fax Number ID" field in the Order table. </value>
		public const String FLD_BILLING_FAX_NUMBER_ID = "billing_fax_number_id";
		/// <value>The constant used for the order "Billing Phone Number ID" field in the Order table. </value>
		public const String FLD_BILLING_EMAIL_ADDRESS_ID = "billing_email_id";
		/// <value>The constant used for the order "Customer ID" field in the Order table. </value>
		public const String FLD_CUSTOMER_ID = "customer_id";
        /// <value>The constant used for the order "Customer ID" field in the Order table. </value>
        public const String FLD_CUSTOMER_PO_NUMBER = "customer_po_number";
        /// <value>The constant used for the order "Installment plan ID" field in the Order table. </value>
		public const String FLD_INSTALLMENT_PLAN_ID = "installment_plan_id";
		/// <value>The constant used for the order "Form ID" field in the Order table. </value>
		public const String FLD_FORM_ID = "form_id";
		/// <value>The constant used for the order "FM ID" field in the Order table. </value>
		public const String FLD_FM_ID = "fm_id";
		/// <value>The constant used for the order "FM ID" field in the Order table. </value>
		public const String FLD_FM_NAME = "fm_name";
		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_ORDER_DATE  = "order_date";
		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_ADJ_AMOUNT  = "adjustment_amount";
        /// <value>The constant used for Profit Rate field in the Order table. </value>
        public const String FLD_PROFIT_RATE = "profit_rate";
		/// <value>The constant used for comments field in the Order table. </value>
		public const String FLD_COMMENTS = "comments";
        /// <value></value>
        public const String FLD_IS_DSD = "is_dsd";
        /// <value></value>
        public const String FLD_IS_CONTINUATION = "is_continuation";


		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_TOTAL_QTY  = "total_quantity";
		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_TOTAL_AMOUNT  = "total_amount";
		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_TOTAL_ADJ_AMOUNT  = "total_adj_amount";
		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_TAX_RATE  = "tax_rate";
		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_TOTAL_TAX_AMOUNT  = "total_tax_amount";
		/// <value>The constant used for Delivery Date field in the Order table. </value>
		public const String FLD_TOTAL_SHIP_FEES  = "total_shipping_fees";
		/// <value>The constant used for total charges field in the Order table. </value>
        public const String FLD_TOTAL_CHARGES = "total_charges_fees";
        /// <value>The constant used for Delivery Date field in the Order table. </value>
        public const String FLD_GRAND_TOTAL = "grand_total_amount";
		/// <value>The constant used for comments field in the Account table. </value>
		public const String FLD_IS_VALIDATION_PERFORMED = "is_validation_performed";

        public const String FLD_DELETED = "deleted";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_USER_ID = "create_user_id";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_LAST_NAME = "create_last_name";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_FIRST_NAME = "create_first_name";
        /// <value>The constant used for "create date" field in the Collection Days table. </value>
        public const String FLD_CREATE_DATE = "create_date";
        /// <value>The constant used for "update user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_USER_ID = "update_user_id";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_LAST_NAME = "update_last_name";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_FIRST_NAME = "update_first_name";
        /// <value>The constant used for "update date" field in the Collection Days table. </value>
        public const String FLD_UPDATE_DATE = "update_date";
    
        
		public OrderHeaderTable() : 
                base(TBL_ORDERS) {
            this.InitClass();
        }
		    
        public OrderHeaderTable(DataTable table) : 
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
            OrderHeaderTable cln = ((OrderHeaderTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OrderHeaderTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_ORDERS;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;
            
			columns.Add(FLD_FULF_ORDER_ID, typeof(System.String));
			columns.Add(FLD_SUPPLY_ORDER_ID, typeof(System.Int32));
			columns.Add(FLD_CAMPAIGN_ID, typeof(System.Int32));			
			columns.Add(FLD_ORDER_GROUP_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_SOURCE_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_STATUS_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_STATUS_NAME, typeof(System.String));
			columns.Add(FLD_ORDER_STATUS_SHORT_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_ORDER_STATUS_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_ORDER_STATUS_COLOR_CODE, typeof(System.String));
            columns.Add(FLD_ORDER_STATUS_CATEGORY_ID, typeof(System.Int32));
            columns.Add(FLD_ORDER_STATUS_CATEGORY_NAME, typeof(System.String));
			columns.Add(FLD_STATUS_REASON_ID, typeof(System.Int32));
			columns.Add(FLD_STATUS_REASON_NAME, typeof(System.String));
			columns.Add(FLD_BILLING_POSTAL_ADDRESS_ID, typeof(System.Int32));
			columns.Add(FLD_BILLING_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_BILLING_FAX_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_BILLING_EMAIL_ADDRESS_ID, typeof(System.Int32));
            columns.Add(FLD_CUSTOMER_ID, typeof(System.Int32));
            columns.Add(FLD_CUSTOMER_PO_NUMBER, typeof(System.String));
			columns.Add(FLD_INSTALLMENT_PLAN_ID, typeof(System.Int32));
			columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_FM_NAME, typeof(System.String));
			columns.Add(FLD_ORDER_DATE, typeof(System.DateTime));			
			columns.Add(FLD_COMMENTS, typeof(System.String));
           

			columns.Add(FLD_TOTAL_QTY, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_TOTAL_AMOUNT, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_TOTAL_ADJ_AMOUNT, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_TOTAL_SHIP_FEES, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_TAX_RATE, typeof(System.Decimal)).DefaultValue = 0;
            columns.Add(FLD_PROFIT_RATE, typeof(System.Decimal)).DefaultValue = 0;
            columns.Add(FLD_TOTAL_CHARGES, typeof(System.Decimal)).DefaultValue = 0;
			
			DataColumn colTotaTaxAmount = columns.Add(FLD_TOTAL_TAX_AMOUNT, typeof(System.Decimal));
			colTotaTaxAmount.Expression = "ISNULL (" + FLD_TOTAL_AMOUNT + ",0) * ISNULL (" + FLD_TAX_RATE + ",0)";
			
			DataColumn colGTotal = columns.Add(FLD_GRAND_TOTAL, typeof(System.Decimal));
            colGTotal.Expression = "ISNULL (" + FLD_TOTAL_AMOUNT + ",0) + ISNULL (" + FLD_TOTAL_SHIP_FEES + ",0) + ISNULL (" + FLD_TOTAL_TAX_AMOUNT + ",0)" + " + ISNULL (" + FLD_TOTAL_CHARGES + ",0)";
			
			columns.Add(FLD_IS_VALIDATION_PERFORMED, typeof(System.Boolean)).DefaultValue = 0;

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_CREATE_LAST_NAME, typeof(System.String));
            columns.Add(FLD_CREATE_FIRST_NAME, typeof(System.String));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
            columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_UPDATE_LAST_NAME, typeof(System.String));
            columns.Add(FLD_UPDATE_FIRST_NAME, typeof(System.String));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));

            columns.Add(FLD_IS_DSD, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_IS_CONTINUATION, typeof(System.Boolean)).DefaultValue = 0;

		}

	}
}
