SELECT 
		 b.OrderId
		, b.Date DateOrderLoadedInFFS
		, b.ShipToAccountId AccountID
      , acc.Name AccountName
		, b.CampaignID
      , b2.OrderID MainOrderID
		,Case
			WHEN b.StatusInstance = 40010 THEN 0
			WHEN b.StatusInstance = 40014 THEN 1
		END IsPartiallyFulfilled
		,rrb.DatePrinted
		,camp.SuppliesDeliveryDate
		,IsNull(rrb.IsPrinted,0) IsPrinted
		, sg.ShipmentGroupName
		, CASE sg.ShipmentGroupName WHEN 'COOKIE DOUGH' THEN camp.CookieDoughDeliveryDate ELSE NULL END CookieDoughDeliveryDate
		,cd.Description OrderType
		,SUM(cod.Quantity) NumLineItems
	FROM		Batch b
	JOIN		CustomerOrderHeader coh
					ON	coh.OrderBatchID = b.ID
					AND	coh.OrderBatchDate = b.Date
	JOIN		CustomerOrderDetail cod
					ON	cod.CustomerOrderHeaderInstance = coh.Instance
	JOIN		BatchDistributionCenter bdc
					ON	bdc.BatchDate = b.Date
					AND	bdc.BatchID = b.ID
	LEFT JOIN	ReportRequestBatch rrb
					ON	rrb.BatchOrderID = b.OrderID
	LEFT JOIN	ShipmentGroup sg 
					ON sg.ShipmentGroupID = rrb.ShipmentGroupID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	LEFT JOIN	QSPCanadaCommon..QSPProductLine pl
					ON	pl.ID = bdc.QSPProductLine --cod.ProductType
					AND pl.ID = cod.ProductType
	LEFT JOIN	QSPCanadaCommon..CAccount acc ON acc.ID = camp.ShipToAccountID
	LEFT JOIN	QSPCanadaCommon..Address ad ON ad.AddressListID = acc.AddressListID AND ad.address_type = 54001
	LEFT JOIN	QSPCanadaCommon..Province prov ON prov.Province_Code = ad.stateProvince
	JOIN		QSPCanadaCommon..CodeDetail cd ON cd.Instance = b.OrderQualifierID
   LEFT JOIN   Batch b2 ON b2.OrderQualifierID IN (39001) AND b2.CampaignID = b.CampaignID AND b2.id < 20000
   WHERE		bdc.StatusInstance IN (40010,40014)
	AND			b.StatusInstance NOT IN (40013,40005)
	AND			b.Date >='7/1/05'
	--AND			cod.delFlag <> 1
	AND			(sg.ShipmentGroupID = pl.ShipmentGroupID)
	/*AND			((cod.DistributionCenterID in (1, 2) AND cod.ProductType in ('+@ProdLine+')) OR b.OrderID IN (SELECT DISTINCT LandedOrderID  
																								FROM OnlineOrderMappingTable  
																								WHERE OnlineOrderID IN (SELECT	DISTINCT b2.OrderID
																														FROM	Batch b2
																														JOIN	CustomerOrderHeader coh2
																																	ON	coh2.OrderBatchID = b2.ID
																																	AND	coh2.OrderBatchDate = b2.Date
																														JOIN	CustomerOrderDetail cod2
																																	ON	cod2.CustomerOrderHeaderInstance = coh2.Instance
																														JOIN	QSPCanadaCommon..QSPProductLine pl2
																																	ON	pl2.ID = cod2.ProductType
																														WHERE	cod2.IsShippedToAccount = 1 
																														AND		b2.OrderQualifierID = 39009
																														AND		b2.StatusInstance not in (40005, 40013)
																														AND		cod2.StatusInstance not in ( 508, 513)
																														AND		cod2.DelFlag <> 1
																														AND		cod2.ProductType in ('+@ProdLine+')
																														AND		sg.ShipmentGroupID = pl2.ShipmentGroupID
																														--AND		(ISNULL(sg.ShipmentGroupID, pl2.ShipmentGroupID) = pl2.ShipmentGroupID)
																														AND		cod2.DistributionCenterID in (1, 2))))*/
	AND		(b.OrderQualifierID <> 39009 OR (cod.IsShippedToAccount = 0 AND cod.DistributionCenterID IN (1, 2) AND cod.StatusInstance NOT IN (508, 513)))
	--AND		sg.ShipmentGroupID NOT IN (3)
	--AND		(ISNULL(camp.CookieDoughDeliveryDate, GETDATE()) < DATEADD(day, 7, GETDATE()) OR sg.ShipmentGroupID NOT IN (2))
	GROUP BY b.OrderId
		, b.Date
		, b.ShipToAccountId
      , acc.Name
		, b.CampaignID
      , b2.OrderID
		,Case
			WHEN b.StatusInstance = 40010 THEN 0
			WHEN b.StatusInstance = 40014 THEN 1
		END 
		,rrb.DatePrinted
		,camp.SuppliesDeliveryDate
		,IsNull(rrb.IsPrinted,0) 
		, sg.ShipmentGroupName
		, camp.CookieDoughDeliveryDate
		, cd.Description
	ORDER BY b.Date