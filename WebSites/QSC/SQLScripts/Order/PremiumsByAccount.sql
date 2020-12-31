--Should exclude BDC
SELECT		fm.FMID,
			fm.FirstName FMFirstName,
			fm.LastName FMLastName,
			acc.ID AccountID,
			acc.Name AccountName,
			--SUM(CASE WHEN p.RemitCode = '3313' AND pd.Nbr_of_Issues	= 12 THEN 1 ELSE 0 END) AS PeopleLow,
			--SUM(CASE WHEN p.RemitCode = '3313' AND pd.Nbr_of_Issues	= 22 THEN 1 ELSE 0 END) AS PeopleMed,
			--SUM(CASE WHEN p.RemitCode = '3313' AND pd.Nbr_of_Issues	= 53 THEN 1 ELSE 0 END) AS PeopleHigh,
			--SUM(CASE WHEN p.Pub_Nbr = 39 AND pd.QSPPremiumID > 0 AND p.RemitCode in ('3186','3187') THEN 1 ELSE 0 END) AS RDReadersDigest,
			--SUM(CASE WHEN p.Pub_Nbr = 39 AND pd.QSPPremiumID > 0 AND p.RemitCode = '11GL' THEN 1 ELSE 0 END) AS RDBestHealth,
			SUM(CASE WHEN p.Pub_Nbr = 39 AND pd.QSPPremiumID > 0 THEN 1 ELSE 0 END) AS RD
			--SUM(CASE WHEN p.Pub_Nbr = 43 AND pd.QSPPremiumID > 0 THEN 1 ELSE 0 END) AS Rogers
			--SUM(CASE WHEN p.RemitCode = '0870' THEN 1 ELSE 0 END) AS Macleans --Temporary, can remove for Fall 2014+
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
WHERE		b.Date BETWEEN '2016-07-01' AND '2017-06-30'
AND			camp.StartDate BETWEEN '2016-07-01' AND '2017-06-30'
AND			cod.DelFlag <> 1
AND			b.StatusInstance <> 40005
GROUP BY	fm.FMID,
			fm.FirstName,
			fm.LastName,
			acc.ID,
			acc.Name
ORDER BY	fm.FMID,
			acc.ID