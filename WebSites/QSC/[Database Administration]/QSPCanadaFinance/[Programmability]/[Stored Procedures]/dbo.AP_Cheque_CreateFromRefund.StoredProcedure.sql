USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_CreateFromRefund]    Script Date: 06/07/2017 09:17:03 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AP_Cheque_CreateFromRefund]

	@Refund_ID		INT,
	@AP_Cheque_ID	INT OUTPUT

AS

DECLARE @RunDate			DATETIME,
		@RefundTypeID		INT,
		@ChequeNumber		BIGINT,
		@BankAccountID		INT

SET	@RunDate = GETDATE()
SET @BankAccountID = 6

BEGIN TRANSACTION

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
SELECT		2, --2: Outstanding
			0,--@ChequeNumber,
			@RunDate,
			@BankAccountID,
			CONVERT(VARCHAR(10), @RunDate, 120)
			
SET @AP_Cheque_ID = SCOPE_IDENTITY()
	
UPDATE	Refund
SET		AP_Cheque_ID = @AP_Cheque_ID
WHERE	Refund_ID = @Refund_ID

COMMIT
GO
