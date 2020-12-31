USE QSPCanadaOrderManagement
GO

select *
from systemoptions
where KeyValue = 'cumulative combo'

select *
from QSPCanadaProduct..PROGRAM_MASTER pm
join qspcanadaproduct..programsection ps on ps.program_id = pm.program_id
where pm.subtype = 30310
and ps.Type = 4
order by ps.id

begin tran
update systemoptions
set Long1Value = 1456, Long2Value = 2019
where KeyValue = 'cumulative combo'
--commit tran
