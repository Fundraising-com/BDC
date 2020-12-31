USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_Cancel]    Script Date: 06/07/2017 09:17:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_Cancel]

	@Refund_ID	INT

AS

DECLARE @AP_Cheque_ID					INT,
		@CustomerOrderHeaderInstance	INT,
		@TransID						INT,
		@BusinessUnitID					INT,
		@CountryCode					VARCHAR(10),
		@Amount							NUMERIC(10, 2)

SELECT	@CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance,
		@TransID = ref.TransID,
		@Amount = ref.Amount
FROM	Refund ref
WHERE	ref.Refund_ID = @Refund_ID

--Get original Cheque if it exists
SELECT	@AP_Cheque_ID = apc.AP_Cheque_ID
FROM	Refund ref
JOIN	AP_Cheque apc
			ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE	ref.Refund_ID = @Refund_ID

SET	@CountryCode = 'CA'

SELECT	@BusinessUnitID = acc.BusinessUnitID
FROM	QSPCanadaOrderManagement..CustomerOrderHeader coh
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = coh.CampaignID
JOIN	QSPCanadaCommon..CAccount acc
			ON	acc.ID = camp.BillToAccountID
WHERE	coh.Instance = @CustomerOrderHeaderInstance

DECLARE @GLEntryID					INT,
		@GL_Debit_Transaction_ID	INT,
		@GL_Debit_Transaction_ID2	INT,
		@GL_Credit_Transaction_ID	INT,
		@GL_Credit_Transaction_ID2	INT


BEGIN TRANSACTION

--Mark Cheque as voided (only keep if Scotia doesn't supply it)
IF @AP_Cheque_ID > 0
BEGIN
	UPDATE	apc
	SET		apc.AP_Cheque_Status_ID = 4 --4: Voided
	FROM	AP_Cheque apc
	JOIN	Refund ref
				ON	ref.AP_Cheque_ID = apc.AP_Cheque_ID
	WHERE	ref.Refund_ID = @Refund_ID
END

--Mark refund as cancelled if done before cheque created
IF @AP_Cheque_ID IS NULL
BEGIN
	UPDATE	ref
	SET		Cancelled = 1
	FROM	Refund ref
	WHERE	ref.Refund_ID = @Refund_ID
END

DECLARE @GLDescription	VARCHAR(30)
SET @GLDescription = 'Cancel Customer Refund Cheque'

EXEC	GL_Entry_Insert
		@Description = @GLDescription,
		@CountryCode = @CountryCode,
		@RefundID = @Refund_ID,
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
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'D'
			ELSE		'C'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 11 --11: Expense
AND		glAccMap.BusinessUnitID = @BusinessUnitID

SET @GL_Debit_Transaction_ID = SCOPE_IDENTITY()

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'D'
			ELSE		'C'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 12 --12: Account Payable - Credit
AND		glAccMap.BusinessUnitID = @BusinessUnitID

SET @GL_Credit_Transaction_ID = SCOPE_IDENTITY()

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'D'
			ELSE		'C'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 14 --14: Acount Payable - Debit
AND		glAccMap.BusinessUnitID = @BusinessUnitID

SET @GL_Debit_Transaction_ID2 = SCOPE_IDENTITY()

INSERT GL_Transaction
(
	GL_Entry_ID,
	GLAccountID,
	Debit_Credit,
	Amount,
	GL_Transaction_Status_ID
)
SELECT	@GLEntryID,
		glAccMap.GLAccountID,
		CASE glAccMap.Debit
			WHEN 0 THEN	'D'
			ELSE		'C'
		END,
		@Amount,
		2
FROM	GLAccountMap glAccMap
WHERE	glAccMap.GLEntryTypeID = 13 --13: Cash

SET @GL_Credit_Transaction_ID2 = SCOPE_IDENTITY()

COMMIT TRANSACTION
GO
