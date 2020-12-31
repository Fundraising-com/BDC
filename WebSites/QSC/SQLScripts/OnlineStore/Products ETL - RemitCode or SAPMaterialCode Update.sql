select *
from core.Brochure b
join core.BrochureOffer bo on bo.brochureid = b.brochureid
join core.Item i on i.ItemID = bo.ItemID
join core.Offer o on o.offerid = bo.offerid
join core.OfferPrice op on op.offerid = o.offerid
join core.ItemOffer iof on iof.itemid = i.itemid
where b.materialgroup1 = '999'
and b.EffectiveEnd > 20171003
and bo.EffectiveEnd > 20180926
and i.UPCCode = '1730'
order by o.OfferCode, o.offerid, bo.BrochureOfferID

--If Item has not been created
begin tran
update i
set SAPID = 'QSPCA2411D', UPCCode = '2411D'
from core.Brochure b
join core.BrochureOffer bo on bo.brochureid = b.brochureid
join core.Item i on i.ItemID = bo.ItemID
join core.Offer o on o.offerid = bo.offerid
join core.OfferPrice op on op.offerid = o.offerid
join core.ItemOffer iof on iof.itemid = i.itemid
where b.materialgroup1 = '999'
and iof.sapid = '524530'
--commit tran

--If Item has already been created but no longer used
begin tran
update core.Item
set SAPID = 'QSPCA9540_OLD', UPCCode = '9540_OLD'
where ItemID = 12589

update core.Offer
set OfferCode = '9540O'
where SAPID IN ('523382')

update core.ItemOffer
set SAPID = '523382_OLD'
where SAPID = '523382'

update core.Item
set SAPID = 'QSPCA9540', UPCCode = '9540'
where ItemID = 13021

--commit tran

--If splitting an item into two items
begin tran
insert store.Entity values (2, 1, getdate(), null, null)
select top 9 * from store.Entity order by entityid desc
insert core.item values (1, 'QSPCAX1730','X1730','CA-Sports Illustrated (No Swimsuit Issue)',1,20180701,99991230,39,0,4968839, getdate(), null, null)
select top 9 * from core.item order by itemid desc
update core.BrochureOffer set itemid = 23564 where BrochureOfferID in (403481,408380,403478,408381)
update core.itemoffer set itemid = 23564 where ItemOfferID in (26680,26681,26682,26683,28350,28351,28352,28848,31616,31617,31618,32571,34333,34334,34335,34336,34337,34338,37227,37228,37229,37230,37231,37232,40015,40016,40017,40018,40019,40020)
--commit tran
