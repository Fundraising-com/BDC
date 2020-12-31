SELECT b.OrderQualifierID, fm.firstname + ' ' + fm.lastname FM, acc.Name AccountName, c.id CampaignID, case b.orderqualifierid when 39001 then 'Main ($2/bag)' when 39022 then 'Sample ($0.50/bag)' ELSE 'unknown' end OrderType, b.OrderID, b.Date OrderDate,
   	cod.CustomerOrderHeaderInstance, cod.TransID, cod.ProductCode, cod.ProductName, cod.Quantity, cod.Price
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
join qspcanadacommon..campaign c on c.id = b.campaignid
join qspcanadacommon..fieldmanager fm on fm.fmid = c.fmid
join QSPCanadaCommon..CAccount acc on acc.id = b.accountid
WHERE	cod.producttype = 46019
and acc.caccountcodeclass = 'FM'
and b.date >= '2017-01-01'
and cod.delflag = 0
order by acc.id, b.date