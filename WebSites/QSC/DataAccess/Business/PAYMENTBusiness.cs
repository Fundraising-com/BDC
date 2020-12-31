namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.PAYMENTTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.PAYMENTData;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	public enum PaymentMethod 
	{
		Other		= 50001,
		CheckCash	= 50002,
		Visa		= 50003,
		MasterCard	= 50004,
		Error		= 50005,
        PayPal      = 50006
    }
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class PAYMENTBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		protected System.Text.RegularExpressions.Regex cardExp = new System.Text.RegularExpressions.Regex( @"(\d{4})(\d{4})(\d{4})(\d{4})" );
		protected string safeOutputExp = "$1********$4";

		public PAYMENTBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public PAYMENTBusiness(bool AsMessageManager):base(AsMessageManager)
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
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectByOrderID(DataTable Table,int OrderID)
		{
			try
			{
				dataAccess.SelectByOrderID(Table,OrderID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectCustomerCreditCardInformation(DataTable Table,int COHI)
		{
			try
			{
				dataAccess.SelectCustomerCreditCardInformation(Table,COHI);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
      public void SelectCustomerGiftCardInformation(DataTable Table, int COHI)
      {
         try
         {
            dataAccess.SelectCustomerGiftCardInformation(Table, COHI);
         }
         catch (Exception ex)
         {
            ManageError(ex);
            messageManager.ValidationExceptionType = ExceptionType.Select;
            throw new ExceptionFulf(messageManager);
         }
      }
      public int UpdateCreditCardInformation(CreditCard creditCardInfo, int customerOrderHeaderInstance, int transID, int newCustomerOrderHeaderInstance, bool closeOrder, int problemCode, int communicationChannelInstance, int communicationSourceInstance, double priceToCharge)
		{
			int returnedNewCustomerOrderHeaderInstance = 0;
			
			bool IsSuccess = false;
			try
			{
				IsSuccess = ValidateCreditCard(creditCardInfo);
				if (IsSuccess)
				{
               returnedNewCustomerOrderHeaderInstance = dataAccess.UpdateCreditCardInformation(creditCardInfo, customerOrderHeaderInstance, transID, newCustomerOrderHeaderInstance, closeOrder, problemCode, communicationChannelInstance, communicationSourceInstance, priceToCharge);
					if (returnedNewCustomerOrderHeaderInstance != 0)
					{
						IsSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
						IsSuccess = false;
					}
				}
			

			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
			}
			if ( !IsSuccess )
			{
				messageManager.Add("The error happened after the transaction. Do not retry, the informations will be updated soon.");
				messageManager.PrepareErrorMessage();
				throw new ValidationException(messageManager);
			}
				
			return returnedNewCustomerOrderHeaderInstance;
			
		}
		public bool ValidateCreditCard(CreditCard CreditCardInfo)
		{
			bool IsSucces = true;

			if(CreditCardInfo.CardHolderName == String.Empty)
			{
				IsSucces = false;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Card Holder Name"));
			}
			if(CreditCardInfo.CreditCardNumber == String.Empty)
			{
				IsSucces = false;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Credit Card Number"));
			}
			if(CreditCardInfo.PaymentMethodID == 0)
			{
				IsSucces = false;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Payment Method"));
			}
			if(CreditCardInfo.ExpirationMonth ==String.Empty)
			{
				IsSucces = false;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Expiration Month "));
			}
			if(CreditCardInfo.ExpirationYear ==String.Empty)
			{
				IsSucces = false;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Expiration Year "));
			}
			if(IsSucces)
			{
				if(!cardExp.IsMatch(CreditCardInfo.CreditCardNumber))
				{
					IsSucces = false;
					messageManager.Add(Message.ERRMSG_VALID_CREDIT_CARD_FORMAT);
				}
			}
			return IsSucces;
		}
		
		public void SelectRefundsByCOD(DataTable Table, int CustomerOrderHeaderInstance, int TransID) 
		{
			try
			{
				dataAccess.SelectRefundsByCOD(Table, CustomerOrderHeaderInstance, TransID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

        public DataTable SelectRefundsAvailableToCancelByCOD(int CustomerOrderHeaderInstance, int TransID)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataAccess.SelectRefundsByCOD(dataTable, CustomerOrderHeaderInstance, TransID);
                DataView dataView = new DataView(dataTable, "RefundCancelled = 0 AND (AP_Cheque_Status_ID IN (2) OR (CreateDate > '2009-02-06 11:46' AND AP_Cheque_Status_ID IS NULL))", "CreateDate DESC", DataViewRowState.CurrentRows); //2: Outstanding, NULL: Refund not yet sent, < 02/2009 is legacy, don't allow cancel
                return dataView.ToTable();
            }
            catch (Exception ex)
            {
                ManageError(ex);
                messageManager.ValidationExceptionType = ExceptionType.Select;
                throw new ExceptionFulf(messageManager);
            }
        }

        public double SelectRefundTotalAmountByCOD(int CustomerOrderHeaderInstance, int TransID)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataAccess.SelectRefundsByCOD(dataTable, CustomerOrderHeaderInstance, TransID);

                object refundTotal = dataTable.Compute("SUM(Amount)", "AP_Cheque_Status_ID IN (1, 2, 3) OR AP_Cheque_Status_ID IS NULL"); //1: Unknown, 2: Outstanding, 3: Paid, NULL: Refund not yet sent or legacy
                
                return refundTotal == System.DBNull.Value ? 0d : Convert.ToDouble(refundTotal);
            }
            catch (Exception ex)
            {
                ManageError(ex);
                messageManager.ValidationExceptionType = ExceptionType.Select;
                throw new ExceptionFulf(messageManager);
            }
        }

        public double SelectMaxRefundAmountByCustomerOrderDetail(int customerOrderHeaderInstance, int transID)
        {
            try
            {
                return dataAccess.SelectMaxRefundAmountByCustomerOrderDetail(customerOrderHeaderInstance, transID);
            }
            catch (Exception ex)
            {
                ManageError(ex);
                messageManager.ValidationExceptionType = ExceptionType.Select;
                throw new ExceptionFulf(messageManager);
            }
        }
        public void CancelCheque(int RefundID)
        {

            try
            {
                dataAccess.CancelRefund(RefundID);
            }
            catch (Exception ex)
            {
                ManageError(ex);
                messageManager.ValidationExceptionType = ExceptionType.Unknown;
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CHEQUE_NUMBER,"", tableRef.FLD_CHEQUE_NUMBER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CHEQUE_PAYER,"",tableRef.FLD_CHEQUE_PAYER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CREDIT_CARD_OWNER,"", tableRef.FLD_CREDIT_CARD_OWNER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CREDIT_CARD_AUTHORIZATION,"", tableRef.FLD_CREDIT_CARD_AUTHORIZATION_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_NOTE_TO_PRINT,"", tableRef.FLD_NOTE_TO_PRINT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_LAST_UPDATED_BY,"", tableRef.FLD_LAST_UPDATED_BY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTRY_CODE,"", tableRef.FLD_COUNTRY_CODE_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}