SELECT		pt.Description ProductLine,
			o.CampaignID,
			o.OrderID,
			o.Date OrderDate,
			oq.Description OrderType,
			isnull((select top 1 OrderID from qspcanadaordermanagement..Batch b where orderqualifierid in (39001,39002) and b.CampaignID = camp.ID), 0) LandedOrderID,
			o.ProductCode,
			o.ProductName,
			o.Quantity,
			cod.isshippedtoaccount,
			cod.Recipient,
			(TotalPrice - PostageAmount -
			CASE WHEN o.SectionType IN (1, 3, 7, 10) THEN 0.00 ELSE (o.tax+o.Tax2) END) *
			(1 - CASE WHEN o.SectionType = 1 THEN CASE WHEN o.CAccountCodeClass = 'FM' THEN 0.00
														WHEN o.OrderQualifierID = 39022 THEN 0.00
														WHEN o.ProgramType IN (30327) THEN 0.75
														WHEN o.ProgramType IN (30323, 30329) THEN 0.45
														ELSE 0.40
													END
					  WHEN o.SectionType = 2 THEN CASE camp.IsStaffOrder WHEN 1 THEN 0.00 ELSE 0.37 END
					  WHEN o.SectionType = 9 THEN 0.40 --Todo
					  WHEN o.SectionType = 11 THEN CASE WHEN o.OrderQualifierID = 39022 THEN 0.00 
														WHEN o.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.40
													END
					  WHEN o.SectionType = 14 THEN CASE WHEN o.OrderQualifierID = 39022 THEN 0.00 
														WHEN o.CAccountCodeClass = 'FM' THEN 0.00
														WHEN o.TRTGenerationCode IN ('2') THEN 0.20
														WHEN o.TRTGenerationCode IN ('0', 'N') THEN 0.00
														ELSE 0.37
													END
					  WHEN o.SectionType = 15 THEN CASE WHEN o.OrderQualifierID = 39022 THEN 0.00 
														WHEN o.CAccountCodeClass = 'FM' THEN 0.00
														ELSE 0.37
													END
					  ELSE 0
			END) - CASE WHEN o.SectionType IN (1) THEN (o.tax+o.Tax2) ELSE 0.00 END NetSales,
			TotalPrice + CASE WHEN o.SectionType IN (1, 3, 7, 10, 11) THEN (o.tax+o.Tax2) ELSE 0.00 END GrossSales
FROM		QSPCanadaFinance.dbo.UDF_GetBillableOrdersFromBatch() o
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = o.CampaignID
JOIN		QSPCanadaCommon..CodeDetail pt ON pt.Instance = o.ProductType
JOIN		qspcanadacommon..codedetail oq ON oq.Instance = o.OrderQualifierID
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = o.Instance AND cod.TransID = o.TransID
where o.date < '2017-01-09'
and cod.distributioncenterid = 1
/*and o.orderid not in (
11289648,
11792447,
11262587,
11309372,
11366732,
11383568,
11389765,
11389638,
11387456,
11405435,
11461220,
11437575,
11559217,
11687152,
11513457,
11508925,
11695176,
11259102,
11280708)*/
order by o.date