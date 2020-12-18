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
	///         The serializale constructor allows objects of type FormTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class FormTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_FORM = "form";
		public const String FLD_PKID = "form_id";
		public const String FLD_FORM_GROUP_ID = "form_group_id";
		public const String FLD_VERSION = "version";
		public const String FLD_FORM_CODE = "form_code";
		public const String FLD_FORM_NAME = "form_name";
		public const String FLD_DESCRIPTION = "description";
		public const String FLD_PROGRAM_BASICS_TEXT = "program_basics_text";
		public const String FLD_ORDER_TERMS_TEXT = "order_terms_text";
		public const String FLD_PROGRAM_TYPE_ID = "program_type_id";
		public const String FLD_PROGRAM_TYPE_NAME = "program_type_name";
        public const String FLD_PROGRAM_ID = "program_id";
        public const String FLD_PROGRAM_NAME = "program_name";
        public const String FLD_ENTITY_TYPE_ID = "entity_type_id";
		public const String FLD_ENTITY_TYPE_NAME = "entity_type_name";
		public const String FLD_TAX_POSTAL_ADDRESS_TYPE_ID = "tax_postal_address_type_id";
		public const String FLD_TAX_POSTAL_ADDRESS_TYPE_NAME = "tax_postal_address_type_name";
		public const String FLD_CLOSING_TIMES = "closing_time";
		public const String FLD_IS_PRODUCT_PRICE_UPDATABLE = "is_product_price_updatable";
        public const String FLD_IS_QUANTITY_ADJUSTMENT_ALLOWED = "is_quantity_adjustment_allowed";
		public const String FLD_IMAGE_URL = "image_url";
		public const String FLD_IS_BASE_FORM = "is_base_form";	
		public const String FLD_PARENT_FORM_ID = "parent_form_id";	
		public const String FLD_ENABLED = "enabled";
        public const String FLD_ACTIVE_PROGRAM_AGREEMENT = "active_program_agreement";
        public const String FLD_WAREHOUSE_TYPE_ID = "warehouse_type_id";    // new field

		public const String FLD_DELETED = "deleted";
		public const String FLD_CREATE_USER_ID = "create_user_id";
		public const String FLD_CREATE_DATE = "create_date";
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		public const String FLD_UPDATE_DATE = "update_date";
    
		public FormTable() : 
                base(TBL_FORM) {
            this.InitClass();
        }
		    
        public FormTable(DataTable table) : 
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
            FormTable cln = ((FormTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new FormTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_FORM;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_FORM_GROUP_ID, typeof(System.Int32));
			columns.Add(FLD_VERSION, typeof(System.Int32));
			columns.Add(FLD_FORM_CODE, typeof(System.String));			
			columns.Add(FLD_FORM_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_PROGRAM_BASICS_TEXT, typeof(System.String));
			columns.Add(FLD_ORDER_TERMS_TEXT, typeof(System.String));
			columns.Add(FLD_PROGRAM_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_PROGRAM_TYPE_NAME, typeof(System.String));
            columns.Add(FLD_PROGRAM_ID, typeof(System.Int32));
            columns.Add(FLD_PROGRAM_NAME, typeof(System.String));
            columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_TAX_POSTAL_ADDRESS_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_TAX_POSTAL_ADDRESS_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_CLOSING_TIMES, typeof(System.DateTime));
			columns.Add(FLD_IS_PRODUCT_PRICE_UPDATABLE, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_IS_QUANTITY_ADJUSTMENT_ALLOWED, typeof(System.Boolean)).DefaultValue = 1;
			columns.Add(FLD_IMAGE_URL, typeof(System.String));
			columns.Add(FLD_IS_BASE_FORM, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_PARENT_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_ENABLED, typeof(System.Boolean)).DefaultValue = 1;
            columns.Add(FLD_ACTIVE_PROGRAM_AGREEMENT, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_WAREHOUSE_TYPE_ID, typeof(System.Int32));

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
