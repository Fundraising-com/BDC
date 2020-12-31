select *
from form
where formcode = '0737'

select *
from text
where entityid = 311335

select *
from text
where entityid between 311330 and 311339


declare @EntityID int;
insert store.Entity values (12, 1, GETDATE(), NULL, NULL)
set @EntityID = @@IDENTITY;
update focus.Form set EntityID = @EntityID where FormCode = '0737';
insert store.Text values (@EntityID, 82, 1, 'Gift Order Form', 0, GETDATE(), NULL, NULL);
insert store.Image values (@EntityID, 178, 1, 850, 1100, 'CanadaGiftOrderForm.pdf', 0, 0, NULL, GETDATE(), NULL, NULL);

select * from entity
where entityid in (311335,311336)

select * from text where entityid in (311335,311336)
select * from image where entityid in (311335,311336)

insert store.Text values (311335, 82, 2, 'Gift Order Form', 0, GETDATE(), NULL, NULL);
insert store.Text values (311336, 82, 2, 'Magazine Order Form', 0, GETDATE(), NULL, NULL);
insert store.Image values (311335, 178, 2, 850, 1100, 'CanadaGiftOrderForm.pdf', 0, 0, NULL, GETDATE(), NULL, NULL);
insert store.Image values (311336, 178, 2, 850, 1100, 'CanadaMagazineOrderForm.pdf', 0, 0, NULL, GETDATE(), NULL, NULL);

select *
from page
where siteid = 114

select *
from pagetype

select * from entity
where entityid = 308874

select * from entitytype