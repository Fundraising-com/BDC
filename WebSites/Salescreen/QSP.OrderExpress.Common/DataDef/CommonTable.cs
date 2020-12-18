using System;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	/// Summary description for CommonTable.
	/// </summary>
	/// 
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CommonTable: DataTable, System.Collections.IEnumerable
	{

        public const String FLD_DELETED = "deleted";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_USER_ID = "create_user_id";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_LAST_NAME = "create_last_name";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_FIRST_NAME = "create_first_name";
        /// <value>The constant used for "create date" field in the Collection Days table. </value>
        public const String FLD_CREATE_DATE = "create_date";
        /// <value>The constant used for "update user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_USER_ID = "update_user_id";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_LAST_NAME = "update_last_name";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_UPDATE_FIRST_NAME = "update_first_name";
        /// <value>The constant used for "update date" field in the Collection Days table. </value>
        public const String FLD_UPDATE_DATE = "update_date";


		public CommonTable() : 
                base("Common") {
            this.InitClass();
        }
		    
        public CommonTable(DataTable table) : base(table.TableName) {
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

		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		protected CommonTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}	
        
        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            CommonTable cln = ((CommonTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new CommonTable();
        }
        
        internal void InitVars() {
            
        }
        
		private void InitClass() 
		{
			//
			// Create the Users table
			//
			DataColumnCollection columns = this.Columns;
        
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_CREATE_LAST_NAME, typeof(System.String));
            columns.Add(FLD_CREATE_FIRST_NAME, typeof(System.String));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
            columns.Add(FLD_UPDATE_LAST_NAME, typeof(System.String));
            columns.Add(FLD_UPDATE_FIRST_NAME, typeof(System.String));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));			
			
		}

	}
}
