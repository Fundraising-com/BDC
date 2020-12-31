USE QSPCanadaOrderManagement

SELECT		cod.ProductCode,
			cod.ProductName,
			SUM(cod.Quantity) Quantity
FROM		CustomerOrderDetail cod
WHERE		cod.CreationDate BETWEEN '2017-01-01' AND '2017-12-31'
AND			cod.ProductType IN (46018)
AND			cod.StatusInstance = 508
AND			cod.DelFlag = 0
GROUP BY	cod.ProductCode,cod.ProductName
ORDER BY	SUM(cod.Quantity) desc