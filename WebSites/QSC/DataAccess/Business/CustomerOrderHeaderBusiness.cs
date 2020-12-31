namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.CustomerOrderHeaderTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CustomerOrderHeaderData;
	using DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject; 
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CustomerOrderHeaderBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public CustomerOrderHeaderBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CustomerOrderHeaderBusiness(bool AsMessageManager):base(AsMessageManager)
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
		public void SelectOrderTotals(DataTable Table,int OrderID)
		{
			try
			{
				dataAccess.SelectOrderTotals(Table,OrderID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
	
		public void SelectAddressInfo(DataTable Table,int CampaignID,AddressType Type)
		{
			try
			{
				dataAccess.Select(Table,CampaignID,Type.ToString());
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectBillToAddress(DataTable Table,int COHI,int TransID)
		{
			try
			{
				dataAccess.SelectCustomerAddress(Table,COHI,TransID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectBillToAddressHistory(DataTable Table,int COHI, int transID)
		{
			try
			{
				dataAccess.SelectCustomerAddressHistory(Table,COHI, transID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectShipToAddressHistory(DataTable Table,int COHI,int TransID)
		{
			try
			{
				dataAccess.SelectShipToAddressHistory(Table,COHI,TransID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectShipToAddress(DataTable Table,int COHI,int TransID)
		{
			try
			{
				dataAccess.SelectShipToAddress(Table,COHI,TransID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectCustomerRefundAddress(DataTable Table,int COHI,int TransID)
		{
			try
			{
				dataAccess.SelectCustomerRefundAddress(Table,COHI,TransID);
			}
			catch(Exception ex)
			{
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public bool RecordRecipientAddressHistory(int customerOrderHeaderInstance, int transID) 
		{
			bool IsSuccess = false;

			try 
			{
				NbRowAffected = 0;
				NbRowAffected = dataAccess.RecordRecipientAddressHistory(customerOrderHeaderInstance, transID);

				if (NbRowAffected != 0)
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
			catch(Exception ex) 
			{
				ManageError(ex);
				messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
				throw new ExceptionFulf(messageManager);
			}
			if ( !IsSuccess )
			{
				throw new ValidationException(messageManager);
			}

			return IsSuccess;
		}

		public void ChangeNameAddress(ChangeOfAddress value, int communicationChannelInstance, int communicationSourceInstance)
		{
			bool IsSuccess = false;
			try
			{
				this.oResultSetReturned = dataAccess.ChangeNameAddress(value, communicationChannelInstance, communicationSourceInstance);
					
				if (this.oResultSetReturned != null)
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
			catch(Exception EX)
			{
				ManageError(EX);
			}
			
			if ( !IsSuccess )
			{
				messageManager.PrepareErrorMessage();
				throw new ValidationException(messageManager);
			}
			
		}
		public void ChangeNameAddressBeforeRemit(ChangeOfAddress value, int communicationChannelInstance, int communicationSourceInstance)
		{
			try
			{
				dataAccess.ChangeNameAddressBeforeRemit(value, communicationChannelInstance, communicationSourceInstance);
			}
			catch(Exception EX)
			{
				ManageError(EX);
			}
		}

		public bool ValidateChangeNameAddress(ChangeOfAddress Value)
		{
			bool isValid = true;
			

			if( Value.CustomerInfo.FirstName == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"First Name"));
				isValid =false;
			}
			if( Value.CustomerInfo.LastName == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Last Name"));
				isValid &=false;
			}
			if( Value.CustomerInfo.CustomerAddress.Street1 == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Street"));
				isValid &=false;
			}
			if( Value.CustomerInfo.CustomerAddress.City == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"City"));
				isValid &=false;
			}
			if( Value.CustomerInfo.CustomerAddress.PostalCode == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Postal Code"));
				isValid &=false;
			}
			if( Value.CustomerInfo.CustomerAddress.StateProvinceCode == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Province"));
				isValid &=false;
			}
			if( Value.CustomerInfo.CustomerAddress.CountryCode == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Country"));
				isValid &=false;
			}
			if( Value.CustomerInfo.PhoneNumber == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Phone Number"));
				isValid &=false;
			}

			
			
			
			return isValid;
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_NEXTDETAILTRANSID,"NextDetailTransID");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ACCOUNTID,"AccountID");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CUSTOMERBILLTOINSTANCE,"CustomerBillToInstance");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_STUDENTINSTANCE,"StudentInstance");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_STATUSINSTANCE,"StatusInstance");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_FIRSTSTATUSINSTANCE,"FirstStatusInstance");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_TOTALPROCESSINGFEE,"TotalProcessingFee");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_TOTALPROCESSINGFEEA,"TotalProcessingFeeA");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_PROCESSINGFEETAX,"ProcessingFeeTax");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_PROCESSINGFEETAXA,"ProcessingFeeTaxA");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_PROCESSINGFEETRANSID,"ProcessingFeeTransID");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ORDERBATCHDATE,"OrderBatchDate");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ORDERBATCHID,"OrderBatchID");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ORDERBATCHSEQUENCE,"OrderBatchSequence");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CREATIONDATE,"CreationDate");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_LASTSENTINVOICEDATE,"LastSentInvoiceDate");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_NUMBERINVOICESSENT,"NumberInvoicesSent");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_FORCEINVOICE,"ForceInvoice");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_DELFLAG,"DelFlag");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_CHANGEDATE,"ChangeDate");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CHANGEUSERID,"", tableRef.FLD_CHANGEUSERID_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
		

	}
}