SELECT coh.toteid,ioi.*, lo.*, cod.*, cs.*, c.*,lo.*, b.orderid, cod.*, ioi.*,lo.*,	coh.paymentmethodinstance, cph.*, ccp.*, b.OrderID, lo.landedorderid, ioi.*, cod.invoicenumber, cs.State, cod.Tax, cod.Tax2, *
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
left join PaymentBatch pb on pb.PaymentID = b.paymentbatchid and pb.PaymentDate = b.PaymentBatchDate
left join Customer c on c.instance = coh.customerbilltoinstance
left join Customer cs on cs.Instance = cod.CustomerShipToInstance
left join customerpaymentheader cph on cph.customerorderheaderinstance = coh.instance
left join CreditCardPayment ccp on ccp.customerpaymentheaderinstance = cph.instance
left join CreditCardBatch ccb on ccb.ID = ccp.BatchID
left join InternetOrderID ioi on ioi.CustomerOrderHeaderInstance = coh.Instance
left join LandedOrder lo on lo.customerorderheaderinstance = coh.instance
left join Student s on s.Instance = coh.StudentInstance
left join Teacher t on t.Instance = s.TeacherInstance
left join CustomerOrderDetailRemitHistory codrh on codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and codrh.TransID = cod.TransID
left join QSPCanadaProduct..PRICING_DETAILS pd on pd.MagPrice_Instance = cod.PricingDetailsID
left join QSPCanadaProduct..Product p on p.Product_Instance = pd.Product_Instance
left join QSPCanadaFinance..INVOICE i on cod.InvoiceNumber = i.INVOICE_ID
join qspcanadacommon..campaign camp on camp.id = b.campaignid
join qspcanadacommon..CAccount acc on acc.id = camp.billtoaccountid
--left join QSPCanadaFinance..InvoiceGenerationLog l on l.CustomerOrderHeaderInstance = coh.instance and l.TransId = cod.transid
WHERE	--b.orderid = 7571551 --TRT Landed
--b.orderid = 7571578 --Landed Mag
--b.orderid = 7571552 -- Candle/CD
--b.orderid in (7571918, 7571921, 7571922) --TRT Redemptions 8=G1 1=G2 2=GN
--b.orderid = 7571660 --Venu
--b.orderid = 8180477 --Prod Mag
b.orderid = 8250246--Prod Candles
--b.orderid = 8180485--Prod Mag 2
order by lo.landedorderid

select *
from OrderClosingLog
where OrderId in (8180485)
and logid >= 97995
order by logid desc

select top 999 *
from QSPCanadaFinance..InvoiceGenerationLog
where orderid = 7571578
order by invoicegenlogid desc

select top 99 * from landedorder l
order by landedorderid desc

select * from internetorderid
where internetorderid = 75184723

EXEC [CloseInternetOrders]
EXEC [pr_CloseResolveOrders]
EXEC [spProcessAllCCPayments]
EXEC qspcanadafinance..[GenerateInvoices]

select * from qspcanadafinance.dbo.udf_getbillableordersfrombatch()

select * from qspcanadafinance..invoice
where order_id = 7571219

begin tran
update Batch set IsInvoiced = 1 where OrderID = 7568537



begin tran
update batch
set isinvoiced = 1
where orderid in (7567047)



--TODO
select *
from OrderClosingLog
where OrderId in (7568536,7568537)

Id	CustomerOrderHeaderInstance	TransId	ProductCode	ProductName	Description
1	12755772	1	S128	Better Homes & Gardens	Zero Tax or  Zero Gross/Net or wrong Tax Calculation for the item (GST/HST) 

Id	CustomerOrderHeaderInstance	TransId	ProductCode	ProductName	Description
1	12757065	2	0100	NULL	Zero Pricing Detail

--why S128 and landed order closed?
select *
from CustomerOrderDetail
where CustomerOrderHeaderInstance = 12755772

select *
from customer
where FirstName = '' or LastName = ''
order by Instance desc

update customer
set FirstName = 'ZZ', LastName = 'ZZ'
where Instance in (
5382042,
5382041,
5382040,
5382039,
5382038,
5381985,
5381984,
5381983,
5381982,
5381981,
5381973
)
--End TODO

select top 999 *
from QSPCanadaFinance..InvoiceGenerationLog
order by invoicegenlogid desc

update Batch
set StatusInstance = 40004
where OrderID in (5994022, 5995069)

update Batch
set OrderQualifierID = 39009
where OrderID in (6185601,6185602)

update CustomerOrderDetail
set ProductType = 46001
where CustomerOrderHeaderInstance in (12755549,
12755549,
12755551,
12755553,
12755555,
12755554,
12755557,
12755558,
12755558,
12755559,
12755560)

update customerpaymentheader
set statusinstance = 600
where CustomerOrderHeaderInstance in (12755564,
12755564,
12755563,
12755565,
12755565)

update CustomerOrderHeader
set AccountID = 2656,
	CampaignID = 91771
where instance in (12304203,12304204,12304205)

update Batch
set AccountID = 2656,
	ShipToAccountID = 2656,
	campaignid = 91771
where OrderID in (5994022, 5995069)

update Teacher
set AccountID = 2656
where instance in (327766,327768)

select top 99 *
from core.customerorder co 
join core.CustomerSubOrder cso on co.customerorderid = cso.customerorderid
join core.CustomerOrderDetail cod on cso.customersuborderid = cod.customersuborderid
left join core.salesorderdetail sod on cod.salesorderdetailid = sod.salesorderdetailid
--join core.salesorder so on sod.salesorderid = so.salesorderid
--join core.sapsqlid s on so.salesorderid = s.sourceid and s.sqlidtypeid = 4
where co.customerorderid in (75184623,75184621)
order by co.customerorderid desc


select top 99 *
from core.CustomerOrderDetail cod
order by cod.customerorderdetailid desc

select top 99 *
order by customerorderid desc
from core.CustomerOrder

select top 99 *
from qspcanadafinance..payment
order by payment_id desc

commit tran
update customerorderdetail
set productName = 'UPDATED', pricingdetailsid = 413193, tax = 1.00, taxA = 1.00, Gross = Price, Net = Price - 1.00, recipient = 'UNKNOWN'
where productName = 'UPDATED'
and customerorderheaderinstance in (
12765707,
12765715,
12765699,
12765728,
12765706,
12765711,
12765710,
12765715,
12765712,
12765702,
12765708,
12765726,
12765706,
12765708,
12765702,
12765693,
12765707,
12765726,
12765728,
12765711,
12765710,
12765699,
12765706,
12765699,
12765711,
12765715,
12765693,
12765702,
12765712,
12765698,
12765715,
12765711)