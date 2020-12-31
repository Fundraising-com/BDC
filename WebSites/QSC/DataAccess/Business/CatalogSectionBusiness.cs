namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using System.Text.RegularExpressions;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	using QSPFulfillment.DataAccess.Data;
	//using tableRef = QSPFulfillment.DataAccess.Common.TableDef.ProductTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CatalogSectionData;
	using QSPFulfillment.DataAccess.Common;

	public enum CatalogSectionType
	{
		None			= 0,
		Inventory		= 1,
		Magazine		= 2,
		FieldSupplies	= 3,
		Incentives		= 4,
		Misc			= 5,
		InventoryNoTax	= 6,
		Prizes			= 7,
        CookieDough     = 9,
        Popcorn       = 10,
        Jewelry         = 11,
        Shipping        = 12,
        Candles         = 13,
        ToRememberThis  = 14,
        Entertainment   = 15,
        GiftCard        = 16,
        PretzelRods2     = 17,
        PretzelRods3     = 18
	}

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CatalogSectionBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public CatalogSectionBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CatalogSectionBusiness(bool AsMessageManager):base(AsMessageManager)
		{
		
		}

		public void SelectSearch(DataTable table, int catalogID) 
		{
			try
			{
				dataAccess.SelectSearch(table, catalogID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public int Insert(int catalogID, string catalogCode, CatalogSectionType type, string name, int fsProgramID, string userID) 
		{
			int newCatalogSectionID = 0;
			
			try
			{
				if (Validate(catalogCode, type, name, fsProgramID, userID)) 
				{
					newCatalogSectionID = dataAccess.Insert(catalogID, catalogCode, Convert.ToInt32(type), name, fsProgramID, userID);
					if (newCatalogSectionID == 0)
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
			if ( newCatalogSectionID == 0 )
			{
				throw new ValidationException(messageManager);
			}
			return newCatalogSectionID;
		}

		public bool Update(int catalogSectionID, string catalogCode, CatalogSectionType type, string name, int fsProgramID, string userID) 
		{
			bool isSuccess = false;
			
			try
			{
				if (Validate(catalogCode, type, name, fsProgramID, userID)) 
				{
					NbRowAffected = 0;
					NbRowAffected = dataAccess.Update(catalogSectionID, catalogCode, Convert.ToInt32(type), name, fsProgramID, userID);
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
			if ( !isSuccess )
			{
				throw new ValidationException(messageManager);
			}
			return isSuccess;
		}

		public bool ValidateDelete(int catalogSectionID)
		{
			bool isValid = false;

			if(dataAccess.SelectCustomerOrderDetailCount(catalogSectionID) == 0) 
			{
				isValid = true;
			} 
			else 
			{
				messageManager.Add(Message.ERRMSG_CANNOT_DELETE_SECTION_0);
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

			return isValid;
		}

		public bool Delete(int catalogSectionID)
		{
			bool isSuccess = true;
			ProductContractAllBusiness productContractAllBusiness = new ProductContractAllBusiness(messageManager);
			ConnectionProvider connectionProvider = null;

			try
			{
				if(ValidateDelete(catalogSectionID))
				{
					// Do not create a new connection provider if there is already
					// an external transaction
					if(this.MainConnectionProvider == null) 
					{
						connectionProvider = new ConnectionProvider();
					}
			
					try 
					{
						// Only open the transaction if there is no external one
						if(connectionProvider != null) 
						{
							this.MainConnectionProvider = connectionProvider;
							productContractAllBusiness.MainConnectionProvider = connectionProvider;

							connectionProvider.OpenConnection();
							connectionProvider.BeginTransaction("DeleteCatalogSection");
						} 
						else 
						{
							productContractAllBusiness.MainConnectionProvider = this.MainConnectionProvider;
						}

						productContractAllBusiness.DeleteByCatalogSectionID(catalogSectionID);

						NbRowAffected = 0;
						NbRowAffected = dataAccess.Delete(catalogSectionID);
						if (NbRowAffected != 0)
						{
							isSuccess = true; 
						}
						else
						{
							messageManager.ValidationExceptionType  = ExceptionType.OtherBusinessRules;
							messageManager.SetErrorMessage(Message.ERRMSG_NO_REC_AFF_VAR_0);
							isSuccess = false;
							throw new ValidationException(messageManager);
						}

						// Only commit if the transaction is local
						if(connectionProvider != null) 
						{
							connectionProvider.CommitTransaction();
							connectionProvider.CloseConnection(false);

							this.MainConnectionProvider = null;
							productContractAllBusiness.MainConnectionProvider = null;
						}
					} 
					catch(Exception ex) 
					{
						// Only rollback if the transaction is local
						if(connectionProvider!= null && connectionProvider.DBConnection.State != ConnectionState.Closed) 
						{
							connectionProvider.RollbackTransaction("DeleteCatalogSection");
							connectionProvider.CloseConnection(false);
						}

						if(ex is ExceptionFulf) 
						{
							messageManager.Add(Message.ERRMSG_CANNOT_DELETE_SECTION_0);
							messageManager.PrepareErrorMessage();
							throw new ExceptionFulf(messageManager);
						} 
						else 
						{
							throw ex;
						}
					}
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

		public void SelectAllCatalogSectionTypes(DataTable table) 
		{
			try
			{
				dataAccess.SelectAllCatalogSectionTypes(table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public ProductType SelectDefaultProductTypeByCatalogSection(int catalogSectionID)
		{
			try
			{
				return (ProductType) dataAccess.SelectDefaultProductTypeByCatalogSection(catalogSectionID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		private bool Validate(string catalogCode, CatalogSectionType type, string name, int fsProgramID, string userID) 
		{
			bool isValid = true;

			if(type == CatalogSectionType.None) 
			{
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, "Catalog Section Type"));
				isValid = false;
			}

			isValid &= IsValid_RequiredField(name, "Catalog Section Name");
			
			if(isValid) 
			{
				isValid &= IsValid_FieldLength(name, "Catalog Section Name", 1, 50);
			}

			if(isValid) 
			{
				if(type == CatalogSectionType.FieldSupplies && fsProgramID == 0) 
				{
					messageManager.Add(Message.ERRMSG_FIELD_SUPPLIES_WITHOUT_PROGRAM_0);
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