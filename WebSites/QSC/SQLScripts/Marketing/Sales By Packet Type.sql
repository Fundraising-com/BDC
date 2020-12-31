select SUM(cod.Price) GrossSale
from CustomerOrderHeader coh
join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
join qspcanadacommon..Campaign c on c.id = b.CampaignID
where c.StartDate between '2015-07-01' and '2015-12-31'
and b.OrderQualifierID in (39001,39002)
and c.ID in (select CampaignID from qspcanadacommon..CampaignProgram where BlackboardPacket = 1)

select SUM(c.NumberOfParticipants) NumberOfStudents
from qspcanadacommon..Campaign c
where c.StartDate between '2015-07-01' and '2015-12-31'
and c.ID in (select CampaignID from qspcanadacommon..CampaignProgram where BlackboardPacket = 1)

select SUM(cod.Price) GrossSale
from CustomerOrderHeader coh
join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
join qspcanadacommon..Campaign c on c.id = b.CampaignID
where c.StartDate between '2015-07-01' and '2015-12-31'
and b.OrderQualifierID in (39001,39002)
and c.ID not in (select CampaignID from qspcanadacommon..CampaignProgram where ISNULL(BlackboardPacket,0) = 1)

select SUM(c.NumberOfParticipants) NumberOfStudents
from qspcanadacommon..Campaign c
where c.StartDate between '2015-07-01' and '2015-12-31'
and c.ID not in (select CampaignID from qspcanadacommon..CampaignProgram where ISNULL(BlackboardPacket,0) = 1)


select COUNT(coh.Instance) NumberOrderForms
from CustomerOrderHeader coh
join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
join qspcanadacommon..Campaign c on c.id = b.CampaignID
where c.StartDate between '2015-07-01' and '2015-12-31'
and b.OrderQualifierID in (39001,39002)
and c.ID in (select CampaignID from qspcanadacommon..CampaignProgram where BlackboardPacket = 1)

select COUNT(coh.Instance) NumberOrderForms
from CustomerOrderHeader coh
join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = coh.Instance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
join qspcanadacommon..Campaign c on c.id = b.CampaignID
where c.StartDate between '2015-07-01' and '2015-12-31'
and b.OrderQualifierID in (39001,39002)
and c.ID not in (select CampaignID from qspcanadacommon..CampaignProgram where ISNULL(BlackboardPacket,0) = 1)
