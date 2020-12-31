select e.EmailID, et.Description EmailType, e.Subject, e.EmailTo, e.ReplyTo, e.Subject, e.DateTimeSent, es.Description EmailStatus
from Messaging.Email e
join Messaging.EmailTemplate et on et.EmailTemplateID = e.EmailTemplateID
join messaging.EmailState es on es.Code = e.EmailStateCode
where EmailTo = 'beeeej@hotmail.com'
order by e.EmailID