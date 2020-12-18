using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of UserData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type UserData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class UserTable : DataTable, System.Collections.IEnumerable
	{		
		//
		//User constants
		//
		/// <value>The constant used for Users table. </value>
		public const String TBL_USERS = "user";
		/// <value>The constant used for PKId field in the Users table. </value>
		public const String FLD_PKID = "user_id";
		/// <value>The constant used for User_Name field in the Users table. </value>
		public const String FLD_USER_NAME = "user_name";
		/// <value>The constant used for Password field in the Users table. </value>
		public const String FLD_PASSWORD  = "password";
		/// <value>The constant used for title field in the Users table. </value>
		public const String FLD_TITLE = "title";
		/// <value>The constant used for Email field in the Users table. </value>		
		public const String FLD_EMAIL = "email";
		/// <value>The constant used for "Best time to call" field in the Users table. </value>
		public const String FLD_BEST_TIME_TO_CALL = "best_time_to_call";
		/// <value>The constant used for "Day phone no" field in the Users table. </value>
		public const String FLD_DAY_PHONE_NO  = "day_phone_no";
		/// <value>The constant used for "Evening phone no" field in the Users table. </value>
		public const String FLD_EVENING_PHONE_NO  = "evening_phone_no";
		/// <value>The constant used for "Fax phone no" field in the Users table. </value>
		public const String FLD_FAX_NO  = "fax_no";
		public const String FLD_ROLE_ID  = "role_id";
		public const string FLD_ROLE_NAME  = "role_name";
		public const string FLD_LAST_NAME = "last_name";
		public const string FLD_FIRST_NAME = "first_name";
		public const string FLD_FM_ID = "fm_id";
		

		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public UserTable() : 
                base(TBL_USERS) {
            this.InitClass();
        }
		    
        public UserTable(DataTable table) : 
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
        
		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public UserTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            UserTable cln = ((UserTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new UserTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Users table
			//
			this.TableName = TBL_USERS;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;            
            
			columns.Add(FLD_USER_NAME,typeof(System.String));
			columns.Add(FLD_PASSWORD, typeof(System.String));
			columns.Add(FLD_TITLE, typeof(System.String));
			columns.Add(FLD_EMAIL, typeof(System.String));
			columns.Add(FLD_BEST_TIME_TO_CALL, typeof(System.String));
			columns.Add(FLD_DAY_PHONE_NO, typeof(System.String));
			columns.Add(FLD_EVENING_PHONE_NO, typeof(System.String));
			columns.Add(FLD_FAX_NO, typeof(System.String));
			columns.Add(FLD_LAST_NAME,typeof(System.String));
			columns.Add(FLD_FIRST_NAME,typeof(System.String));
			columns.Add(FLD_FM_ID,typeof(System.String));
			columns.Add(FLD_ROLE_ID,typeof(System.Int32));
			columns.Add(FLD_ROLE_NAME,typeof(System.String));

			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

		
	}
}
