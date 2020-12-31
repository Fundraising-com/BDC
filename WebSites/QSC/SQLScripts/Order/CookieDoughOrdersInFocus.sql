select	case co.CustomerOrderStateID when 38 then 'OrderFullyKeyed' else 'OrderStillBeingKeyed' end OrderStatus, cod.CustomerOrderDetailID, cod.Quantity, cod.OfferCode, 
		cont.SAPContractNo CampaignID, ca.SAPAcctNo AccountID, ca.Name1 AccountName, ca.Address, ca.City, ca.StateProvinceAbbr, ca.PostalCode, 
		s.StudentID, s.FirstName StudentFirstName, s.LastName StudentLastName, s.ClassroomID, cl.LeaderName Teacher, cso.SendToName Customer, t.ToteID, co.CustomerOrderID
from core.CustomerOrder co
join core.customersuborder cso on cso.CustomerOrderID = co.CustomerOrderID
join core.CustomerOrderDetail cod on cod.CustomerSubOrderID = cso.CustomerSubOrderID
join core.Tote t on t.ToteID = co.ToteIDContract
join core.Contract cont on cont.ContractID = t.ContractID
left join core.ContractAddress ca on ca.contractID = cont.ContractID and ca.IsOrganization = 1
left join core.Student s on s.studentid = co.studentid
left join core.Classroom cl on cl.ClassroomID = s.ClassroomID
where co.formcode in ('0737')--, '0745')
--and co.CustomerOrderStateID = 38
order by ToteIDContract, s.ClassroomID, s.LastName