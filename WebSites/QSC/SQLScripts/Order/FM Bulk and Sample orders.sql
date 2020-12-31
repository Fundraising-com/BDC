select *
from QSPCanadaCommon..CAccount
where name like '%bailey%'

--FM Sample/Bulk orders
select  b.OrderQualifierID, cod.ProductType, a.CAccountCodeClass, a.CAccountCodeGroup, cod.InvoiceNumber, *
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
left join QSPCanadaFinance..INVOICE i on i.INVOICE_ID = cod.InvoiceNumber
join QSPCanadaCommon..CAccount a on a.id = coh.accountid
where a.CAccountCodeClass = 'FM'
--and cod.producttype in (46002, 46020) --Gift
and cod.producttype in (46008, 46013, 46014) --Prize
--and cod.producttype in (46018) --Cookie Dough
--and cod.producttype in (46019) --Chocolate
and cod.InvoiceNumber > 0
and i.INVOICE_DATE between '2012-01-01' and '2013-01-01'
and a.Name like '%bailey%'
order by i.invoice_id

--FM Prize Overages
select *
from QSPCanadaFinance..ADJUSTMENT
where ADJUSTMENT_TYPE_ID = 49032
