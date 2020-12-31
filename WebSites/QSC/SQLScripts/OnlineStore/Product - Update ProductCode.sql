select *
from core.Brochure b
join core.BrochureOffer bo on bo.brochureid = b.brochureid
join core.Item i on i.ItemID = bo.ItemID
join core.Offer o on o.offerid = bo.offerid
left join core.OfferPrice op on op.offerid = o.offerid
where b.materialgroup1 = '999'
--and b.BrochureID = 1867
--and i.UPCCode = 'F129'
and i.itemdescshort in ('CA-Boating - Digital')
order by o.sapid

begin tran
update core.item
set sapid = 'QSPCABM8', UPCCode = 'BM8'
where itemid = 13598
--commit tran

select top 99 *
from Integration.ETLLog
where ETLJobTypeCode = 1
order by ETLLogID desc
