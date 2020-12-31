USE [QSPFulfillment]

SELECT	cart.EDS_Order_ID,
		o.Order_Date,
		o.Order_Status_ID
INTO	#MissingOrders
FROM	[ORDER] o,
		QSPEcommerce..Cart cart
WHERE	cart.X_Order_ID = o.Order_ID
--AND		o.Order_Status_ID IN (201, 301, 401, 501, 601, 701) --just in case
AND		cart.Eds_Order_ID IN (

)
ORDER BY	O.ORDER_DATE

SELECT	*
FROM	#MissingOrders

BEGIN TRAN T1

UPDATE		o
SET			o.Order_Status_ID = 101
FROM		[order] o,
			QSPEcommerce..Cart c,
			#MissingOrders mo
WHERE		c.X_Order_ID = o.Order_ID
AND			mo.EDS_Order_ID = c.EDS_Order_ID

COMMIT TRAN T1

--Once Extracted, set back to original status

SELECT		o.Order_Date,
			o.Order_Status_ID
FROM		[order] o,
			QSPEcommerce..Cart c,
			#MissingOrders mo
WHERE		c.X_Order_ID = o.Order_ID
AND			mo.EDS_Order_ID = c.EDS_Order_ID

BEGIN TRAN T2

UPDATE		o
SET			o.Order_Status_ID = mo.Order_Status_ID
FROM		[order] o,
			QSPEcommerce..Cart c,
			#MissingOrders mo
WHERE		c.X_Order_ID = o.Order_ID
AND			mo.EDS_Order_ID = c.EDS_Order_ID

COMMIT TRAN T2

EXEC QSPCanadaOrderManagement..pr_InternetOrder_SelectMissing

DROP TABLE	#MissingOrders