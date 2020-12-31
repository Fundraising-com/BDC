select t.groupname, *
from focus.ToteTracking tt
join focus.WorkflowStep wfs on wfs.workflowstepid = tt.WorkflowStepID
join core.Tote t on t.toteid = tt.toteid
where wfs.WorkflowStepTypeID IN (19,8,5,26,25,6,30)
and tt.ToteID in (
723927
)
order by tt.ToteID

begin tran

update tt
set tt.WorkflowStepStateID = 4
from focus.ToteTracking tt
join focus.WorkflowStep wfs on wfs.workflowstepid = tt.WorkflowStepID
where wfs.WorkflowStepTypeID IN (19,8,5,26,25,6,30)
and tt.ToteID in (
723927
)

commit tran
