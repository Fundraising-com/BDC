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
	public class OLMCUSPTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_OLMCUSP = "OLMCUSP";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_PKID = "OLCUST";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_ACCOUNT_ID = "OL#INT";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_TYPE = "OLTYPE";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_NAME = "OLNMBL";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_ORG = "OLORBL";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_ATTN = "OLSPBL";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_ADDR1 = "OLADB1";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_ADDR2 = "OLADB2";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_CITY = "OLCYBL";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_STATE = "OLSTBL";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_ZIP = "OLZPBL";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_ZIP4 = "OLZ4BL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_BILL_PHONE = "OLPHBL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FAX = "OLFAX#";
		/// <value>The constant used for Tax Exemption Number field in the Account table. </value>
		public const String FLD_EMAIL = "OLEMAL";
		/// <value>The constant used for Tax Exemption Expiration Date field in the Account table. </value>
		public const String FLD_SHIP_NAME = "OLNMSH";
		/// <value>The constant used for credit limit field in the Account table. </value>
		public const String FLD_SHIP_ORG = "OLORSH";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_SHIP_ATTN = "OLSPSH";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ADDR1 = "OLADS1";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ADDR2 = "OLADS2";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_CITY = "OLCYSH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_STATE = "OLSTSH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ZIP = "OLZPSH";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_SHIP_ZIP4 = "OLZ4SH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_PHONE = "OLPHSH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RESIDENCE_CODE = "OLRESD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FSM = "OL#FSM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FSM_NAME = "fm_name";		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_EXEMPTION_NO = "OLTAXE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENROLLMENT = "OL#STD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORG_TYPE = "OLSTYP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORG_LEVEL = "OLSLEV";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TRADE_CLASS = "OLTCLS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREDIT_LIMIT = "OLAMLM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OPERATION_TYPE = "OLRCTP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS = "OLSTAT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS_NAME = "account_status_name";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS_COLOR_CODE = "color_code";


		        
		public OLMCUSPTable() : 
                base(TBL_OLMCUSP) {
            this.InitClass();
        }
		    
        public OLMCUSPTable(DataTable table) : 
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
            OLMCUSPTable cln = ((OLMCUSPTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OLMCUSPTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_OLMCUSP;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_ACCOUNT_ID, typeof(System.Int32));
			columns.Add(FLD_TYPE, typeof(System.String));  //For chocolate account it's WFC
			columns.Add(FLD_BILL_NAME, typeof(System.String));
			columns.Add(FLD_BILL_ORG, typeof(System.String));
			columns.Add(FLD_BILL_ATTN, typeof(System.String));
			columns.Add(FLD_BILL_ADDR1, typeof(System.String));
			columns.Add(FLD_BILL_ADDR2, typeof(System.String));
			columns.Add(FLD_BILL_CITY, typeof(System.String));
			columns.Add(FLD_BILL_STATE, typeof(System.String));
			columns.Add(FLD_BILL_ZIP, typeof(System.String));
			columns.Add(FLD_BILL_ZIP4, typeof(System.String));
			columns.Add(FLD_BILL_PHONE, typeof(System.String));

			columns.Add(FLD_FAX, typeof(System.String));
			columns.Add(FLD_EMAIL, typeof(System.String));

			columns.Add(FLD_SHIP_NAME, typeof(System.String));
			columns.Add(FLD_SHIP_ORG, typeof(System.String));
			columns.Add(FLD_SHIP_ATTN, typeof(System.String));
			columns.Add(FLD_SHIP_ADDR1, typeof(System.String));
			columns.Add(FLD_SHIP_ADDR2, typeof(System.String));
			columns.Add(FLD_SHIP_CITY, typeof(System.String));
			columns.Add(FLD_SHIP_STATE, typeof(System.String));
			columns.Add(FLD_SHIP_ZIP, typeof(System.String));
			columns.Add(FLD_SHIP_ZIP4, typeof(System.String));
			columns.Add(FLD_SHIP_PHONE, typeof(System.String));
			
			columns.Add(FLD_RESIDENCE_CODE, typeof(System.String));

			columns.Add(FLD_FSM, typeof(System.Decimal));
			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));
			columns.Add(FLD_ENROLLMENT, typeof(System.Int32));
			columns.Add(FLD_ORG_TYPE, typeof(System.String));
			columns.Add(FLD_ORG_LEVEL, typeof(System.String));
			columns.Add(FLD_TRADE_CLASS, typeof(System.String));			
			columns.Add(FLD_CREDIT_LIMIT, typeof(System.Decimal));
			columns.Add(FLD_OPERATION_TYPE, typeof(System.String));

			columns.Add(FLD_STATUS, typeof(System.String));

			//Add supplementary field to have the description
			columns.Add(FLD_FSM_NAME, typeof(System.String));
			columns.Add(FLD_STATUS_NAME, typeof(System.String));
			columns.Add(FLD_STATUS_COLOR_CODE, typeof(System.String));




			
			
        			
		}
	}
}
