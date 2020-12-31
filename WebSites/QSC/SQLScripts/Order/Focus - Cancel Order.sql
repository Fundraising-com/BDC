select *
from vw_Orders
where customerorderid = 89196975

select * from customerorderstate
select * from customersuborderstate
select * from customerorderdetailstate

begin tran
update core.CustomerOrder
set CustomerOrderStateID = 23
where CustomerOrderID = 89196975

update core.CustomerOrderdetail
set CustomerOrderdetailStateID = 7
where CustomerOrderDetailID = 105019856

update core.CustomerSubOrder
set CustomerSubOrderStateID = 3
where CustomerSubOrderID = 98497458
--commit tran
