USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_Group_CreateFromStatement]    Script Date: 06/07/2017 09:17:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_Group_CreateFromStatement]

	@StatementID	INT,
	@RefundID		INT OUTPUT

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

DECLARE	@Address1	VARCHAR(50),		@Address2	VARCHAR(50),
		@City		VARCHAR(50),
		@Province	VARCHAR(25),
		@PostalCode	VARCHAR(15),
		@Country		VARCHAR(25),
		@RefundTypeID		INT,
		@ChangedBy 			INT,
		@ErrorMessage		VARCHAR(200),
		@AP_Cheque_ID		INT

SET @ChangedBy = -1
SET @RefundTypeID = 2 --2: Group Refund

SELECT		@Address1 = ISNULL(accAdd.Street1, ''),
			@Address2 = ISNULL(accAdd.Street2, ''),
			@City = ISNULL(accAdd.City, ''),
			@Province = ISNULL(accAdd.StateProvince, ''),
			@PostalCode = SUBSTRING(accAdd.Postal_Code, 1, 3) + ' ' + SUBSTRING(accAdd.Postal_Code, 4 ,3),
			@Country = ISNULL(accAdd.Country, '')
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

	EXEC AP_Refund_Group_Create
		@CampaignID = @CampaignID,
		@AccountID = @AccountID,
		@Address1 =	@Address1,		@Address2 =	@Address2,
		@City =	@City,
		@Province =	@Province,
		@PostalCode = @PostalCode,
		@Country = @Country,
		@RefundTypeID = @RefundTypeID,
		@RefundAmount = @RefundAmount,
		@ChangedBy = -1,
		@RefundID = @RefundID OUTPUT

	UPDATE	[Statement]
	SET		Refund_ID = @RefundID
	WHERE	StatementID = @StatementID

END
GO
