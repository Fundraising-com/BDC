using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of CatalogItemDetailTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CatalogItemDetailTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CatalogItemDetailTable : DataTable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for catalog_item_detail table. </value>
		public const String TBL_CATALOG_ITEM_DETAIL = "catalog_item_detail";
		/// <value>The constant used for PKId field in the CatalogItemDetail table. </value>
		public const String FLD_PKID = "catalog_item_detail_id";
		/// <value>The constant used for the catalog_item_detail name field in the Order table. </value>
		public const String FLD_CODE = "catalog_item_detail_code";
		/// <value>The constant used for the catalog_item_detail name field in the Order table. </value>
		public const String FLD_NAME = "catalog_item_detail_name";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_ID = "catalog_item_id";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_CODE = "catalog_item_code";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_NAME = "catalog_item_name";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_NB_UNITS = "catalog_item_nb_units";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_CATALOG_ITEM_DESC = "catalog_item_desc";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_PRODUCT_TYPE_ID = "product_type_id";		
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_NB_UNITS = "nb_units";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_PRICE = "price";
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_TERM = "term";		
		/// <value>The constant used for catalog_item_detail Unit field in the Order table. </value>
		public const String FLD_IS_DEFAULT = "is_default";	
		/// <value>The constant used for the order "Price" field in the Order table. </value>
		public const String FLD_TAX_RATE = "tax_rate";
		/// <value>The constant used for the order "Price" field in the Order table. </value>
		public const String FLD_DISPLAY_ORDER = "display_order";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_NB_DAY_LEAD_TIME = "nb_day_lead_time";
        /// <value>The constant used for product Unit field in the Order table. </value>
        public const String FLD_PROFIT_RATE = "profit_rate";
        /// <value>The constant used for product Unit field in the Order table. </value>
        public const String FLD_SCHOOL_PRICE = "school_price";
        public const String FLD_FORM_SECTION_TYPE_ID = "form_section_type_id";
        public const String FLD_FORM_SECTION_NUMBER = "form_section_number";
        
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";

		

		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public CatalogItemDetailTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		
        
		/// <summary>
		///     Constructor for CampaignTable.  
		///     <remarks>Initialize a CampaignData instance by building the table schema.</remarks> 
		/// </summary>
		public CatalogItemDetailTable()
		{
			//
			// Create the tables in the dataset
			//
			BuildDataTable();
		}
                
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable: CatalogItemDetail
		//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Campaign table
			//
			this.TableName = TBL_CATALOG_ITEM_DETAIL;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;
            
			columns.Add(FLD_PRODUCT_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_CATALOG_ITEM_ID, typeof(System.Int32));			
			columns.Add(FLD_CODE, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_NB_UNITS, typeof(System.Int32));
			columns.Add(FLD_PRICE, typeof(System.Decimal));
			columns.Add(FLD_TERM, typeof(System.Int32));
			columns.Add(FLD_IS_DEFAULT, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CATALOG_ITEM_CODE, typeof(System.String));	
			columns.Add(FLD_CATALOG_ITEM_NAME, typeof(System.String));	
			columns.Add(FLD_CATALOG_ITEM_DESC, typeof(System.String));	
			columns.Add(FLD_CATALOG_ITEM_NB_UNITS, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_TAX_RATE, typeof(System.Decimal)).DefaultValue = 0;
			columns.Add(FLD_DISPLAY_ORDER, typeof(System.Int32));
			columns.Add(FLD_NB_DAY_LEAD_TIME, typeof(System.Int32));
            columns.Add(FLD_FORM_SECTION_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_FORM_SECTION_NUMBER, typeof(System.Int32));
            
            columns.Add(FLD_PROFIT_RATE, typeof(System.Single)).DefaultValue = 0;
            //School Price
            DataColumn colSchoolPrice = columns.Add(FLD_SCHOOL_PRICE, typeof(System.Decimal));
            colSchoolPrice.Expression = "ISNULL (" + FLD_PRICE + ",0) / (1 - ISNULL (" + FLD_PROFIT_RATE + ",0))";			
			

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));		
			
		}

	}
}
