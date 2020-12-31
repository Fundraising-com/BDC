USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GenerateInvoices]    Script Date: 10/06/2009 17:58:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE [dbo].[GenerateInvoices] 
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
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

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
DECLARE @TaxID 		int
DECLARE @TaxRate 		numeric(10,2)
DECLARE @DueAmount		numeric(10,2)
DECLARE @InvoiceTotal		numeric(10,2)
DECLARE @RetVal 		int
DECLARE @IsNotValid		int
DECLARE @RunDate		datetime
DECLARE @ItemCount		int

-- Get the Orders that are ready to be invoiced from udf function
-- Only include those orders where the batch has been completed at least 3 days ago to allow for payment information to be entered.
SELECT * INTO #TempBillableOrdersFromBatch 
FROM UDF_GetBillableOrdersFromBatch() 
WHERE ProductCode <> '9999' --Check that the item code isn't 9999
--AND DATEDIFF(dd, CONVERT(datetime, CONVERT(varchar, DateBatchCompleted,112)), CONVERT(datetime, CONVERT(varchar, GETDATE(),112))) >=3
--AND OrderID NOT IN (85312) -- problems with these orders 85758

-- Create Index
CREATE INDEX OrderIDIndex1 on #TempBillableOrdersFromBatch (OrderID)

--Check the payment status before invoicing.  Not generating any invoices for orders where there is a pending credit card.
DELETE #TempBillableOrdersFromBatch WHERE OrderID in (SELECT OrderID FROM #TempBillableOrdersFromBatch WHERE ISNULL(CreditCardStatus, 19000) IN (19003, 19004)) 
--Do not include any items where there is a bad credit card.  When/If the cc issue is corrected these items will be included in a problem solver order.
DELETE #TempBillableOrdersFromBatch WHERE ISNULL(CreditCardStatus, 19000) IN (19001, 19002, 19005) 

--select * from #TempBillableOrdersFromBatch

SELECT @RunDate =  GetDate()

