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
	public class AppItemDetailTable : DataTable, System.Collections.IEnumerable
	{		
		//
		//AppItem_Detail constants
		//
		/// <value>The constant used for CM_AppItems_DetailItems table. </value>
		public const String TBL_DETAIL_ITEMS = "CM_DetailItems";
		/// <value>The constant used for PKId field in the CM_AppItems_DetailItems table. </value>
		public const String FLD_PKID = "DetailItem_ID";
		/// <value>The constant used for Value field in the CM_AppItems_DetailItems table. </value>
		public const String FLD_VALUE  = "Value";
		/// <value>The constant used for Name field in the CM_AppItems_DetailItems table. </value>
		public const String FLD_NAME  = "Name";
		/// <value>The constant used for Description field in the CM_AppItems_DetailItems table. </value>
		public const String FLD_DESCRIPTION = "Description";
		/// <value>The constant used for Detail Type field in the CM_AppItems_DetailItems table. </value>		
		public const String FLD_DETAIL_TYPE_ID = "DetailType_ID";
		/// <value>The constant used for Display Order field in the CM_AppItems_DetailItems table. </value>		
		public const String FLD_DISPLAY_ORDER = "DisplayOrder";
		/// <value>The constant used for No Detail field in the CM_AppItems_DetailItems table. </value>		
		public const String FLD_NO_DETAIL = "NoDetail";
		/// <value>The constant used for Is Default field in the CM_AppItems_DetailItems table. </value>		
		public const String FLD_IS_DEFAULT = "IsDefault";
		

		public AppItemDetailTable() : base(TBL_DETAIL_ITEMS) 
		{
			this.InitClass();
		}
				
		public AppItemDetailTable(DataTable table) : base(table.TableName) 
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
			AppItemDetailTable cln = ((AppItemDetailTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
		    
		protected override DataTable CreateInstance() 
		{
			return new AppItemDetailTable();
		}
		    
		internal void InitVars() 
		{
			        
		}
		    
		private void InitClass() 
		{
			//
			// Create the table
			//
			this.TableName =  TBL_DETAIL_ITEMS;
			DataColumnCollection columns = this.Columns;
		    
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_VALUE, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_NO_DETAIL, typeof(System.Int32));
			columns.Add(FLD_DISPLAY_ORDER, typeof(System.Int32));
			columns.Add(FLD_IS_DEFAULT, typeof(System.Boolean));
			//The Enumeration Detail Type is based on this field
			columns.Add(FLD_DETAIL_TYPE_ID, typeof(System.Int32));  	
		        		
		}

		
	}
}
