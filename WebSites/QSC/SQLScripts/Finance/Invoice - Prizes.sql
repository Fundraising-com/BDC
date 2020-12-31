USE [QSPCanadaFinance]
drop table #TempBillableOrdersFromBatch
--select * from invoice_Section order by invoice_id desc
--select top 2 * from invoice order by invoice_id desc
--Select * from qspcanadaordermanagement.dbo.codedetail where instance = 61005
declare @orderid int
select @orderid = 9963286
/*







*/
SELECT 
	COH.PaymentMethodInstance,
	CCP.StatusInstance as CreditCardStatus,
	CPH.PaymentBatchId,
	CPH.StatusInstance as PaymentHeaderStatus, 
	B.ID, B.Date,B.StatusInstance as BatchStatusId, B.AccountID, B.CampaignID, OrderID, IsNull(CheckPayableToQSPAmount,0) ChequePaymentAmount, A.Name,  Adr.StateProvince State, --ProvinceCode is from Adrress table (Acct's Shipto Address )MS July19 05
	CD1.Description as BatchStatus, CD2.Description as OrderType,
 	COH.Instance, COD.TransId,COD.StatusInstance as ItemStatus,COD.PricingDetailsId,COD.ProgramSectionId,COD.ProductCode, COD.ProductType,
	--Disabled MS 07Apr05
	/*CCP.StatusInstance CreditCardStatus, B.ID, B.Date, B.AccountID, B.CampaignID, OrderID, A.Name, 
	CD1.Description as BatchStatus, CD2.Description as OrderType,
 	COH.Instance, COD.ProductCode, COD.ProductType,*/
 	CASE COD.ProductType
		WHEN 46001 Then (COD.ProductName) 	    	 --Mag
		WHEN 46002 Then (Product_Description_Alt) 	--Gift
		WHEN 46003 Then (Product_Description_Alt) 	--WFC
		WHEN 46005 Then (Product_Description_Alt) 	--Food
		WHEN 46006 Then (COD.ProductName) 		--Book
		WHEN 46007 Then (COD.ProductName) 		--Music
		WHEN 46008 Then (COD.ProductName) 		--Kanata Prizes	
		WHEN 46010 Then (COD.ProductName) 		--MMB
		WHEN 46011 Then (COD.ProductName) 		--National
		WHEN 46012 Then (COD.ProductName) 		--Video
		ELSE (COD.ProductName)
	END as ProductName,
	COD.Quantity, 
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price) 	                 	 --Mag
		WHEN 46002 Then (COD.Price/COD.Quantity) 	--Gift
		WHEN 46003 Then (COD.Price/COD.Quantity) 	--WFC
		WHEN 46004 Then (COD.Price/COD.Quantity)	--Kanata (Bulk) Items may Include incentive MS Oct 11, 2005
		WHEN 46005 Then (COD.Price/COD.Quantity)	--Food
		WHEN 46006 Then (COD.Price/COD.Quantity) 	--Book
		WHEN 46007 Then (COD.Price/COD.Quantity) 	--Music
		WHEN 46008 Then (COD.Price/COD.Quantity) 	--Kanata Prizes	
		WHEN 46010 Then (COD.Price) 		      	--MMB
		WHEN 46011 Then (COD.Price/COD.Quantity) 	--National
		WHEN 46012 Then (COD.Price/COD.Quantity) 	--Video
		WHEN 46013 Then (COD.Price/COD.Quantity) 	--Incentive for Kanata 	MS Feb28,2007
		WHEN 46014 Then (COD.Price/COD.Quantity) 	--Incentive for Kanata
		WHEN 46015 Then (COD.Price/COD.Quantity) 	--Incentive for Kanata
		ELSE 0			
	END as Price, 
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price) --Mag
		WHEN 46002 Then (COD.Price) --Gift
		WHEN 46003 Then (COD.Price) --WFC
		WHEN 46004 Then (COD.Price) -- Kanata Bulk Items MS Oct 11, 2005
		WHEN 46005 Then (COD.Price) --Food
		WHEN 46006 Then (COD.Price) --Book
		WHEN 46007 Then (COD.Price) --Music
		WHEN 46008 Then (COD.Price) 	--Kanata Prizes	
		WHEN 46010 Then (COD.Price) --MMB
		WHEN 46011 Then (COD.Price) --National
		WHEN 46012 Then (COD.Price) --Video
		WHEN 46013 Then (COD.Price) 	--Incentive for Kanata  MS Feb28,2007
		WHEN 46014 Then (COD.Price) 	--Incentive for Kanata
		WHEN 46015 Then (COD.Price) 	--Incentive for Kanata
		ELSE 0			
	END as TotalPrice,
	COD.Tax, COD.Tax2, 	--Tax = GST/HST, Tax2 = PST
	COD.Net, COD.Gross, 	-- Net is without taxes, Gross includes taxes
	CD3.Description as PaymentMethod, PS.Type as SectionType, PST.Description as SectionTypeDescription, 
	IsTaxIncluded, IsPriceWithTax, IsIncentive, DateBatchCompleted,
	CPH.Instance CPHInstance,CPH.TotalAmount CPHTotalAmount,CCP.BatchId CCPaymentBatchId --MS Added Jan24, 2006
