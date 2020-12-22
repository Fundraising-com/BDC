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
	public class PromoSubdivisionTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_PROMO_SUBDIVISION = "QSPForm_promo_subdivision";
		public const string FLD_PKID = "promo_subdivision_id";
		public const string FLD_PROMO_ID = "promo_id";
        public const string FLD_SUBDIVISION_CODE = "subdivision_code";
		public const string FLD_DELETED = "deleted";
		public const string FLD_NAME = "name";
		public const string FLD_SUBDIVISION_NAME_1 = "subdivision_name_1";
		public const string FLD_DESCRIPTION = "description";
		
		public const string FLD_CREATE_DATE = "create_date";
		public const string FLD_CREATE_USER_ID = "create_user_id";
		public const string FLD_UPDATE_DATE = "update_date";
		public const string FLD_UPDATE_USER_ID = "update_user_id";
        
		public PromoSubdivisionTable() : 
			base(TBL_PROMO_SUBDIVISION) 
		{
			this.InitClass();
		}
		    
		public PromoSubdivisionTable(DataTable table) : 
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
		public PromoSubdivisionTable(SerializationInfo info, StreamingContext context) : base(info, context) 
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
			this.TableName = TBL_PROMO_SUBDIVISION;
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
			
			
			columns.Add(FLD_PROMO_ID, typeof(System.Int32));
			columns.Add(FLD_SUBDIVISION_CODE, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String));
			columns.Add(FLD_DELETED, typeof(System.Boolean));
			
			
			columns.Add(FLD_CREATE_DATE, typeof(System.String));
			columns.Add(FLD_CREATE_USER_ID,typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.String));
			columns.Add(FLD_UPDATE_USER_ID,typeof(System.Int32));
			
		}

		
	}
}
