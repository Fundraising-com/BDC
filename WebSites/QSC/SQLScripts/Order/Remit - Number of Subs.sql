use QSPCanadaOrderManagement
go

--ensure 400+ subs in the upcoming remit
select count(*)
from customerorderdetailremithistory codrh
/*join CustomerOrderDetail cod on cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance and cod.TransID = codrh.TransID
join CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END*/
where codrh.Status = 42000
--and not (cod.ProductCode LIKE 'D%' AND ISNULL(cust.Email, '') = '')