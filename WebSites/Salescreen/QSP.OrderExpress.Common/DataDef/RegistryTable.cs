using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of Registry Table.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type RegistryTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class RegistryTable : DataTable
	{		
		//
		//User constants
		//
		/// <value>The constant used for Registry table. </value>
		public const String TBL_REGISTRY = "registry";
		/// <value>The constant used for PKId field in the Registry table. </value>
		public const String FLD_PKID = "registry_id";
		/// <value>The constant used for fm_id field in the Registry table. </value>
		public const String FLD_USER_ID = "user_id";
		/// <value>The constant used for role field in the Registry table. </value>
		public const String FLD_ROLE = "role";
		/// <value>The constant used for user_id field in the Registry table. </value>
		public const String FLD_FMID = "fm_id";		
		/// <value>The constant used for User_Name field in the Registry table. </value>
		public const String FLD_USER_NAME = "user_name";
		/// <value>The constant used for User_Name field in the Registry table. </value>
		public const String FLD_COMPLETE_USER_NAME = "complete_user_name";
		/// <value>The constant used for Password field in the Registry table. </value>
		public const String FLD_PASSWORD = "password";
		/// <value>The constant used for Password field in the Registry table. </value>
		public const String FLD_LOGIN_DATETIME = "login_datetime";
		/// <value>The constant used for Password field in the Registry table. </value>
		public const String FLD_LOGOUT_DATETIME = "logout_datetime";
			
		

		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public RegistryTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		
        
		/// <summary>
		///     Constructor for UserData.  
		///     <remarks>Initialize a UserData instance by building the table schema.</remarks> 
		/// </summary>
		public RegistryTable()
		{
			//
			// Create the tables in the dataset
			//
			BuildDataTable();
		}
                
		//----------------------------------------------------------------
		// Sub BuildDataTable:
		//   Creates the following datatable: Periods
		//----------------------------------------------------------------
		private void BuildDataTable()
		{
			//
			// Create the Periods table
			//
			this.TableName = TBL_REGISTRY;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.DefaultValue = 0;

            columns.Add(FLD_USER_ID, typeof(System.Int32));
			columns.Add(FLD_ROLE, typeof(System.Int32));			
			columns.Add(FLD_FMID, typeof(System.String));
			columns.Add(FLD_USER_NAME, typeof(System.String));
			columns.Add(FLD_COMPLETE_USER_NAME, typeof(System.String));
			columns.Add(FLD_PASSWORD, typeof(System.String));
			columns.Add(FLD_LOGIN_DATETIME, typeof(System.DateTime));
			columns.Add(FLD_LOGOUT_DATETIME, typeof(System.DateTime));			

			
		}
	}
}
