select top 9 *
from portal.ParticipantContract
where onlineidbase32 = '23GWZE2'

select *
from Portal.Participant
where participantid = 1143313

select *
from portal.ParticipantCampaign
where ParticipantID = 1143313

select top 9 *
from portal.Registrant
where RegistrantID = 936585

select *
from Portal.RegistrantEmail
where registrantid = 936585

select *
from portal.Participant
where ParticipantID = 1143313

select top 9 *
from core.Student
where OnlineIDBase32 = '23GWZE2'

select *
from store.StorefrontSession
where participantid = 1143313

begin tran
delete portal.ParticipantContract
where ID = 1143194

delete portal.ParticipantCampaign
where ParticipantID = 1143313

delete from store.StorefrontSession
where StorefrontSessionID = 105515271

delete portal.Participant
where ParticipantID = 1143313
--commit tran
