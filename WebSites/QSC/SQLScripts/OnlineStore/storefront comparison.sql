select *
from core.contract c
join store.contractstorefront csf on csf.contractid = c.contractid
where csf.storefrontid = 85
and c.ProgramStartDate between 20160701 and 20161231

1519 Mag Storefront Contracts vs 379 Gift/CD Storefront Contracts

select sf.Name Storefront, i.ItemTypeDesc ItemType, SUM(cod.Quantity) SalesQuantity, SUM(OfferValue * Quantity) SalesPrice, COUNT(DISTINCT co.CustomerOrderID) NumOrders
from core.customerorder co with (nolock)
join core.customersuborder cso with (nolock) on cso.customerorderid = co.customerorderid
join core.customerorderdetail cod with (nolock) on cod.customersuborderid= cso.customersuborderid
join core.itemtype i on i.itemtypeid = cod.itemtypeid
join store.StorefrontSession sfs on sfs.customerorderid = co.customerorderid
join store.storefront sf on sf.storefrontid = sfs.storefrontid
where sf.storefrontid in (85, 226)
and co.Created >= '20160827'
and co.CustomerOrderStateID = 39
group by sf.Name, i.ItemTypeDesc
order by sf.Name, i.ItemTypeDesc
