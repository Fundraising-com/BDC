USE QSPCanadaOrderManagement

DECLARE @OrderID INT
SET @OrderID = 916593

SELECT	*
FROM	OrderStageReceipt
WHERE	OrderID = @OrderID

INSERT INTO OrderStageReceiptBatch
VALUES (GETDATE(), -1, 1, NULL, GETDATE())

DECLARE @OrderStageReceiptBatchID INT
SET @OrderStageReceiptBatchID = SCOPE_IDENTITY()

INSERT INTO OrderStageReceipt
VALUES (@OrderStageReceiptBatchID, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 59008, NULL, NULL, 
		NULL, NULL, NULL, @OrderID, 0, 56001, NULL, GETDATE())

DECLARE @OrderStageReceiptID INT
SET @OrderStageReceiptID = SCOPE_IDENTITY()

EXEC OrderStageReceipt_UpdateOrderStageInfo @OrderStageReceiptID