USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GL_Entry_InsertPayment]    Script Date: 06/07/2017 09:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GL_Entry_InsertPayment]

	@PaymentID	INT

AS

DECLARE	@GLEntryID			INT,
		@PaymentDescription	VARCHAR(20),
		@CountryCode		VARCHAR(10),
		@BusinessUnitID		INT,
		@PaymentAmount		NUMERIC(12, 2),
		@PaymentMethodID	INT

SET @PaymentDescription = 'Payment'
SET	@CountryCode = 'CA'

SELECT	@BusinessUnitID = acc.BusinessUnitID
FROM	Payment pmt
JOIN	QSPCanadaOrderManagement..Batch b
			ON	b.OrderID = pmt.Order_ID
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = b.AccountID
WHERE	pmt.Payment_ID = @PaymentID

SELECT	@PaymentAmount = pmt.Payment_Amount,
		@PaymentMethodID = pmt.Payment_Method_ID
FROM	Payment pmt
WHERE	pmt.Payment_ID = @PaymentID

BEGIN TRANSACTION

EXEC	GL_Entry_Insert
		@PaymentID = @PaymentID,
		@Description = @PaymentDescription,
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
			@PaymentAmount,
			2
FROM		GLAccountMap glAccMap
WHERE		glAccMap.GLEntryTypeID = 5 --5: Account Receivable
AND			glAccMap.PaymentMethodID = @PaymentMethodID
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
			@PaymentAmount,
			2
FROM		GLAccountMap glAccMap
WHERE		glAccMap.GLEntryTypeID = 6 --6: Cash
AND			glAccMap.PaymentMethodID = @PaymentMethodID
AND			ISNULL(glAccMap.BusinessUnitID, @BusinessUnitID) = @BusinessUnitID

COMMIT
GO
