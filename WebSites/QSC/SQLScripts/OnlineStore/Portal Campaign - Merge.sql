select *
from portal.Campaign c
where c.ContractID = 501745

select *
from portal.ParticipantCampaign pc
join portal.Campaign c on c.CampaignID = pc.CampaignID
join portal.Participant p on p.ParticipantID = pc.ParticipantID
where c.ContractID = 501745
and c.campaignid in (217633,237087)
order by pc.ParticipantID

begin tran

update l
set campaignid = 217633
from portal.ParticipantCampaign pc
join portal.Campaign c on c.CampaignID = pc.CampaignID
join portal.Leader l on l.LeaderID = pc.LeaderID
where c.CampaignID = 237087

--Delete ParticipantCampaign record where Participant is already tied to both Campaigns

delete portal.ParticipantCampaignContestRule
where participantcampaignid IN (3414106,3414101,3414094,3414111,3414090,3413942,3414100,3414115,3414114,3414110,3414096,3414095,3414107,3414091,3414093,3414105,3414092,3414089,3414097,3414087,3414109,3414099,3414116)

delete portal.ParticipantCampaign
where ID IN (3414106,3414101,3414094,3414111,3414090,3413942,3414100,3414115,3414114,3414110,3414096,3414095,3414107,3414091,3414093,3414105,3414092,3414089,3414097,3414087,3414109,3414099,3414116)

--

update portal.ParticipantCampaign 
set CampaignID = 217633
where CampaignID = 237087

update store.StorefrontSession
set CampaignID = 217633
where CampaignID = 237087

delete portal.Campaign
where CampaignID = 237087

update portal.Campaign
set EndDate = '2017-09-25'
where CampaignID = 217633

--commit tran