SELECT	pt.Description ProductLine,
		b.CampaignID,
		b.OrderID,
		b.Date OrderDate,
		oq.Description OrderType,
		cod.ProductCode,
		cod.ProductName,
		cod.Quantity,
		cod.isshippedtoaccount,
		cod.Recipient
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN	QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
JOIN	QSPCanadaCommon..CodeDetail pt ON pt.Instance = cod.ProductType
JOIN	qspcanadacommon..codedetail oq ON oq.Instance = b.OrderQualifierID
WHERE	b.statusinstance in (40010, 40014)
and		b.date >= '2016-07-01'
and		b.date < '2017-01-11'
and		cod.distributioncenterid = 1
and		cod.statusinstance not in (501, 506, 507, 508, 512, 513)
order by b.date, b.orderid desc