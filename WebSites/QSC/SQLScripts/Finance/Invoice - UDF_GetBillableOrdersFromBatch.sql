USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetBillableOrdersFromBatch]    Script Date: 10/06/2009 17:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER      FUNCTION [dbo].[UDF_GetBillableOrdersFromBatch] ( )
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
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
RETURN
(SELECT 
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
	CPH.Instance CPHInstance,CPH.TotalAmount CPHTotalAmount,CCP.BatchId CCPaymentBatchId, --MS Added Jan24, 2006
	B.OrderQualifierID
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
COD.Quantity <> 0
and	IsNull(B.IsInvoiced,0) = 0					-- Not Invoiced Yet	
	AND B.OrderTypeCode NOT IN (41002)--, 41009) 				--CAFS, MAGNET
	AND B.OrderQualifierID NOT IN (39012,39011,39008,39018,39019)  	--OrderCorrect, Internet Fix, Cust Serv, Kanata Psolver,Gift Psolver
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


		/*OR
			(  --Gift , WFC, Food, Book, Music Picked or Pickable Added MS 7Apr05
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007,  46010, 46011, 46012) --Gift , WFC, Food, Book, Music, MMB, National, Video
				AND COD.StatusInstance in (510,511) -- Pickable,Picked See Gnerate Invoices Proc
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
			)*/
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
			--Invoicing for FM Kanata only MS Oct 07, 2005
			(B.OrderTypeCode IN (41006, 41007,41011) --FM or FMBulk
			 AND B.OrderQualifierID = 39006	   --Kanata
			 AND COD.StatusInstance in (508)   --Shipped
			 --AND QuantityShipped > 0
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
	--AND COD.PricingDetailsID <> 0 --Ignore these records. 0 Indicates there is a problem.
	--AND COD.ProgramSectionID <> 0 			-- Ignore these records. 0 Indicates there is a problem.
	AND B.StatusInstance in  (40013,40010,40012)  		--Batch is Fulfilled or Picked
	AND B.OrderQualifierID not in ( 39011)		 	-- Internet Fix
	AND B.OrderQualifierID not in ( 39021)			-- --Issue Tracker#1528 Time Staff
	AND COD.DelFlag=0					--Not logically Deleted
	AND B.OrderTypeCode  NOT IN (41006,41007,41011) 		--donot invoice FM Kanata Orders
	AND COD.ProductCode <> 'NNNN'
	-- Disable Exclude Loonie Library Issue # 800 MS sept 22, 2006, these should be generated but not printed  
	AND C.ID NOT IN (Select CampaignId From QSPCanadaCommon.dbo.CampaignProgram Where ProgramId =24 And DeletedTf=0)
	AND orderid not in  ( 1005318,1006153,1008787,1010694,304595) --Old Pre Dec05 Orders
	AND orderid not in  ( 400012) --Old 2006 Orders not shipped
	AND orderid not in  (307670 ) --Issue Tracker #303
--and orderid in (507638, 507791	,507764	,507714	,507706	,507688	,507638	,507617	,507614	,507613	,507612	,507609	,507608	,507600	,507535	) 
)--RETURN



















































