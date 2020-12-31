select *
from systemoptions
where KeyValue in ('cumulative combo','cumulative mag')

select *
from QSPCanadaProduct..ProgramSection ps
join QSPCanadaProduct..ProgramSectionType t on t.ID = ps.type
join QSPCanadaProduct..PROGRAM_MASTER pm on pm.program_id = ps.program_id
where pm.subtype = 30310
and ps.type = 4
order by ps.id desc

begin tran
update systemoptions
set long1value = 1343, Long2Value = 2018
where KeyValue = 'cumulative combo'

update systemoptions
set long1value = 1179, Long2Value = 2018
where KeyValue = 'cumulative mag'

--commit tran