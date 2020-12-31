namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.ProductTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.ProductData;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;


	/// <summary>
	///     This class contains the business rules that are used for a 
	///     collection Day.
	/// </summary>
	public class ProductBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		dataAccessRef dataAccess = new dataAccessRef();
		public ProductBusiness(Message messageManager) : base(messageManager) { }

		public ProductBusiness(bool asMessageManager) : base(asMessageManager) { }

		protected dataAccessRef DataAccess 
		{
			get 
			{
				return dataAccess;
			}
		}

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

		public void SelectAll(DataTable table, string productCode, string remitCode, string productName, int year, string season, int productStatus, ProductType productType, int publisherID, int fulfillmentHouseID) 
		{
			try
			{
				dataAccess.SelectAllProducts(table, CleanString(productCode), CleanString(remitCode), CleanString(productName), year, season, productStatus, Convert.ToInt32(productType), publisherID, fulfillmentHouseID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectOne(DataTable Table,int Product_Instance)
		{
			try
			{
				dataAccess.SelectOne(Table,Product_Instance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectByProductCode(DataTable Table,string Product_Code)
		{
			try
			{
				dataAccess.SelectByProductCode(Table,Product_Code);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public void SelectByProductCode(DataTable Table,string Product_Code, int Year, string Season)
		{
			try
			{
				dataAccess.SelectByProductCode(Table,Product_Code, Year, Season);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectByProductInstance(DataTable table, int productInstance)
		{
			try
			{
				dataAccess.SelectByProductInstance(table, productInstance);
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

		public void SelectByCampaignTitleCode(DataTable Table,string titleCode,int CampaignID,int ProductType,int CustomerInstance)
		{
			try
			{
				dataAccess.SelectByCampaignTitleCode(Table,titleCode,CampaignID,ProductType,CustomerInstance);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectByCampaignTitleCode(DataTable Table,string titleCode,int CampaignID,int ProductType,int CustomerInstance,int CouponSetID)
		{
			try
			{
				dataAccess.SelectByCampaignTitleCode(Table,titleCode,CampaignID,ProductType,CustomerInstance,CouponSetID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectProductByCatalogSectionID(DataTable Table, int ProgramSectionID)
		{
			try
			{
				dataAccess.SelectProductByCatalogSectionID(Table, ProgramSectionID);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public int ProductReplacement(Batch batch, int userID) 
		{
			ConnectionProvider connectionProvider = null;
			BatchData batchDataAccess;
			CustomerOrderHeaderData cohDataAccess;

			bool IsSuccess = true;

			try
			{
				if(ValidateProductReplacement(batch)) 
				{
					connectionProvider = new ConnectionProvider();
					batchDataAccess = new BatchData();
					cohDataAccess = new CustomerOrderHeaderData();

					dataAccess.MainConnectionProvider = connectionProvider;
					batchDataAccess.MainConnectionProvider = connectionProvider;
					cohDataAccess.MainConnectionProvider = connectionProvider;
					connectionProvider.OpenConnection();
					connectionProvider.BeginTransaction("ProductReplacement");

					batch.OrderID = batchDataAccess.CreateBatchForProductReplacement(batch.Campaign.CampaignID, Convert.ToInt32(batch.OrderQualifierID), batch.Comment, userID);

					if(batch.OrderID != 0)
					{
						foreach(OrderHeader orderHeader in batch.OrderHeaders) 
						{
							orderHeader.CustomerOrderHeaderInstance = cohDataAccess.CreateOrderHeaderForProductReplacement(batch.OrderID, orderHeader.TeacherFirstName, orderHeader.TeacherLastName, orderHeader.StudentFirstName, orderHeader.StudentLastName, userID);

							if(orderHeader.CustomerOrderHeaderInstance != 0) 
							{
								foreach(ProductItem item in orderHeader.ProductItems) 
								{
									if(dataAccess.AddNewItemForProductReplacement(orderHeader.CustomerOrderHeaderInstance, item.MagPrice_instance, item.Quantity, item.EnterredPrice, item.PriceOverrideReason, item.ProductReplacementReason) == 0) 
									{
										messageManager.Add(Message.ERRMSG_NO_REC_AFF_VAR_0);
										throw new Exception(Message.ERRMSG_NO_REC_AFF_VAR_0);
									}
								}
							} 
							else 
							{
								messageManager.Add(Message.ERRMSG_NO_REC_AFF_VAR_0);
								throw new Exception(Message.ERRMSG_NO_REC_AFF_VAR_0);
							}
						}

						if(IsSuccess) 
						{
							batchDataAccess.ForceCloseOrder(batch.OrderID);

							connectionProvider.CommitTransaction();
							connectionProvider.CloseConnection(false);
						} 
					} 
					else 
					{
						messageManager.Add(Message.ERRMSG_NO_REC_AFF_VAR_0);
						throw new Exception(Message.ERRMSG_NO_REC_AFF_VAR_0);
					}
				} 
				else 
				{
					IsSuccess = false;
				}
			}
			catch (Exception ex)
			{	
				if(connectionProvider != null && connectionProvider.DBConnection.State != ConnectionState.Closed) 
				{
					connectionProvider.RollbackTransaction("ProductReplacement");
					connectionProvider.CloseConnection(false);
				}

				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.OtherBusinessRules;
				throw new ExceptionFulf(messageManager);
			}

			if(!IsSuccess) 
			{
				messageManager.ValidationExceptionType =  ExceptionType.OtherBusinessRules;
				throw new ExceptionFulf(messageManager);
			}

			return batch.OrderID;
		}

		public bool ValidateProductReplacement(Batch batch) 
		{
			bool isValid = true;
			
			isValid &= (batch.Campaign.CampaignID != 0);

			foreach(OrderHeader orderHeader in batch.OrderHeaders) 
			{
				isValid &= ValidateOrderHeaderForProductReplacement(orderHeader);
			}

			if(batch.Comment.Length > 300) 
			{
				messageManager.ValidationExceptionType  = ExceptionType.MaxLength;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {"Comment", "300"}));
				
				isValid = false;
			}

			if(!isValid) 
				messageManager.PrepareErrorMessage();

			return isValid;
		}

		private bool ValidateOrderHeaderForProductReplacement(OrderHeader orderHeader) 
		{
			bool isValid = true;

			if(orderHeader.TeacherFirstName.Length > 50) 
			{
				messageManager.ValidationExceptionType  = ExceptionType.MaxLength;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {"Teacher First Name", "50"}));

				isValid = false;
			}

			if(orderHeader.TeacherLastName.Length > 50) 
			{
				messageManager.ValidationExceptionType  = ExceptionType.MaxLength;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {"Teacher Last Name", "50"}));

				isValid = false;
			}

			if(orderHeader.StudentFirstName.Length > 50) 
			{
				messageManager.ValidationExceptionType  = ExceptionType.MaxLength;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {"Student First Name", "50"}));

				isValid = false;
			}

			if(orderHeader.StudentLastName.Length > 50) 
			{
				messageManager.ValidationExceptionType  = ExceptionType.MaxLength;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_MAX_LENGTH_VAR_2, new String[] {"Student Last Name", "50"}));

				isValid = false;
			}

			foreach(ProductItem item in orderHeader.ProductItems) 
			{
				isValid &= ValidateItemForProductReplacement(item);
			}

			return isValid;
		}

		private bool ValidateItemForProductReplacement(ProductItem item) 
		{
			bool isValid = true;

			if(item.Catalog_Price != 0 && item.EnterredPrice == 0 && item.PriceOverrideReason == 45004) 
			{
				messageManager.ValidationExceptionType  = ExceptionType.RequiredFields;
				messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1,"Override Reason"));
				isValid = false;
			} 
			else 
			{
				isValid &= (item.MagPrice_instance != 0);

				if(item.Quantity == 0) 
				{
					messageManager.Add(messageManager.FormatErrorMessage(Message.VALMSG_REQUIRED_FIELD_VAR_1, "Quantity"));
					isValid = false;
				}

				isValid &= (item.PriceOverrideReason != 0);
				isValid &= (item.ProductType != 0);
			}

			if(!isValid) 
				messageManager.PrepareErrorMessage();

			return isValid;
		}

		public void SelectPricingDetailsForNewSubToInvoice(DataTable Table, string ProductCode, int NumberOfIssues, int ProgramSectionID, float CatalogPrice)
		{
			try
			{
				dataAccess.SelectPricingDetailsForNewSubToInvoice(Table, ProductCode, NumberOfIssues, ProgramSectionID, CatalogPrice);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void SelectAllProductCategories(DataTable Table) 
		{
			try
			{
				dataAccess.SelectAllProductCategories(Table);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public virtual int Insert(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription, string userID, string vendorProductCode)
		{
			int productInstance = 0;
			
			try
			{
				if(Validate(productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription) && ValidateProductCode(productCode, season, year)) 
				{
                    productInstance = dataAccess.InsertProductInformation(productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription, userID, vendorProductCode);
					if (productInstance == 0)
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
			if (productInstance == 0)
			{
				messageManager.PrepareErrorMessage();
				throw new ValidationException(messageManager);
			}
			return productInstance;
		}

		public virtual bool Update(int productInstance, string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription, string vendorProductCode) 
		{
			bool IsSuccess = false;
			
			try
			{
				if (Validate(productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription)) 
				{
					NbRowAffected = 0;
                    NbRowAffected = dataAccess.UpdateProductInformation(productInstance, productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription, vendorProductCode);
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

		public bool ValidateDelete(int productInstance)
		{
			bool isValid = false;

			if(dataAccess.SelectCustomerOrderDetailCount(productInstance) == 0) 
			{
				isValid = true;
			} 
			else 
			{
				messageManager.Add(Message.ERRMSG_CANNOT_DELETE_PRODUCT_0);
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

			return isValid;
		}

		public bool Delete(int productInstance, ProductType productType)
		{
			bool isSuccess = true;
			ProductContractBusiness productContractBusiness = ProductContractBusinessFactory.Instance.GetProductContractBusiness(productType, new Message(false));
			ConnectionProvider connectionProvider;

			try
			{
				if(ValidateDelete(productInstance))
				{
					connectionProvider = new ConnectionProvider();

					try 
					{
						this.MainConnectionProvider = connectionProvider;
						productContractBusiness.MainConnectionProvider = connectionProvider;

						connectionProvider.OpenConnection();
						connectionProvider.BeginTransaction("DeleteProduct");

						productContractBusiness.DeleteByProductInstance(productInstance);

						NbRowAffected = 0;
						NbRowAffected = dataAccess.Delete(productInstance);
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

						connectionProvider.CommitTransaction();
						connectionProvider.CloseConnection(false);

						this.MainConnectionProvider = null;
						productContractBusiness.MainConnectionProvider = null;
					} 
					catch(Exception ex) 
					{
						if (connectionProvider.DBConnection.State != ConnectionState.Closed) 
						{
							connectionProvider.RollbackTransaction("DeleteProduct");
							connectionProvider.CloseConnection(false);
						}

						if(ex is ExceptionFulf) 
						{
							messageManager.Add(Message.ERRMSG_CANNOT_DELETE_PRODUCT_0);
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

		protected virtual bool Validate(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription) 
		{
			bool isValid = true;

			isValid &= ValidateRequiredFields(productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription);
			isValid &= ValidateFieldLength(productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription);
			isValid &= ValidateCustom(productCode, season, year, productName, productSortName, language, categoryID, status, productType, daysLeadTime, numberOfIssues, publisherID, fulfillmentHouseID, comment, vendorNumber, vendorSiteName, payGroupLookupCode, currency, GSTRegistrationNumber, HSTRegistrationNumber, PSTRegistrationNumber, oracleCode, prizeLevel, prizeLevelQuantityRequired, remitCode, isQSPExclusive, englishDescription, frenchDescription);

			if(!isValid) 
			{
				messageManager.PrepareErrorMessage();
			}

			return isValid;
		}

		protected virtual bool ValidateRequiredFields(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription) 
		{
			bool isValid = true;

			isValid &= IsValid_RequiredField(productCode, "UMC Code");
			isValid &= IsValid_RequiredField(season, "Season");
			isValid &= IsValid_RequiredField(year, "Year");
			isValid &= IsValid_RequiredField(status, "Status");
			isValid &= IsValid_RequiredField(productType, "Product Type");
			isValid &= IsValid_RequiredField(currency, "Currency");

			return isValid;
		}

		protected virtual bool ValidateFieldLength(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription) 
		{
			bool isValid = true;

			isValid &= IsValid_FieldLength(productCode, "UMC Code", 1, 20);
			isValid &= IsValid_FieldLength(productName, "Product Name", 0, 55);
			isValid &= IsValid_FieldLength(productSortName, "Product Sort Name", 0, 55);
			isValid &= IsValid_FieldLength(comment, "Comment", 0, 200);
			isValid &= IsValid_FieldLength(oracleCode, "Oracle Code", 0, 50);
			isValid &= IsValid_FieldLength(prizeLevel, "Level Attained", 0, 10);
			isValid &= IsValid_FieldLength(remitCode, "Remit Code", 0, 20);
			isValid &= IsValid_FieldLength(englishDescription, "English Description", 0, 50);
			isValid &= IsValid_FieldLength(frenchDescription, "French Description", 0, 50);

			return isValid;
		}

		protected virtual bool ValidateCustom(string productCode, string season, int year, string productName, string productSortName, string language, int categoryID, int status, int productType, int daysLeadTime, int numberOfIssues, int publisherID, int fulfillmentHouseID, string comment, string vendorNumber, string vendorSiteName, string payGroupLookupCode, int currency, string GSTRegistrationNumber, string HSTRegistrationNumber, string PSTRegistrationNumber, string oracleCode, string prizeLevel, int prizeLevelQuantityRequired, string remitCode, bool isQSPExclusive, string englishDescription, string frenchDescription) 
		{
			return true;
		}

		protected virtual bool ValidateProductCode(string productCode, string season, int year) 
		{
			bool isValid = true;

			if(!this.dataAccess.ValidateProductCode(productCode, season, year)) 
			{
				messageManager.Add(Message.ERRMSG_PRODUCT_CODE_ALREADY_EXISTS);
				isValid = false;
			}

			if(!isValid) 
			{
				messageManager.PrepareErrorMessage();
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
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_PRODUCT_CODE,"Product_Code");
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
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRODUCT_CODE,"", tableRef.FLD_PRODUCT_CODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRODUCT_SEASON,"", tableRef.FLD_PRODUCT_SEASON_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ALPHA_CODE, "",tableRef.FLD_ALPHA_CODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRODUCT_NAME,"", tableRef.FLD_PRODUCT_NAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRODUCT_SORT_NAME,"", tableRef.FLD_PRODUCT_SORT_NAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_AGES,"", tableRef.FLD_AGES_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_INTERNET,"", tableRef.FLD_INTERNET_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COVERRECEIVED,"", tableRef.FLD_COVERRECEIVED_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_STATUS, "",tableRef.FLD_STATUS_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COMMENT,"", tableRef.FLD_COMMENT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_FULFILL_HOUSE_NBR,"", tableRef.FLD_FULFILL_HOUSE_NBR_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_MAIL_DT,"", tableRef.FLD_MAIL_DT_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ISSUEDATEUSED,"", tableRef.FLD_ISSUEDATEUSED_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_LOGGED_BY,"", tableRef.FLD_LOGGED_BY_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_LANG,"", tableRef.FLD_LANG_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_VENDORNUMBER,"", tableRef.FLD_VENDORNUMBER_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_VENDORSITENAME,"", tableRef.FLD_VENDORSITENAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PAYGROUPLOOKUPCODE,"",tableRef.FLD_PAYGROUPLOOKUPCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_TERMSNAME,"", tableRef.FLD_TERMSNAME_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_COUNTRYCODE,"", tableRef.FLD_COUNTRYCODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_UNITOFMEASURE, "",tableRef.FLD_UNITOFMEASURE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ORACLECODE,"", tableRef.FLD_ORACLECODE_LENGTH);
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_PRIZE_LEVEL,"", tableRef.FLD_PRIZE_LEVEL_LENGTH);
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

		public void SelectByCampaignProductCatalogCode(DataTable Table,string CatalogCode, string titleCode,int CampaignID, int bIsFmAccount)
		{
			try
			{
				dataAccess.SelectByCampaignProductCatalogCode(Table,CatalogCode, titleCode,CampaignID ,bIsFmAccount);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}
		public bool SelectOrderDetailInBatch(Batch batch)
		{
			bool bOk = true;
			DataTable Table = new DataTable();
			try
			{
				int lOrderID = batch.OrderID;
				if(lOrderID > 0)
				{
					DataTable b = new DataTable();
					BatchData bData = new BatchData();
					bData.SelectOneByOrderID(b, lOrderID);

					DataRow bRow =b.Rows[0]; 
					batch.Date = bRow[BatchTable.FLD_DATE].ToString();
					batch.ID = Convert.ToInt32(bRow[BatchTable.FLD_ID].ToString());
					batch.Status = Convert.ToInt32(bRow[BatchTable.FLD_STATUSINSTANCE].ToString());

					batch.OrderQualifierID = (OrderQualifier) Convert.ToInt32(bRow[BatchTable.FLD_ORDERQUALIFIERID].ToString());
					
					if (bRow[BatchTable.FLD_ORDERDELIVERYDATE].ToString() != "")
						batch.OrderDeliveryDate = Convert.ToDateTime(bRow[BatchTable.FLD_ORDERDELIVERYDATE].ToString());

					batch.Campaign = new Campaign();
					batch.Campaign.CampaignID = Convert.ToInt32(bRow[BatchTable.FLD_CAMPAIGNID].ToString());
					batch.Campaign.AccountID= Convert.ToInt32(bRow[BatchTable.FLD_ACCOUNTID].ToString());
					
					DataTable c = new DataTable();
					CampaignData cData = new CampaignData();
					cData.SelectOne(c, batch.Campaign.CampaignID);
					batch.Campaign.EstimatedGrossSales = Convert.ToDouble(c.Rows[0][CampaignTable.FLD_ESTIMATEDGROSS].ToString());
										
					DataTable a = new DataTable();
					AccountData aData = new AccountData();
					aData.SelectFieldManager(a, batch.Campaign.CampaignID);
					DataRow aRow =a.Rows[0]; 
					batch.Campaign.FMID = aRow[CampaignTable.FLD_FMID].ToString();

					//batch.Campaign.IsFMAccount = (batch.Campaign.FMID != null && batch.Campaign.FMID != '9999');

					// grab the coh's for this order
					CustomerOrderHeaderData cohData = new CustomerOrderHeaderData();
					cohData.SelectAllByBatch(Table, batch.Date, batch.ID);

					foreach(DataRow row in Table.Rows) 
					{

						OrderHeader h = new OrderHeader();
						h.CustomerOrderHeaderInstance = Convert.ToInt32(row[CustomerOrderHeaderTable.FLD_INSTANCE].ToString());
						h.CustomerBillToInstance = Convert.ToInt32(row[CustomerOrderHeaderTable.FLD_CUSTOMERBILLTOINSTANCE].ToString());
						batch.OrderHeaders.Add(h);	
						DataTable codDataTable = new DataTable();
						//this.ddlCategory.Items.Add(new ListItem(row[CodeDetailTable.FLD_DESCRIPTION].ToString(), row[CodeDetailTable.FLD_INSTANCE].ToString()));
						CustomerOrderDetailData codData = new CustomerOrderDetailData();
						codData.SelectAllByCustomerOrderHeaderInstance(codDataTable,
									Convert.ToInt32(row[CustomerOrderHeaderTable.FLD_INSTANCE].ToString()),
									0);

						foreach(DataRow codRow in codDataTable.Rows)
						{
							//Create a ProductItem and add it to the OrderHeader in the Batch object
							ProductItem i = new ProductItem();
							i.TransID = Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_TRANSID].ToString());
							i.MagPrice_instance=Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_PRICINGDETAILSID].ToString());
							i.Product_code=codRow[CustomerOrderDetailTable.FLD_PRODUCTCODE].ToString();
							i.Product_sort_name=codRow[CustomerOrderDetailTable.FLD_PRODUCTNAME].ToString();
							i.Quantity=Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_QUANTITY].ToString());
							i.Catalog_Price=(float)Convert.ToDouble(codRow[CustomerOrderDetailTable.FLD_CATALOGPRICE].ToString());
							i.Catalog_Name=codRow["CatalogName"].ToString();
							i.StatusInstance= Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_STATUSINSTANCE].ToString());
							i.Recipient= codRow[CustomerOrderDetailTable.FLD_RECIPIENT].ToString();
							i.ProductType= Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_PRODUCTTYPE].ToString());
							i.PriceOverrideReason = Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_PRICEOVERRIDEID].ToString());
							i.CustomerShipToInstance = Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_CUSTOMERSHIPTOINSTANCE].ToString());
							DataTable overrideReasonTable = new DataTable("OverrideReasons");
							CodeDetailBusiness codeDetailBusiness = new CodeDetailBusiness(new Message(false));
							codeDetailBusiness.SelectOne(overrideReasonTable, i.PriceOverrideReason);

							i.EnterredPrice = (float)(Convert.ToDouble(codRow[CustomerOrderDetailTable.FLD_PRICE].ToString()) / i.Quantity);
							i.IsDeleted = Convert.ToInt32(codRow[CustomerOrderDetailTable.FLD_DELFLAG]);
							/*
							DataTable pid = new DataTable();
							ProductContractData pcd = new ProductContractData();
							pcd.SelectOneSingle(pid, i.MagPrice_instance);
							DataRow a = pid.Rows[0];
							i.Catalog_Name=a["CatalogName"].ToString();
							*/			
							batch.OrderHeaders[0].ProductItems.Add(i);
						}
					}
				
				}

			}
			catch (Exception ex)
			{	
				
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.OtherBusinessRules;
				throw new ExceptionFulf(messageManager);
			}
			return bOk;
		}

	}
}