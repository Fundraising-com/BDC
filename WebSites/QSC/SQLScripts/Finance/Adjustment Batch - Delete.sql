USE [QSPCanadaFinance]

SELECT	*
FROM	AdjustmentBatch

SELECT	*
FROM	Adjustment
WHERE	Adjustment_Batch_ID = 37

DELETE	Adjustmentbatch
WHERE	ID = 37

DELETE	adjustment
WHERE	adjustment_batch_id = 37
