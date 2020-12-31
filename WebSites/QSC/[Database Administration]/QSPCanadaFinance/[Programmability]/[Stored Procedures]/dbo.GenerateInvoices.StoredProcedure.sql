USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GenerateInvoices]    Script Date: 06/07/2017 09:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerateInvoices] 
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 5/6/2004 
--   Generate Invoices for Orders that have been sent to remit or shipped
--   Multiple steps
--   MTC 7/28/2004
--   Added code to do GL entries for Invoice (Generate_Gl_For_Invoice)
--   Removed Code to insert into invoice_by_qsp_product table.  This is being handled by Generate_Gl_For_Invoice proc now.
--   MTC 10/25/2004
--   Check the payment status before invoicing.  Not generating any invoices for orders where there is a bad credit card.
--   MTC 11/30/2004
--   Do not include items where the credit card status is 19001, 19002 (Errors).
--   Do not include any orders where the credit card status is 19003, 19004 (Waiting for payments to clear).
--   MTC 12/1/2004
--   Do not include any orders where credit card status is 19005.  Means payment amount is 0.  Removed the 3 day waiting period for payments and adjustments.
--   MTC 12/13/2004
--   Do not invoice Internet Fix (39011) Orders.
--   Email List of orders failed to invoice  Added March 21, 2006 MS
--   Update Incoice Number in COD for only those which are being invoiced Added April 5
--   Count number of items for each program and insert in Invoice Section alongwith Amount
--   Update CC payment status for Online before Invoicing MS jan 24, 2007
--	 CRL 8/2/2011
--	 Added section to calculate group profit amount and rate (0) for processing fees
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

--All Internet Orders update for CC payments
--EXEC QSPCanadaFinance.dbo.Update_CreditCardPayment_ForQSPCAOrders

--Start Generating Invoices
DECLARE @OrderID 		int
DECLARE @CampaignID 	int
DECLARE @AccountID 		int
DECLARE @ProductType		int
DECLARE @Net			numeric(10,2)
DECLARE @ProfitPercentage 	numeric(10,6)
DECLARE @GroupProfitRate	numeric(10,6)
DECLARE @ThirdPartyProfitRate	numeric(10,6)
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
DECLARE @ThirdPartyProfitAmount	numeric(10,2)
DECLARE @TotalTaxableAmount  numeric(10,2)
DECLARE @TotalPostage numeric(10,2)
DECLARE @USPostageAmount numeric(10,2)
DECLARE @TaxID 		int
DECLARE @TaxRate 		numeric(10,3)
DECLARE @DueAmount		numeric(10,2)
DECLARE @InvoiceTotal		numeric(10,2)
DECLARE @RetVal 		int
DECLARE @IsNotValid		int
DECLARE @RunDate		datetime
DECLARE @ItemCount		int
DECLARE @OrderQualifierID int
DECLARE @TRTGenerationCode varchar(4)
DECLARE @ProgramType int

-- Get the Orders that are ready to be invoiced from udf function
-- Only include those orders where the batch has been completed at least 3 days ago to allow for payment information to be entered.
SELECT * INTO #TempBillableOrdersFromBatch 
FROM UDF_GetBillableOrdersFromBatch() 
WHERE ProductCode <> '9999' --Check that the item code isn't 9999 - Illegible product

-- Create Index
CREATE INDEX OrderIDIndex1 on #TempBillableOrdersFromBatch (OrderID)

--Check the payment status before invoicing.  Not generating any invoices for orders where there is a pending credit card.
DELETE #TempBillableOrdersFromBatch WHERE OrderID in (SELECT OrderID FROM #TempBillableOrdersFromBatch WHERE ISNULL(CreditCardStatus, 19000) IN (19003, 19004)) 
--Do not include any items where there is a bad credit card.  When/If the cc issue is corrected these items will be included in a problem solver order.
DELETE #TempBillableOrdersFromBatch WHERE ISNULL(CreditCardStatus, 19000) IN (19001, 19002, 19005) 

SELECT @RunDate =  GetDate()

-- Loop through records using unique orderid
SELECT DISTINCT OrderId, CampaignID, AccountID, OrderQualifierID INTO #TempOrderID FROM #TempBillableOrdersFromBatch
-- Create Index
CREATE INDEX OrderIDIndex2 on #TempOrderID (OrderID)

