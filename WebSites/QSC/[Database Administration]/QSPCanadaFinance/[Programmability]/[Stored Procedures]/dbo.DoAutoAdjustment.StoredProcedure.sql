USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[DoAutoAdjustment]    Script Date: 06/07/2017 09:17:06 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[DoAutoAdjustment]

	@DateTo	DATETIME

AS

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   This procedure calculate account balance by campaign on run date and create an adjustment if the balance is $5 Debit/Credit
--   Generate an email for FInance listing all account and amount adjusted.
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

SET NOCOUNT ON

DECLARE	@AccountType 		INT
DECLARE @AdjustmentType		INT
DECLARE @CountryCode 		VARCHAR(2)
DECLARE @AccountID 			INT
DECLARE @CampaignId 		INT
DECLARE @AdjustmentAmount  	NUMERIC(12,5)
DECLARE @GLAdjustmentAmount NUMERIC(12,5)
DECLARE @AdjustmentId 		INT 
DECLARE @cnt 				INT

--If no end date given, go until last night midnight
IF ISNULL(@DateTo, '') = ''
BEGIN
	SET @DateTo = CONVERT(DATETIME, CONVERT(VARCHAR(10), GETDATE(), 101))
END

--Transaction Level Entries
CREATE TABLE #IPA
(
	FMID				VARCHAR(4),
	FMName				VARCHAR(50),
	OrderQualifier		INT,
	OrderType			INT,
	AdjustmentType		INT,
	AccountID			INT,
	AccountName			VARCHAR(50), 
	AgingDays			INT,
	CampaignID			INT,
	TransactionID		INT,
	InvoiceAmount  		NUMERIC(10, 2),
	PaymentAmount 		NUMERIC(10, 2),
	AdjustmentAmount 	NUMERIC(14, 6)
) 

CREATE  TABLE #CA
(
	CampaignID INT
)

CREATE  TABLE #Amt
(
	ID			INT IDENTITY,
	AccountID	INT,
	AccountType	INT,
	CampaignID	INT, 
	AccountName	VARCHAR(100),
	Amount		NUMERIC(14, 6)
)

CREATE TABLE ##AccountAdjusted
(
	AccountID	INT,
	AccountName VARCHAR(100),
	CampaignID  INT,
	Amount	 	NUMERIC(14, 6),
	[Type] 	 	VARCHAR(10)
)

--Invoices
INSERT		#IPA
SELECT		camp.FMID,
			fm.LastName + ', ' + fm.FirstName,  
			b.OrderQualifierID,
			b.OrderTypeCode,
			Null,
			b.AccountID,
			acc.[Name],
			MAX(DATEDIFF(dd, ISNULL(inv.Invoice_Date, GETDATE()), GETDATE())),
			b.CampaignID,
			inv.Invoice_ID,
			inv.Invoice_Amount,
			CONVERT(NUMERIC(10, 2), 0.0),
			CONVERT(NUMERIC(14, 6), 0.0) 
FROM 		Invoice inv
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = B.AccountID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		b.OrderQualifierID IN (39001, 39002, 39003, 39005, 39006, 39020) --Main Staff or supplementary
AND 		inv.Invoice_Date <= @DateTo
GROUP BY	b.AccountID,
			acc.[Name],
			b.CampaignID,
			camp.FMID,
			fm.LastName,
			fm.Firstname,
			b.OrderTypeCode,
			b.OrderQualifierID,
			inv.Invoice_ID,
			inv.Invoice_Amount

--Payments	
INSERT		#IPA
SELECT		camp.FMID,
			fm.LastName + ', ' + fm.Firstname,  
			b.OrderQualifierID,
			b.OrderTypeCode,
			Null,
			b.AccountID,
			acc.[Name],
			MAX(DATEDIFF(dd, ISNULL(Payment_Effective_Date, GETDATE()), GETDATE())),
			b.CampaignID,
			pmt.Payment_ID,
			CONVERT(Numeric(10, 2), 0.0) ,
			ISNULL(pmt.Payment_Amount, 0),
			CONVERT(Numeric(14, 6), 0.0) 
FROM		QSPCanadaOrderManagement..Batch b
JOIN		Payment pmt
				ON	pmt.Order_ID = b.OrderID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = b.AccountID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		Payment_Effective_Date <= @DateTo
