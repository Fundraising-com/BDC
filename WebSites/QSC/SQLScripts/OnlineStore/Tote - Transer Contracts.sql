select *
from core.contract
where sapcontractno in ('99177','100453')
and ContractTypeID = 2

select dbo.CalcMod10Checksum(t.ToteID), *
from core.tote t
where t.contractid in (431121, 422263)

/*select *
from vw_Orders o
join core.brochure b on b.brochureid = o.brochureid
where toteidcontract = 677746
order by b.brochureid*/

--Get the ToteContractBrochureID's to delete
select tcb.ID ToteContractBrochureID, *
from core.Tote t
left join core.ToteContractBrochure tcb on tcb.ToteID = t.ToteID
left join core.ContractBrochure cb on cb.ContractBrochureID = tcb.contractbrochureid
left join core.Brochure b on b.brochureid = cb.brochureid
where t.ToteID = 669140

--Get the ContractBrochureID's to insert into ToteContractBrochure
select cb.ContractBrochureID, *
from core.Contract c
join core.ContractBrochure cb on cb.ContractID = c.ContractID
join core.Brochure b on b.brochureid = cb.brochureid
where c.ContractID in (421886)
order by c.ContractID, cb.ContractBrochureID

begin tran

update	core.Tote
set		ContractID = 421886,
		SwapContractID = 421886
where	ToteID = 669140

--ToteID, ContractBrochureID
insert core.ToteContractBrochure values (669140, 1477987, GETDATE(), null, null)
insert core.ToteContractBrochure values (669140, 1482042, GETDATE(), null, null)

delete core.totecontractbrochure
where id in (18201574, 18201575, 18201576)

--commit tran


