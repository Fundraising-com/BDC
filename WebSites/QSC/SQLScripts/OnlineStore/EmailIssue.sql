set transaction isolation level snapshot;

select senderAddr.Uri FromAddress, mar.Uri ToAddress, mail.Subject, mail.Body, mail.Created EmailCreatedDate, mail.Sent EmailSentDate
from Dispatch.Mail mail
inner join Dispatch.MailAddress senderAddr on (mail.SenderAddressId = senderAddr.Id) 
INNER JOIN Dispatch.MailRecipient mailr ON mailr.MailId = mail.Id and typecode = 1
INNER JOIN Dispatch.MailAddress mar ON mar.Id = mailr.AddressId
where mail.Created < '2015-09-25'
and mail.Sent between '2015-09-27' and '2015-09-28'
order by senderAddr.Uri, mail.Id