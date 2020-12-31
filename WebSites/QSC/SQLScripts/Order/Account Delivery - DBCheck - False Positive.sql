select FocusCustomerOrderID, FocusToteID, *
from GA_Aplus..tblorderform f
join GA_Aplus..tblOrderFormItem i on i.orderformid = f.orderformid
join GA_Aplus..tblMaterialNumSeqCd m on m.materialnumseqcdid = i.materialnumseqcdid
where FocusCustomerOrderID in (
100839791	,
100839791	,
100845400	,
100845400	,
100887792	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100902225	,
100902225	,
100902225	,
100902225	,
100902225	,
100908906	,
100909134	,
100957394	,
100957394	
)

begin tran
insert ga.focus.customerorderexport
select distinct FocusCustomerOrderID, cod.CustomerOrderDetailID, FocusToteID, NULL
from GA_Aplus..tblorderform f
join GA_Aplus..tblOrderFormItem i on i.orderformid = f.orderformid
join GA_Aplus..tblMaterialNumSeqCd m on m.materialnumseqcdid = i.materialnumseqcdid
join ga.core.customersuborder cso on cso.customerorderid = FocusCustomerOrderID
join ga.core.customerorderdetail cod on cod.CustomerSubOrderID = cso.CustomerSubOrderID
where cod.IsShippedToAccount = 1
and FocusCustomerOrderID in (
100839791	,
100839791	,
100845400	,
100845400	,
100887792	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100898061	,
100902225	,
100902225	,
100902225	,
100902225	,
100902225	,
100908906	,
100909134	,
100957394	,
100957394	
)
--commit tran

--select top 9 * from ga.focus.customerorderexport
--CustomerOrderID, CustomerOrderDetailID, (Landed)ToteID, NULL
begin tran
insert GA.Focus.CustomerOrderExport values (100835504, 123195094, 757605, NULL)
--commit tran

select *
from core.Contract c
join core.ContractAddress ca on ca.IsSoldTo=1 and ca.ContractID = c.ContractID
join core.ContractAddress ca2 on ca2.IsSoldTo = 1 and ca2.SAPAcctNo = ca.SAPAcctNo
join core.Contract c2 on c2.ContractID = ca2.ContractID
join core.ContractBrochure cb on cb.ContractID = c2.ContractID
join core.Brochure b on b.BrochureID = cb.BrochureID
left join core.Tote t on t.ContractID = c.ContractID
left join core.Tote t2 on t2.ContractID = c2.ContractID
where t.ToteID in (
757459)
and c2.ContractTypeID not in (3)
order by c2.ContractID
