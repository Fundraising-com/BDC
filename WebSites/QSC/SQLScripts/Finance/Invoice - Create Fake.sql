USE QSPCanadaFinance
GO

DECLARE	@CampaignID INT
SET		@CampaignID = 108219

BEGIN TRAN

DECLARE	@AccountID INT,
		@AccountTypeID INT,
		@OrderID INT,
		@InvoiceID INT,
		@AccountingYear INT,
		@AccountingPeriod INT,
		@BatchDate DATETIME,
		@BatchID INT

SELECT	@AccountID = acc.ID,
		@AccountTypeID = CASE acc.CAccountCodeClass WHEN 'FM' THEN 50602 ELSE 50601 END
FROM	QSPCanadaCommon..Campaign camp
JOIN	QSPCanadaCommon..CAccount acc ON acc.ID = camp.BillToAccountID
WHERE	camp.ID = @CampaignID

SELECT	@OrderID = MAX(Order_ID) + 1
FROM	INVOICE
WHERE	Order_ID < 10000

INSERT INVOICE VALUES (@AccountID, @AccountTypeID, @OrderID, GETDATE(), GETDATE(),	0.00,NULL,NULL,GETDATE(),GETDATE(),	'612',	'CA',	'Y',GETDATE(), getdate(),NULL)

SELECT @InvoiceID = SCOPE_IDENTITY()

EXEC	[dbo].[AccountingPeriod_GetCurrent]
		@AccountingYear = @AccountingYear OUTPUT,
		@AccountingPeriod = @AccountingPeriod OUTPUT

INSERT GL_Entry VALUES (@InvoiceID, NULL, NULL, @AccountingYear, @AccountingPeriod, GETDATE(), NULL, 'Fake Invoice', 'N', 'CA', NULL, NULL, 3)

SELECT	@BatchDate = CONVERT(char(10), GETDATE(),126)

SELECT	@BatchID = MAX(ID) + 1
FROM	QSPCanadaOrderManagement..Batch
WHERE	Date = @BatchDate
AND		ID >= 20000

SET	@BatchID = ISNULL(@BatchID, 20000)

INSERT QSPCanadaOrderManagement..Batch VALUES (@BatchDate, @BatchID, @AccountID,	0, 0.00, 0.00, 40013, NULL,	NULL, NULL,	0,	0,	0,	0,	0,	0,	0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, GETDATE(),	NULL, NULL,	NULL, 41006, @CampaignID, NULL,	NULL, @AccountID, NULL,	NULL, NULL,	NULL, NULL,	GETDATE(), GETDATE(), NULL,	NULL, NULL,	NULL, NULL,	NULL, NULL,	NULL, NULL,	NULL, NULL,	NULL, NULL,	NULL, NULL,	39002, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL,	NULL, @OrderID, NULL, NULL,	NULL, 1, NULL, 0, NULL,	NULL, NULL,	NULL, NULL,	NULL, NULL)

COMMIT TRAN
