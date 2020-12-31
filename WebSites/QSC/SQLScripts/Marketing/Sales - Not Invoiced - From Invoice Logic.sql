USE [QSPCanadaFinance]
GO

SELECT	b.OrderID, b.Date OrderDate, SUM(cod.Price) TotalPrice, (SELECT TOP 1 l.InvoiceGenErrorMessage FROM InvoiceGenerationLog l where l.ORDERID = b.OrderID AND l.datetimecreated >= '2016-11-09') ReasonNotInvoiced
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
       	QSPCanadaOrderManagement.dbo.CustomerPaymentHeader CPH ON CPH.CustomerOrderHeaderInstance = COH.Instance LEFT OUTER JOIN
       	QSPCanadaOrderManagement.dbo.CreditCardPayment CCP ON CCP.CustomerPaymentHeaderInstance = CPH.Instance LEFT OUTER JOIN
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
	AND B.OrderQualifierID NOT IN (39007,39012,39011,39008,39018,39019)  	--OrderCorrect, Internet Fix, Cust Serv, Kanata Psolver,Gift Psolver
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
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007,  46010, 46011, 46012, 46018, 46019, 46020, 46022) --Gift , WFC, Food, Book, Music, MMB, National, Video, Cookie Dough, Chocolate, Jewelry, Candles
--				AND COD.StatusInstance in (508) -- Shipped  
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
				--Don't invoice for product not yet shipped
				--AND QuantityShipped > 0
				--AND QuantityShipped = COD.Quantity
			)
		OR
			(--Gift , WFC, Food, Book, Music, Cookie Dough, Chocolate, Jewelry
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007,  46010, 46011, 46012, 46018, 46019, 46020, 46022) --Gift , WFC, Food, Book, Music, MMB, National, Video, Jewelry, Candles
--				AND COD.StatusInstance in (502,509,510,511) 
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
			)
		OR
			(--Field Supplies
				COD.ProductType IN (46004) --Field Supplies
--				AND COD.StatusInstance in (508) -- Shipped  
				AND B.OrderTypeCode NOT IN (41003, 41004) -- CREDITCM, DEBITCM
				--Don't invoice for product not yet shipped
--				AND QuantityShipped > 0
--				AND QuantityShipped = COD.Quantity
				AND B.OrderQualifierID NOT IN (39007) --Don't invoice Field Supplies that were automatically generated
				AND B.Date >= '2014-07-01'
			)
		OR
			(--Book, Music Unable to ship
				COD.ProductType IN (46006, 46007) --Book, Music
--				AND COD.StatusInstance in (513) --Unshippable - from bad address but school took money so invoice 		
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
--			 AND COD.StatusInstance in (508)   --Shipped
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
--				AND COD.StatusInstance in (508, 513) -- Shipped, Unshippable  
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
--	AND B.StatusInstance in  (40013,40010,40012,40014)  		--Batch is Fulfilled or Picked
	AND B.OrderQualifierID not in ( 39011)		 	-- Internet Fix
	AND B.OrderQualifierID not in ( 39021)			-- --Issue Tracker#1528 Time Staff
	AND COD.DelFlag=0					--Not logically Deleted
	--AND B.OrderTypeCode  NOT IN (41006,41007,41011) 		--donot invoice FM Kanata Orders
	AND COD.ProductCode <> 'NNNN'
	AND ISNULL(COD.PricingDetailsID, 0) <> 0
	-- Disable Exclude Loonie Library Issue # 800 MS sept 22, 2006, these should be generated but not printed  
	AND C.ID NOT IN (Select CampaignId From QSPCanadaCommon.dbo.CampaignProgram Where ProgramId =24 And DeletedTf=0)
	AND b.orderid not in  ( 1005318,1006153,1008787,1010694,304595) --Old Pre Dec05 Orders
	AND b.orderid not in  ( 400012) --Old 2006 Orders not shipped
	AND b.orderid not in  (307670 ) --Issue Tracker #303
	and ProductCode <> '9999'
	and b.StatusInstance not in (40001, 40002, 40004, 40005)
GROUP BY b.OrderID, b.Date
ORDER BY b.OrderID