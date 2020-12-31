USE [QSPCanadaOrderManagement]

DECLARE	@OrderID			INT
DECLARE @IsFieldSupplyOrder	BIT
DECLARE	@SentForShipment	BIT

SET	@OrderID = 1108993
      
SELECT	b.OrderID, b.OrderQualifierID, camp.Status CampaignStatus, *
FROM	Batch b
JOIN	QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
WHERE	b.OrderID = @OrderID

SELECT	cod.*, *
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN	Customer cust
			ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
WHERE	b.orderid = @OrderID

/*
SELECT	*
FROM	ShipmentRequestOrder sro
JOIN	ShipmentRequestCustomerOrderHeader srcoh
			ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
JOIN	ShipmentRequestCustomerOrderDetail srcod
			ON	srcod.ShipmentRequestCustomerOrderHeaderID = srcoh.ShipmentRequestCustomerOrderHeaderID
WHERE	sro.OrderID = @OrderID
*/

SELECT	@IsFieldSupplyOrder = CASE OrderQualifierID WHEN 39007 THEN 1 ELSE 0 END
FROM	Batch
WHERE	OrderID = @OrderID

UPDATE	batch
SET		batch.StatusInstance = 40005
FROM	Batch b
WHERE	orderID = @OrderID

UPDATE	cod
SET		cod.DelFlag = 1
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

/*
DELETE	srcod
FROM	ShipmentRequestOrder sro
JOIN	ShipmentRequestCustomerOrderHeader srcoh
			ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
JOIN	ShipmentRequestCustomerOrderDetail srcod
			ON	srcod.ShipmentRequestCustomerOrderHeaderID = srcoh.ShipmentRequestCustomerOrderHeaderID
WHERE	sro.OrderID = @OrderID

DELETE	srcoh
FROM	ShipmentRequestOrder sro
JOIN	ShipmentRequestCustomerOrderHeader srcoh
			ON	srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
WHERE	sro.OrderID = @OrderID

DELETE	sro
FROM	ShipmentRequestOrder sro
WHERE	sro.OrderID = @OrderID
*/

IF @IsFieldSupplyOrder = 1
BEGIN

	USE [QSPCanadaCommon]

	UPDATE	camp
	SET		FSOrderRecCreated = 0
	FROM	Campaign camp
	JOIN	QSPCanadaOrderManagement..Batch batch
				ON	batch.CampaignID = camp.ID
	WHERE	batch.OrderID = @OrderID

END