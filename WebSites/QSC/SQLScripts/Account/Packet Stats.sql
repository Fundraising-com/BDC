select distinct fm.FirstName + ' ' + fm.LastName FMLastName, acc.ID AccountID, acc.Name AccountName, c.ID CampaignID, dbo.UDF_GetCampaignPrograms(c.ID) Programs/*p.Name Program*/, c.StartDate CampaignStartDate, c.EndDate CampaignEndDate, c.SuppliesDeliveryDate, c.NumberOfParticipants, c.NumberOfStaff
from Campaign c
join CAccount acc on acc.id = c.billtoaccountid
join CampaignProgram cp on cp.CampaignID = c.ID and cp.DeletedTF = 0
join FieldManager fm on fm.fmid = c.fmid
join Program p on p.id = cp.programid
where (cp.FieldSupplyPacket = 1 OR cp.ProgramID in (1,2))
and c.Status = 37002
and c.startdate between '2017-07-01' and '2017-12-31'
and c.onlineonlyprograms = 0
order by fm.FirstName + ' ' + fm.LastName