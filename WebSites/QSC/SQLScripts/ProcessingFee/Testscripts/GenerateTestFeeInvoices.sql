USE [QSPCanadaFinance]

--All Internet Orders update for CC payments
EXEC QSPCanadaFinance.dbo.Update_CreditCardPayment_ForQSPCAOrders

--Start Generating Invoices
DECLARE @OrderID 		int
DECLARE @CampaignID 	int
DECLARE @AccountID 		int
DECLARE @ProductType		int
DECLARE @Net			numeric(10,2)
DECLARE @ProfitPercentage 	numeric(10,2)
DECLARE @GroupProfitRate	numeric(10,2)
DECLARE @StaffOrderDiscount 	numeric(10,2)
DECLARE @InvoiceID		int
DECLARE @AccountType		int
DECLARE @SectionType		int
DECLARE @InvoiceSectionID	int
DECLARE @TotalTaxIncluded	numeric(10,2)
DECLARE @TotalTaxExcluded	numeric(10,2)
DECLARE @Tax			numeric(10,4)
DECLARE @Tax2		numeric(10,4)
DECLARE @TotalTax		numeric(10,4)
DECLARE @GroupProfitAmount	numeric(10,2)
DECLARE @TotalTaxableAmount  numeric(10,2)
DECLARE @TotalPostage numeric(10,2)
DECLARE @USPostageAmount numeric(10,2)
DECLARE @TaxID 		int
DECLARE @TaxRate 		numeric(10,2)
DECLARE @DueAmount		numeric(10,2)
DECLARE @InvoiceTotal		numeric(10,2)
DECLARE @RetVal 		int
DECLARE @IsNotValid		int
DECLARE @RunDate		datetime
DECLARE @ItemCount		int

DECLARE @iprocessingFeeProductLineID int
DECLARE @GlEntryTypeId int

SET @iprocessingFeeProductLineID = 46017
-- Change if insert yields different identity for GLEntryTypeId
SET @GlEntryTypeId = 17

-- Get the Orders that are ready to be invoiced from udf function
-- Only include those orders where the batch has been completed at least 3 days ago to allow for payment information to be entered.
SELECT * INTO #TempBillableOrdersFromBatch 
FROM QSPCanadaFinance..UDF_GetBillableOrdersFromBatch() 
WHERE ProductCode ='PFEE' --Check that the item code isn't 9999

-- Create Index
CREATE INDEX OrderIDIndex1 on #TempBillableOrdersFromBatch (OrderID)

--Check the payment status before invoicing.  Not generating any invoices for orders where there is a pending credit card.
DELETE #TempBillableOrdersFromBatch WHERE OrderID in (SELECT OrderID FROM #TempBillableOrdersFromBatch WHERE ISNULL(CreditCardStatus, 19000) IN (19003, 19004)) 
--Do not include any items where there is a bad credit card.  When/If the cc issue is corrected these items will be included in a problem solver order.
DELETE #TempBillableOrdersFromBatch WHERE ISNULL(CreditCardStatus, 19000) IN (19001, 19002, 19005) 

SELECT @RunDate =  GetDate()

-- Loop through records using unique orderid
SELECT DISTINCT OrderId, CampaignID, AccountID, OrderQualifierID INTO #TempOrderID FROM #TempBillableOrdersFromBatch
SELECT DISTINCT OrderId FROM #TempOrderID

-- Create Index
CREATE INDEX OrderIDIndex2 on #TempOrderID (OrderID)

