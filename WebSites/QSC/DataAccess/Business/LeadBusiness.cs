namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.LeadTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.LeadData;
	using QSPFulfillment.DataAccess.Common;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class LeadBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		
		public LeadBusiness(Message MessageManager):base(MessageManager)
	{
		
	}
		public LeadBusiness(bool AsMessageManager):base(AsMessageManager)
	{
		
	}
		public bool Delete(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Delete(Table,dataAccess);
		}
		public bool Insert(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation with the overriden Validate Method 
			//is in the current class
			return this.Insert(Table,dataAccess);	
		}
		public bool UpdateBatch(tableRef Table)
		{			
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.UpdateBatch(Table,dataAccess);
		}
		public bool Update(tableRef Table)
		{
			//We call a method from the inherit class, but the
			//validation is provide by the overriden Validate Method 
			//is in the current class
			return this.Update(Table, dataAccess);
		}
		public void SelectAll(DataTable Table,string FMID)
		{
			try
			{
				dataAccess.SelectAll(Table, FMID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectOne(DataTable Table,int UserID)
		{
			try
			{
				dataAccess.SelectOneWUserIDLogic(Table,UserID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		//----------------------------------------------------------------
		// Function Validate:
		//   Validates 
		// Returns:
		//   true if validation is successful 
		//   false if invalid fields exist 
		// Parameters:
		//   [in]  row: to be validated
		//   [out] row: If there are fields
		//              that contain errors they are individually marked.  
		//----------------------------------------------------------------
		protected override bool Validate(DataRow Row)
		{
			bool isValid = true;
			//Clear all errors
			Row.ClearErrors();
			if ((Row.RowState == DataRowState.Added) || (Row.RowState == DataRowState.Modified))
			{
				isValid = IsValid_RequiredFields(Row);
				isValid &= IsValids_FieldLength(Row);
			}
			return isValid;
		}
		//----------------------------------------------------------------
		// Function IsValid_RequiredField:
		//   Validates a specific tableRef field as Mandatory 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  Row: Row to be validated
		//----------------------------------------------------------------
		private bool IsValid_RequiredFields(DataRow Row)
		{
			bool IsValid = true;
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_USERID,"UserID");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CONTACTWORKPHONENUMBER,"Contact Work Phone Number");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_PROVINCE,"Province");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CONTACTNAME,"Contact Name");



			
			if (!IsValid)
			{
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
			}
			return IsValid;
		}
		//----------------------------------------------------------------
		// Function IsValid_FieldLength:
		//   Validates a specific tableRef field against his maxlength 
		// Returns:
		//   False if field fails the validation rules.
		// Parameters:
		//   [in]  Row: Row to be validated
		//   [in]  fieldName: field in to be validated
		//   [in]  maxLen: max length for the field
		//----------------------------------------------------------------
		private bool IsValids_FieldLength(DataRow Row)
		{
			bool isValid = true;
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTNAME,"Contact Name", tableRef.FLD_CONTACTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTHOMEPHONENUMBER,"Phone Number", tableRef.FLD_CONTACTHOMEPHONENUMBER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTWORKPHONENUMBER,"Home Phone Number", tableRef.FLD_CONTACTWORKPHONENUMBER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTFAXNUMBER,"Fax Number", tableRef.FLD_CONTACTFAXNUMBER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTEMAIL,"Email", tableRef.FLD_CONTACTEMAIL_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_SCHOOLGROUP,"School Group", tableRef.FLD_SCHOOLGROUP_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CITYTOWN,"City Town", tableRef.FLD_CITYTOWN_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PROVINCE,"Province", tableRef.FLD_PROVINCE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_INTERESTEDINWHAT,"Interested in What...", tableRef.FLD_INTERESTEDINWHAT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_WHEREHEARABOUTQSP,"Where did you hear...", tableRef.FLD_WHEREHEARABOUTQSP_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COMMENTS, "Comments", tableRef.FLD_COMMENTS_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_FMID,"Field Manager ID" ,tableRef.FLD_FMID_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}