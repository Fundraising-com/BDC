select *
from Core.contract c
join Core.contractAddress ca on ca.ContractID = c.ContractID
where ca.IsSalesPerson = 1
and c.ContractTypeID in (2, 3)
and ca.Name1 like '%Karen El%' --Sandy McCarty, Doug Nysetvold
order by c.ContractID

begin tran

insert Messaging.EmailContractOptOut
select c.ContractID, 2, GETDATE(), NULL, NULL
from Core.contract c
join Core.contractAddress ca on ca.ContractID = c.ContractID
where ca.IsSalesPerson = 1
and c.ContractTypeID in (2, 3)
and ca.Name1 like '%Daryl Beamish%' --Sandy McCarty, Doug Nysetvold
order by c.ContractID

--commit tran

SELECT * FROM Messaging.EmailContractOptOut

------

select *
from messaging.emailsalespersonoptout

select *
from core.salesperson
where firstname = 'karen'

begin tran
insert messaging.emailsalespersonoptout values ( , 2, GETDATE(), NULL, NULL)
--commit tran