set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[pr_CustomerOrderDetail_SelectMissingOnlineSubs]

AS


		DECLARE	@now		smalldatetime
		DECLARE @MonthStart Int
		DECLARE @YearStart 	Int

SET		@now = getDate()

SELECT	@YearStart = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) = 1 THEN YEAR(CONVERT(smalldatetime,@now)) - 1
		WHEN MONTH(CONVERT(smalldatetime,@now)) >= 1 THEN YEAR(CONVERT(smalldatetime,@now))
		ELSE 0
		END,
		@MonthStart = CASE
		WHEN MONTH(CONVERT(smalldatetime,@now)) > 7 OR MONTH(CONVERT(smalldatetime,@now)) = 1 THEN 7
		WHEN MONTH(CONVERT(smalldatetime,@now)) BETWEEN 2 AND 7 THEN 2
		ELSE 0
		END


SELECT		cart.eds_Order_ID,
			ioi.CustomerOrderHeaderInstance,
			ci.Catalog_Item_Code,
			p.RemitCode,
			o.order_Date,
			o.Order_Status_ID,
			pa.First_Name,
			pa.Last_Name,
			pa.Address1,
			pa.Address2,
			pa.City,
			pa.Zip AS PostalCode
FROM		COM_OLTP1.QSPFulfillment.dbo.[ORDER] o
JOIN		COM_OLTP1.QSPEcommerce.dbo.Cart cart
				ON	cart.x_Order_ID = O.order_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Order_Detail od
				ON	od.Order_ID = o.Order_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Source s
				ON	o.Source_ID = s.Source_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Catalog_Item_Detail cid
				ON	cid.Catalog_Item_Detail_ID = od.Catalog_Item_Detail_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Catalog_Item ci
				ON	ci.Catalog_Item_ID = cid.Catalog_Item_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Shipment_Group sg
				ON	sg.Shipment_Group_ID = od.Shipment_Group_ID
JOIN		COM_OLTP1.QSPFulfillment.dbo.Postal_Address pa
				ON	pa.Postal_Address_ID = sg.Shipping_Postal_Address_ID
LEFT JOIN	InternetOrderID ioi
				ON	ioi.InternetOrderID = cart.Eds_Order_ID
LEFT JOIN	CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = ioi.CustomerOrderHeaderInstance
				AND	cod.Recipient = pa.First_Name + '  ' + pa.Last_Name
LEFT JOIN	QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
LEFT JOIN	QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
				AND	p.RemitCode = ci.Catalog_Item_Code	
WHERE		YEAR(o.order_date) >= @YearStart
AND			MONTH(o.order_date) >= @MonthStart
AND			o.Order_Status_ID IN (201, 301, 401, 501, 601, 701)
AND			s.Source_Group_ID = 3
AND			pd.MagPrice_Instance IS NULL
ORDER BY	o.Order_Date