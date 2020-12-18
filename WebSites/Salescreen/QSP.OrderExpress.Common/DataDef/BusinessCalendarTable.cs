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
	public class BusinessCalendarTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_BUSINESS_CALENDAR = "business_calendar";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "business_date";
		/// <value>The constant used for "Form Code" field in the Order table. </value>
		public const String FLD_IS_WEEKEND = "weekend";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_IS_HOLIDAY = "holiday";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_IS_CLOSED = "closed";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_NB_DAY_LEAD_TIME = "nb_day_lead_time";		
		
        
		public BusinessCalendarTable() : 
                base(TBL_BUSINESS_CALENDAR) {
            this.InitClass();
        }
		    
        public BusinessCalendarTable(DataTable table) : 
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
        
		protected BusinessCalendarTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            BusinessCalendarTable cln = ((BusinessCalendarTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new BusinessCalendarTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_BUSINESS_CALENDAR;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.DateTime));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....			
            
			columns.Add(FLD_IS_WEEKEND, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_IS_HOLIDAY, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_IS_CLOSED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_NB_DAY_LEAD_TIME, typeof(System.Int32)).DefaultValue = -1;

			
		}

	}
}
