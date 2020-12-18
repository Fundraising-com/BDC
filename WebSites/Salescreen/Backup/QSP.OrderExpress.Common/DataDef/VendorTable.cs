using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing vendor information.
	///     <remarks>
	///         This class is used to define the shape of VendorData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type VendorData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class VendorTable : DataTable, System.Collections.IEnumerable
	{		
		//
		//Vendor constants
		//
		/// <value>The constant used for Vendors table. </value>
		public const String TBL_VENDOR = "vendor";
		/// <value>The constant used for PKId field in the Vendors table. </value>
		public const String FLD_PKID = "vendor_id";
		/// <value>The constant used for PKId field in the Vendors table. </value>
		public const String FLD_FULF_VENDOR_ID = "fulf_vendor_id";
		/// <value>The constant used for Vendor_Name field in the Vendors table. </value>
		public const String FLD_CODE = "vendor_code";
		/// <value>The constant used for Vendor_Name field in the Vendors table. </value>
		public const String FLD_NAME = "vendor_name";
		/// <value>The constant used for Password field in the Vendors table. </value>
		public const String FLD_POSTAL_ADDRESS_ID  = "postal_address_id";
		/// <value>The constant used for title field in the Vendors table. </value>
		public const String FLD_PHONE_NUMBER_ID = "phone_number_id";
		/// <value>The constant used for title field in the Vendors table. </value>
		public const String FLD_FAX_NUMBER_ID = "fax_number_id";
		/// <value>The constant used for Email field in the Vendors table. </value>		
		public const String FLD_EMAIL_ID = "email_id";
		/// <value>The constant used for "Best time to call" field in the Vendors table. </value>
		public const String FLD_DIVISION = "division";
		/// <value>The constant used for "Day phone no" field in the Vendors table. </value>
		public const String FLD_VENDOR_TERM  = "vendor_term";
		/// <value>The constant used for "Evening phone no" field in the Vendors table. </value>
		public const String FLD_ORACLE_VENDOR_CODE  = "oracle_vendor_code";


		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create vendor id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update vendor id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public VendorTable() : 
                base(TBL_VENDOR) {
            this.InitClass();
        }
		    
        public VendorTable(DataTable table) : 
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
		public VendorTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            VendorTable cln = ((VendorTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new VendorTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Vendors table
			//
			this.TableName = TBL_VENDOR;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1; 
           
            columns.Add(FLD_FULF_VENDOR_ID,typeof(System.String));
			columns.Add(FLD_CODE,typeof(System.String));
			columns.Add(FLD_NAME,typeof(System.String));
			columns.Add(FLD_POSTAL_ADDRESS_ID, typeof(System.Int32));
			columns.Add(FLD_PHONE_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_FAX_NUMBER_ID, typeof(System.Int32));
			columns.Add(FLD_EMAIL_ID, typeof(System.Int32));
			columns.Add(FLD_DIVISION, typeof(System.String));
			columns.Add(FLD_VENDOR_TERM, typeof(System.String));
			columns.Add(FLD_ORACLE_VENDOR_CODE, typeof(System.String));

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

		
	}
}
