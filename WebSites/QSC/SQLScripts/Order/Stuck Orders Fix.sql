SELECT	cod.*, b.*, ioi.*,*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN Customer cust
			ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
LEFT JOIN internetorderid ioi
			ON	ioi.CustomerorderHeaderInstance = coh.Instance
left join customerpaymentheader cph on cph.customerorderheaderinstance = coh.instance
left join creditcardpayment ccp on ccp.customerpaymentheaderinstance = cph.instance
left join landedorder lo on lo.customerorderheaderinstance = coh.instance
WHERE	b.OrderID = 12136630

order by cod.statusinstance



select *
from qspcanadacommon..codedetail

select *
from qspcanadafinance..payment
where order_id = 915813

select *
from orderclosinglog
where orderid = 8494549

update orderclosinglog
set isfixed = 1, DateFixed = getdate()
where orderid = 10060709

--qsp.ca / 40003 / 40001 (first check the CC was charged)
update batch
set statusinstance = 40002--, ordertypecode= 41002
where orderid in (
10230561
)

--Then run close Resolve Orders job
EXEC	[dbo].[CloseInternetOrders]
EXEC	[dbo].[pr_Batch_SelectStuck]
EXEC	[dbo].[pr_CustomerOrderDetail_SelectStuck]

--cust svc
EXEC pr_CloseOrder 10023366

--Zero Quantity
update customerorderdetail
set quantity = 1
where customerorderheaderinstance = 11709020
and transid = 2

--Zero Program Section
EXEC	[dbo].[pr_CustomerOrderDetail_UpdateProduct]
		@ProductPriceInstance = 376768,
		@CustomerOrderHeaderInstance = 11709020,
		@TransID = 2,
		@CloseOrder = 1

--Zero Pricing Detail - Cust svc should do a Product Update

select *
from batchdistributioncenter
where batchid = 5038
and batchdate = '2011-10-28 00:00:00.000'

--CA StaffOrder flag
select isstafforder, *
from qspcanadacommon..campaign
where id = 80375

select isstafforder
from batch
where orderid = 10060709

update batch
set isstafforder = 1
where orderid = 10060709

--40004 - landed - just after updating to 40004
begin tran
declare @OrderID int
set @OrderID = 11312404
update batch
set statusinstance = 40002
where orderid = @OrderID
exec DoCloseOrder @OrderID,1
declare @RetVal int
exec spPostCloseVerification @orderid, @RetVal OUTPUT
print @RetVal
exec pr_Insert_BatchDistributionCenter @OrderID
exec pr_ProcessReserveQuantities_ByOrderId_V2 @OrderID

DECLARE @ShipmentGroupID INT

SET @ShipmentGroupID = null --1: Gift/Prizes, 2: Cookie Dough, 3: Field Supplies, 4: Popcorn

DECLARE	ShipmentGroup CURSOR FOR
SELECT	DISTINCT pl.ShipmentGroupID
FROM	BatchDistributionCenter bdc
JOIN	Batch b ON b.ID = bdc.BatchID AND b.Date = bdc.BatchDate
JOIN	QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
WHERE	b.OrderID = @OrderID
AND		(pl.ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL)
			
OPEN ShipmentGroup
FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID
WHILE @@FETCH_STATUS = 0
begin
	exec pr_cleanprintqueue @OrderID, @ShipmentGroupID
	exec pr_Ins_Report_Parameters_V2 @OrderId, -1, @ShipmentGroupID

	FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID

end
CLOSE ShipmentGroup
DEALLOCATE ShipmentGroup
--commit tran


--40004 - landed - Stuck at Prize Generation or Later
/*
select *
from OnlineOrderMappingTable
where landedorderid = 
*/

begin tran

declare @OrderID int, @id int, @date datetime, @supplemental int

set @OrderID = 12136630

select @id = id, @date = date, @supplemental = case orderqualifierid when 39001 then 0 else 1 end
from Batch
where OrderID = @OrderID

