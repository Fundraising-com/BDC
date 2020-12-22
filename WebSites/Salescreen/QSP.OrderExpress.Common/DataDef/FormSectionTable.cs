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
	public class FormSectionTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Form Header constants
		// 
		/// <value>The constant used for Form table. </value>
		public const String TBL_FORM_SECTION = "form_section";
		/// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_PKID = "form_section_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_FORM_ID = "form_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_FORM_SECTION_TITLE = "form_section_title";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_FORM_SECTION_NUMBER = "form_section_number";
        // <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_FORM_SECTION_TYPE_ID = "form_section_type_id";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_CATALOG_ID = "catalog_id";
        // <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_CATALOG_ITEM_CATEGORY_ID = "catalog_item_category_id";
		// <value>The constant used for PKId field in the Form table. </value>
		public const String FLD_CATALOG_ITEM_CATEGORY_NAME = "catalog_item_category_name";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_DESCRIPTION = "description";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
		
		
		public FormSectionTable() : 
                base(TBL_FORM_SECTION) {
            this.InitClass();
        }
		    
        public FormSectionTable(DataTable table) : 
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
            FormSectionTable cln = ((FormSectionTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new FormSectionTable();
        }
        
        internal void InitVars() {
            
        }


        public bool IsContainFormSectionType(int FormSectionTypeID)
        {
            DataView dv = new DataView(this);
            string sFilter = "";
            sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionTypeID.ToString();

            dv.RowFilter = sFilter;

            return (dv.Count > 0);
        }

        public bool IsContainFormSectionType(int FormSectionTypeID, int FormSectionNumber)
        {
            DataView dv = new DataView(this);
            string sFilter = "";
            sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",1) = " + FormSectionTypeID.ToString();
            if (FormSectionNumber > 1)
            {
                sFilter = sFilter + " AND " + FLD_FORM_SECTION_NUMBER + " = " + FormSectionNumber.ToString();
            }
            else
            {
                sFilter = sFilter + " AND ISNULL(" + FLD_FORM_SECTION_NUMBER + ",0) <= 1";
            }
            dv.RowFilter = sFilter;

            return (dv.Count > 0);
        }
        
        private void InitClass() {	
			//
			// Create the Campaign table
			//
			this.TableName = TBL_FORM_SECTION;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
            Column.AutoIncrement = true;
            Column.AutoIncrementSeed = 0;
            Column.AutoIncrementStep = -1;
            

			columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_FORM_SECTION_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_FORM_SECTION_TITLE, typeof(System.String));
			columns.Add(FLD_FORM_SECTION_NUMBER, typeof(System.Int32));
            columns.Add(FLD_CATALOG_ID, typeof(System.Int32));
            columns.Add(FLD_CATALOG_ITEM_CATEGORY_ID, typeof(System.Int32));
			columns.Add(FLD_CATALOG_ITEM_CATEGORY_NAME, typeof(System.String));
            columns.Add(FLD_DESCRIPTION, typeof(System.String)); 
            columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
		}

	}
}