into #TempBillableOrdersFromBatch
FROM   QSPCanadaCommon.dbo.Address Adr RIGHT OUTER JOIN
       	QSPCanadaCommon.dbo.CAccount ShipAcc ON Adr.AddressListID = ShipAcc.AddressListID AND Adr.address_type = 54001 RIGHT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.Batch B ON ShipAcc.Id = B.ShipToAccountID LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.Campaign C ON C.ID = B.CampaignID LEFT OUTER JOIN
      	QSPCanadaOrderManagement.dbo.CustomerOrderHeader COH ON COH.OrderBatchDate = B.[Date] AND COH.OrderBatchID = B.ID LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CustomerOrderDetail COD ON COD.CustomerOrderHeaderInstance = COH.Instance AND COD.ProgramSectionID <> 4 LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CAccount A ON A.Id = B.AccountID LEFT OUTER JOIN
      	QSPCanadaProduct.dbo.ProgramSection PS ON PS.ID = COD.ProgramSectionID LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.ProgramSectionType PST ON PST.ID = PS.Type LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CustomerPaymentHeader CPH ON CPH.CustomerOrderHeaderInstance = COH.Instance LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CreditCardPayment CCP ON CCP.CustomerPaymentHeaderInstance = CPH.Instance LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD1 ON CD1.Instance = B.StatusInstance LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD2 ON CD2.Instance = B.OrderTypeCode LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD3 ON CD3.Instance = COH.PaymentMethodInstance LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.PRICING_DETAILS Price ON COD.PricingDetailsID = Price.MagPrice_Instance LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.ProductDescription Prod ON Prod.PRODUCT_CODE = Price.OracleCode 	AND ISNULL(Prod.LANGUAGE_CODE, 'EN') = ISNULL(C.Lang, 'EN')
WHERE 
	IsNull(B.IsInvoiced,0) = 0					-- Not Invoiced Yet	
--	AND B.OrderTypeCode NOT IN (41002)--, 41009) 				--CAFS, MAGNET
--	AND B.OrderQualifierID NOT IN (39012,39011,39008,39018,39019)  	--OrderCorrect, Internet Fix, Cust Serv, Kanata Psolver,Gift Psolver
	AND
	(
			(--Magazine	
				COD.ProductType = 46001 			--Magazine
				--AND COD.StatusInstance in ( 507, 512) 		--Sent To Remit, Unable to Remit - from bad address but school took money so invoice 	Disabled MS  7Apr05
				AND COD.StatusInstance in ( 502,507, 512,514,515) 	--502 will be checked in Invoice Generation Proc 		
				AND B.OrderTypeCode NOT IN (41003, 41004) 	--CREDITCM, DEBITCM
			)
		OR
			(--Gift , WFC, Food, Book, Music
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007,  46010, 46011, 46012) --Gift , WFC, Food, Book, Music, MMB, National, Video
				AND COD.StatusInstance in (508) -- Shipped  
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
				--Don't invoice for product not yet shipped
				AND QuantityShipped > 0
				AND QuantityShipped = COD.Quantity
			)
		OR
			(--Gift , WFC, Food, Book, Music
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007,  46010, 46011, 46012) --Gift , WFC, Food, Book, Music, MMB, National, Video
				AND COD.StatusInstance in (502,509,510,511) 
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
			)


		
		OR
			(--Book, Music Unable to ship
				COD.ProductType IN (46006, 46007) --Book, Music
				AND COD.StatusInstance in (513) --Unshippable - from bad address but school took money so invoice 		
				AND B.OrderTypeCode NOT IN (41003, 41004) --CREDITCM, DEBITCM
			)
		OR
			(--Credit/Debit Memos only
				B.OrderTypeCode IN (41003, 41004) --CREDITCM, DEBITCM
				AND B.StatusInstance = 40004 --Approved
				--COD.StatusInstance = 502 --Approved and Paid??
			)	
		OR 
			(--Invoicing for FM Kanata only MS Oct 07, 2005
			--(B.OrderTypeCode IN (41006, 41007,41011) --FM or FMBulk
			  B.OrderQualifierID in (39018, 39006)	   --Kanata
			 AND COD.StatusInstance in (508)   --Shipped
			 --AND QuantityShipped > 0
			)	
	
	)
	/*
	--  Check the payment status before invoicing.  Not generating any invoices for orders where there is a bad credit card.
	AND ( --Payment
		(
			COH.PaymentMethodInstance <> 50002 --Credit Cards = 50003 Visa and 50004 MC
			AND CCP.StatusInstance = 19000 --Credit Card good payment --should be 19000 for good.  19003 is pending.

		)
		OR
		(
			COH.PaymentMethodInstance = 50002 --Cash/Check
		)
	         )
	
*/
	AND 
	        (COD.Price <> 0 )--Check Price Billed <> 0

	AND --Incentive calculated if order is incentive and associated campaign contains at least one incentive program.
	     (
			(IsNull( IsIncentive,0) = 0) -- include
		OR
			( IsIncentive = 1 and IncentiveCalculationStatus = 1 ) -- include				
	    ) 
	--Disabled MS  7Apr05 see Generate Invoices Proc
	--AND B.StatusInstance in  (40013)--,40015) --Batch is good and Fulfilled
	--AND COD.PricingDetailsID <> 0 --Ignore these records. 0 Indicates there is a problem.
	--AND COD.ProgramSectionID <> 0 			-- Ignore these records. 0 Indicates there is a problem.
	AND B.StatusInstance in  (40013,40010,40012)  		--Batch is Fulfilled or Picked
	AND B.OrderQualifierID not in ( 39011)		 	-- Internet Fix
	AND B.OrderQualifierID not in ( 39021)			-- --Issue Tracker#1528 Time Staff
	AND COD.DelFlag=0					--Not logically Deleted
	--AND B.OrderTypeCode  NOT IN (41006,41007,41011) 		--donot invoice FM Kanata Orders
	AND COD.ProductCode <> 'NNNN'
	-- Disable Exclude Loonie Library Issue # 800 MS sept 22, 2006, these should be generated but not printed  
	AND C.ID NOT IN (Select CampaignId From QSPCanadaCommon.dbo.CampaignProgram Where ProgramId =24 And DeletedTf=0)
	AND orderid not in  ( 1005318,1006153,1008787,1010694,304595) --Old Pre Dec05 Orders
	AND orderid not in  ( 400012) --Old 2006 Orders not shipped
	AND orderid not in  (307670 ) --Issue Tracker #303
