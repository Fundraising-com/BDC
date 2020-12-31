USE QSPCanadaOrderManagement

SELECT		seas.FiscalYear,
			seas.Season,
			codrh.TitleCode,
			codrh.RemitCode,
			codrh.MagazineTitle,
			COUNT(codrh.TitleCode) AS Total_Subs,
			SUM(cod.Price) AS GrossSale,
			SUM(ISNULL(codrh.BasePrice, 0) * ISNULL(codrh.RemitRate, 0)) AS RemitAmount, 
			CASE codrh.CurrencyID WHEN 801 THEN 'CAD' WHEN 802 THEN 'USD' END AS Currency
FROM		CustomerOrderDetailRemitHistory codrh
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch batch
				ON	batch.ID = coh.OrderBatchID
				AND	batch.[Date] = coh.OrderBatchDate
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
JOIN		QSPCanadaCommon..Season seas
				ON	seas.StartDate <= rb.[Date]
				AND	seas.EndDate > rb.[Date]
				AND	seas.Season IN ('F', 'S')
WHERE		batch.StatusInstance <> 40005
AND			codrh.Status IN (42000, 42001)
AND			cod.DelFlag <> 1
GROUP BY	seas.FiscalYear,
			seas.Season,
			codrh.RemitCode,
			codrh.TitleCode,
			codrh.MagazineTitle,
			codrh.CurrencyID
ORDER BY	seas.FiscalYear,
			seas.Season,
			codrh.RemitCode,
			codrh.TitleCode
