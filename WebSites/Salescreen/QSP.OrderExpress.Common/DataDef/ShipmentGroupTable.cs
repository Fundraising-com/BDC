using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of OrganizationTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type OrganizationTable to be remoted.
	///     </remarks>
	/// </summary>
	/// 
	    
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class ShipmentGroupTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Organization table. </value>
		public const String TBL_SHIPMENT_GROUP = "shipment_group";
		/// <value>The constant used for PKId field in the Organization table. </value>
		public const String FLD_PKID = "shipment_group_id";
		/// <value>The constant used for Organization Name field in the Organization table. </value>
		public const String FLD_SHIPPING_POSTAL_ADDRESS_ID = "shipping_postal_address_id";
		/// <value>The constant used for the order "Shipping Phone Number ID" field in the Order table. </value>
		public const String FLD_SHIPPING_PHONE_NUMBER_ID = "shipping_phone_number_id";
		/// <value>The constant used for the order "Shipping Fax Number ID" field in the Order table. </value>
		public const String FLD_SHIPPING_FAX_NUMBER_ID = "shipping_fax_number_id";
		/// <value>The constant used for the order "Shipping Phone Number ID" field in the Order table. </value>
		public const String FLD_SHIPPING_EMAIL_ADDRESS_ID = "shipping_email_id";
		/// <value>The constant used for Shipping Date field in the Organization table. </value>
		public const String FLD_SHIPMENT_DATE = "shipment_date";		
		/// <value>The constant used for Shipping Date field in the Organization table. </value>
		public const String FLD_REQUESTED_DELIVERY_DATE = "requested_delivery_date";
        public const String FLD_REQUESTED_DELIVERY_TIME = "requested_delivery_time";		
		/// <value>The constant used for Shipping Date field in the Organization table. </value>
		public const String FLD_DELIVERY_NLT = "delivery_no_later_than";		
		/// <value>The constant used for Shipping Date field in the Organization table. </value>
		public const String FLD_NB_DAY_LEAD_TIME = "nb_day_lead_time";		
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
		public const String FLD_SHIPPING_CHARGES= "shipping_charges";
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
        public const String FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID = "expedited_freight_charge_payment_assignment_type_id";
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
        public const String FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_NAME = "expedited_freight_charge_payment_assignment_type_name";
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
		public const String FLD_DELIVERY_WAREHOUSE_ID = "delivery_warehouse_id";
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
		public const String FLD_DELIVERY_FULF_WAREHOUSE_ID = "delivery_fulf_warehouse_id";		
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
		public const String FLD_DELIVERY_WAREHOUSE_NAME = "warehouse_name";
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
		public const String FLD_DELIVERY_METHOD_ID = "delivery_method_id";
		/// <value>The constant used for Shipping Charges field in the Organization table. </value>
		public const String FLD_DELIVERY_METHOD_NAME = "delivery_method_name";
		/// <value>The constant used for Address Title field in the Postal Address table. </value>
		public const String FLD_TITLE = "TitleAddress";
		/// <value>The constant used for the order "Source ID" field in the Order table. </value>
		public const String FLD_SHIP_SUPPLY_ID = "shipment_supply_group_id";	
		/// <value>The constant used for the order "Source ID" field in the Order table. </value>
		public const String FLD_SHIP_SUPPLY_TO = "shipment_supply_to";	
		/// <value>The constant used for Shipping Date field in the Organization table. </value>
		public const String FLD_SUPPLY_REQUESTED_DELIVERY_DATE = "supply_requested_delivery_date";		
		/// <value>The constant used for the order "Source ID" field in the Order table. </value>
		public const String FLD_SUPPLY_DELIVERY_NLT = "supply_delivery_nlt";	
		

		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public ShipmentGroupTable() : 
                base(TBL_SHIPMENT_GROUP) {
            this.InitClass();
        }
		    
        public ShipmentGroupTable(DataTable table) : 
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
            ShipmentGroupTable cln = ((ShipmentGroupTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new ShipmentGroupTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {
			//
			// Create the Groups table
			//
			this.TableName =  TBL_SHIPMENT_GROUP;	
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID >= 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			//Column.DefaultValue = 0;
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_SHIPPING_POSTAL_ADDRESS_ID, typeof(System.Int32));			
			columns.Add(FLD_SHIPPING_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_SHIPPING_FAX_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_SHIPPING_EMAIL_ADDRESS_ID, typeof(System.Int32));
			columns.Add(FLD_DELIVERY_METHOD_ID, typeof(System.Int32));
			columns.Add(FLD_DELIVERY_METHOD_NAME, typeof(System.String));
			columns.Add(FLD_DELIVERY_WAREHOUSE_ID, typeof(System.Int32));	
			columns.Add(FLD_DELIVERY_FULF_WAREHOUSE_ID, typeof(System.Int32));
			columns.Add(FLD_DELIVERY_WAREHOUSE_NAME, typeof(System.String));
			columns.Add(FLD_REQUESTED_DELIVERY_DATE, typeof(System.DateTime));
			columns.Add(FLD_SHIPMENT_DATE, typeof(System.DateTime));	
			columns.Add(FLD_DELIVERY_NLT, typeof(System.DateTime));	
			columns.Add(FLD_NB_DAY_LEAD_TIME, typeof(System.Int32));	
			columns.Add(FLD_SHIPPING_CHARGES, typeof(System.Decimal));
			columns.Add(FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID, typeof(System.Int32)).DefaultValue = 0;
			columns.Add(FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_TITLE, typeof(System.String));
			columns.Add(FLD_SHIP_SUPPLY_ID, typeof(System.Int32)).DefaultValue = -1;;
			columns.Add(FLD_SUPPLY_REQUESTED_DELIVERY_DATE, typeof(System.DateTime));
			columns.Add(FLD_SUPPLY_DELIVERY_NLT, typeof(System.DateTime));				
			columns.Add(FLD_SHIP_SUPPLY_TO, typeof(System.Int32)).DefaultValue = 2; //Same than the Order
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));
            columns.Add(FLD_REQUESTED_DELIVERY_TIME, typeof(System.DateTime));

        }
        
        
	}
		
}