--and orderid in (507638, 507791	,507764	,507714	,507706	,507688	,507638	,507617	,507614	,507613	,507612	,507609	,507608	,507600	,507535	) 
and orderid = @orderid


declare @IsNotValid int
EXEC dbo.ValidateOrderForInvoiceGeneration  @orderid , @IsNotValid  OUTPUT
print @IsNotValid

--select * from qspcanadacommon.dbo.codedetail where instance=61005
--drop table #TempBillableOrdersFromBatch
select * from  #TempBillableOrdersFromBatch

--Start Generating Invoicesw
--DECLARE @OrderID 		int
DECLARE @CampaignID 	int
DECLARE @AccountID 		int
DECLARE @ProductType		int
DECLARE @COH int
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
--DECLARE @IsNotValid		int
DECLARE @RunDate		datetime
DECLARE @ItemCount		int

-- Get the Orders that are ready to be invoiced from udf function
-- Only include those orders where the batch has been completed at least 3 days ago to allow for payment information to be entered.
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
SELECT DISTINCT OrderId, CampaignID, AccountID, Instance INTO #TempOrderID FROM #TempBillableOrdersFromBatch
--select * from #TempOrderID
-- Create Index
CREATE INDEX OrderIDIndex2 on #TempOrderID (OrderID)

WHILE EXISTS (SELECT TOP 1 OrderID, CampaignID, AccountID,Instance FROM #TempOrderID) 
		BEGIN 
		          	SELECT @OrderID 	= OrderID, 
				@CampaignID	= CampaignID,  
				@AccountID	= AccountID ,
				@COH = Instance
			FROM #TempOrderID

		--	update qspcanadaordermanagement.dbo.CustomerOrderheader Set PaymentMethodInstance = 50002 where instance = @COH
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
								           @GroupProfitRate, @GroupProfitAmount, @TotalTax, 0,@ItemCount, -999, 
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
			EXEC GL_Entry_InsertInvoice @InvoiceID
					

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

		Set @SendEmailTo =  'qsp-finance-canada@qsp.com,qsp-IT-canada@qsp.com'


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
		Into tempdb.##ErrorRecords
		From 	QSPCanadaFinance.dbo.InvoiceGenerationLog L (NOLOCK), 
			QSPCanadaOrderManagement..Batch B (NOLOCK)
		Where B.OrderId=L.OrderId
		And B.IsInvoiced=0 
		And DateTimeCreated >='6/22/10'
		Order By 2,1
		
		Select @Cnt = Count(*)	From tempdb.##ErrorRecords

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
