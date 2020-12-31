select *
from QSPCanadaCommon..FieldManager
where FirstName = 'michelle'

select acc.Id, *
from QSPCanadaCommon..Campaign c
join QSPCanadaCommon..CAccount acc on acc.Id = c.BillToAccountID
where FMID = '0075'
and acc.CAccountCodeClass = 'FM'
and acc.Name like 'QSP%'

select co.CustomerOrderID, cus.FirstName+' ' +cus.lastname 'CustomerName', cusa.entityname 'Recipient',
cusa.Address1 'StreetAddress', cusa.City, cusa.stateprovinceabbr as 'State', cusa.PostalCode,
co.created 'Order Date' , 
coalesce(bod.itemdescshort,i.itemdescshort) as 'Title', cod.Quantity, op.OfferPrice, (cod.Quantity * op.OfferPrice) as 'Total Price'
from core.ContractAddress ca 
join core.Contract c on c.ContractID = ca.contractid
join core.Tote t on t.ContractID = c.contractid
join core.CustomerOrder co on co.ToteIDContract = t.ToteID
join core.Customer cus on cus.CustomerID = co.CustomerID 
join core.CustomerSubOrder cso on cso.CustomerOrderID = co.CustomerOrderID
join core.CustomerAddress cusa on cusa.CustomerAddressID = cso.CustomerAddressID 
join core.CustomerOrderDetail cod on cod.CustomerSubOrderID = cso.CustomerSubOrderID
left join core.Student s on s.StudentID = cod.StudentID 
join core.OfferPrice op on op.OfferPriceID = cod.OfferPriceID
join core.BrochureOffer bo on bo.OfferID = op.OfferID and bo.BrochureID = cod.BrochureID
left join core.Item i on i.ItemID = bo.ItemID
left join core.BrochureOfferDesc bod on bod.BrochureOfferDescID = bo.BrochureOfferDescID
where ca.SAPAcctNo = 34043
and ca.IsSoldTo = 1
--and c.ContractTypeID = 3 
and op.OfferPrice >= 0 
and co.Created >= '2014-07-01'
and co.Created <= CAST(dateadd(day,1,'2014-10-24') as DATE)
and cod.CustomerOrderDetailStateID not in (0,7) 
and co.CustomerOrderStateID not in (22,23,0)
order by s.LastName, s.FirstName, co.Created