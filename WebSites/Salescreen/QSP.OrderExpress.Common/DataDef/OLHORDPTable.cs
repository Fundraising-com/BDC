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
	public class OLHORDPTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_OLHORDP = "OLHORDP";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_PKID = "OL#ORD";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_FULF_ACCT_ID = "OLCUST";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_ORDER_ID = "OL#QID";
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
		public const String FLD_TAX_EXEMPTION_NO = "OLTAXE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_REMARK = "OLREMK";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FSM = "OL#FSM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FSM_NAME = "fm_name";		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREDIT_CARD = "OLCRCD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREDIT_CARD_NO = "OLCRNO";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CUST_PURCHASE_ORDER_NO = "OLCUPO";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORDER_TYPE = "OLTYPE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENROLLMENT = "OL#GRP";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_MONTH = "OLMODL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_DAY = "OLDYDL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_YEAR = "OLYRDL";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NLT_MONTH = "OLMOTH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NLT_DAY = "OLDYTH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NLT_YEAR = "OLYRTH";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NOTE1 = "OLDEL1";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NOTE2 = "OLDEL2";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_PICKUP_AT_WHS = "OLPKUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FULF_WHS_ID = "OL#PWS";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TOTAL_AMOUNT = "OLESSA";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TOTAL_TAX_AMOUNT = "OLESTX";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_PERCENTAGE = "OLEPTX";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TOTAL_FREIGHT_AMOUNT = "OLESFT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TOTAL_QUANTITY = "OL#ESQ";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORDER_STAGE = "OLSTAG";		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OPERATION_TYPE = "OLRCCD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS = "OLSTAT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS_NAME = "order_status_name";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS_COLOR_CODE = "color_code";


		        
		public OLHORDPTable() : 
                base(TBL_OLHORDP) {
            this.InitClass();
        }
		    
        public OLHORDPTable(DataTable table) : 
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
            OLHORDPTable cln = ((OLHORDPTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OLHORDPTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_OLHORDP;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_ORDER_ID, typeof(System.Int32));
			columns.Add(FLD_FULF_ACCT_ID, typeof(System.Int32));

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

			columns.Add(FLD_REMARK, typeof(System.String));

			columns.Add(FLD_FSM, typeof(System.Decimal));
			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));

			columns.Add(FLD_CREDIT_CARD, typeof(System.String));
			columns.Add(FLD_CREDIT_CARD_NO, typeof(System.Int32));
			columns.Add(FLD_CUST_PURCHASE_ORDER_NO, typeof(System.String));
			columns.Add(FLD_ORDER_TYPE, typeof(System.String));		
			columns.Add(FLD_ENROLLMENT, typeof(System.Int32));

			columns.Add(FLD_DELIVERY_MONTH, typeof(System.Int32));	
			columns.Add(FLD_DELIVERY_DAY, typeof(System.Int32));	
			columns.Add(FLD_DELIVERY_YEAR, typeof(System.Int32));	
			
			columns.Add(FLD_DELIVERY_NLT_MONTH, typeof(System.Int32));	
			columns.Add(FLD_DELIVERY_NLT_DAY, typeof(System.Int32));	
			columns.Add(FLD_DELIVERY_NLT_YEAR, typeof(System.Int32));	

			columns.Add(FLD_DELIVERY_NOTE1, typeof(System.String));	
			columns.Add(FLD_DELIVERY_NOTE2, typeof(System.String));	
		
			columns.Add(FLD_PICKUP_AT_WHS, typeof(System.String));	
			columns.Add(FLD_FULF_WHS_ID, typeof(System.Int32));	


			columns.Add(FLD_OPERATION_TYPE, typeof(System.String));
			columns.Add(FLD_STATUS, typeof(System.Int32));

			//Add supplementary field to have the description
			columns.Add(FLD_FSM_NAME, typeof(System.String));
			columns.Add(FLD_STATUS_NAME, typeof(System.String));
			columns.Add(FLD_STATUS_COLOR_CODE, typeof(System.String));




			
			
        			
		}
	}
}
