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
	///         The serializale constructor allows objects of type TemplateEmailTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class TemplateEmailTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_TEMPLATE_EMAIL = "template_email";
		/// <value>The constant used for PKId template_email in the Order table. </value>
		public const String FLD_PKID = "template_email_id";
		/// <value>The constant used for the order "Form Name" template_email in the Order table. </value>
		public const String FLD_TEMPLATE_EMAIL_NAME = "template_email_name";
		/// <value>The constant used for the order "Description" template_email in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
		/// <value>The constant used for "Form Code" template_email in the Order table. </value>
		public const String FLD_FROM = "from_name";
		/// <value>The constant used for the order "Form Name" template_email in the Order table. </value>
		public const String FLD_SUBJECT = "subject";
		/// <value>The constant used for "Form Code" template_email in the Order table. </value>
		public const String FLD_BODY_TEXT = "body_text";
		/// <value>The constant used for the order "Form Name" template_email in the Order table. </value>
		public const String FLD_BODY_HTML = "body_html";
		/// <value>The constant used for the order "Description" template_email in the Order table. </value>
		public const String FLD_TEMPLATE_EMAIL_SP = "template_email_sp";
		/// <value>The constant used for the order "Description" template_email in the Order table. </value>
		public const String FLD_PARAMETER_NAME = "parameter_name";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" template_email in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" template_email in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" template_email in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" template_email in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public TemplateEmailTable() : 
                base(TBL_TEMPLATE_EMAIL) {
            this.InitClass();
        }
		    
        public TemplateEmailTable(DataTable table) : 
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
        
		protected TemplateEmailTable(SerializationInfo info, StreamingContext context) : base(info, context) 
		{		
		}

        public System.Collections.IEnumerator GetEnumerator() {
            return this.Rows.GetEnumerator();
        }
        
        public override DataTable Clone() {
            TemplateEmailTable cln = ((TemplateEmailTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new TemplateEmailTable();
        }
        
        internal void InitVars() {
            
        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_TEMPLATE_EMAIL;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_TEMPLATE_EMAIL_NAME, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
			columns.Add(FLD_FROM, typeof(System.String));
			columns.Add(FLD_SUBJECT, typeof(System.String));
			columns.Add(FLD_BODY_TEXT, typeof(System.String));
			columns.Add(FLD_BODY_HTML, typeof(System.String));
			columns.Add(FLD_TEMPLATE_EMAIL_SP, typeof(System.String));
			columns.Add(FLD_PARAMETER_NAME, typeof(System.String));
			
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
