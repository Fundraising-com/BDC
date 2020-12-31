--Orders that were not invoiced in 2017 as not all items shipped
select fm.firstname + ' ' + fm.lastname FM, cdOQ.[Description] AS [Source], b.orderid, b.Date OrderDate, inv.invoice_id, inv.invoice_date, SUM(s.NET_BEFORE_TAX - ISNULL(s.US_Postage_Amount,0.00)) NetSale
from QSPCanadaOrderManagement..Batch b 
join QSPCanadaCommon..CodeDetail cdOQ on cdOQ.Instance = b.OrderQualifierID
join qspcanadafinance..invoice inv on b.OrderID = inv.ORDER_ID
join QSPCanadaFinance..INVOICE_SECTION s on s.INVOICE_ID = inv.INVOICE_ID 
join qspcanadacommon..Campaign c on c.id = b.campaignid
join qspcanadacommon..fieldmanager fm on fm.fmid = c.fmid
where (inv.INVOICE_DATE >= '2018-01-01')
and b.Date between '2017-01-01' and '2018-01-01'
and		s.section_type_id IN (1,2,9,10,11,13,14,15)
group by fm.firstname + ' ' + fm.lastname, cdOQ.[Description], b.orderid, b.Date, inv.invoice_id, inv.invoice_date
--AND			fm.FMID NOT IN ('0508')
--AND			(ISNULL(@IncludeExcludedCampaigns, 0) = 1 OR camp.ExcludeFromSalesBase = 0)
--AND			(ISNULL(@IncludeBDCReferredAccounts, 0) = 1 OR ISNULL(acc.ParentID, 0) NOT IN (34838))
order by fm.firstname + ' ' + fm.lastname, b.orderID

SELECT b.orderid, b.orderqualifierid,	cod.*, *
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	cod.producttype not in (46014)
and cod.statusinstance not in (500)
and not (cod.producttype in (46017) and cod.statusinstance = 501)

--Checks that D+H deposited in 2012 but we booked GL in 2013
select p.PAYMENT_AMOUNT,*
from QSPCanadaFinance..PAYMENT p
join QSPCanadaOrderManagement..Batch b on b.OrderID = p.ORDER_ID
where p.PAYMENT_EFFECTIVE_DATE between '2013-01-01' and '2013-01-05'
--and b.DateSent between '2012-07-01' and '2013-01-01'
and p.PAYMENT_METHOD_ID in (50002)
order by b.OrderQualifierID

--Adjustments
select a.adjustment_amount,*
from QSPCanadaFinance..ADJUSTMENT a
--join QSPCanadaOrderManagement..Batch b on b.OrderID = a.ORDER_ID
where a.ADJUSTMENT_EFFECTIVE_DATE >= '2013-01-01'
and b.DateCreated < '2013-01-01'
order by b.OrderQualifierID
