SELECT		fm.Firstname + ' ' + fm.Lastname FM, acc.ID AccountID, acc.Name AccountName, camp.ID CampaignID, cdCampStat.Description CampaignStatus, camp.StartDate CampaignStartDate, camp.EndDate CampaignEndDate,
			p.Name LandedProgramWithNoLandedSales, SUM(cod.QuantityShipped) QuantityBrochuresShippedOfSaidProgram, QSPCanadaCommon.dbo.UDF_GetCampaignPrograms(camp.ID) LandedProgramsRunInCampaign
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CodeDetail cdCampStat ON cdCampStat.Instance = camp.Status
JOIN		QSPCanadaCommon..CampaignProgram cp ON cp.CampaignID = camp.ID AND cp.DeletedTF = 0 AND cp.OnlineOnly = 0
JOIN		QSPCanadaCommon..Program p ON p.ID = cp.ProgramID
JOIN		QSPCanadaCommon..CAccount acc ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
LEFT JOIN	(Batch b
JOIN		QSPCanadaFinance..Invoice inv ON inv.Order_ID = b.OrderID
JOIN		QSPCanadaFinance..Invoice_Section invSec ON invSec.Invoice_ID = inv.Invoice_ID)
				ON b.CampaignID = camp.ID AND b.OrderQualifierID IN (39001, 39002, 39006)
				AND invSec.ProgramType = CASE cp.ProgramID WHEN 1 THEN 30306 WHEN 2 THEN 30306 WHEN 23 THEN 30310 WHEN 39 THEN 30310 WHEN 44 THEN 30308 WHEN 50 THEN 30317 WHEN 53 THEN 30323 WHEN 54 THEN 30324 
											WHEN 55 THEN 30325 WHEN 56 THEN 30326 WHEN 58 THEN 30328 WHEN 59 THEN 30329 WHEN 60 THEN 30306 ELSE 0 END

JOIN		Batch bF ON bF.CampaignID = camp.ID
JOIN		CustomerOrderHeader coh ON coh.OrderBatchID = bF.ID AND coh.OrderBatchDate = bF.Date
JOIN		CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance AND cod.DelFlag <> 1 AND cod.StatusInstance = 508
JOIN		QSPCanadaCommon..CampaignToContentCatalog ccc ON ccc.CampaignID = bF.CampaignID AND ccc.ProgramID = cp.ProgramID
JOIN		QSPCanadaProduct..pricing_details pd ON pd.Magprice_Instance = cod.PricingDetailsID AND pd.FSIsBrochure = 1 AND pd.FSContent_Catalog_Code = ccc.Content_Catalog_Code
 
WHERE		camp.OnlineOnlyPrograms = 0
AND			camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31'
AND			invSec.Invoice_Section_ID IS NULL
AND			cp.ProgramID in (1,2,44,50,53,54,55,56,58,59,60)
GROUP BY	fm.Firstname + ' ' + fm.Lastname, acc.ID, acc.Name, camp.ID, cdCampStat.Description, camp.StartDate, camp.EndDate, p.Name

-----

SELECT		fm.Firstname + ' ' + fm.Lastname FM, SUM(cod.QuantityShipped) QuantityBrochuresShippedWithNoResultingSale
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CampaignProgram cp ON cp.CampaignID = camp.ID AND cp.DeletedTF = 0 AND cp.OnlineOnly = 0
JOIN		QSPCanadaCommon..Program p ON p.ID = cp.ProgramID
JOIN		QSPCanadaCommon..CAccount acc ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
LEFT JOIN	(Batch b
JOIN		QSPCanadaFinance..Invoice inv ON inv.Order_ID = b.OrderID
JOIN		QSPCanadaFinance..Invoice_Section invSec ON invSec.Invoice_ID = inv.Invoice_ID)
				ON b.CampaignID = camp.ID AND b.OrderQualifierID IN (39001, 39002, 39006)
				AND invSec.ProgramType = CASE cp.ProgramID WHEN 1 THEN 30306 WHEN 2 THEN 30306 WHEN 23 THEN 30310 WHEN 39 THEN 30310 WHEN 44 THEN 30308 WHEN 50 THEN 30317 WHEN 53 THEN 30323 WHEN 54 THEN 30324 
											WHEN 55 THEN 30325 WHEN 56 THEN 30326 WHEN 58 THEN 30328 WHEN 59 THEN 30329 WHEN 60 THEN 30306 ELSE 0 END

JOIN		Batch bF ON bF.CampaignID = camp.ID
JOIN		CustomerOrderHeader coh ON coh.OrderBatchID = bF.ID AND coh.OrderBatchDate = bF.Date
JOIN		CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = coh.Instance AND cod.DelFlag <> 1 AND cod.StatusInstance = 508
JOIN		QSPCanadaCommon..CampaignToContentCatalog ccc ON ccc.CampaignID = bF.CampaignID AND ccc.ProgramID = cp.ProgramID
JOIN		QSPCanadaProduct..pricing_details pd ON pd.Magprice_Instance = cod.PricingDetailsID AND pd.FSIsBrochure = 1 AND pd.FSContent_Catalog_Code = ccc.Content_Catalog_Code
 
WHERE		camp.OnlineOnlyPrograms = 0
AND			camp.StartDate BETWEEN '2016-07-01' AND '2016-12-31'
AND			invSec.Invoice_Section_ID IS NULL
AND			cp.ProgramID in (1,2,44,50,53,54,55,56,58,59,60)
GROUP BY	fm.Firstname + ' ' + fm.Lastname
ORDER BY	SUM(cod.QuantityShipped) DESC