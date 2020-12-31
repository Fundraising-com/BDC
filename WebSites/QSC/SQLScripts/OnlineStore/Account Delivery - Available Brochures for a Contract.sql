select *
from core.Contract c
join core.ContractAddress ca on ca.IsSoldTo=1 and ca.ContractID = c.ContractID
join core.ContractAddress ca2 on ca2.IsSoldTo = 1 and ca2.SAPAcctNo = ca.SAPAcctNo
join core.Contract c2 on c2.ContractID = ca2.ContractID
join core.ContractBrochure cb on cb.ContractID = c2.ContractID
join core.Brochure b on b.BrochureID = cb.BrochureID
left join core.Tote t2 on t2.ContractID = c2.ContractID
where c2.ContractTypeID not in (3)
and c.ContractTypeID in (3)
and c.contractid = 500734
order by c2.ContractID
