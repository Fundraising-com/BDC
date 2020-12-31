use [QSPCanadaOrderManagement]

declare @CustomerOrderHeaderInstance int
declare @TransID int
declare @OrderID int

set @CustomerOrderHeaderInstance = 9375947
set @TransID = 1
set @OrderID = 9583714 --nonbhe

DECLARE @Filename varchar(137)
SET @Filename = 'ShippingNotificationSample.xml'

DELETE	sr
FROM	ShipmentReceiptBatch srb
JOIN	ShipmentReceipt sr
			ON	sr.ShipmentReceiptBatchID = srb.ShipmentReceiptBatchID
WHERE	srb.Filename = @Filename

DELETE	srb
FROM	ShipmentReceiptBatch srb
WHERE	srb.Filename = @Filename

SELECT b.OrderQualifierID, cod.ProductType, b.StatusInstance, cod.StatusInstance, cod.CustomerOrderHeaderInstance, cod.TransID, sel.ProcName, sel.Desc1, sel.IsFixed, b.DateCreated
from customerorderdetail cod
join customerorderheader coh
	on coh.instance = cod.customerorderheaderinstance
join batch b on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
left join QSPCanadaCommon..SystemErrorLog sel
	on sel.OrderID = b.OrderID
where b.orderID = @OrderID

/*update b
set statusinstance = 40010,
	datecreated = GETDATE()
from batch b
JOIN		BatchDistributionCenter bdc
				ON	bdc.BatchDate = b.Date
				AND	bdc.BatchID = b.ID
				AND	bdc.DistributionCenterID = 2 --2: Unigistix
where orderID = @OrderID*/

update cod
set statusinstance = 509
from customerorderdetail cod
join customerorderheader coh
	on coh.instance = cod.customerorderheaderinstance
join batch b on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
JOIN		BatchDistributionCenter bdc
				ON	bdc.BatchDate = b.Date
				AND	bdc.BatchID = b.ID
				AND	bdc.DistributionCenterID = 2 --2: Unigistix
where CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
and TransID = @TransID

delete QSPCanadaCommon..SystemErrorLog
where OrderID = @OrderID

delete ShipmentVariation
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

/*UPDATE	bdc
SET		StatusInstance = 40012
FROM	BatchDistributionCenter bdc
WHERE	bdc.BatchID = @BatchID
AND		bdc.BatchDate = @BatchDate*/

delete shipmentreceipt
where customerorderheaderinstance = @customerorderheaderinstance
and transid = @transid