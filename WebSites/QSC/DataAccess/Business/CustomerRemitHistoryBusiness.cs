namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.CustomerRemitHistoryTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CustomerRemitHistoryData;
	using DataAccess.Common;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CustomerRemitHistoryBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public CustomerRemitHistoryBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CustomerRemitHistoryBusiness(bool AsMessageManager):base(AsMessageManager)
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
				isValid &= IsValids_FieldLength(Row);
			}
			return isValid;
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_LASTNAME,"", tableRef.FLD_LASTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_FIRSTNAME,"", tableRef.FLD_FIRSTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ADDRESS1,"", tableRef.FLD_ADDRESS1_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ADDRESS2,"", tableRef.FLD_ADDRESS2_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CITY,"", tableRef.FLD_CITY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_STATE,"", tableRef.FLD_STATE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ZIP,"", tableRef.FLD_ZIP_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ZIPPLUSFOUR,"", tableRef.FLD_ZIPPLUSFOUR_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_USERIDMODIFIED,"", tableRef.FLD_USERIDMODIFIED_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}