Select *
from QspCanadaCommon.dbo.SystemErrorLog   
where Orderid = @OrderID

update QspCanadaCommon.dbo.SystemErrorLog
set IsReviewed = 1, IsFixed = 1
where Orderid = @OrderID

update	cod
set		delflag = 1
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.OrderID = @OrderID
AND		cod.ProductType IN (46013, 46014)

delete bdc
from BatchDistributionCenter bdc
join batch b on b.id = bdc.batchid and b.date = bdc.BatchDate
where b.OrderID = @OrderID

declare @SuccessTF int
exec GenerateIncentiveOrders_NonEnvelope_V2 @id,@Date, @supplemental,@SuccessTF OUTPUT, '1'
print @SuccessTF

update QSPCanadaOrderManagement..CustomerOrderDetail set StatusInstance=510
	from QSPCanadaOrderManagement..CustomerOrderDetail ,
		QSPCanadaOrderManagement..CustomerOrderHeader,
		QSPCanadaOrderManagement..Batch
	   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
			QSPCanadaOrderManagement..CustomerOrderHeader.Instance
		and OrderBatchDate=Date
		and OrderBatchID=id
		and OrderID = @orderid
		and (producttype not in ( 46001, 46017, 46012, 46021, 46023, 46024) OR (ProductType = 46024 AND dbo.UDF_Entertainment_IsShippedToAccount(@OrderId) = 1))
		and CustomerOrderDetail.StatusInstance  not in ( 513, 501, 508)
		and (CustomerOrderDetail.StatusInstance  not in (500) OR ProductType IN (46004, 46013, 46014))
		and CustomerOrderDetail.DelFlag = 0

update QSPCanadaOrderManagement..CustomerOrderDetail set StatusInstance=508
	from QSPCanadaOrderManagement..CustomerOrderDetail ,
		QSPCanadaOrderManagement..CustomerOrderHeader,
		QSPCanadaOrderManagement..Batch
	   where  QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=
			QSPCanadaOrderManagement..CustomerOrderHeader.Instance
		and OrderBatchDate=Date
		and OrderBatchID=id
		and OrderID = @orderid
		and producttype in (46023)
		and CustomerOrderDetail.StatusInstance  not in (500, 501, 508, 513)

update	QSPCanadaOrderManagement..CustomerOrderDetail 
set		StatusInstance=508,
		QuantityShipped = Quantity
from	QSPCanadaOrderManagement..CustomerOrderDetail ,
		QSPCanadaOrderManagement..CustomerOrderHeader,
		QSPCanadaOrderManagement..Batch
where	QSPCanadaOrderManagement..CustomerOrderDetail.CustomerOrderHeaderInstance=QSPCanadaOrderManagement..CustomerOrderHeader.Instance
		and OrderBatchDate=Date
		and OrderBatchID=id
		and OrderID = @orderid
		and producttype in (46024) AND dbo.UDF_Entertainment_IsShippedToAccount(@OrderId) = 0
		and CustomerOrderDetail.StatusInstance  not in (500, 501, 508, 513)


declare @RetVal int
exec spPostCloseVerification @orderid, @RetVal OUTPUT
print @RetVal

exec pr_Insert_BatchDistributionCenter @OrderID
exec pr_ProcessReserveQuantities_ByOrderId_V2 @OrderID

DECLARE @ShipmentGroupID INT

SET @ShipmentGroupID = null --1: Gift/Prizes, 2: Cookie Dough, 3: Field Supplies, 4: Popcorn

DECLARE	ShipmentGroup CURSOR FOR
SELECT	DISTINCT pl.ShipmentGroupID
FROM	BatchDistributionCenter bdc
JOIN	Batch b ON b.ID = bdc.BatchID AND b.Date = bdc.BatchDate
JOIN	QSPCanadaCommon..QSPProductLine pl ON pl.ID = bdc.QSPProductLine
WHERE	b.OrderID = @OrderID
AND		(pl.ShipmentGroupID = @ShipmentGroupID OR @ShipmentGroupID IS NULL)
			
