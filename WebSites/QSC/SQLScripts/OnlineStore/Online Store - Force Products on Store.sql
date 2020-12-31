select *
from core.brochureoffer bo
join core.brochure b on b.brochureid = bo.brochureid
join core.item i on i.itemid = bo.itemid
join core.offer o on o.offerid = bo.offerid
join core.offerprice op on op.offerid = o.offerid
where b.materialgroup1 = '999'
and b.ProgramTypeID in (134, 133, 110, 106, 105, 148, 149, 150, 152, 153, 155, 157, 171)
and bo.EffectiveEnd > 20160701

begin tran
update i
set EffectiveEnd = 99991231
from core.brochureoffer bo
join core.brochure b on b.brochureid = bo.brochureid
join core.item i on i.itemid = bo.itemid
where b.materialgroup1 = '999'
and b.ProgramTypeID in (134, 133, 110, 106, 105, 148, 149, 150, 152, 153, 155, 157, 171)
and bo.EffectiveEnd > 20160701

update b
set EffectiveEnd = 20170714
from core.brochureoffer bo
join core.brochure b on b.brochureid = bo.brochureid
join core.item i on i.itemid = bo.itemid
where b.materialgroup1 = '999'
and b.ProgramTypeID in (134, 133, 110, 106, 105, 148, 149, 150, 152, 153, 155, 157, 171)
and bo.EffectiveEnd > 20160701

update o
set EffectiveEnd = 20170714
from core.brochureoffer bo
join core.brochure b on b.brochureid = bo.brochureid
join core.item i on i.itemid = bo.itemid
join core.offer o on o.offerid = bo.offerid
where b.materialgroup1 = '999'
and b.ProgramTypeID in (134, 133, 110, 106, 105, 148, 149, 150, 152, 153, 155, 157, 171)
and bo.EffectiveEnd > 20160701

update op
set EffectiveEnd = 20170714
from core.brochureoffer bo
join core.brochure b on b.brochureid = bo.brochureid
join core.item i on i.itemid = bo.itemid
join core.offer o on o.offerid = bo.offerid
join core.offerprice op on op.offerid = o.offerid
where b.materialgroup1 = '999'
and b.ProgramTypeID in (134, 133, 110, 106, 105, 148, 149, 150, 152, 153, 155, 157, 171)
and bo.EffectiveEnd > 20160701

update bo
set EffectiveEnd = 20170714
from core.brochureoffer bo
join core.brochure b on b.brochureid = bo.brochureid
join core.item i on i.itemid = bo.itemid
where b.materialgroup1 = '999'
and b.ProgramTypeID in (134, 133, 110, 106, 105, 148, 149, 150, 152, 153, 155, 157, 171)
and bo.EffectiveEnd > 20160701

--commit tran
