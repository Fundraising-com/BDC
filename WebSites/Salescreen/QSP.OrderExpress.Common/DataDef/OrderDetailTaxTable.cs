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
	///         The serializale constructor allows objects of type OrderDetailTaxTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class OrderDetailTaxTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_ORDER_DETAILS_TAX = "order_detail_tax";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "order_detail_tax_id";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_ORDER_DETAIL_ID = "order_detail_id";
		/// <value>The constant used for the order "Product ID" field in the Order table. </value>
		public const String FLD_TAX_CALCULATION_METHOD_ID = "tax_calculation_method_id";
		/// <value>The constant used for the order "Product ID" field in the Order table. </value>
		public const String FLD_TAX_TYPE_ID = "tax_type_id";
		/// <value>The constant used for the order "Product ID" field in the Order table. </value>
		public const String FLD_TAX_LEVEL_ID = "tax_level_id";
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_TAX_RATE = "tax_rate";
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_AMOUNT = "tax_amount";
		/// <value>The constant used for Quantity field in the Order table. </value>
		public const String FLD_TAX_EXEMPTABLE = "tax_exemptable";
		
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
		
		
		public OrderDetailTaxTable() : 
                base(TBL_ORDER_DETAILS_TAX) {
            this.InitClass();
        }
		    
        public OrderDetailTaxTable(DataTable table) : 
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
            OrderDetailTaxTable cln = ((OrderDetailTaxTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OrderDetailTaxTable();
        }
        
        internal void InitVars() {
            
        }

        public void CleanData()
        {
            //Remove detail with quantity 0
            bool IsFound = true;
            int iIndexFound = -1;
            DataView dv = new DataView(this);
            dv.Sort = FLD_AMOUNT;
            while (IsFound)
            {
                iIndexFound = dv.Find(0);
                if (iIndexFound == -1)
                {
                    IsFound = false;
                    break;
                }
                else
                {
                    dv[iIndexFound].Row.Delete();
                }
            }
        }
        
        private void InitClass() {	
			//
			// Create the Order Detail Tax table
			//
			this.TableName = TBL_ORDER_DETAILS_TAX;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;

            columns.Add(FLD_ORDER_DETAIL_ID, typeof(System.Int32));
			columns.Add(FLD_TAX_CALCULATION_METHOD_ID, typeof(System.Int32));
			columns.Add(FLD_TAX_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_TAX_LEVEL_ID, typeof(System.Int32));
			columns.Add(FLD_TAX_RATE, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_AMOUNT, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_TAX_EXEMPTABLE, typeof(System.Boolean)).DefaultValue = 0;

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
		}

	}
}
