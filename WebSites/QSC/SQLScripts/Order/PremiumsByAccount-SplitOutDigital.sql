--Should exclude BDC
SELECT		'2015' FiscalYear,
			fm.FMID,
			fm.FirstName FMFirstName,
			fm.LastName FMLastName,
			acc.ID AccountID,
			acc.Name AccountName,
			SUM(CASE WHEN p.Pub_Nbr = 39 AND pd.QSPPremiumID > 0 AND p.Product_Code NOT LIKE 'D%' THEN 1 ELSE 0 END) AS RDPrint,
			SUM(CASE WHEN p.Pub_Nbr = 39 AND pd.QSPPremiumID > 0 AND p.Product_Code LIKE 'D%' THEN 1 ELSE 0 END) AS RDDigital,
			SUM(CASE WHEN p.Pub_Nbr = 43 AND pd.QSPPremiumID > 0 AND p.Product_Code NOT LIKE 'D%' THEN 1 ELSE 0 END) AS RogersPrint,
			SUM(CASE WHEN p.Pub_Nbr = 43 AND pd.QSPPremiumID > 0 AND p.Product_Code LIKE 'D%' THEN 1 ELSE 0 END) AS RogersDigital
FROM		CustomerOrderDetail cod
JOIN		CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b
				ON	b.ID = coh.OrderBatchID
				AND	b.[Date] = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.ShipToAccountID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID 
JOIN		QSPCanadaProduct..Product p
				ON	p.Product_Instance = pd.Product_Instance
WHERE		camp.StartDate BETWEEN '2014-07-01' AND '2015-06-30'
AND			cod.DelFlag <> 1
AND			b.StatusInstance <> 40005
GROUP BY	fm.FMID,
			fm.FirstName,
			fm.LastName,
			acc.ID,
			acc.Name
ORDER BY	fm.FMID,
			acc.ID