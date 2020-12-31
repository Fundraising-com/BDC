select CAST(c.ContractID as varchar) + CAST(dbo.CalcMod10Checksum(c.ContractID) as varchar) as OnlineID, *
from core.Contract c
join core.ContractAddress ca on ca.IsSoldTo=1 and ca.ContractID = c.ContractID
join core.ContractAddress ca2 on ca2.IsSoldTo = 1 and ca2.SAPAcctNo = ca.SAPAcctNo
join core.Contract c2 on c2.ContractID = ca2.ContractID
join core.ContractBrochure cb on cb.ContractID = c2.ContractID
join core.Brochure b on b.BrochureID = cb.BrochureID
left join core.Tote t2 on t2.ContractID = c2.ContractID
join store.ContractStorefront csf on csf.ContractID = c.ContractID
where c2.ContractTypeID not in (3)
and c.ContractTypeID in (3)
and t2.ToteID is null
and c.IsShippingToAccountAllowed = 1
and c.DivisionCode = 20
and b.ProgramTypeID = 37 -- Cookie Dough
--and b.BrochureCode LIKE 'CDO%'
order by c2.ContractID
