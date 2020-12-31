USE [QSPCanadaFinance]

DECLARE @CustomerOrderHeaderInstance	INT,
		@TransID						INT,
		@CreateNewCheque				BIT,
		@CancelOldCheque				BIT,
		@CatalystCheque					BIT

SET	@CustomerOrderHeaderInstance = 9521062
SET @TransID = 1
SET	@CancelOldCheque = CONVERT(BIT, 1)
SET	@CreateNewCheque = CONVERT(BIT, 1)
SET	@CatalystCheque = CONVERT(BIT, 0)

SELECT		apc.ChequeNumber, *
FROM		Refund ref
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND			TransID = @TransID

DECLARE	@FirstName						NVARCHAR(50),
		@LastName						NVARCHAR(50),
		@Address1						NVARCHAR(50),
		@Address2						NVARCHAR(50),
		@City							VARCHAR(50),
		@ProvinceCode					VARCHAR(2),
		@PostalCode						VARCHAR(10),
		@Amount							NUMERIC(10, 2),
		@Reason							NVARCHAR(255),
		@IncidentID						INT,
		@iUserID						INT,
		@ErrorMessage					VARCHAR(200),
		@GLAccountingYear				INT,
		@GLAccountingPeriod				INT,
		@RunDate						DATETIME,
		@GL_Entry_ID					INT,
		@LiabilityGLAccountNumber		VARCHAR(50),
		@DistGLAccountNumber			VARCHAR(50),
		@CashGLAccountNumber			VARCHAR(50)
		
SET @RunDate = GETDATE()

--Get original Refund
SELECT	TOP 1
		@FirstName = FirstName,
		@LastName = LastName,
		@Address1 = Address1,
		@Address2 = Address2,
		@City = City,
		@ProvinceCode = Province,
		@PostalCode = PostalCode,
		@Amount = Amount,
		@Reason = Comment,
		@IncidentID = 1
FROM	Refund
WHERE	CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND		TransID = @TransID
ORDER BY CreateDate DESC

SELECT	@GLAccountingYear = Accounting_Year,
		@GLAccountingPeriod = Accounting_Period
FROM	QSPCanadaFinance..Accounting_Period 
WHERE	@RunDate BETWEEN Start_Date AND End_Date
AND		Is_Closed = 'N'

SET @LiabilityGLAccountNumber = '062.2001.0000.1007.00.00.000'
SET @CashGLAccountNumber = '062.1000.0000.0000.00.00.000'
SET @DistGLAccountNumber = '062.3406.0000.1007.09.70.000'

BEGIN TRANSACTION

--Create GL Reversal Entries for original cheque
IF @CancelOldCheque = CONVERT(BIT, 1)
BEGIN
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
	SELECT	@GLAccountingYear,
			@GLAccountingPeriod,
			@RunDate,
			NULL,
			'Cancel Customer Refund Cheque',
			'N',
			'CA'
			
	SET @GL_Entry_ID = SCOPE_IDENTITY()

	INSERT INTO GL_TRANSACTION
	SELECT	@GL_Entry_ID,
			@DistGLAccountNumber,
			'C',
			@Amount,
			2 --Status 1 = new, 2 = approved

	INSERT INTO GL_TRANSACTION
	SELECT	@GL_Entry_ID,
			@LiabilityGLAccountNumber,
			'D',
			@Amount,
			2 --Status 1 = new, 2 = approved

	IF (@CatalystCheque = 0)
	BEGIN
		INSERT INTO GL_TRANSACTION
		SELECT	@GL_Entry_ID,
				@LiabilityGLAccountNumber,
				'C',
				@Amount,
				2 --Status 1 = new, 2 = approved

		INSERT INTO GL_TRANSACTION
		SELECT	@GL_Entry_ID,
				@CashGLAccountNumber,
				'D',
				@Amount,
				2 --Status 1 = new, 2 = approved
	END
END

--Create New Refund
IF @CreateNewCheque = CONVERT(BIT, 1)
BEGIN
	EXEC	[dbo].[AP_Refund_DoCustomerRefund]
			@CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance,
			@TransID = @TransID,
			@FirstName = @FirstName,
			@LastName = @LastName,
			@Address1 = @Address1,
			@Address2 = @Address2,
			@City = @City,
			@ProvinceCode = @ProvinceCode,
			@PostalCode = @PostalCode,
			@Amount = @Amount,
			@Reason = @Reason,
			@IncidentID = @IncidentID,
			@iUserID = 612,
			@ErrorMessage = @ErrorMessage OUTPUT
	SELECT	@ErrorMessage as N'@ErrorMessage'
END

SELECT		apc.ChequeNumber, *
FROM		Refund ref
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND			TransID = @TransID

COMMIT TRANSACTION