select sum(cod.quantity) NumberUnits
from customerorderheader coh
join customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
where cod.creationdate between '2016-01-01' and '2016-12-31'
--and coh.formcode = '0745'
and cod.producttype in (46001)

Cookie Dough - 307190
Gift - 56684
Magazine - 116135
TRT - 101
Entertainment - 13

select  sum(cod.quantity) NumberUnitsShipped
from customerorderheader coh
join customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
join batch b on b.id = coh.orderbatchid and coh.orderbatchdate =b.date
where cod.creationdate between '2016-01-01' and '2016-12-31'
--and coh.formcode = '0745'
and b.orderqualifierid not in (39009)
and cod.producttype in (46024)
and cod.statusinstance in (507,508)

Cookie Dough - 309681
Gift - 59565
Magazine - 117041
TRT - 103
Entertainment - 15
