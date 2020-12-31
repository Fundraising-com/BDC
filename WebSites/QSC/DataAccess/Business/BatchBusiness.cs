namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.BatchTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.BatchData;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class BatchBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		
		public BatchBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public BatchBusiness(bool AsMessageManager):base(AsMessageManager)
		{
		
		}
		public void SelectSearch(DataTable Table,int SearchType,string SearchCriteria)
		{
			try
			{
				dataAccess.SelectSearch(Table,SearchType,SearchCriteria);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
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
		public void SelectAll(DataTable Table)
		{
			try
			{
				dataAccess.SelectAll(Table);
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ORDERID,"OrderID");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_KE3FILENAME,"", tableRef.FLD_KE3FILENAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CHANGEUSERID,"", tableRef.FLD_CHANGEUSERID_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CLERK,"", tableRef.FLD_CLERK_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_USERIDCREATED,"", tableRef.FLD_USERIDCREATED_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_BILLTOFMID,"", tableRef.FLD_BILLTOFMID_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_SHIPTOFMID,"", tableRef.FLD_SHIPTOFMID_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTFIRSTNAME,"", tableRef.FLD_CONTACTFIRSTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTLASTNAME,"", tableRef.FLD_CONTACTLASTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTEMAIL,"", tableRef.FLD_CONTACTEMAIL_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CONTACTPHONE,"", tableRef.FLD_CONTACTPHONE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COMMENT,"", tableRef.FLD_COMMENT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTRYCODE,"", tableRef.FLD_COUNTRYCODE_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}