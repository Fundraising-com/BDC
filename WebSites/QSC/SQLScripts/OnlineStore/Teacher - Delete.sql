select *
from core.Contract c
join portal.Campaign camp on camp.contractid = c.contractid
left join portal.leader l on l.campaignid = camp.campaignid
left join portal.participantcampaign pc on pc.leaderid = l.leaderid
left join portal.Participant p on p.ParticipantID = pc.ParticipantID
where c.contractid = 440686

begin tran
update portal.leader
set IsSelectable = 0
where leaderid in (
751866,
751867,
751868,
751869,
751870,
751871,
751872)

update portal.ParticipantCampaign
set leaderid = null
where ID = 2211352
--commit tran
