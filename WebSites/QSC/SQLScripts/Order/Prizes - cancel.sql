select *
from batch b
join customerorderheader coh on orderbatchid = id and orderbatchdate = date
join customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
where b.orderid = 912675
and cod.producttype = 46013

begin tran

update cod
set delflag = 1
from batch b
join customerorderheader coh on orderbatchid = id and orderbatchdate = date
join customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
where b.orderid = 912675
and cod.producttype = 46013

update batch
set statusinstance = 40013
where orderid = 912675