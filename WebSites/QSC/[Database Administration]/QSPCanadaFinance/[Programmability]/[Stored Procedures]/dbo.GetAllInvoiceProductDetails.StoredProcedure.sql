USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllInvoiceProductDetails]    Script Date: 06/07/2017 09:17:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllInvoiceProductDetails]
	@InvoiceID	numeric(10,2),
	@OrderID numeric(10,2) = null
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/6/2004 
--   Get Invoice Product Details List For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON
IF (@InvoiceID = 0)
BEGIN
SELECT COD.ProductCode, COD.ProductName,  COD.CatalogPrice, COD.Quantity as 'NumIssues',
	CASE COD.ProductType
		WHEN 46001 Then (convert(numeric(10,2),COD.Price)) 	                   --Mag
		WHEN 46002 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Gift
		WHEN 46003 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --WFC
		WHEN 46005 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Food
		WHEN 46006 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Book
		WHEN 46007 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Music
		WHEN 46010 Then (convert(numeric(10,2),COD.Price)) --MMB
		WHEN 46011 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --National
		WHEN 46012 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Video
		ELSE 0			
	END as Price, 
	CASE COD.ProductType
		WHEN 46001 Then (Count(*)) 	   --Mag
		WHEN 46002 Then (COD.Quantity) --Gift
		WHEN 46003 Then (COD.Quantity) --WFC
		WHEN 46005 Then (COD.Quantity) --Food
		WHEN 46006 Then (COD.Quantity) --Book
		WHEN 46007 Then (COD.Quantity) --Music
		WHEN 46010 Then (Count(*)) --MMB
		WHEN 46011 Then (COD.Quantity) --National
		WHEN 46012 Then (COD.Quantity) --Video
	END AS 'QTYOrdered',
	CASE COD.ProductType
		WHEN 46001 Then (Count(*)) 	   	  --Mag
		WHEN 46002 Then (COD.QuantityShipped) --Gift
		WHEN 46003 Then (COD.QuantityShipped) --WFC
		WHEN 46005 Then (COD.QuantityShipped) --Food
		WHEN 46006 Then (COD.QuantityShipped) --Book
		WHEN 46007 Then (COD.QuantityShipped) --Music
		WHEN 46010 Then (Count(*)) --MMB
		WHEN 46011 Then (COD.QuantityShipped) --National
		WHEN 46012 Then (COD.QuantityShipped) --Video
	END AS 'QuantityShipped',
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price*Count(*)) --Mag
		WHEN 46002 Then (COD.Price) --Gift
		WHEN 46003 Then (COD.Price) --WFC
		WHEN 46005 Then (COD.Price) --Food
		WHEN 46006 Then (COD.Price) --Book
		WHEN 46007 Then (COD.Price) --Music
		WHEN 46010 Then (COD.Price*Count(*)) --MMB
		WHEN 46011 Then (COD.Price) --National
		WHEN 46012 Then (COD.Price) --Video
		ELSE 0			
	END as TotalPrice
FROM QSPCanadaOrderManagement..Batch B 
	LEFT JOIN Invoice I on B.OrderID = I.Order_ID
	LEFT JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	LEFT JOIN QSPCanadaOrderManagement..CustomerOrderDetail COD on COD.CustomerOrderHeaderInstance = COH.Instance 
	LEFT JOIN QSPCanadaOrderManagement..CustomerPaymentHeader CPH on CPH.CustomerOrderHeaderInstance = COH.Instance 
	LEFT JOIN QSPCanadaOrderManagement..CreditCardPayment CCP on CCP.CustomerPaymentHeaderInstance = CPH.Instance
	LEFT JOIN QSPCanadaProduct..Pricing_Details Price on COD.PricingDetailsID = MagPrice_Instance
WHERE  B.OrderID= @OrderID
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
	AND COD.PricingDetailsID <> 0 --Ignore these records. 0 Indicates there is a problem.
