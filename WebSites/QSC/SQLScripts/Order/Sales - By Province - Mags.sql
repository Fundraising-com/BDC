SELECT		cust.State Province,
			p.RemitCode,
			p.Product_Sort_Name,
			count(cod.CustomerOrderHeaderInstance) NumSubs
FROM		Invoice inv
JOIN		QSPCanadaOrderManagement..Batch b
				ON	b.OrderID = inv.Order_ID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.OrderBatchDate = b.Date and coh.OrderBatchID = b.ID
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaOrderManagement..Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END
JOIN		QSPCanadaProduct..PRICING_DETAILS pd
				ON	pd.MagPrice_Instance = cod.pricingdetailsid
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_instance = pd.Product_instance
where		p.Type = 46001
and			cod.creationdate >= '2013-07-01'
GROUP BY	p.RemitCode,
			p.Product_Sort_Name,
			cust.State
ORDER BY	p.RemitCode,
			cust.State