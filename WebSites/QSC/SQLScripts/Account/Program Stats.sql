select fm.FirstName + ' ' + fm.LastName FMLastName, acc.ID AccountID, acc.Name AccountName, c.ID CampaignID, p.Name Program, c.StartDate CampaignStartDate, c.EndDate CampaignEndDate,
		isnull((select top 1 'Yes' from qspcanadaordermanagement..Batch b where orderqualifierid in (39001,39002) and b.CampaignID = c.ID), 'No') LandedOrderInFFS
from Campaign c
join CAccount acc on acc.id = c.billtoaccountid
join CampaignProgram cp on cp.CampaignID = c.ID and cp.DeletedTF = 0
join FieldManager fm on fm.fmid = c.fmid
join Program p on p.id = cp.programid
where c.Status = 37002
and cp.programID in (53,54)
and cp.deletedtf = 0
--and c.ID in (select b.CampaignID from qspcanadaordermanagement..Batch b where orderqualifierid in (39001,39002))
order by cp.ProgramID, isnull((select top 1 'Yes' from qspcanadaordermanagement..Batch b where orderqualifierid in (39001,39002) and b.CampaignID = c.ID), 'No')