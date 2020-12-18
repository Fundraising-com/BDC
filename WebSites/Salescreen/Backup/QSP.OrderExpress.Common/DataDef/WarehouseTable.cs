using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing warehouse information.
	///     <remarks>
	///         This class is used to define the shape of WarehouseData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type WarehouseData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class WarehouseTable : DataTable, System.Collections.IEnumerable
	{		
		//
		//Warehouse constants
		//
		/// <value>The constant used for Warehouses table. </value>
		public const String TBL_WAREHOUSE = "warehouse";
		/// <value>The constant used for PKId field in the Warehouses table. </value>
		public const String FLD_PKID = "warehouse_id";
		/// <value>The constant used for Warehouse_Name field in the Warehouses table. </value>
		public const String FLD_FULF_WAREHOUSE_ID = "fulf_warehouse_id";
		/// <value>The constant used for Warehouse_Name field in the Warehouses table. </value>
		public const String FLD_NAME = "warehouse_name";
		/// <value>The constant used for Warehouse_Name field in the Warehouses table. </value>
		public const String FLD_COMPANY_NAME = "company_name";
		/// <value>The constant used for Warehouse_Name field in the Warehouses table. </value>
		public const String FLD_SHORT_DESCRIPTION = "short_description";
		/// <value>The constant used for the order "Status ID" field in the Order table. </value>
		public const String FLD_WAREHOUSE_STATUS_ID = "warehouse_status_id";
		/// <value>The constant used for the order "Status ID" field in the Order table. </value>
		public const String FLD_WAREHOUSE_STATUS_NAME = "warehouse_status_name";
		/// <value>The constant used for the order "Status ID" field in the Order table. </value>
		public const String FLD_WAREHOUSE_STATUS_COLOR_CODE = "color_code";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_VENDOR_ID  = "vendor_id";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_VENDOR_NAME  = "vendor_name";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_POSTAL_ADDRESS_ID  = "postal_address_id";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_PHONE_NUMBER_ID  = "phone_number_id";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_FAX_NUMBER_ID  = "fax_number_id";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_RECEIVING_PHONE_NUMBER_ID  = "receiving_phone_number_id";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_EMAIL_ID  = "email_id";
		/// <value>The constant used for Password field in the Warehouses table. </value>
		public const String FLD_IS_VENDOR_WAREHOUSE  = "is_vendor_warehouse";
        /// <value>The constant used for Password field in the Warehouses table. </value>
        public const String FLD_PICK_UP = "pick_up";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create warehouse id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update warehouse id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public WarehouseTable() : 
                base(TBL_WAREHOUSE) {
            this.InitClass();
        }
		    
        public WarehouseTable(DataTable table) : 
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
        
		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public WarehouseTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            WarehouseTable cln = ((WarehouseTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new WarehouseTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Warehouses table
			//
			this.TableName = TBL_WAREHOUSE;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;            
            
			columns.Add(FLD_NAME,typeof(System.String));
			columns.Add(FLD_COMPANY_NAME,typeof(System.String));
			columns.Add(FLD_SHORT_DESCRIPTION,typeof(System.String));
			columns.Add(FLD_WAREHOUSE_STATUS_ID, typeof(System.Int32));
			columns.Add(FLD_WAREHOUSE_STATUS_NAME, typeof(System.String));
			columns.Add(FLD_WAREHOUSE_STATUS_COLOR_CODE, typeof(System.String));
			columns.Add(FLD_FULF_WAREHOUSE_ID,typeof(System.Int32));
			columns.Add(FLD_VENDOR_ID, typeof(System.Int32));
			columns.Add(FLD_VENDOR_NAME, typeof(System.String));
			columns.Add(FLD_POSTAL_ADDRESS_ID, typeof(System.Int32));
			columns.Add(FLD_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_FAX_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_RECEIVING_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_EMAIL_ID, typeof(System.Int32));
			columns.Add(FLD_IS_VENDOR_WAREHOUSE, typeof(System.Boolean));
            columns.Add(FLD_PICK_UP, typeof(System.Boolean)).DefaultValue = 0;

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

		
	}
}
