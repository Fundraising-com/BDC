USE QSPCanadaOrderManagement

SELECT		fm.Firstname + ' ' + fm.Lastname FM,
			camp.ID CampaignID,
			acc.ID AccountID,
			acc.Name AccountName,
			cd.Description BrochureType,
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL A%' THEN cod.Quantity ELSE 0 END) 'Level A',
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL B%' THEN cod.Quantity ELSE 0 END) 'Level B',
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL C%' THEN cod.Quantity ELSE 0 END) 'Level C',
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL D%' THEN cod.Quantity ELSE 0 END) 'Level D',
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL E%' THEN cod.Quantity ELSE 0 END) 'Level E',
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL F%' THEN cod.Quantity ELSE 0 END) 'Level F',
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL G%' THEN cod.Quantity ELSE 0 END) 'Level G',
			SUM(CASE WHEN cod.ProductName LIKE '%LEVEL H%' THEN cod.Quantity ELSE 0 END) 'Level H'

FROM		CustomerOrderDetail cod
JOIN		QSPCanadaProduct..PRICING_DETAILS pd ON pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product p ON p.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaProduct..ProgramSection ps ON ps.ID = pd.ProgramSectionID
JOIN		QSPCanadaProduct..Program_Master pm ON pm.Program_ID = ps.Program_ID
JOIN		CustomerOrderHeader coh ON coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		Batch b ON b.ID = coh.OrderBatchID AND b.Date = coh.OrderBatchDate
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN		QSPCanadaCommon..CAccount acc ON acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..FieldManager fm ON fm.FMID = camp.FMID
JOIN		QSPCanadaCommon..CodeDetail cd ON cd.Instance = pm.SubType
WHERE		cod.CreationDate BETWEEN '2016-07-01' AND '2016-12-31'
AND			cod.ProductType IN (46008,46013,46014)
AND			cod.ProductName LIKE '%LEVEL %'
AND			cod.DelFlag = 0
GROUP BY	fm.Firstname + ' ' + fm.Lastname,
			camp.ID,
			acc.ID,
			acc.Name,
			cd.Description
ORDER BY	fm.Firstname + ' ' + fm.Lastname,
			acc.ID,
			cd.Description