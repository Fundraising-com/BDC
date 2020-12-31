SELECT		cust.LastName,
			cust.FirstName,
			cust.Address1,
			cust.Address2,
			cust.City,
			cust.Zip PostalCode,
			cust.State Province,
			cod.ProductCode,
			cod.productName,
			cod.CreationDate OrderDate,
			rb.Date RemitDate
FROM		CustomerOrderDetail cod
join		customerorderheader coh
				on coh.instance = cod.customerorderheaderinstance
join		CustomerOrderDetailRemitHistory codrh
				on codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				and codrh.TransID = cod.TransID
join		RemitBatch rb
				ON rb.ID = codrh.RemitBatchID
JOIN		Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
WHERE		rb.Date between '2014-08-01' and '2015-08-01'
AND			codrh.Status = 42001
AND			cod.ProductCode in (
'D090',
'D086',
'D098',
'D087',
'D093',
'D094',
'D097',
'D092',
'D096',
'D089',
'D091',
'D095',
'D088')
AND			cod.DelFlag = 0
ORDER BY rb.ID,	cust.LastName, cust.FirstName