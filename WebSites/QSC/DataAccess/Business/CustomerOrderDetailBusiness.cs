namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.CustomerOrderDetailTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CustomerOrderDetailData;
	using DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CustomerOrderDetailBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		
		public CustomerOrderDetailBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CustomerOrderDetailBusiness(bool AsMessageManager):base(AsMessageManager)
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

		public void SelectOne(DataTable Table, Int32 CustomerOrderHeaderInstance, Int32 TransID, int AdjustPriceToStaffOrder)
		{
			try
			{
				dataAccess.SelectOne(Table, CustomerOrderHeaderInstance, TransID, AdjustPriceToStaffOrder);
		    }
		    catch (Exception ex)
		    {	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
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
		public void SelectSubscriptionForChadd(DataTable Table,int CustomerOrderHeaderInstance, int TransID, bool ShowCancelledSubs, bool ShowCurrentSubscription)
		{
			
			try
			{
				dataAccess.SelectSubscriptionForChadd(Table,CustomerOrderHeaderInstance, TransID, ShowCancelledSubs, ShowCurrentSubscription);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
			
		}
		public void UpdateSubscriptionForChadd(int CustomerOrderHeaderInstance, int TransID, int CustomerInstance)
		{
			
			try
			{
				dataAccess.UpdateSubscriptionForChadd(CustomerOrderHeaderInstance, TransID, CustomerInstance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
			}
			
		}
		public bool NewItem(NewSubcription newItem, int productType, int orderQualifierID, int oldCustomerOrderHeaderInstance, int oldTransID)
		{
			bool IsSuccess = false;
			try
			{
				if (ValidateNewItem(newItem))
				{	
					NbRowAffected = 0;
					NbRowAffected = dataAccess.NewItem(newItem, productType, orderQualifierID, oldCustomerOrderHeaderInstance, oldTransID);
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
				else 
				{
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

		public bool CancelSub(CancelSubscription Value)
		{
			bool IsSuccess = false;
			try
			{
				
					NbRowAffected = 0;
					NbRowAffected = dataAccess.CancelSub(Value);
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
		public Action GetCancelAction(int CustomerOrderHeaderInstance,int TransID)
		{
			try
			{
				return (Action)dataAccess.GetCancelAction(CustomerOrderHeaderInstance,TransID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
			
		}
		public bool CancelSubPriorToRemit(CancelSubscription Value)
		{
			bool IsSuccess = false;
			try
			{
				
					NbRowAffected = 0;
					NbRowAffected = dataAccess.CancelSubPriorToRemit(Value);
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

        public bool ResendSub(ResendSubscription Value)
        {
            bool IsSuccess = false;
            try
            {

                NbRowAffected = 0;
                NbRowAffected = dataAccess.ResendSub(Value);
                if (NbRowAffected != 0)
                {
                    IsSuccess = true;
                }
                else
                {
                    messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
                    messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
                    IsSuccess = false;
                }
            }
            catch (Exception EX)
            {
                ManageError(EX);
                return false;
            }

            if (!IsSuccess)
            {
                throw new ValidationException(messageManager);
            }
            return IsSuccess;
        }

        public bool ProductUpdate(int CustomerOrderHeaderInstance, int TransID, int MagPriceInstance)
        {
            bool IsSuccess = false;
            try
            {
                NbRowAffected = 0;
                NbRowAffected = dataAccess.ProductUpdate(CustomerOrderHeaderInstance, TransID, MagPriceInstance);
                if (NbRowAffected != 0)
                {
                    IsSuccess = true;
                }
                else
                {
                    messageManager.ValidationExceptionType = ExceptionType.OtherBusinessRules;
                    messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
                    IsSuccess = false;
                }
            }
            catch (Exception EX)
            {
                ManageError(EX);
                return false;
            }
            if (!IsSuccess)
            {
                throw new ValidationException(messageManager);
            }
            return IsSuccess;
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRODUCTCODE,"Product Code", tableRef.FLD_PRODUCTCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRODUCTNAME, "Product Name",tableRef.FLD_PRODUCTNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_RENEWAL,"Renewal", tableRef.FLD_RENEWAL_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_RECIPIENT,"Recipient", tableRef.FLD_RECIPIENT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_CHANGEUSERID, "Change User ID",tableRef.FLD_CHANGEUSERID_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ALPHAPRODUCTCODE,"Alpha Product Code", tableRef.FLD_ALPHAPRODUCTCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUPONPAGE,"Coupon Page", tableRef.FLD_COUPONPAGE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_FDINDICATOR, "Indicator",tableRef.FLD_FDINDICATOR_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_MKTGINDICATOR,"", tableRef.FLD_MKTGINDICATOR_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_GIFTCD, "",tableRef.FLD_GIFTCD_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_SUPPORTERNAME, "Supporter Name",tableRef.FLD_SUPPORTERNAME_LENGTH);
			return isValid;
		}
		
		private bool ValidateNewItem(NewSubcription NewSub)
		{
			bool isValid = true;

			if(NewSub.Price == 0 && NewSub.OverrideCode == 45004)
			{
				messageManager.ValidationExceptionType  = ExceptionType.RequiredFields;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Override Reason"));			
				isValid = false;
			}
			else 
			{
				isValid &= IsValid_RequiredField(NewSub.FirstName, "First Name");
				isValid &= IsValid_RequiredField(NewSub.LastName, "Last Name");
				isValid &= IsValid_RequiredField(NewSub.Address1, "Address 1");
				isValid &= IsValid_RequiredField(NewSub.City, "City");
				isValid &= IsValid_RequiredField(NewSub.PostalCode, "Postal Code");
				isValid &= IsValid_RequiredField(NewSub.Province, "Province");
			}

			if(!isValid)
				messageManager.PrepareErrorMessage();

			return isValid;
		}

		protected bool IsValid_RequiredField(object FieldToValidate, string FieldForErrorMessage)
		{
			if (FieldToValidate.ToString() == string.Empty)
			{
				//
				// Mark the field as invalid
				//
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, FieldForErrorMessage));
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
				return false;
			}
			return true;
		}

		protected override DBInteractionBase DataAccessReference
		{
			get
			{
				return this.dataAccess;
			}
		}
		
		
		

	}
}