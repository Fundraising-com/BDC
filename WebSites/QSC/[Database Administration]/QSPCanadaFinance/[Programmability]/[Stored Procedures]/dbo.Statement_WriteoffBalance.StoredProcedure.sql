USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_WriteoffBalance]    Script Date: 06/07/2017 09:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Statement_WriteoffBalance]

	@CampaignID	INT,
	@DateTo		DATETIME

AS

DECLARE	@CampaignBalance	NUMERIC(10, 2),
		@AccountID			INT,
		@AdjustmentType		INT,
		@Value				INT,
		@AbsCampaignBalance	NUMERIC(10, 2)

SELECT	@AccountID = camp.BillToAccountID
FROM	QSPCanadaCommon..Campaign camp
WHERE	camp.ID = @CampaignID

SET @CampaignBalance = dbo.UDF_Account_GetBalance(@CampaignID, NULL, @DateTo)

IF @CampaignBalance < 0.00
BEGIN
	SET @AdjustmentType = 49021
END
ELSE
BEGIN
	SET @AdjustmentType = 49022
END

SET @AbsCampaignBalance = ABS(@CampaignBalance)

IF @CampaignBalance <> 0.00
BEGIN

	EXEC AddInvoiceAdjustment
		@AccountID = @AccountID,
		@OrderID = NULL,
		@InternalComment = 'Writeoff for amount less than $5',
		@Amount = @AbsCampaignBalance,
		@CampaignID = @CampaignID,
		@AdjustmentType = @AdjustmentType,
		@ChangedBy = -1,
		@Value = @Value OUTPUT

END
GO
