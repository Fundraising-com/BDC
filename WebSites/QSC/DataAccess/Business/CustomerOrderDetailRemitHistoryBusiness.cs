namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.CustomerOrderDetailRemitHistoryTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CustomerOrderDetailRemitHistoryData;
	using DataAccess.Common;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CustomerOrderDetailRemitHistoryBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public CustomerOrderDetailRemitHistoryBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CustomerOrderDetailRemitHistoryBusiness(bool AsMessageManager):base(AsMessageManager)
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
		public void SelectAll(DataTable Table)
		{
			try
			{
				dataAccess.SelectAll(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType = ExceptionType.Select;
				throw new QSPFulfillment.DataAccess.Common.ExceptionFulf(messageManager, ex);
				
			}
		}
		public void SelectOne(DataTable Table,Int32 CustomerOrderHeaderInstance,Int32 TransID,Int32 RemitBatchID)
		{
			try
			{
				dataAccess.SelectOne(Table,CustomerOrderHeaderInstance,TransID,RemitBatchID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectOneLastTransaction(DataTable Table, Int32 CustomerOrderHeaderInstance,Int32 TransID)
		{
			try
			{
			dataAccess.SelectOneLastTransaction(Table,CustomerOrderHeaderInstance,TransID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectByCOHInstance(DataTable Table, Int32 CustomerOrderHeaderInstance,int TransID)
		{
			try
			{
				dataAccess.SelectByCOHInstance(Table,CustomerOrderHeaderInstance,TransID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectByDate(DataTable Table,int Status,DateTime From,DateTime To)
		{
			try
			{
			dataAccess.SelectByDate(Table,Status,From,To);
			}
			catch(Exception ex)
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_COUNTRYCODE,"CountryCode");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CUSTOMERREMITHISTORYINSTANCE,"CustomerRemitHistoryInstance");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTRYCODE,"", tableRef.FLD_COUNTRYCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_LANG,"", tableRef.FLD_LANG_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PREMIUMCODE,"", tableRef.FLD_PREMIUMCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PREMIUMDESCRIPTION,"", tableRef.FLD_PREMIUMDESCRIPTION_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ABCCODE,"", tableRef.FLD_ABCCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_RENEWAL,"", tableRef.FLD_RENEWAL_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_TITLECODE,"", tableRef.FLD_TITLECODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_MAGAZINETITLE,"", tableRef.FLD_MAGAZINETITLE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COMMENT,"", tableRef.FLD_COMMENT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_GIFTORDERTYPE,"", tableRef.FLD_GIFTORDERTYPE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_SUPPORTERNAME,"", tableRef.FLD_SUPPORTERNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_USERIDCHANGED,"", tableRef.FLD_USERIDCHANGED_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_EFFORTKEY,"", tableRef.FLD_EFFORTKEY_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}