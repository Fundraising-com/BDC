USE QSPCanadaOrderManagement
GO

CREATE TABLE #temp (ShipmentReceiptID int)

select *
from shipmentreceipt
where batchorderid = 912669 

UPDATE	QSPCanadaOrderManagement..shipmentreceipt
SET		StatusID = 3
WHERE	ShipmentReceiptID IN (
789189,
789190,
789191)

INSERT	#temp
SELECT	ShipmentReceiptID
FROM	QSPCanadaOrderManagement..shipmentreceipt
WHERE	ShipmentReceiptBatchID = 984 AND StatusID <> 3

begin tran
DECLARE @ShipmentReceiptID INT
DECLARE c cursor for 
      SELECT ShipmentReceiptID
      FROM  #temp
      
Open c
fetch NEXT from c into @ShipmentReceiptID

while @@fetch_status = 0
BEGIN
	exec QSPCanadaOrderManagement..ShipmentReceipt_UpdateShipmentInfo @ShipmentReceiptID
    fetch NEXT from c into @ShipmentReceiptID
END

DROP TABLE #temp
CLOSE c
DEALLOCATE c

/*
exec shipmentreceipt_selecterror
exec shipmentreceipt_selectmissing

select *
from shipmentreceipt
where shipmentreceiptbatchid in (984)


select *
from customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join batch b on orderbatchid = id and orderbatchdate = date
where b.orderid = 912669
*/