WHILE EXISTS (SELECT		TOP 1
							OrderID,
							CampaignID,
							AccountID
				FROM		#TempOrderID) 
		BEGIN 
		    SELECT		TOP 1
						@OrderID = OrderID, 
						@CampaignID	= CampaignID,  
						@AccountID = AccountID 
			FROM		#TempOrderID
			ORDER BY	CASE	WHEN OrderQualifierID IN (39013, 39015, 39009) THEN 1 --Cust Service + Online should be generated first so they get picked up by next landed invoice
								ELSE 2
						END

			-- Validate order before generating Invoice
			EXEC dbo.ValidateOrderForInvoiceGeneration  @OrderId , @IsNotValid  OUTPUT

			If IsNull(@IsNotValid,1) = 0    -- Validated
			BEGIN

			-- Get Account Type (Account, FM, Emp, POS)
				EXEC GetAccountType @AccountID, @AccountType OUTPUT

				--Check if staff order and get staff order percentage
				EXEC GetStaffOrderDiscount @OrderID, @CampaignID, @StaffOrderDiscount OUTPUT
				
				--Do not insert new invoice for tests. Rather, locate the latest invoice related to the same order, and use that one
				SET @InvoiceID = (SELECT MAX (Invoice_ID) FROM INVOICE WHERE ORDER_ID = @OrderID)
				--EXEC AddFinanceInvoice @AccountID, @AccountType, @OrderID, -999, @InvoiceID OUTPUT
				SELECT @InvoiceID as CurrentInvoiceID
				
				--Loop through sections and insert an Invoice_Section record
				SELECT SectionType,
					--convert(numeric(10,4),sum(totalprice)) 		as TotalTaxIncluded, 	MS Jan 23,2007 Rounding issue For GroupSales and Profit
					Round(sum(totalprice),2) 				as TotalTaxIncluded,
					--convert(numeric(10,4),sum(totalprice-tax-Tax2)) 	as TotalTaxExcluded,
					Round(sum(totalprice),2)-Round(sum(tax+Tax2),2) as TotalTaxExcluded,
					--convert(numeric(10,4),sum(Tax)) 		as Tax,
					Round(sum(Tax),2) 				as Tax,
					--convert(numeric(10,4),sum(Tax2)) 		as Tax2,
					Round(sum(Tax2),2) 				as Tax2,
					--convert(numeric(10,4),sum(Tax+Tax2)) 		as TotalTax,
					Round(sum(Tax+Tax2),2) 			as TotalTax,
					Round(sum(PostageAmount),2)     as TotalPostage,
					Case SectionType
						WHEN 2 THEN Count(*)
						ELSE Sum(Quantity)
					END as ItemCount
				INTO #Section
				FROM #TempBillableOrdersFromBatch 
				WHERE OrderID = @OrderID
				GROUP BY SectionType
				
				WHILE EXISTS (SELECT TOP 1 SectionType, TotalTaxIncluded, TotalTaxExcluded, Tax, Tax2, TotalTax, TotalPostage FROM #Section) 
					BEGIN 
						SELECT @SectionType 	      = SectionType, 
							@TotalTaxIncluded   = TotalTaxIncluded, 
							@TotalTaxExcluded   = TotalTaxExcluded,
							@Tax                       = Tax,
							@Tax2		       = Tax2,
							@TotalTax                = TotalTax,
							@ItemCount	       = ItemCount,
							@TotalPostage      = TotalPostage
						FROM #Section
				
					    SELECT @SectionType as CurrentSectionType
						IF ISNULL(@StaffOrderDiscount,-1) > 0 --it's a staff order so include the tax in calculation.
							BEGIN
								SET @GroupProfitRate 	 = 0
								SET @GroupProfitAmount = 0
								--SET @TotalTaxExcluded = convert(numeric(10,2),(@TotalTaxIncluded*@StaffOrderDiscount)-@TotalTax)    MS Jan 23,2007 Rounding issue For GroupSales and Profit
								SET @TotalTaxExcluded = Round ( ((@TotalTaxIncluded)*@StaffOrderDiscount)- @TotalTax,2)
								--SET @TotalTaxIncluded =  convert(numeric(10,2),@TotalTaxIncluded*@StaffOrderDiscount)
								SET @TotalTaxIncluded =  Round(@TotalTaxIncluded*@StaffOrderDiscount,2)

							END
						ELSE
							BEGIN
								IF (@SectionType = 2) --Section type - Magazine
								BEGIN --Mag section gets calculated by using @TotalTaxExcluded
										EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 1, @ProfitPercentage  OUTPUT 
										SET @GroupProfitRate = @ProfitPercentage
										SET @GroupProfitAmount = Round(@ProfitPercentage*(@TotalTaxExcluded-@TotalPostage),2) --Added round Jan23,07
								END
								IF (@SectionType = 6) --Section type - Inventory Product Without Tax (Cookie Dough)
									BEGIN --Mag section gets calculated by using @TotalTaxExcluded
										EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 3, @ProfitPercentage  OUTPUT 
										SET @GroupProfitRate = IsNull(@ProfitPercentage,0)
										SET @GroupProfitAmount =Round (IsNull(@ProfitPercentage,0) *  @TotalTaxIncluded,2 ) --Added round Jan23,07
										
										SET @TotalTaxExcluded = @TotalTaxIncluded  -- CatalogPrice without Tax
										SET @TotalTax = 0 --No Tax (Cookie Dough)
									END
								IF (@SectionType = 7) --Prizes (Kanata)
									BEGIN --Prize items's price does not include tax and tax calc is like Inv product
										SET @GroupProfitRate = 0
										SET @GroupProfitAmount = 0
										
										SET @TotalTaxExcluded = @TotalTaxIncluded  -- CatalogPrice without Tax
										SET @TotalTaxIncluded = @TotalTaxExcluded + @TotalTax
										
									END
								IF (@SectionType = 8) --Processing fees
									BEGIN
										--No Profit for Processing fees
										SET @GroupProfitRate = 0
										SET @GroupProfitAmount = 0
									END
								ELSE --Section type - Inventory etc.
								IF (@SectionType  not in (2, 6,7, 8))
									BEGIN --Gift section gets calculated by using @TotalTaxIncluded
										EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 2, @ProfitPercentage  OUTPUT 
										SET @GroupProfitRate = @ProfitPercentage
										SET @GroupProfitAmount = Round (@ProfitPercentage*@TotalTaxIncluded,2) --Added Round jan23,07
									END
							END

						EXEC AddFinanceInvoiceSection @InvoiceID, @SectionType, @TotalTaxIncluded, @TotalTaxExcluded,  
											   @GroupProfitRate, @GroupProfitAmount, @TotalTax, @TotalPostage,@ItemCount, -999, 
											   @InvoiceSectionID OUTPUT, @TotalTaxableAmount OUTPUT, @DueAmount OUTPUT
						
						
						CREATE TABLE #TempTax --Check taxid here for QC taxes and different calculation
						(
							TaxID int,
							TaxRate numeric(10,2)
						)

						
						--Get the tax rate and id for this campaign
						INSERT #TempTax
						EXEC QSPCanadaCommon..GetTaxRateAndIDForCampaign @CampaignID, @SectionType
						
						WHILE EXISTS (SELECT DISTINCT TOP 1 TaxID, TaxRate FROM #TempTax ORDER BY TaxID) 
							BEGIN 
								SELECT @TaxID 	      = TaxID, 
									@TaxRate	      = TaxRate
								FROM #TempTax

								--Add invoice section tax record except Cookie Dough
								IF  @SectionType <> 6
								BEGIN
									IF (@TaxID = 3 or @TaxID = 7 or @TaxID = 8 or @TaxID = 9 )   --KET removed #6 Ontario as it is not PST as of 7/1/10
										BEGIN
											--PST
											EXEC AddInvoiceSectionTax @InvoiceSectionID, @TaxID, @TotalTaxableAmount, @TaxRate, @Tax2, -999
										END
									ELSE
										BEGIN
											EXEC AddInvoiceSectionTax @InvoiceSectionID, @TaxID, @TotalTaxableAmount, @TaxRate, @Tax, -999
										END
								END					
								DELETE FROM #TempTax WHERE TaxID = @TaxID	
								
							END
						DROP TABLE #TempTax

						SET @TaxID		= null
						SET @TaxRate		= null
						SET @TotalTaxIncluded	= null
						SET @TotalTaxExcluded= null
						SET @Tax		= null
						SET @Tax2		= null
						SET @TotalTax		= null
						SET @ItemCount	=null

						DELETE FROM #Section WHERE SectionType = @SectionType 
					END
				DROP TABLE #Section

				--Update the invoice total amount record
				EXEC UpdateFinanceInvoice @InvoiceID, -999

				-- Update only those which are being invoiced  April 5, 2005 BY MS
				UPDATE  QSPCanadaOrderManagement.dbo.CustomerorderDetail
				SET  InvoiceNumber = @InvoiceID
				FROM  #TempBillableOrdersFromBatch
				WHERE  #TempBillableOrdersFromBatch.OrderId=@OrderID
				AND QSPCanadaOrderManagement.dbo.CustomerorderDetail.Transid =    #TempBillableOrdersFromBatch.TransId
				AND QSPCanadaOrderManagement.dbo.CustomerorderDetail.CustomerorderHeaderInstance =     #TempBillableOrdersFromBatch.Instance

				--Do GL stuff for Invoice
				--1 Create a new Invoice_By_QSP_Product, only for the processing fee
				CREATE TABLE #Invoice_By_QSP_Product
				(
					Invoice_ID INT,
					QSP_Product_Line_ID INT,
					Product_Amount NUMERIC(14, 6),
					ProductLine_GP NUMERIC(14, 6),
					ProductLine_Tax1 NUMERIC(14, 6),
					ProductLine_Tax2 NUMERIC(14, 6),
					US_Postage_Amount NUMERIC(14, 6)
				)

				INSERT INTO #Invoice_By_QSP_Product
				(
					Invoice_ID,
					QSP_Product_Line_ID,
					Product_Amount,
					ProductLine_GP,
					ProductLine_Tax1,
					ProductLine_Tax2,
					US_Postage_Amount
				)
				SELECT		inv.Invoice_ID,
							prodLine.ID,
							CASE prodLine.ID
								WHEN 46008 THEN	cod.Price
								ELSE			cod.Price - Tax - Tax2
							END,
							CASE prodLine.ID
								WHEN 46001 THEN	ISNULL((cod.Price - Tax - Tax2 - isnull(pd.PostageAmount*pd.PostageRemitRate*isnull(pd.ConversionRate,0),0)) * (invSec.Group_Profit_Rate / 100.00), 0)
								WHEN 46002 THEN	ISNULL((cod.Price) * (invSec.Group_Profit_Rate / 100.00), 0)
								ELSE			ISNULL((cod.Price - Tax - Tax2) * (invSec.Group_Profit_Rate / 100.00), 0)
							END,
							ISNULL(Tax, 0),
							ISNULL(Tax2, 0),
							ISNULL(pd.PostageAmount*pd.PostageRemitRate*isnull(pd.ConversionRate,0), 0)
				FROM		Invoice inv
				JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
								ON	cod.InvoiceNumber = inv.Invoice_ID
				JOIN		QSPCanadaCommon..QSPProductLine prodLine
								ON	prodLine.ID = cod.ProductType
				JOIN		QSPCanadaProduct..Pricing_Details pd
								ON	pd.MagPrice_Instance = cod.PricingDetailsID
				JOIN		QSPCanadaProduct..ProgramSection ps
								ON	ps.ID = pd.ProgramSectionID
				JOIN		Invoice_Section invSec
								ON	invSec.Invoice_ID = inv.Invoice_ID
								AND	invSec.Section_Type_ID = ps.[Type]
				WHERE		inv.Invoice_ID = @InvoiceID
				AND			prodLine.ID = @iprocessingFeeProductLineID --46017: Processing fees
				AND			(SELECT dbo.UDF_Section_Vs_Entity(@InvoiceID, invSec.Section_Type_ID, '62')) = 'Y'

				INSERT	Invoice_By_QSP_Product
				(
					Invoice_ID,
					QSP_Product_Line_ID,
					Product_Amount,
					ProductLine_GP,
					ProductLine_Tax1,
					ProductLine_Tax2,
					US_Postage_Amount
				)
				SELECT		Invoice_ID,
							QSP_Product_Line_ID,
							ROUND(SUM(Product_Amount), 2),
							ROUND(SUM(ProductLine_GP), 2),
							ROUND(SUM(ProductLine_Tax1), 2),
							ROUND(SUM(ProductLine_Tax2), 2),
							ROUND(SUM(US_Postage_Amount), 2)
				FROM		#Invoice_By_QSP_Product
				GROUP BY	Invoice_ID,
							QSP_Product_Line_ID
				ORDER BY	QSP_Product_Line_ID

				DROP TABLE #Invoice_By_QSP_Product

				
				DECLARE	@GLEntryID			INT,
				@InvoiceDescription	VARCHAR(20),
				@CountryCode		VARCHAR(10),
				@BusinessUnitID		INT

				SET @InvoiceDescription = 'Invoice'
				SET	@CountryCode = 'CA'

				SELECT	@BusinessUnitID = acc.BusinessUnitID
				FROM	Invoice inv
				JOIN	QSPCanadaOrderManagement..Batch b
							ON	b.OrderID = inv.Order_ID
				JOIN	QSPCanadaCommon..CAccount acc
							ON	acc.ID = b.AccountID
				WHERE	inv.Invoice_ID = @InvoiceID

				BEGIN TRANSACTION
				
				
				--Don't create GL Entry, as there is already one for this invoice, but need to recover the original GLEntryID
				/* EXEC	GL_Entry_Insert
						@InvoiceID = @InvoiceID,
						@Description = @InvoiceDescription,
						@CountryCode = @CountryCode,
						@BusinessUnitID = @BusinessUnitID,
						@GLEntryID = @GLEntryID OUTPUT */
						
				SET @GLEntryID = (SELECT TOP 1 GL_Entry_ID FROM GL_ENTRY WHERE INVOICE_ID = @InvoiceID)

				/* To be corrected with actual Processing Fee GLEntryType */
				INSERT GL_Transaction
				(
					GL_Entry_ID,
					GLAccountID,
					Debit_Credit,
					Amount,
					GL_Transaction_Status_ID
				)
				SELECT		@GLEntryID,
							glAccMap.GLAccountID,
							CASE glAccMap.Debit
								WHEN 0 THEN	'C'
								ELSE		'D'
							END,
							invProd.Product_Amount,
							2
				FROM		Invoice_By_QSP_Product invProd
				JOIN		GLAccountMap glAccMap
								ON	glAccMap.ProductLineID = invProd.QSP_Product_Line_ID
				WHERE		invProd.Invoice_ID = @InvoiceID
				AND			glAccMap.GLEntryTypeID = @GlEntryTypeId --Processing fee
				AND			glAccMap.BusinessUnitID = @BusinessUnitID

				INSERT GL_Transaction
				(
					GL_Entry_ID,
					GLAccountID,
					Debit_Credit,
					Amount,
					GL_Transaction_Status_ID
				)
				SELECT		@GLEntryID,
							glAccMap.GLAccountID,
							CASE glAccMap.Debit
								WHEN 0 THEN	'C'
								ELSE		'D'
							END,
							SUM(invSecTax.Tax_Amount),
							2
				FROM		Invoice_Section invSec
				JOIN		Invoice_Section_Tax invSecTax
								ON	invSecTax.Invoice_Section_ID = invSec.Invoice_Section_ID
				JOIN		GLAccountMap glAccMap
								ON	glAccMap.TaxID = invSecTax.Tax_ID
				WHERE		invSec.Invoice_ID = @InvoiceID
				AND			glAccMap.GLEntryTypeID = 4 --4: Tax
				AND			glAccMap.BusinessUnitID = @BusinessUnitID 
				AND 		invSec.section_type_id = 8 /* Processing fee tax only, as rest of tax invoice has already been generated */
				GROUP BY	invSecTax.Tax_ID,
							glAccMap.GLAccountID,
							glAccMap.Debit



				COMMIT

				--Reset Variables
				SET @CampaignID 		= null
				SET @AccountID 		= null
				SET @ProductType		= null
				SET @Net			= null
				SET @ProfitPercentage 		= null
				SET @GroupProfitRate		= null
				SET @StaffOrderDiscount 	= null
				SET @InvoiceID		= null
				SET @AccountType		= null
				SET @SectionType		= null
				SET @TotalTaxIncluded		= null
				SET @TotalTaxExcluded	= null
				SET @Tax			= null
				SET @Tax2			= null
				SET @TotalTax			= null
				SET @GroupProfitAmount	= null
				SET @InvoiceSectionID		= null
				SET @TotalTaxableAmount	= null
				SET @TaxID			= null
				SET @TaxRate			= null
				SET @InvoiceTotal		= 0.0 --total counter
				SET @DueAmount		= null
				SET @ItemCount		=null
				
				--Keep loop going until no more recs
				DELETE FROM #TempOrderID WHERE OrderID = @OrderID
			END 
			-- Validation failed 
			DELETE FROM #TempOrderID WHERE OrderID = @OrderID
			
		END

		--Clean Up
		DROP TABLE #TempOrderID
		DROP TABLE #TempBillableOrdersFromBatch

SET NOCOUNT OFF
