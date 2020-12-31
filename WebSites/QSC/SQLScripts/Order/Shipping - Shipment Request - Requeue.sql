USE [QSPCanadaOrderManagement]

--Must rename the original files
--May need to requeue the batch reports first
--Todo, delete the original shipmentrequest, or fix shipment_selectall to select the latest one

DECLARE @OrderID INT
SET @OrderID = 10180422

SELECT	*
FROM	CustomerOrderDetail cod
JOIN	Customerorderheader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch batch
			ON	batch.ID = coh.OrderBatchID
			AND	batch.date = coh.OrderBatchDate
WHERE	batch.OrderID = @OrderID
AND		cod.DistributionCenterID = 2


UPDATE	batch
SET		StatusInstance = 40010
FROM	Batch batch
WHERE	batch.OrderID = @OrderID

UPDATE	cod
SET		StatusInstance = 509
FROM	CustomerOrderDetail cod
JOIN	Customerorderheader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch batch
			ON	batch.ID = coh.OrderBatchID
			AND	batch.date = coh.OrderBatchDate
WHERE	batch.OrderID = @OrderID
AND		cod.DistributionCenterID = 2

UPDATE	QSPCanadaCommon..SystemErrorLog
SET		IsFixed = 1
WHERE	OrderID = @OrderID

delete	srcod
from	ShipmentRequestOrder sro
join	ShipmentRequestCustomerOrderHeader srcoh on srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
join	ShipmentRequestCustomerOrderDetail srcod on srcod.ShipmentRequestCustomerOrderHeaderID = srcoh.ShipmentRequestCustomerOrderHeaderID
where	OrderID = @OrderID 

delete	srcoh
from	ShipmentRequestOrder sro
join	ShipmentRequestCustomerOrderHeader srcoh on srcoh.ShipmentRequestOrderID = sro.ShipmentRequestOrderID
where	OrderID = @OrderID 

delete	sro
from	ShipmentRequestOrder sro
where	OrderID = @OrderID 