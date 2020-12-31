USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetBillableOrdersFromBatch]    Script Date: 06/07/2017 09:17:15 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetBillableOrdersFromBatch]	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/14/2004 
--   Get Billable Orders For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT 
	B.ID, B.Date, B.AccountID, B.CampaignID, OrderID, A.Name, 
	CD1.Description as BatchStatus, CD2.Description as OrderType,
 	COH.Instance, COD.ProductCode,
 	COD.ProductName, COD.Quantity, 
	CASE PriceOverrideID
		WHEN 45001	Then 	(COD.Price) 		-- Coupon
		WHEN 45002	Then	(COD.CatalogPrice) 	-- Invalid Price (use catalog price)
		WHEN 45003	Then	(COD.Price) 		-- Closest Matching Offer
		WHEN 45004	Then  	(COD.Price) 		-- Straight Order (no override)
		ELSE    0 
	END as Price, 
	CASE PriceOverrideID
		WHEN 45001	Then 	(CASE COD.ProductType
						WHEN 46001 Then (COD.Price) 	                   --Mag
						WHEN 46002 Then (COD.Price*COD.Quantity) --Gift
						WHEN 46003 Then (COD.Price*COD.Quantity) --WFC
						WHEN 46005 Then (COD.Price*COD.Quantity) --Food
						WHEN 46006 Then (COD.Price*COD.Quantity) --Book
						WHEN 46007 Then (COD.Price*COD.Quantity) --Music
						ELSE 0			
					END) 	-- Coupon
		WHEN 45002	Then	(CASE COD.ProductType
						WHEN 46001 Then (COD.CatalogPrice)    		      --Mag
						WHEN 46002 Then (COD.CatalogPrice*COD.Quantity) --Gift
						WHEN 46003 Then (COD.CatalogPrice*COD.Quantity) --WFC
						WHEN 46005 Then (COD.CatalogPrice*COD.Quantity) --Food
						WHEN 46006 Then (COD.CatalogPrice*COD.Quantity) --Book
						WHEN 46007 Then (COD.CatalogPrice*COD.Quantity) --Music
						ELSE 0			
					END) 	-- Use Catalog Price when there is an Invalid Price 
		WHEN 45003	Then	(CASE COD.ProductType
						WHEN 46001 Then (COD.Price) 	                   --Mag
						WHEN 46002 Then (COD.Price*COD.Quantity) --Gift
						WHEN 46003 Then (COD.Price*COD.Quantity) --WFC
						WHEN 46005 Then (COD.Price*COD.Quantity) --Food
						WHEN 46006 Then (COD.Price*COD.Quantity) --Book
						WHEN 46007 Then (COD.Price*COD.Quantity) --Music
						ELSE 0		
					END)	-- Closest Matching Offer
		WHEN 45004	Then  	(CASE COD.ProductType
						WHEN 46001 Then (COD.Price) 	                   --Mag
						WHEN 46002 Then (COD.Price*COD.Quantity) --Gift
						WHEN 46003 Then (COD.Price*COD.Quantity) --WFC
						WHEN 46005 Then (COD.Price*COD.Quantity) --Food
						WHEN 46006 Then (COD.Price*COD.Quantity) --Book
						WHEN 46007 Then (COD.Price*COD.Quantity) --Music
						ELSE 0			
					END)	-- Straight Order (No Override)
		ELSE    0 
	END as  TotalPrice,
	COD.Tax, COD.Tax2, --Tax = GST, Tax2 = PST/HST
	COD.Net, COD.Gross, -- Net is without taxes, Gross includes taxes
	CD3.Description as PaymentMethod, PS.Type as SectionType, PST.Description as SectionTypeDescription, 
	IsTaxIncluded, IsPriceWithTax, IsIncentive
 FROM
	QSPCanadaOrderManagement..Batch B
	 LEFT JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	 LEFT JOIN QSPCanadaOrderManagement..CustomerOrderDetail COD on COD.CustomerOrderHeaderInstance = COH.Instance 
			AND COD.ProgramSectionID <> 4 --NOT Incentive Item
	 LEFT JOIN QSPCanadaOrderManagement..Account A on A.ID = B.AccountID
	 LEFT JOIN QSPCanadaProduct..ProgramSection PS on PS.ID = COD.ProgramSectionID
	 LEFT JOIN QSPCanadaProduct..ProgramSectionType PST on PST.ID = PS.Type
	 LEFT JOIN QSPCanadaOrderManagement..CustomerPaymentHeader CPH on CPH.CustomerOrderHeaderInstance = COH.Instance 
	 LEFT JOIN QSPCanadaOrderManagement..CreditCardPayment CCP on CCP.CustomerPaymentHeaderInstance = CPH.Instance
	 LEFT JOIN QSPCanadaCommon..CodeDetail CD1 on CD1.Instance = B.StatusInstance
	 LEFT JOIN QSPCanadaCommon..CodeDetail CD2 on CD2.Instance = B.OrderTypeCode
	 LEFT JOIN QSPCanadaCommon..CodeDetail CD3 on CD3.Instance = COH.PaymentMethodInstance
 WHERE 
	B.IsInvoiced = 0 -- not invoiced yet	
	AND
	(
			(--Magazine	
				COD.ProductType = 46001 --Magazine
				AND COD.StatusInstance = 507 --Sent To Remit ??				
				AND(
					(	--Non MagNet
						B.StatusInstance = 40004 --Approved
						AND B.OrderTypeCode NOT IN (41002, 41003, 41004, 41009) --CAFS, CREDITCM, DEBITCM, MAGNET
				        	)
				       	OR
					(	--MagNet
						B.StatusInstance = 40002 --In Progress
						AND B.OrderTypeCode = 41009 --MAGNET
				       	 )
				        )
			)
		OR
			(--GIFT
				COD.ProductType IN (46002, 46003, 46005,  46006, 46007) --Gift , WFC, Food, Book, Music
				AND COD.StatusInstance = 508 --Shipped ??
				AND B.StatusInstance = 40004 --Approved
				--new table ??
				--AND QuantityShipped > 0
				--AND QuantityShipped = COD.Quantity
					
			)
		OR
			(--Credit/Debit Memos only
				COD.StatusInstance = 502 --Approved and Paid??
				AND B.OrderTypeCode IN (41003, 41004) --CREDITCM, DEBITCM
				AND B.StatusInstance = 40004 --Approved
			)			
	         )
	
	AND ( --Payment
		(
			COH.PaymentMethodInstance <> 47002 --Credit Cards
			-- AND CCP.StatusInstance = ??? --Credit Card Accepted 101 and approved
		)
		OR
		(
			COH.PaymentMethodInstance = 47002 --Check
		)
	         )

	AND 
	        (
			SELECT CASE PriceOverrideID
				WHEN 45001	Then 	(COD.Price) 		-- Coupon
				WHEN 45002	Then	(COD.CatalogPrice) 	-- Invalid Price (Use Catalog Price)
				WHEN 45003	Then	(COD.Price) 		-- Closest Matching Offer
				WHEN 45004	Then  	(COD.Price) 		-- Straight Order
				else	-1 --need to populate PriceOverrideID field
			END				
	      ) <> 0 --Check Price Billed <> 0

	AND --Incentive calculated if order is incentive and associated campaign contains at least one incentive program.
	     (
			( IsIncentive = 0) -- include
		OR
			( IsIncentive = 1 and IncentiveCalculationStatus = 1 ) -- include				
	    )  

ORDER BY ORDERID, COD.ProductType, ProductName
	
SET NOCOUNT OFF
GO
