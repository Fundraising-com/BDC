select	i.ItemTypeDesc ItemType,
		SUM(CASE WHEN co.FormCode IN ('000A', '000B', '0009') THEN CASE cod.OfferValue WHEN 0.00 THEN op.offerprice ELSE cod.OfferValue END * Quantity ELSE 0.00 END) OnlineSales,
		SUM(CASE WHEN co.FormCode NOT IN ('000A', '000B', '0009') THEN CASE cod.OfferValue WHEN 0.00 THEN op.offerprice ELSE cod.OfferValue END * Quantity ELSE 0.00 END) LandedSales
from core.customerorder co with (nolock)
join core.customersuborder cso with (nolock) on cso.customerorderid = co.customerorderid
join core.customerorderdetail cod with (nolock) on cod.customersuborderid= cso.customersuborderid
join core.itemtype i on i.itemtypeid = CASE WHEN cod.ItemTypeID = 2 and cod.OfferCode like 'C%' THEN 12 ELSE cod.itemtypeid END
left join core.offerprice op on op.OfferPriceID = cod.OfferPriceID
where co.CustomerOrderStateID = 39
and co.Created between '20150701' and '20151117'
and co.FormCode IN ('000A', '000B', '0009', '000I', '000J', '0737', '0745')
and cod.IsVoucherRedemption = 0
group by i.ItemTypeDesc
order by i.ItemTypeDesc

select *
from form
where formcode in (
'0009',
'0737',
'0174',
'000B',
'000I',
'000J',
'0745',
'000A')

2015 4838997.50
2016 4784733.00
select 	SUM(CASE cod.OfferValue WHEN 0.00 THEN op.offerprice ELSE cod.OfferValue END * Quantity) OnlineSales--, COUNT(DISTINCT c.ContractID) NumberGroups
from core.customerorder co with (nolock)
join core.customersuborder cso with (nolock) on cso.customerorderid = co.customerorderid
join core.customerorderdetail cod with (nolock) on cod.customersuborderid= cso.customersuborderid
left join core.offerprice op on op.OfferPriceID = cod.OfferPriceID
where co.CustomerOrderStateID = 39
and co.Created between '20160701' and '20161117'
and co.FormCode IN ('000A', '000B', '0009')
2015 2012
2016 1922
select 	COUNT(DISTINCT t.ContractID) NumberGroups
from core.customerorder co with (nolock)
join core.tote t with (nolock) on t.toteid = co.toteidcontract
where co.CustomerOrderStateID = 39
and co.Created between '20160701' and '20161117'
and co.FormCode IN ('000A', '000B', '0009')

2015 $2,405
2016 $2,490