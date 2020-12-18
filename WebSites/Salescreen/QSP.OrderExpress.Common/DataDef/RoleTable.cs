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
	///         The serializale constructor allows objects of type CampaignData to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class RoleTable : DataTable
	{
		//
		//Order Header constants
		// 
		/// <value>The constant used for catalog table. </value>
		public const String TBL_ROLE = "Role";
		/// <value>The constant used for PKId field in the Catalog table. </value>
		public const String FLD_PKID = "role_id";
		/// <value>The constant used for PKId field in the Catalog table. </value>
		public const String FLD_NAME = "role_name";
		/// <value>The constant used for PKId field in the Catalog table. </value>
		public const String FLD_DESCRIPTION = "description";
		

		

		public RoleTable() : base(TBL_ROLE) 
		{
			this.InitClass();
		}
		    
		public RoleTable(DataTable table) : base(table.TableName) 
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
			RoleTable cln = ((RoleTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
	        
		protected override DataTable CreateInstance() 
		{
			return new RoleTable();
		}
	        
		internal void InitVars() 
		{
		            
		}
	        
		private void InitClass() 
		{			
			//
			// Create the Catalog table
			//
			this.TableName = TBL_ROLE;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID <= 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			
			
		}

	}
}
