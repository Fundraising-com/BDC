USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_Group_CreateCheque]    Script Date: 06/07/2017 09:17:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Refund_Group_CreateCheque]

	@CampaignID			INT,
	@RefundAccountID	INT,
	@Address1			VARCHAR(50),	@Address2			VARCHAR(50),
	@City				VARCHAR(50),
	@Province			VARCHAR(25),
	@PostalCode			VARCHAR(15),
	@Country			VARCHAR(25),
	@RefundAmount		NUMERIC(12,2),
	@ChangedBy			INT,
	@ErrorMessage		VARCHAR(200) OUTPUT,
	@RefundID 			INT OUTPUT,
	@AP_Cheque_ID		INT OUTPUT

AS

DECLARE @RunDate			DATETIME,
		@RefundType			INT,
		@AdjustmentTypeID	INT,
		@AdjustmentTypeName	VARCHAR(50),
		@AdjustmentID		INT,
		@ChequeNumber		BIGINT,
		@BankAccountID		INT

SET @RunDate = GETDATE()
SET	@RefundType = 2
SET @AdjustmentTypeID = 49024 --Refund Cheque
SET @BankAccountID = 6

SELECT	@AdjustmentTypeName = [Name]
FROM	Adjustment_Type
WHERE	Adjustment_Type_ID = @AdjustmentTypeID

SET @AdjustmentTypeName = @AdjustmentTypeName + ' - Group Refund'

BEGIN TRANSACTION

EXEC AddGroupRefundRecord NULL, @RefundType, @RefundAmount, @Address1, @Address2, @City, @Province, @PostalCode, @Country, 
		@RunDate, 'SYSTEM', NULL, NULL, @CampaignID, NULL, NULL, NULL, @RefundID OUTPUT

IF @@ERROR <> 0 OR ISNULL(@RefundID, 0) = 0
BEGIN
	ROLLBACK
	SET @ErrorMessage = 'Failed to insert refund header record'
	INSERT INTO QSPCanadaFinance..GROUP_REFUND_ERRORLOG VALUES (@RunDate, @RefundAccountID, @CampaignID, @RefundAmount, @ErrorMessage, NULL)
	RETURN
END

EXEC QSPCanadaFinance.dbo.AddInvoiceAdjustment @RefundAccountID, NULL, 'GROUP REFUND', @RefundAmount, @CampaignID, @AdjustmentTypeID, @ChangedBy, @RefundID, @AdjustmentID OUTPUT 

IF @@ERROR <> 0 OR ISNULL(@AdjustmentID, 0) = 0    
BEGIN
	ROLLBACK
	SET @ErrorMessage = 'Failed to insert Adjustment record'
	INSERT INTO QSPCanadaFinance.dbo.GROUP_REFUND_ERRORLOG VALUES (@RunDate, @RefundAccountID, @CampaignID, @RefundAmount, @ErrorMessage, NULL)
	RETURN
END

SELECT	@ChequeNumber = ISNULL(MAX(ChequeNumber), 0) + 1
FROM	AP_Cheque
WHERE	Bank_Account_ID = @BankAccountID

INSERT AP_Cheque
(
	AP_Cheque_Status_ID,
	ChequeNumber,
	CreationDate,
	Bank_Account_ID,
	ChequePayableDate
)
SELECT	2, --2: Outstanding
		@ChequeNumber,
		@RunDate,
		@BankAccountID,
		CONVERT(VARCHAR(10), @RunDate, 120)

SET @AP_Cheque_ID = SCOPE_IDENTITY()

IF @@ERROR <> 0 OR ISNULL(@AP_Cheque_ID, 0) = 0
BEGIN
	ROLLBACK
	SET @ErrorMessage = 'Group Refund - Error - Failed to insert Group Refund Cheque record'
	RETURN
END

UPDATE	Refund
SET		AP_Cheque_ID = @AP_Cheque_ID
WHERE	Refund_ID = @RefundID

COMMIT
GO
