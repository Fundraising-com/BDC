--See if any orders collided and merged together
select *
from Batch b
join CustomerOrderHeader coh on coh.OrderBatchDate =b.Date and coh.OrderBatchID = b.ID
where b.Date < '2015-07-01'
and coh.CreationDate > '2015-07-01'

begin tran

update qspcanadafinance..INVOICE
set ORDER_ID = 100000000 + Order_ID
where Order_ID between 9500002 and 9719325
and INVOICE_DATE < '2009-08-01'

update QSPCanadaFinance..PAYMENT
set order_id = 100000000 + Order_ID
where Order_ID between 9500002 and 9719325
and PAYMENT_EFFECTIVE_DATE < '2009-08-01'

update QSPCanadaFinance..Adjustment
set order_id = 100000000 + Order_ID
where Order_ID between 9500002 and 9719325
and ADJUSTMENT_EFFECTIVE_DATE < '2009-08-01'

update OrderStageTracking
set orderid = 100000000 + OrderID
where OrderID between 9500002 and 9719325
and stagedate < '2009-08-01'

update m
set m.LandedOrderID = 100000000 + m.LandedOrderID
from OnlineOrderMappingTable m
join Batch b on b.OrderID = m.LandedOrderID
where b.OrderID between 9500002 and 9719325
and b.Date < '2009-07-01'

update m
set m.OnlineOrderID = 100000000 + m.OnlineOrderID
from OnlineOrderMappingTable m
join Batch b on b.OrderID = m.OnlineOrderID
where b.OrderID between 9500002 and 9719325
and b.Date < '2009-07-01'

update OrderClosingLog
set OrderID = 100000000 + OrderID
where OrderID between 9500002 and 9719325
and DateTimeCreated < '2009-07-01'

update so
set so.OrderID = 100000000 + so.OrderID
from ShipmentOrder so
join Batch b on b.OrderID = so.OrderID
where b.OrderID between 9500002 and 9719325
and b.Date < '2009-07-01'

update ReportRequestBatch
set BatchOrderId = 100000000 + BatchOrderId
where BatchOrderId between 9500002 and 9719325
and CreateDate < '2009-07-01'

update Batch
set OrderID = 100000000 + OrderID
where OrderID between 9500002 and 9719325
and Date < '2009-07-01'

--commit tran