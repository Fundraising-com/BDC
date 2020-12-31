USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetBillableOrdersFromBatch]    Script Date: 06/07/2017 09:17:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      FUNCTION [dbo].[UDF_GetBillableOrdersFromBatch] ( )
RETURNS TABLE
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/23/2004 
--   Get Billable Orders For Canada Finance System
--   MTC 8/13/2004
--   Using QSPCanadaProduct..ProductDescription for Gift, WFC, Choc Product Names.  MMB will still use COD.ProductName.
--   MTC 10/1/2004
--   Allowing Mag order items with 512 status (Unable to remit) to be invoiced
--   MTC 10/25/2004
--   Checking the payment status now in GenerateInvoices.  Not generating any invoices for orders where there is a bad credit card.
--   MTC 11/30/2004
--   Do not Invoice Gift Supplemental Orders - per KT.
--   MTC 12/13/2004
--   Do not invoice Internet Fix (39011) Orders.
--	 CRL 8/2/2011
--	 Additional cases for proecessing fees (Product 46017)
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
RETURN
(SELECT 
	COH.PaymentMethodInstance,
	CCP.StatusInstance as CreditCardStatus,
	CPH.PaymentBatchId,
	CPH.StatusInstance as PaymentHeaderStatus, 
	B.ID, 
	B.Date,
	B.StatusInstance as BatchStatusId, 
	B.AccountID, 
	B.CampaignID, 
	OrderID, 
	IsNull(CheckPayableToQSPAmount,0) ChequePaymentAmount, 
	A.Name,  
	--Adr.StateProvince State, --ProvinceCode is from Adrress table (Acct's Shipto Address )MS July19 05
	CASE cod.IsShippedToAccount WHEN 1 THEN Adr.StateProvince ELSE cust.[State] END [State],
	CD1.Description as BatchStatus, 
	CD2.Description as OrderType,
 	COH.Instance, 
	COD.TransId,
	COD.StatusInstance as ItemStatus,
	COD.PricingDetailsId,
	COD.ProgramSectionId,
	COD.ProductCode, 
	COD.ProductType,
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
		WHEN 46017 Then (COD.Price)					--Processing fees, independant of quantity
		WHEN 46018 Then (COD.Price/COD.Quantity)	--Cookie Dough
		WHEN 46019 Then (COD.Price/COD.Quantity)	--Chocolate
		WHEN 46020 Then (COD.Price/COD.Quantity)	--Jewelry
		WHEN 46021 Then (COD.Price/COD.Quantity)	--Shipping
		WHEN 46022 Then (COD.Price/COD.Quantity) 	--Candle
		WHEN 46023 Then (COD.Price/COD.Quantity) 	--TRT
		WHEN 46024 Then (COD.Price/COD.Quantity) 	--Savings Pass
		WHEN 46025 Then (COD.Price/COD.Quantity) 	--Pretzel Rods
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
		WHEN 46017 Then (COD.Price)		--Processing fees
		WHEN 46018 Then (COD.Price)		--Cookie Dough
		WHEN 46019 Then (COD.Price)		--Chocolate
		WHEN 46020 Then (COD.Price)		--Jewelry
		WHEN 46021 Then (COD.Price)		--Shipping
		WHEN 46022 Then (COD.Price)		--Candle
		WHEN 46023 Then (COD.Price)		--TRT
		WHEN 46024 Then (COD.Price)		--Entertainment
		WHEN 46025 Then (COD.Price)		--Pretzel Rods
		ELSE 0			
	END as TotalPrice,
	COD.Tax, COD.Tax2, 	--Tax = GST/HST, Tax2 = PST
	COD.Net, COD.Gross, 	-- Net is without taxes, Gross includes taxes
	CD3.Description as PaymentMethod, 
	CASE COD.ProductType
		WHEN 46017 THEN 8 --Proc Fee switch from 2 to 8 so GP is not included
		ELSE			PS.Type
	END AS SectionType,
	CASE COD.ProductType
		WHEN 46017 THEN 'Processing fees' --Proc Fee switch from 2 to 8 so GP is not included
		ELSE			PST.Description
	END AS SectionTypeDescription,
	IsTaxIncluded, 
	IsPriceWithTax, 
	IsIncentive, 
	DateBatchCompleted,
	CPH.Instance CPHInstance,
	CPH.TotalAmount CPHTotalAmount,
	CCP.BatchId CCPaymentBatchId, --MS Added Jan24, 2006
	isnull(Price.PostageAmount,0) *  isnull(Price.PostageRemitRate,0) * isnull(Price.ConversionRate,0)  as PostageAmount,
	b.OrderQualifierID,
	A.CAccountCodeClass,
	CASE cod.ProductType WHEN 46023 THEN ISNULL(coh.TRTGenerationCode, 1) ELSE NULL END AS TRTGenerationCode,
	cod.IsVoucherRedemption,
	PM.SubType ProgramType
FROM   QSPCanadaCommon.dbo.Address Adr RIGHT OUTER JOIN
       	QSPCanadaCommon.dbo.CAccount ShipAcc ON Adr.AddressListID = ShipAcc.AddressListID AND Adr.address_type = 54001 RIGHT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.Batch B ON ShipAcc.Id = B.ShipToAccountID LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.Campaign C ON C.ID = B.CampaignID LEFT OUTER JOIN
      	QSPCanadaOrderManagement.dbo.CustomerOrderHeader COH ON COH.OrderBatchDate = B.[Date] AND COH.OrderBatchID = B.ID LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CustomerOrderDetail COD ON COD.CustomerOrderHeaderInstance = COH.Instance AND COD.ProgramSectionID <> 4 LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CAccount A ON A.Id = B.AccountID LEFT OUTER JOIN
      	QSPCanadaProduct.dbo.ProgramSection PS ON PS.ID = COD.ProgramSectionID LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.ProgramSectionType PST ON PST.ID = PS.Type LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.PROGRAM_MASTER PM ON PM.Program_ID = PS.Program_ID LEFT OUTER JOIN
       	(QSPCanadaOrderManagement.dbo.CustomerPaymentHeader CPH JOIN
       		QSPCanadaOrderManagement.dbo.CreditCardPayment CCP ON CCP.CustomerPaymentHeaderInstance = CPH.Instance AND SUBSTRING(ISNULL(CCP.CreditCardNumber,0),1,1)<>'9')
			ON CPH.CustomerOrderHeaderInstance = COH.Instance LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD1 ON CD1.Instance = B.StatusInstance LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD2 ON CD2.Instance = B.OrderTypeCode LEFT OUTER JOIN
       	QSPCanadaCommon.dbo.CodeDetail CD3 ON CD3.Instance = COH.PaymentMethodInstance LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.PRICING_DETAILS Price ON COD.PricingDetailsID = Price.MagPrice_Instance LEFT OUTER JOIN
       	QSPCanadaProduct.dbo.ProductDescription Prod ON Prod.PRODUCT_CODE = Price.OracleCode 	AND ISNULL(Prod.LANGUAGE_CODE, 'EN') = ISNULL(C.Lang, 'EN') LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END

WHERE 
	IsNull(B.IsInvoiced,0) = 0					-- Not Invoiced Yet	
	and COD.Quantity <> 0
	--AND B.OrderTypeCode NOT IN (41002)--, 41009) 				--CAFS, MAGNET
	AND B.OrderQualifierID NOT IN (39012,39011,39008,39018,39019)  	--OrderCorrect, Internet Fix, Cust Serv, Kanata Psolver,Gift Psolver
	AND
	(
			(--Magazine	
				COD.ProductType = 46001 			--Magazine
				--AND COD.StatusInstance in ( 507, 512) 		--Sent To Remit, Unable to Remit - from bad address but school took money so invoice 	Disabled MS  7Apr05
				AND (COD.StatusInstance in ( 502,507, 508, 512,514,515,517) 	--502 will be checked in Invoice Generation Proc 		
					OR COD.StatusInstance in (501) AND COD.ProductCode LIKE 'D%' AND ISNULL(cust.Email, '') = '') --Invoice Digital Subs missing email address as school collected money
				AND B.OrderTypeCode NOT IN (41003, 41004) 	--CREDITCM, DEBITCM
			)
		OR
			(--Gift , WFC, Food, Book, Music, Cookie Dough, Chocolate, Jewelry, Candles
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007,  46010, 46011, 46012, 46018, 46019, 46020, 46022, 46025) --Gift , WFC, Food, Book, Music, MMB, National, Video, Cookie Dough, Chocolate, Jewelry, Candles, Pretzel Rods
				AND COD.StatusInstance in (508) -- Shipped  
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
				--Don't invoice for product not yet shipped
				--AND QuantityShipped > 0
				--AND QuantityShipped = COD.Quantity
			)
		OR
			(--Gift , WFC, Food, Book, Music, Cookie Dough, Chocolate, Jewelry
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007,  46010, 46011, 46012, 46018, 46019, 46020, 46022, 46025) --Gift , WFC, Food, Book, Music, MMB, National, Video, Jewelry, Candles, Pretzel Rods
				AND COD.StatusInstance in (502,509,510,511) 
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
			)
		OR
			(--Field Supplies
				COD.ProductType IN (46004) --Field Supplies
				AND COD.StatusInstance in (508) -- Shipped  
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
				--Don't invoice for product not yet shipped
				AND QuantityShipped > 0
				AND QuantityShipped = COD.Quantity
				AND B.Date >= '2014-07-01'
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
			(B.OrderQualifierID = 39006	   --PAP/Draw Prizes
			 AND COD.StatusInstance in (502, 508, 509, 510, 511)   --Shipped
			 AND COD.ProductType NOT IN (46004)
			 AND B.Date >= '2013-07-01' --Prize Overage Process retired and order invoiced right away
			)
		OR
			--Invoicing for Processing fees, Shipping
			(
				COD.ProductType IN (46017, 46021)			 --Processing fee, Shipping
				--AND ISNULL(COD.IsVoucherRedemption, 0) = 0
				AND COD.StatusInstance = 502				--Order Status good
				AND B.OrderTypeCode NOT IN (41003, 41004) 	--CREDITCM, DEBITCM
			)
		OR
			(--TRT, Entertainment
				COD.ProductType IN (46023, 46024)
				AND COD.StatusInstance in (502, 508, 509, 510, 511, 513)
			)
	)
	
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
	--AND COD.PricingDetailsID <> 0 --Ignore these records. 0 Indicates there is a problem.	--AND COD.ProgramSectionID <> 0 			-- Ignore these records. 0 Indicates there is a problem.
	AND B.StatusInstance in  (40013,40010,40012,40014)  		--Batch is Fulfilled or Picked
	AND B.OrderQualifierID not in ( 39011)		 	-- Internet Fix
	AND B.OrderQualifierID not in ( 39021)			-- --Issue Tracker#1528 Time Staff
	AND COD.DelFlag=0					--Not logically Deleted
	--AND B.OrderTypeCode  NOT IN (41006,41007,41011) 		--donot invoice FM Kanata Orders
	AND COD.ProductCode <> 'NNNN'
	AND ISNULL(COD.PricingDetailsID, 0) <> 0
	-- Disable Exclude Loonie Library Issue # 800 MS sept 22, 2006, these should be generated but not printed  
	AND C.ID NOT IN (Select CampaignId From QSPCanadaCommon.dbo.CampaignProgram Where ProgramId =24 And DeletedTf=0)
	AND orderid not in  ( 1005318,1006153,1008787,1010694,304595) --Old Pre Dec05 Orders
	AND orderid not in  ( 400012) --Old 2006 Orders not shipped
	AND orderid not in  (307670 ) --Issue Tracker #303
	--and ISNULL(coh.FormCode, '0') not in ('000I')
--and orderid in (507638, 507791	,507764	,507714	,507706	,507688	,507638	,507617	,507614	,507613	,507612	,507609	,507608	,507600	,507535	) 
)--RETURN
GO
