USE [QSPCanadaOrderManagement]

DECLARE @Filename	VARCHAR(137)
SET @Filename = 'tiunishp.20090911133954.gpg'

SELECT	*
FROM	ShipmentReceiptBatch srb
JOIN	ShipmentReceipt sr
			ON	sr.ShipmentReceiptBatchID = srb.ShipmentReceiptBatchID
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = sr.CustomerOrderHeaderInstance
			AND	cod.TransID = sr.TransID
WHERE	srb.Filename = @Filename
ORDER BY sr.ShipmentReceiptID

DECLARE @IsAlreadyProcessed	BIT
SELECT	@IsAlreadyProcessed = CONVERT(BIT, COUNT(*))
FROM	ShipmentReceiptBatch srb
JOIN	ShipmentReceipt sr
			ON	sr.ShipmentReceiptBatchID = srb.ShipmentReceiptBatchID
WHERE	srb.Filename = @Filename
AND		sr.StatusID = 2 --2: Processed

IF @IsAlreadyProcessed = 1
BEGIN

	UPDATE	cod
	SET		cod.StatusInstance = 509,
			cod.ShipmentID = NULL
	FROM	CustomerOrderDetail cod
	JOIN	ShipmentReceipt sr
				ON	sr.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	sr.TransID = cod.TransID
	JOIN	ShipmentReceiptBatch srb
				ON	srb.ShipmentReceiptBatchID = sr.ShipmentReceiptBatchID
	WHERE	srb.Filename = @Filename
	AND		sr.StatusID = 2 --2: Processed

	UPDATE	sr
	SET		sr.StatusID = 3 --3: Disabled from being Processed
	FROM	ShipmentReceipt sr
	JOIN	ShipmentReceiptBatch srb
				ON	srb.ShipmentReceiptBatchID = sr.ShipmentReceiptBatchID
	WHERE	srb.Filename = @Filename
	AND		sr.StatusID = 2 --2: Processed

END

--Must add these eventually
/*delete ShipmentVariation
where CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
and TransID = @TransID

delete	ship
FROM	Shipment ship
JOIN	CustomerOrderDetail cod
			ON	cod.ShipmentID = ship.ID
where CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
and TransID = @TransID

UPDATE	b
SET		StatusInstance = 40012
FROM	Batch b
WHERE	b.OrderID = @OrderID

UPDATE	bdc
SET		StatusInstance = 40012
FROM	BatchDistributionCenter bdc
WHERE	bdc.BatchID = @BatchID
AND		bdc.BatchDate = @BatchDate

--Remove waybill entry
*/