USE QSPCanadaOrderManagement
GO

--Sales GL Details.xlsx
SELECT		inv.Invoice_ID, inv.Invoice_Date, b.OrderID, cod.CustomerOrderHeaderInstance, cod.TransID, cdProductType.Description ProductType,
			cod.ProductCode, cod.ProductName, cod.Price, cust.Firstname + ' ' + cust.Lastname CustomerName, cust.Address1 CustomerShippingAddress1,
			cust.Address2 CustomerShippingAddress2, cust.State CustomerShippingProvince, cust.Zip CustomerShippingPostalCode
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		QSPCanadaFinance..Invoice inv ON inv.Invoice_ID = cod.InvoiceNumber
JOIN		QSPCanadaCommon..CodeDetail cdProductType ON cdProductType.Instance = cod.ProductType
LEFT JOIN	Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
WHERE		inv.Invoice_Date BETWEEN '2016-07-01' AND '2016-12-31'
ORDER BY	inv.Invoice_ID