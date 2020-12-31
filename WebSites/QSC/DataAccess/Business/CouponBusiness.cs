namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.CouponTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CouponData;
	using DataAccess.Common;
	using DataAccess.Common.ActionObject;
	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CouponBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public CouponBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CouponBusiness(bool AsMessageManager):base(AsMessageManager)
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
		public int ValidateCoupon(string CouponNumber)
		{
			int Value =0;
			try
			{

				if(CouponNumber  != string.Empty)
				{

					 Value = (int)dataAccess.ValidateCoupon(CouponNumber);
				
					if(Value == -1)
					{
						messageManager.ValidationExceptionType =  ExceptionType.OtherBusinessRules;	
						
						messageManager.Add(Message.ERRMSG_COUPON_INVALID);
					
						messageManager.PrepareErrorMessage();
						throw new ExceptionFulf(messageManager);

					}
				}
				else
				{
					messageManager.ValidationExceptionType =  ExceptionType.RequiredFields;
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"certificate"));
					messageManager.PrepareErrorMessage();
					throw new ExceptionFulf(messageManager);
				}
			}
				
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
			return Value;
		}

		public int InsertCustomer(Customer CustomerInfo,string UserID)
		{
			try
			{
				if(ValidateInsertCustomer(CustomerInfo))
				{

					this.oResultSetReturned = dataAccess.InsertCustomer(CustomerInfo,UserID);
				
					
				}
				else
				{
					messageManager.PrepareErrorMessage();
					throw new ExceptionFulf(messageManager);
				}
			}
				
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
			return this.RowAffected;
		}
		public int UpdateCustomer(Customer CustomerInfo,string UserID)
		{
			try
			{
				if(ValidateInsertCustomer(CustomerInfo))
				{

					dataAccess.UpdateCustomer(CustomerInfo,UserID);
				
					
				}
				else
				{
					messageManager.PrepareErrorMessage();
					throw new ExceptionFulf(messageManager);
				}
			}
				
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
			return this.RowAffected;
		}
		public int GetCurrentQSPCampaign()
		{
			try
			{
				return (int)dataAccess.GetCurrentQSPCampaign();
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
				throw new ExceptionFulf(messageManager);
			}
		}
		public int UpdateCouponStatus(string CouponID)
		{
			try
			{
				return dataAccess.UpdateCouponStatus(CouponID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Unknown;
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_COUPONSETID,"CouponSetID");
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_ISUSED,"IsUsed");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ID,"", tableRef.FLD_ID_LENGTH);
			return isValid;
		}
		private bool ValidateInsertCustomer(Customer CustomerInfo)
		{
			bool isValid = true;

			if(CustomerInfo.FirstName == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"First Name"));
				isValid =false;
			}
			if(CustomerInfo.LastName == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Last Name"));
				isValid &=false;
			}
			if(CustomerInfo.CustomerAddress.Street1 == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Street"));
				isValid &=false;
			}
			if(CustomerInfo.CustomerAddress.City == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"City"));
				isValid &=false;
			}
			if(CustomerInfo.CustomerAddress.PostalCode == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Postal Code"));
				isValid &=false;
			}
			if(CustomerInfo.CustomerAddress.StateProvinceCode == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Province"));
				isValid &=false;
			}
			if(CustomerInfo.CustomerAddress.CountryCode == String.Empty)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Country"));
				isValid &=false;
			}

			if(CustomerInfo.Type == CustomerType.none)
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Customer Type"));
				isValid &=false;
			}
			return isValid;

		}
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}