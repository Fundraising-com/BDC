select top 9 *
from portal.ParticipantContract pc
join portal.participant p on p.participantid = pc.ParticipantID
join portal.registrant r on r.RegistrantID = p.RegistrantID
join portal.RegistrantEmail re on re.registrantid = r.registrantid
where onlineidbase32 = '24SBZFT'

select top 9 *
from portal.ParticipantContract pc
join portal.participant p on p.participantid = pc.ParticipantID
join portal.registrant r on r.RegistrantID = p.RegistrantID
join portal.RegistrantEmail re on re.registrantid = r.registrantid
where re.emailaddress = 'test2@test.ca'

begin tran
update portal.Registrant
set UserPrincipalId = 0 --1058556
where RegistrantID = 478321

update portal.Registrant
set UserPrincipalId = 1058556 --2810902
where RegistrantID = 1922664

update portal.RegistrantEmail
set EmailAddress = 'test2@test.ca' --sdkoehler@hotmail.com
where RegistrantEmailID = 1900972
--commit tran
