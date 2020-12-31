DECLARE @OrderID INT
SET	@OrderID = 9720369

SELECT	ship.*
FROM	Shipment ship
JOIN	ShipmentOrder so
			ON	so.ShipmentID = ship.ID
WHERE	so.OrderID = @OrderID

BEGIN TRANSACTION

UPDATE	ship
SET		FMEmailNotificationSent = '1995-01-01'
FROM	Shipment ship
JOIN	ShipmentOrder so
			ON	so.ShipmentID = ship.ID
WHERE	so.OrderID = @OrderID

COMMIT TRANSACTION