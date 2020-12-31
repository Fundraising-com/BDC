SELECT	inv.INVOICE_ID, inv.INVOICE_DATE, b.OrderID, cod.CustomerOrderHeaderInstance, cod.TransID,
		cdProductType.Description ProductType, cod.ProductCode, cod.ProductName, cod.Price,
		cust.FirstName + ' ' + cust.LastName CustomerName, cust.Address1 CustomerShippingAddress1, cust.Address2 CustomerShippingAddress2,
		cust.State CustomerShippingProvince, cust.Zip CustomerShippingPostalCode		 
FROM	QSPCanadaFinance..Invoice inv
JOIN	QSPCanadaOrderManagement..CustomerOrderDetail cod ON cod.InvoiceNumber = inv.INVOICE_ID
JOIN	QSPCanadaOrderManagement..CustomerOrderHeader coh ON cod.CustomerOrderHeaderInstance = coh.Instance
JOIN	QSPCanadaOrderManagement..Batch b ON coh.OrderBatchDate = b.Date and coh.OrderBatchID = b.ID
JOIN	Customer cust
			ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
JOIN	QSPCanadaCommon..CodeDetail cdProductType ON cdProductType.Instance = cod.ProductType
WHERE	inv.INVOICE_DATE BETWEEN '2015-07-01' AND '2015-12-31'
ORDER BY inv.INVOICE_ID