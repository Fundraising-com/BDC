using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
    ///         This class is used to define the shape of TAXFLETable.
	///     </remarks>
	///     <remarks>
    ///         The serializale constructor allows objects of type TAXFLETable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class TAXFLETable : DataTable, System.Collections.IEnumerable
	{
		//
        //TAXFLE constants        
        // 
        /// <value>The constant used for TAXFLE table. </value>
		public const String TBL_TAXFLE = "TAXFLE";
        /// <value>The constant used for STATUS CODE field in the TAXFLE table. </value>
        public const String FLD_STATUS = "STATUS";
        /// <value>The constant used for STATE-COUNTY-CITY CODE field in the TAXFLE table. </value>
        public const String FLD_STCOCI = "STCOCI";
        /// <value>The constant used for CITY NAME field in the TAXFLE table. </value>
        public const String FLD_CITYNM = "CITYNM";
        /// <value>The constant used for FOOD STATE TAX in the TAXFLE table. </value>
        public const String FLD_FDSTTX = "FDSTTX";
        /// <value>The constant used for FOOD COUNTY TAX in the TAXFLE table. </value>
        public const String FLD_FDCOTX = "FDCOTX";
        /// <value>The constant used for FOOD CITY TAX in the TAXFLE table. </value>
        public const String FLD_FDCITX = "FDCITX";
        /// <value>The constant used for FOOD TAX CODE in the TAXFLE table. </value>
        public const String FLD_FDTXCD = "FDTXCD";
        /// <value>The constant used for GIFT STATE TAX in the TAXFLE table. </value>
        public const String FLD_GISTTX = "GISTTX";
        /// <value>The constant used for GIFT COUNTY TAX in the TAXFLE table. </value>
        public const String FLD_GICOTX = "GICOTX";
        /// <value>The constant used for GIFT CITY TAX in the TAXFLE table. </value>
        public const String FLD_GICITX = "GICITX";
        /// <value>The constant used for GIFT TAX CODE in the TAXFLE table. </value>
        public const String FLD_GITXCD = "GITXCD";
        /// <value>The constant used for DATE LAST UPDATE in the TAXFLE table. </value>
        public const String FLD_DTLUPD = "DTLUPD";
        /// <value>The constant used for STATE NAME in the TAXFLE table. </value>
        public const String FLD_STXNAM = "STXNAM";
        /// <value>The constant used for COUNTY NAME in the TAXFLE table. </value>
        public const String FLD_CNTY = "CNTY";
        /// <value>The constant used for EXTRA SPACE in the TAXFLE table. </value>
        public const String FLD_UNONE = "UNONE";

		        
		public TAXFLETable() : 
                base(TBL_TAXFLE) {
            this.InitClass();
        }
		    
        public TAXFLETable(DataTable table) : 
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
            TAXFLETable cln = ((TAXFLETable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new TAXFLETable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_TAXFLE;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_STCOCI, typeof(System.Int32));

            columns.Add(FLD_STATUS, typeof(System.String));
			columns.Add(FLD_CITYNM, typeof(System.String));
			columns.Add(FLD_FDSTTX, typeof(System.Decimal));
            columns.Add(FLD_FDCOTX, typeof(System.Decimal));
            columns.Add(FLD_FDCITX, typeof(System.Decimal));
            columns.Add(FLD_FDTXCD, typeof(System.String));
            columns.Add(FLD_GISTTX, typeof(System.Decimal));
            columns.Add(FLD_GICOTX, typeof(System.Decimal));
            columns.Add(FLD_GICITX, typeof(System.Decimal));
            columns.Add(FLD_GITXCD, typeof(System.String));
            columns.Add(FLD_DTLUPD, typeof(System.Int32));
            columns.Add(FLD_STXNAM, typeof(System.String));
            columns.Add(FLD_CNTY, typeof(System.String));
            columns.Add(FLD_UNONE, typeof(System.String));			
			
        			
		}
	}
}
