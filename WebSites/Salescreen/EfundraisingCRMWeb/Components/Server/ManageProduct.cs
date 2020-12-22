using System;
using efundraising.EFundraisingCRM;
using System.Data;
using System.Collections;
using log4net;

//using EFundraisingCRMWeb.App_Data;

namespace EFundraisingCRMWeb.Components.Server
{
	/// <summary>
	/// Summary description for Product.
	/// </summary>
	public class ManageProduct
	{
        public static ILog Logger { get; set; }

        static ManageProduct()
		{
            Logger = LogManager.GetLogger(typeof(ManageProduct));
		}
		

		#region Public Static Methods

		public static readonly string fixedProfitType = "fixed";
		public static readonly string percentageProfitType = "percentage";

		public static bool IsFixedProfitType(string profitType)
		{
			return (string.Compare(profitType, fixedProfitType, true) ==0);
		}

		public static  decimal CalculateTotalProfit(DataTable dt)
		{
			decimal result = decimal.Zero;
			for (int i=0; i < dt.Rows.Count; i++)
			{
				result +=  System.Convert.ToDecimal( dt.Rows[i]["total_profit"]);
			}
			return result;
		}
		public static decimal CalculateTotalAmount(DataTable dt)
		{
			
			decimal result = decimal.Zero;
			for (int i=0; i < dt.Rows.Count; i++)
			{
				result +=  System.Convert.ToDecimal(dt.Rows[i]["total_amount"]);
			}
			return result;
		}

		public static DataTable CreateDataTableProductStructure()
		{			
			DataTable dt = new DataTable();
			dt.Columns.Add("package_id", typeof(int));
			dt.Columns.Add("scratch_book_id", typeof(int));
			dt.Columns.Add("sales_id", typeof(int));
			dt.Columns.Add("sales_item_no", typeof(int));
			dt.Columns.Add("participant_id", typeof(int));
			dt.Columns.Add("product_name");
			dt.Columns.Add("product_code");
			dt.Columns.Add("product_class_id", typeof(short));
			dt.Columns.Add("quantity", typeof(int));
			dt.Columns.Add("unit_price_sold", typeof(decimal));
			dt.Columns.Add("raising_potential", typeof(decimal));
			dt.Columns.Add("profit", typeof(decimal));
			dt.Columns.Add("profittype");
			dt.Columns.Add("total_amount", typeof(decimal), "quantity*raising_potential");
			//dt.Columns.Add("total_amount", typeof(decimal), "quantity*raising_potential*0.01*(100 - profit)");
			//dt.Columns.Add("total_profit", typeof(decimal), "quantity*(raising_potential - unit_price_sold)");
			//dt.Columns.Add("total_profit", typeof(decimal), "quantity*raising_potential*profit*0.01");
			dt.Columns.Add("total_profit", typeof(decimal), "IIF(profittype='fixed', quantity*profit, quantity*raising_potential*profit*0.01)");
			return dt;
		}

		public static ScratchBook[] GetScratchBook(System.Web.SessionState.HttpSessionState theSession)
		{
				if (theSession[Global.scratchBookConstCache] == null)
					theSession[Global.scratchBookConstCache] = ScratchBook.GetScratchBooks();

				return (ScratchBook[])theSession[Global.scratchBookConstCache];
		}

		
		public static ScratchBook GetScratchBookByID(int scratchBookId, System.Web.SessionState.HttpSessionState theSession)
		{
			efundraising.EFundraisingCRM.ScratchBook[] scrBooks = ManageProduct.GetScratchBook(theSession);
			for (int i=0; i< scrBooks.Length; i++)
			{
				if (scrBooks[i].ScratchBookId == scratchBookId)
					return scrBooks[i];
			}
			return null;
		}

		public static DataTable RetrieveProductsByPackageId(int packageId, System.Web.SessionState.HttpSessionState theSession)
		{
			Package p = Global.GetPackageById(packageId);
			DataTable dt = ManageProduct.CreateDataTableProductStructure();
			if (p.ScratchBookCollection.Count == 0)
			{
				ScratchBook[] books = GetScratchBook(theSession);
				
				
				if (books != null && books.Length > 0)
				{
					for (int i=0; i< books.Length; i++)
					{
						if (books[i].IsActive == 1 && books[i].PackageId == packageId 
							&& Sale.IsTallySaleProductClass(books[i].ProductClassId))
						{
							p.ScratchBookCollection[books[i].ScratchBookId] = books[i];
							DataRow row = dt.NewRow();
							row["package_id"] = packageId;
							row["scratch_book_id"] = books[i].ScratchBookId;
							row["sales_id"] = int.MinValue;
							row["sales_item_no"] = int.MinValue;
							row["participant_id"] = int.MinValue;
							row["product_name"] = books[i].Description;
							row["product_code"] = books[i].ProductCode ;
							row["product_class_id"] = books[i].ProductClassId;
							row["quantity"] = 0;
							row["unit_price_sold"] = decimal.Zero;
							row["raising_potential"] = Convert.ToDecimal(books[i].RaisingPotential);
							try
							{
								row["profit"] = Convert.ToDecimal(p.ProfitDefault*(decimal)100.00);
							}
							catch (Exception)
							{
								row["profit"] = (decimal)100.00;
							}
							// Scratchbook having a fixed profit.
							if (books[i].FixedProfit > 0)
							{
								row["profittype"] = fixedProfitType;
								row["profit"] = books[i].FixedProfit;
							}
							else
							{
								row["profittype"] = "percentage";
								try
								{
									row["profit"] = Convert.ToDecimal(p.ProfitDefault*(decimal)100.00);
								}
								catch (Exception)
								{
									row["profit"] = (decimal)100;
								}
							}
							dt.Rows.Add(row);
						}
					}
		
					DataRow[] rows = dt.Select("", "product_code, product_name");
					DataTable result = dt.Clone();
					for (int i=0; i< rows.Length; i++)
						result.ImportRow(rows[i]);
					return result;
				}
			}
			else
			{
				foreach (object obj in p.ScratchBookCollection.Values)
				{
					ScratchBook theBook = obj as ScratchBook;
					if (theBook != null)
					{
						DataRow row = dt.NewRow();
						row["package_id"] = packageId;
						row["scratch_book_id"] = theBook.ScratchBookId;
						row["sales_id"] = int.MinValue;
						row["sales_item_no"] = int.MinValue;
						row["participant_id"] = int.MinValue;
						row["product_name"] = theBook.Description;
						row["product_code"] = theBook.ProductCode ;
						row["product_class_id"] = theBook.ProductClassId;
						row["quantity"] = 0;
						row["unit_price_sold"] = decimal.Zero;
						row["raising_potential"] = Convert.ToDecimal(theBook.RaisingPotential);
						try
						{
							row["profit"] = Convert.ToDecimal(p.ProfitDefault*(decimal)100.00);
						}
						catch (Exception)
						{
							row["profit"] = (decimal)100;
						}
						// Scratchbook having a fixed profit.
						if (theBook.FixedProfit > 0)
						{
							row["profittype"] = fixedProfitType;
							row["profit"] = theBook.FixedProfit;
						}
						else
						{
							row["profittype"] = "percentage";
							try
							{
								row["profit"] = Convert.ToDecimal(p.ProfitDefault*(decimal)100.00);
							}
							catch (Exception)
							{
								row["profit"] = (decimal)100;
							}
						}
						dt.Rows.Add(row);
					}
				}
				DataRow[] rows = dt.Select("", "product_code, product_name");
				DataTable result = dt.Clone();
				for (int i=0; i< rows.Length; i++)
					result.ImportRow(rows[i]);
				return result;
			}

			return dt;
		}

