using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of CatalogGroupTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type CatalogGroupTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class CatalogGroupTable : DataTable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for catalog_group table. </value>
		public const String TBL_CATALOG_GROUP = "catalog_group";
		/// <value>The constant used for PKId field in the CatalogGroup table. </value>
		public const String FLD_PKID = "catalog_group_id";
		/// <value>The constant used for the catalog_group name field in the Order table. </value>
		public const String FLD_CODE = "catalog_group_code";
		/// <value>The constant used for the catalog_group name field in the Order table. </value>
		public const String FLD_NAME = "catalog_group_name";
		/// <value>The constant used for catalog_group Unit field in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";

		

		public CatalogGroupTable() : base(TBL_CATALOG_GROUP) 
		{
			this.InitClass();
		}
		    
		public CatalogGroupTable(DataTable table) : base(table.TableName) 
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
			CatalogGroupTable cln = ((CatalogGroupTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
		        
		protected override DataTable CreateInstance() 
		{
			return new CatalogGroupTable();
		}
		        
		internal void InitVars() 
		{

						        
		}
		        
		private void InitClass() 
		{
			//
			// Create the Campaign table
			//
			this.TableName = TBL_CATALOG_GROUP;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_CODE, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));		
			
		}

	}
}
