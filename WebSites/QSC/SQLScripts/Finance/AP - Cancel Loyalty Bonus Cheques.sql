USE QSPCanadaFinance

SELECT		ref.Campaign_ID,
			acc.ID AS Account_ID,
			ref.Amount
INTO		#LoyaltyBonusToCancel
FROM		Refund ref
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = ref.Campaign_ID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
WHERE		apc.ChequeNumber in (7000056769, 7000056932, 7000057016, 7000057138)

--If cancelling a Loyalty Bonus Credit
SELECT		adj.Campaign_ID,
			adj.Account_ID,
			adj.Adjustment_Amount AS Amount
INTO		#LoyaltyBonusToCancel
FROM		Adjustment adj
WHERE		adj.Adjustment_ID = 87008

SELECT	*
FROM	#LoyaltyBonusToCancel

DECLARE @DoAsAdjustment BIT
SET @DoAsAdjustment = 1

DECLARE @RunDate			DATETIME,
		@AccountID			INT

SET @RunDate = GETDATE()

DECLARE	@GLDescriptionText		VARCHAR(50),
		@GLAccountingYear		INT,
		@GLAccountingPeriod		INT,
		@GLCurrentStartDate		DATETIME,
		@GLCurrentEndDate		DATETIME,
		@GLEntryID				INT,
		@CampaignID				INT,
		@Amount					DECIMAL(8,2),
		@AdjustmentID			INT
	

SELECT	@GLCurrentStartDate = [Start_Date],
		@GLCurrentEndDate = End_Date,
		@GLAccountingYear = Accounting_Year,
		@GLAccountingPeriod = Accounting_Period
FROM	QSPCanadaFinance..Accounting_Period 
WHERE	GETDATE() BETWEEN [Start_Date] AND End_Date
AND		Is_Closed = 'N'


BEGIN TRANSACTION

DECLARE		Bonus CURSOR FOR
SELECT		Campaign_ID,
			Account_ID,
			Amount
FROM		#LoyaltyBonusToCancel
ORDER BY	Campaign_ID

OPEN Bonus
FETCH NEXT FROM Bonus INTO @CampaignID, @AccountID, @Amount


WHILE @@FETCH_STATUS = 0
BEGIN

	IF @DoAsAdjustment = 1
	BEGIN
		DECLARE @AccountType INT
		EXEC GetAccountType @AccountID, @AccountType OUTPUT

		INSERT	Adjustment
		SELECT	@AccountID,
				ISNULL(@AccountType, 0),
				49009, --Other Credit
				@RunDate, 
				-1 * @Amount, 
				NULL, --Note To Print, 
				@RunDate, 
				NULL, --Last Update
				'SYSTEM',
				'CA', --Country Code
				'Cancel Loyalty Bonus - CampaignID ' + CONVERT(VARCHAR(10), @CampaignID),
				NULL, --OrderID
				@CampaignID,
				NULL

		SET @AdjustmentID = SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		SET @AdjustmentID = NULL
	END

	SET @GLDescriptionText = 'Loyalty Bonus Credit Cancel - CampaignID ' + CONVERT(VARCHAR(10), @CampaignID)

	INSERT INTO GL_ENTRY
	(
		ADJUSTMENT_ID,
		ACCOUNTING_YEAR, 
		ACCOUNTING_PERIOD, 
		GL_ENTRY_DATE, 
		GL_POSTING_DATE, 
		[DESCRIPTION], 
		IS_POSTED, 
		COUNTRY_CODE
	)
	SELECT 	@AdjustmentID,
			@GLAccountingYear,
			@GLAccountingPeriod,
			@RunDate,
			NULL,
			@GLDescriptionText,
			'N',
			'CA'

	SET @GLEntryID = SCOPE_IDENTITY()

	INSERT GL_TRANSACTION
	SELECT @GLEntryID,
		'062.3401.0000.1007.09.70.000',
		'C',
		@Amount,
		2--Status 1 = new, 2 = approved.  They are approved when the adjustment is approved which is a manual process.

	INSERT GL_TRANSACTION
	SELECT @GLEntryID,
		'062.2007.0000.1007.00.00.000',
		'D',
		@Amount,
		2--Status 1 = new, 2 = approved.  They are approved when the adjustment is approved which is a manual process.

	IF @DoAsAdjustment = 0
	BEGIN
		SET @GLDescriptionText = 'Loyalty Bonus Cheque Cancel - CampaignID ' + CONVERT(VARCHAR(10), @CampaignID)

		INSERT INTO GL_ENTRY
		(
			ACCOUNTING_YEAR, 
			ACCOUNTING_PERIOD, 
			GL_ENTRY_DATE, 
			GL_POSTING_DATE, 
			[DESCRIPTION], 
			IS_POSTED, 
			COUNTRY_CODE
		)
		SELECT 	@GLAccountingYear,
				@GLAccountingPeriod,
				@RunDate,
				NULL,
				@GLDescriptionText,
				'N',
				'CA'

		SET @GLEntryID = SCOPE_IDENTITY()

		IF @@ERROR <> 0 OR ISNULL(@GLEntryID, 0) = 0 --Error creating GL Entry
		BEGIN
			ROLLBACK
			PRINT 'Error - Failed to insert GL Entry record for CampaignID ' + CONVERT(VARCHAR(10), @CampaignID)
			RETURN
		END

		INSERT GL_TRANSACTION
		SELECT @GLEntryID,
			'062.1260.0000.1007.00.00.000',
			'C',
			@Amount,
			2--Status 1 = new, 2 = approved.  They are approved when the adjustment is approved which is a manual process.

		INSERT GL_TRANSACTION
		SELECT @GLEntryID,
			'062.2007.0000.1007.00.00.000',
			'D',
			@Amount,
			2--Status 1 = new, 2 = approved.  They are approved when the adjustment is approved which is a manual process.

		INSERT GL_TRANSACTION
		SELECT @GLEntryID,
			'062.2007.0000.1007.00.00.000',
			'C',
			@Amount,
			2--Status 1 = new, 2 = approved.  They are approved when the adjustment is approved which is a manual process.

		INSERT GL_TRANSACTION
		SELECT @GLEntryID,
			'062.1000.0000.0000.00.00.000',
			'D',
			@Amount,
			2--Status 1 = new, 2 = approved.  They are approved when the adjustment is approved which is a manual process.
	END
	
	FETCH NEXT FROM Bonus INTO @CampaignID, @AccountID, @Amount

END
CLOSE Bonus
DEALLOCATE Bonus

COMMIT

DROP TABLE #LoyaltyBonusToCancel