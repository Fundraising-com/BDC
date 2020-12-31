USE [QSPCanadaOrderManagement]

DECLARE	@OrderID			INT
DECLARE @IsFieldSupplyOrder	BIT
DECLARE	@SentForShipment	BIT

SET	@OrderID = 10066994

SELECT	*
FROM	Batch b
WHERE	b.OrderID = @OrderID

SELECT	cod.*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.OrderID = @OrderID

SELECT	*
FROM	ShipmentRequestOrder sro
JOIN	ShipmentRequestCustomerOrderHeader srcoh
			ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
JOIN	ShipmentRequestCustomerOrderDetail srcod
			ON	srcod.ShipmentRequestCustomerOrderHeaderID = srcoh.ShipmentRequestCustomerOrderHeaderID
WHERE	sro.OrderID = @OrderID

SELECT	@IsFieldSupplyOrder = CASE OrderQualifierID WHEN 39007 THEN 1 ELSE 0 END
FROM	Batch
WHERE	OrderID = @OrderID

UPDATE	batch
SET		batch.StatusInstance = 40010
FROM	Batch b
WHERE	orderID = @OrderID

UPDATE	cod
SET		cod.DelFlag = 0
FROM	CustomerOrderDetail cod
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
WHERE	b.OrderID = @OrderID

UPDATE	QSPCanadaCommon..SystemErrorLog
SET		IsReviewed = 1,
		IsFixed = 1
WHERE	OrderID = @OrderID

IF @IsFieldSupplyOrder = 1
BEGIN

	USE [QSPCanadaCommon]

	UPDATE	camp
	SET		FSOrderRecCreated = 1
	FROM	Campaign camp
	JOIN	QSPCanadaOrderManagement..Batch batch
				ON	batch.CampaignID = camp.ID
	WHERE	batch.OrderID = @OrderID

END