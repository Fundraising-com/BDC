USE QSPCanadaOrderManagement
GO

Canadian Cycling – 12FV – in the Magazine $34 – product table $24
Moto Journal     – 4772 - in the Magazine $25 – product table $29

select b.OrderQualifierID, *
from CustomerOrderDetail cod
join CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
join Batch b on b.Date = coh.OrderBatchDate and b.ID = coh.OrderBatchID
where cod.ProductCode in ('12FV','4772')
and b.Date >= '2013-07-01'
and b.OrderQualifierID <> 39009
order by b.Date
