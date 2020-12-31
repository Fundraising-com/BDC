select *
from Shipmentorder
where orderid = 1090554

select *
from shipment
where id = 1001133

select *
from ShipmentWayBill
where shipmentid = 1001133 

SELECT	*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	OrderID = 1090554

begin tran

--Delete Invoice

delete ShipmentOrder
where ShipmentID = 1001133

delete Shipment
where ID = 1001133

delete ShipmentWayBill
where shipmentid = 1001133 

update Batch
set StatusInstance = 40010
where OrderID = 1090554

update CustomerOrderDetail
set ShipmentID = null, statusinstance = 509
where ShipmentID = 1001133

UPDATE	BatchDistributionCenter
SET		StatusInstance = 40010
WHERE	BatchID = 20000
AND		BatchDate = '2014-12-21 00:00:00.000'

--Notify FM it hasn't shipped

COMMIT
