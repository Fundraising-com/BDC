--Find ParticipantID

select *
from portal.RegistrantEmail
where emailaddress = 'dmdemers@hotmail.com'

select *
from portal.Registrant
where RegistrantID = 774879

select *
from portal.participant
where RegistrantID = 774879

select *
from portal.ParticipantEmailMessage m
join messaging.Email e on e.EmailID = m.EmailID
where m.ParticipantID = 950563

begin tran
update e
set EmailStateCode = 6, Modified = GETDATE()
from portal.ParticipantEmailMessage m
join messaging.Email e on e.EmailID = m.EmailID
where participantid in (950563)
and EmailStateCode = 0
--commit tran
