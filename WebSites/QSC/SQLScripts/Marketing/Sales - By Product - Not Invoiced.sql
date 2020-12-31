USE QSPCanadaOrderManagement

SELECT		cd.Description ProductType,
			cod.ProductCode,
			p.OracleCode SAPMaterialCode,
			cod.productName,
			SUM(CASE b.OrderQualifierid WHEN 39009 THEN cod.Gross ELSE 0 END) OnlineGrossSale,
			SUM(CASE b.OrderQualifierid WHEN 39009 THEN 0 ELSE cod.Gross END) LandedGrossSale
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaCommon..CodeDetail cd on cd.Instance = cod.ProductType
JOIN		QSPCanadaProduct..Pricing_Details pd on pd.magprice_instance = cod.pricingdetailsid
JOIN		QSPcanadaProduct..Product p on p.Product_Instance = pd.Product_Instance
WHERE		cod.CreationDate BETWEEN '2017-07-01' AND '2017-11-09'
AND			cod.ProductType IN (46001, 46002, 46018, 46019, 46020, 46022, 46023)
AND			cod.DelFlag = 0
AND			b.OrderQualifierID NOT IN (39007,39012,39011,39008,39018,39019)
AND			cod.StatusInstance NOT IN (500)
AND			b.StatusInstance IN (40010,40012,40013,40014)
AND			cod.DelFlag = 0
AND			cod.ProductCode <> 'NNNN'
AND			ISNULL(cod.PricingDetailsID, 0) <> 0
GROUP BY	cod.ProductCode, p.OracleCode, cod.ProductType, cod.ProductName, cd.Description
ORDER BY	cd.Description, cod.ProductCode
