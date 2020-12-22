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
	public class OrderStatusChangeTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_ORDER_STATUS_CHANGE = "order_status_change";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "order_status_change_id";
        public const String FLD_ORDER_ID = "order_id";
        public const String FLD_FULF_ORDER_ID = "fulf_order_id";

        public const String FLD_ORDER_STATUS_ID = "order_status_id";
        public const String FLD_STATUS_CHANGE_REASON = "status_change_reason";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
        public const String FLD_ORDER_STATUS_NAME = "order_status_name";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
        public const String FLD_ORDER_STATUS_SHORT_DESCRIPTION = "order_status_short_description";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
        public const String FLD_ORDER_STATUS_DESCRIPTION = "order_status_description";
        /// <value>The constant used for the order "Status ID" field in the Order table. </value>
        public const String FLD_ORDER_STATUS_COLOR_CODE = "color_code";

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
    
		
		
		public OrderStatusChangeTable() : 
                base(TBL_ORDER_STATUS_CHANGE) {
            this.InitClass();
        }
		    
        public OrderStatusChangeTable(DataTable table) : 
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
            OrderStatusChangeTable cln = ((OrderStatusChangeTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OrderStatusChangeTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {	
			//
			// Create the Campaign table
			//
			this.TableName = TBL_ORDER_STATUS_CHANGE;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;

            columns.Add(FLD_ORDER_ID, typeof(System.Int32));
            columns.Add(FLD_FULF_ORDER_ID, typeof(System.String));
            columns.Add(FLD_ORDER_STATUS_ID, typeof(System.Int32));
            columns.Add(FLD_ORDER_STATUS_NAME, typeof(System.String));
            columns.Add(FLD_ORDER_STATUS_SHORT_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_ORDER_STATUS_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_ORDER_STATUS_COLOR_CODE, typeof(System.String));
            columns.Add(FLD_STATUS_CHANGE_REASON, typeof(System.String));

            columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_CREATE_LAST_NAME, typeof(System.String));
            columns.Add(FLD_CREATE_FIRST_NAME, typeof(System.String));
            columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
            columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_UPDATE_LAST_NAME, typeof(System.String));
            columns.Add(FLD_UPDATE_FIRST_NAME, typeof(System.String));
            columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
		}

	}
}
