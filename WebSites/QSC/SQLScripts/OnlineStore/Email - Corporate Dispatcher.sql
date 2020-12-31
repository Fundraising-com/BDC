--sqlp08
use SWCorporate_AppServices; 

set transaction isolation level snapshot; 

select mail.Id, mail.SenderDisplayName, mail.ReplyToDisplayName, mail.Subject, mail.Body, mail.sent EmailSentDate, replyToAddr.Uri FromEmail, mar.Uri ToEmail, comStat.Description EmailStatus
from Dispatch.Mail mail 
inner join Dispatch.MailAddress senderAddr 
on (mail.SenderAddressId = senderAddr.Id) 
inner join Dispatch.MailAddress replyToAddr 
on (mail.ReplyToAddressId = replyToAddr.Id) 
INNER JOIN Dispatch.MailRecipient mailr
ON mailr.MailId = mail.Id
INNER JOIN Dispatch.MailAddress mar
ON mar.Id = mailr.AddressId
INNER JOIN Dispatch.CommunicationStatus comStat
ON comStat.Code = mailr.CommunicationStatusCode
--where mar.Uri in ('kdasilva.89@gmail.com','kiradaniel1983@gmail.com')
--where mail.replytoaddressid = 8744946
--and mail.created >= '2015-07-01'
--where replyToAddr.Uri = 'oesap@yahoo.ca'
--and mail.Sent >= '2016-07-01'
where mail.Id IN (67073832)
order by mail.Id
