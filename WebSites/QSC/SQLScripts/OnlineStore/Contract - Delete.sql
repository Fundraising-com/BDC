select *
from core.Contract c
join core.ContractAddress ca on ca.contractid = c.contractid
where c.ContractID = 421537

select *
from core.Contract c
join core.ContractAddress ca on ca.contractid = c.contractid
where c.ContractID = 453808

select *
from core.ContractBrochure
where ContractID = 453808

select *
from core.contractaddress
where name1 = 'BROADACRES JUNIOR SCHOOL'

select *
from core.ToteContractBrochure
where ContractBrochureID in (
1618226,
1618227,
1618228,
1618229,
1618230,
1618231)

select *
from core.CustomerOrder
where ToteIDContract = 694195

select *
from vw_Orders
where toteidcontract = 694195

select *
from store.StorefrontSession s
join store.StorefrontSessionStudent ss on ss.StorefrontSessionID = s.StorefrontSessionID
where customerorderid in (
94002728,
94000620)

select *
from core.Contract c
join store.contractstorefront csf on csf.contractid = c.ContractID
where c.ContractID = 421537

select *
from core.Contract c
join core.ContractAddress ca on ca.contractid = c.contractid
where ca.sapacctno = '9288'
and c.DivisionCode = 40

select *
from Integration.ContractCampaignMapping
where ContractID = 453808

select *
from Store.StorefrontOrderDetail
where CustomerOrderDetailID in (111606804,111588389,111588465)

select *
from portal.Campaign
where contractid = 453808

select *
from store.storefrontsession s
join store.StorefrontSessionStudent ss on ss.StorefrontSessionID = s.StorefrontSessionID
where CampaignID = 138862

select *
from store.storefrontsession
where storefrontsessionid in (103314856,103374445,103374379,103314193,103375726,103315369,103315463,103315155,103355028)

select *
from store.storefrontsession
where CustomerOrderID in (94000620,94002728)

begin tran
delete store.StorefrontSessionStudent
where storefrontsessionid in (103314856,103374445,103374379,103314193,103375726,103315369,103315463,103315155,103355028,103296252)

delete store.storefrontsession
where storefrontsessionid in (103314856,103374445,103374379,103314193,103375726,103315369,103315463,103315155,103355028,103296252)

delete Store.StorefrontOrderDetail
where CustomerOrderDetailID in (111606804,111588389,111588465)

delete portal.Campaign
where CampaignID = 138862

delete core.customerorderdetail
where customerorderdetailid in (111606804,111588389,111588465)

delete core.CustomerSubOrderShip
where customersuborderid in (104867822,104885153,104885154,104867823)

delete core.CustomerSubOrder
where customersuborderid in (104867822,104885153,104885154,104867823)

delete Focus.customerordertracking
where customerorderid in (94000620,94002728)

delete core.CustomerOrder
where CustomerOrderID in (94000620,94002728)

delete core.ContractAddress
where contractid = 453808

delete core.ToteContractBrochure
where ToteID = 694195

delete core.Tote
where ToteID = 694195


delete core.ContractBrochure
where contractid = 453808

delete store.ContractStorefront
where contractid = 453808

delete core.Contract
where contractid = 453808
--commit tran