OPEN ShipmentGroup
FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID
WHILE @@FETCH_STATUS = 0
begin
	exec pr_cleanprintqueue @OrderID, @ShipmentGroupID
	exec pr_Ins_Report_Parameters_V2 @OrderId, -1, @ShipmentGroupID

	FETCH NEXT FROM ShipmentGroup INTO @ShipmentGroupID

end
CLOSE ShipmentGroup
DEALLOCATE ShipmentGroup

/*update batch
set statusinstance = 40013
where orderid in (8771270,8771275,8771271,8771272)*/

--commit tran

--Cancel Line item
begin tran
update customerorderdetail
set delflag = 1
where customerorderheaderinstance = 11984005
and transid = 1

--remit item
declare @remitstatus int
exec spRemitIndividualItem 11865537, 3, @remitstatus

--CC pending
	select * from QSPCanadaOrderManagement..CustomerOrderHeader,
				    QSPCanadaOrderManagement..CustomerPaymentHeader,
				    QSPCanadaOrderManagement..CreditCardPayment,
					QSPCanadaOrderManagement..Batch
				where orderbatchdate=date
					and orderbatchid=id
					and customerorderheaderinstance=QSPCanadaOrderManagement..CustomerOrderHeader.instance
					and Customerpaymentheaderinstance = QSPCanadaOrderManagement..CustomerPaymentHeader.Instance
					and QSPCanadaOrderManagement..CreditCardPayment.StatusInstance in (19003, 19004)
					AND ORDERID=915811

update CreditCardPayment
set StatusInstance = 19001
where CustomerPaymentHeaderInstance = 5670970

insert CreditCardBatch values ('ZZZ', 18506, '1995-01-01 00:00:00.000', '1995-01-01 00:00:00.000', 40004, 6, 0, 18506, GETDATE(), '612', GETDATE(), '612', 58001, 0)
exec [spDoCreditCardGL] 18506

update qspcanadafinance..INVOICE
set INVOICE_AMOUNT = 1223.53
where INVOICE_ID = 706688

--40006 -- CC pending, look at QPay script to see which transaction still pending

--40013
select *
from qspcanadafinance..invoicegenerationlog
where orderid = 916483

select *
from QSPCanadaFinance.dbo.UDF_GetBillableOrdersFromBatch()
where orderid = 11705819

select *
from batchaudit
where orderid = 11705819

--Incorrect cheque amount
select checkpayabletoqspamount, *
from batch
where orderid = 916483

select *
from qspcanadafinance..payment
where order_id = 916483
and payment_method_id not in (50003, 50004)

--Stuck Subs due to missing CPH+CCP records
SELECT	MAX(Instance) + 1
FROM	CustomerPaymentHeader
begin tran
insert CreditCardPayment values (106119331, '53XXXXXXXXXX0000', '0000', 20100, null, null, GETDATE(), null, 19001, GETDATE(), '612', GETDATE(), null, 0, 'x')
insert CustomerPaymentHeader values (106119331, 12941341, 0, '2014-11-02 00:00:00.000', 1001, 1, 0, 123.00, GETDATE(), '612', GETDATE(), '612', 601, 1, null)

--Missing Recipient Name
select * from CustomerOrderDetail where CustomerOrderHeaderInstance = 12983079
begin tran
update CustomerOrderDetail
set Recipient = 'UNKNOWN'
where CustomerOrderHeaderInstance = 12983079
and TransID = 2

--Zero Tax - See Tax - Update CustomerOrderDetail.sql

--40001 Order may be elsewhere
select *
from batch
where orderid between 12490810 and 12490820

begin tran
update batch
set StatusInstance = 40005
where OrderID = 12490815
--commit tran
