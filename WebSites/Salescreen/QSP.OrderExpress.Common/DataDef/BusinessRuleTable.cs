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
	///         The serializale constructor allows objects of type BusinessRuleTable to be remoted.
	///     </remarks>
	/// </summary>
	[System.ComponentModel.DesignerCategory("Code")]
	[SerializableAttribute] 
	public class BusinessRuleTable : DataTable, System.Collections.IEnumerable
	{
		//
		//Business Rules Table constants
		// 
		/// <value>The constant used for Order table. </value>
		public const String TBL_BUSINESS_RULE = "business_rule";
		/// <value>The constant used for PKId field in the Order table. </value>
		public const String FLD_PKID = "business_rule_id";
		/// <value>The constant used for "Form Code" field in the Order table. </value>
		public const String FLD_FORM_ID = "form_id";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_FIELD_ID = "field_id";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_FIELD_NAME = "field_name";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_FIELD_TYPE_ID = "field_type_id";
		/// <value>The constant used for the order "Form Name" field in the Order table. </value>
		public const String FLD_FIELD_TYPE_NAME = "field_type_name";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_LOGICAL_OPERATOR_ID = "logical_operator_id";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_LOGICAL_OPERATOR_NAME = "logical_operator_name";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_NAME = "business_rule_name";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_MESSAGE = "message";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_VALUE_TO_COMPARE = "value_to_compare";
		/// <value>The constant used for the order "Description" field in the Order table. </value>
		public const String FLD_DESCRIPTION = "description";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_FORM_SECTION_NUMBER = "form_section_number";
        // <value>The constant used for PKId field in the Form table. </value>
        public const String FLD_FORM_SECTION_TYPE_ID = "form_section_type_id";
        /// <value>The constant used for "update date" field in the Collection Days table. </value>
        public const String FLD_IS_FORM_PROPERTY = "is_form_property";
        /// <value>The constant used for "update date" field in the Collection Days table. </value>
        public const String FLD_RULE_EXPRESSION = "rule_expression";
        /// <value>The constant used for the order "IsValid" field in the Order table. </value>
		public const String FLD_IS_VALID = "IsValid";
		
		public const String FLD_DELETED = "deleted";
		/// <value>The constant used for "create user id" field in the Collection Days table. </value>
		public const String FLD_CREATE_USER_ID = "create_user_id";
		/// <value>The constant used for "create date" field in the Collection Days table. </value>
		public const String FLD_CREATE_DATE = "create_date";
		/// <value>The constant used for "update user id" field in the Collection Days table. </value>
		public const String FLD_UPDATE_USER_ID = "update_user_id";
		/// <value>The constant used for "update date" field in the Collection Days table. </value>
		public const String FLD_UPDATE_DATE = "update_date";
    
        
		public BusinessRuleTable() : 
                base(TBL_BUSINESS_RULE) {
            this.InitClass();
        }
		    
        public BusinessRuleTable(DataTable table) : 
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
            BusinessRuleTable cln = ((BusinessRuleTable)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override DataTable CreateInstance() {
            return new BusinessRuleTable();
        }
        
        internal void InitVars() {
            
        }

        public DataRow GetFormPropertyRow(string FormPropertyName)
        {
            
            DataRow row;
            int iIndex = -1;
            DataView dv = new DataView(this);
            dv.Sort = FLD_FIELD_NAME;
            iIndex = dv.Find(FormPropertyName);
            if (iIndex > -1)
            {
                row = dv[iIndex].Row;
                return row;
            }
            else
            {
                return null;
            }
                
            
        }

        public String GetFormPropertyValueString(string FormPropertyName)
        {
            string sValue = "";
            DataRow row;
            row = GetFormPropertyRow(FormPropertyName);
            if (row != null)
            {
                if (!row.IsNull(FLD_VALUE_TO_COMPARE))
                    sValue = row[FLD_VALUE_TO_COMPARE].ToString();

            }


            return sValue;

        }

        public String GetFormPropertyValueString(int FormProperty)
        {
            string sValue = "";
            DataRow row;
            row = GetFormPropertyRow(FormProperty);
            if (row != null)
            {
                if (!row.IsNull(FLD_VALUE_TO_COMPARE))
                    sValue = row[FLD_VALUE_TO_COMPARE].ToString();

            }


            return sValue;

        }

        public String GetFormPropertyValueString(int FormProperty, int FormSectionTypeID, int FormSectionNumber)
        {
            string sValue = "";
            DataRow row;
            row = GetFormPropertyRow(FormProperty, FormSectionTypeID, FormSectionNumber);
            if (row != null)
            {
                if (!row.IsNull(FLD_VALUE_TO_COMPARE))
                    sValue = row[FLD_VALUE_TO_COMPARE].ToString();

            }


            return sValue;

        }

        

        public DataRow GetFormPropertyRow(int FormPropertyID)
        {

            DataRow row;
            int iIndex = -1;
            DataView dv = new DataView(this);
            dv.Sort = FLD_FIELD_ID;
            iIndex = dv.Find(FormPropertyID);
            if (iIndex > -1)
            {
                row = dv[iIndex].Row;
                return row;
            }
            else
            {
                return null;
            }


        }
        public DataRow GetFormPropertyRow(int FormPropertyID, int FormSectionTypeID, int FormSectionNumber)
        {

            DataRow row;
            int iIndex = -1;
            DataView dv = new DataView(this);
            dv.Sort = FLD_FIELD_ID;
            string sFilter = "";
            sFilter = "ISNULL(" + FLD_FORM_SECTION_TYPE_ID + ",0) = " + FormSectionTypeID.ToString();
            sFilter = sFilter +  "AND ISNULL(" + FLD_FORM_SECTION_NUMBER + ",0) = " + FormSectionNumber.ToString();
            dv.RowFilter = sFilter;
            iIndex = dv.Find(FormPropertyID);
            if (iIndex > -1)
            {
                row = dv[iIndex].Row;
                return row;
            }
            else
            {
                return null;
            }


        }

        public void SetFormProperty(int FormProperty, String sValue, int UserID)
        {
            SetFormProperty(FormProperty, 0, 0, sValue, UserID);

        }

        public void SetFormProperty(int FormProperty, int FormSectionTypeID, int FormSectionNumber, String sValue, int UserID)
        {
            DataRow row;
            row = GetFormPropertyRow(FormProperty, FormSectionTypeID, FormSectionNumber);
            if (row != null)
            {
                if (sValue.Trim().Length > 0)
                {                    
                    row[FLD_VALUE_TO_COMPARE] = sValue.Trim();
                    row[FLD_UPDATE_USER_ID] = UserID;
                }
                else
                {
                    row.Delete();
                }
            }
            else
            {
                if (sValue.Trim().Length > 0)
                {
                    DataRow newRow = this.NewRow();
                    newRow[FLD_FIELD_ID] = FormProperty;
                    newRow[FLD_FORM_SECTION_TYPE_ID] = FormSectionTypeID;
                    if (FormSectionNumber > 0)
                        newRow[FLD_FORM_SECTION_NUMBER] = FormSectionNumber;
                    else
                        newRow[FLD_FORM_SECTION_NUMBER] = DBNull.Value;
                    newRow[FLD_LOGICAL_OPERATOR_ID] = 1;
                    newRow[FLD_VALUE_TO_COMPARE] = sValue.Trim();
                    newRow[FLD_CREATE_USER_ID] = UserID;
                    this.Rows.Add(newRow);
                }

            }

        }
        
        private void InitClass() {			
			//
			// Create the Order Header table
			//
			this.TableName = TBL_BUSINESS_RULE;
			DataColumnCollection columns = this.Columns;
        
			DataColumn Column = columns.Add(FLD_PKID, typeof(System.Int32));
            
			Column.AllowDBNull = false;
			//For the system, when PKID = 0, that means that is a new record
			//When we will update the row we will be able to know what kind of operation
			//we will have to do....
			Column.AutoIncrement = true;
			Column.AutoIncrementSeed = 0;
			Column.AutoIncrementStep = -1;
            
			columns.Add(FLD_FORM_ID, typeof(System.Int32));
			columns.Add(FLD_FIELD_ID, typeof(System.Int32));
			columns.Add(FLD_FIELD_NAME, typeof(System.String));
			columns.Add(FLD_FIELD_TYPE_ID, typeof(System.Int32));
			columns.Add(FLD_FIELD_TYPE_NAME, typeof(System.String));
			columns.Add(FLD_LOGICAL_OPERATOR_ID, typeof(System.Int32));
			columns.Add(FLD_LOGICAL_OPERATOR_NAME, typeof(System.String));
			columns.Add(FLD_NAME, typeof(System.String)).DefaultValue = "New Business Rules";
			columns.Add(FLD_MESSAGE, typeof(System.String));
			columns.Add(FLD_VALUE_TO_COMPARE, typeof(System.String));
			columns.Add(FLD_DESCRIPTION, typeof(System.String));
            columns.Add(FLD_FORM_SECTION_TYPE_ID, typeof(System.Int32));
            columns.Add(FLD_FORM_SECTION_NUMBER, typeof(System.Int32));
            columns.Add(FLD_IS_FORM_PROPERTY, typeof(System.Boolean)).DefaultValue = 0;
            columns.Add(FLD_RULE_EXPRESSION, typeof(System.String));
            columns.Add(FLD_IS_VALID, typeof(System.Boolean)).DefaultValue = 0;
			
			columns.Add(FLD_DELETED, typeof(System.Boolean)).DefaultValue = 0;
			columns.Add(FLD_CREATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_CREATE_DATE, typeof(System.DateTime));
			columns.Add(FLD_UPDATE_USER_ID, typeof(System.Int32));
			columns.Add(FLD_UPDATE_DATE, typeof(System.DateTime));	
			
		}

	}
}
