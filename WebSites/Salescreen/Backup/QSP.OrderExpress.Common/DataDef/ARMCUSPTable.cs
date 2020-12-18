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
	public class ARMCUSPTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_ARMCUSP = "ARMCUSP";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_PKID = "ARCUST";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_NAME = "ARNMBL";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_ORG = "ARORBL";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_ATTN = "ARSPBL";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_ADDR1 = "ARADB1";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_ADDR2 = "ARADB2";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_CITY = "ARCYBL";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_STATE = "ARSTBL";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_BILL_ZIP = "ARZPBL";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_BILL_ZIP4 = "ARZ4BL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_BILL_PHONE = "ARPHBL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FAX = "ARFAX#";
		/// <value>The constant used for Tax Exemption Number field in the Account table. </value>
		public const String FLD_EMAIL = "AREMAL";
		/// <value>The constant used for Tax Exemption Expiration Date field in the Account table. </value>
		public const String FLD_SHIP_NAME = "ARNMSH";
		/// <value>The constant used for credit limit field in the Account table. </value>
		public const String FLD_SHIP_ORG = "ARORSH";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_SHIP_ATTN = "ARSPSH";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ADDR1 = "ARADS1";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ADDR2 = "ARADS2";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_CITY = "ARCYSH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_STATE = "ARSTSH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ZIP = "ARZPSH";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_SHIP_ZIP4 = "ARZ4SH";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_SHIP_POSTAL_CODE = "ARPHSH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_PHONE = "ARPHSH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RESIDENCE_CODE = "ARRESD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_STATE = "AR#STE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_COUNTY = "AR#CNT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_CITY = "AR#CTY";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_COMMENT = "ARCOMM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_WAREHOUSE = "AR#WHS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FSM = "AR#FSM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_EXEMPTION_NO = "ARTAXE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENROLLMENT = "AR#STD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORG_TYPE = "ARSTYP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORG_LEVEL = "ARSLEV";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TRADE_CLASS = "ARTCLS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREDIT_LIMIT = "ARAMLM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_COLL_AGENCY = "ARCOLL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_COLL_MONTH = "ARMOCL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_COLL_DAY = "ARDYCL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_COLL_YEAR = "ARYRCL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_COLL_CENTURY = "ARCNCL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_COLL_TOTAL = "ARAMTC";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREATE_MONTH = "ARMOEN";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREATE_DAY = "ARDYEN";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREATE_YEAR = "ARYREN";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_UPDATE_MONTH = "ARMOUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_UPDATE_DAY = "ARDYUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_UPDATE_YEAR = "ARYRUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_LAST_SALES_MONTH = "ARMOLS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_LAST_SALES_DAY = "ARDYLS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_LAST_SALES_YEAR = "ARYRLS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS = "ARSTAT";


		        
		public ARMCUSPTable() : 
                base(TBL_ARMCUSP) {
            this.InitClass();
        }
		    
        public ARMCUSPTable(DataTable table) : 
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
            ARMCUSPTable cln = ((ARMCUSPTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new ARMCUSPTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_ARMCUSP;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

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

			columns.Add(FLD_TAX_STATE, typeof(System.Decimal));
			columns.Add(FLD_TAX_COUNTY, typeof(System.Decimal));
			columns.Add(FLD_TAX_CITY, typeof(System.Decimal));
			columns.Add(FLD_COMMENT, typeof(System.String));
			columns.Add(FLD_WAREHOUSE, typeof(System.Int32));
			columns.Add(FLD_FSM, typeof(System.Decimal));
			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));
			columns.Add(FLD_ENROLLMENT, typeof(System.Int32));
			columns.Add(FLD_ORG_TYPE, typeof(System.String));
			columns.Add(FLD_ORG_LEVEL, typeof(System.String));
			columns.Add(FLD_TRADE_CLASS, typeof(System.String));
			
			columns.Add(FLD_CREDIT_LIMIT, typeof(System.Decimal));
			columns.Add(FLD_COLL_AGENCY, typeof(System.String));
			columns.Add(FLD_COLL_MONTH, typeof(System.Int32));
			columns.Add(FLD_COLL_DAY, typeof(System.Int32));
			columns.Add(FLD_COLL_YEAR, typeof(System.Int32));
			columns.Add(FLD_COLL_CENTURY, typeof(System.Int32));
			columns.Add(FLD_COLL_TOTAL, typeof(System.Int32));
			columns.Add(FLD_CREATE_MONTH, typeof(System.Int32));
			columns.Add(FLD_CREATE_DAY, typeof(System.Int32));
			columns.Add(FLD_CREATE_YEAR, typeof(System.Int32));
			columns.Add(FLD_UPDATE_MONTH, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DAY, typeof(System.Int32));
			columns.Add(FLD_UPDATE_YEAR, typeof(System.Int32));
			columns.Add(FLD_LAST_SALES_MONTH, typeof(System.Int32));
			columns.Add(FLD_LAST_SALES_DAY, typeof(System.Int32));
			columns.Add(FLD_LAST_SALES_YEAR, typeof(System.Int32));

			columns.Add(FLD_STATUS, typeof(System.String));



			
			
        			
		}
	}
}
