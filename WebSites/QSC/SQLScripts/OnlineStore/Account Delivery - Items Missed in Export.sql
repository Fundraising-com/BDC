select *
from core.ContractAddress ca
join core.contract c on c.contractid = ca.ContractID
join core.Tote t on t.contractid = c.contractid
where ca.SAPAcctNo = '634357'
and ca.IsSoldTo = 1

select *
from GA_Aplus..tblorder o
join ga_aplus..tblorderform f on f.orderid = o.orderid
join ga_aplus..tblOrderFormItem i on i.OrderFormID = f.OrderFormID
where o.orderid = 282373
order by f.isprepaid

select *
from core.customerorder co
join core.customersuborder cso on cso.customerorderid = co.customerorderid
join core.customerorderdetail cod on cod.customersuborderid = cso.customersuborderid
left join GA_Aplus..tblOrderForm f on f.FocusCustomerOrderID = co.CustomerOrderID
left join GA_Aplus..tblOrder o on o.OrderID = f.OrderID
where cod.isshippedtoaccount = 1
and co.ToteIDContract = 123542
and co.CustomerOrderStateID in (11)
and co.CustomerOrderID = 106224768
order by cod.CustomerOrderDetailID

select *
from GA_Aplus..tblOrderFormItem i
join GA_Aplus..tblMaterialNumSeqCd m on m.MaterialNumSeqCdID = i.MaterialNumSeqCdID
where OrderFormID = 14662936

