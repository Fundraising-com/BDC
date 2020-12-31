select *
from core.Item i
join core.BrochureOffer bo on bo.ItemID = i.ItemID
join core.Offer o on o.OfferID = bo.OfferID
where i.ItemID = 19272

begin tran
update i
set UPCCode = 'D150', SAPID = 'QSPCADs150'
from core.Item i
join core.BrochureOffer bo on bo.ItemID = i.ItemID
join core.Offer o on o.OfferID = bo.OfferID
where i.ItemID = 19272

update o
set OfferCode = 'DG150'
from core.Item i
join core.BrochureOffer bo on bo.ItemID = i.ItemID
join core.Offer o on o.OfferID = bo.OfferID
where i.ItemID = 19272
--commit tran

select top 99 *
from Integration.ETLLog
where etljobtypecode = 1
order by ETLLogID desc