select c.SAPContractNo, c.contractid, CAST(t.ToteID as varchar) + CAST(dbo.CalcMod10Checksum(t.ToteID) as varchar) as ToteID, CAST(co.CustomerOrderID as varchar) + CAST(dbo.CalcMod10Checksum(co.CustomerOrderID) as varchar) as CustomerOrderID, *
from core.CustomerOrder co
join core.tote t on t.toteid = co.ToteIDContract
join core.Contract c on c.contractid = t.contractid
where t.ToteID =  788124 
order by co.customerorderid desc

--Check if there are any other Totes tied to the same Contract that need to be fixed as well
select CAST(t.ToteID as varchar) + CAST(dbo.CalcMod10Checksum(t.ToteID) as varchar) as ToteID, *
from core.tote t
where t.contractid = 505241

--737 Gift
--745 Mag
select *
from focus.form

select *
from Integration.ContractCampaignMapping
where ContractID = 506382

begin tran
delete Integration.ContractCampaignMapping
where ContractID = 425476
and FulfCampaignID = 100911
--commit tran

select * from store.ContractOnlineBlock where ContractID = 384794

begin tran
insert store.ContractOnlineBlock values (384794)
--commit tran

SELECT DISTINCT s.SQLID, co.CustomerOrderID, co.ToteIDContract  --online order
  FROM Core.SalesOrder so with(nolock)
  JOIN Core.SAPSQLID s with(nolock) ON s.SourceID = so.SalesOrderID AND s.SQLIDTypeID = 4 /* SalesOrder */
  JOIN Core.SalesOrderDetail sod with(nolock) ON sod.SalesOrderID = so.SalesOrderID AND so.SAPTransmittedDate IS NULL AND so.ArrivalTypeID in (93, 94, 97, 70, 71, 72, 73)
  JOIN Core.CustomerOrderDetail cod with(nolock) ON cod.SalesOrderDetailID = sod.SalesOrderDetailID
  JOIN Core.CustomerSubOrder  cso with(nolock) ON cso.CustomerSubOrderID = cod.CustomerSubOrderID
  JOIN Core.CustomerOrder co with(nolock) ON co.CustomerOrderID = cso.CustomerOrderID  and co.CustomerOrderStateID = 38 /* ReadyToExportFFS */
  Where co.CustomerOrderID = 101240575