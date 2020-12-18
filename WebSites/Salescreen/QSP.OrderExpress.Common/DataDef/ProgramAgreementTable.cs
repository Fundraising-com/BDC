using System;
using System.Data;
using System.Runtime.Serialization;

namespace QSPForm.Common.DataDef
{
	/// <summary>
	///     A custom serializable dataset containing user information.
	///     <remarks>
	///         This class is used to define the shape of ProgramAgreementTable.
	///     </remarks>
	///     <remarks>
	///         The serializale constructor allows objects of type ProgramAgreementTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class ProgramAgreementTable : DataTable, System.Collections.IEnumerable
	{
		//
		//User constants
		// 
		/// <value>The constant used for ProgramAgreement table. </value>
		public const String TBL_PROGRAM_AGREEMENT = "program_agreement";
		/// <value>The constant used for PKId field in the ProgramAgreement table. </value>
		public const String FLD_PKID = "program_agreement_id";
		/// <value>The constant used for Organization ID field in the ProgramAgreement table. </value>
		public const String FLD_FORM_ID = "form_id";
		/// <value>The constant used for ProgramAgreement Type ID field in the ProgramAgreement table. </value>
		public const String FLD_PROGRAM_AGREEMENT_STATUS_ID = "program_agreement_status_id";		
		/// <value>The constant used for ProgramAgreement Type ID field in the ProgramAgreement table. </value>
		public const String FLD_PROGRAM_AGREEMENT_STATUS_NAME = "program_agreement_status_name";		
		/// <value>The constant used for ProgramAgreement Type ID field in the ProgramAgreement table. </value>
		public const String FLD_PROGRAM_AGREEMENT_STATUS_SHORT_DESCRIPTION = "program_agreement_status_short_description";
        /// <value>The constant used for ProgramAgreement Type ID field in the ProgramAgreement table. </value>
        public const String FLD_PROGRAM_AGREEMENT_STATUS_DESCRIPTION = "program_agreement_status_description";
        /// <value>The constant used for ProgramAgreement Type ID field in the ProgramAgreement table. </value>
		public const String FLD_PROGRAM_AGREEMENT_STATUS_COLOR_CODE = "color_code";		
		/// <value>The constant used for Organization ID field in the ProgramAgreement table. </value>
		public const String FLD_NAME = "program_agreement_name";
		/// <value>The constant used for Organization ID field in the ProgramAgreement table. </value>
		public const String FLD_FULF_PROGRAM_AGREEMENT_ID = "fulf_program_agreement_id";
        /// <value>The constant used for fiscal year field in the ProgramAgreement table. </value>
        public const String FLD_FISCAL_YEAR = "fiscal_year";
		/// <value>The constant used for FM ID field in the ProgramAgreement table. </value>
		public const String FLD_FM_ID = "fm_id";
		/// <value>The constant used for FM ID field in the ProgramAgreement table. </value>
		public const String FLD_FM_NAME = "fm_name";
		/// <value>The constant used for Tax Exemption Number field in the ProgramAgreement table. </value>
		public const String FLD_TAX_EXEMPTION_NO = "tax_exemption_number";
		/// <value>The constant used for Tax Exemption Expiration Date field in the ProgramAgreement table. </value>
		public const String FLD_TAX_EXEMPTION_EXP_DATE = "tax_exemption_expiration_date";
        /// <value>The constant used for Campaign Name field in the Campaigns table. </value>
        public const String FLD_START_DATE = "start_date";
        /// <value>The constant used for End Date field in the Campaigns table. </value>
        public const String FLD_END_DATE = "end_date";
        /// <value>The constant used for Campaign Name field in the Campaigns table. </value>
        public const String FLD_HOLIDAY_START_DATE = "holiday_start_date";
        /// <value>The constant used for End Date field in the Campaigns table. </value>
        public const String FLD_HOLIDAY_END_DATE = "holiday_end_date";
        /// <value>The constant used for Enrollment field in the Campaigns table. </value>
        public const String FLD_ENROLLMENT = "enrollment";
        /// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
        public const String FLD_GOAL_ESTIMATED_GROSS = "goal_estimated_gross";
        /// <value>The constant used for Enrollment field in the Campaigns table. </value>
        public const String FLD_RENEWAL_SIGN_UP_TERM = "renewal_sign_up_term";
        /// <value>The constant used for GOAL Estimated Gross field in the Campaigns table. </value>
        public const String FLD_PROFIT_RATE = "profit_rate";
        /// <value>The constant used for comments field in the ProgramAgreement table. </value>
		public const String FLD_COMMENTS = "comments";
        public const String FLD_PRICED = "priced";
		
        /// <value>The constant used for comments field in the ProgramAgreement table. </value>
		public const String FLD_IS_VALIDATION_PERFORMED = "is_validation_performed";
		/// <value>The constant used for comments field in the ProgramAgreement table. </value>
		public const String FLD_INFO_STATUS = "info_status";
		


		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_LAST_NAME = "create_last_name";
        /// <value>The constant used for "create user id" field in the Collection Days table. </value>
        public const String FLD_CREATE_FIRST_NAME = "create_first_name";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public ProgramAgreementTable() : 
                base(TBL_PROGRAM_AGREEMENT) {
            this.InitClass();
        }
		    
        public ProgramAgreementTable(DataTable table) : 
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
        
        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            ProgramAgreementTable cln = ((ProgramAgreementTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new ProgramAgreementTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() 
		{
			//
			// Create the Groups table
			//
			this.TableName =  TBL_PROGRAM_AGREEMENT;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;

            columns.Add(FLD_FULF_PROGRAM_AGREEMENT_ID, typeof(System.String));
            columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_PROGRAM_AGREEMENT_STATUS_ID, typeof(System.Int32));	
			columns.Add(FLD_PROGRAM_AGREEMENT_STATUS_NAME, typeof(System.String));
			columns.Add(FLD_PROGRAM_AGREEMENT_STATUS_SHORT_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_PROGRAM_AGREEMENT_STATUS_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_PROGRAM_AGREEMENT_STATUS_COLOR_CODE, typeof(System.String));
            columns.Add(FLD_FM_ID, typeof(System.String));
			columns.Add(FLD_FM_NAME, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_NO, typeof(System.String));
			columns.Add(FLD_TAX_EXEMPTION_EXP_DATE, typeof(System.DateTime));
            columns.Add(FLD_START_DATE, typeof(System.DateTime));
            columns.Add(FLD_END_DATE, typeof(System.DateTime));
            columns.Add(FLD_HOLIDAY_START_DATE, typeof(System.DateTime));
            columns.Add(FLD_HOLIDAY_END_DATE, typeof(System.DateTime)); columns.Add(FLD_ENROLLMENT, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_GOAL_ESTIMATED_GROSS, typeof(System.Decimal)).DefaultValue = 0;
            columns.Add(FLD_RENEWAL_SIGN_UP_TERM, typeof(System.Int32)).DefaultValue = 0;
            columns.Add(FLD_PROFIT_RATE, typeof(System.Single)).DefaultValue = 0;
            columns.Add(FLD_COMMENTS, typeof(System.String));
            columns.Add(FLD_PRICED, typeof(System.Boolean));
            columns.Add(FLD_IS_VALIDATION_PERFORMED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_INFO_STATUS, typeof(System.Int32)).DefaultValue = InfoStatus.NONE;
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	

			
        			
		}
	}
}