		public static void SetDefaultValueWhenError(System.Web.UI.WebControls.DropDownList DDList, string defaultValue)
		{
			decimal defaultProfit = Convert.ToDecimal(defaultValue);
			if (DDList != null && DDList.Items.Count >1)
			{
				try
				{
					for (int i=0; i < DDList.Items.Count-1; i++)
					{
						decimal lowValue = Convert.ToDecimal(DDList.Items[i].Value);
						if (defaultProfit > lowValue)
						{
							decimal highValue = Convert.ToDecimal(DDList.Items[i+1].Value);
							DDList.SelectedIndex = i;
							if ((highValue - defaultProfit) < (defaultProfit - lowValue) )
								DDList.SelectedIndex = i+1;
							return;
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		public static decimal CalculateProfitPercentage(decimal unitPrice, decimal raisingPotential)
		{
			decimal raisingZero = Convert.ToDecimal(0.0000000001);
			if (raisingPotential <  raisingZero || unitPrice > raisingPotential)
				return decimal.Zero;

			decimal profit = ((raisingPotential - unitPrice)/raisingPotential)*(decimal)100.00;

			if (profit >= Convert.ToDecimal(99.99))
				return Convert.ToDecimal(99.99);

			return profit;
		}

		public static decimal CalculateUnitPrice(decimal profit, decimal raisingPotential)
		{
			if (profit > (decimal)100 || profit < decimal.Zero)
				return decimal.Zero;

			return ( (decimal)100.00*raisingPotential - profit*raisingPotential)*(decimal)0.01;
		}

		public static decimal CalculateUnitPriceFromFixedPrice(decimal profit, decimal raisingPotential)
		{

			return raisingPotential - profit;
		}

		public static DataTable RetrieveTallySaleItemsBySaleId(int saleID, int packageId,ref string errorMessage)
		{
			errorMessage = string.Empty;
			if (saleID == int.MinValue)
				return null;

			DataTable dt = ManageProduct.CreateDataTableProductStructure();
			SalesItem[] items = SalesItem.GetSalesItemsBySaleID(saleID);
			Hashtable hs = new Hashtable();
			if (items != null && packageId != int.MinValue)
			{				
				for (int i=0; i< items.Length; i++)
				{
					ScratchBook sbItem = ScratchBook.GetScratchBookByID(items[i].ScratchBookId);
					if (sbItem != null && Sale.IsTallySaleProductClass(sbItem.ProductClassId) )
					{
						if (hs[items[i].ScratchBookId] == null)
							hs[items[i].ScratchBookId] = sbItem;
						else
						{
							errorMessage = string.Format("It is not a valid tally sale ({0}): Having duplicate scratch_book_Ids ({1}) in sale items"
								, saleID.ToString(), items[i].ScratchBookId.ToString());
							return null;
							//throw new EFundraisingCRMException("It is not a valide sale which have duplicate ScratchBookId in sale items");
						}

						DataRow row = dt.NewRow();
						row["package_id"] = packageId;
						row["scratch_book_id"] = items[i].ScratchBookId;
						row["sales_id"] = saleID;
						row["sales_item_no"] = items[i].SalesItemNo;
						row["participant_id"] = items[i].ParticipantId;
						ScratchBook sb = hs[items[i].ScratchBookId] as ScratchBook;

					
						decimal raisingPotential = decimal.Zero;
						if (sb != null)
							raisingPotential = Convert.ToDecimal(sb.RaisingPotential);

						row["raising_potential"] = raisingPotential;
						if (sb != null)
						{
							if (sb.FixedProfit > 0)
							{
								row["profittype"] = fixedProfitType;
								row["profit"] = sb.FixedProfit;
							}
							else
							{
								row["profittype"] = "percentage";
								row["profit"] =CalculateProfitPercentage(Convert.ToDecimal(items[i].UnitPriceSold), raisingPotential);
							}

							
						}
						else
						{
							row["profit"] = 0;
						}

						row["unit_price_sold"] = Convert.ToDecimal(items[i].UnitPriceSold);
						row["product_code"] = string.Empty ;
						row["product_name"] = string.Empty;
						row["product_class_id"] = short.MinValue;
						if (sb != null)
						{
							row["product_code"] = sb.ProductCode;
							row["product_name"] = sb.Description;
							row["product_class_id"] = sb.ProductClassId;
						}
						row["quantity"] = items[i].QuantitySold;
						dt.Rows.Add(row);
					}
//					else
//					{
//						if (sbItem != null)
//							errorMessage = string.Format("It is not a valid tally sale ({0}): Having non-tally-sale item ({1})"
//								, saleID.ToString(), items[i].SalesItemNo.ToString());
//						else
//							errorMessage = string.Format("It is not a valid tally sale ({0}): could not find this sale item {1} in scratch book", 
//								saleID.ToString(), items[i].SalesItemNo.ToString());
//						return null;
//					}
				}


				if (items.Length > 0)
				{
		
					DataRow[] rows = dt.Select("", "product_code, product_name");
					DataTable result = dt.Clone();
					for (int i=0; i< rows.Length; i++)
						result.ImportRow(rows[i]);
					return result;
				}
			}
			return dt;
		}

		public static bool profitSelectEqual(decimal d1, decimal d2)
		{
			decimal epsilon = (decimal)0.001;
			if (Math.Abs(d1-d2) < epsilon)
			   return true;
			return false;
			//return decimal.Equals(d1, d2);
		}

		public static Sale SaveTallySale(DataTable dt, Client cl,int saleId, Sale additionInfo)
		{
			if (cl != null && dt != null && dt.Rows.Count > 0)
			{
				if (saleId == int.MinValue)
				{
					return CreateNewTallySale(saleId, dt, cl, additionInfo);
				}
				else
				{
					return UpdateTallySale(saleId, dt, cl, additionInfo);
				}
			}
			return null;
		}


		public static bool IsSalePackByParticipant(int salesId)
		{
			SalesItem[] salesItems = SalesItem.GetSalesItemsBySaleID(salesId);
			for (int i=0; i < salesItems.Length; i++)
			{
				if (salesItems[i].ParticipantId != int.MinValue)
				{													
					ScratchBook sb = ScratchBook.GetScratchBookByID(salesItems[i].ScratchBookId);
					if (sb != null && Sale.IsTallySaleProductClass(sb.ProductClassId) ) // Tally Sales PackBy Participant.
					{
						return true;
					}
				}
			}
			return false;
		}


		public static bool HavingKnownParticipant(SalesItem[] salesItems)
		{
			for (int i=0; i < salesItems.Length; i++)
			{
				if (salesItems[i].ParticipantId != int.MinValue)
				{
					return true;
				}
			}
			return false;
		}

		public static Sale[] GetSaleListByClient(Client cl, ref int[] scratchBookId)
		{
			if (cl == null || cl.ClientId == int.MinValue)
				return null;

			ArrayList arSale = new ArrayList();
			ArrayList arScratchBookId = new ArrayList();
			Sale[] theSales = Sale.GetSalesByClient(cl);
			if (theSales == null)
				return null;
			
			scratchBookId = null;
			for (int i=0; i< theSales.Length; i++)
			{
				SalesItem[] salesItems = SalesItem.GetSalesItemsBySaleID(theSales[i].SalesId);
				if (salesItems != null && salesItems.Length > 0)
				{
					bool bfound = false;
					for (int j=0; j< salesItems.Length && !bfound; j++)
					{														
						ScratchBook sb = ScratchBook.GetScratchBookByID(salesItems[j].ScratchBookId);
						if (sb != null && Sale.IsTallySaleProductClass(sb.ProductClassId) ) // Tally Sales.
						{
							theSales[i].IsEnterByStudent = HavingKnownParticipant(salesItems);
							bfound = true;
							arSale.Add(theSales[i]);
							arScratchBookId.Add(sb.ScratchBookId);
						}
					}
				}
			}
			Sale[] sales = (Sale[])arSale.ToArray(typeof(Sale));
			scratchBookId = (int[])arScratchBookId.ToArray(typeof(int));
			return sales;
		}



		
		private static Sale BuildSale(Client cl)
		{
			Sale sale = null;
			Lead l = Lead.GetLeadByID(cl.LeadId);
			if (l != null)
			{
				// determine the billing company
				int billingCompany = BillingCompany.eFundraising_USA.BillingCompanyID;  // 1 is the default - efundraising USA
				if (l.PromotionId == 5953 || l.PromotionId == 5961)
					billingCompany = BillingCompany.FR.BillingCompanyID;

				sale = 
					new Sale(int.MinValue, 
					l.ConsultantId, 
					short.MinValue,
					1,
					PaymentTerm.COD_30Days.PaymentTermId,
					cl.ClientSequenceCode,
					cl.ClientId,
					SalesStatus.New.SalesStatusID, GetPaymentMethodId("CHECK")
					,short.Parse(PoStatus.Pending.PoStatusId.ToString()),
					ProductionStatus.Default.ProductionStatusID,
					int.MinValue, // Sponsor Consultant
					int.MinValue, // AR Consultant
					ARStatus.NotPaid.ARStatusID,
					cl.LeadId,
					billingCompany,
					short.MinValue, // upfront payment 
					int.MinValue, // confirmer id
					CollectionStatus.Default.CollectionStatusID,
					ConfirmationMethod.CreditCard.ConfirmationMethodID, 
					CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID,  
					null, // PO number
					string.Empty, //creditInfo.creditCardNumber, 
					string.Empty, //creditInfo.expDate,
					DateTime.Now,	// sale date
					0, // shipping fees
					0, // shipping fees discounts
					DateTime.MinValue, // payment due date
					DateTime.MinValue, // confirmed date
					DateTime.MinValue, // scheduled delivery date
					DateTime.MinValue, // scheduled ship date
					DateTime.MinValue, // actual ship date
					null, // way bill no.
					null, // comment
					0, // is coupons sheet assigned
					0, // total amount
					DateTime.MinValue, // invoice date
					DateTime.MinValue, // cancellation date
					0, // is ordered
					DateTime.MinValue,  // PO received date
					0, // is delivered
					0, // local sponsor found
					DateTime.MinValue, // return date
					DateTime.MinValue, // reship date
					0, // upfront payment required
					DateTime.MinValue, // upfront payment due date
					0, // is sponsor required
					DateTime.MinValue,  // actual delivery date
					null,	// accounting comment
					null,	// social security number
					null,	// social security address
					null,	// social security city
					null,	// social security state
					null,	// social security country
					null,	// social security zip
					0,
					DateTime.MinValue,	// promise due date
					0,	// general flag (always 0)
					short.MinValue);	// fuel surcharge (always null));


				// Need to investigate in contructor
				//				sale.PaymentMethodId = GetPaymentMethodId("default");
				//				sale.SalesStatusId = SalesStatus.New.SalesStatusID;
				//				sale.PoStatusId = short.Parse(PoStatus.Pending.PoStatusId.ToString());
				//				sale.ProductionStatusId = ProductionStatus.Default.ProductionStatusID;
				//				sale.ArStatusId = ARStatus.NotPaid.ARStatusID;
				//				sale.LeadId = cl.LeadId;
				//				sale.BillingCompanyId = billingCompany;
				//				sale.CollectionStatusId = CollectionStatus.Default.CollectionStatusID;
				//				sale.ConfirmationMethodId = ConfirmationMethod.CreditCard.ConfirmationMethodID;
				//				sale.CreditApprovalMethodId = CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID;
				//				sale.SalesDate = DateTime.Now;
				//				sale.ClientSequenceCode = cl.ClientSequenceCode;
				//				sale.PaymentTermId = PaymentTerm.Prepaid.PaymentTermId;
				//				sale.ConsultantId = l.ConsultantId;
			}
			return sale;
		}

		private static SalesItem BuildSaleItem(DataRow dr, int quantityLimit, int salesItemNo, string theProfitStr)
		{
			try
			{
				decimal productPrice = decimal.Zero;
				if (theProfitStr == fixedProfitType)
				{
					productPrice = CalculateUnitPriceFromFixedPrice(Convert.ToDecimal(dr["profit"].ToString()),
						Convert.ToDecimal(dr["raising_potential"]));
				}
				else
				{
					productPrice = CalculateUnitPrice(Convert.ToDecimal(dr["profit"]),
						Convert.ToDecimal(dr["raising_potential"]));
				}

				int quantity = Convert.ToInt32(dr["quantity"]);
				if (quantity > quantityLimit )
				{
					// Caculate Unit_price_sold by setting profit and raising potentatial. Then			
					SalesItem salesItem = new SalesItem(int.MinValue, 
						salesItemNo, 
						Convert.ToInt32(dr["scratch_book_id"]), 
						Convert.ToInt16(ServiceType.Bulk.ServiceTypeId),
						Convert.ToInt16(dr["product_class_id"]),"", quantity,
						productPrice,
						0,
						"",
						(quantity * productPrice), // total price,
						0,
						0,
						0,
						0,
						0,
						quantity,
						null);
					return salesItem;
				}
			}
			catch (Exception ex)
			{
				throw new EFundraisingCRMException("UpdateTallySale", ex);
			}

			return null;
		}


		#endregion

		#region Pack By student

		public static Sale ConvertToTallySalePackByStudent(int saleId, Client cl, Sale additionSaleInfo,
			DataTable newStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			DataTable existingStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			Hashtable studentSaleItems /* A Hashtable of DataTable of ManageProduct.CreateDataTableProductStructure */,
			DataTable mainDT)
		{
			if (saleId == int.MinValue)
				return null;

			
			Sale theSale = PrepareSaleToUpdateTallySalePackByStudent(saleId, cl, newStudents, existingStudents, studentSaleItems, mainDT);

			if (theSale != null)
			{
				// Cannot do the conversion if there is no participant
				if (theSale.Participants.Count < 1)
					return null;
				SetAdditionInfo(theSale, additionSaleInfo);
				theSale.Convert2TallySalePackByParticipants = true;

				// Do the conversion.				
				TransactionController trans = new TransactionController();
				SaleCollection salesCollection = new SaleCollection();
				salesCollection.Add(theSale);
				trans.UpdateAndInsertSalesItemsPackByParticipants(cl, salesCollection, GetCommentsBySaleId(theSale));
				return theSale;
			}

			return null;
		}

		public static Sale SaveTallySalePackByStudent(int saleId, Client cl, Sale addtionSaleInfo,
			DataTable newStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			DataTable existingStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			Hashtable studentSaleItems /* A Hashtable of DataTable of ManageProduct.CreateDataTableProductStructure */,
			DataTable mainDT)
			
		{
			

			if (saleId == int.MinValue)
			{
				Sale newSale = null;
				try
				{
					newSale = DoSaveTallySalePackByStudent( cl, addtionSaleInfo, newStudents, existingStudents, studentSaleItems, mainDT);
					if (newSale != null)
                        efundraising.EFundraisingCRM.Sale.RecalculeTotalSaleAmount(newSale.SalesId);
				}
				catch (Exception ex)
				{					
					Logger.Error("DoSaleTallySalePackByStudent: Cannot SaveTallySalePackByStudent", ex);
				}
				return newSale;
			}
			else
			{
				Sale theSale = PrepareSaleToUpdateTallySalePackByStudent(saleId, cl, newStudents, existingStudents, studentSaleItems, mainDT);
				SetAdditionInfo(theSale, addtionSaleInfo);

				if (theSale != null)
				{
					try
					{
						TransactionController trans = new TransactionController();
						SaleCollection salesCollection = new SaleCollection();
						salesCollection.Add(theSale);
						trans.UpdateAndInsertSalesItemsPackByParticipants(cl, salesCollection, GetCommentsBySaleId(theSale));
					}
					catch (Exception ex)
					{
						Logger.Error("UpdateAndInsertSalesItemsPackByParticipants: Cannot Update Sale", ex);
					}
					return theSale;
				}
				return null;
			}

		}


		public static DataTable RetrieveSaleItemsBySaleIdAndStudentId(int saleID, int packageId, int studentId, ref string errorMesssage)
		{
			DataTable dt = null;
			string lcErrorMesssage = string.Empty;
			Hashtable hashTable = new Hashtable();
			DataTable allSaleItemsDataTbl = RetrieveSaleItemsPackByStudentBySaleId(saleID, packageId,ref lcErrorMesssage);
			if (lcErrorMesssage != string.Empty)
			{
				errorMesssage = lcErrorMesssage;
				return null;
			}
			if (allSaleItemsDataTbl != null && allSaleItemsDataTbl.Rows.Count >0)
			{
				dt = allSaleItemsDataTbl.Clone();
				DataRow[] rows = allSaleItemsDataTbl.Select(string.Format("participant_id={0}", studentId.ToString()) );
				for (int i=0; i < rows.Length; i++)
				{
					if (hashTable[rows[i]["sale_item_no"]] == null)
						hashTable[rows[i]["sale_item_no"]] = rows[i]["sale_item_no"];
					else
					{
						errorMesssage = "Each student can not have duplicate sale items";
						return null;
					}
					dt.ImportRow(rows[i]);
				}

			}
			return dt;
		}

		public static DataTable RetrieveSaleItemsPackByStudentBySaleId(int saleID, int packageId,ref string errorMessage)
		{
			errorMessage = string.Empty;
			if (saleID == int.MinValue)
				return null;

			DataTable dt = ManageProduct.CreateDataTableProductStructure();
			SalesItem[] items = SalesItem.GetSalesItemsBySaleID(saleID);
			Hashtable hs = new Hashtable();
			if (items != null && packageId != int.MinValue)
			{				
				for (int i=0; i< items.Length; i++)
				{
					ScratchBook sbItem = ScratchBook.GetScratchBookByID(items[i].ScratchBookId);
					if (sbItem != null && Sale.IsTallySaleProductClass(sbItem.ProductClassId) )
					{
						if (hs[items[i].ScratchBookId] == null)
							hs[items[i].ScratchBookId] = sbItem;

						DataRow row = dt.NewRow();
						row["package_id"] = packageId;
						row["scratch_book_id"] = items[i].ScratchBookId;
						row["sales_id"] = saleID;
						row["sales_item_no"] = items[i].SalesItemNo;
						row["participant_id"] = items[i].ParticipantId;
						ScratchBook sb = hs[items[i].ScratchBookId] as ScratchBook;

					
						decimal raisingPotential = decimal.Zero;
						if (sb != null)
							raisingPotential = Convert.ToDecimal(sb.RaisingPotential);

						row["raising_potential"] = raisingPotential;
						if (sb != null)
						{
							if (sb.FixedProfit > 0)
							{
								row["profit"] = sb.FixedProfit;
								row["profittype"] = ManageProduct.fixedProfitType;
							}
							else
							{
								row["profit"] =CalculateProfitPercentage(Convert.ToDecimal(items[i].UnitPriceSold), raisingPotential);
								row["profittype"] = ManageProduct.percentageProfitType;
							}
						}
						else
							row["profit"] = 0;

						row["unit_price_sold"] = Convert.ToDecimal(items[i].UnitPriceSold);
						row["product_code"] = string.Empty ;
						row["product_name"] = string.Empty;
						row["product_class_id"] = short.MinValue;
						if (sb != null)
						{
							row["product_code"] = sb.ProductCode;
							row["product_name"] = sb.Description;
							row["product_class_id"] = sb.ProductClassId;
						}
						row["quantity"] = items[i].QuantitySold;
						dt.Rows.Add(row);
					}
//					else
//					{
//						if (sbItem != null)
//							errorMessage = string.Format("It is not a valid tally sale ({0}): Having non-tally-sale item ({1})"
//								, saleID.ToString(), items[i].SalesItemNo.ToString());
//						else
//							errorMessage = string.Format("It is not a valid tally sale ({0}): could not find this sale item {1} in scratch book", 
//								saleID.ToString(), items[i].SalesItemNo.ToString());
//						return null;
//					}
				}


				if (items.Length > 0)
				{
		
					DataRow[] rows = dt.Select("", "product_code, product_name");
					DataTable result = dt.Clone();
					for (int i=0; i< rows.Length; i++)
						result.ImportRow(rows[i]);
					return result;
				}
			}
			return dt;
		}

		
		private static Sale PrepareSaleToUpdateTallySalePackByStudent(int saleId, Client cl,
			DataTable newStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			DataTable existingStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			Hashtable studentSaleItems /* A Hashtable of DataTable of ManageProduct.CreateDataTableProductStructure */,
			DataTable itemTable)
		{
			Sale sale = Sale.GetSaleByID(saleId);

			if (sale == null)
				return null;


			int saleItemNo = int.MaxValue - 100000;

			if (newStudents != null)
			{
				for (int t=0; t < newStudents.Rows.Count; t++)
				{
					// Create new students from newStudentList.
					Participant p = new Participant(int.MinValue);
					p.FirstName = (string)newStudents.Rows[t]["first_name"];
					p.LastName = (string)newStudents.Rows[t]["last_name"];

					DataTable dt = studentSaleItems[Convert.ToInt32(newStudents.Rows[t]["participant_id"])] as DataTable;
					if (dt != null && dt.Rows.Count >0)
					{
						// From student_id of newStudentList find sale item in studentSaleItems then create new sale items.
						for (int k=0; k < dt.Rows.Count; k++)
						{
							// 0 in quantityLimit means: Insert only when quantity > 0
							int scratchBookID = Convert.ToInt32(dt.Rows[k]["scratch_book_id"]);
							SalesItem item = BuildSaleItem(dt.Rows[k], 0, int.MinValue, GetScratchBookProfitType(itemTable, scratchBookID));
							if (item != null)
							{
								// Add Sale item into the participant sale item list.
								item.SalesId = sale.SalesId;
								p.SalesItems.Add(item);
								saleItemNo++;
							}
						}
					}
					sale.Participants.Add(p);
				}


			}
			//
			if (existingStudents != null)
			{
				// Go thru studentSaleItems to create participant's sale items.
				foreach (object obj in studentSaleItems.Keys)
				{
					int partId = Convert.ToInt32(obj);
					if (partId != int.MinValue)
					{
						DataRow[] rows = existingStudents.Select(string.Format("participant_id={0}", partId));
						if (rows != null && rows.Length >0) // A participant has sale items to be created.
						{
							// Retrieve a DataTable of sale items of the participant.
							DataTable dt = studentSaleItems[partId] as DataTable;
							if (dt != null && dt.Rows.Count >0)
							{
								Participant p = Participant.GetParticipantByID(partId);
								if (p != null)
								{
									for (int j=0; j < dt.Rows.Count; j++)
									{
										int quantity = Convert.ToInt32(dt.Rows[j]["quantity"]);
										decimal profit = Convert.ToDecimal(dt.Rows[j]["profit"]);
										decimal productPrice = decimal.Zero;
										int scratchBookID = Convert.ToInt32(dt.Rows[j]["scratch_book_id"]);

										string theProfitStr = GetScratchBookProfitType(itemTable, scratchBookID);
										if (theProfitStr == fixedProfitType)
										{
											productPrice = CalculateUnitPriceFromFixedPrice(Convert.ToDecimal(dt.Rows[j]["profit"].ToString()),
												Convert.ToDecimal(dt.Rows[j]["raising_potential"]));
										}
										else
										{
											productPrice = CalculateUnitPrice(Convert.ToDecimal(dt.Rows[j]["profit"]),
												Convert.ToDecimal(dt.Rows[j]["raising_potential"]));
										}


										SalesItem item = null;
										saleItemNo = Convert.ToInt32(dt.Rows[j]["sales_item_no"]);
										if (saleItemNo < 0) // It is to insert a new sale item.
										{											
											// -1 in quantityLimit means: Update also quantity = 0
											item = BuildSaleItem(dt.Rows[j], -1, int.MinValue, GetScratchBookProfitType(itemTable, scratchBookID));
										}
										else
										{
											// Retrieve sale item from database.
											item = SalesItem.GetSalesItemBySaleIdAndSaleItemNo(saleId, saleItemNo);
											// Setting items informtation according to the interface.
											if (item != null && quantity >= 0)
											{
												item.QuantitySold = quantity;
												// Calculate Unit_price_sold by setting profit and raising potentatial. Then
												// Update Item.
												item.UnitPriceSold = productPrice;
												item.SalesAmount = (quantity * productPrice);
												item.NbUnitsSold = quantity;
											}
										}

										if (item != null && p.ParticipantId != int.MinValue)
										{
											// Set sale item participant id.
											item.ParticipantId = p.ParticipantId;
											//Add Sale item into participant sale item list.
											item.SalesId = sale.SalesId;
											p.SalesItems.Add(item);
										}

									}
									sale.Participants.Add(p);
								}
							}
						}
					}
				}
			}


			

			return sale;
		}



		private static string GetScratchBookProfitType(DataTable mainDT, int scratchBookID)
		{
			DataRow[] rows = mainDT.Select(string.Format("scratch_book_id={0}", scratchBookID.ToString()));
			if (rows == null || rows.Length < 1)
				return percentageProfitType;
			else
			{
				return (string)rows[0]["profittype"];
			}
		}

		private static Sale DoSaveTallySalePackByStudent(Client cl, Sale addtionSaleInfo, 
			DataTable newStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			DataTable existingStudents /* ManageStudent.CreateDataTableStudentStructure*/, 
			Hashtable studentSaleItems /* A Hashtable of DataTable of ManageProduct.CreateDataTableProductStructure */,
			DataTable itemTable)
		{
			Sale sale = BuildSale(cl);

			if (sale == null)
				return null;



			SetAdditionInfo(sale, addtionSaleInfo);
			int saleItemNo = 1;

			if (newStudents != null)
			{
				for (int t=0; t < newStudents.Rows.Count; t++)
				{
					// Create new students from newStudentList.
					Participant p = new Participant(int.MinValue);
					p.FirstName = (string)newStudents.Rows[t]["first_name"];
					p.LastName = (string)newStudents.Rows[t]["last_name"];

					DataTable saleItemsDt = studentSaleItems[Convert.ToInt32(newStudents.Rows[t]["participant_id"])] as DataTable;
					if (saleItemsDt != null && saleItemsDt.Rows.Count >0)
					{
						// From student_id of newStudentList find sale item in studentSaleItems then create new sale items.
						DataRow[] itemRows = saleItemsDt.Select("quantity > 0");
						for (int k=0; k < itemRows.Length; k++)
						{
							// 0 in quantityLimit means: Insert only when quantity > 0
							int scratchBookID = Convert.ToInt32(itemRows[k]["scratch_book_id"]);
							SalesItem item = BuildSaleItem(itemRows[k], 0, int.MinValue, GetScratchBookProfitType(itemTable, scratchBookID));
							if (item != null)
							{
								// Add Sale item into the participant sale item list.
								p.SalesItems.Add(item);
								saleItemNo++;
							}
						}
					}
					if (p.SalesItems.Count > 0)
						sale.Participants.Add(p);
				}


			}

			if (existingStudents != null)
			{
				// Go thru studentSaleItems to create participant's sale items.
				foreach (object obj in studentSaleItems.Keys)
				{
					int partId = Convert.ToInt32(obj);
					if (partId != int.MinValue)
					{
						DataRow[] rows = existingStudents.Select(string.Format("participant_id={0}", partId));
						if (rows != null && rows.Length >0)// A participant has sale items to be created.
						{
							// Retrieve a DataTable of sale items of the participant.
							DataTable saleItemsDt = studentSaleItems[partId] as DataTable;
							if (saleItemsDt != null && saleItemsDt.Rows.Count >0)
							{
								Participant p = Participant.GetParticipantByID(partId);
								if (p != null)
								{
									// Create sale items for the participant.
									DataRow[] itemRows = saleItemsDt.Select("quantity > 0");
									for (int j=0; j < itemRows.Length; j++)
									{
										int scratchBookID = Convert.ToInt32(itemRows[j]["scratch_book_id"]);
										SalesItem item = BuildSaleItem(itemRows[j], 0, int.MinValue, GetScratchBookProfitType(itemTable, scratchBookID));
										if (item != null)
										{
											try
											{
												item.ParticipantId = p.ParticipantId;
											}
											catch (Exception ex)
											{
												throw new EFundraisingCRMException("DoSaleTallySalePackByStudent: Cannot create item", ex);
											}
											// Sale item into participant sale item list.
											p.SalesItems.Add(item);
											saleItemNo++;
										}

									}
									if (p.SalesItems.Count > 0)
										sale.Participants.Add(p);
								}
							}
						}
					}
				}
			}

			if (sale.Participants.Count > 0)
			{
				TransactionController trans = new TransactionController();
				SaleCollection salesCollection = new SaleCollection();
				CommentsCollection commentCollection = new CommentsCollection();
				salesCollection.Add(sale);
				
				Lead l = Lead.GetLeadByID(cl.LeadId);
				//
				Comments comment = null;
				if (l != null)
				{
					comment = new Comments(int.MinValue,
						2, // Priority : medium
						int.MinValue,l.ConsultantId,l.LeadId);
				}
				else
				{
					comment = new Comments(int.MinValue,2, // Priority : medium
						int.MinValue);
				}
				comment.EntryDate = DateTime.Now;
				comment.Comment = sale.Comment;
				commentCollection.Add(comment);
				//
				trans.InsertSalesPackByParticipants(cl, salesCollection, commentCollection);
				return sale;
			}

			return null;
		}



		public static ArrayList GetParticipantsByClient(Client cl)
		{
			if (cl == null || cl.ClientId == int.MinValue)
				return null;

			Sale[] theSales = Sale.GetSalesByClient(cl);
			if (theSales == null)
				return null;

			ArrayList p = new ArrayList();

			for (int i=0; i< theSales.Length; i++)
			{
				SalesItem[] salesItems = SalesItem.GetSalesItemsBySaleID(theSales[i].SalesId);
				if (salesItems != null)
				{
					for (int j=0; j< salesItems.Length; j++)
					{
						if (salesItems[j].ParticipantId != int.MinValue && !p.Contains(salesItems[j].ParticipantId))
							p.Add(salesItems[j].ParticipantId);
					}
				}
			}

			return p;
		}

		#endregion

		#region Private Static Methods

		private static void SetAdditionInfo(Sale s, Sale additionSale)
		{
			if (s != null && additionSale != null)
			{
				if (additionSale.SalesDate != DateTime.MinValue)
					s.SalesDate = additionSale.SalesDate;
				s.PaymentDueDate = additionSale.PaymentDueDate;
				s.ScheduledDeliveryDate = additionSale.ScheduledDeliveryDate;

				s.PaymentMethodId = additionSale.PaymentMethodId;
				s.PaymentTermId = additionSale.PaymentTermId;
				s.ShippingFees = additionSale.ShippingFees;
				s.Fuelsurcharge = additionSale.Fuelsurcharge;
				if (s.ShippingFees < Decimal.Zero)
					s.ShippingFees = Decimal.Zero;
				if (s.Fuelsurcharge < 0)
					s.Fuelsurcharge = 0;

				if (s.PaymentMethodId < 0)
					s.PaymentMethodId = GetPaymentMethodId("CHECK");
				if (s.PaymentTermId < 0)
					s.PaymentTermId = PaymentTerm.COD_30Days.PaymentTermId;

				s.IsPackedByStudent = additionSale.IsPackedByStudent;
				s.Comment = additionSale.Comment;
			}
		}

		public static short GetPaymentMethodId(string creditCardType)
		{
			
			switch (creditCardType.ToUpper())
			{
				case "VISA"	: return 2;
				case "MASTERCARD" : return 3;
				case "CHECK":
					return 1;
				default:
					return 2;
			}
		}
		
		private static Sale CreateNewTallySale(int saleId, DataTable saleDt, Client cl, Sale additionInfo)
		{
			DataRow[] rows = saleDt.Select("quantity > 0");
			if (rows == null || rows.Length < 1)
				return null;

			Sale sale = null;
			Lead l = efundraising.EFundraisingCRM.Lead.GetLeadByID(cl.LeadId);
			if (l != null)
			{
				// determine the billing company
				int billingCompany = BillingCompany.eFundraising_USA.BillingCompanyID;  // 1 is the default - efundraising USA
				if (l.PromotionId == 5953 || l.PromotionId == 5961)
					billingCompany = BillingCompany.FR.BillingCompanyID;

				sale = BuildSale(cl);
				SetAdditionInfo(sale, additionInfo);
				int salesItemNo = 1;

				
				for (int i=0; i < rows.Length; i++)
				{
					try
					{
						decimal productPrice = decimal.Zero;

						string theProfitStr = rows[i]["profittype"].ToString().Trim();
						if (theProfitStr == fixedProfitType)
						{
							productPrice = CalculateUnitPriceFromFixedPrice(Convert.ToDecimal(rows[i]["profit"].ToString()),
								Convert.ToDecimal(rows[i]["raising_potential"]));
						}
						else
						{
							productPrice = CalculateUnitPrice(Convert.ToDecimal(rows[i]["profit"]),
								Convert.ToDecimal(rows[i]["raising_potential"]));
						}
						int quantity = Convert.ToInt32(rows[i]["quantity"]);
						if (quantity > 0 )
						{
							// Caculate Unit_price_sold by setting profit and raising potentatial. Then			
							SalesItem salesItem = new SalesItem(int.MinValue, 
								salesItemNo, 
								Convert.ToInt32(rows[i]["scratch_book_id"]), 
								Convert.ToInt16(ServiceType.Bulk.ServiceTypeId),
								Convert.ToInt16(rows[i]["product_class_id"]),"", quantity,
								productPrice,
								0,
								"",
								(quantity * productPrice), // total price,
								0,
								0,
								0,
								0,
								0,
								quantity,
								null);
					
							// Insert Sale Item.
							sale.SalesItems.Add(salesItem);						
							salesItemNo++;
						}
					}
					catch (Exception ex)
					{
						throw new EFundraisingCRMException("CreateNewTallySale", ex);
					}
				}
				// Insert Sale: Sale.InsertSale(saleItems_ToBeInserted);
				TransactionController trans = new TransactionController();
				SaleCollection salesCollection = new SaleCollection();
				CommentsCollection commentCollection = new CommentsCollection();
				salesCollection.Add(sale);
				//
				Comments comment = new Comments(int.MinValue,
					2, // Priority : medium
					int.MinValue,l.ConsultantId,l.LeadId);
				comment.EntryDate = DateTime.Now;
				comment.Comment = sale.Comment;
				commentCollection.Add(comment);
				trans.InsertSalesForExistingClient(cl, salesCollection, commentCollection);
				return sale;
			}
			return null;
		}

		
		private static Sale UpdateTallySale(int saleId, DataTable saleDt, Client cl, Sale additionInfo)
		{
			Sale sale = Sale.GetSaleByID(saleId);
			if (sale != null)
			{
				SetAdditionInfo(sale, additionInfo);
				SalesItemCollection insertItems = new SalesItemCollection();
				SalesItemCollection updateItems = new SalesItemCollection();
				DataRow[] rows = saleDt.Select("");
				int maxSalesItemNo = SalesItem.GetMaxSalesItemNo(saleId);
				for (int i=0; i < rows.Length; i++)
				{
					try
					{
						int quantity = Convert.ToInt32(rows[i]["quantity"]);
						int saleItemNo = Convert.ToInt32(rows[i]["sales_item_no"]);
						decimal profit = Convert.ToDecimal(rows[i]["profit"]);
						decimal productPrice = decimal.Zero;

						string theProfitStr = rows[i]["profittype"].ToString().Trim();
						if (theProfitStr == fixedProfitType)
						{
							productPrice = CalculateUnitPriceFromFixedPrice(Convert.ToDecimal(rows[i]["profit"].ToString()),
								Convert.ToDecimal(rows[i]["raising_potential"]));
						}
						else
						{
							productPrice = CalculateUnitPrice(Convert.ToDecimal(rows[i]["profit"]),
								Convert.ToDecimal(rows[i]["raising_potential"]));
						}

						if (saleItemNo == int.MinValue)
						{
							if (quantity >0)
							{
								// Caculate Unit_price_sold by setting profit and raising potentatial. Then	
								// Insert Sale Item.
								if (maxSalesItemNo > -1)
								{				
									maxSalesItemNo++;
									SalesItem salesItem = new SalesItem(int.MinValue, 
										maxSalesItemNo, 
										Convert.ToInt32(rows[i]["scratch_book_id"]), 
										Convert.ToInt16(ServiceType.Bulk.ServiceTypeId),
										Convert.ToInt16(rows[i]["product_class_id"]),"", quantity,
										productPrice,
										0,
										"",
										(quantity * productPrice), // total price,
										0,
										0,
										0,
										0,
										0,
										quantity,
										null);	
									insertItems.Add(salesItem);
									sale.SalesItems.Add(salesItem);
								}
								else
								{
									throw new EFundraisingCRMException("UpdateTallySale: Cannot get max(sales_item_no");
								}
							}
						}
						else
						{
							SalesItem item = SalesItem.GetSalesItemBySaleIdAndSaleItemNo(saleId,saleItemNo);
							if (item != null && quantity >= 0)
							{
								item.QuantitySold = quantity;
								// Caculate Unit_price_sold by setting profit and raising potentatial. Then
								// Update Item.
								item.UnitPriceSold = productPrice;
								item.SalesAmount = (quantity * productPrice);
								//item.PaidAmount = (quantity * productPrice);
								item.NbUnitsSold = quantity;
								updateItems.Add(item);
								sale.SalesItems.Add(item);
							}
						}
					}
					catch (Exception ex)
					{
						throw new EFundraisingCRMException("UpdateTallySale", ex);
					}
				}
				
				// Update Sale: Sale.UpdateSale(saleItems_ToBeUpdated, saleItems_ToBeInserted);
				TransactionController trans = new TransactionController();
				trans.UpdateAndInsertSalesItems(cl, sale, insertItems, updateItems, GetCommentsBySaleId(sale));
			}

			return sale;
		}


		static private CommentsCollection GetCommentsBySaleId(Sale sale)
		{
			
			Comments[] cts = Comments.GetCommentsBySaleID(sale.SalesId);
			CommentsCollection commentCol = new CommentsCollection();
			for (int i=0; i< cts.Length; i++)
			{
				commentCol.Add(cts[i]);
			}
			return commentCol;
		}



		#endregion

		#region Utilities For Printing
		public static DataTable BuildReportHeader(string[] reportHeaders, string[] reportHeadersData)
		{
			DataTable dt = new DataTable();
			for (int i=0; i< reportHeaders.Length; i++)
			{
				dt.Columns.Add(reportHeaders[i]);
			}

			DataRow r = dt.NewRow();
			for (int i=0; i< reportHeaders.Length; i++)
				r[reportHeaders[i]] = reportHeadersData[i];
			dt.Rows.Add(r);
			return dt;
		}

		public static DataSet BuildReportDataSet(DataTable dtData,string[] reportHeaders, string[] reportHeadersData)
		{
			DataSet ds = new DataSet();
			DataTable dtHeader = BuildReportHeader(reportHeaders, reportHeadersData);
			ds.Tables.Add(dtHeader);

			DataTable dt = new DataTable();
			for (int i=0; i< reportHeaders.Length; i++)
			{
				dt.Columns.Add(reportHeaders[i]);
			}

			for (int i=0; i < dtData.Rows.Count; i++)
			{
				decimal theQuantity = Convert.ToDecimal(dtData.Rows[i]["quantity"]);
				if (theQuantity > 0)
				{
					decimal theProfit = decimal.Zero;
					DataRow r = dt.NewRow();
					r["Product|width:150;height:25"] = dtData.Rows[i]["product_name"].ToString();
					if (ManageProduct.IsFixedProfitType((string)dtData.Rows[i]["profittype"]))
					{
						theProfit = Convert.ToDecimal(dtData.Rows[i]["profit"]);
						r["Profit"] = theProfit.ToString("C");
					}
					else
					{
						theProfit = Convert.ToDecimal(dtData.Rows[i]["profit"])*(decimal)0.01;
						r["Profit"] = theProfit.ToString("P");
					}
					r["Product Code"] = dtData.Rows[i]["product_code"].ToString();
					r["Quantity"] = dtData.Rows[i]["quantity"].ToString();
					r["Raising Potential"] = dtData.Rows[i]["raising_potential"].ToString();
					r["Total Amount"] = dtData.Rows[i]["total_amount"].ToString();
					r["Total Profit"] = dtData.Rows[i]["total_profit"].ToString();
					dt.Rows.Add(r);
				}
			}

			ds.Tables.Add(dt);

			return ds;
		}
		#endregion
	}
}
