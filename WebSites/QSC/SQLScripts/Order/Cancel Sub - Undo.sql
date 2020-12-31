USE QSPCanadaOrderManagement
GO

declare @CustomerOrderHeaderInstance INT,
		@TransID INT

set @CustomerOrderHeaderInstance = 12800460
set @TransID = null

select *
from CustomerOrderDetailRemitHistory
where CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
and TransID = ISNULL(@TransID, TransID)
and Status = 42002

/*
select *
from remitbatch
where id = 260009

begin tran
update customerorderdetailremithistory
set status = 42000
where customerorderheaderinstance = 12800460
and transid = 1
commit tran
*/

begin tran

delete crh
from CustomerRemitHistory crh
join CustomerOrderDetailRemitHistory codrh on codrh.CustomerRemitHistoryInstance = crh.Instance
where codrh.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
and codrh.TransID = ISNULL(@TransID, TransID)
and codrh.Status = 42002

delete codrh
from CustomerOrderDetailRemitHistory codrh
where codrh.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
and codrh.TransID = ISNULL(@TransID, TransID)
and codrh.Status = 42002

commit tran
