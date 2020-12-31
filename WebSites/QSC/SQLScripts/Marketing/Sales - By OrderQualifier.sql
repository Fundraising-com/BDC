USE QSPCanadaOrderManagement

SELECT		cd.Description OrderType,
			b.Date OrderDate,
			b.OrderID,
			SUM(CASE cod.ProductType WHEN 46001 THEN 1 ELSE cod.Quantity END) Quantity
FROM		CustomerOrderDetail cod
join		CustomerOrderHeader coh on coh.Instance = cod.CustomerOrderHeaderInstance
join		Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..CodeDetail cd on cd.Instance = b.OrderQualifierID
WHERE		b.Date BETWEEN '2015-01-01' AND '2015-12-31'
AND			cod.DelFlag = 0
AND			b.OrderQualifierID IN (39002, 39005, 39018, 39019, 39023)
AND			cod.producttype not in (46017,46021)
GROUP BY	cd.Description, b.Date, b.OrderID
ORDER BY	cd.Description, b.Date, b.OrderID

