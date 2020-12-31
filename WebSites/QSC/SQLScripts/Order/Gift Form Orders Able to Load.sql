select co.*
from core.customerorder co
join core.Tote t on t.ToteID = co.ToteIDContract
join core.Contract c on c.ContractID = t.ContractID
where CustomerOrderStateID = 38
and FormCode = '0737'
and c.ContractID not in (

select distinct c.ContractID
from core.customerorder co
join core.Tote t on t.ToteID = co.ToteIDContract
join core.Contract c on c.ContractID = t.ContractID
where CustomerOrderStateID = 38
and FormCode <> '0737'

)
