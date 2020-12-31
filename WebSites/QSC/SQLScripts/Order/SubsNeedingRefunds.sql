use QSPCanadaOrderManagement
go

select *
from CustomerOrderDetail cod
left join CustomerPaymentHeader cph on cph.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
where cod.ProductCode in ('12FV', '4772')
and cod.CreationDate >= '2013-07-01'
and cod.Price > 25.00
and cod.StatusInstance not in (500)
and cod.CreationDate between '2013-12-01' and '2014-01-01'
order by cod.CustomerOrderHeaderInstance
