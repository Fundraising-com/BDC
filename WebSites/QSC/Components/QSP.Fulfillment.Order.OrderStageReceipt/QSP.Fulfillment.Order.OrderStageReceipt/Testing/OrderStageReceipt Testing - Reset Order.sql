USE [QSPCanadaOrderManagement]

DECLARE @Filename varchar(137)
SET @Filename = 'OrderStageReceiptSample.xml'

DELETE	osr
FROM	OrderStageReceiptBatch osrb
JOIN	OrderStageReceipt osr
			ON	osr.OrderStageReceiptBatchID = osrb.OrderStageReceiptBatchID
WHERE	osrb.Filename = @Filename

DELETE	osrb
FROM	OrderStageReceiptBatch osrb
WHERE	osrb.Filename = @Filename

DELETE	ost
FROM	OrderStageTracking ost
WHERE	ost.ResolveFilename = @Filename