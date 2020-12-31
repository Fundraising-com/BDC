namespace QSPFulfillment.DataAccess.Business
{
	using System;
	using System.Data;
	using QSPFulfillment.DataAccess.Common.TableDef;
	using QSPFulfillment.DataAccess.Data;
	using tableRef = QSPFulfillment.DataAccess.Common.TableDef.CampaignProgramTable;
	using dataAccessRef =QSPFulfillment.DataAccess.Data.KanataOEData;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.DataAccess.Common.ActionObject;
	
	public class KanataOEBusiness : QSPFulfillment.DataAccess.Business.BusinessSystem
	{
		private const int ORDER_PRODUCT_QUANTITY_THRESHOLD = 9999999; //over 50 items per order requires approval

		dataAccessRef dataAccess = new dataAccessRef();
	
		public KanataOEBusiness(Message MessageManager):base(MessageManager)
		{
	
		}
		/*public KanataOEBusiness(bool AsMessageManager):base(AsMessageManager)
		{
	
		}*/

		public bool Delete(tableRef Table)
		{
			return this.Delete(Table,dataAccess);
		}
		public bool Insert(tableRef Table)
		{
			return this.Insert(Table,dataAccess);	
		}
		public bool UpdateBatch(tableRef Table)
		{			
			return this.UpdateBatch(Table,dataAccess);
		}
		public bool Update(tableRef Table)
		{
			return this.Update(Table, dataAccess);
		}


		public void ListCatalogForKanataOrder(DataTable Table,int CampaignID, int IsFMAcc)
		{
			try
			{
				dataAccess.SelectAllCatalogForKanata(Table, CampaignID, IsFMAcc);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		public void KanataSelectItemsByCatalogAndAccountType(DataTable Table,string CatalogCode, string titleCode,int CampaignID, int bIsFmAccount)
		{
			try
			{
				dataAccess.KanataSelectItemsByCatalogAndAccountType(Table,CatalogCode, titleCode,CampaignID ,bIsFmAccount);
			}
			catch (Exception ex)
			{	
				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.Select;
				throw new ExceptionFulf(messageManager);
			}
		}

		//Kanata Order Entry
		public int KanataOrderEntry(Batch batch, string billTo, string shipto ,string Fname, string Lname, string email, string address1, string address2, string county, string city, string stateProvince, string postal, string postal2, string country, int userID, bool bSaveOnly ) 
		{
			ConnectionProvider connectionProvider = null;
			BatchData batchDataAccess;
			CustomerOrderHeaderData cohDataAccess;

			bool IsSuccess = true;
			int retVal = 0;

			try
			{
				if (ValidateOrderQualifier(batch))
				{
					//WFC Bonus should be forced to Campaign #31936
					int modifiedCampaignID = batch.Campaign.CampaignID;
					if (batch.OrderQualifierID == OrderQualifier.WFCSigningBonus)
					{
						modifiedCampaignID = 31936;
						billTo = "OTHER";
						shipto = "OTHER";
					}

					ProductBusiness productBusiness = new ProductBusiness(new Message(true));
					if(productBusiness.ValidateProductReplacement(batch)) 
					{
						connectionProvider = new ConnectionProvider();
						batchDataAccess = new BatchData();
						cohDataAccess = new CustomerOrderHeaderData();

						dataAccess.MainConnectionProvider = connectionProvider;
						batchDataAccess.MainConnectionProvider = connectionProvider;
						cohDataAccess.MainConnectionProvider = connectionProvider;
						connectionProvider.OpenConnection();
						connectionProvider.BeginTransaction("KanataOrderEntry");

                        UpdateItemPriceForDeductingGroupProfit(batch);

						// Start a new batch if need be
						if(batch.OrderID == 0)
							batch.OrderID = batchDataAccess.CreateBatchForKanataOrder(modifiedCampaignID, batch.Campaign.IsFMAccount, Convert.ToInt32(batch.OrderQualifierID), batch.Comment, batch.OrderDeliveryDate, userID);
						else
							batchDataAccess.UpdateBatchForKanataOrder(batch.OrderID, Convert.ToInt32(batch.OrderQualifierID), batch.OrderDeliveryDate);

						retVal = batch.OrderID;

						if(batch.OrderID != 0)
						{
							foreach(OrderHeader orderHeader in batch.OrderHeaders) 
							{
								//Create Bill To Customer if it doesn't exist
								int billToInstance = 0;
								if (orderHeader.CustomerOrderHeaderInstance == 0)
								{
									CustomerData customerBillToData = new CustomerData();
									
									if (billTo == "FM")
										billToInstance = customerBillToData.CreateCustomerFM(batch.Campaign.FMID, Convert.ToString(userID), 54004);
									else if (billTo == "OTHER") //just for WFC Bonus orders
										billToInstance = customerBillToData.CreateCustomer(Fname, Lname, email, address1, address2, county, city, stateProvince, postal, postal2, Convert.ToString(userID));
									else
										billToInstance = customerBillToData.CreateCustomerAccount(batch.Campaign.AccountID, Convert.ToString(userID));
								}

								//create order header if it doesn't exist
								if(orderHeader.CustomerOrderHeaderInstance == 0)
									orderHeader.CustomerOrderHeaderInstance = cohDataAccess.CreateOrderHeaderForKanataOrder(batch.OrderID, billToInstance, userID);
								else
									cohDataAccess.UpdateOrderHeaderForKanataOrder(orderHeader.CustomerOrderHeaderInstance, billTo, Fname, Lname, email,  address1,  address2,  city,  stateProvince,  postal, postal2, userID);

								//if Shipping to somewhere other than billing address, create Customer Instance
								int shipToInstance;
								if (billTo != shipto)
								{
									CustomerData customerShipToData = new CustomerData();
									
									if (shipto == "FM")
										shipToInstance = customerShipToData.CreateCustomerFM(batch.Campaign.FMID, Convert.ToString(userID), 54004);
									else if (shipto == "SCHOOL")
										shipToInstance = customerShipToData.CreateCustomerAccount(batch.Campaign.AccountID, Convert.ToString(userID));
									else
										shipToInstance = customerShipToData.CreateCustomer(Fname, Lname, email, address1, address2, county, city, stateProvince, postal, postal2, Convert.ToString(userID));
								}
								else
									shipToInstance = 0;

								if(orderHeader.CustomerOrderHeaderInstance != 0) 
								{
									foreach(ProductItem item in orderHeader.ProductItems) 
									{
										// add new item
										if(item.TransID <= 0)
										{
											if(dataAccess.AddNewItemForKanataOrder(orderHeader.CustomerOrderHeaderInstance, item.MagPrice_instance, shipToInstance, Fname, Lname, item.Quantity, item.EnterredPrice, item.PriceOverrideReason) == 0) 
											{
												messageManager.Add(Message.ERRMSG_NO_REC_AFF_VAR_0);
												throw new Exception(Message.ERRMSG_NO_REC_AFF_VAR_0);
											}
										}
										else // update/delete item
										{
											int Deleted = 0;
											if (item.IsDeleted == 1)
												Deleted = 1;

											ProductData productData = new ProductData();
											if( productData.UpdateItem(orderHeader.CustomerOrderHeaderInstance,
												item.TransID, shipToInstance, Fname + " " + Lname,item.Quantity,item.EnterredPrice,item.MagPrice_instance,item.PriceOverrideReason,
												item.StatusInstance, Deleted) == 0)
											{
												messageManager.Add(Message.ERRMSG_NO_REC_AFF_VAR_0);
												throw new Exception(Message.ERRMSG_NO_REC_AFF_VAR_0);
											}
										}
									}
								} 
								else 
								{
									messageManager.Add(Message.ERRMSG_NO_REC_AFF_VAR_0);
									throw new Exception(Message.ERRMSG_NO_REC_AFF_VAR_0);
								}
							}

							if (IsSuccess)
							{
								if(bSaveOnly==false)
								{
									bool orderValid = ValidateOrderQuantity(batch);
																	
									if (orderValid) 
									{
										batchDataAccess.ForceCloseOrder(batch.OrderID);
									}
									else
									{
										retVal = 1; //order over threshold
										batchDataAccess.BatchUpdateStatus(batch.OrderID, 40016);
									}
								}
							}

							connectionProvider.CommitTransaction();
							connectionProvider.CloseConnection(false);
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
				else
				{
					IsSuccess = false;
					messageManager.ValidationExceptionType  = ExceptionType.Integrity;
					messageManager.Add(Message.ERRMSG_INVALID_ORDER_QUALIFIER);
				}
			}
			catch (Exception ex)
			{	
				if(connectionProvider != null && connectionProvider.DBConnection.State != ConnectionState.Closed) 
				{
					connectionProvider.RollbackTransaction("KanataOrderEntry");
					connectionProvider.CloseConnection(false);
				}

				ManageError(ex);
				messageManager.ValidationExceptionType =  ExceptionType.OtherBusinessRules;
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

			if(!IsSuccess) 
			{
				messageManager.ValidationExceptionType =  ExceptionType.OtherBusinessRules;
				messageManager.PrepareErrorMessage();
				throw new ExceptionFulf(messageManager);
			}

			return retVal;
		}

        private static void UpdateItemPriceForDeductingGroupProfit(Batch batch)
        {
            //if (batch.OrderQualifierID != OrderQualifier.FMBulkSupply)
            //{
                /*int chocolateTotalQuantity = GetChocolateQuantity(batch);

                if (chocolateTotalQuantity >= 16)
                {
                    foreach (OrderHeader orderHeader in batch.OrderHeaders)
                    {
                        foreach (ProductItem item in orderHeader.ProductItems)
                        {
                            if (item.ProductType == (int)ProductType.Chocolate)
                            {
                                item.Catalog_Price = 120;
                                item.Price = 120;
                                item.EnterredPrice = 120;
                            }
                        }
                    }
                }
                else if (chocolateTotalQuantity >= 8 && chocolateTotalQuantity < 16)
                {
                    foreach (OrderHeader orderHeader in batch.OrderHeaders)
                    {
                        foreach (ProductItem item in orderHeader.ProductItems)
                        {
                            if (item.ProductType == (int)ProductType.Chocolate)
                            {
                                item.Catalog_Price = 132;
                                item.Price = 132;
                                item.EnterredPrice = 132;
                            }
                        }
                    }
                }*/
            //}
        }

        /*private static int GetChocolateQuantity(Batch batch)
        {
            int chocolateTotalQuantity = 0;
            foreach (OrderHeader orderHeader in batch.OrderHeaders)
            {
                foreach (ProductItem item in orderHeader.ProductItems)
                {
                    if (item.ProductType == (int)ProductType.Chocolate)
                        chocolateTotalQuantity += item.Quantity;
                }
            }
            return chocolateTotalQuantity;
        }*/

        public bool ValidateOrderQuantity(Batch batch)
		{
			return (batch.OrderHeaders.GetTotalQuantityofProducts() < ORDER_PRODUCT_QUANTITY_THRESHOLD);
		}

		public bool ValidateOrderQualifier(Batch batch)
		{
			bool isValid = true;

			//If any items are $0 then order qualfier must be a Problem Solver

			if (batch.OrderQualifierID == OrderQualifier.KanataPSolver
				|| batch.OrderQualifierID == OrderQualifier.ProblemSolver
				|| batch.OrderQualifierID == OrderQualifier.GiftPSolver
				|| batch.OrderQualifierID == OrderQualifier.WFCSigningBonus
                || batch.OrderQualifierID == OrderQualifier.BookProblemSolver
				|| batch.OrderQualifierID == OrderQualifier.FMBulkSupply)
			{
				return isValid;
			}

			foreach (OrderHeader orderHeader in batch.OrderHeaders)
			{
				foreach (ProductItem item in orderHeader.ProductItems)
				{
					isValid &= item.EnterredPrice > 0.0;
				}
			}
			
			return isValid;
		}

		/*protected override bool Validate(DataRow Row)
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
		

		private bool IsValid_RequiredFields(DataRow Row)
		{
			bool IsValid = true;
			IsValid &= IsValid_RequiredField(Row,tableRef.FLD_DELETEDTF,"DeletedTF");
			if (!IsValid)
			{
				messageManager.ValidationExceptionType =  ExceptionType.RequiredFields;
			}
			return IsValid;
		}
		
		private bool IsValids_FieldLength(DataRow Row)
		{
			bool isValid = true;
			isValid &= IsValid_FieldLength(Row, tableRef.FLD_ISPRECOLLECT, "",tableRef.FLD_ISPRECOLLECT_LENGTH);
			return isValid;
		}
*/
		
		protected override DBInteractionBase DataAccessReference
		{
			get{return this.dataAccess;}
		}
		
	}
}







