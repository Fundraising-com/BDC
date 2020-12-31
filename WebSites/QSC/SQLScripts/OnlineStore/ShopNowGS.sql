select top 1000 o.CustomerOrderID, t.ContractID,roc.sapcontractno,cas.sapacctno,cas.name1, t2.ToteID,(CAST(o.CustomerOrderID as varchar) + cast(dbo.CalcMod10Checksum(o.customerorderid) as varchar) ),
dbo.CalcMod10Checksum( cast((CAST(o.CustomerOrderID as varchar) + cast(dbo.CalcMod10Checksum(o.customerorderid) as varchar) ) as Int))
from core.Tote t 
join core.customerorder o on o.toteidcontract = t.toteid
join core.contractaddress ca on ca.contractid = t.contractid and ca.isorganization = 1
join core.contractaddress cas on cas.contractid = t.contractid and cas.issoldto = 1
join core.Contract roc on roc.ContractID = ca.ContractID
left join core.Tote t2 on t2.ContractID = t.ContractID and t2.WorkflowID = 14
where t.WorkflowID = 2 and roc.DivisionCode = 21 
and t.LastWorkflowStepTypeID = 6
