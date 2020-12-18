using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
    [System.ComponentModel.DesignerCategory("Code")]
    [SerializableAttribute] 
    public class ProgramAgreementCatalogTable : DataTable, System.Collections.IEnumerable
    {
        //
        //User constants
        // 
        /// <value>The constant used for ProgramAgreement table. </value>
        public const String TBL_PROGRAM_AGREEMENT_CATALOG = "program_agreement_catalog";
        /// <value>The constant used for PKId field in the ProgramAgreement table. </value>
        public const String FLD_PKID = "program_agreement_catalog_id";
        /// <value>The constant used for Organization ID field in the ProgramAgreement table. </value>
        public const String FLD_PROGRAM_AGREEMENT_ID = "program_agreement_id";
        public const string FLD_CATALOG_NAME = "catalog_name";
        public const String FLD_CATALOG_ID = "catalog_id";
        /// <value>The constant used for entity field in the Postal Address Entity table. </value>
        public const String FLD_ENTITY_TYPE_ID = "entity_type_id";
        /// <value>The constant used for entity field in the Postal Address Entity table. </value>
        public const String FLD_ENTITY_ID = "entity_id";	

        public ProgramAgreementCatalogTable() : 
                base(TBL_PROGRAM_AGREEMENT_CATALOG) {
            this.InitClass();
        }
    
		 public ProgramAgreementCatalogTable(DataTable table) : 
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
            ProgramAgreementCatalogTable cln = ((ProgramAgreementCatalogTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new ProgramAgreementCatalogTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_PROGRAM_AGREEMENT_CATALOG;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

            columns.Add(FLD_PROGRAM_AGREEMENT_ID, typeof(System.Int32));
            columns.Add(FLD_CATALOG_ID, typeof(System.Int32));
            columns.Add(FLD_CATALOG_NAME, typeof(System.String));
            columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_ENTITY_ID, typeof(System.Int32));

            this.PrimaryKey = new DataColumn[] { this.Columns[FLD_PKID] };
		}
    }
}
