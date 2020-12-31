IF object_id('tempdb..#TempBillableOrdersFromBatch ') IS NOT NULL
BEGIN
   DROP TABLE #TempBillableOrdersFromBatch
END

GO

IF object_id('tempdb..#TempOrderID ') IS NOT NULL
BEGIN
   DROP TABLE #TempOrderID
END

GO

DELETE FROM QSPCAnadaFinance..INVOICE_BY_QSP_PRODUCT
WHERE QSP_Product_Line_ID = 46017

DELETE FROM GL_Transaction
WHERE GLAccountID IN
	(SELECT GLAccountID 
	FROM GLAccountMap  
	WHERE GLEntryTypeID = 17)

GO

DELETE FROM QSPCanadaFinance..INVOICE_SECTION_TAX
WHERE Invoice_Section_ID In
	(SELECT INVOICE_Section_ID
	FROM QSPCanadaFinance..INVOICE_SECTION
	WHERE SECTION_TYPE_ID = 8)

GO
DECLARE @InvoiceID int
DECLARE cInvoiceCursor cursor for
	SELECT INVOICE_ID FROM INVOICE_SECTION WHERE SECTION_TYPE_ID = 8

OPEN cInvoiceCursor
fetch NEXT from cOrderHeaderCursor into @InvoiceID;

while @@fetch_status = 0
BEGIN
	
	DELETE FROM QSPCanadaFinance..Invoice_Section
	WHERE SECTION_TYPE_ID = 8 AND Invoice_ID = @InvoiceID;

	EXEC UpdateFinanceInvoice @InvoiceID, -999

	fetch NEXT from cOrderHeaderCursor into @InvoiceID;
END


GO







