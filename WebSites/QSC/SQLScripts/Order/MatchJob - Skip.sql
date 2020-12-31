select top 99 *
from MatchJob
order by ID desc

begin tran
update MatchJob
set Status = 999
where ID between 41232 and 41312
and ID not in (41233, 41234, 41312)

update MatchJob
set Status = 0
where ID between 41232 and 41312
and ID not in (41233, 41234, 41239)

select *
from MatchJob
where AccountCampaignID in (
96576,
95454,
96906,
95716)

select *
from StudentInput
where MatchJobID in (41317)

select *
from TeacherInput
where MatchJobID in (41239)

select *
from StudentMatch m
join Student s1 on s1.Instance = m.MasterStudentInputInstance
join Student s2 on s2.Instance = m.SubordinateStudentInputInstance
where MatchJobID in (41317)

select *
from TeacherMatch m
join Teacher s1 on s1.Instance = m.MasterTeacherInputInstance
join Teacher s2 on s2.Instance = m.SubordinateTeacherInputInstance
where MatchJobID in (41239)