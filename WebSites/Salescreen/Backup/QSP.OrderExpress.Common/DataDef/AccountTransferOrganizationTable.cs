using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
    [System.ComponentModel.DesignerCategory("Code")]
    [SerializableAttribute]
    public class AccountTransferOrganizationTable : DataTable, System.Collections.IEnumerable
    {
        //
        //User constants
        // 
        /// <value>The constant used for Account Transfer Organization table. </value>
        public const String TBL_ORGANIZATION = "AccountTransferorganization";
        /// <value>The constant used for PKId field in the Organization table. </value>
        public const String FLD_PKID = "flagpoleinstance";
        /// <value>The constant used for Organization Name(FlagPole Name) field in the Organization table. </value>
        public const String FLD_NAME = "flagpole_name";
        

        	public AccountTransferOrganizationTable() : 
                base(TBL_ORGANIZATION) {
            this.InitClass();
        }

        public AccountTransferOrganizationTable(DataTable table)
            : base(table.TableName) {
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
            AccountTransferOrganizationTable cln = ((AccountTransferOrganizationTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new AccountTransferOrganizationTable();
        }
        
        internal void InitVars() {
            
        }

        private void InitClass()
        {
            //
            // Create the Groups table
            //
            this.TableName = TBL_ORGANIZATION;
            DataColumnCollection columns = this.Columns;

            DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));

            //Column.AllowDBNull = false;
            columns.Add(FLD_NAME, typeof(System.String));
        }

    }
}