AND			b.OrderQualifierID IN (39001, 39002, 39003, 39005, 39006, 39020) --Main Staff or supplementary
GROUP BY	b.AccountID,
			acc.[Name],
			b.CampaignID,
			camp.FMID,
			fm.LastName,
			fm.Firstname,
			b.OrderTypeCode,
			b.OrderQualifierID,
			pmt.Payment_Id,
			pmt.Payment_Amount

--All CAs where we have activity
INSERT	#CA
SELECT	DISTINCT CampaignID
FROM	#IPA

--Adjustments
INSERT		#IPA
SELECT		camp.FMID,
			fm.LastName + ', ' + fm.Firstname, 
			Null,
			Null,
			adj.Adjustment_Type_ID,
			adj.Account_ID,
			acc.[Name],
			MAX(DATEDIFF(dd, ISNULL(adj.Adjustment_Effective_Date, GETDATE()), GETDATE())),
			adj.Campaign_ID,
			adj.Adjustment_ID,
			CONVERT(numeric(10,2), 0.0),
			CONVERT(numeric(10,2), 0.0),
			ISNULL(adj.Adjustment_Amount, 0.0)
FROM  		Adjustment adj
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = adj.Account_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = adj.Campaign_ID
				AND	camp.BillToAccountID = adj.Account_ID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
WHERE		adj.Campaign_ID IN (SELECT CampaignID FROM #CA)
AND			adj.Adjustment_Effective_Date <= @DateTo
GROUP BY	adj.Account_ID,
			adj.Campaign_ID,
			acc.[Name],
			camp.FMID,
			fm.LastName,
			fm.Firstname,
			adj.Adjustment_Type_ID,
			adj.Adjustment_ID,
			adj.Adjustment_Amount

--Get All account where Balance is $5 or less
INSERT INTO #Amt	(AccountID,
					AccountType,
					CampaignID,
					AccountName,
					Amount)
SELECT		AccountID,
			0 AS AccountType,
			CampaignID, 
			AccountName,
			SUM((ISNULL(InvoiceAmount, 0) - ISNULL(PaymentAmount, 0) - ISNULL(AdjustmentAmount, 0))) AS Amount
FROM		#IPA 
GROUP BY	FMID,
			FMName,
			AccountID,
			CampaignID,
			AccountName
HAVING		ABS(SUM(ISNULL(InvoiceAmount, 0) - ISNULL(PaymentAmount, 0) - ISNULL(AdjustmentAmount, 0))) BETWEEN 0.01 AND 5
ORDER BY	FMName,
			AccountName,
			AccountID,
			Amount

--Adjust each Account
SELECT	@cnt = MAX(ID)
FROM	#Amt

WHILE @cnt > 0
BEGIN
	SELECT 	@AccountID = AccountID,
			@CampaignID = CampaignID,
			@AdjustmentAmount = Amount
	FROM	#Amt
	WHERE	ID = @cnt

	EXEC	GetAccountType @AccountID, @AccountType OUTPUT 
			
	IF @AccountType > 0
	BEGIN		
		UPDATE	#Amt
		SET		AccountType = @AccountType
		WHERE	AccountID = @AccountID
	END

	--Determine Adjustment Type 
	IF SIGN(@AdjustmentAmount) = -1
	BEGIN
		SET @AdjustmentType = 49021
		SET @GLAdjustmentAmount = ABS(@AdjustmentAmount)
	END
	ELSE IF SIGN(@AdjustmentAmount) = 1
	BEGIN
		SET @AdjustmentType = 49022
		SET @GLAdjustmentAmount = @AdjustmentAmount
	END

	IF SIGN(@AdjustmentAmount) IN (1, -1)
	BEGIN

		INSERT INTO Adjustment (
						Account_ID,
						Account_Type_ID,
						Adjustment_Type_ID,
						Adjustment_Effective_Date,
						Adjustment_Amount,
						Date_Created,
						DateTime_Modified,
						Last_Updated_By,
						Country_Code,
						Internal_Comment,
						Campaign_ID)
		VALUES(
				@AccountID,
	       		@AccountType,
				@AdjustmentType,
	       		DATEADD(MINUTE, -1, @DateTo), --so the adjustments will be included in cheque/statement calculations
	       		@AdjustmentAmount,
				GETDATE(),
				GETDATE(),
				'DoAutoAdjustment',
				'CA',
				'Auto Adjustment for Amount $5 or less',
				@CampaignID
				)

		--Get the AdjustmentID
		SET @AdjustmentID = SCOPE_IDENTITY()
							
		--Do GL Entries
		EXEC AddGLEntriesForAdjustment @AccountID, Null, @AdjustmentID, @AdjustmentType, @GLAdjustmentAmount, -1
	
	END

	SET @cnt = @cnt - 1
