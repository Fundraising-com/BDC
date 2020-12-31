USE QSPCanadaOrderManagement

SELECT		cd.Description BrochureType,
			p.OracleCode SAPMaterialNumber,
			cod.ProductCode,
			cod.productName,
			SUM(CASE cod.IsShippedToAccount WHEN 1 THEN cod.Quantity ELSE 0 END) ShipToAccountQuantity,
			SUM(CASE cod.IsShippedToAccount WHEN 0 THEN cod.Quantity ELSE 0 END) ShipToCustomerQuantity,
			SUM(cod.Quantity) TotalQuantity
FROM		CustomerOrderDetail cod
JOIN		QSPCanadaProduct..PRICING_DETAILS pd ON pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p ON p.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaProduct..ProgramSection ps ON ps.ID = pd.ProgramSectionID
JOIN		QSPCanadaProduct..Program_Master pm ON pm.Program_ID = ps.Program_ID
JOIN		QSPCanadaCommon..CodeDetail cd ON cd.Instance = pm.SubType
WHERE		cod.CreationDate BETWEEN '2016-07-01' AND '2016-12-31'
AND			cod.ProductType IN (46002, 46020)
AND			pm.SubType NOT IN (30308)
AND			cod.DelFlag = 0
GROUP BY	cod.ProductCode, cod.ProductName, p.OracleCode, cd.Description
ORDER BY	cd.Description, cod.ProductCode
