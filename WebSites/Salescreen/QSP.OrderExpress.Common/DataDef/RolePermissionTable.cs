using System;
using System.Data;
using System.Runtime.Serialization;
    

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of AppMenuItemData.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type AppMenuItemData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class RolePermissionTable : DataTable, System.Collections.IEnumerable
	{		
		//
		//AppItemData constants
		//
		/// <value>The constant used for CM_AppItems table. </value>
		public const String TBL_ROLE_PERMISSION = "CM_Roles_Permissions";
		/// <value>The constant used for PKId field in the CM_AppItems table. </value>
		public const String FLD_PKID = "Role_Permission_ID";
		/// <value>The constant used for NoAppMenuItem field in the CM_AppItems table. </value>
		public const String FLD_ROLE_ID = "Role_ID";
		/// <value>The constant used for Section Id field in the CM_AppItems table. </value>
		public const String FLD_ENTITY_TYPE_ID = "EntityTypeID";
		/// <value>The constant used for Description field in the CM_AppItems table. </value>
		public const String FLD_RIGHT_VIEW = "Right_View";
		/// <value>The constant used for URL field in the CM_AppItems table. </value>		
		public const String FLD_RIGHT_INSERT = "Right_Insert";
		/// <value>The constant used for URL field in the CM_AppItems table. </value>		
		public const String FLD_RIGHT_UPDATE = "Right_Update";
		/// <value>The constant used for Instruction field in the CM_AppItems table. </value>
		public const String FLD_RIGHT_DELETE = "Right_Delete";
		
		

		public RolePermissionTable() : base(TBL_ROLE_PERMISSION) 
		{
			this.InitClass();
		}
				
		public RolePermissionTable(DataTable table) : base(table.TableName) 
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
			RolePermissionTable cln = ((RolePermissionTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
		    
		protected override DataTable CreateInstance() 
		{
			return new RolePermissionTable();
		}
		    
		internal void InitVars() 
		{
			        
		}
		    
		private void InitClass() 
		{
			//
			// Create the table
			//
			this.TableName =  TBL_ROLE_PERMISSION;
			DataColumnCollection columns = this.Columns;
		    
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_ROLE_ID, typeof(System.Int32));
			columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_RIGHT_VIEW, typeof(System.Boolean));
			columns.Add(FLD_RIGHT_INSERT, typeof(System.Boolean));
			columns.Add(FLD_RIGHT_UPDATE, typeof(System.Boolean));
			columns.Add(FLD_RIGHT_DELETE, typeof(System.Boolean));
			
		        		
		}

		
	}
}
