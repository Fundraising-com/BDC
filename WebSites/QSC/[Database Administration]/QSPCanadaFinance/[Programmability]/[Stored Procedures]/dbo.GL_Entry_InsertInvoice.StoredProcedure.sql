USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Entry_InsertInvoice]    Script Date: 06/07/2017 09:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_Entry_InsertInvoice]

	@InvoiceID	INT

AS

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--	 CRL 8/2/2011
--	 Additional block for inserting processing fees transactions
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

EXEC	Invoice_By_QSP_Product_Insert
		@InvoiceID = @InvoiceID

DECLARE	@GLEntryID			INT,
		@InvoiceDescription	VARCHAR(20),
		@CountryCode		VARCHAR(10),
		@BusinessUnitID		INT,
		@GlEntryTypeId		int

SET @InvoiceDescription = 'Invoice'
SET	@CountryCode = 'CA'
SET @GlEntryTypeId = 17

SELECT	@BusinessUnitID = acc.BusinessUnitID
FROM	Invoice inv
JOIN	QSPCanadaOrderManagement..Batch b
			ON	b.OrderID = inv.Order_ID
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = b.AccountID
WHERE	inv.Invoice_ID = @InvoiceID

BEGIN TRANSACTION

EXEC	GL_Entry_Insert
		@InvoiceID = @InvoiceID,
		@Description = @InvoiceDescription,
		@CountryCode = @CountryCode,
		@BusinessUnitID = @BusinessUnitID,
		@GLEntryID = @GLEntryID OUTPUT

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT		@GLEntryID,
			glAccMap.GLAccountID,
			CASE glAccMap.Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			invProd.Product_Amount - ISNULL(invProd.US_Postage_Amount, 0.00),
			2
FROM		Invoice_By_QSP_Product invProd
JOIN		GLAccountMap glAccMap
				ON	glAccMap.ProductLineID = invProd.QSP_Product_Line_ID
WHERE		invProd.Invoice_ID = @InvoiceID
AND			glAccMap.GLEntryTypeID = 1 --1: Sales Revenue
AND			glAccMap.BusinessUnitID = @BusinessUnitID


INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT		@GLEntryID,
			glAccMap.GLAccountID,
			CASE glAccMap.Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			invProd.Product_Amount,
			2
FROM		Invoice_By_QSP_Product invProd
JOIN		GLAccountMap glAccMap
				ON	glAccMap.ProductLineID = invProd.QSP_Product_Line_ID
WHERE		invProd.Invoice_ID = @InvoiceID
AND			glAccMap.GLEntryTypeID = @GlEntryTypeId --Processing fee GL entry type
AND			glAccMap.BusinessUnitID = @BusinessUnitID

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT		@GLEntryID,
			glAccMap.GLAccountID,
			CASE glAccMap.Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			invProd.Product_Amount,
			2
FROM		Invoice_By_QSP_Product invProd
JOIN		GLAccountMap glAccMap
				ON	glAccMap.ProductLineID = invProd.QSP_Product_Line_ID
WHERE		invProd.Invoice_ID = @InvoiceID
AND			glAccMap.GLEntryTypeID = 18 -- Shipping Fee
AND			glAccMap.BusinessUnitID = @BusinessUnitID

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT		TOP 1
			@GLEntryID,
			glAccMap.GLAccountID,
			CASE glAccMap.Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			inv.Invoice_Amount,
			2
FROM		Invoice inv
JOIN		Invoice_By_QSP_Product invProd
				ON	invProd.Invoice_ID = inv.Invoice_ID
JOIN		GLAccountMap glAccMap
				ON	glAccMap.ProductLineID = invProd.QSP_Product_Line_ID
WHERE		invProd.Invoice_ID = @InvoiceID
AND			glAccMap.GLEntryTypeID = 2 --2: Account Receivable
AND			glAccMap.BusinessUnitID = @BusinessUnitID

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT		@GLEntryID,
			glAccMap.GLAccountID,
			CASE glAccMap.Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			SUM(invSecTax.Tax_Amount),
			2
FROM		Invoice_Section invSec
JOIN		Invoice_Section_Tax invSecTax
				ON	invSecTax.Invoice_Section_ID = invSec.Invoice_Section_ID
JOIN		GLAccountMap glAccMap
				ON	glAccMap.TaxID = invSecTax.Tax_ID
WHERE		invSec.Invoice_ID = @InvoiceID
AND			glAccMap.GLEntryTypeID = 4 --4: Tax
AND			glAccMap.BusinessUnitID = @BusinessUnitID
AND			invSecTax.Tax_Amount > 0
GROUP BY	invSecTax.Tax_ID,
			glAccMap.GLAccountID,
			glAccMap.Debit

--USD Remit Postage Liability
INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT		@GLEntryID,
			glAccMap.GLAccountID,
			CASE glAccMap.Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			SUM(invProd.US_Postage_Amount),
			2
