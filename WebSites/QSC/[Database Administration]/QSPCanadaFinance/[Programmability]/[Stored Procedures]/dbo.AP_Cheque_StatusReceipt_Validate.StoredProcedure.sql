USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_StatusReceipt_Validate]    Script Date: 06/07/2017 09:17:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_StatusReceipt_Validate]

	@ChequeStatusReceiptID		INT,
	@IsChequeStatusReceiptValid	BIT OUTPUT

AS

SET @IsChequeStatusReceiptValid = CONVERT(BIT, 1)

DECLARE	@Error							BIT,
		@ErrorMessage					VARCHAR(1000),
		@RecExist						BIT,
		@AP_Cheque_ID					INT

SELECT		TOP 1
			@Error = 1
FROM		AP_Cheque_StatusReceipt apcsr
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = apcsr.AP_Cheque_ID
WHERE		apcsr.AP_Cheque_StatusReceipt_ID = @ChequeStatusReceiptID
AND			apc.AP_Cheque_ID IS NULL

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	AP_Cheque_StatusReceipt
	SET		AP_Cheque_StatusReceipt_Status_ID = 4 --4: AP_Cheque_ID was not in an Outstanding state
	WHERE	AP_Cheque_StatusReceipt_ID = @ChequeStatusReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsChequeStatusReceiptValid = CONVERT(BIT, 0)
END

SELECT		TOP 1
			@Error = CONVERT(BIT, 1)
FROM		AP_Cheque_StatusReceipt apcsrCurrent
JOIN		AP_Cheque_StatusReceipt_Batch apcsrbCurrent
				ON	apcsrbCurrent.AP_Cheque_StatusReceipt_Batch_ID = apcsrCurrent.AP_Cheque_StatusReceipt_Batch_ID
JOIN		AP_Cheque_StatusReceipt_Batch apcsrbExisting
				ON	apcsrbExisting.Filename = apcsrbCurrent.Filename
JOIN		AP_Cheque_StatusReceipt apcsrExisting
				ON	apcsrExisting.AP_Cheque_StatusReceipt_Batch_ID = apcsrbExisting.AP_Cheque_StatusReceipt_Batch_ID
WHERE		apcsrCurrent.AP_Cheque_StatusReceipt_ID = @ChequeStatusReceiptID
AND			apcsrExisting.AP_Cheque_StatusReceipt_Status_ID IN (2) --2: Enabled

IF ISNULL(@Error, 0) = 1
BEGIN
	UPDATE	AP_Cheque_StatusReceipt
	SET		AP_Cheque_StatusReceipt_Status_ID = 5 --5: Cheque Status Receipt File for Cheque was already Validateed
	WHERE	AP_Cheque_StatusReceipt_ID = @ChequeStatusReceiptID
	
	SET @Error = CONVERT(BIT, 0)
	SET @IsChequeStatusReceiptValid = CONVERT(BIT, 0)
END
GO
