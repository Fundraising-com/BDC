USE QSPCanadaOrderManagement

SELECT		cd.Description ProductType,
			cod.ProductCode,
			cod.productName,
			CASE cod.ProductType WHEN 46001 THEN COUNT(cod.Quantity) ELSE SUM(cod.Quantity) END Quantity
FROM		CustomerOrderDetail cod
JOIN		QSPCanadaCommon..CodeDetail cd on cd.Instance = cod.ProductType
WHERE		cod.CreationDate BETWEEN '2014-07-01' AND '2014-11-30'
AND			cod.ProductType IN (46001, 46018, 46022, 46023)
AND			cod.InvoiceNumber > 0
AND			cod.DelFlag = 0
GROUP BY	cod.ProductCode, cod.ProductType, cod.ProductName, cd.Description
ORDER BY	cd.Description, cod.ProductCode
