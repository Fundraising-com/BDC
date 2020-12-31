select *
from core.Tote t
where t.toteid = 596758

select *
from core.Tote t
join core.Contract c on c.ContractID = t.ContractID
where c.ContractID = 385233

select *
from core.Tote t
join core.Contract c on c.ContractID = t.ContractID
join core.CustomerOrder co on co.ToteIDContract = t.ToteID
where c.ContractID = 385233

select *
from core.SuperToteTote
where SuperToteID in (6965)

select *
from core.SuperTote
where SuperToteID in (6965)

select *
from Focus.totetracking tt
join focus.WorkflowStep wfs on wfs.WorkflowStepID = tt.WorkflowStepID
where toteid in (596758,596761)


begin tran
delete core.SuperToteTote
where  ToteID = 596758

update core.Tote
set LastWorkflowStepTypeID = 29
where ToteID = 596758

delete focus.ToteTracking
where ToteTrackingID = 3571191

update core.SuperTote
set NumCouponsSent = 25
where SuperToteID = 6965