GROUP BY COD.ProductCode, COD.ProductName, COD.ProductType, COD.Quantity, COD.CatalogPrice, PriceOverrideID, COD.Price, COD.QuantityShipped
ORDER BY ProductName
END
ELSE
BEGIN
SELECT COD.ProductCode, COD.ProductName,  COD.CatalogPrice, COD.Quantity as 'NumIssues',
	CASE COD.ProductType
		WHEN 46001 Then (convert(numeric(10,2),COD.Price)) 	                   --Mag
		WHEN 46002 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Gift
		WHEN 46003 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --WFC
		WHEN 46005 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Food
		WHEN 46006 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Book
		WHEN 46007 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Music
		WHEN 46010 Then (convert(numeric(10,2),COD.Price)) --MMB
		WHEN 46011 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --National
		WHEN 46012 Then (convert(numeric(10,2),COD.Price/COD.Quantity)) --Video
		ELSE 0			
	END as Price, 
	CASE COD.ProductType
		WHEN 46001 Then (Count(*)) 	   --Mag
		WHEN 46002 Then (COD.Quantity) --Gift
		WHEN 46003 Then (COD.Quantity) --WFC
		WHEN 46005 Then (COD.Quantity) --Food
		WHEN 46006 Then (COD.Quantity) --Book
		WHEN 46007 Then (COD.Quantity) --Music
		WHEN 46010 Then (Count(*)) --MMB
		WHEN 46011 Then (COD.Quantity) --National
		WHEN 46012 Then (COD.Quantity) --Video
	END AS 'QTYOrdered',
	CASE COD.ProductType
		WHEN 46001 Then (Count(*)) 	   	  --Mag
		WHEN 46002 Then (COD.QuantityShipped) --Gift
		WHEN 46003 Then (COD.QuantityShipped) --WFC
		WHEN 46005 Then (COD.QuantityShipped) --Food
		WHEN 46006 Then (COD.QuantityShipped) --Book
		WHEN 46007 Then (COD.QuantityShipped) --Music
		WHEN 46010 Then (Count(*)) --MMB
		WHEN 46011 Then (COD.QuantityShipped) --National
		WHEN 46012 Then (COD.QuantityShipped) --Video
	END AS 'QuantityShipped',
	CASE COD.ProductType
		WHEN 46001 Then (COD.Price*Count(*)) --Mag
		WHEN 46002 Then (COD.Price) --Gift
		WHEN 46003 Then (COD.Price) --WFC
		WHEN 46005 Then (COD.Price) --Food
		WHEN 46006 Then (COD.Price) --Book
		WHEN 46007 Then (COD.Price) --Music
		WHEN 46010 Then (COD.Price*Count(*)) --MMB
		WHEN 46011 Then (COD.Price) --National
		WHEN 46012 Then (COD.Price) --Video
		ELSE 0			
	END as TotalPrice
FROM QSPCanadaOrderManagement..Batch B 
	LEFT JOIN Invoice I on B.OrderID = I.Order_ID
	LEFT JOIN QSPCanadaOrderManagement..CustomerOrderHeader COH on COH.OrderBatchDate = B.Date AND COH.OrderBatchID = B.ID 
	LEFT JOIN QSPCanadaOrderManagement..CustomerOrderDetail COD on COD.CustomerOrderHeaderInstance = COH.Instance 
	LEFT JOIN QSPCanadaOrderManagement..CustomerPaymentHeader CPH on CPH.CustomerOrderHeaderInstance = COH.Instance 
	LEFT JOIN QSPCanadaOrderManagement..CreditCardPayment CCP on CCP.CustomerPaymentHeaderInstance = CPH.Instance
	LEFT JOIN QSPCanadaProduct..Pricing_Details Price on COD.PricingDetailsID = MagPrice_Instance
WHERE Invoice_ID = @InvoiceID
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
	AND COD.PricingDetailsID <> 0 --Ignore these records. 0 Indicates there is a problem.
GROUP BY COD.ProductCode, COD.ProductName, COD.ProductType, COD.Quantity, COD.CatalogPrice, PriceOverrideID, COD.Price, COD.QuantityShipped
ORDER BY ProductName
END
SET NOCOUNT OFF
GO
