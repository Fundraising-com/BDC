select *
from CustomerOrderDetail cod
join CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
left join CustomerPaymentHeader cph on cph.customerorderheaderinstance = coh.instance
left join CreditCardPayment ccp on ccp.CustomerPaymentHeaderInstance = cph.Instance
JOIN		Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END

where ccp.StatusInstance = 19000
and cod.ProductCode like 'D%' and isnull(cust.Email,'') = ''
--and (cust.StatusInstance = 301 or isnull(cod.PricingDetailsID, 0) = 0)
and cod.DelFlag <> 1
and cod.CreationDate >= '2014-07-01'
and b.OrderQualifierID IN (39001, 39002, 39009)
order by cod.CustomerOrderHeaderInstance desc