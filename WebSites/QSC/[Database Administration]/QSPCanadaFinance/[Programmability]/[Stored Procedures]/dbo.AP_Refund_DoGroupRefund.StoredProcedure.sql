USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_DoGroupRefund]    Script Date: 06/07/2017 09:17:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_DoGroupRefund]

	@CampaignID						INT,
	@CreateCheque					BIT,
	@ForceChequeCreation			BIT,
	@ToDate							DATETIME

AS

SET NOCOUNT ON

DECLARE	@ErrorMessage  		VARCHAR(100),
		@SendEmailToIT		VARCHAR(1000),
		@RefundMinimum		NUMERIC(14,6),
		@AccountID			INT,
		@RefundID			INT,
		@AP_Cheque_ID		INT

IF ISNULL(@CampaignID, 0) > 0
BEGIN
	SELECT	@AccountID = BillToAccountID
	FROM	QSPCanadaCommon..Campaign
	WHERE	ID = @CampaignID
END

IF ISNULL(@ToDate, '') = ''
BEGIN
	SET @ToDate = CONVERT(DATETIME, CONVERT(VARCHAR(10), GETDATE(), 101))
END

SET @SendEmailToIT = 'qsp-IT-canada@qsp.com'
SET	@RefundMinimum = 5.00

DECLARE @RunDate			DATETIME
DECLARE @internalComment	VARCHAR(100)

SET @RunDate = GETDATE()

CREATE TABLE #Campaign
(
	CampaignID			INT,
	AccountID			INT,
	CampaignCredit		NUMERIC(14, 6),
	AccountCredit		NUMERIC(14, 6)
)

INSERT INTO	#Campaign
(
	AccountID,
	CampaignID,
	CampaignCredit,
	AccountCredit
)
SELECT		camp.BillToAccountID,
			camp.ID,
			-1 * dbo.UDF_Account_GetBalance(camp.ID, NULL, @ToDate),
			-1 * dbo.UDF_Account_GetBalance(NULL, camp.BillToAccountID, @ToDate)
FROM		QSPCanadaCommon..Campaign camp
WHERE			camp.BillToAccountID = ISNULL(@AccountID, camp.BillToAccountID)

IF ISNULL(@CampaignID, 0) > 0
BEGIN
	PRINT 'Account data'
	SELECT	*
	FROM	#Campaign
	WHERE	AccountID = @AccountID
END

--Remove campaigns that are not in credit position
DELETE
FROM	#Campaign
WHERE	CampaignCredit <= 0

--Remove campaigns that were not asked for
IF ISNULL(@CampaignID, 0) > 0
BEGIN
	DELETE
	FROM	#Campaign
	WHERE	CampaignID <> @CampaignID
END

DELETE
FROM	#Campaign
WHERE	CampaignCredit < @RefundMinimum

IF CONVERT(BIT, @ForceChequeCreation) = 0
BEGIN
	--Remove accounts that are not in credit position
	DELETE
	FROM	#Campaign
	WHERE	AccountCredit < 0.00
END

PRINT 'Cheques that will be created'
SELECT	*
FROM	#Campaign

IF @CreateCheque = 1
BEGIN

	DECLARE @RefundCampaignID 	INT
	DECLARE	@RefundAccountID	INT
	DECLARE @Address1			VARCHAR(50)	DECLARE @Address2			VARCHAR(50)
	DECLARE @City				VARCHAR(50)
	DECLARE @Province			VARCHAR(25)
	DECLARE @PostalCode			VARCHAR(15)
	DECLARE @Country			VARCHAR(25)
	DECLARE @RefundAmount		NUMERIC(12,2)
	DECLARE @RowCount 			INT
	DECLARE @OldRefundID 		INT
	DECLARE @ChangedBy 			INT
	DECLARE @Cnt 				INT

	SET @ChangedBy = -1

	BEGIN TRANSACTION

	DECLARE		RefundCampaign CURSOR FOR
	SELECT		camp.CampaignID,
				camp.AccountID,
				ISNULL(accAdd.Street1, ''),
				ISNULL(accAdd.Street2, ''),
				ISNULL(accAdd.City, ''),
				ISNULL(accAdd.StateProvince, ''),
				SUBSTRING(accAdd.Postal_Code, 1, 3) + ' ' + SUBSTRING(accAdd.Postal_Code, 4 ,3),
				ISNULL(accAdd.Country, ''),
				ISNULL(CONVERT(NUMERIC(10, 2), camp.CampaignCredit), 0.00)
	FROM		#Campaign camp
	JOIN		QSPCanadaCommon..CAccount fulfAcc
					ON	fulfAcc.ID = camp.AccountID
	JOIN		QSPCanadaCommon..Address accAdd
					ON	accAdd.AddressListID = fulfAcc.AddressListID
					AND	accAdd.Address_Type = 54002 -- Billto
	ORDER BY	camp.AccountID,
				camp.CampaignID

	OPEN RefundCampaign
	FETCH NEXT FROM RefundCampaign INTO @RefundCampaignID, @RefundAccountID, @Address1,
							@Address2, @City, @Province, @PostalCode, @Country, @RefundAmount

	WHILE @@FETCH_STATUS = 0
	BEGIN

		EXEC AP_Refund_Group_CreateCheque
				@RefundCampaignID, @RefundAccountID, @Address1, @Address2, @City, @Province, @PostalCode, @Country, 
				@RefundAmount, @ChangedBy, @ErrorMessage OUTPUT, @RefundID OUTPUT, @AP_Cheque_ID OUTPUT

		IF ISNULL(@ErrorMessage, '') <> ''
		BEGIN
			ROLLBACK
			EXEC QSPCanadaCommon..Send_EMail  'GroupRefunds@qsp.com', @SendEmailToIT, 'Error in Group Refund', @ErrorMessage
			RETURN
		END

	FETCH NEXT FROM RefundCampaign INTO @RefundCampaignID, @RefundAccountID, @Address1,
							@Address2, @City, @Province, @PostalCode, @Country, @RefundAmount
	END
	CLOSE RefundCampaign
	DEALLOCATE RefundCampaign

	COMMIT

	IF ISNULL(@CampaignID, 0) > 0
	BEGIN
		PRINT 'Refund Created'
		SELECT		TOP 1
					*
		FROM		Refund
		ORDER BY	Refund_ID DESC
	END

END

DROP TABLE #Campaign
GO
