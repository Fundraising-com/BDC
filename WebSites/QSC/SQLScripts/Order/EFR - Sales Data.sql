SELECT	ioi.InternetOrderID,
		coh.creationdate AS FulfSystemImportDate,
		o.Create_Date AS qspcaOrderCreateDate,
		DATEPART(MONTH, o.Create_Date) AS qspcaOrderCreateMonth,
		camp.ID as CampaignID,
		acc.Name,
		cod.CustomerOrderHeaderInstance,
		cod.TransID,
		cod.ProductCode,
		cod.ProductName,
		cod.Price AS PriceWithTax,
		cod.Tax,
		cod.Tax2,
		cod.Price - (cod.Tax + cod.Tax2) AS PriceMinusTax,
		cod.StatusInstance,
		cod.CatalogPrice,
		cod.PriceOverrideID,
		cod.Net,
		cod.Gross
FROM	CA_OLTP1.QSPCanadaOrderManagement.dbo.InternetOrderID ioi
JOIN	CA_OLTP1.QSPCanadaOrderManagement.dbo.CustomerOrderHeader coh 
			ON	coh.Instance = ioi.CustomerOrderHeaderInstance
JOIN	CA_OLTP1.QSPCanadaOrderManagement.dbo.CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN	CA_OLTP1.QSPCanadaCommon.dbo.Campaign camp
			ON	camp.ID = coh.CampaignID
JOIN	CA_OLTP1.QSPCanadaCommon.dbo.Caccount acc
			ON	acc.ID = camp.BillToAccountID
JOIN	QSPEcommerce..Cart cart
			ON	cart.EDS_Order_ID = ioi.InternetOrderID
JOIN	QSPFulfillment..[order] o on o.Order_ID = cart.X_Order_ID
WHERE	camp.fmid = '0508'
AND		o.Create_Date > '2009-01-01'
ORDER BY coh.CreationDate DESC

