namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.CustomerTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CustomerData;
	using DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CustomerBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public CustomerBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CustomerBusiness(bool AsMessageManager):base(AsMessageManager)
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

		public void SelectCustomerByCOH(DataTable Table, int CustomerOrderHeaderInstance) 
		{
			try
			{
				dataAccess.SelectCustomerByCOH(Table, CustomerOrderHeaderInstance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectCustomerByCOD(DataTable Table, int CustomerOrderHeaderInstance, int TransID) 
		{
			try
			{
				dataAccess.SelectCustomerByCOD(Table, CustomerOrderHeaderInstance, TransID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

        public void UpdateCustomerEmailByCOD(int CustomerOrderHeaderInstance, int TransID, string Email)
        {
            try
            {
                dataAccess.UpdateCustomerEmailByCOD(CustomerOrderHeaderInstance, TransID, Email);
            }
            catch (Exception ex)
            {
                ManageError(ex);
                messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
                throw new ExceptionFulf(messageManager);
            }
        }

		public int InsertForCHADD(Customer CustomerInfo, string UserID) 
		{
			try 
			{
				return dataAccess.InsertForCHADD(CustomerInfo, UserID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.OtherBusinessRules;
				throw new ExceptionFulf(messageManager);
			}  
		}
		
		public void RefundCustomer(RefundCustomer RefundCustomerInfo)
		{
		
			try
			{
				dataAccess.RefundCustomer(RefundCustomerInfo);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
			}  
		}

		public void SelectAllResolveCreditCardRefunds(DataTable table) 
		{
			try
			{
				dataAccess.SelectAllResolveCreditCardRefunds(table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public bool UpdateResolveCreditCardRefund(string creditCardNumber, int refundStatus) 
		{
			bool IsSuccess = false;
			try
			{
				NbRowAffected = 0;
				NbRowAffected = dataAccess.UpdateResolveCreditCardRefund(creditCardNumber, refundStatus);
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
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			if ( !IsSuccess )
			{
				throw new ValidationException(messageManager);
			}
			return IsSuccess;
		}

		public void ValidateRefundCustomer(RefundCustomer Value)
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
			
			if( Value.RefundAmount == 0 )
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Refund Amount"));
				isValid &=false;
			}
			if(Value.RefundAmount > Value.RegularPrice) 
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.ERRMSG_CANNOT_BE_HIGHER_2, new string[] {"Refund Amount", "Regular Price"}));
				isValid &=false;
			}
			if(!isValid)
			{
				messageManager.PrepareErrorMessage();
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_STATUSINSTANCE,"StatusInstance");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_OVERRIDEADDRESS,"OverrideAddress");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_LASTNAME,"Last Name", tableRef.FLD_LASTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_FIRSTNAME,"First Name", tableRef.FLD_FIRSTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ADDRESS1,"Address 1", tableRef.FLD_ADDRESS1_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ADDRESS2,"Address 2", tableRef.FLD_ADDRESS2_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CITY,"City", tableRef.FLD_CITY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTY,"County", tableRef.FLD_COUNTY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_STATE,"State", tableRef.FLD_STATE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ZIP,"ZIP", tableRef.FLD_ZIP_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ZIPPLUSFOUR,"", tableRef.FLD_ZIPPLUSFOUR_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CHANGEUSERID,"", tableRef.FLD_CHANGEUSERID_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_EMAIL,"Email", tableRef.FLD_EMAIL_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PHONE,"Phone", tableRef.FLD_PHONE_LENGTH);
			return isValid;
		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}

	}
}