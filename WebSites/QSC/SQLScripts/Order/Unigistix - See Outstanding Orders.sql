SELECT   Batch.OrderID,  ISNULL(CONVERT(varchar(10), TransmissionLog.TransmissionDate, 101), 
                      'Not Avail') AS TransmissionDate, aCType.Description, count(*), CONVERT(varchar(10), Batch.[Date], 101) AS DateLoaded, 
			CONVERT(varchar(10), Batch.DateReceived, 101) AS DateRecieved, 
                      Batch.CampaignID, Batch.EnterredCount, Batch.OrderDetailCount, aType.Description AS type_description, 
                      aQ.Description AS order_qualifier_description, aStatus.Description, a.Name, 
	QSPCanadaCommon.dbo.UDF_GetCampaignPrograms(Batch.CampaignID) AS CampaignPrograms
FROM TransmissionLog 
	RIGHT OUTER JOIN Batch (nolock)
	inner join customerorderheader coh on orderbatchdate=batch.date	and orderbatchid=batch.id
	inner join customerorderdetail cod on coh.instance = cod.customerorderheaderinstance
	INNER JOIN QSPCanadaCommon.dbo.CodeDetail    aStatus ON Batch.StatusInstance = aStatus.Instance  
	INNER JOIN QSPCanadaCommon.dbo.CodeDetail aType ON Batch.OrderTypeCode = aType.Instance  
	INNER JOIN QSPCanadaCommon.dbo.CodeDetail aQ ON Batch.OrderQualifierID = aQ.Instance 
	INNER JOIN QSPCanadaCommon.dbo.CodeDetail aCType on COD.ProductType = aCType.Instance
	INNER JOIN QSPCanadaCommon.dbo.CAccount a ON Batch.AccountID = a.Id 
	LEFT OUTER JOIN OrderStageTracking ON OrderStageTracking.OrderID = Batch.OrderID AND OrderStageTracking.Stage = 59006 ON 
                      TransmissionLog.ID = OrderStageTracking.TransmissionID
WHERE (Batch.StatusInstance = 40012) AND (DATEPART([year], Batch.[Date]) >= 2006) AND (DATEPART([month], Batch.[Date]) >= 1) AND (DATEPART([day], Batch.[Date]) >= 1)
--and producttype not in (  46002, 46013, 46014)
and cod.statusinstance  in ( 509)
--and batch.orderqualifierid = 39009

--and batch.orderid=9535388

--and TransmissionDate < '11/7/07'
group by Batch.OrderID, producttype, batch.date, DateReceived, batch.campaignid, Batch.EnterredCount, Batch.OrderDetailCount,
aCType.Description,
aType.Description,
	aQ.Description, aStatus.Description, a.Name,TransmissionDate

ORDER BY batch.orderid
