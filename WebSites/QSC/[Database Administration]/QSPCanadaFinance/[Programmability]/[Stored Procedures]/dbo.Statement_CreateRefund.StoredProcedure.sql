USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_CreateRefund]    Script Date: 06/07/2017 09:17:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Statement_CreateRefund]

	@StatementID	INT

AS

DECLARE	@CampaignID			INT,
		@AccountID			INT,
		@CampaignBalance	NUMERIC(10, 2)

SELECT	@CampaignID = camp.ID,
		@AccountID = camp.BillToAccountID,
		@CampaignBalance = stat.Balance
FROM	[Statement] stat
JOIN	QSPCanadaCommon..Campaign camp
			ON	camp.ID = stat.CampaignID
WHERE	stat.StatementID = @StatementID

DECLARE	@AccountAddress1	VARCHAR(50),		@AccountAddress2	VARCHAR(50),
		@AccountCity		VARCHAR(50),
		@AccountProvince	VARCHAR(25),
		@AccountPostalCode	VARCHAR(15),
		@AccountCountry		VARCHAR(25),
		@ChangedBy 			INT,
		@ErrorMessage		VARCHAR(200),
		@RefundID			INT,
		@AP_Cheque_ID		INT

SET @ChangedBy = -1

SELECT		@AccountAddress1 = ISNULL(accAdd.Street1, ''),
			@AccountAddress2 = ISNULL(accAdd.Street2, ''),
			@AccountCity = ISNULL(accAdd.City, ''),
			@AccountProvince = ISNULL(accAdd.StateProvince, ''),
			@AccountPostalCode = SUBSTRING(accAdd.Postal_Code, 1, 3) + ' ' + SUBSTRING(accAdd.Postal_Code, 4 ,3),
			@AccountCountry = ISNULL(accAdd.Country, '')
FROM		[Statement] stat
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = stat.AccountID
JOIN		QSPCanadaCommon..Address accAdd
				ON	accAdd.AddressListID = acc.AddressListID
				AND	accAdd.Address_Type = 54002 -- Billto
WHERE		stat.StatementID = @StatementID


DECLARE @RefundAmount NUMERIC(12, 2)
SET @RefundAmount = -1 * @CampaignBalance

IF @RefundAmount > 0.00
BEGIN

	EXEC AP_Refund_Group_CreateCheque
			@CampaignID, @AccountID, @AccountAddress1, @AccountAddress2, @AccountCity, @AccountProvince,
			@AccountPostalCode, @AccountCountry, @RefundAmount, @ChangedBy, @ErrorMessage OUTPUT, @RefundID OUTPUT, @AP_Cheque_ID OUTPUT

	UPDATE	[Statement]
	SET		AP_Cheque_ID = @AP_Cheque_ID
	WHERE	StatementID = @StatementID
END
GO
