namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.ActionTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.ActionData;
	using QSPFulfillment.DataAccess.Common;
	
	public enum Action 
	{
		None = 0,
		CancelSub=1,
		NewSub =2,
		IssueCustomerRefund=3,
		ChangeNameAddress =4,
		SwitchLetter =5,
		NotifyPublWriting =6,
		NotifyPublByPhone=7,
		NoActionRequired=8,
		ProofPaymtRequired=9,
		DirectCustomerToPubl=10,
		CustomerInformedOfSituation=11,
		HoldForNotification=12,
		MissingNameAddess = 13,
		CancelSubBeforeRemit=14,
		SaveCustomer =15,
		CancelSwitchLetter = 16,
		ReprintSwitchLetter = 17,
		CreditCard=18,
		//CreditCardRefund = 19,
		CCCallAttempt1=19,
		CCCallAttempt2=20,
		CCCallAttempt3=21,
		CCRechargeFailRemoveFromOEFU=22,
        CancelCustomerRefund=23,
        ResendSub=24,
        ProductUpdate=25,
        CCCallAttempt4 = 26,
        CCCallAttempt5 = 27,
        UpdateEmail = 28,
        NewItem = 100,
		NewSubToInvoice = 150,
		NewItemToInvoice = 151,
		NewSubTimeStaffOrLoonie = 152,
		OEDeptActionRequired =200,
		ARDeptActionRequired   =300,
		HeadOfficeFollowUpPOP =400,
		HeadOfficeFollowUpRefund = 401,
		APDeptActionRequired=500,
	}

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class ActionBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		public const int CAMPAIGNID = 43462; //Campaign ID created for the Time Staff or Loonie action

		dataAccessRef dataAccess = new dataAccessRef();
		
		public ActionBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public ActionBusiness(bool AsMessageManager):base(AsMessageManager)
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
		public void SelectAll(DataTable Table,int CustomerOrderHeaderInstance,int TransID, bool CreditCardAction)
		{
			try
			{
				dataAccess.SelectAll(Table,CustomerOrderHeaderInstance,TransID, CreditCardAction);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectOne(DataTable Table,Action Value)
		{
			try
			{
				dataAccess.SelectOne(Table,(int)Value);
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_DESCRIPTION,"Description");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_REPONSIBLEDEPTINSTANCE,"ReponsibleDeptInstance");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ISNOTIFYPUBLISHERPRINT,"IsNotifyPublisherPrint");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ISACTIONUSERUPDATABLE,"IsActionUserUpdatable");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_DESCRIPTION,"Description", tableRef.FLD_DESCRIPTION_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}