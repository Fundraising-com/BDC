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
	///         The serializale constructor allows objects of type TaxCalculationMethodTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class TaxCalculationMethodTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_TAX_CALCULATION_METHOD = "tax_calculation_method";
		/// <value>The constant used for PKId tax_calculation_method in the Order table. </value>
		public const String FLD_PKID = "tax_calculation_method_id";
		/// <value>The constant used for the order "Form Name" tax_calculation_method in the Order table. </value>
		public const String FLD_SUBDIVISION_CODE = "subdivision_code";
		/// <value>The constant used for "Form Code" tax_calculation_method in the Order table. </value>
		public const String FLD_TAX_LEVEL_ID = "tax_level_id";
		/// <value>The constant used for the order "Form Name" tax_calculation_method in the Order table. </value>
        public const String FLD_TAX_LEVEL_NAME = "tax_level_name";
		/// <value>The constant used for "Form Code" tax_calculation_method in the Order table. </value>
		public const String FLD_PRODUCT_TYPE_ID = "product_type_id";
		/// <value>The constant used for the order "Form Name" tax_calculation_method in the Order table. </value>
        public const String FLD_ORGANIZATION_TYPE_ID = "organization_type_id";
		/// <value>The constant used for the order "Description" tax_calculation_method in the Order table. </value>
		public const String FLD_TAX_EXEMPTABLE = "tax_exemptable";
        /// <value>The constant used for "Form Code" tax_calculation_method in the Order table. </value>
        public const String FLD_MIN_SALES_THRESOLD = "min_sales_thresold";
        /// <value>The constant used for the order "Form Name" tax_calculation_method in the Order table. </value>
        public const String FLD_MIN_ITEM_THRESOLD = "min_item_thresold";
    
        
		public TaxCalculationMethodTable() : 
                base(TBL_TAX_CALCULATION_METHOD) {
            this.InitClass();
        }
		    
        public TaxCalculationMethodTable(DataTable table) : 
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
        
		protected TaxCalculationMethodTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            TaxCalculationMethodTable cln = ((TaxCalculationMethodTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new TaxCalculationMethodTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_TAX_CALCULATION_METHOD;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_SUBDIVISION_CODE, typeof(System.String));
			columns.Add(FLD_TAX_LEVEL_ID, typeof(System.Int32));
            columns.Add(FLD_PRODUCT_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORGANIZATION_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_TAX_EXEMPTABLE, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_MIN_SALES_THRESOLD, typeof(System.Decimal));
            columns.Add(FLD_MIN_ITEM_THRESOLD, typeof(System.Decimal));
						
		}

	}
}