-- Loop through records using unique orderid
SELECT DISTINCT OrderId, CampaignID, AccountID, OrderQualifierID INTO #TempOrderID FROM #TempBillableOrdersFromBatch
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
			
			--Insert Invoice Record
			EXEC AddFinanceInvoice @AccountID, @AccountType, @OrderID, -999, @InvoiceID OUTPUT					
			
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
				Case SectionType
					WHEN 2 THEN Count(*)
					ELSE Sum(Quantity)
				END as ItemCount
			INTO #Section
			FROM #TempBillableOrdersFromBatch 
			WHERE OrderID = @OrderID
			GROUP BY SectionType
			
			WHILE EXISTS (SELECT TOP 1 SectionType, TotalTaxIncluded, TotalTaxExcluded, Tax, Tax2, TotalTax FROM #Section) 
				BEGIN 
					SELECT @SectionType 	      = SectionType, 
						@TotalTaxIncluded   = TotalTaxIncluded, 
						@TotalTaxExcluded   = TotalTaxExcluded,
						@Tax                       = Tax,
						@Tax2		       = Tax2,
						@TotalTax                = TotalTax,
						@ItemCount	       = ItemCount
					FROM #Section
					
					IF ISNULL(@StaffOrderDiscount,-1) > 0 --it's a staff order so include the tax in calculation.
						BEGIN
							SET @GroupProfitRate 	 = 0
							SET @GroupProfitAmount = 0
							--SET @TotalTaxExcluded = convert(numeric(10,2),(@TotalTaxIncluded*@StaffOrderDiscount)-@TotalTax)    MS Jan 23,2007 Rounding issue For GroupSales and Profit
							SET @TotalTaxExcluded = Round ( (@TotalTaxIncluded*@StaffOrderDiscount)- @TotalTax,2)
							--SET @TotalTaxIncluded =  convert(numeric(10,2),@TotalTaxIncluded*@StaffOrderDiscount)
							SET @TotalTaxIncluded =  Round(@TotalTaxIncluded*@StaffOrderDiscount,2)
						END
					ELSE
						BEGIN
							IF (@SectionType = 2) --Section type - Magazine
							BEGIN --Mag section gets calculated by using @TotalTaxExcluded
									EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 1, @ProfitPercentage  OUTPUT 
									SET @GroupProfitRate = @ProfitPercentage
									SET @GroupProfitAmount = Round(@ProfitPercentage*@TotalTaxExcluded,2) --Added round Jan23,07
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
							ELSE --Section type - Inventory etc.
							IF (@SectionType  not in (2, 6,7))
								BEGIN --Gift section gets calculated by using @TotalTaxIncluded
									EXEC GetGroupProfitPercentage @OrderID, @CampaignID, 2, @ProfitPercentage  OUTPUT 
									SET @GroupProfitRate = @ProfitPercentage
									SET @GroupProfitAmount = Round (@ProfitPercentage*@TotalTaxIncluded,2) --Added Round jan23,07
								END
						END

					EXEC AddFinanceInvoiceSection @InvoiceID, @SectionType, @TotalTaxIncluded, @TotalTaxExcluded,  
								           @GroupProfitRate, @GroupProfitAmount, @TotalTax, @ItemCount, -999, 
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
								IF (@TaxID = 3 or @TaxID = 6 or @TaxID = 7 or @TaxID = 8 or @TaxID = 9 or @TaxID = 10)  
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
		
			--Loop through product types and insert an Invoice_By_QSP_Product record
			-- This is being handled by Generate_Gl_For_Invoice proc now.
			/*************************************************************************************************
			--SELECT ProductType, convert(numeric(10,2),sum(net)) as Net
			--INTO #ProductType
			--FROM #TempBillableOrdersFromBatch 
			--WHERE OrderID = @OrderID
			--GROUP BY ProductType
		
			--WHILE EXISTS (SELECT TOP 1 ProductType, Net FROM #ProductType) 
			--	BEGIN 
			--		SELECT @ProductType 	      = ProductType, 
			--			@Net		      = Net
			--		FROM #ProductType
			--
			--		--Add invoice by product 
			--	EXEC AddInvoiceByProduct @InvoiceID, @ProductType, @Net

			--		DELETE FROM #ProductType WHERE ProductType = @ProductType
			--	END
			--DROP TABLE #ProductType
			*********************************************************************************************************/

			
			--Update the invoice total amount record
			EXEC UpdateFinanceInvoice @InvoiceID, -999

			--Update batch IsInvoiced to true
			EXEC QSPCanadaOrderManagement..UpdateBatch_IsInvoiced @OrderID

			--Update Tracking table
			EXEC QSPCanadaOrderManagement.dbo.UpdateOrderStageTrackingForInvoiced @OrderID

			--Update InvoiceNumber for COD records.
			--EXEC QSPCanadaOrderManagement..UpdateCOD_InvoiceNumber @OrderID, @InvoiceID Disable on April 5, 2005 BY MS

			-- Update only those which are being invoiced  April 5, 2005 BY MS
			UPDATE  QSPCanadaOrderManagement.dbo.CustomerorderDetail
			SET  InvoiceNumber = @InvoiceID
			FROM  #TempBillableOrdersFromBatch
			WHERE  #TempBillableOrdersFromBatch.OrderId=@OrderID
			AND QSPCanadaOrderManagement.dbo.CustomerorderDetail.Transid =    #TempBillableOrdersFromBatch.TransId
			AND QSPCanadaOrderManagement.dbo.CustomerorderDetail.CustomerorderHeaderInstance =     #TempBillableOrdersFromBatch.Instance

			--Magnet Invoices.  They cannot be printed as they generate for tracking of Magnet orders only??  
			--The miscellaneous charges are likely for postage. As we bill the account $0.49 per mailing for Magnet Orders.	
			-- Misc charges are always with taxes
			--Add Misc charges for cc charge and magnet

			--Do GL stuff for Invoice
			EXEC Generate_Gl_For_Invoice @InvoiceID, @RetVal OUTPUT
			
			--Link Any existing Cust Service and Online Invoices to this one if applicable
			EXEC Invoice_LinkNonPrintedToPrinted @InvoiceID

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
		Declare @SendEmailTo		Varchar(100)
		Declare @Cnt 			Int

		Set @SendEmailTo =  'qsp-finance-canada@qsp.com;qsp-IT-canada@qsp.com;qsp-operations-canada@qsp.com'
		--Set @SendEmailTo =  'juan_martinez@qsp.com'


		Set @path = 'E:\Projects\Paylater\QSPCAFinance\InvoiceErrorLogs\' 

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
		And B.IsInvoiced=0 
		And DateTimeCreated >=@RunDate
		Order By 2,1
		
		Select @Cnt = Count(*)	From ##ErrorRecords

		--If there are orders with error create a log file and email
		If @Cnt > 0 
		Begin

			Set @SQLcommand = 'bcp "tempdb.##ErrorRecords" out "E:\Projects\Paylater\QSPCAFinance\InvoiceErrorLogs\' + @Filename + '" -c -q -T '
			Exec master..xp_cmdshell @SQLcommand

			Set @Body= 	'List of Orders, failed to invoice on '+
					Convert(Varchar(30),@RunDate,113)+Char(13)+Char(13)+
					'Please review attached file indicating OrderId,Date,AccountId,CampaignId and Reason respectively '+Char(13)+Char(13)	
				
			Set @FileAttachment = @path+@Filename

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



