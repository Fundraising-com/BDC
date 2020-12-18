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
	public class AppItemTable : DataTable, System.Collections.IEnumerable
	{		
		//
		//AppItemData constants
		//
		/// <value>The constant used for CM_AppItems table. </value>
		public const String TBL_APP_ITEM = "CM_AppItems";
		/// <value>The constant used for PKId field in the CM_AppItems table. </value>
		public const String FLD_PKID = "AppItem_ID";
		/// <value>The constant used for NoAppMenuItem field in the CM_AppItems table. </value>
		public const String FLD_NO = "NoAppItem";
		/// <value>The constant used for Section Id field in the CM_AppItems table. </value>
		public const String FLD_SECTION_ID = "Section_ID";
		/// <value>The constant used for Name field in the CM_AppItems table. </value>
		public const String FLD_NAME  = "Name";
		/// <value>The constant used for Description field in the CM_AppItems table. </value>
		public const String FLD_DESCRIPTION = "Description";
		/// <value>The constant used for URL field in the CM_AppItems table. </value>		
		public const String FLD_PAGE_URL = "PageUrl";
		/// <value>The constant used for URL field in the CM_AppItems table. </value>		
		public const String FLD_CONTROL_URL = "ControlUrl";
		/// <value>The constant used for Instruction field in the CM_AppItems table. </value>
		public const String FLD_INSTRUCTION = "Instruction";
		/// <value>The constant used for Image field in the CM_AppItems table. </value>
		public const String FLD_IMAGE_URL = "ImageUrl";
		/// <value>The constant used for Report Name field in the CM_AppItems table. </value>
		public const String FLD_REPORT_NAME = "ReportFileName";
		/// <value>The constant used for the SP of the Report field in the CM_AppItems table. </value>
		public const String FLD_REPORT_SP = "Report_SP";		
		/// <value>The constant used for Display Menu field in the CM_AppItems table. </value>
		public const String FLD_DISPLAY = "DisplayMenu";
		/// <value>The constant used for NoStop field in the CM_AppItems table. </value>
		public const String FLD_NO_STEP = "NoStep";
		/// <value>The constant used for Parent ID field (use for recursive relation) in the CM_AppItems table. </value>
		public const String FLD_PARENT_ID = "AppItem_ParentID";		
		/// <value>The constant used for IsMenu field in the CM_AppItems table. </value>
		public const String FLD_IS_MENU = "IsMenu";
		/// <value>The constant used for DisplayOrder field in the CM_AppItems table. </value>
		public const String FLD_ORDER = "DisplayOrder";
		/// <value>The constant used for IsAdvMenu field in the CM_AppItems table. </value>
		public const String FLD_IS_ADVANCED_MENU = "IsAdvMenu";
		/// <value>The constant used for IsAdvMenu field in the CM_AppItems table. </value>
		public const String FLD_OPEN_IN_NEW_PAGE = "OpenInNewPage";
		/// <value>The constant used for IsAdvMenu field in the CM_AppItems table. </value>
		public const String FLD_PAGE_TITLE = "PageTitle";
		/// <value>The constant used for IsAdvMenu field in the CM_AppItems table. </value>
		public const String FLD_SECTION_TITLE = "SectionTitle";
		/// <value>The constant used for IsAdvMenu field in the CM_AppItems table. </value>
		public const String FLD_ENTITY_TYPE_ID = "EntityTypeID";
		

		public AppItemTable() : base(TBL_APP_ITEM) 
		{
			this.InitClass();
		}
				
		public AppItemTable(DataTable table) : base(table.TableName) 
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
			AppItemTable cln = ((AppItemTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
		    
		protected override DataTable CreateInstance() 
		{
			return new AppItemTable();
		}
		    
		internal void InitVars() 
		{
			        
		}
		    
		private void InitClass() 
		{
			//
			// Create the table
			//
			this.TableName =  TBL_APP_ITEM;
			DataColumnCollection columns = this.Columns;
		    
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_NO, typeof(System.Int32));
			columns.Add(FLD_SECTION_ID, typeof(System.Int32));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_PAGE_URL, typeof(System.String));
			columns.Add(FLD_INSTRUCTION, typeof(System.String));			
			columns.Add(FLD_IMAGE_URL, typeof(System.String));
			columns.Add(FLD_REPORT_NAME, typeof(System.String));
			columns.Add(FLD_REPORT_SP, typeof(System.String));
			columns.Add(FLD_DISPLAY, typeof(System.Boolean));
			columns.Add(FLD_NO_STEP, typeof(System.Int32));
			columns.Add(FLD_PARENT_ID, typeof(System.Int32));
			columns.Add(FLD_IS_MENU, typeof(System.Boolean));
			columns.Add(FLD_ORDER, typeof(System.Int32));
			columns.Add(FLD_IS_ADVANCED_MENU, typeof(System.Boolean));
			columns.Add(FLD_CONTROL_URL, typeof(System.String));
			columns.Add(FLD_OPEN_IN_NEW_PAGE, typeof(System.Boolean));
			columns.Add(FLD_PAGE_TITLE, typeof(System.String));
			columns.Add(FLD_SECTION_TITLE, typeof(System.String));
			columns.Add(FLD_ENTITY_TYPE_ID, typeof(System.Int32));	//For Security Context
		        		
		}

		
	}
}