WHILE EXISTS (SELECT TOP 1 OrderID, CampaignID, AccountID FROM #TempOrderID) 
	BEGIN 
		SELECT		TOP 1
					@OrderID = OrderID, 
					@CampaignID	= CampaignID,  
					@AccountID = AccountID,
					@OrderQualifierID = OrderQualifierID
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
			
			DECLARE	@IsFMAccount BIT
			SELECT	@IsFMAccount = CASE CAccountCodeClass WHEN 'FM' THEN 1 ELSE 0 END
			FROM	#TempBillableOrdersFromBatch
			WHERE	OrderID = @OrderID
			
			DECLARE @IsPrinted char(10)
			SET @IsPrinted = CASE @IsFMAccount WHEN 1 THEN 'Y' ELSE 'N' END
			
			--Insert Invoice Record
			EXEC AddFinanceInvoice @AccountID, @AccountType, @OrderID, -999, @IsPrinted, @InvoiceID OUTPUT					
			
			--Loop through sections and insert an Invoice_Section record
			SELECT SectionType,
				Round(sum(totalprice),2) 				as TotalTaxIncluded,
				Round(sum(totalprice),2)-Round(sum(tax+Tax2),2) as TotalTaxExcluded,
				Round(sum(Tax),2) 				as Tax,
				Round(sum(Tax2),2) 				as Tax2,
				Round(sum(Tax+Tax2),2) 			as TotalTax,
				Round(sum(PostageAmount),2)     as TotalPostage,
				Case SectionType
					WHEN 2 THEN Count(*)
					ELSE Sum(Quantity)
				END as ItemCount,
				ISNULL(TRTGenerationCode, 0) TRTGenerationCode,
				ProgramType
			INTO #Section
			FROM #TempBillableOrdersFromBatch 
			WHERE OrderID = @OrderID
			AND ISNULL(IsVoucherRedemption, 0) = 0
			GROUP BY	SectionType,
						TRTGenerationCode,
						IsVoucherRedemption,
						ProgramType
			
			WHILE EXISTS (SELECT TOP 1 SectionType, TotalTaxIncluded, TotalTaxExcluded, Tax, Tax2, TotalTax, TotalPostage, TRTGenerationCode, ProgramType FROM #Section) 
				BEGIN 
					SELECT @SectionType 	      = SectionType, 
						@TotalTaxIncluded   = TotalTaxIncluded, 
						@TotalTaxExcluded   = TotalTaxExcluded,
						@Tax                       = Tax,
						@Tax2		       = Tax2,
						@TotalTax                = TotalTax,
						@ItemCount	       = ItemCount,
						@TotalPostage      = TotalPostage,
						@TRTGenerationCode = TRTGenerationCode,
						@ProgramType = ProgramType
					FROM #Section
			
					IF ISNULL(@StaffOrderDiscount,-1) > 0 --*No longer used*--it's a staff order so include the tax in calculation.
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
							IF (@SectionType IN (2, 16)) --Section type - Magazine, Gift Card
							BEGIN --Mag section gets calculated by using @TotalTaxExcluded
									EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 1, @ItemCount, @ProfitPercentage  OUTPUT 
									
									IF @ProfitPercentage IS NULL
									BEGIN
										SET @ProfitPercentage = 0.37
									END

									SET @GroupProfitRate = @ProfitPercentage
									SET @GroupProfitAmount = Round(@ProfitPercentage*(@TotalTaxExcluded-@TotalPostage),2) --Added round Jan23,07
							END
							IF (@SectionType = 3) --Field Supplies
								BEGIN
									--Field Supply items's price does not include tax
									SET @TotalTaxExcluded = @TotalTaxIncluded
									SET @TotalTaxIncluded = @TotalTaxExcluded + @TotalTax

									SET @GroupProfitRate = 0
									SET @GroupProfitAmount = 0
								END
							IF (@SectionType = 6) --Section type - Inventory Product Without Tax
								BEGIN
									/*EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 3, @ItemCount, @ProfitPercentage  OUTPUT 
									SET @GroupProfitRate = IsNull(@ProfitPercentage,0)
									SET @GroupProfitAmount =Round (IsNull(@ProfitPercentage,0) *  @TotalTaxIncluded,2 ) --Added round Jan23,07*/

									SET @GroupProfitRate = 0
									SET @GroupProfitAmount = 0
									
									SET @TotalTaxExcluded = @TotalTaxIncluded  -- CatalogPrice without Tax
									SET @TotalTax = 0 --No Tax
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
							IF (@SectionType = 9) --Cookie Dough
								BEGIN
									EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 46018, @ItemCount, @ProfitPercentage  OUTPUT
									SET @GroupProfitRate = @ProfitPercentage
									--SET @GroupProfitRate = 0.40
									--SET @GroupProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE WHEN @ItemCount < 336 THEN 0.35 ELSE 0.40 END END
									SET @GroupProfitAmount = Round(@GroupProfitRate*(@TotalTaxExcluded),2)
								END
							IF (@SectionType = 10) --Popcorn
								BEGIN
									--SET @GroupProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE @IsFMAccount WHEN 1 THEN 0.00 ELSE 0.50 END END
									EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 46019, @ItemCount, @ProfitPercentage  OUTPUT
									SET @GroupProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE @IsFMAccount WHEN 1 THEN 0.00 ELSE CASE ISNULL(@ProfitPercentage, 0) WHEN 0 THEN 0.45 ELSE @ProfitPercentage END END END
									SET @GroupProfitAmount = Round(@GroupProfitRate*(@TotalTaxExcluded),2)
								END
							IF (@SectionType = 11) --The Cure Jewelry
								BEGIN
									--EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 3, @ProfitPercentage  OUTPUT 
									SET @GroupProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE @IsFMAccount WHEN 1 THEN 0.00 ELSE 0.35 END END
									SET @GroupProfitAmount = Round(@GroupProfitRate*(@TotalTaxIncluded),2)
									SET @ThirdPartyProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE @IsFMAccount WHEN 1 THEN 0.00 ELSE 0.05 END END
									SET @ThirdPartyProfitAmount = Round(@ThirdPartyProfitRate*(@TotalTaxIncluded),2)
								END
							IF (@SectionType = 12) --Shipping fees
								BEGIN
									SET @TotalTaxExcluded = @TotalTaxIncluded
									SET @TotalTaxIncluded = @TotalTaxExcluded + @TotalTax

									--No Profit for Shipping fees
									SET @GroupProfitRate = 0
									SET @GroupProfitAmount = 0
								END
							IF (@SectionType = 14) --TRT
								BEGIN
									IF @ProfitPercentage IS NULL
									BEGIN
										SET @ProfitPercentage = 0.37
									END
									
									IF @TRTGenerationCode IN ('2')
										SET @ProfitPercentage = 0.20
									ELSE IF @TRTGenerationCode IN ('0', 'N')
										SET @ProfitPercentage = 0.00
									ELSE
										SET @ProfitPercentage = 0.37
									
									IF @OrderQualifierID = 39022
									BEGIN
										SET @ProfitPercentage = 0.00
									END
									
									IF @IsFMAccount = 1
									BEGIN
										SET @ProfitPercentage = 0.00
									END
									
									SET @GroupProfitRate = @ProfitPercentage
									SET @GroupProfitAmount = Round (@ProfitPercentage*@TotalTaxIncluded,2) --Added Round jan23,07
								END
							IF (@SectionType = 15) --Savings Pass
								BEGIN
									EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 46024, @ItemCount, @ProfitPercentage  OUTPUT
									SET @GroupProfitRate = @ProfitPercentage

									IF @ProgramType = 30342
									BEGIN
										SET @GroupProfitRate = 0.50
									END

									SET @GroupProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE @IsFMAccount WHEN 1 THEN 0.00 ELSE CASE ISNULL(@GroupProfitRate, 0) WHEN 0 THEN 0.40 ELSE @GroupProfitRate END END END
									SET @GroupProfitAmount = Round(@GroupProfitRate*(@TotalTaxIncluded),2)
								END
							IF (@SectionType = 17) --Pretzel Rods 40%
								BEGIN
									SET @GroupProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE @IsFMAccount WHEN 1 THEN 0.00 ELSE 0.40 END END
									SET @GroupProfitAmount = Round(@GroupProfitRate*(@TotalTaxIncluded),2)

									SET @TotalTaxExcluded = @TotalTaxIncluded
									SET @TotalTaxIncluded = @TotalTaxExcluded + @TotalTax
								END
							IF (@SectionType = 18) --Pretzel Rods 30%
								BEGIN
									SET @GroupProfitRate = CASE @OrderQualifierID WHEN 39022 THEN 0.00 ELSE CASE @IsFMAccount WHEN 1 THEN 0.00 ELSE 0.30 END END
									SET @GroupProfitAmount = Round(@GroupProfitRate*(@TotalTaxIncluded),2)
								END
							ELSE --Section type - Inventory etc.
							IF (@SectionType  not in (2, 3, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16, 17, 18))
								BEGIN
									
									/*DECLARE @IsRunningFestival BIT
									SELECT	@IsRunningFestival = 1
									FROM	QSPCanadaCommon..CampaignProgram cp
									WHERE	cp.CampaignID = @CampaignID
									AND		cp.DeletedTF = 0
									AND		cp.ProgramID = 54 --Festival
									
									IF (@IsRunningFestival = 1)
										SET @ProfitPercentage = 0.40
									ELSE
										SET @ProfitPercentage = 0.45*/
									
									SET @ProfitPercentage = 0.40
									
									IF @ProgramType = 30327 --Donations
									BEGIN
										SET @ProfitPercentage = 0.75
									END
									
									IF @ProgramType = 30323 --Gifts We Love
									BEGIN
										SET @ProfitPercentage = 0.45
									END

									IF @ProgramType = 30329 --Chocolate Flyer
									BEGIN
										SET @ProfitPercentage = 0.45
									END

									IF @ProgramType = 30332 --Stainless Steel Travel Cup
									BEGIN
										SET @ProfitPercentage = 0.3667
									END

									IF @ProgramType = 30337 --Tervis Tumblers
									BEGIN
										SET @ProfitPercentage = 0.30
									END

									IF @ProgramType = 30345 --Cool Cards
									BEGIN
										SET @ProfitPercentage = 0.35
									END

									IF @ProgramType = 30346 --Rally
									BEGIN
										SET @ProfitPercentage = 0.30
									END
																
									IF @OrderQualifierID = 39022
									BEGIN
										SET @ProfitPercentage = 0.00
									END
									
									IF @IsFMAccount = 1
									BEGIN
										SET @ProfitPercentage = 0.00
									END
																		
									SET @GroupProfitRate = @ProfitPercentage
									SET @GroupProfitAmount = Round (@ProfitPercentage*@TotalTaxIncluded,2) --Added Round jan23,07
								END
						END

					IF @ThirdPartyProfitRate IS NULL
						SET @ThirdPartyProfitRate = 0.00

					IF @ThirdPartyProfitAmount IS NULL
						SET @ThirdPartyProfitAmount = 0.00

					EXEC AddFinanceInvoiceSection @InvoiceID, @SectionType, @TotalTaxIncluded, @TotalTaxExcluded,  
										   @GroupProfitRate, @GroupProfitAmount, @ThirdPartyProfitRate, @ThirdPartyProfitAmount, @TotalTax, @TotalPostage,@ItemCount, -999, @ProgramType,
										   @InvoiceSectionID OUTPUT, @TotalTaxableAmount OUTPUT, @DueAmount OUTPUT
					

					/*IF (@OrderQualifierID IN (39001,39002,39009)) --Old Way, based on group billing address
					BEGIN
					
						CREATE TABLE #TempTaxOld --Check taxid here for QC taxes and different calculation
						(
							TaxID int,
							TaxRate numeric(10,3)
						)

						--Get the tax rate and id for this campaign
						INSERT #TempTaxOld
						EXEC QSPCanadaCommon..GetTaxRateAndIDForCampaign @CampaignID, @SectionType
						
						WHILE EXISTS (SELECT DISTINCT TOP 1 TaxID, TaxRate FROM #TempTaxOld ORDER BY TaxID) 
							BEGIN 
								SELECT @TaxID 	      = TaxID, 
									@TaxRate	      = TaxRate
								FROM #TempTaxOld

								--Add invoice section tax record except Cookie Dough
								IF  @SectionType <> 6
								BEGIN
									IF (@TaxID = 3 or @TaxID = 10 or @TaxID = 8 or @TaxID = 9 )   --KET removed #6 Ontario as it is not PST as of 7/1/10
										BEGIN
											--PST
											EXEC AddInvoiceSectionTax @InvoiceSectionID, @TaxID, @TotalTaxableAmount, @TaxRate, @Tax2, -999
										END
									ELSE
										BEGIN
											EXEC AddInvoiceSectionTax @InvoiceSectionID, @TaxID, @TotalTaxableAmount, @TaxRate, @Tax, -999
										END
								END					
								DELETE FROM #TempTaxOld WHERE TaxID = @TaxID	
								
							END
						DROP TABLE #TempTaxOld
					END
					ELSE*/
					BEGIN --New Way, based on customer shipping address
						
						CREATE TABLE #TempTax
						(
							TaxID int,
							TaxRate numeric(10,3),
							TotalTaxableAmountForInvoiceSectionTaxID numeric(10,2),
							TaxForInvoiceSectionTaxID numeric(10,4)
						)

						INSERT		#TempTax							
						SELECT		T.TAX_ID, T.TAX_RATE, NULL, CASE WHEN T.TAX_ID IN (3, 8, 9, 10) THEN SUM(ISNULL(d.Tax2, 0.00)) ELSE SUM(ISNULL(d.Tax, 0.00)) END
						FROM		#TempBillableOrdersFromBatch d
						JOIN		QSPCanadaCommon..TaxProvince TP on TP.Province_Code = d.[State]
						JOIN		QSPCanadaCommon..TaxApplicableTax AppTax on AppTax.Country_Code = TP.Country_Code AND AppTax.Province_Code = TP.Province_Code AND AppTax.SECTION_TYPE_ID = d.SectionType
						JOIN		QSPCanadaCommon..Tax T on T.Tax_ID = TP.TAX_ID and AppTax.Tax_ID = T.Tax_ID
						WHERE		d.SectionType = @SectionType
						AND			d.OrderID = @OrderID
						AND			ISNULL(IsVoucherRedemption, 0) = 0
						AND			ProgramType = @ProgramType
						GROUP BY	T.TAX_ID,
									T.TAX_RATE
									
						--EXEC QSPCanadaCommon..GetTaxInfoForInvoiceSection @InvoiceSectionID
						/*SELECT	DISTINCT T.Tax_ID, T.TAX_RATE, TotalTaxableAmountForInvoiceSectionTaxID, TaxForInvoiceSectionTaxID
						FROM	QSPCanadaFinance..INVOICE_SECTION invSec
						JOIN	QSPCanadaOrderManagement..CustomerOrderDetail cod on cod.InvoiceNumber = invSec.INVOICE_ID and --need to get only cod's in the sectiontype, not sure how. cod.ProductType = invSec.SECTION_TYPE_ID
						JOIN	QSPCanadaOrderManagement..CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
						JOIN	QSPCanadaOrderManagement..Customer cust
									ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
															WHEN 0 THEN coh.CustomerBillToInstance
															ELSE		cod.CustomerShipToInstance
														END
						JOIN	QSPCanadaCommon..TaxProvince TP on TP.Province_Code = cust.[State]
						JOIN	QSPCanadaCommon..TaxApplicableTax AppTax on AppTax.Country_Code = TP.Country_Code AND AppTax.Province_Code = TP.Province_Code
						JOIN	QSPCanadaCommon..Tax T on T.Tax_ID = TP.TAX_ID and AppTax.Tax_ID = T.Tax_ID
						WHERE	invSec.Invoice_Section_ID = @InvoiceSectionID*/
						
						DECLARE @TotalTaxableAmountForInvoiceSectionTaxID numeric(10,2),
								@TaxForInvoiceSectionTaxID numeric(10,4)
						
						WHILE EXISTS (SELECT DISTINCT TOP 1 TaxID, TaxRate, TotalTaxableAmountForInvoiceSectionTaxID, TaxForInvoiceSectionTaxID FROM #TempTax ORDER BY TaxID) 
							BEGIN 
								SELECT @TaxID 									= TaxID, 
									@TaxRate									= TaxRate,
									@TotalTaxableAmountForInvoiceSectionTaxID	= TotalTaxableAmountForInvoiceSectionTaxID,
									@TaxForInvoiceSectionTaxID					= TaxForInvoiceSectionTaxID
								FROM #TempTax

								--Add invoice section tax record except Cookie Dough
								IF  @SectionType <> 6
								BEGIN
									IF (@TaxID = 3 or @TaxID = 10 or @TaxID = 8 or @TaxID = 9 )   --KET removed #6 Ontario as it is not PST as of 7/1/10
										BEGIN
											--PST
											EXEC AddInvoiceSectionTax @InvoiceSectionID, @TaxID, @TotalTaxableAmountForInvoiceSectionTaxID, @TaxRate, @TaxForInvoiceSectionTaxID, -999
										END
									ELSE
										BEGIN
											EXEC AddInvoiceSectionTax @InvoiceSectionID, @TaxID, @TotalTaxableAmountForInvoiceSectionTaxID, @TaxRate, @TaxForInvoiceSectionTaxID, -999
										END
								END					
								DELETE FROM #TempTax WHERE TaxID = @TaxID	
								
							END
							
						DROP TABLE #TempTax

					END					

					SET @TaxID		= null
					SET @TaxRate		= null
					SET @TotalTaxIncluded	= null
					SET @TotalTaxExcluded= null
					SET @Tax		= null
					SET @Tax2		= null
					SET @TotalTax		= null
					SET @ItemCount	=null
					SET @ProfitPercentage = null
					SET @GroupProfitRate = null
					SET @ThirdPartyProfitRate = null
					SET @ThirdPartyProfitAmount = null

					DELETE FROM #Section WHERE SectionType = @SectionType AND TRTGenerationCode = @TRTGenerationCode AND ProgramType = @ProgramType
				END
			DROP TABLE #Section
	
			--Update the invoice total amount record
			EXEC UpdateFinanceInvoice @InvoiceID, -999

			--Update batch IsInvoiced to true
			EXEC QSPCanadaOrderManagement..UpdateBatch_IsInvoiced @OrderID

			--Update Tracking table
			EXEC QSPCanadaOrderManagement.dbo.UpdateOrderStageTrackingForInvoiced @OrderID

			-- Update only those which are being invoiced  April 5, 2005 BY MS
			UPDATE  QSPCanadaOrderManagement.dbo.CustomerorderDetail
			SET  InvoiceNumber = @InvoiceID
			FROM  #TempBillableOrdersFromBatch
			WHERE  #TempBillableOrdersFromBatch.OrderId=@OrderID
			AND QSPCanadaOrderManagement.dbo.CustomerorderDetail.Transid =    #TempBillableOrdersFromBatch.TransId
			AND QSPCanadaOrderManagement.dbo.CustomerorderDetail.CustomerorderHeaderInstance =     #TempBillableOrdersFromBatch.Instance

			--Do GL stuff for Invoice
			EXEC GL_Entry_InsertInvoice @InvoiceID
			
			--Link Any existing Cust Service and Online Invoices to this one if applicable
			EXEC Invoice_LinkNonPrintedToPrinted @InvoiceID

			--Reset Variables
			SET @CampaignID 		= null
			SET @AccountID 		= null
			SET @ProductType		= null
			SET @Net			= null
			SET @ProfitPercentage 		= null
			SET @GroupProfitRate		= null
			SET @ThirdPartyProfitRate		= null
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
			SET @ThirdPartyProfitAmount	= null
			SET @InvoiceSectionID		= null
			SET @TotalTaxableAmount	= null
			SET @TaxID			= null
			SET @TaxRate			= null
			SET @InvoiceTotal		= 0.0 --total counter
			SET @DueAmount		= null
			SET @ItemCount		=null
			SET @OrderQualifierID = null
			SET @TRTGenerationCode = null
			
			--Keep loop going until no more recs
			DELETE FROM #TempOrderID WHERE OrderID = @OrderID
		END 
		-- Validation failed 
		DELETE FROM #TempOrderID WHERE OrderID = @OrderID
			
	END

	--Clean Up
	DROP TABLE #TempOrderID
	DROP TABLE #TempBillableOrdersFromBatch

	
	/*************************************************************************************************************************************************
	--Email List of orders failed to invoice 
	--Added March 21, 2006 MS
	**************************************************************************************************************************************************/
	If @@Error =0 
	Begin
	-- Insert error record into temp table and make ErrorLogFile and Email
		Declare @SQLcommand 	Varchar(1000)
		Declare @Filename		Varchar(100)
		Declare @Body 			Varchar(500)
		Declare @path 			Varchar(200)
		Declare @FileAttachment	Varchar(200)
		Declare @SendEmailTo		Varchar(400)
		Declare @Cnt 			Int

		Set @SendEmailTo =  'Debbie.Cyr@qsp.ca;Amanda.Cyr@qsp.ca;jmiles@gafundraising.com'


		Set @path = 'Q:\Projects\Paylater\QSPCAFinance\InvoiceErrorLogs\'

		Set @Filename =  'InvoiceGenerationLog_' + 
			Cast(Datepart(YEAR,	@RunDate) 	AS Varchar) +
			Cast(Datepart(MONTH,	@RunDate) 	AS Varchar) +
			Cast(Datepart(DAY,	@RunDate) 	AS Varchar) + 
			Cast(Datepart(HOUR,	@RunDate) 	AS Varchar) + 
			Cast(Datepart(MINUTE,	@RunDate)	AS Varchar) + 
			Cast(Datepart(SECOND,	@RunDate) 	AS Varchar)+'.txt'

		Select Distinct L.OrderId,Convert(Varchar(10),B.Date,101) OrderDate,
			L.AccountId,
			L.CamapaignId,
			InvoiceGenErrorMessage
		Into ##ErrorRecords
		From 	QSPCanadaFinance.dbo.InvoiceGenerationLog L (NOLOCK), 
			QSPCanadaOrderManagement..Batch B (NOLOCK)
		Where B.OrderId=L.OrderId
		And ISNULL(B.IsInvoiced, 0) = 0 
		And DateTimeCreated >=@RunDate
		Order By 2,1
		
		Select @Cnt = Count(*)	From ##ErrorRecords

		--If there are orders with error create a log file and email
		If @Cnt > 0 
		Begin

			Set @SQLcommand = 'bcp "tempdb.##ErrorRecords" out "Q:\Projects\Paylater\QSPCAFinance\InvoiceErrorLogs\' + @Filename + '" -S '+ @@servername + ' -c -q -T '
			Exec master..xp_cmdshell @SQLcommand

			Set @Body= 	'List of Orders, failed to invoice on '+
					Convert(Varchar(30),@RunDate,113)+Char(13)+Char(13)+
					'Please review attached file indicating OrderId,Date,AccountId,CampaignId and Reason respectively '+Char(13)+Char(13)	
				
			Set @FileAttachment = @path+@Filename

			Exec msdb.dbo.sysmail_start_sp --if tempdb fills up this turns off, so ensure it is started

			Exec  QSPCanadaCommon.dbo.Send_EMAIL_ATTACH 'InvoiceGeneration@QSP.com', @SendEmailTo,'Order Not Invoiced',@Body,@FileAttachment
			--exec msdb.dbo.sp_send_dbmail @profile_name = 'InvoiceGeneration',@recipients = @SendEmailTo, @subject='Order Not Invoiced',@file_attachments=@FileAttachment
		End

		Drop table ##ErrorRecords 

		--Update InvoiceGenerationlog for orders where invoice has already been generated
		Update QSPCanadaFinance.dbo.InvoiceGenerationLog
		Set IsFixed=1 , DateFixed=Getdate()
		From  QSPCanadaOrderManagement..batch B
		Where B.OrderId = QSPCanadaFinance.dbo.InvoiceGenerationLog.OrderId
		And QSPCanadaFinance.dbo.InvoiceGenerationLog.DateTimeCreated >= '01/01/2006' --Cast(Convert(Varchar(10),Getdate()-1,101)as dateTime)
		And B.Isinvoiced = 1
		And QSPCanadaFinance.dbo.InvoiceGenerationLog.IsFixed=0
	
	End

SET NOCOUNT OFF
GO
