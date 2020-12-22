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
	public class LogoTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_LOGO = "QSPForm_logo";
		public const string FLD_PKID = "logo_id";
		public const string FLD_LOGO_NAME = "logo_name";
		public const string FLD_FSM_ID = "field_sales_manager_id";
		public const string FLD_FILE_EXTENSION = "file_extension";
		public const string FLD_DESCRIPTION = "description";
        public const string FLD_DELETED = "deleted";
		public const string FLD_ENABLED = "enabled";
		public const string FLD_NATIONAL = "IsNational";
		public const string FLD_FM_ID = "FM_ID";
		public const string FLD_START_DATE= "labeling_start_date";
		public const string FLD_END_DATE= "labeling_end_date";
		public const string FLD_SUBDIVISION_ID = "subdivision_id";
		public const string FLD_CREATE_DATE = "create_date";
		public const string FLD_CREATE_USER_ID = "create_user_id";
		public const string FLD_UPDATE_DATE = "update_date";
		public const string FLD_UPDATE_USER_ID = "update_user_id";
		public const string FLD_CATEGORY = "image_category_id";
        
		public LogoTable() : 
			base(TBL_LOGO) 
		{
			this.InitClass();
		}
		    
		public LogoTable(DataTable table) : 
			base(table.TableName) 
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
        
		/// <summary>
		///     Constructor to support serialization.
		///     <remarks>Constructor that supports serialization.</remarks> 
		///     <param name="info">The SerializationInfo object to read from.</param>
		///     <param name="context">Information on who is calling this method.</param>
		/// </summary>
		public LogoTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
        
		public override DataTable Clone() 
		{
			LogoTable cln = ((LogoTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
        
		protected override DataTable CreateInstance() 
		{
			return new LogoTable();
		}
        
		internal void InitVars() 
		{
            
		}
        
		private void InitClass() 
		{			
			//
			// Create the Users table
			//
			this.TableName = TBL_LOGO;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;            

			//columns.Add(FLD_PKID,typeof(System.Int32));
			columns.Add(FLD_LOGO_NAME,typeof(System.String));
			columns.Add(FLD_FSM_ID,typeof(System.Int32));
			columns.Add(FLD_FILE_EXTENSION,typeof(System.String));
			columns.Add(FLD_DESCRIPTION,typeof(System.String));
			columns.Add(FLD_DELETED, typeof(System.Boolean));
			columns.Add(FLD_ENABLED, typeof(System.Boolean));
			columns.Add(FLD_NATIONAL, typeof(System.Boolean));
			columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_CATEGORY, typeof(System.Int32));
			
			
			columns.Add(FLD_CREATE_DATE, typeof(System.String));
			columns.Add(FLD_CREATE_USER_ID,typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.String));
			columns.Add(FLD_UPDATE_USER_ID,typeof(System.Int32));
			
		}

		
	}
}
