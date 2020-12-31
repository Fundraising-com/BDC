select *
from customerorder
where customerorderid in (
75186308,
75186309,
75186310,
75186311,
75186312,
75186313,
75186314,
75186315,
75186316,
75186317)

select top 99 *
from Automation.Batch
where BatchTypeID = 6
order by BatchID desc

select * from Automation.batch where BatchTypeID=6 and StatusID=1

select top 99 * from core.salesorder order by salesorderid desc

--Move problem batches into next sales aggreggation
begin tran
update Automation.Batch
set DateTimeCreated = GETDATE()
where BatchTypeID=6 and StatusID=1 and DateTimeLastModified IS NULL
--commit tran

--Permanently remove problem batch from sales aggreggation
begin tran
update Automation.Batch
set StatusID = 2
where BatchTypeID=6 and StatusID=1
--commit tran
