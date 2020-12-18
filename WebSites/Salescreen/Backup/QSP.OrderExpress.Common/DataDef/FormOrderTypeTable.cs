using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
    ///         This class is used to define the shape of FormOrderTypeTable.
	///     </remarks>
	///     <remarks>
    ///         The serializale constructor allows objects of type FormOrderTypeTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class FormOrderTypeTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Form Header constants
		// 
		/// <value>The constant used for Form table. </value>
		public const String TBL_FORM_ORDER_TYPE = "form_order_type";
		/// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_PKID = "form_order_type_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_FORM_ID = "form_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_ORDER_TYPE_ID = "order_type_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_ORDER_TYPE_NAME = "order_type_name";		
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
		
		
		public FormOrderTypeTable() : 
                base(TBL_FORM_ORDER_TYPE) {
            this.InitClass();
        }

        public FormOrderTypeTable(DataTable table)
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
            FormOrderTypeTable cln = ((FormOrderTypeTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new FormOrderTypeTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {	
			//
			// Create the Campaign table
			//
			this.TableName = TBL_FORM_ORDER_TYPE;
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
			columns.Add(FLD_ORDER_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORDER_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
		}

	}
}
