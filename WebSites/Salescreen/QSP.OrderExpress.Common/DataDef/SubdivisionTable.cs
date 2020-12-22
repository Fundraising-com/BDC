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
	public class SubdivisionTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_SUBDIVISION = "Subdivision";
		public const string FLD_PKID = "subdivision_code";
		public const string FLD_COUNTRY_CODE = "country_code";
		public const string FLD_SUBDIVISION_NAME_1= "subdivision_name_1";
		public const string FLD_SUBDIVISION_NAME_2= "subdivision_name_2";
		public const string FLD_SUBDIVISION_NAME_3= "subdivision_name_3";
        public const string FLD_REGIONAL_DIVISION = "regional_division";
		public const string FLD_SUBDIVISION_CATEGORY = "subdivision_category";
		
		public SubdivisionTable() : 
			base(TBL_SUBDIVISION) 
		{
			this.InitClass();
		}
		    
		public SubdivisionTable(DataTable table) : 
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
		public SubdivisionTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
        
		public override DataTable Clone() 
		{
			PromoTable cln = ((PromoTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
        
		protected override DataTable CreateInstance() 
		{
			return new PromoTable();
		}
        
		internal void InitVars() 
		{
            
		}
        
		private void InitClass() 
		{			
			//
			// Create the Users table
			//
			this.TableName = TBL_SUBDIVISION;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.String));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;            

			columns.Add(FLD_COUNTRY_CODE,typeof(System.String));
			columns.Add(FLD_SUBDIVISION_NAME_1,typeof(System.String));
			columns.Add(FLD_SUBDIVISION_NAME_1,typeof(System.String));
			columns.Add(FLD_SUBDIVISION_NAME_1,typeof(System.String));
			columns.Add(FLD_REGIONAL_DIVISION, typeof(System.String));
			columns.Add(FLD_SUBDIVISION_CATEGORY, typeof(System.String));			
		}

		
	}
}