FROM		Invoice_By_QSP_Product invProd
JOIN		GLAccountMap glAccMap
				ON	glAccMap.ProductLineID = invProd.QSP_Product_Line_ID
WHERE		invProd.Invoice_ID = @InvoiceID
AND			glAccMap.GLEntryTypeID = 15 --15: Postage Liability
AND			invProd.US_Postage_Amount > 0.00
AND			glAccMap.BusinessUnitID = @BusinessUnitID
GROUP BY	glAccMap.GLAccountID,
			glAccMap.Debit


--To ensure credits and debits balance, make this booking the difference

DECLARE @GLDifferential		NUMERIC(12, 2),
		@AETotal			NUMERIC(12, 2),
		@FirstProductLine	INT

SELECT	@GLDifferential = SUM(	CASE glTran.Debit_Credit
									WHEN 'C' THEN	glTran.Amount * -1
									ELSE			glTran.Amount
								END)
FROM	GL_Entry glEnt
JOIN	GL_Transaction glTran
			ON	glTran.GL_Entry_ID = glEnt.GL_Entry_ID
WHERE	glEnt.GL_Entry_ID = @GLEntryID

SELECT	@AETotal = SUM(ProductLine_GP)
FROM	Invoice_By_QSP_Product
WHERE	Invoice_ID = @InvoiceID

SELECT		TOP 1
			@FirstProductLine = QSP_Product_Line_ID
FROM		Invoice_By_QSP_Product
WHERE		Invoice_ID = @InvoiceID
ORDER BY	QSP_Product_Line_ID

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT		@GLEntryID,
			glAccMap.GLAccountID,
			CASE glAccMap.Debit
				WHEN 0 THEN	'C'
				ELSE		'D'
			END,
			SUM(invProd.ProductLine_GP - CASE WHEN @FirstProductLine = invProd.QSP_Product_Line_ID THEN @AETotal + @GLDifferential ELSE 0 END),
			2
FROM		Invoice_By_QSP_Product invProd
JOIN		GLAccountMap glAccMap
				ON	glAccMap.ProductLineID = invProd.QSP_Product_Line_ID
WHERE		invProd.Invoice_ID = @InvoiceID
AND			glAccMap.GLEntryTypeID = 3 --3: Account Earning
AND			invProd.ProductLine_GP > 0.00
AND			glAccMap.BusinessUnitID = @BusinessUnitID
GROUP BY	glAccMap.GLAccountID,
			glAccMap.Debit


--TRT Redemption Orders - Relieve Liability into Recognized Revenue for the amount of the initial Voucher

DECLARE		@VoucherAmount NUMERIC(10, 2)

SELECT		@VoucherAmount = SUM(cod.Price)
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.[Date] = coh.OrderBatchDate
JOIN		Invoice inv
				ON	inv.INVOICE_ID = cod.InvoiceNumber
WHERE		cod.IsVoucherRedemption = 1
AND			cod.ProductType = 46023
AND			ISNULL(cod.DelFlag, 0) = 0
AND			inv.INVOICE_ID = @InvoiceID

IF @VoucherAmount > 0.00
BEGIN
	INSERT GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				@VoucherAmount,
				2
	FROM		GLAccountMap glAccMap
	WHERE		glAccMap.GLEntryTypeID = 19 --TRT Redemption Liability Relief
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.ProductLineID = 46023
	
	INSERT GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				@VoucherAmount,
				2
	FROM		GLAccountMap glAccMap
	WHERE		glAccMap.GLEntryTypeID = 20 --TRT Redemption Revenue Recognition
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.ProductLineID = 46023
END

DECLARE		@ShippingFeesAmount NUMERIC(10, 2)

SELECT		@ShippingFeesAmount = SUM(cod.Price)
FROM		QSPCanadaOrderManagement..CustomerOrderDetail cod
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.[Date] = coh.OrderBatchDate
JOIN		Invoice inv
				ON	inv.INVOICE_ID = cod.InvoiceNumber
WHERE		cod.IsVoucherRedemption = 1
AND			cod.ProductType = 46021
AND			ISNULL(cod.DelFlag, 0) = 0
AND			inv.INVOICE_ID = @InvoiceID

IF @ShippingFeesAmount > 0.00
BEGIN
	INSERT GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				@ShippingFeesAmount,
				2
	FROM		GLAccountMap glAccMap
	WHERE		glAccMap.GLEntryTypeID = 21 --Shipping Fees Redemption Liability Relief
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.ProductLineID = 46021
	
	INSERT GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				@ShippingFeesAmount,
				2
	FROM		GLAccountMap glAccMap
	WHERE		glAccMap.GLEntryTypeID = 22 --Shipping fees Redemption Revenue Recognition
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.ProductLineID = 46021
END

COMMIT
GO
