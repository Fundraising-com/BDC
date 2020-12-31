begin tran

insert store.StorefrontPromotion(StorefrontID, PromotionID, Created)
select sf.StorefrontID, p.PromotionID, GETDATE() from store.Promotion p 
join store.StoreFront sf on sf.SiteID in (114, 115, 116)
where p.InternalName like 'CA-%'

--commit

--85, 86, 87
select *
from store.StoreFront
where SiteID in (114,115,116)

select *
from store.Promotion

select *
from store.StorefrontPromotion
order by ID

select sf.StorefrontID, p.PromotionID, GETDATE()
from store.Promotion p 
join store.StoreFront sf on sf.SiteID in (114,115,116)
where p.InternalName like 'CA-%'

