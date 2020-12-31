select *
from vw_orders
where customerorderid = 98032502

begin tran
update core.customerorder
set customerorderstateid = 0
where CustomerOrderID = 98032502

update core.CustomerOrderDetail
set customerorderdetailstateid = 0
where CustomerOrderDetailid = 119524986
--commit tran
