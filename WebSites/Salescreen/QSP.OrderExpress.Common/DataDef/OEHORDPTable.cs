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
	public class OEHORDPTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_OEHORDP = "OEHORDP"; 
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_PKID = "OE#ORD";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_FULF_ACCT_ID = "OECUST";
		/// <value>The constant used for Tax Exemption Expiration Date field in the Account table. </value>
		public const String FLD_SHIP_NAME = "OENMSH";
		/// <value>The constant used for credit limit field in the Account table. </value>
		public const String FLD_SHIP_ORG = "OEORSH";
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_SHIP_ATTN = "OESPSH";
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ADDR1 = "OEADS1";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ADDR2 = "OEADS2";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_CITY = "OECYSH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_STATE = "OESTSH";		
		/// <value>The constant used for Account Type ID field in the Account table. </value>
		public const String FLD_SHIP_ZIP = "OEZPSH";		
		/// <value>The constant used for Organization ID field in the Account table. </value>
		public const String FLD_SHIP_ZIP4 = "OEZ4SH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_PHONE = "OEPHSH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RESIDENCE_CODE = "OERESD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_STATE_CODE = "OE#STE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_COUNTY_CODE = "OE#CNT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_CITY_CODE = "OE#CTY";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_EXEMPTION_NO = "OETAXE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_REMARK = "OEREMK";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DEFAULT_FULF_WHS_ID = "OE#WDE";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FSM = "OE#FSM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FSM_NAME = "fm_name";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENTERED_MONTH = "OEMOEN";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENTERED_DAY = "OEDYEN";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENTERED_YEAR = "OEYREN";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENTERED_TIME = "OETMEN";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_EDITED_MONTH = "OEMOED";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_EDITED_DAY = "OEDYED";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_EDITED_YEAR = "OEYRED";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_EDITED_TIME = "OETMED";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OPENED_MONTH = "OEMOPS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OPENED_DAY = "OEDYPS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OPENED_YEAR = "OEYRPS";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OPENED_TIME = "OETMPS";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELEASE_MONTH = "OEMORL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELEASE_DAY = "OEDYRL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELEASE_YEAR = "OEYRRL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELEASE_CENTURY = "OECNRL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELEASE_TIME = "OETMRL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELEASE_BY = "OEBYRL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELEASE_OVERRIDE = "OEOVRL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_UNRELEASE_REASON = "OECDRL";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_MONTH = "OEMODL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_DAY = "OEDYDL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_YEAR = "OEYRDL";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NLT_MONTH = "OEMOTH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NLT_DAY = "OEDYTH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NLT_YEAR = "OEYRTH";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_PRESALE_CARD_NO = "OE#CRD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_EDIT_FLAG = "OEEDIT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREDIT_CARD = "OECRCD";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CREDIT_CARD_NO = "OECRNO";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_RELATED_ORDER_NO = "OE#ORR";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DEFAULT_REASON = "OERSDF";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CUST_PURCHASE_ORDER_NO = "OLCUPO";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_AFFECT_INVENTORY = "OEPOST";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORDER_TYPE = "OETYPE";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_USER_DEFINED_CODE = "OECODE";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DEFAULT_COMMISSION = "OEPCDF";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_EXTRA_COMMISSION = "OEPCEX";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SOB_COMMISSION = "OEPCSB";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_YUMMY_COMMISSION = "OEPCYM";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_NY_METRO_PRICING = "OEPTYM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_PRICING_TYPE = "OEPTYP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENROLLMENT = "OE#GRP";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NOTE1 = "OEDEL1";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_DELIVERY_NOTE2 = "OEDEL2";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_PICKUP_AT_WHS = "OEPKUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CARRIER_CODE = "OECARR";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CONFIRM_NO = "OECFRM";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_INVOICE_MONTH = "OEMOIV";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_INVOICE_DAY = "OEDYIV";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_INVOICE_YEAR = "OEYRIV";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_INVOICE_CENTURY = "OECNIV";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TOTAL_AMOUNT = "OEAMSA";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TOTAL_TAX_AMOUNT = "OEAMTX";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TAX_PERCENTAGE = "OEPCTX";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_NON_COMMISSION_AMOUNT = "OEAMNC";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_NON_COMMISSION_CODE = "OECDNC";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SPLIT_COMMISSION = "OESPCM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OVERRIDE_FSM = "OE#OFM";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OVERRIDE_COMMISSION_PERCENTAGE = "OEPCOV";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_OVERRIDE_CODE = "OECDOV";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_INVOICE_WAREHOUSE = "OE#WIV";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_INVOICE_WEIGHT = "OE#WGT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_TOTAL_QUANTITY = "OE#QTO";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ACCOUNTING_MONTH = "OEMOAC";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ACCOUNTING_YEAR = "OEYRAC";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ACCOUNTING_CENTURY = "OECNAC";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_FISCAL_YEAR = "OEFSCL";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_VIA = "OEVIA";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_PRO_NO = "OEPRO#";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_FULF_WHS_ID = "OE#WSH";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_MONTH = "OEMOSH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_DAY = "OEDYSH";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_SHIP_YEAR = "OEYRSH";		


		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_CALLED_CODE = "OECALL";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_IS_UNRELEASED = "OEUNRL";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_LETTER_PRINTED = "OELETR";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_IS_PRESALE = "OEPSAL";
		
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS = "OESTAT";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_STATUS_NAME = "status_name";

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_UPDATE_MONTH = "OEMOUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_UPDATE_DAY = "OEDYUP";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_UPDATE_YEAR = "OEYRUP";	

		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_USER_ID = "OEUSER";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ENTERED_BY = "OEBYEN";
		/// <value>The constant used for FM ID field in the Account table. </value>
		public const String FLD_ORIGINAL_INVOICE_NO = "OE#ORX";	

		


		        
		public OEHORDPTable() : 
                base(TBL_OEHORDP) {
            this.InitClass();
        }
		    
        public OEHORDPTable(DataTable table) : 
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
            OEHORDPTable cln = ((OEHORDPTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OEHORDPTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_OEHORDP;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_FULF_ACCT_ID, typeof(System.String));

			columns.Add(FLD_SHIP_NAME, typeof(System.String));
			columns.Add(FLD_SHIP_ORG, typeof(System.String));
			columns.Add(FLD_SHIP_ATTN, typeof(System.String));
			columns.Add(FLD_SHIP_ADDR1, typeof(System.String));
			columns.Add(FLD_SHIP_ADDR2, typeof(System.String));
			columns.Add(FLD_SHIP_CITY, typeof(System.String));	
			columns.Add(FLD_SHIP_STATE, typeof(System.String));
			columns.Add(FLD_SHIP_ZIP, typeof(System.Int32));
			columns.Add(FLD_SHIP_ZIP4, typeof(System.Int32));
			columns.Add(FLD_SHIP_PHONE, typeof(System.String));
			columns.Add(FLD_RESIDENCE_CODE, typeof(System.String));

			columns.Add(FLD_TAX_STATE_CODE, typeof(System.Int32));
			columns.Add(FLD_TAX_COUNTY_CODE, typeof(System.Int32));
			columns.Add(FLD_TAX_CITY_CODE, typeof(System.Int32));

			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));
			columns.Add(FLD_REMARK, typeof(System.String));

			columns.Add(FLD_DEFAULT_FULF_WHS_ID, typeof(System.Int32));
			columns.Add(FLD_FSM, typeof(System.Int32));
			columns.Add(FLD_FSM_NAME, typeof(System.String));

			columns.Add(FLD_ENTERED_MONTH, typeof(System.Int32));
			columns.Add(FLD_ENTERED_DAY, typeof(System.Int32));
			columns.Add(FLD_ENTERED_YEAR, typeof(System.Int32));
			columns.Add(FLD_ENTERED_TIME, typeof(System.Int32));

			columns.Add(FLD_EDITED_MONTH, typeof(System.Int32));
			columns.Add(FLD_EDITED_DAY, typeof(System.Int32));
			columns.Add(FLD_EDITED_YEAR, typeof(System.Int32));
			columns.Add(FLD_EDITED_TIME, typeof(System.Int32));

			columns.Add(FLD_OPENED_MONTH, typeof(System.Int32));
			columns.Add(FLD_OPENED_DAY, typeof(System.Int32));
			columns.Add(FLD_OPENED_YEAR, typeof(System.Int32));
			columns.Add(FLD_OPENED_TIME, typeof(System.Int32));

			columns.Add(FLD_RELEASE_MONTH, typeof(System.Int32));
			columns.Add(FLD_RELEASE_DAY, typeof(System.Int32));
			columns.Add(FLD_RELEASE_YEAR, typeof(System.Int32));
			columns.Add(FLD_RELEASE_CENTURY, typeof(System.Int32));
			columns.Add(FLD_RELEASE_TIME, typeof(System.Int32));
			columns.Add(FLD_RELEASE_BY, typeof(System.String));
			columns.Add(FLD_RELEASE_OVERRIDE, typeof(System.String));
			columns.Add(FLD_UNRELEASE_REASON, typeof(System.String));

			columns.Add(FLD_DELIVERY_MONTH, typeof(System.Int32));
			columns.Add(FLD_DELIVERY_DAY, typeof(System.Int32));
			columns.Add(FLD_DELIVERY_YEAR, typeof(System.Int32));
			
			columns.Add(FLD_DELIVERY_NLT_MONTH, typeof(System.Int32));
			columns.Add(FLD_DELIVERY_NLT_DAY, typeof(System.Int32));
			columns.Add(FLD_DELIVERY_NLT_YEAR, typeof(System.Int32));

			columns.Add(FLD_PRESALE_CARD_NO, typeof(System.Int32));
			columns.Add(FLD_EDIT_FLAG, typeof(System.String));
			columns.Add(FLD_CREDIT_CARD, typeof(System.String));
			columns.Add(FLD_CREDIT_CARD_NO, typeof(System.Int32));

			columns.Add(FLD_RELATED_ORDER_NO, typeof(System.Int32));
			columns.Add(FLD_DEFAULT_REASON, typeof(System.String));
			columns.Add(FLD_CUST_PURCHASE_ORDER_NO, typeof(System.String));

			columns.Add(FLD_AFFECT_INVENTORY, typeof(System.String));
			columns.Add(FLD_ORDER_TYPE, typeof(System.String));
			columns.Add(FLD_USER_DEFINED_CODE, typeof(System.String));

			columns.Add(FLD_DEFAULT_COMMISSION, typeof(System.Decimal));
			columns.Add(FLD_EXTRA_COMMISSION, typeof(System.Decimal));
			columns.Add(FLD_SOB_COMMISSION, typeof(System.Decimal));
			columns.Add(FLD_YUMMY_COMMISSION, typeof(System.Decimal));

			columns.Add(FLD_NY_METRO_PRICING, typeof(System.String));
			columns.Add(FLD_PRICING_TYPE, typeof(System.String));
			columns.Add(FLD_ENROLLMENT, typeof(System.Int32));

			columns.Add(FLD_DELIVERY_NOTE1, typeof(System.String));
			columns.Add(FLD_DELIVERY_NOTE2, typeof(System.String));
			
			columns.Add(FLD_PICKUP_AT_WHS, typeof(System.String));
			columns.Add(FLD_CARRIER_CODE, typeof(System.String));
			columns.Add(FLD_CONFIRM_NO, typeof(System.String));

			columns.Add(FLD_INVOICE_MONTH, typeof(System.Int32));
			columns.Add(FLD_INVOICE_DAY, typeof(System.Int32));
			columns.Add(FLD_INVOICE_YEAR, typeof(System.Int32));
			columns.Add(FLD_INVOICE_CENTURY, typeof(System.Int32));

			columns.Add(FLD_TOTAL_AMOUNT, typeof(System.Decimal));
			columns.Add(FLD_TOTAL_TAX_AMOUNT, typeof(System.Decimal));
			columns.Add(FLD_TAX_PERCENTAGE, typeof(System.Decimal));
			columns.Add(FLD_NON_COMMISSION_AMOUNT, typeof(System.Decimal));
			columns.Add(FLD_NON_COMMISSION_CODE, typeof(System.String));
			columns.Add(FLD_SPLIT_COMMISSION, typeof(System.String));
			columns.Add(FLD_OVERRIDE_FSM, typeof(System.Int32));
			columns.Add(FLD_OVERRIDE_COMMISSION_PERCENTAGE, typeof(System.Decimal));
			columns.Add(FLD_OVERRIDE_CODE, typeof(System.Int32));

			columns.Add(FLD_INVOICE_WAREHOUSE, typeof(System.Int32));
			columns.Add(FLD_INVOICE_WEIGHT, typeof(System.Decimal));
			columns.Add(FLD_TOTAL_QUANTITY, typeof(System.Int32));
			
			columns.Add(FLD_ACCOUNTING_MONTH, typeof(System.Int32));
			columns.Add(FLD_ACCOUNTING_YEAR, typeof(System.Int32));
			columns.Add(FLD_ACCOUNTING_CENTURY, typeof(System.Int32));
			columns.Add(FLD_FISCAL_YEAR, typeof(System.Int32));

			columns.Add(FLD_SHIP_VIA, typeof(System.String));
			columns.Add(FLD_PRO_NO, typeof(System.String));

			columns.Add(FLD_SHIP_FULF_WHS_ID, typeof(System.Int32));

			columns.Add(FLD_SHIP_MONTH, typeof(System.Int32));
			columns.Add(FLD_SHIP_DAY, typeof(System.Int32));
			columns.Add(FLD_SHIP_YEAR, typeof(System.Int32));


			columns.Add(FLD_CALLED_CODE, typeof(System.String));

			columns.Add(FLD_IS_UNRELEASED, typeof(System.String));
			columns.Add(FLD_LETTER_PRINTED, typeof(System.String));
			columns.Add(FLD_IS_PRESALE, typeof(System.String));
			
			columns.Add(FLD_STATUS, typeof(System.String));
			columns.Add(FLD_STATUS_NAME, typeof(System.String));

			columns.Add(FLD_UPDATE_MONTH, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DAY, typeof(System.Int32));
			columns.Add(FLD_UPDATE_YEAR, typeof(System.Int32));

			columns.Add(FLD_USER_ID, typeof(System.String));
			columns.Add(FLD_ENTERED_BY, typeof(System.String));
			columns.Add(FLD_ORIGINAL_INVOICE_NO, typeof(System.Int32));
			
        			
		}
	}
}
