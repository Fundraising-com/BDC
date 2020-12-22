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
	public class PromoTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_PROMO = "QSPForm_promo";
		public const string FLD_PKID = "promo_id";
		public const string FLD_PROMO_COUPON_ID = "promo_coupon_id";
		public const string FLD_PROMO_NAME = "promo_name";
		public const string FLD_FSM_ID = "field_sales_manager_id";
		public const string FLD_FM_ID = "fm_id";
		public const string FLD_FM_NAME = "fm_name";
		public const string FLD_FILE_EXTENSION = "file_extension";
		public const string FLD_DESCRIPTION = "description";
		public const string FLD_START_DATE= "labeling_start_date";
		public const string FLD_END_DATE= "labeling_end_date";
		public const string FLD_DELETED = "deleted";
		public const string FLD_ENABLED = "enabled";
		public const string FLD_NATIONAL = "IsNational"; //Calculated field
		
		public const string FLD_APPROVED = "approved";
		public const string FLD_APPROVE_USER_ID = "approve_user_id";
		public const string FLD_APPROVE_USER_NAME = "approve_user_name";
		public const string FLD_APPROVE_DATE = "approve_date";
		public const string FLD_APPROVE_CODE = "approve_code";
		public const string FLD_IS_VALIDATION_PERFORMED = "is_validation_performed";
		
		public const string FLD_CREATE_DATE = "create_date";
		public const string FLD_CREATE_USER_ID = "create_user_id";
		public const string FLD_UPDATE_DATE = "update_date";
		public const string FLD_UPDATE_USER_ID = "update_user_id";

		public const string FLD_CATEGORY = "category";
        
		public PromoTable() : 
			base(TBL_PROMO) 
		{
			this.InitClass();
		}
		    
		public PromoTable(DataTable table) : 
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
		public PromoTable(SerializationInfo info, StreamingContext context) : base(info, context) 
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
			this.TableName = TBL_PROMO;
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
			columns.Add(FLD_PROMO_NAME,typeof(System.String));
			columns.Add(FLD_PROMO_COUPON_ID,typeof(System.Int32));
			columns.Add(FLD_FSM_ID,typeof(System.Int32));
			columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_FM_NAME, typeof(System.String));
			columns.Add(FLD_FILE_EXTENSION,typeof(System.String));
			columns.Add(FLD_DESCRIPTION,typeof(System.String));
			columns.Add(FLD_START_DATE, typeof(System.DateTime));
			columns.Add(FLD_END_DATE, typeof(System.DateTime));

			columns.Add(FLD_ENABLED, typeof(System.Boolean));
			columns.Add(FLD_NATIONAL, typeof(System.Boolean));
			
			columns.Add(FLD_APPROVED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_APPROVE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_APPROVE_USER_NAME, typeof(System.String));
			columns.Add(FLD_APPROVE_DATE, typeof(System.DateTime));
			columns.Add(FLD_APPROVE_CODE, typeof(System.String));
			columns.Add(FLD_IS_VALIDATION_PERFORMED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CATEGORY,typeof(System.Int32));
			
			
			columns.Add(FLD_DELETED, typeof(System.Boolean));			
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_CREATE_USER_ID,typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID,typeof(System.Int32));
			
		}

		
	}
}
