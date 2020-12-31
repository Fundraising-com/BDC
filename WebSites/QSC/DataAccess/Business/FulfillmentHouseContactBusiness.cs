namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using System.Text.RegularExpressions;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.FulfillmentHouseContactData;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class FulfillmentHouseContactBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();

		public FulfillmentHouseContactBusiness(Message messageManager) : base(messageManager) { }
		public FulfillmentHouseContactBusiness(bool asMessageManager) : base(asMessageManager) { }
		
		public void SelectAll(DataTable table)
		{
			try
			{
				dataAccess.SelectAll(table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllByFulfillmentHouseID(DataTable table, int fulfillmentHouseID)
		{
			try
			{
				dataAccess.SelectAllByFulfillmentHouseID(table, fulfillmentHouseID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public int Insert(int fulfillmentHouseID, string contactFirstName, string contactLastName, string positionTitle, string email, string workPhone, string fax, string customerServiceContactFirstName, string customerServiceContactLastName, string customerServiceContactEmail, string customerServiceContactPhone) 
		{
			int newFulfillmentHouseContactID = 0;

			try
			{
				if(Validate(contactFirstName, contactLastName, positionTitle, email, workPhone, fax, customerServiceContactFirstName, customerServiceContactLastName, customerServiceContactEmail, customerServiceContactPhone)) 
				{
					newFulfillmentHouseContactID = dataAccess.Insert(fulfillmentHouseID, contactFirstName, contactLastName, positionTitle, email, workPhone, fax, customerServiceContactFirstName, customerServiceContactLastName, customerServiceContactEmail, customerServiceContactPhone);
					
					if(newFulfillmentHouseContactID == 0)
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
					}
				} 
			}
			catch(Exception EX)
			{
				ManageError(EX);
			}
			if(newFulfillmentHouseContactID == 0)
			{
				throw new ValidationException(messageManager);
			}

			return newFulfillmentHouseContactID;
		}

		public bool Update(int fulfillmentHouseContactID, string contactFirstName, string contactLastName, string positionTitle, string email, string workPhone, string fax, string customerServiceContactFirstName, string customerServiceContactLastName, string customerServiceContactEmail, string customerServiceContactPhone, bool isMainContact) 
		{
			bool isSuccess = false;
			FulfillmentHouseContactProductBusiness fulfillmentHouseContactProductBusiness;
			FulfillmentHouseContactProductTable fulfillmentHouseContactProductTable;
			
			try
			{
				if(Validate(contactFirstName, contactLastName, positionTitle, email, workPhone, fax, customerServiceContactFirstName, customerServiceContactLastName, customerServiceContactEmail, customerServiceContactPhone)) 
				{
					if(isMainContact) 
					{
						fulfillmentHouseContactProductBusiness = new FulfillmentHouseContactProductBusiness(messageManager);
						fulfillmentHouseContactProductTable = new FulfillmentHouseContactProductTable();

						fulfillmentHouseContactProductBusiness.SelectAllByFulfillmentHouseContactID(fulfillmentHouseContactProductTable, fulfillmentHouseContactID);

						foreach(DataRow row in fulfillmentHouseContactProductTable.Rows) 
						{
							fulfillmentHouseContactProductBusiness.Delete(fulfillmentHouseContactID, Convert.ToInt32(row["ID"]), false);
						}
					}

					NbRowAffected = 0;
					NbRowAffected = dataAccess.Update(fulfillmentHouseContactID, contactFirstName, contactLastName, positionTitle, email, workPhone, fax, customerServiceContactFirstName, customerServiceContactLastName, customerServiceContactEmail, customerServiceContactPhone);
					if(NbRowAffected != 0)
					{
						isSuccess = true; 
					}
					else
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
						isSuccess = false;
					}
				} 
				else 
				{
					isSuccess = false;
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
			if ( !isSuccess )
			{
				throw new ValidationException(messageManager);
			}

			return isSuccess;
		}

		public void Delete(int fulfillmentHouseContactID)
		{
			ConnectionProvider connectionProvider = new ConnectionProvider();

			try
			{
				FulfillmentHouseContactProductBusiness fulfillmentHouseContactProductBusiness = new FulfillmentHouseContactProductBusiness(messageManager);
				DataTable table = new DataTable("FulfillmentHouseContact_Product");

				fulfillmentHouseContactProductBusiness.SelectAllByFulfillmentHouseContactID(table, fulfillmentHouseContactID);
				
				this.MainConnectionProvider = connectionProvider;
				fulfillmentHouseContactProductBusiness.MainConnectionProvider = connectionProvider;
				connectionProvider.OpenConnection();
				connectionProvider.BeginTransaction("DeleteFulfillmentHouseContact");

				foreach(DataRow row in table.Rows) 
				{
					fulfillmentHouseContactProductBusiness.Delete(fulfillmentHouseContactID, Convert.ToInt32(row["ID"]), false);
				}

				dataAccess.Delete(fulfillmentHouseContactID);

				connectionProvider.CommitTransaction();
				connectionProvider.CloseConnection(false);

				this.MainConnectionProvider = null;
			}
			catch(Exception ex)
			{	
				if (connectionProvider.DBConnection.State != ConnectionState.Closed) 
				{
					connectionProvider.RollbackTransaction("DeleteFulfillmentHouseContact");
					connectionProvider.CloseConnection(false);
				}

				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		private bool Validate(string contactFirstName, string contactLastName, string positionTitle, string email, string workPhone, string fax, string customerServiceContactFirstName, string customerServiceContactLastName, string customerServiceContactEmail, string customerServiceContactPhone) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(contactFirstName, "Contact First Name");
			isValid &= IsValid_RequiredField(contactLastName, "Contact Last Name");

			if(isValid) 
			{
				isValid &= IsValid_FieldLength(contactFirstName, "Contact First Name", 1, 50);
				isValid &= IsValid_FieldLength(contactLastName, "Contact Last Name", 1, 50);
				isValid &= IsValid_FieldLength(positionTitle, "Contact Position Title", 0, 50);
				isValid &= IsValid_FieldLength(email, "Contact Email", 0, 100);
				isValid &= IsValid_FieldLength(workPhone, "Work Phone", 0, 20);
				isValid &= IsValid_FieldLength(fax, "Fax", 0, 20);
				isValid &= IsValid_FieldLength(customerServiceContactFirstName, "Customer Service Contact First Name", 0, 50);
				isValid &= IsValid_FieldLength(customerServiceContactLastName, "Customer Service Contact Last Name", 0, 50);
				isValid &= IsValid_FieldLength(customerServiceContactEmail, "Customer Service Contact Email", 0, 100);
				isValid &= IsValid_FieldLength(customerServiceContactPhone, "Customer Service Contact Phone", 0, 50);
			}

			if(!isValid)
				messageManager.PrepareErrorMessage();

			return isValid;
		}

		protected bool IsValid_FieldLength(object FieldToValidate, string FieldForErrorMessage, short minLen, short maxLen)
		{
			bool isValid;

			short i = (short)(FieldToValidate.ToString().Trim().Length);
			if ( (i < minLen) || (i > maxLen) )
			{
				//
				// Mark the field as invalid
				//
				if(minLen != maxLen) 
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_LENGTH_RANGE_VAR_3, new String[] {FieldForErrorMessage, minLen.ToString(), maxLen.ToString()}));
				}
				else 
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {FieldForErrorMessage, maxLen.ToString()}));
				}
				messageManager.ValidationExceptionType = ExceptionType.MaxLength;
				isValid = false;
			}
			else 
			{
				isValid = true;
			}

			return isValid;
		}

		protected bool IsValid_RequiredField(object FieldToValidate, string FieldForErrorMessage)
		{
			bool isValid;

			if (FieldToValidate.ToString() == string.Empty)
			{
				//
				// Mark the field as invalid
				//
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, FieldForErrorMessage));
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
				isValid = false;
			} 
			else 
			{
				isValid = true;
			}

			return isValid;
		}

		protected bool IsValid_RequiredField(int FieldToValidate, string FieldForErrorMessage)
		{
			bool isValid;

			if (FieldToValidate == 0)
			{
				//
				// Mark the field as invalid
				//
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, FieldForErrorMessage));
				messageManager.ValidationExceptionType = ExceptionType.RequiredFields;
				isValid = false;
			} 
			else 
			{
				isValid = true;
			}

			return isValid;
		}

		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
	}
}