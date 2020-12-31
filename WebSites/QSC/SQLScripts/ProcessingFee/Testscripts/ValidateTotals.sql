SELECT     Count(TransID), SUM(isnull(Net, 0)), SUM(isnull(Tax, 0)+isnull(Tax2, 0))
FROM         QSPCanadaOrderManagement..CustomerOrderDetail
WHERE     (ProductCode = 'PFEE') AND (InvoiceNumber <> 0)

SELECT     SUM(isnull(TOTAL_TAX_INCLUDED, 0)), SUM(isnull(TOTAL_TAX_EXCLUDED, 0)), SUM(isnull(TOTAL_TAX_AMOUNT, 0))
FROM         QSPCanadaFinance..INVOICE_SECTION
WHERE     (SECTION_TYPE_ID = 8)

SELECT     SUM(isnull(AMOUNT, 0)) AS Expr1
FROM         QSPCanadaFinance..GL_TRANSACTION
WHERE     (GLAccountID = 220)