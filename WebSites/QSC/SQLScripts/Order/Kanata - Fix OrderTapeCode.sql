USE QSPCanadaOrderManagement

BEGIN TRANSACTION

UPDATE	batch
SET		OrderTypeCode = 41011
FROM	Batch batch
JOIN	QSPCanadaFinance..Invoice inv
			ON	inv.Order_ID = batch.OrderID
WHERE	inv.Invoice_ID in (279963)

COMMIT TRANSACTION