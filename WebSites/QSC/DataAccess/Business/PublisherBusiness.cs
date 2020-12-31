namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using System.Text.RegularExpressions;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.PublisherData;
	using QSPFulfillment.DataAccess.Common;

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class PublisherBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();

		public PublisherBusiness(Message messageManager) : base(messageManager) { }
		public PublisherBusiness(bool asMessageManager) : base(asMessageManager) { }
		
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

		public int Insert(string status, string publisherName, string address1, string address2, string city, string province, string postalCode, string country)
		{
			int newPublisherID = 0;
			
			try
			{
				if(Validate(status, publisherName, address1, address2, city, province, postalCode, country)) 
				{
					newPublisherID = dataAccess.Insert(status, publisherName, address1, address2, city, province, postalCode, country);
					
					if(newPublisherID == 0)
					{
						messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
						messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
					}
				} 
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return 0;
			}
			if(newPublisherID == 0)
			{
				throw new ValidationException(messageManager);
			}

			return newPublisherID;
		}

		public bool Update(int publisherID, string status, string publisherName, string address1, string address2, string city, string province, string postalCode, string country) 
		{
			bool isSuccess = false;
			
			try
			{
				if(Validate(status, publisherName, address1, address2, city, province, postalCode, country)) 
				{
					NbRowAffected = 0;
					NbRowAffected = dataAccess.Update(publisherID, status, publisherName, address1, address2, city, province, postalCode, country);
					
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
			if(!isSuccess )
			{
				throw new ValidationException(messageManager);
			}

			return isSuccess;
		}

		private bool Validate(string status, string publisherName, string address1, string address2, string city, string province, string postalCode, string country)
		{
			ProvinceTable provinceTable;
			ProvinceBusiness busProvince;
			Regex rexPostalCode;
			bool isValid = true;

			isValid &= IsValid_RequiredField(status, "Status");
			isValid &= IsValid_RequiredField(publisherName, "Publisher Name");
			isValid &= IsValid_RequiredField(address1, "Address Line 1");
			isValid &= IsValid_RequiredField(city, "City");
			isValid &= IsValid_RequiredField(province, "State / Province");
			isValid &= IsValid_RequiredField(postalCode, "Zip / Postal Code");
			isValid &= IsValid_RequiredField(country, "Country");
			
			if(isValid) 
			{
				isValid &= IsValid_FieldLength(publisherName, "Publisher Name", 1, 80);
				isValid &= IsValid_FieldLength(address1, "Address Line 1", 1, 50);
				isValid &= IsValid_FieldLength(address2, "Address Line 2", 0, 50);
				isValid &= IsValid_FieldLength(city, "City", 1, 50);
				isValid &= IsValid_FieldLength(postalCode, "Postal Code", 1, 10);
			}

			if(isValid) 
			{
				provinceTable = new ProvinceTable();
				busProvince = new ProvinceBusiness();

				busProvince.SelectByCountryCode(provinceTable, (CountryCode) Enum.Parse(typeof(CountryCode), country, true));
				provinceTable.DefaultView.RowFilter = "PROVINCE_CODE = '" + province + "'";
				if(provinceTable.DefaultView.Count == 0) 
				{
					messageManager.Add(Message.ERRMSG_STATE_PROVINCE_0);
					isValid = false;
				}

				if(country == "CA") 
				{
					rexPostalCode = new Regex(@"^\D\d\D\s*\d\D\d$");
					if(!rexPostalCode.Match(postalCode).Success) 
					{
						messageManager.Add(Message.ERRMSG_POSTALCODE_INVALID_0);
						isValid = false;
					}
				} 
				else if(country == "US") 
				{
					rexPostalCode = new Regex(@"(^\d{5}$)|(^\d{5}-\d{4}$)");
					if(!rexPostalCode.Match(postalCode).Success) 
					{
						messageManager.Add(Message.ERRMSG_ZIP_INVALID_0);
						isValid = false;
					}
				} 
				else 
				{
					isValid = false;
				}
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