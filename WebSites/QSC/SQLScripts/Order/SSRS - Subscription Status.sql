 select c.path, s.LastRunTime, s.LastStatus, *
 from Subscriptions s
  INNER JOIN dbo.Catalog C
 ON s.report_oid = C.itemid
 JOIN ActiveSubscriptions actSub ON actSub.SubscriptionID = s.SubscriptionID
 order by s.LastRunTime desc

select *
from ActiveSubscriptions
where SubscriptionID = 'CE6D1321-A66D-4AEC-B903-73175BB670FD'

select count(*)
from Notifications
where SubscriptionID = 'CE6D1321-A66D-4AEC-B903-73175BB670FD'

begin tran
delete ActiveSubscriptions
where SubscriptionID = '6B4F25A1-E072-4490-80C4-6EA8DE16E4C9'

delete Notifications
where SubscriptionID = '6B4F25A1-E072-4490-80C4-6EA8DE16E4C9'

--commit tran
