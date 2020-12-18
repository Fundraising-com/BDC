using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of OrganizationTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type OrganizationTable to be remoted.
	///     </remarks>
	/// </summary>
	/// 
	    
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class OrganizationTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for Organization table. </value>
		public const String TBL_ORGANIZATION = "organization";
		/// <value>The constant used for PKId field in the Organization table. </value>
		public const String FLD_PKID = "organization_id";
		/// <value>The constant used for Organization Name field in the Organization table. </value>
		public const String FLD_NAME = "organization_name";
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_TYPE_ID = "organization_type_id";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_TYPE_NAME = "organization_type_name";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_LEVEL_ID = "organization_level_id";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_LEVEL_NAME = "organization_level_name";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_ORG_STATUS_ID = "organization_status_id";		
		/// <value>The constant used for FM ID field in the Organization table. </value>
		public const String FLD_FM_ID = "fm_id";
		/// <value>The constant used for FM ID field in the Organization table. </value>
		public const String FLD_FM_NAME = "fm_name";
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_BIZ_DIVISION_ID = "business_division_id";		
		/// <value>The constant used for Organization Type ID field in the Organization table. </value>
		public const String FLD_BIZ_DIVISION_NAME = "business_division_name";		
		/// <value>The constant used for Tax Exemption Number field in the Organization table. </value>
		public const String FLD_TAX_EXEMPTION_NO = "tax_exemption_number";
		/// <value>The constant used for Tax Exemption Expiration Date field in the Organization table. </value>
		public const String FLD_TAX_EXEMPTION_EXP_DATE = "tax_exemption_expiration_date";
		/// <value>The constant used for MDR PID field in the Organization table. </value>
		public const String FLD_MDRPID = "MDRPID";
		/// <value>The constant used for comments field in the Organization table. </value>
		public const String FLD_COMMENTS = "comments";

		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public OrganizationTable() : 
                base(TBL_ORGANIZATION) {
            this.InitClass();
        }
		    
        public OrganizationTable(DataTable table) : 
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
            OrganizationTable cln = ((OrganizationTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new OrganizationTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {
			//
			// Create the Groups table
			//
			this.TableName =  TBL_ORGANIZATION;	
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID >= 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			//Column.DefaultValue = 0;
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_ORG_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_ORG_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_ORG_LEVEL_ID, typeof(System.Int32));
			columns.Add(FLD_ORG_LEVEL_NAME, typeof(System.String));
			columns.Add(FLD_ORG_STATUS_ID, typeof(System.Int32));			
			columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_FM_NAME, typeof(System.String));
			columns.Add(FLD_BIZ_DIVISION_ID, typeof(System.Int32)).DefaultValue = 1; //For US
			columns.Add(FLD_BIZ_DIVISION_NAME, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_EXP_DATE, typeof(System.DateTime));
			columns.Add(FLD_MDRPID, typeof(System.String));
			columns.Add(FLD_COMMENTS, typeof(System.String));

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
        }
        
        
	}
		
}

