select *
from core.CustomerOrder co
join core.customersuborder cso on cso.customerorderid = co.customerorderid
join core.customerorderdetail cod on cod.customersuborderid = cso.customersuborderid
join core.student s on s.studentid = cod.studentid
join core.customeraddress ca on ca.customeraddressid = cso.customeraddressid
where co.customerorderid in (106098471, 106098468)