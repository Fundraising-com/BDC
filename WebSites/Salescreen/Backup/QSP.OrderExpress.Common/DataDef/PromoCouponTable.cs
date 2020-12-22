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
	public class PromoCouponTable : DataTable, System.Collections.IEnumerable
	{		
		
		public const string TBL_PROMO_COUPON = "QSPForm_promo_coupon";
        public const string FLD_PKID = "promotion_id";
        public const string FLD_DESCRIPTION = "description";
        public const string FLD_PROMO_TEXT_ID = "promo_text_id";
        public const string FLD_PROMO_TEXT_NAME = "promo_text_name";
        public const string FLD_PROMO_TEXT_DESCRIPTION = "promo_text_description";
        public const string FLD_PROMO_LOGO_ID = "promo_logo_id";
        public const string FLD_PROMO_LOGO_NAME = "promo_logo_name";
        public const string FLD_NATIONAL = "IsNational";
        public const string FLD_VENDOR_ID = "vendor_id";
        public const string FLD_VENDOR_NAME = "vendor_name";
        public const string FLD_LABELING_START_DATE = "labeling_start_date";
        public const string FLD_LABELING_END_DATE = "labeling_end_date";
        public const string FLD_FIELD_SALES_MANAGER_ID = "field_sales_manager_id";
        public const string FLD_FM_ID = "fm_id";
        public const string FLD_FM_NAME = "fm_name";
        public const string FLD_EXPIRATION_DATE = "expiration_date";
        public const string FLD_DELETED = "deleted";
        public const string FLD_CREATE_DATE = "create_date";
        public const string FLD_CREATE_USER_ID = "create_user_id";
        public const string FLD_UPDATE_DATE = "update_date";
        public const string FLD_UPDATE_USER_ID = "update_user_id";
        public const string FLD_PROMO_LANDSCAPE_ID = "promo_landscape_id";
        public const string FLD_PROMO_PORTRAIT_ID = "promo_portrait_id";
        
		public PromoCouponTable() : 
			base(TBL_PROMO_COUPON) 
		{
			this.InitClass();
		}
		    
		public PromoCouponTable(DataTable table) : 
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
		public PromoCouponTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}		

		public System.Collections.IEnumerator GetEnumerator() 
		{
			return this.Rows.GetEnumerator();
		}
        
		public override DataTable Clone() 
		{
			PromoCouponTable cln = ((PromoCouponTable)(base.Clone()));
			cln.InitVars();
			return cln;
		}
        
		protected override DataTable CreateInstance() 
		{
			return new PromoCouponTable();
		}
        
		internal void InitVars() 
		{
            
		}
        
		private void InitClass() 
		{			
			//
			// Create the Users table
			//
			this.TableName = TBL_PROMO_COUPON;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;         
   
            columns.Add(FLD_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_PROMO_TEXT_ID,typeof(System.Int32));
            columns.Add(FLD_PROMO_LOGO_ID,typeof(System.Int32));

            columns.Add(FLD_PROMO_TEXT_NAME, typeof(System.String));
            columns.Add(FLD_PROMO_TEXT_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_PROMO_LOGO_NAME, typeof(System.String));

            //promotion image
            columns.Add(FLD_PROMO_PORTRAIT_ID, typeof(System.Int32));
            columns.Add(FLD_PROMO_LANDSCAPE_ID, typeof(System.Int32));

            columns.Add(FLD_VENDOR_ID,typeof(System.Int32));
            columns.Add(FLD_VENDOR_NAME, typeof(System.String));
            columns.Add(FLD_LABELING_START_DATE, typeof(System.String));
            columns.Add(FLD_LABELING_END_DATE, typeof(System.String));
            columns.Add(FLD_FIELD_SALES_MANAGER_ID,typeof(System.Int32));
            columns.Add(FLD_FM_ID,typeof(System.Int32));
            columns.Add(FLD_FM_NAME, typeof(System.String));
            columns.Add(FLD_EXPIRATION_DATE , typeof(System.String));
            columns.Add(FLD_NATIONAL, typeof(System.Boolean));
			
			//System field
			columns.Add(FLD_DELETED, typeof(System.Boolean));			
			columns.Add(FLD_CREATE_DATE, typeof(System.String));
			columns.Add(FLD_CREATE_USER_ID,typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.String));
			columns.Add(FLD_UPDATE_USER_ID,typeof(System.Int32));
			
		}

		
	}
}
