USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_Batch_Customer_Create]    Script Date: 06/07/2017 09:17:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_Batch_Customer_Create]

	@AP_Cheque_Batch_ID	INT OUTPUT

AS

DECLARE @RunDate			DATETIME,
		@ChequeType			VARCHAR(50),
		@Refund_ID			INT,
		@ChequeNumber		BIGINT,
		@AP_Cheque_ID		INT,
		@BankAccountID		INT

SET	@RunDate = GETDATE()
SET @ChequeType = 'Customer Refund'
SET @BankAccountID = 6

SELECT		TOP 1 1
FROM		Refund ref
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = ref.CustomerOrderHeaderInstance
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		apc.AP_Cheque_ID IS NULL
AND			ref.Refund_Type_ID = 1 --Customer Refund
AND			ref.Cancelled = 0
AND			ref.CreateDate > '2009-02-06 11:46' --Started sending Customer Refunds directly at this time
AND			ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 1 --Only send Time refunds automatically

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
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = ref.CustomerOrderHeaderInstance
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.Date = coh.OrderBatchDate
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		apc.AP_Cheque_ID IS NULL
AND			ref.Refund_Type_ID = 1 --Customer Refund
AND			ref.Cancelled = 0
AND			ref.CreateDate > '2009-02-06 11:46' --Started sending Customer Refunds directly at this time
AND			ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 1 --Only send Time refunds automatically
OPEN Refund
FETCH NEXT FROM Refund INTO @Refund_ID

WHILE @@FETCH_STATUS = 0
BEGIN		EXEC AP_Cheque_CreateFromRefund		@Refund_ID = @Refund_ID,		@AP_Cheque_ID = @AP_Cheque_ID OUTPUT
	UPDATE	AP_Cheque
	SET		AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
	WHERE	AP_Cheque_ID = @AP_Cheque_ID

	FETCH NEXT FROM Refund INTO @Refund_ID

END
CLOSE Refund
DEALLOCATE Refund

COMMIT
GO
