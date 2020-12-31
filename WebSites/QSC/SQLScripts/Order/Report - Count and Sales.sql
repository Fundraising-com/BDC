SELECT	c.IsStaffOrder, count(*) AS NumSubs, sum(pd.Basic_Price_Yr) AS Cost
FROM	CustomerOrderDetail cod
JOIN	QSPCanadaProduct..Pricing_Details pd
			ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN	QSPCanadaProduct..Product p
			ON	p.Product_Instance = pd.Product_Instance
JOIN	CustomerOrderHeader coh
			ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN	QSPCanadaCommon..Campaign c
			ON	c.ID = coh.CampaignID
WHERE	pd.Pricing_Year = 2008 and pd.Pricing_Season = 'S'
GROUP BY c.IsStaffOrder