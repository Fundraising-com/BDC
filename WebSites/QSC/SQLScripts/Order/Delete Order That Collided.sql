select *
from Batch b
left join CustomerOrderHeader coh on coh.OrderBatchDate =b.Date and coh.OrderBatchID = b.ID
left join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
left join Incident i on i.customerorderheaderinstance = coh.Instance and i.TransID = cod.TransID
left join IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
left join LandedOrder lo on lo.CustomerOrderHeaderInstance = coh.Instance
left join QSPCanadaFinance..INVOICE inv on inv.INVOICE_ID = cod.InvoiceNumber
--left join QSPCanadaFinance..PAYMENT p on p.ORDER_ID = 9508173
--left join QSPCanadaFinance..ADJUSTMENT adj on adj.ORDER_ID = 9508173
left join CustomerOrderDetailRemitHistory codrh on codrh.customerorderheaderinstance = cod.customerorderheaderinstance and cod.transid = codrh.transid
left join CustomerRemitHistory crh on crh.instance =codrh.customerremithistoryinstance
where b.Date < '2015-07-01'
and coh.CreationDate > '2015-07-01'
order by cod.ProductType

begin tran
delete LandedOrder where LandedOrderID in
(
select lo.LandedOrderID
from Batch b
left join CustomerOrderHeader coh on coh.OrderBatchDate =b.Date and coh.OrderBatchID = b.ID
left join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
left join Incident i on i.customerorderheaderinstance = coh.Instance and i.TransID = cod.TransID
left join IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
left join LandedOrder lo on lo.CustomerOrderHeaderInstance = coh.Instance
left join QSPCanadaFinance..INVOICE inv on inv.INVOICE_ID = cod.InvoiceNumber
where b.Date < '2015-07-01'
and coh.CreationDate > '2015-07-01'
)

update CustomerOrderDetail
set DelFlag = 1
where CustomerOrderHeaderInstance in (
select coh.Instance
from Batch b
left join CustomerOrderHeader coh on coh.OrderBatchDate =b.Date and coh.OrderBatchID = b.ID
left join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
left join Incident i on i.customerorderheaderinstance = coh.Instance and i.TransID = cod.TransID
left join IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
left join LandedOrder lo on lo.CustomerOrderHeaderInstance = coh.Instance
left join QSPCanadaFinance..INVOICE inv on inv.INVOICE_ID = cod.InvoiceNumber
where b.Date < '2015-07-01'
and coh.CreationDate > '2015-07-01'
)

delete crh
from Batch b
left join CustomerOrderHeader coh on coh.OrderBatchDate =b.Date and coh.OrderBatchID = b.ID
left join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
left join Incident i on i.customerorderheaderinstance = coh.Instance and i.TransID = cod.TransID
left join IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
left join LandedOrder lo on lo.CustomerOrderHeaderInstance = coh.Instance
left join QSPCanadaFinance..INVOICE inv on inv.INVOICE_ID = cod.InvoiceNumber
left join QSPCanadaFinance..PAYMENT p on p.ORDER_ID = 9508173
left join QSPCanadaFinance..ADJUSTMENT adj on adj.ORDER_ID = 9508173
left join CustomerOrderDetailRemitHistory codrh on codrh.customerorderheaderinstance = cod.customerorderheaderinstance and cod.transid = codrh.transid
left join CustomerRemitHistory crh on crh.instance =codrh.customerremithistoryinstance
where b.Date < '2015-07-01'
and coh.CreationDate > '2015-07-01'

delete codrh
from Batch b
left join CustomerOrderHeader coh on coh.OrderBatchDate =b.Date and coh.OrderBatchID = b.ID
left join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
left join Incident i on i.customerorderheaderinstance = coh.Instance and i.TransID = cod.TransID
left join IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
left join LandedOrder lo on lo.CustomerOrderHeaderInstance = coh.Instance
left join QSPCanadaFinance..INVOICE inv on inv.INVOICE_ID = cod.InvoiceNumber
left join QSPCanadaFinance..PAYMENT p on p.ORDER_ID = 9508173
left join QSPCanadaFinance..ADJUSTMENT adj on adj.ORDER_ID = 9508173
left join CustomerOrderDetailRemitHistory codrh on codrh.customerorderheaderinstance = cod.customerorderheaderinstance and cod.transid = codrh.transid
where b.Date < '2015-07-01'
and coh.CreationDate > '2015-07-01'

commit tran
