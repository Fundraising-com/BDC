USE [QSPCanadaOrderManagement]
GO

select *
from customerorderdetailremithistory
where customerorderheaderinstance = 11562145
and transid = 2

--If chad already sent, requeue it
EXEC [dbo].[pr_Remit_ReRemitSubsByCOD]
		@CustomerOrderHeaderInstance = 11562145,
		@TransID = 2

--change pending chad into a new sub
begin tran
update h
set status = 42000
from customerorderdetailremithistory h
where customerorderheaderinstance = 11562145
and transid = 2
and status = 42006
commit tran