--9,138,563.56 (6,537,368.33 invoiced)
select sum(cod.price) GrossInvoicedSale2016
from qspcanadaordermanagement..batch b
join qspcanadaordermanagement..customerorderheader coh on coh.orderbatchdate = b.date and coh.orderbatchid = b.id
join qspcanadaordermanagement..customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
where b.Date between '2016-07-01' and '2016-12-26'
and b.orderqualifierid in (39001,39002,39009, 39006, 39015)
and b.StatusInstance not in (40005)
and cod.DelFlag = 0
and cod.StatusInstance not in (500)
and cod.invoicenumber>0

--9,147,661.69 (8,010,491.35 invoiced)
select sum(cod.price) GrossInvoicedSale2015
from qspcanadaordermanagement..batch b
join qspcanadaordermanagement..customerorderheader coh on coh.orderbatchdate = b.date and coh.orderbatchid = b.id
join qspcanadaordermanagement..customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
left join invoice i on i.invoice_id = cod.invoicenumber and i.invoice_date <= '2015-12-27'
where b.Date between '2015-07-01' and '2015-12-26'
and b.orderqualifierid in (39001,39002,39009, 39006, 39015)
and b.StatusInstance not in (40005)
and cod.DelFlag = 0
and cod.StatusInstance not in (500)
and i.invoice_id > 0

select sum(invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) NetInvoicedSales2016
from qspcanadaordermanagement..batch b
join invoice inv on inv.Order_id = b.OrderID
join invoice_section invSec on invSec.Invoice_ID = inv.Invoice_ID
where b.Date between '2016-07-01' and '2016-12-26'
and b.orderqualifierid in (39001,39002,39009, 39006, 39015)

select sum(invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) NetInvoicedSales2015
from qspcanadaordermanagement..batch b
join invoice inv on inv.Order_id = b.OrderID
join invoice_section invSec on invSec.Invoice_ID = inv.Invoice_ID
where b.Date between '2015-07-01' and '2015-12-26'
and b.orderqualifierid in (39001,39002,39009, 39006, 39015)
-----

select distinct l.OrderId
into #uninvoicedcurrent
from invoicegenerationlog l
where datetimecreated between '2016-12-26' and '2016-12-27'
order by l.orderid

select sum(cod.price) GrossInProgressSales2016 --b.orderid, b.date OrderDate, oq.Description OrderSource, sum(cod.price) TotalPrice, (SELECT TOP 1 l.InvoiceGenErrorMessage FROM InvoiceGenerationLog l where l.ORDERID = b.OrderID AND l.datetimecreated between '2016-11-14' and '2016-11-15') ReasonNotInvoiced 
from #uninvoicedcurrent l
join qspcanadaordermanagement..batch b on b.orderid = l.orderid
join qspcanadaordermanagement..customerorderheader coh on coh.orderbatchdate = b.date and coh.orderbatchid = b.id
join qspcanadaordermanagement..customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
join qspcanadacommon..codedetail oq on oq.instance = b.orderqualifierid
--group by b.orderid, b.date, oq.Description 
--order by b.orderid

-----

select distinct l.OrderId
into #uninvoicedprior
from invoicegenerationlog l
where datetimecreated between '2015-12-26' and '2015-12-27'
order by l.orderid

select sum(cod.price) GrossInProgressSales2015 --b.orderid, b.date OrderDate, oq.Description OrderSource, sum(cod.price) TotalPrice, (SELECT TOP 1 l.InvoiceGenErrorMessage FROM InvoiceGenerationLog l where l.ORDERID = b.OrderID AND l.datetimecreated between '2015-11-14' and '2015-11-15') ReasonNotInvoiced
from #uninvoicedprior l
join qspcanadaordermanagement..batch b on b.orderid = l.orderid
join qspcanadaordermanagement..customerorderheader coh on coh.orderbatchdate = b.date and coh.orderbatchid = b.id
join qspcanadaordermanagement..customerorderdetail cod on cod.customerorderheaderinstance = coh.instance
join qspcanadacommon..codedetail oq on oq.instance = b.orderqualifierid
--group by b.orderid, b.date, oq.Description 
--order by b.orderid

select sum(invSec.Net_Before_Tax - ISNULL(invSec.US_Postage_Amount, 0.00)) NetInProgressSales2015
from #uninvoicedprior l
join qspcanadaordermanagement..batch b on b.orderid = l.orderid
join invoice inv on inv.Order_id = b.OrderID
join invoice_section invSec on invSec.Invoice_ID = inv.Invoice_ID
