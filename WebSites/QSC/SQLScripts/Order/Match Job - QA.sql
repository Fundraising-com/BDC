select *
from matchjob
where accountcampaignid = 99011

select *
from TeacherInput
where accountcampaignid = 99011
and name like '%morin%' --and name not like '%az%'
order by instance

select *
from TeacherMatch m
left join Teacher tm on tm.Instance = m.MasterTeacherInputInstance
left join Teacher ts on ts.instance = m.subordinateteacherinputinstance
where matchjobid = 45957
--and ((tm.Name like '%par%' and tm.Name not like '%az%') or (ts.Name like '%par%' and ts.Name not like '%az%'))
--and (m.masterteacherinputinstance = 405304 or m.subordinateteacherinputinstance = 405304)
order by m.masterteacherinputinstance


select *
from StudentInput
where matchjobid = 45959
--and firstname like '%owen%'
order by instance

select *
from StudentMatch m
left join Student tm on tm.Instance = m.MasterStudentInputInstance
left join Student ts on ts.instance = m.subordinateStudentinputinstance
where matchjobid = 45959
--and ((tm.Name like '%par%' and tm.Name not like '%az%') or (ts.Name like '%par%' and ts.Name not like '%az%'))
--and (m.masterteacherinputinstance = 405304 or m.subordinateteacherinputinstance = 405304)
--and (m.masterstudentinputinstance in (3007973,3009817) or m.subordinatestudentinputinstance in (3007973,3009817))
and tm.teacherinstance <> ts.teacherinstance
order by m.masterstudentinputinstance
