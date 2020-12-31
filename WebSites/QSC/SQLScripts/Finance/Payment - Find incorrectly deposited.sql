--GAO cheque payments incorrectly deposited into Time
select *
from qspcanadafinance..payment pmt
join qspcanadaordermanagement..batch b on b.orderid = pmt.order_id
join qspcanadacommon..campaign c on c.id = b.campaignid
join qspcanadafinance..invoice inv on inv.order_id = b.orderid
join qspcanadafinance..gl_entry e on e.invoice_id = inv.invoice_id
where c.startdate between '2011-07-01' and '2011-12-31'
and e.businessunitid in (3,4)
and pmt.payment_method_id not in (50003, 50004)
--and ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 0

--Time cheque payments incorectly deposited into GAO
select *
from qspcanadafinance..payment pmt
join qspcanadaordermanagement..batch b on b.orderid = pmt.order_id
join qspcanadacommon..campaign c on c.id = b.campaignid
join qspcanadafinance..invoice inv on inv.order_id = b.orderid
join qspcanadafinance..gl_entry e on e.invoice_id = inv.invoice_id
where c.startdate >= '2012-01-01'
and e.businessunitid in (1,2)
and pmt.payment_method_id not in (50003, 50004)
--and ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 1

--GAO CC payments incorectly deposited into Time
select *
from qspcanadafinance..payment pmt
join qspcanadafinance..invoice inv on inv.order_id = pmt.order_id
join qspcanadafinance..gl_entry e on e.invoice_id = inv.invoice_id
where e.businessunitid in (3,4)
and pmt.payment_method_id in (50003, 50004)
--and ISNULL(dbo.UDF_BusinessUnit_IsTimeOrder(b.OrderID), 0) = 1