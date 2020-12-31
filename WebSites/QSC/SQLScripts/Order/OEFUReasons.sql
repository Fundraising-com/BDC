select	co.CustomerOrderID, e.CustomerOrderDetailID, co.ToteIDContract ToteID, f.fieldname Issue, c.FirstName, c.LastName, cont.SAPContractNo CampaignID
from core.CustomerOrder co
join core.Customer c on c.CustomerID = co.CustomerID
join core.Tote t on t.ToteID = co.ToteIDContract
join core.Contract cont on cont.ContractID = t.ContractID
join focus.CustomerOrderError e on e.customerorderid = co.CustomerOrderID
join focus.Field f on f.FieldID = e.FieldID
where co.FormCode in ('0737','0745') --Canada Order Forms
and co.CustomerOrderStateID in (38, 39) --Ready for Export, Exported
order by co.CustomerOrderID
