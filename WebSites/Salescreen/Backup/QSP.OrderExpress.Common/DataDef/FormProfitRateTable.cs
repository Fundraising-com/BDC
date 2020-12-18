using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
    ///         This class is used to define the shape of FormProfitRateTable.
	///     </remarks>
	///     <remarks>
    ///         The serializale constructor allows objects of type FormProfitRateTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class FormProfitRateTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Form Header constants
		// 
		/// <value>The constant used for Form table. </value>
		public const String TBL_FORM_PROFIT_RATE = "form_profit_rate";
		/// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_PKID = "form_profit_rate_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_FORM_ID = "form_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_PROFIT_RATE_ID = "profit_rate_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_PROFIT_RATE = "profit_rate";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_COMMISSION_RATE = "commission_rate";		
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
		
		
		public FormProfitRateTable() : 
                base(TBL_FORM_PROFIT_RATE) {
            this.InitClass();
        }

        public FormProfitRateTable(DataTable table)
            : 
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
            FormProfitRateTable cln = ((FormProfitRateTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new FormProfitRateTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {	
			//
			// Create the Campaign table
			//
			this.TableName = TBL_FORM_PROFIT_RATE;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID <= 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
            Column.AutoIncrementSeed = 0;
            Column.AutoIncrementStep = -1;

			columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_PROFIT_RATE_ID, typeof(System.Int32));
			columns.Add(FLD_PROFIT_RATE, typeof(System.Decimal));
            columns.Add(FLD_COMMISSION_RATE, typeof(System.Decimal));
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
		}

	}
}
