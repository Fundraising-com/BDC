USE QSPCanadaOrderManagement
GO

/*select * from shipmentrequestorder
where orderid = 912669

select top 199 * from shipmentwaybill
where waybillnumber = 'UNAA174105'

select top 100 *
from shipmentreceipt
where batchorderid = 912669
order by shipmentreceiptid desc

select top 10 *
from shipmentreceiptbatch
order by shipmentreceiptbatchid desc

select *
from customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join batch b on orderbatchid = id and orderbatchdate = date
where b.orderid = 10071954               
*/

insert shipmentreceiptbatch values (getdate(), NULL)

select top 1 * from shipmentreceiptbatch order by shipmentreceiptbatchid desc

--If there is a receipt in error
begin tran
insert shipmentreceipt
select	1000, --ShipmentReceiptBatchID
		1,
		BatchOrderID,
		CustomerOrderHeaderInstance,
		TransID,
		ProductCode,
		(SELECT cod.Quantity FROM CustomerOrderDetail cod WHERE cod.CustomerOrderHeaderInstance = sr.CustomerOrderHeaderInstance AND cod.TransID = sr.TransID),
		Courier,
		NumBoxes,
		[Weight],
		'UNAA174105',
		'2012-06-21',
		'2012-06-21',
		getdate()
from shipmentreceipt sr
where batchorderid = 912669


--If no receipt to start with
select b.OrderID, cod.CustomerOrderHeaderInstance, cod.TransID, cod.ProductCode, cod.Quantity
into #orders
from customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join batch b on orderbatchid = id and orderbatchdate = date
where b.orderid = 10080847          
and cod.distributioncenterid > 0
and cod.statusinstance <> 508

begin tran

insert shipmentreceipt
select	1038, --ShipmentReceiptBatchID
		1,
		OrderID,
		CustomerOrderHeaderInstance,
		TransID,
		ProductCode,
		Quantity,
		'',
		1,
		1.00,
		OrderID,
		'2012-09-20',
		'2012-09-20',
		getdate()
from #orders

drop table #orders

select *
from shipmentreceipt
where batchorderid = 10080847         
order by shipmentreceiptid desc

exec ShipmentReceipt_UpdateShipmentInfo 802337

commit tran

--for multiple run ShipmentReceipt_Reprocess.sql

select *
from batch b
join customerorderheader coh on orderbatchid = id and orderbatchdate = date
join customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
join shipment s on s.id = cod.shipmentid
where orderid = 10080847 