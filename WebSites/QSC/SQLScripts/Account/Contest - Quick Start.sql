select fmid, count(*)
from campaign
where startdate >= '2017-01-01'
and ApprovedStatusDate <= '2016-12-23'
group by fmid
order by count(*)

select ApprovedStatusDate, *
from campaign
where startdate >= '2017-01-01'
and fmid = '1553'
and ApprovedStatusDate > '2016-12-23'
order by id

select *
from CampaignAudit	
where id = 104117
order by auditdate
