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
	///         The serializale constructor allows objects of type ProgramTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class ProgramTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_PROGRAM = "program";
		/// <value>The constant used for PKId program in the Order table. </value>
		public const String FLD_PKID = "program_id";
		/// <value>The constant used for the order "Form Name" program in the Order table. </value>
		public const String FLD_PROGRAM_NAME = "program_name";
		/// <value>The constant used for "Form Code" program in the Order table. </value>
		public const String FLD_PROGRAM_TYPE_ID = "program_type_id";
		/// <value>The constant used for the order "Form Name" program in the Order table. </value>
		public const String FLD_PROGRAM_TYPE_NAME = "program_type_name";
        /// <value>The constant used for the order "Form Name" program in the Order table. </value>
        public const String FLD_DESCRIPTION = "description";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" program in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" program in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" program in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" program in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public ProgramTable() : 
                base(TBL_PROGRAM) {
            this.InitClass();
        }
		    
        public ProgramTable(DataTable table) : 
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
        
		protected ProgramTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            ProgramTable cln = ((ProgramTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new ProgramTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_PROGRAM;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_PROGRAM_NAME, typeof(System.String));
			columns.Add(FLD_PROGRAM_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_PROGRAM_TYPE_NAME, typeof(System.String));
            columns.Add(FLD_DESCRIPTION, typeof(System.String));
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
