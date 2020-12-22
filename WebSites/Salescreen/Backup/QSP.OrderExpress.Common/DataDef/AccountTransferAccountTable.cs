using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
    [System.ComponentModel.DesignerCategory("Code")]
    [SerializableAttribute]
    public class AccountTransferAccountTable : DataTable, System.Collections.IEnumerable
    {
        //
        //User constants
        // 
        /// <value>The constant used for Account Transfer Organization table. </value>
        public const String TBL_ORGANIZATION = "AccountTransferAccount";
        /// <value>The constant used for PKId field in the Organization table. </value>
        public const String FLD_PKID = "account_id";
        /// <value>The constant used for Organization Name(FlagPole Name) field in the Organization table. </value>
        public const String FLD_NAME = "organization_name";
        /// <value>The constant used for fulfilment ID field in the Organization table. </value>
        public const String FLD_FULF_ACCOUNT_ID = "fulf_account_id";
        /// <value>The constant used for account name field . </value>
        public const String ACCOUNT_NAME = "account_name";
        /// <value>The constant used for city field . </value>
        public const String CITY = "city";
        /// <value>The constant used for state field. </value>
        public const String STATE = "State";
        /// <value>The constant used for zip field. </value>
        public const String ZIP = "zip";
        /// <value>The constant used for FM ID field in the Account table. </value>
        public const String FLD_FM_ID = "fm_id";
        /// <value>The constant used for FM ID field in the Account table. </value>
        public const String FLD_FM_NAME = "fmname";


        public AccountTransferAccountTable()
            :
            base(TBL_ORGANIZATION)
        {
            this.InitClass();
        }

        public AccountTransferAccountTable(DataTable table)
            : base(table.TableName)
        {
            if ((table.CaseSensitive != table.DataSet.CaseSensitive))
            {
                this.CaseSensitive = table.CaseSensitive;
            }
            if ((table.Locale.ToString() != table.DataSet.Locale.ToString()))
            {
                this.Locale = table.Locale;
            }
            if ((table.Namespace != table.DataSet.Namespace))
            {
                this.Namespace = table.Namespace;
            }
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
            this.DisplayExpression = table.DisplayExpression;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.Rows.GetEnumerator();
        }

        public override DataTable Clone()
        {
            AccountTransferAccountTable cln = ((AccountTransferAccountTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }

        protected override DataTable CreateInstance()
        {
            return new AccountTransferAccountTable();
        }

        internal void InitVars()
        {

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
            columns.Add(FLD_FULF_ACCOUNT_ID, typeof(System.String));
            columns.Add(ACCOUNT_NAME, typeof(System.String));
            columns.Add(CITY, typeof(System.String));
            columns.Add(STATE, typeof(System.String));
            columns.Add(ZIP, typeof(System.String));
            columns.Add(FLD_FM_ID, typeof(System.String));
            columns.Add(FLD_FM_NAME, typeof(System.String));
        }

    }
}
