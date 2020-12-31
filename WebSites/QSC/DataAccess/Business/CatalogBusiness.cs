namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using System.Text.RegularExpressions;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	//using tableRef = QSPFulfillment.DataAccess.Common.TableDef.ProductTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.CatalogData;
	using QSPFulfillment.DataAccess.Common;

	public enum CatalogType
	{
		None					= 0,
		Fundraising				= 30301,
		Incentives				= 30302,
		Rewards					= 30303,
		Promotional				= 30304,
		Dollars20Certificate	= 30305,
        MagazineRegular         = 30306,
        MagazineFaculty         = 30307,
        CookieDough             = 30308,
        TaylorsTotes            = 30309,
        Prizes                  = 30310,
        PrizesFMBulk            = 30311,
        Gift                    = 30312,
        GiftFMBulk              = 30313,
        FieldSupplies           = 30314,
        FieldSuppliesFMBulk     = 30315,
        Candles                 = 30316,
        ToRememberThis          = 30317,
        ToRememberThisFaculty   = 30318,
        ToRememberThisFMBulk    = 30319,
        Entertainment           = 30320,
        EntertainmentFMBulk     = 30321,
        EntertainmentFaculty    = 30322,
        DreamBig                = 30323,
        Festival                = 30324,
        OrganicEdibles          = 30325,
        KitchenCollection       = 30326,
        Donations               = 30327,
        NaturallyGood           = 30328,
        EnjoySomethingSweet     = 30329,
        Top20Magazines          = 30330,
        Popcorn                 = 30331,
        StainlessSteelTravelCup = 30332,
        DepositOnlyExtraService = 30333,
        QSPSavingsPass          = 30334,
        GiftCard                = 30335,
        PapaJackPopcorn         = 30336,
        Tervis                  = 30337,
        PretzelRods2            = 30338,
        TheCureJewelry          = 30339,
        GourmetTastyTreats      = 30340,
        PretzelRods3            = 30341,
        LeapLabels              = 30342,
        LeapLabelsFaculty       = 30343,
        QSPSavingsPassFaculty   = 30344,
        CoolCards               = 30345,
        Rally                   = 30346
   }

   public enum CatalogStatus
	{
		None					= 0,
		Inactive				= 30401,
		Pending					= 30402,
		Approved				= 30403,
		InUse					= 30404
	}

	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class CatalogBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public CatalogBusiness(Message MessageManager):base(MessageManager)
		{
		
		}
		public CatalogBusiness(bool AsMessageManager):base(AsMessageManager)
		{
		
		}

		public void SelectSearch(DataTable table, string code, string name, int year, string season, CatalogType type, string language, CatalogStatus status, int campaignID, string productCode)
		{
			try
			{
				dataAccess.SelectSearch(table, CleanString(code), CleanString(name), year, season, Convert.ToInt32(type), language, Convert.ToInt32(status), campaignID, CleanString(productCode));
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllCatalogFinancialYears(DataTable Table)
		{
			try
			{
				dataAccess.SelectAllCatalogFinancialYears(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllCatalogSeasons(DataTable Table)
		{
			try
			{
				dataAccess.SelectAllCatalogSeasons(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllCatalogTypes(DataTable Table)
		{
			try
			{
				dataAccess.SelectAllCatalogTypes(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllCatalogStatus(DataTable Table)
		{
			try
			{
				dataAccess.SelectAllCatalogStatus(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllPremiums(DataTable Table) 
		{
			try
			{
				dataAccess.SelectAllPremiums(Table, 0, "");
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllPremiums(DataTable Table, int Year, string Season) 
		{
			try
			{
				dataAccess.SelectAllPremiums(Table, Year, Season);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllPayGroupLookUpCodes(DataTable Table) 
		{
			try
			{
				dataAccess.SelectAllPayGroupLookUpCodes(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllVendorSiteNames(DataTable Table) 
		{
			try
			{
				dataAccess.SelectAllVendorSiteNames(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllPhones(DataTable Table, int PhoneListID)
		{
			try
			{
				dataAccess.SelectAllPhones(Table, PhoneListID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllPhones(DataTable Table)
		{
			try
			{
				dataAccess.SelectAllPhones(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectSeason(DataTable Table, int Year, string Season) 
		{
			try
			{
				dataAccess.SelectSeason(Table, Year, Season);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public double SelectGSTTaxRate() 
		{
			try
			{
				return dataAccess.SelectGSTTaxRate();
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public double SelectHSTTaxRate() 
		{
			try
			{
				return dataAccess.SelectHSTTaxRate();
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public int InsertCatalogInformations(string CatalogCode, string CatalogName, int CatalogType, string Language, int Year, string Season, int CatalogStatus, string IsReplacement, string UserID) 
		{
			int NewCatalogID = 0;
			
			try
			{
				if (ValidateCatalogInformations(0, CatalogCode, CatalogName, CatalogType, Language, Year, Season, CatalogStatus, IsReplacement, UserID)) 
				{
					NewCatalogID = dataAccess.InsertCatalogInformations(CatalogCode, CatalogName, CatalogType, Language, Year, Season, CatalogStatus, IsReplacement, UserID);

					if (NewCatalogID == 0)
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
			if ( NewCatalogID == 0 )
			{
				throw new ValidationException(messageManager);
			}
			return NewCatalogID;
		}

		public int InsertProductCategory(string Description) 
		{
			int NewCategoryID = 0;
			
			try
			{
				if (ValidateProductCategory(Description)) 
				{
					NewCategoryID = dataAccess.InsertProductCategory(Description);
					if (NewCategoryID == 0)
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
			if ( NewCategoryID == 0 )
			{
				throw new ValidationException(messageManager);
			}
			return NewCategoryID;
		}

		public int InsertPremiumInformations(string PremiumCode, int Year, string Season, bool IsActive, string EnglishDescription, string FrenchDescription, string UserID) 
		{
			int NewPremiumID = 0;
			
			try
			{
				if (ValidatePremiumInformations(PremiumCode, Year, Season, IsActive, EnglishDescription, FrenchDescription, UserID)) 
				{
					NewPremiumID = dataAccess.InsertPremiumInformations(PremiumCode, Year, Season, IsActive ? 1 : 0, EnglishDescription, FrenchDescription, UserID);
					if (NewPremiumID == 0)
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
			if ( NewPremiumID == 0 )
			{
				throw new ValidationException(messageManager);
			}
			return NewPremiumID;
		}

		public int InsertPhoneInformations(int Type, int PhoneListID, string PhoneNumber, string BestTimeToCall) 
		{
			int NewPhoneID = 0;
			
			try
			{
				if (ValidatePhoneInformations(Type, PhoneNumber, BestTimeToCall)) 
				{
					NewPhoneID = dataAccess.InsertPhoneInformations(Type, PhoneListID, PhoneNumber, BestTimeToCall);
					if (NewPhoneID == 0)
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
			if ( NewPhoneID == 0 )
			{
				throw new ValidationException(messageManager);
			}
			return NewPhoneID;
		}

		public bool UpdateCatalogInformations(int CatalogID, string CatalogCode, string CatalogName, int CatalogType, string Language, int Year, string Season, int CatalogStatus, string IsReplacement, string UserID) 
		{
			bool IsSuccess = false;
			
			try
			{
				if (ValidateCatalogInformations(CatalogID, CatalogCode, CatalogName, CatalogType, Language, Year, Season, CatalogStatus, IsReplacement, UserID)) 
				{
					NbRowAffected = 0;
					NbRowAffected = dataAccess.UpdateCatalogInformations(CatalogID, CatalogCode, CatalogName, CatalogType, Language, Year, Season, CatalogStatus, IsReplacement, UserID);
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

		public bool UpdateProductCategory(int CategoryID, string Description) 
		{
			bool IsSuccess = false;
			
			try
			{
				if (ValidateProductCategory(Description)) 
				{
					NbRowAffected = 0;
					NbRowAffected = dataAccess.UpdateProductCategory(CategoryID, Description);
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

		public bool UpdatePremiumInformations(int PremiumID, string PremiumCode, int Year, string Season, bool IsActive, string EnglishDescription, string FrenchDescription, string UserID) 
		{
			bool IsSuccess = false;
			
			try
			{
				if (ValidatePremiumInformations(PremiumCode, Year, Season, IsActive, EnglishDescription, FrenchDescription, UserID)) 
				{
					NbRowAffected = 0;
					NbRowAffected = dataAccess.UpdatePremiumInformations(PremiumID, PremiumCode, Year, Season, IsActive ? 1 : 0, EnglishDescription, FrenchDescription, UserID);
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

		public bool UpdatePhoneInformations(int PhoneID, int Type, string PhoneNumber, string BestTimeToCall) 
		{
			bool IsSuccess = false;
			
			try
			{
				if (ValidatePhoneInformations(Type, PhoneNumber, BestTimeToCall)) 
				{
					NbRowAffected = 0;
					NbRowAffected = dataAccess.UpdatePhoneInformations(PhoneID, Type, PhoneNumber, BestTimeToCall);
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

		public bool DeletePhone(int PhoneID) 
		{
			bool IsSuccess = false;
			
			try
			{
				
				NbRowAffected = 0;
				NbRowAffected = dataAccess.DeletePhone(PhoneID);
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

		private bool ValidateCatalogInformations(int CatalogID, string CatalogCode, string CatalogName, int CatalogType, string Language, int Year, string Season, int CatalogStatus, string IsReplacement, string UserID) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(CatalogCode, "Catalog Code");
			isValid &= IsValid_RequiredField(CatalogName, "Catalog Name");
			isValid &= IsValid_RequiredField(CatalogType, "Type");
			isValid &= IsValid_RequiredField(Language, "Language");
			isValid &= IsValid_RequiredField(Year, "Year");
			isValid &= IsValid_RequiredField(Season, "Season");
			isValid &= IsValid_RequiredField(CatalogStatus, "Status");
			
			if(isValid) 
			{
				isValid &= IsValid_FieldLength(CatalogCode, "Catalog Code", 5, 50);
				isValid &= IsValid_FieldLength(CatalogName, "Catalog Name", 1, 50);
			}

			if(isValid)
			{
				isValid &= ValidateCatalogUnicity(CatalogID, CatalogCode);
			}

			if(!isValid)
				messageManager.PrepareErrorMessage();

			return isValid;
		}

		//Validate catalog uniqueness: check if current catalog code already exists in the system
		private bool ValidateCatalogUnicity(int catalogID, string catalogCode)
		{
			try
			{
				if(dataAccess.SelectCatalogCount(catalogID, catalogCode).Equals(0))
				{
					return true;
				}
				else
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_UNICITY_VAR_2, new string [2] {"Catalog", "Catalog Code"}));
					messageManager.ValidationExceptionType = ExceptionType.Unicity;
					return false;
				}
			}
			catch(Exception EX)
			{
				ManageError(EX);
				return false;
			}
		}

		private bool ValidateProductCategory(string Description) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(Description, "Description");
			
			if(isValid) 
			{
				isValid &= IsValid_FieldLength(Description, "Description", 1, 64);
			}

			if(!isValid)
				messageManager.PrepareErrorMessage();

			return isValid;
		}

		private bool ValidatePremiumInformations(string PremiumCode, int Year, string Season, bool IsActive, string EnglishDescription, string FrenchDescription, string UserID) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(PremiumCode, "Premium Code");
			isValid &= IsValid_RequiredField(Year, "Year");
			isValid &= IsValid_RequiredField(Season, "Season");
			
			if(isValid) 
			{
				isValid &= IsValid_FieldLength(PremiumCode, "Premium Code", 1, 20);
				isValid &= IsValid_FieldLength(EnglishDescription, "English Description", 0, 100);
				isValid &= IsValid_FieldLength(FrenchDescription, "French Description", 0, 100);
			}

			if(!isValid)
				messageManager.PrepareErrorMessage();

			return isValid;
		}

		private bool ValidatePhoneInformations(int Type, string PhoneNumber, string BestTimeToCall) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(PhoneNumber, "Phone Number");
			isValid &= IsValid_RequiredField(Type, "Phone Type");
			
			if(isValid) 
			{
				isValid &= IsValid_FieldLength(PhoneNumber, "Phone Number", 1, 50);
				isValid &= IsValid_FieldLength(BestTimeToCall, "Best Time To Call", 0, 2000);
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

		//method to validate the deletion request
		public bool ValidateDelete(int catalogID)
		{
			bool isValid = false;

			if(dataAccess.SelectCustomerOrderDetailCount(catalogID) == 0) 
			{
				isValid = true;
			} 
			else 
			{
				messageManager.Add(Message.ERRMSG_CANNOT_DELETE_CATALOG_0);
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

			return isValid;
		}

		//delete catalog by catalogID
		public bool Delete(int catalogID)
		{
			bool isSuccess = true;

			CatalogSectionBusiness catalogSectionBusiness = new CatalogSectionBusiness(messageManager);
			ConnectionProvider connectionProvider = null;

			DataTable catalogSectionsTable = new DataTable("CatalogSections");

			try
			{
				//validate "Delete" action again, in case orders were already placed
				if(ValidateDelete(catalogID))
				{	
					connectionProvider = new ConnectionProvider();

					try 
					{
						this.MainConnectionProvider = connectionProvider;
						catalogSectionBusiness.MainConnectionProvider = connectionProvider;

						connectionProvider.OpenConnection();
						connectionProvider.BeginTransaction("DeleteCatalog");
						
						//get all sections for the current catalogID
						catalogSectionBusiness.SelectSearch(catalogSectionsTable, catalogID); 

						//for each section in the catalog
						foreach(DataRow catalogSection in catalogSectionsTable.Rows)
						{
							//use CatalogSectionBusiness.Delete() method to delete the section and its contracts.
							//all exceptions are handled in that method
							catalogSectionBusiness.Delete(Convert.ToInt32(catalogSection["ID"]));
						}

						/************* delete the catalog itself ***************/
						NbRowAffected = 0;
						NbRowAffected = dataAccess.DeleteCatalog(catalogID);
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
						/*******************************************************/

						connectionProvider.CommitTransaction();
						connectionProvider.CloseConnection(false);

						this.MainConnectionProvider = null;
						catalogSectionBusiness.MainConnectionProvider = null;
					} 
					catch(Exception ex) 
					{
						// Rollback if exception
						if(connectionProvider!= null && connectionProvider.DBConnection.State != ConnectionState.Closed) 
						{
							connectionProvider.RollbackTransaction("DeleteCatalog");
							connectionProvider.CloseConnection(false);
						}

						if(ex is ExceptionFulf) 
						{
							messageManager.Add(Message.ERRMSG_CANNOT_DELETE_CATALOG_0);
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

		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}

	}
}