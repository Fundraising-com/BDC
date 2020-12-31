--Adding 1 Adjustment to GL
DECLARE @AccountID int
DECLARE @OrderID int
DECLARE @AdjustmentID int
DECLARE @AdjustmentType int
DECLARE @Amount numeric(10,2)
DECLARE @ChangedBy varchar(20)

SET @AccountID = 814
SET @OrderID = 802011
SET @AdjustmentID = 76791
SET @AdjustmentType = 49007
SET @Amount = 6.00
SET @ChangedBy = -1
EXEC  [QSPCanadaFinance].[dbo].[AddGLEntriesForAdjustment] @AccountID, @OrderID, @AdjustmentID, @AdjustmentType, @Amount, @ChangedBy

--Adding many adjustments to GL
SELECT		*
INTO		#AdjustmentsNoGL
FROM		Adjustment
WHERE		Adjustment_ID BETWEEN 82874 AND 83331

SELECT	*
FROM	#AdjustmentsNoGL

BEGIN TRAN t1

DECLARE @Account_ID			INT
DECLARE @Order_ID			INT
DECLARE @Adjustment_ID		INT
DECLARE @Adjustment_Type_ID	INT
DECLARE @Adjustment_Amount	NUMERIC(10,2)
DECLARE @ChangedBy			VARCHAR(20)

SET @ChangedBy = -1

DECLARE Adjustments CURSOR FOR
SELECT	Account_ID
		Adjustment_ID,
		Adjustment_Type_ID,
		CONVERT(NUMERIC(10, 2), Adjustment_Amount)
FROM	#AdjustmentsNoGL

OPEN	Adjustments
FETCH	Next FROM Adjustments INTO @Account_ID, @Adjustment_ID, @Adjustment_Type_ID, @Adjustment_Amount
	
WHILE @@FETCH_STATUS = 0
BEGIN	
	
	EXEC  AddGLEntriesForAdjustment @Account_ID, @Order_ID, @Adjustment_ID, @Adjustment_Type_ID, @Adjustment_Amount, @ChangedBy

FETCH	Next FROM Adjustments INTO @Account_ID, @Adjustment_ID, @Adjustment_Type_ID, @Adjustment_Amount
END
CLOSE Adjustments
DEALLOCATE Adjustments
COMMIT TRAN t1

DROP TABLE #AdjustmentsNoGL
