USE [QSPCanadaFinance]
GO

SELECT	inv.Invoice_ID, e.GL_Entry_ID, t.GL_Transaction_ID, inv.Order_ID
INTO	#InvoicesToDelete
FROM	Invoice inv
left JOIN	GL_Entry e
			ON	e.Invoice_ID = inv.Invoice_ID
left JOIN	GL_Transaction t
			ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE	inv.Invoice_ID IN (
974141
)

SELECT	*
FROM	#InvoicesToDelete

BEGIN TRAN

/*
DELETE	sics
FROM	StatementInvoiceCustSvc sics
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = sics.InvoiceID

DELETE	sio
FROM	StatementInvoiceOnline sio
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = sio.InvoiceID

DELETE	si
FROM	StatementInvoice si
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = si.InvoiceID

*/

DELETE  GL_Transaction
FROM	GL_Transaction t
JOIN	#InvoicesToDelete atd
			ON	atd.GL_Entry_ID = t.GL_Entry_ID

DELETE  GL_Entry
FROM	GL_Entry e
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = e.Invoice_ID

DELETE  invProd
FROM	Invoice_By_QSP_Product invProd
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = invProd.Invoice_ID

DELETE  InvSecTax
FROM	Invoice_Section invSec
JOIN	Invoice_Section_Tax invSecTax
			ON	invSecTax.Invoice_Section_ID = invSec.Invoice_Section_ID
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = invSec.Invoice_ID

DELETE  InvSec
FROM	Invoice_Section invSec
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = invSec.Invoice_ID

UPDATE	inv
SET		inv.Printed_Invoice_ID = NULL
FROM	Invoice inv
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = inv.PRINTED_INVOICE_ID

DELETE  Invoice
FROM	Invoice inv
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = inv.Invoice_ID

UPDATE	cod
SET		InvoiceNumber = NULL
FROM	QSPCanadaOrderManagement..CustomerOrderDetail cod
JOIN	#InvoicesToDelete atd
			ON	atd.Invoice_ID = cod.InvoiceNumber

UPDATE	b
SET		IsInvoiced = 0
FROM	QSPCanadaOrderManagement..batch b
JOIN	#InvoicesToDelete atd
			ON	atd.Order_ID = b.OrderID

delete	ost
FROM	QSPCanadaOrderManagement..OrderStageTracking ost
JOIN	#InvoicesToDelete atd
			ON	atd.Order_ID = ost.OrderID
where	Stage = 59007

COMMIT TRAN

/*
SELECT	*
FROM	dbo.UDF_GetBillableOrdersFromBatch()
WHERE	OrderID = 916580
*/