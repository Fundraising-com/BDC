select dm.FirstName + ' ' + dm.LastName Manager, fm.FirstName + ' ' + fm.LastName Rep, COUNT(*) NumberCampaigns
from Campaign camp
join FieldManager fm on fm.FMID = camp.FMID
join FieldManager dm on dm.FMID = fm.DMID
where StartDate between '2015-01-01' and '2015-06-30'
and camp.Status = 37002
group by dm.FirstName, dm.LastName, fm.FirstName, fm.LastName
order by dm.FirstName, COUNT(*) desc