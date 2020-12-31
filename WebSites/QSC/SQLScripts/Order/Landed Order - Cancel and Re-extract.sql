SELECT	b.OrderQualifierID, b.orderid, i.Invoice_ID, cod.*
into	#orders
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN internetorderid ioi
			ON	ioi.CustomerorderHeaderInstance = coh.Instance
left join customerpaymentheader cph on cph.customerorderheaderinstance = coh.instance
left join creditcardpayment ccp on ccp.customerpaymentheaderinstance = cph.instance
left join QSPCanadaFinance..INVOICE i on i.order_id = b.orderid
where b.OrderID in (
12891028)


select distinct invoice_id from #orders

select * from #orders
order by CustomerOrderHeaderInstance, TransID

begin tran
update b
set StatusInstance = 40005
from Batch b
join #orders o on o.OrderID = b.OrderID

update cod
set delflag = 1
from CustomerOrderDetail cod
join #orders o on o.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and o.TransID = cod.TransID

update batch
set orderid = 2129
where orderid = 12891028

delete internetorderid
where internetorderid = 104464550

commit tran