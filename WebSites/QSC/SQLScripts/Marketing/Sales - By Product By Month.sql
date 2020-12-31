USE QSPCanadaOrderManagement

SELECT		cod.ProductCode,
			(SELECT ISNULL(SUM(cod2.Quantity),0) FROM CustomerOrderDetail cod2 WHERE cod2.ProductCode = cod.ProductCode AND cod2.CreationDate BETWEEN '2012-08-01' AND '2012-09-01') Aug2012,
			(SELECT ISNULL(SUM(cod2.Quantity),0) FROM CustomerOrderDetail cod2 WHERE cod2.ProductCode = cod.ProductCode AND cod2.CreationDate BETWEEN '2012-09-01' AND '2012-10-01') Sep2012,
			(SELECT ISNULL(SUM(cod2.Quantity),0) FROM CustomerOrderDetail cod2 WHERE cod2.ProductCode = cod.ProductCode AND cod2.CreationDate BETWEEN '2012-10-01' AND '2012-11-01') Oct2012,
			(SELECT ISNULL(SUM(cod2.Quantity),0) FROM CustomerOrderDetail cod2 WHERE cod2.ProductCode = cod.ProductCode AND cod2.CreationDate BETWEEN '2012-11-01' AND '2012-12-01') Nov2012,
			(SELECT ISNULL(SUM(cod2.Quantity),0) FROM CustomerOrderDetail cod2 WHERE cod2.ProductCode = cod.ProductCode AND cod2.CreationDate BETWEEN '2012-12-01' AND '2013-01-01') Dec2012
FROM		CustomerOrderDetail cod
WHERE		cod.CreationDate BETWEEN '2012-08-01' AND '2013-01-01'
AND			cod.ProductType IN (46018)
AND			cod.StatusInstance = 508
AND			cod.DelFlag = 0
GROUP BY	cod.ProductCode
ORDER BY	cod.ProductCode
