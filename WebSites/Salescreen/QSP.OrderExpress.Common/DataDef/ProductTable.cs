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
	public class ProductTable : DataTable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for product table. </value>
		public const String TBL_PRODUCT = "product";
		/// <value>The constant used for PKId field in the Product table. </value>
		public const String FLD_PKID = "product_id";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_CODE = "product_code";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_NAME = "product_name";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_PRODUCT_TYPE_ID = "product_type_id";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_VENDOR_ID = "vendor_id";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_VENDOR_NAME = "vendor_name";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_NB_UNITS = "nb_units";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_PRICE = "price";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_UNIT_COST = "unit_cost";
		/// <value>The constant used for product Unit field in the Order table. </value>
		public const String FLD_NB_DAY_LEAD_TIME = "nb_day_lead_time";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_VENDOR_ITEM_CODE = "vendor_item_code";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_ORACLE_CODE = "oracle_code";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_COMMISSION = "comission";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_IS_FREE_SAMPLE = "is_free_sample";
		/// <value>The constant used for the product name field in the Order table. </value>
		public const String FLD_IMAGE_URL = "image_url";


		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";

		/*ADDED BY ERIC CHAREST*/
		public const String FLD_PRODUCT_TYPE_NAME = "product_type_name";
		public const String FLD_BUSINESS_DIVISION_ID = "business_division_id";
		public const String FLD_BUSINESS_DIVISION_NAME = "business_division_name";
		public const String FLD_COUPON_ID = "coupon_id";

		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public ProductTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		
        
		/// <summary>
		///     Constructor for CampaignTable.  
		///     <remarks>Initialize a CampaignData instance by building the table schema.</remarks> 
		/// </summary>
		public ProductTable()
		{
			//
			// Create the tables in the dataset
			//
			BuildDataTable();
		}
                
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable: Orders
		//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Campaign table
			//
			this.TableName = TBL_PRODUCT;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;
            
			columns.Add(FLD_PRODUCT_TYPE_ID, typeof(System.Int32));			
			columns.Add(FLD_VENDOR_ID, typeof(System.Int32));
			columns.Add(FLD_COUPON_ID, typeof(System.Int32));
			columns.Add(FLD_VENDOR_NAME, typeof(System.String));
			columns.Add(FLD_CODE, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_NB_UNITS, typeof(System.Int32));
			columns.Add(FLD_PRICE, typeof(System.Decimal));
			columns.Add(FLD_UNIT_COST, typeof(System.Decimal));
			columns.Add(FLD_NB_DAY_LEAD_TIME, typeof(System.Int32));
			columns.Add(FLD_VENDOR_ITEM_CODE, typeof(System.String));
			columns.Add(FLD_ORACLE_CODE, typeof(System.String));
			columns.Add(FLD_COMMISSION, typeof(System.Decimal));
			columns.Add(FLD_IS_FREE_SAMPLE, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_IMAGE_URL, typeof(System.String));

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));		

			columns.Add(FLD_PRODUCT_TYPE_NAME,typeof(System.String));
			columns.Add(FLD_BUSINESS_DIVISION_ID, typeof(System.Int32));
			columns.Add(FLD_BUSINESS_DIVISION_NAME,typeof(System.String));			
		}

	}
}
