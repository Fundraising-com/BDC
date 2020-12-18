using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of AccountTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type AccountTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class IVMWHSPTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_IVMWHSP = "IVMWHSP";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_PKID = "IV#WHS";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_NAME = "IVNMWH";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_COMPANY_NAME = "IVCOWH";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_ADDRESS_ATTN = "IVATWH";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ADDRESS_LINE_1 = "IVADW1";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ADDRESS_LINE_2 = "IVADW2";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ADDRESS_CITY = "IVCYWH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ADDRESS_STATE = "IVSTWH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_ADDRESS_ZIP = "IVZPWH";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_ADDRESS_ZIP4 = "IVZ4WH";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_ADDRESS_POST_OFFICE_BOX = "IVBXWH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_PHONE_NUMBER = "IVPHWH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FAX = "IVFXWH";
		/// <value>The constant used for Tax Exemption Number field in the Account table. </value>
		public const String FLD_EMAIL = "IVEMAD";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_RECEIVING_PHONE_NUMBER = "IVPHRC";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_RECEIVING_PHONE_EXTENSION = "IVEXRC";		

		/// <value>The constant used for Tax Exemption Number field in the Account table. </value>
		public const String FLD_ALTERNATE_WAREHOUSE_1 = "IV#AL1";
		/// <value>The constant used for Tax Exemption Number field in the Account table. </value>
		public const String FLD_ALTERNATE_WAREHOUSE_2 = "IV#AL2";
		/// <value>The constant used for Tax Exemption Number field in the Account table. </value>
		public const String FLD_VENDOR_ID = "IVCDVN";
		
		/// <value>The constant used for Tax Exemption Expiration Date field in the Account table. </value>
		public const String FLD_EDI = "IVEDI";
		/// <value>The constant used for credit limit field in the Account table. </value>
		public const String FLD_EDI_PARTNER = "IVEDIP";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_COUPON = "IVDFCP";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_RELEASE_CODE = "IVCDRL";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_PO_FOB = "IVFOB";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_VSA_DIVISION = "IV#VSA";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_LAST_UPDATE_MONTH = "IVMOUP";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_LAST_UPDATE_DAY = "IVDYUP";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_LAST_UPDATE_YEAR = "IVYRUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_USER_ID = "IVUSER";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS = "IVWSTA";


		        
		public IVMWHSPTable() : 
                base(TBL_IVMWHSP) {
            this.InitClass();
        }
		    
        public IVMWHSPTable(DataTable table) : 
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
            IVMWHSPTable cln = ((IVMWHSPTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new IVMWHSPTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_IVMWHSP;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_COMPANY_NAME, typeof(System.String));
			columns.Add(FLD_ADDRESS_ATTN, typeof(System.String));
			columns.Add(FLD_ADDRESS_LINE_1, typeof(System.String));		
			columns.Add(FLD_ADDRESS_LINE_2, typeof(System.String));	
			columns.Add(FLD_ADDRESS_CITY, typeof(System.String));	
			columns.Add(FLD_ADDRESS_STATE, typeof(System.String));		
			columns.Add(FLD_ADDRESS_ZIP, typeof(System.Int32));	
			columns.Add(FLD_ADDRESS_ZIP4, typeof(System.Int32));
			columns.Add(FLD_ADDRESS_POST_OFFICE_BOX, typeof(System.String));
			columns.Add(FLD_PHONE_NUMBER, typeof(System.String));
			columns.Add(FLD_FAX, typeof(System.String));
			columns.Add(FLD_EMAIL, typeof(System.String));
			columns.Add(FLD_RECEIVING_PHONE_NUMBER, typeof(System.String));		
			columns.Add(FLD_RECEIVING_PHONE_EXTENSION, typeof(System.String));	
			columns.Add(FLD_ALTERNATE_WAREHOUSE_1, typeof(System.Int32));
			columns.Add(FLD_ALTERNATE_WAREHOUSE_2, typeof(System.Int32));
			columns.Add(FLD_VENDOR_ID, typeof(System.String));
			columns.Add(FLD_EDI, typeof(System.String));
			columns.Add(FLD_EDI_PARTNER, typeof(System.String));
			columns.Add(FLD_COUPON, typeof(System.String));
			columns.Add(FLD_RELEASE_CODE, typeof(System.String));		
			columns.Add(FLD_PO_FOB, typeof(System.String));		
			columns.Add(FLD_VSA_DIVISION, typeof(System.Int32));		
			columns.Add(FLD_LAST_UPDATE_MONTH, typeof(System.Int32));		
			columns.Add(FLD_LAST_UPDATE_DAY, typeof(System.Int32));
			columns.Add(FLD_LAST_UPDATE_YEAR, typeof(System.Int32));
			columns.Add(FLD_USER_ID, typeof(System.String));

			columns.Add(FLD_STATUS, typeof(System.String));



			
			
        			
		}
	}
}
