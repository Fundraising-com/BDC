SELECT		b.CampaignID,
			b.OrderID,
			cod.CustomerOrderHeaderInstance,
			cod.TransID,
			ioi.InternetOrderID,
			cod.StatusInstance AS SubStatus,
			codrh.Status AS RemitStatus,
			cod.CreationDate,
			cod.Price AS SubPrice,
			cod.Tax + cod.Tax2 AS SubTaxes,
			CONVERT(NVARCHAR, ROUND((cod.Price - (cod.Tax + cod.Tax2)) * cp.GroupProfit * .01, 6)) AS PotentialGroupProfit,
			CASE WHEN	cod.CreationDate BETWEEN CONVERT(NVARCHAR, '2008-09-15 00:00:00.000', 101) AND CONVERT(NVARCHAR, '2009-01-21 00:00:00.000', 101)
						AND	(codrh.Status IN (42000, 42001, 42004, 42010)
						OR cod.StatusInstance = 508)
						THEN CONVERT(NVARCHAR, ROUND((cod.Price - (cod.Tax + cod.Tax2)) * cp.GroupProfit * .01, 6))
						ELSE 'N/A'
			END AS RealizedGroupProfit
FROM		QSPCanadaOrderManagement..Batch b	 	        
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = b.CampaignID
				AND	camp.IsStaffOrder = 0
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.[Date]
LEFT JOIN	QSPCanadaOrderManagement..InternetOrderID ioi
				ON	ioi.CustomerOrderHeaderInstance = coh.Instance
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
				AND	cod.DelFlag = 0
LEFT JOIN	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
				ON	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
				AND	codrh.TransID = cod.TransID
JOIN		QSPCanadaCommon..CampaignProgram cp
				ON	cp.CampaignID = camp.ID
				AND	cp.ProgramID IN (1,2)
				AND cp.DeletedTF = 0
WHERE		b.OrderQualifierID = 39009
AND			b.CampaignID = 58911
ORDER BY	cod.statusinstance,
			cod.CustomerOrderHeaderInstance,
			cod.TransID

--Nov 30 2009 and prior version
SELECT		b.CampaignID,
			b.OrderID,
			d.CustomerOrderHeaderInstance,
			d.TransID,
			ioi.InternetOrderID,
			d.StatusInstance AS SubStatus,
			d.CreationDate,
			d.Price AS SubPrice,
			d.Tax + d.Tax2 AS SubTaxes,
			CASE WHEN (EXISTS (SELECT CustomerOrderHeaderInstance, TransID FROM QSPCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory RH
		  				WHERE  RH.CustomerOrderHeaderInstance = D.CustomerOrderHeaderInstance AND RH.TransID = D.TransID
						AND	RH.Status IN (42000, 42001, 42004)
		  				AND DateChanged BETWEEN CONVERT(NVARCHAR, '2008-09-15 00:00:00.000', 101) AND CONVERT(NVARCHAR, '2008-12-1 00:00:00.000', 101))
						OR D.StatusInstance=508 ) THEN CONVERT(NVARCHAR, Round(((price)-Round((D.Tax + D.Tax2),2))*CP.GroupProfit * .01,2) )
				ELSE 'N/A'
			END AS RealizedGroupProfit,
			CONVERT(NVARCHAR, Round(((price)-Round((D.Tax + D.Tax2),2))*CP.GroupProfit * .01,2) ) AS PotentialGroupProfit
FROM    	QSPcanadaOrdermanagement.dbo.Batch B	 	        
			LEFT  JOIN QSPCanadaCommon.dbo.CAccount A 	             ON B.AccountID =A.Id
			LEFT  JOIN QSPCanadaCommon.dbo.AddressList AL 	             ON A.AddressListID = AL.ID
			LEFT  JOIN QSPCanadaCommon.dbo.Address AdShip 	             ON AL.ID = AdShip.AddressListID AND AdShip.Address_Type = 54002
			LEFT JOIN QSPCanadaCommon.dbo.TaxApplicableTax TAT	ON AdShip.StateProvince = TAT.Province_Code and TAT.Tax_Id =1 AND tat.SECTION_TYPE_ID=2
			LEFT  JOIN QSPCanadaCommon.dbo.Tax GST		             ON GST.Tax_Id = TAT.Tax_Id 
			LEFT JOIN QSPCanadaCommon.dbo.TaxApplicableTax TATPST	ON AdShip.StateProvince = TATPST.Province_Code AND TATPST.Tax_Id NOT IN(1,2,4,5) AND TATPST.SECTION_TYPE_ID=2
			LEFT  JOIN QSPCanadaCommon.dbo.Tax PST			ON PST.Tax_Id = TATPST.Tax_Id
			LEFT JOIN QSPCanadaCommon.dbo.TaxApplicableTax TATHST	ON AdShip.StateProvince = TATHST.Province_Code AND TATHST.Tax_Id IN(2,4,5) AND TATHST.SECTION_TYPE_ID=2
			LEFT  JOIN QSPCanadaCommon.dbo.Tax HST		             ON HST.Tax_Id = TATHST.Tax_Id,
			QSPCanadaCommon.dbo.Campaign C	 	   	        
			LEFT  JOIN QSPCanadaCommon.dbo.CampaignProgram CP           ON (CP.CampaignId=C.Id AND CP.ProgramId IN(1,2) AND CP.Deletedtf=0),
			QSPCanadaOrdermanagement.dbo.CustomerOrderHeader H
			LEFT JOIN QSPCanadaOrderManagement..InternetOrderID ioi ON ioi.CustomerOrderHeaderInstance = h.Instance      ,
			QSPCanadaOrdermanagement.dbo.CustomerOrderDetail    D ,
			QSPCanadaproduct..pricing_Details PDet			        ,
			QSPCanadaproduct..product Prd				        
		WHERE B.OrderQualifierId=39009
		AND	B.Campaignid=C.[Id]
		AND 	B.[ID] = H.OrderBatchId
		AND	B.[DATE]= H.OrderBatchDate
		AND	D.CustomerOrderHeaderInstance = H.Instance
		AND     PDet.MagPrice_Instance=D.PricingDetailsId
		AND 	Prd.Product_Instance=PDet.Product_Instance
		AND 	D.DelFlag=0	
AND			b.CampaignID = 58911
ORDER BY	d.statusinstance,
			d.CustomerOrderHeaderInstance,
			d.TransID