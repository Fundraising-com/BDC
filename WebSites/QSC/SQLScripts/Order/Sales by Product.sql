USE [QSPCanadaOrderManagement]
GO

SELECT	*
FROM	CustomerOrderDetail cod
JOIN	QSPCanadaProduct..Pricing_Details pd
			ON	pd.Magprice_Instance = cod.PricingDetailsID
JOIN	QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance
WHERE	p.Product_Code IN ('1048531','1048578')
AND		cod.DelFlag = 0

SELECT	b.orderid, productcode, productname, cdPL.Description ProductLine, quantity, price, case cod.statusinstance when 508 then 'shipped' else 'not shipped' end, cod.creationdate, cod.customerorderheaderinstance, cod.transid
FROM	CustomerOrderDetail cod
JOIN	QSPCanadaProduct..Pricing_Details pd
			ON	pd.Magprice_Instance = cod.PricingDetailsID
JOIN	QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	Batch b
			ON	b.ID = coh.OrderBatchID
			AND	b.Date = coh.OrderBatchDate
JOIN	QSPCanadaCommon..codedetail cdPL
			ON	cdPL.Instance = cod.ProductType
WHERE	p.type IN (46002, 46020, 46019, 46018)
AND		cod.DelFlag = 0
and cod.creationdate >= '2012-07-01'