USE QSPCanadaOrderManagement

SELECT		fm.FirstName + ' ' + fm.LastName FM,
			COUNT(camp.ID) NumberCampaigns,
			SUM(CASE WHEN p.ID IN (1, 2, 60, 44, 50, 55, 58, 56, 59, 54, 53) THEN cod.Quantity ELSE 0 END) TotalBrochures,
			SUM(CASE WHEN p.ID IN (1, 2) THEN cod.Quantity ELSE 0 END) MagazineBrochures,
			SUM(CASE WHEN p.ID IN (44) THEN cod.Quantity ELSE 0 END) CookieDoughBrochures,
			--SUM(CASE WHEN p.ID IN (50) THEN cod.Quantity ELSE 0 END) TRTBrochures,
			SUM(CASE WHEN p.ID IN (55) THEN cod.Quantity ELSE 0 END) BloomDoughBrochures,
			--SUM(CASE WHEN p.ID IN (58) THEN cod.Quantity ELSE 0 END) NaturallyGoodBrochures,
			--SUM(CASE WHEN p.ID IN (56) THEN cod.Quantity ELSE 0 END) KitchenCollectionBrochures,
			SUM(CASE WHEN p.ID IN (59) THEN cod.Quantity ELSE 0 END) EnjoysomethingSweetBrochures,
			--SUM(CASE WHEN p.ID IN (54) THEN cod.Quantity ELSE 0 END) FestivalBrochures,
			SUM(CASE WHEN p.ID IN (53) THEN cod.Quantity ELSE 0 END) DreamBigBrochures,
			SUM(CASE WHEN p.ID IN (60) THEN cod.Quantity ELSE 0 END) Top20MagazineFlyer
FROM		CustomerOrderDetail cod
JOIN		QSPCanadaProduct..PRICING_DETAILS pd ON pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..ProgramSection ps ON ps.ID = pd.ProgramSectionID
--JOIN		QSPCanadaProduct..PROGRAM_MASTER pm ON pm.Program_ID = ps.Program_ID
JOIN		QSPCanadaProduct..ProgFSSectionMap pms ON pms.CATALOG_SECTION_ID = ps.ID
JOIN		QSPCanadaCommon..Program p ON p.ID = pms.PROGRAM_ID
JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
WHERE		cod.CreationDate BETWEEN '2017-07-01' AND '2017-12-31'
AND			cod.DelFlag = 0
AND			pd.FSIsBrochure = 1
AND			cod.StatusInstance = 508
GROUP BY	fm.FirstName + ' ' + fm.LastName
ORDER BY	SUM(cod.Quantity) DESC