END

--Send Automated Email to Finance about adjustments Made
BEGIN
	-- Insert error record into temp table and make ErrorLogFile and Email
	DECLARE @SQLcommand 	VARCHAR(1000)
	DECLARE @Filename		VARCHAR(100)
	DECLARE @Body 			VARCHAR(500)
	DECLARE @path 			VARCHAR(200)
	DECLARE @FileAttachment	VARCHAR(200)
	DECLARE @SendEmailTo	VARCHAR(100)

	SET @SendEmailTo = 'qsp-finance-canada@qsp.com,qsp-IT-canada@qsp.com'

	SET @path = 'E:\Projects\Paylater\QSPCAFinance\AutoAdjustmentLogs\' 

	SET @Filename =  'AutoAdjustmentLog_' + 
		CAST(DATEPART(YEAR, @DateTo) 	AS VARCHAR) +
		CAST(DATEPART(MONTH, @DateTo) 	AS VARCHAR) +
		CAST(DATEPART(DAY, @DateTo) 	AS VARCHAR) + 
		CAST(DATEPART(HOUR, @DateTo) 	AS VARCHAR) + 
		CAST(DATEPART(MINUTE, @DateTo)	AS VARCHAR) + 
		CAST(DATEPART(SECOND, @DateTo) AS VARCHAR) + '.txt'
	
	INSERT INTO		##AccountAdjusted
	SELECT DISTINCT QSPCanadaOrderManagement.dbo.UDF_PADSTRING(QSPCanadaOrderManagement.dbo.UDF_PADSTRING(AccountID, ' ', LEN(AccountID) + 1, 'R'), ' ', 8, 'R') AS AccountID, 
					QSPCanadaOrderManagement.dbo.UDF_PADSTRING(QSPCanadaOrderManagement.dbo.UDF_PADSTRING(AccountName,' ', LEN(AccountName) + 1,'R'), ' ', 55, 'R') AS AccountName, 
					QSPCanadaOrderManagement.dbo.UDF_PADSTRING(QSPCanadaOrderManagement.dbo.UDF_PADSTRING(CampaignID,' ', LEN(CampaignID) + 1, 'R'), ' ', 8, 'R') AS CampaignID, 
					QSPCanadaOrderManagement.dbo.UDF_PADSTRING(QSPCanadaOrderManagement.dbo.UDF_PADSTRING(ABS(Amount),' ', LEN(Amount) + 1, 'R'), ' ', 12, 'R') AS Amount, 
					CASE SIGN(Amount)
						WHEN -1 THEN 'Credit'
						WHEN 1	THEN 'Debit'
					END AS [Type]
	FROM			#AMT
	ORDER BY		AccountName,
					CampaignID
	
	SELECT @Cnt = COUNT(*) FROM ##AccountAdjusted

	--If there are orders with error create a log file and email
	IF @Cnt > 0 
	BEGIN

		SET @SQLcommand = 'bcp "##AccountAdjusted" OUT "E:\Projects\Paylater\QSPCAFinance\AutoAdjustmentLogs\' + @Filename + '" -c -q -T '
		EXEC master..xp_cmdshell @SQLcommand

		SET @Body = 'The following file lists campaigns whose balance was $5 or less and have subsequently been written off as of  ' +
				CONVERT(VARCHAR(30), @DateTo, 113) + CHAR(13) + CHAR(13)
			
		SET @FileAttachment = @path + @Filename

		EXEC  QSPCanadaCommon.dbo.Send_EMAIL_ATTACH 'CampaignWriteoff@QSP.com', @SendEmailTo, 'Adjustment writeoff for campaigns with a balance of $5 or less', @Body, @FileAttachment
	END

	DROP TABLE ##AccountAdjusted 
END

DROP TABLE   #Amt
DROP TABLE   #IPA
DROP TABLE   #CA
GO
