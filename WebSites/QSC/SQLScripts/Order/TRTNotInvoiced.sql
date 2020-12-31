SELECT	CASE WHEN cod.StatusInstance = 500 THEN 'CC Error'
			 WHEN cod.Quantity = 0 THEN 'Bad Quantity'
			 WHEN cust.StatusInstance = 301 THEN 'Address Error'
			 WHEN isnull(b.IsInvoiced, 0) = 0 THEN 'OrderNotFulfilledAndInvoicedYet'
			 ELSE 'Unknown'
		END Issue,
		cod.*, cust.*, b.*, *
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN Customer cust
			ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
WHERE	cod.producttype = 46023
and cod.invoicenumber = 0
and isnull(cod.IsVoucherRedemption,0) = 0
and cod.DelFlag = 0
and coh.Instance not in
(
	SELECT	CustomerOrderHeaderInstance
	FROM	Incident i
	JOIN	IncidentAction ia on ia.IncidentInstance = i.IncidentInstance
	WHERE	ia.ActionInstance IN (1, 14, 18, 150, 151) --1 cancel, 14 cancel before remit, 18 CC update, 150 new sub to invoice, 151 new item to invoice
)
order by cod.customerorderheaderinstance