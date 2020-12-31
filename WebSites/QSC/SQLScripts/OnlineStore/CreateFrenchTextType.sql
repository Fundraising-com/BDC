/*
select *
from site
*/

select *
from store.Site s
left join Store.Text t on s.EntityID = t.EntityID
left join store.TextType tt on tt.Code = t.TextTypeCode
left join store.Entity e on e.EntityID = t.EntityID
left join store.EntityType et on et.Code = e.EntityTypeCode
--left join store.Storefront sf on sf.SiteID = s.SiteID
--left join store.Text t2 on sf.OverrideSiteEntityID = t2.EntityID
where s.SiteID = 114
order by t.LanguageCode

begin tran
select *
into #tt
from Store.Text t
where t.EntityID = 100001

insert into Store.Text
select 103707, TextTypeCode, 2, 'FR - ' + Content, IsSuppressed, GETDATE(), NULL, NULL
from #tt
order by texttypecode

----

select t.Code
into #m
from store.TextType t
left join #tt on #tt.TextTypeCode = t.Code
where #tt.TextTypeCode is null

begin tran
insert into Store.Text
select 103707, Code, 2, 'FR - Blah', 0, GETDATE(), NULL, NULL
from #m
order by code
