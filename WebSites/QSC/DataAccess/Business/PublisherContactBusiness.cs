namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using System.Text.RegularExpressions;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.PublisherContactData;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class PublisherContactBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();

		public PublisherContactBusiness(Message messageManager) : base(messageManager) { }
		public PublisherContactBusiness(bool hasMessageManager) : base(hasMessageManager) { }
		
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

		public void SelectAllByPublisherID(DataTable table, int publisherID)
		{
			try
			{
				dataAccess.SelectAllByPublisherID(table, publisherID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void Insert(DataTable table, int publisherID, string contactFirstName, string contactLastName, string positionTitle, string email)
		{
			bool isSuccess = false;

			try
			{
				if(Validate(contactFirstName, contactLastName, positionTitle, email)) 
				{
					dataAccess.Insert(table, publisherID, contactFirstName, contactLastName, positionTitle, email);
					isSuccess = true;
				} 
				else 
				{
					isSuccess = false;
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
			}
			if(!isSuccess)
			{
				throw new ValidationException(messageManager);
			}
		}

		public bool Update(int publisherContactID, string contactFirstName, string contactLastName, string positionTitle, string email, bool isMainContact) 
		{
			bool isSuccess = false;
			PublisherContactProductBusiness publisherContactProductBusiness;
			PublisherContactProductTable publisherContactProductTable;
			
			try
			{
				if(Validate(contactFirstName, contactLastName, positionTitle, email)) 
				{
					if(isMainContact) 
					{
						publisherContactProductBusiness = new PublisherContactProductBusiness(messageManager);
						publisherContactProductTable = new PublisherContactProductTable();

						publisherContactProductBusiness.SelectAllByPublisherContactID(publisherContactProductTable, publisherContactID);

						foreach(DataRow row in publisherContactProductTable.Rows) 
						{
							publisherContactProductBusiness.Delete(publisherContactID, Convert.ToInt32(row["ID"]), false);
						}
					}

					NbRowAffected = 0;
					NbRowAffected = dataAccess.Update(publisherContactID, contactFirstName, contactLastName, positionTitle, email);
					if (NbRowAffected != 0)
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
			if(!isSuccess)
			{
				throw new ValidationException(messageManager);
			}

			return isSuccess;
		}

		public void Delete(int publisherContactID)
		{
			ConnectionProvider connectionProvider = new ConnectionProvider();

			try
			{
				PublisherContactProductBusiness publisherContactProductBusiness = new PublisherContactProductBusiness(messageManager);
				DataTable table = new DataTable("PublisherContact_Product");

				publisherContactProductBusiness.SelectAllByPublisherContactID(table, publisherContactID);
				
				this.MainConnectionProvider = connectionProvider;
				publisherContactProductBusiness.MainConnectionProvider = connectionProvider;
				connectionProvider.OpenConnection();
				connectionProvider.BeginTransaction("DeletePublisherContact");

				foreach(DataRow row in table.Rows) 
				{
					publisherContactProductBusiness.Delete(publisherContactID, Convert.ToInt32(row["ID"]), false);
				}

				dataAccess.Delete(publisherContactID);

				connectionProvider.CommitTransaction();
				connectionProvider.CloseConnection(false);

				this.MainConnectionProvider = null;
			}
			catch(Exception ex)
			{	
				if (connectionProvider.DBConnection.State != ConnectionState.Closed) 
				{
					connectionProvider.RollbackTransaction("DeletePublisherContact");
				}

				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		private bool Validate(string contactFirstName, string contactLastName, string positionTitle, string email) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(contactFirstName, "Contact First Name");
			isValid &= IsValid_RequiredField(contactLastName, "Contact Last Name");

			if(isValid) 
			{
				isValid &= IsValid_FieldLength(contactFirstName, "Contact First Name", 1, 30);
				isValid &= IsValid_FieldLength(contactLastName, "Contact Last Name", 1, 30);
				isValid &= IsValid_FieldLength(positionTitle, "Contact Position Title", 0, 50);
				isValid &= IsValid_FieldLength(email, "Contact Email", 0, 50);
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