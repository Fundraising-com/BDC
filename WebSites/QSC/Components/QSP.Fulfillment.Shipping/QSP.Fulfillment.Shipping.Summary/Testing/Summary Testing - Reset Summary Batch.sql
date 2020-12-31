use [QSPCanadaOrderManagement]

declare @OrderID int
--set @OrderID = 704463 --nonbhe
set @OrderID = 9583714 --bhe

DELETE	sb
FROM	ShipmentBatch sb
JOIN	ShipmentOrder so
			ON	so.ShipmentBatchID = sb.ID
WHERE	so.OrderID = @OrderID

UPDATE	ShipmentOrder
SET		ShipmentBatchID = NULL
WHERE	OrderID = @OrderID

SELECT	*
FROM	ShipmentOrder
WHERE	ShipmentBatchID IS NULL