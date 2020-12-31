USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_Group_Create]    Script Date: 06/07/2017 09:17:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_Group_Create]

	@CampaignID			INT,
	@AccountID			INT,
	@Address1			VARCHAR(50),	@Address2			VARCHAR(50),
	@City				VARCHAR(50),
	@Province			VARCHAR(25),
	@PostalCode			VARCHAR(15),
	@Country			VARCHAR(25),
	@RefundTypeID		INT,
	@RefundAmount		NUMERIC(12,2),
	@ChangedBy			INT,
	@RefundID 			INT OUTPUT

AS

DECLARE @RunDate			DATETIME,
		@AdjustmentTypeID	INT,
		@AdjustmentTypeName	VARCHAR(50),
		@AdjustmentID		INT,
		@ChequeNumber		BIGINT,
		@BankAccountID		INT

SET @RunDate = GETDATE()
SET @BankAccountID = 6

SELECT	@AdjustmentTypeID = Adjustment_Type_ID
FROM	Refund_Type
WHERE	Refund_Type_ID = @RefundTypeID

SELECT	@AdjustmentTypeName = [Name]
FROM	Adjustment_Type
WHERE	Adjustment_Type_ID = @AdjustmentTypeID

BEGIN TRANSACTION

EXEC AddGroupRefundRecord NULL, @RefundTypeID, @RefundAmount, @Address1, @Address2, @City, @Province, @PostalCode, @Country, 
		@RunDate, 'SYSTEM', NULL, NULL, @CampaignID, NULL, NULL, NULL, @RefundID OUTPUT

EXEC QSPCanadaFinance.dbo.AddInvoiceAdjustment @AccountID, NULL, 'GROUP REFUND', @RefundAmount, @CampaignID, @AdjustmentTypeID, @ChangedBy, @RefundID, @AdjustmentID OUTPUT 

COMMIT
GO
