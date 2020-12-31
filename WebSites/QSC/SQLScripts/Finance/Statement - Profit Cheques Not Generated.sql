select CampaignID, AccountID, balance, FMFirstName, FMLastName, AccountName, AccountContactFirstName, AccountContactLastName, AccountPhoneNumber, s.CreationDate
from statementprintrequesterror e
join statement s on s.statementid = e.statementid
where e.CreationDate >= '2015-04-01'
