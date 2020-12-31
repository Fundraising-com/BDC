select *
from core.Brochure b
join core.BrochureOffer bo on bo.brochureid = b.brochureid
join core.Item i on i.ItemID = bo.ItemID
join core.Offer o on o.offerid = bo.offerid
left join core.OfferPrice op on op.offerid = o.offerid
where b.materialgroup1 = '999'
and b.BrochureID = 1867
and i.UPCCode = 'F129'