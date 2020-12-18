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
	public class FINGDSTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_FINGDS = "FINGDS";
		/// <value>The constant used for Warehouse Number field in the Account table. </value>
		public const String FLD_FGWHSE = "FGWHSE";
		/// <value>The constant used for Product Item Number field in the Account table. </value>
		public const String FLD_FGITEM = "FGITEM";
		/// <value>The constant used for Product coupon Code field in the Account table. </value>
		public const String FLD_FGCOUP = "FGCOUP";
		/// <value>The constant used for Product availability in the Account table. </value>
		public const String FLD_AVAILABLE = "FGAVAL";

		        
		public FINGDSTable() : 
                base(TBL_FINGDS) {
            this.InitClass();
        }
		    
        public FINGDSTable(DataTable table) : 
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
            FINGDSTable cln = ((FINGDSTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new FINGDSTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_FINGDS;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_FGWHSE, typeof(System.String));

			columns.Add(FLD_FGITEM, typeof(System.String));
			columns.Add(FLD_FGCOUP, typeof(System.String));
			columns.Add(FLD_AVAILABLE, typeof(System.String));
			



			
			
        			
		}
	}
}
