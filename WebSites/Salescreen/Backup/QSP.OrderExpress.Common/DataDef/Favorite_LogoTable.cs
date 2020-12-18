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
	public class Favorite_LogoTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_LOGO = "Favorite_logo";
		public const string FLD_PKID = "favorite_logo_id";
		//public const string FLD_USER_ID = "user_id";
        public const string FLD_FIELD_SALES_MANAGER_ID = "field_sales_manager_id";
		public const string FLD_LOGO_ID = "logo_id";
        
		public Favorite_LogoTable() : 
			base(TBL_LOGO) 
		{
			this.InitClass();
		}
		    
		public Favorite_LogoTable(DataTable table) : 
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
        public Favorite_LogoTable(SerializationInfo info, StreamingContext context)
            : base(info, context) 
		{		
		}		

		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
        
		public override DataTable Clone() 
		{
            Favorite_LogoTable cln = ((Favorite_LogoTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
        
		protected override DataTable CreateInstance() 
		{
            return new Favorite_LogoTable();
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
            columns.Add(FLD_FIELD_SALES_MANAGER_ID, typeof(System.Int32));
			columns.Add(FLD_LOGO_ID,typeof(System.Int32));			
		}

		
	}
}
