select fm.FirstName + ' ' + fm.LastName FMLastName, acc.ID AccountID, acc.Name AccountName, c.ID CampaignID, p.Name Program, c.StartDate CampaignStartDate, c.EndDate CampaignEndDate
from Campaign c
join CAccount acc on acc.id = c.billtoaccountid
join CampaignProgram cp on cp.CampaignID = c.ID and cp.DeletedTF = 0
join FieldManager fm on fm.fmid = c.fmid
join Program p on p.id = cp.programid
where cp.BlackboardPacket = 1
and c.Status = 37002
order by fm.FirstName + ' ' + fm.LastName