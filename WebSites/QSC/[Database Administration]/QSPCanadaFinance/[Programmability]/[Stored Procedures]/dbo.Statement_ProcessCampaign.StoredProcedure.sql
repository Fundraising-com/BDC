USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_ProcessCampaign]    Script Date: 06/07/2017 09:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Statement_ProcessCampaign]

	@CampaignID		INT,
	@StatementRunID	INT,
	@DateTo			DATETIME,
	@StatementID	INT OUTPUT

AS

IF @StatementRunID = 0
BEGIN
	SET @StatementRunID = NULL
END

BEGIN TRANSACTION

INSERT INTO [Statement]
(
	StatementRunID,
	StatementDate,
	CreationDate,
	AccountID,
	CampaignID,
	IsStaffCampaign,
	Lang,
	CampaignPrograms,
	FMID,
	FMFirstName,
	FMLastName,
	PaymentTerms,
	CorpAttn,
	CorpAddress1,
	CorpAddress2,
	CorpCity,
	CorpProvince,
	CorpPostalCode,
	CorpPhoneNumber,
	CorpGSTNumber,
	CorpQSTNumber,
	AccountName,
	AccountContactFirstName,
	AccountContactLastName,
	AccountAddress1,
	AccountAddress2,
	AccountCity,
	AccountProvince,
	AccountPostalCode,
	AccountZip4,
	AccountPhoneNumber
)
SELECT	@StatementRunID,
		CONVERT(VARCHAR(10), DATEADD(DAY, -1, @DateTo), 120), --Statement date should be yesterday
		GETDATE(),
		AccountID,
		CampaignID,
		IsStaffCampaign,
		Lang,
		CampaignPrograms,
		FMID,
		FMFirstName,
		FMLastName,
		PaymentTerms,
		CorpAttn,
		CorpAddress1,
		CorpAddress2,
		CorpCity,
		CorpProvince,
		CorpPostalCode,
		CorpPhoneNumber,
		CorpGSTNumber,
		CorpQSTNumber,
		AccountName,
		AccountContactFirstName,
		AccountContactLastName,
		AccountAddress1,
		AccountAddress2,
		AccountCity,
		AccountProvince,
		AccountPostalCode,
		AccountZip4,
		AccountPhoneNumber
FROM	dbo.UDF_Statement_GetHeader(@CampaignID)

SET @StatementID = SCOPE_IDENTITY()

CREATE TABLE #StatementDetail
(
	TransactionTypeID		INT,
	TransactionID			INT,
	TransactionAmount		NUMERIC(12, 2)
)

INSERT INTO #StatementDetail
(
	TransactionTypeID,
	TransactionID,
	TransactionAmount
)
SELECT	TransactionTypeID,
		TransactionID,
		TransactionAmount
FROM	UDF_Statement_GetDetails_WithBusLogic(@DateTo)
WHERE	CampaignID = @CampaignID

INSERT INTO StatementInvoice
(
	StatementID,
	InvoiceID
)
SELECT	@StatementID,
		TransactionID
FROM	#StatementDetail
WHERE	TransactionTypeID = 1 --1: Invoice

INSERT INTO StatementPayment
(
	StatementID,
	PaymentID
)
SELECT	@StatementID,
		TransactionID
FROM	#StatementDetail
WHERE	TransactionTypeID = 2 --2: Payment

INSERT INTO StatementAdjustment
(
	StatementID,
	AdjustmentID
)
SELECT	@StatementID,
		TransactionID
FROM	#StatementDetail
WHERE	TransactionTypeID = 3 --3: Adjustment

INSERT INTO StatementInvoiceOnline
(
	StatementID,
	InvoiceID
)
SELECT	@StatementID,
		TransactionID
FROM	#StatementDetail
WHERE	TransactionTypeID = 4 --4: Online Profit

INSERT INTO StatementInvoiceCustSvc
(
	StatementID,
	InvoiceID
)
SELECT	@StatementID,
		TransactionID
FROM	#StatementDetail
WHERE	TransactionTypeID = 5 --5: Customer Service Profit


DECLARE @Balance NUMERIC(12, 2)

SELECT	@Balance = SUM(TransactionAmount)
FROM	#StatementDetail

UPDATE	stat
SET		Balance = @Balance
FROM	[Statement] stat
WHERE	stat.StatementID = @StatementID

UPDATE	QSPCanadaCommon..Campaign
SET		ForceStatementPrint = 0
WHERE	ID = @CampaignID
AND		ForceStatementPrint = 1

COMMIT TRANSACTION
GO
