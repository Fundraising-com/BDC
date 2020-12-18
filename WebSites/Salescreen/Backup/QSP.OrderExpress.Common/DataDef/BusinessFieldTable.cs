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
	///         The serializale constructor allows objects of type BusinessFieldTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class BusinessFieldTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_BUSINESS_FIELD = "field";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "field_id";
		/// <value>The constant used for "Form Code" field in the Order table. </value>
		public const String FLD_FIELD_TYPE_ID = "field_type_id";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_FIELD_TYPE_NAME = "field_type_name";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_FIELD_NAME = "field_name";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_IS_APPLY_TO_ACCOUNT = "is_apply_to_account";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_IS_APPLY_TO_CREDIT_APPLICATION = "is_apply_to_credit_application";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_IS_APPLY_TO_ORDER = "is_apply_to_order";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_IS_APPLY_TO_COUPON = "is_apply_to_coupon";
        /// <value>The constant used for the order "Description" field in the Order table. </value>
        public const String FLD_IS_APPLY_TO_PROGRAM_AGREEMENT = "is_apply_to_program_agreement";
        /// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_IS_SYSTEM = "is_system";
        /// <value>The constant used for "update date" field in the Collection Days table. </value>
        public const String FLD_IS_FORM_PROPERTY = "is_form_property";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
		
    
        
		public BusinessFieldTable() : 
                base(TBL_BUSINESS_FIELD) {
            this.InitClass();
        }
		    
        public BusinessFieldTable(DataTable table) : 
                base(table.TableName) {
            if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                this.CaseSensitive = table.CaseSensitive;
            }
            if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                this.Locale = table.Locale;
            }
            if ((table.Namespace != table.DataSet.Namespace)) {
                this.Namespace = table.Namespace;
            }
            this.Prefix = table.Prefix;
            this.MinimumCapacity = table.MinimumCapacity;
            this.DisplayExpression = table.DisplayExpression;

			
        }
        
		protected BusinessFieldTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            BusinessFieldTable cln = ((BusinessFieldTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new BusinessFieldTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_BUSINESS_FIELD;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_FIELD_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_FIELD_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_FIELD_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_IS_APPLY_TO_ACCOUNT, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_IS_APPLY_TO_CREDIT_APPLICATION, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_IS_APPLY_TO_ORDER, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_IS_APPLY_TO_COUPON, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_IS_APPLY_TO_PROGRAM_AGREEMENT, typeof(System.Boolean)).DefaultValue = 0; 
            columns.Add(FLD_IS_SYSTEM, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_IS_FORM_PROPERTY, typeof(System.Boolean)).DefaultValue = 0;
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

		protected override void OnRowChanging(DataRowChangeEventArgs e)
		{			 
			string colName = e.Row[FLD_FIELD_NAME].ToString();
			ValidationTable dTblVal = new ValidationTable();
			if (dTblVal.Columns.Contains(colName))
			{
				DataColumn col = dTblVal.Columns[colName];					
				bool isSystem = (dTblVal.IsColumnSystem(col)); 
				bool OldIsSystem = false;
				DataView dv = new DataView(this);
				dv.Sort = FLD_PKID;
				int iIndex = dv.Find(e.Row[FLD_PKID]);				
				if (iIndex > -1)
				{
					if (!dv[iIndex].Row.IsNull(FLD_IS_SYSTEM))
					{
						OldIsSystem = Convert.ToBoolean(dv[iIndex][FLD_IS_SYSTEM]);
					}
					if (OldIsSystem != isSystem)
						dv[iIndex][FLD_IS_SYSTEM] = isSystem;				
				}
			}			
			base.OnRowChanging (e);
			
		}

	}
}
