USE [QSPCanadaFinance]

DECLARE @RemitBatchID		INT,
		@RemitCode			VARCHAR(10),
		@ChequeNumber		BIGINT,
		@BankAccountID		INT,
		@CreateNewCheque	BIT

SET	@RemitBatchID = 1395
SET @RemitCode = '9924'

SET	@ChequeNumber = NULL
SET	@BankAccountID = NULL --4:CA, 5:US

SET	@CreateNewCheque = CONVERT(BIT, 0)

SELECT		apc.ChequeNumber, *
FROM		AP_Cheque_Remit apcr
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = apcr.AP_Cheque_ID
WHERE		apcr.RemitBatchID = ISNULL(@RemitBatchID, apcr.RemitBatchID)
AND			apcr.RemitCode = ISNULL(@RemitCode, apcr.RemitCode)
AND			apc.ChequeNumber = ISNULL(@ChequeNumber, apc.ChequeNumber)
AND			apc.Bank_Account_ID = ISNULL(@BankAccountID, apc.Bank_Account_ID)

DECLARE	@AP_Cheque_Remit_ID	INT,
		@FulfillmentHouseID	INT,
		@PublisherID		INT,
		@ProductSortName	NVARCHAR(50),
		@NetAmount			NUMERIC(14, 2),
		@GSTAmount			NUMERIC(10, 2),
		@HSTAmount			NUMERIC(10, 2),
		@PSTAmount			NUMERIC(10, 2),
		@GrossAmount		NUMERIC(14, 2),
		@CurrencyCode		VARCHAR(3),
		@Address1			VARCHAR(50),
		@Address2			VARCHAR(50),
		@City				VARCHAR(50),
		@Province			VARCHAR(2),
		@PostalCode			VARCHAR(10),
		@CountryCode		VARCHAR(2),
		@Comment			VARCHAR(150),
		@BusinessUnitID		INT,
		@Description		VARCHAR(MAX)
		

--Get original Remit cheque
SELECT		TOP 1
			@AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID,
			@FulfillmentHouseID = apcr.FulfillmentHouseID,
			@PublisherID = apcr.PublisherID,
			@ProductSortName = apcr.ProductSortName,
			@NetAmount = apcr.NetAmount,
			@GSTAmount = apcr.GSTAmount,
			@HSTAmount = apcr.HSTAmount,
			@PSTAmount = apcr.PSTAmount,
			@CurrencyCode = apcr.CurrencyCode,
			@Address1 = apcr.Address1,
			@Address2 = apcr.Address2,
			@City = apcr.City,
			@Province = apcr.Province,
			@PostalCode = apcr.PostalCode,
			@CountryCode = apcr.CountryCode,
			@Comment = apcr.Comment
FROM		AP_Cheque_Remit apcr
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = apcr.AP_Cheque_ID
WHERE		apcr.RemitBatchID = ISNULL(@RemitBatchID, apcr.RemitBatchID)
AND			apcr.RemitCode = ISNULL(@RemitCode, apcr.RemitCode)
AND			apc.ChequeNumber = ISNULL(@ChequeNumber, apc.ChequeNumber)
AND			apc.Bank_Account_ID = ISNULL(@BankAccountID, apc.Bank_Account_ID)
ORDER BY	apc.CreationDate DESC

--Get original GL Entries
SELECT	gle.GL_Entry_ID,
		gle.Description,
		gle.Country_Code,
		gle.BusinessUnitID
INTO	#gleOriginal
FROM	GL_Entry gle
WHERE	gle.AP_Cheque_Remit_ID = @AP_Cheque_Remit_ID

--Get original GL Transactions
SELECT	glt.GL_Transaction_ID,
		glt.GL_Entry_ID,
		glt.GLAccountID,
		glt.Debit_Credit,
		glt.Amount,
		glt.GL_Transaction_Status_ID
INTO	#gltOriginal
FROM	GL_Transaction glt
JOIN	#gleOriginal gleO
			ON	gleO.GL_Entry_ID = glt.GL_Entry_ID

--Update to PS GL accounts if cheques were done in Catalyst
UPDATE	#gltOriginal
SET		GLAccountID =	CASE GLAccountID
							WHEN 44 THEN 158
							WHEN 40 THEN 159
							WHEN 46 THEN 160
							WHEN 82 THEN 161
							WHEN 1 THEN 162
							WHEN 2 THEN 163
						END
WHERE	GLAccountID IN (44, 40, 46, 82, 1, 2)

DECLARE @GLAccountingYear		INT,
		@GLAccountingPeriod		INT,
		@GLRemitExpense			VARCHAR(30),
		@GLCash					VARCHAR(30),
		@GLGST					VARCHAR(30),
		@GLHST					VARCHAR(30),
		@GLPST					VARCHAR(30),
		@GL_Entry_ID			INT

BEGIN TRANSACTION

--Create GL Reversal Entries for original cheque
WHILE EXISTS(SELECT GL_Entry_ID FROM #gleOriginal)
BEGIN

	DECLARE		@OriginalGLEntryID	INT
	SELECT		TOP 1
				@OriginalGLEntryID = GL_Entry_ID,
				@CountryCode = Country_Code,
				@BusinessUnitID = BusinessUnitID,
				@Description = [Description]
	FROM		#gleOriginal
	ORDER BY	GL_Entry_ID

	SET @Description = 'Cancel ' + CONVERT(VARCHAR(MAX), @Description)

	EXEC	GL_Entry_Insert
			@Description = @Description,
			@CountryCode = @CountryCode,
			@APChequeRemitID = @AP_Cheque_Remit_ID,
			@BusinessUnitID = @BusinessUnitID,
			@GLEntryID = @GL_Entry_ID OUTPUT

	DELETE	#gleOriginal
	WHERE	GL_Entry_ID = @OriginalGLEntryID

	INSERT GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GL_Entry_ID,
				GLAccountID,
				CASE Debit_Credit
					WHEN 'C' THEN	'D'
					ELSE			'C'
				END,
				Amount,
				GL_Transaction_Status_ID
	FROM		#gltOriginal
	WHERE		GL_Entry_ID = @OriginalGLEntryID
	ORDER BY	GL_Transaction_ID

	DELETE	#gltOriginal
	WHERE	GL_Entry_ID = @OriginalGLEntryID

	SELECT	*
	FROM	GL_Entry
	WHERE	GL_Entry_ID = @GL_Entry_ID

END

IF @CreateNewCheque = CONVERT(BIT, 1)
BEGIN
	EXEC	[dbo].[AP_Remit_SendAP]
			@RemitBatchID = @RemitBatchID,
			@GenerateChequeFile = 1,
			@RemitCode = @RemitCode

	SELECT		TOP 1
				*
	FROM		AP_Cheque_Remit apcr
	ORDER BY	apcr.AP_Cheque_Remit_ID DESC

END

COMMIT TRANSACTION

DROP TABLE #gleOriginal
DROP TABLE #gltOriginal

--To push this batch execute switch to the Remit user and run this:
--EXEC QSPCanadaFinance..AP_Remit_CreateBatch