USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_Group_CreateFromAdjustment]    Script Date: 06/07/2017 09:17:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_Group_CreateFromAdjustment]

	@AdjustmentID		INT

AS

DECLARE	@RunDate					DATETIME,
		@RefundTypeID				INT,
		@RefundAmount				NUMERIC(12, 2),
		@AdjustmentTypeID			INT,
		@AdjustmentTypeRecipientID	INT,
		@CampaignID					INT,
		@AccountID					INT,
		@Address1					VARCHAR(50),		@Address2					VARCHAR(50),
		@City						VARCHAR(50),
		@Province					VARCHAR(25),
		@PostalCode					VARCHAR(15),
		@Country					VARCHAR(25),
		@ChequeNumber				BIGINT,
		@BankAccountID				INT,
		@RefundID					INT,
		@ErrorMessage				VARCHAR(200),
		@AP_Cheque_ID				INT

SET @RunDate = GETDATE()
SET @BankAccountID = 6

SELECT	@AdjustmentTypeID = adjType.Adjustment_Type_ID,
		@AdjustmentTypeRecipientID = adjType.Adjustment_Type_Recipient_ID,
		@RefundTypeID = adjType.Refund_Type_ID,
		@CampaignID = adj.Campaign_ID,
		@AccountID = adj.Account_ID,
		@RefundAmount = ABS(adj.Adjustment_Amount)
FROM	Adjustment_Type adjType
JOIN	Adjustment adj
			ON	adj.Adjustment_Type_ID = adjType.Adjustment_Type_ID
WHERE	adj.Adjustment_ID = @AdjustmentID

--Code below should be refactored so that the refund has a payee address as well as a delivery address

SELECT		@Address1 = ISNULL(accAdd.Street1, ''),
			@Address2 = ISNULL(accAdd.Street2, ''),
			@City = ISNULL(accAdd.City, ''),
			@Province = ISNULL(accAdd.StateProvince, ''),
			@PostalCode = SUBSTRING(accAdd.Postal_Code, 1, 3) + ' ' + SUBSTRING(accAdd.Postal_Code, 4 ,3),
			@Country = ISNULL(accAdd.Country, '')
FROM		QSPCanadaCommon..CAccount acc
JOIN		QSPCanadaCommon..Address accAdd
				ON	accAdd.AddressListID = acc.AddressListID
				AND	accAdd.Address_Type = 54002 -- Billto
WHERE		acc.ID = @AccountID

/*IF @AdjustmentTypeRecipientID = 1 --1: QSP
BEGIN
	SET @Address1 = '695 Riddell Road'
	SET @Address2 = NULL
	SET @City = 'Orangeville'
	SET @Province = 'ON'
	SET @PostalCode = 'L9W 4Z5'
	SET @Country = 'CA'
END
ELSE IF @AdjustmentTypeRecipientID = 2 --2: Group
BEGIN
	SELECT		@Address1 = ISNULL(accAdd.Street1, ''),
				@Address2 = ISNULL(accAdd.Street2, ''),
				@City = ISNULL(accAdd.City, ''),
				@Province = ISNULL(accAdd.StateProvince, ''),
				@PostalCode = SUBSTRING(accAdd.Postal_Code, 1, 3) + ' ' + SUBSTRING(accAdd.Postal_Code, 4 ,3),
				@Country = ISNULL(accAdd.Country, '')
	FROM		QSPCanadaCommon..CAccount acc
	JOIN		QSPCanadaCommon..Address accAdd
					ON	accAdd.AddressListID = acc.AddressListID
					AND	accAdd.Address_Type = 54002 -- Billto
	WHERE		acc.ID = @AccountID
END
ELSE IF @AdjustmentTypeRecipientID = 3 --3: FM
BEGIN
	SELECT		@Address1 = ISNULL(fmAdd.Street1, ''),
				@Address2 = ISNULL(fmAdd.Street2, ''),
				@City = ISNULL(fmAdd.City, ''),
				@Province = ISNULL(fmAdd.StateProvince, ''),
				@PostalCode = SUBSTRING(fmAdd.Postal_Code, 1, 3) + ' ' + SUBSTRING(fmAdd.Postal_Code, 4 ,3),
				@Country = ISNULL(fmAdd.Country, '')
	FROM		QSPCanadaCommon..Campaign camp
	JOIN		QSPCanadaCommon..FieldManager fm
					ON	fm.FMID = camp.FMID
	JOIN		QSPCanadaCommon..Address fmAdd
					ON	fmAdd.AddressListID = fm.AddressListID
					AND	fmAdd.Address_Type = 54004 --54004: Home
	WHERE		camp.ID = @CampaignID
END*/

BEGIN TRANSACTION

EXEC AP_Refund_Group_Create
	@CampaignID = @CampaignID,
	@AccountID = @AccountID,
	@Address1 =	@Address1,	@Address2 =	@Address2,
	@City =	@City,
	@Province =	@Province,
	@PostalCode = @PostalCode,
	@Country = @Country,
	@RefundTypeID = @RefundTypeID,
	@RefundAmount = @RefundAmount,
	@ChangedBy = -1,
	@RefundID = @RefundID OUTPUT

UPDATE	Adjustment
SET		Refund_ID = @RefundID
WHERE	Adjustment_ID = @AdjustmentID

COMMIT
GO
