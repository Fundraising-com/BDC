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
	///         The serializale constructor allows objects of type BusinessExceptionTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class BusinessExceptionTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_BUSINESS_EXCEPTION = "business_exception";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "business_exception_id";
		/// <value>The constant used for "Name" field in the Order table. </value>
		public const String FLD_NAME = "business_exception_name";
		/// <value>The constant used for "exception type id" field in the Order table. </value>
		public const String FLD_EXCEPTION_TYPE_ID = "exception_type_id";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_EXCEPTION_TYPE_NAME = "exception_type_name";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_MESSAGE = "message";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_WARNING_MESSAGE = "warning_message";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_EXPRESSION = "exception_expression";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_FEES_VALUE_EXPRESSION = "fees_value_expression";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_FEES_VALUE_AMOUNT = "fees_value_amount";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_FORM_ID = "form_id";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_IS_VALID = "IsValid";
		/// <value>The constant used for "app item id" field in the Order table. </value>
		public const String FLD_APP_ITEM_ID = "app_item_id";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_ENTITY_TYPE_ID = "entity_type_id";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_ENTITY_TYPE_NAME = "entity_type_name";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_FORM_SECTION_NUMBER = "form_section_number";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_FORM_SECTION_TYPE_ID = "form_section_type_id";
        
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public BusinessExceptionTable() : 
                base(TBL_BUSINESS_EXCEPTION) {
            this.InitClass();
        }
		    
        public BusinessExceptionTable(DataTable table) : 
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
        
		protected BusinessExceptionTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            BusinessExceptionTable cln = ((BusinessExceptionTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new BusinessExceptionTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_BUSINESS_EXCEPTION;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_NAME, typeof(System.String)).DefaultValue = "New Business Exceptions";;
			columns.Add(FLD_EXCEPTION_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_EXCEPTION_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_WARNING_MESSAGE, typeof(System.String));
			columns.Add(FLD_MESSAGE, typeof(System.String));
			columns.Add(FLD_EXPRESSION, typeof(System.String));
			columns.Add(FLD_FEES_VALUE_EXPRESSION, typeof(System.String));
			columns.Add(FLD_FEES_VALUE_AMOUNT, typeof(System.Decimal));
			columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_APP_ITEM_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_NAME, typeof(System.String));
            columns.Add(FLD_FORM_SECTION_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_FORM_SECTION_NUMBER, typeof(System.Int32));
            
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
