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
	///         The serializale constructor allows objects of type BusinessCalendarTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class WarehouseBusinessCalendarTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_WAREHOUSE_BUSINESS_CALENDAR = "warehouse_business_calendar";
		/// <value>The constant used for PKId field in the Warehouses table. </value>
		public const String FLD_WAREHOUSE_ID = "warehouse_id";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_BUSINESS_DATE = "business_date";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_IS_CLOSED = "closed";		
        
		public WarehouseBusinessCalendarTable() : 
                base(TBL_WAREHOUSE_BUSINESS_CALENDAR) {
            this.InitClass();
        }
		    
        public WarehouseBusinessCalendarTable(DataTable table) : 
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
        
		protected WarehouseBusinessCalendarTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            WarehouseBusinessCalendarTable cln = ((WarehouseBusinessCalendarTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new WarehouseBusinessCalendarTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_WAREHOUSE_BUSINESS_CALENDAR;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_WAREHOUSE_ID, typeof(System.Int32));            
			Column.AllowDBNull = false;
			columns.Add(FLD_BUSINESS_DATE, typeof(System.DateTime));
			Column.AllowDBNull = false;				
            
			columns.Add(FLD_IS_CLOSED, typeof(System.Boolean)).DefaultValue = 0;			
			
		}

	}
}
