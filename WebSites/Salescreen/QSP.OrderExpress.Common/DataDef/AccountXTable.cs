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
	///         The serializale constructor allows objects of type AccountXTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class AccountXTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Account table. </value>
		public const String TBL_ACCOUNTX = "QSPForm_X_account";
		/// <value>The constant used for PKId field in the Account table. </value>
		public const String FLD_PKID = "account_id";
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_TYPE_ID = "organization_type_id";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_TYPE_NAME = "organization_type_name";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_LEVEL_ID = "organization_level_id";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_LEVEL_NAME = "organization_level_name";		
		
		
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public AccountXTable() : 
                base(TBL_ACCOUNTX) {
            this.InitClass();
        }
		    
        public AccountXTable(DataTable table) : 
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
            AccountXTable cln = ((AccountXTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new AccountXTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_ACCOUNTX;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_ORG_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORG_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_ORG_LEVEL_ID, typeof(System.Int32));
			columns.Add(FLD_ORG_LEVEL_NAME, typeof(System.String));

			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
        			
		}
	}
}
