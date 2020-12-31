USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_Batch_Group_Create]    Script Date: 06/07/2017 09:17:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AP_Cheque_Batch_Group_Create]

	@AP_Cheque_Batch_ID	INT OUTPUT

AS

DECLARE @RunDate			DATETIME,
		@ChequeType			VARCHAR(50),
		@Refund_ID			INT,
		@ChequeNumber		BIGINT,
		@AP_Cheque_ID		INT,
		@BankAccountID		INT

SET	@RunDate = GETDATE()
SET @ChequeType = 'Group Refund - Standalone'
SET @BankAccountID = 6

SELECT		TOP 1 1
FROM		Refund ref
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		apc.AP_Cheque_ID IS NULL
AND			ref.Refund_Type_ID IN (3, 4) --3: Loyalty Bonus, 4: Refer A Friend
AND			ref.Cancelled = 0
AND			ref.CreateDate > '2009-01-28 15:20' --Started sending Group Refunds directly at this time

IF @@ROWCOUNT = 0
BEGIN
	SET	@AP_Cheque_Batch_ID = 0
	RETURN
END

BEGIN TRANSACTION

INSERT INTO AP_Cheque_Batch
(
	CreationDate,
	[Type]
)
SELECT	@RunDate,
		@ChequeType

SET @AP_Cheque_Batch_ID = SCOPE_IDENTITY()

DECLARE	Refund CURSOR FOR
SELECT		ref.Refund_ID
FROM		Refund ref
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		apc.AP_Cheque_ID IS NULL
AND			ref.Refund_Type_ID IN (3, 4) --3: Loyalty Bonus, 4: Refer A Friend
AND			ref.Cancelled = 0
AND			ref.CreateDate > '2009-01-28 15:20' --Started sending Group Refunds directly at this time
ORDER BY	ref.Amount DESC --Set the order of the cheques by Amount so it is easy to match up to the Credit Statements

OPEN Refund
FETCH NEXT FROM Refund INTO @Refund_ID

WHILE @@FETCH_STATUS = 0
BEGIN	EXEC AP_Cheque_CreateFromRefund		@Refund_ID = @Refund_ID,		@AP_Cheque_ID = @AP_Cheque_ID OUTPUT
	UPDATE	AP_Cheque
	SET		AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
	WHERE	AP_Cheque_ID = @AP_Cheque_ID

	FETCH NEXT FROM Refund INTO @Refund_ID

END
CLOSE Refund
DEALLOCATE Refund

COMMIT
GO
