SELECT		p.RemitCode,
			SUM(CASE WHEN codrh.GiftOrderType in ('R') THEN 1
				ELSE 0
			END) RegularGiftCards,
			SUM(CASE WHEN codrh.GiftOrderType in ('X') THEN 1
				ELSE 0
			END) HolidayGiftCards
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
				AND	codrh.Status in (42000,42001)
JOIN		RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
				AND	p.Status = 30600
WHERE		codrh.GiftOrderType in ('X','R')
AND			cod.IsGiftCardSent = 1
AND			rb.[Date] between '2012-07-01' and '2012-12-31'
GROUP BY	p.RemitCode
ORDER BY	p.RemitCode