SELECT	b.Date OrderDate, b.OrderID, cod.CustomerOrderHeaderInstance, cod.TransID, cod.ProductCode, cod.Quantity, cod.ProductName, cd.Description ProductType,
		c.ID CampaignID, c.StartDate CampaignStartDate, c.EndDate CampaignEndDate, fm.Firstname + ' ' + fm.Lastname FM, c.CookieDoughDeliveryDate,
		DATEDIFF(dd, b.Date, CASE c.CookieDoughDeliveryDate WHEN '1995-01-01' THEN c.EndDate ELSE c.CookieDoughDeliveryDate END) ShippingDelay
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
left join Batch b2 on b2.campaignid = b.campaignid and b2.orderqualifierid = 39001
join	QSPCanadaCommon..Campaign c on c.id = b.CampaignID
join	QSPCanadaCommon..FieldManager fm on fm.fmid = c.fmid
join	qspcanadacommon..codedetail cd on cd.instance = cod.producttype
WHERE	cod.isshippedtoaccount = 1
and		b.orderqualifierid = 39009
and		cod.statusinstance not in (508)
and		b2.OrderID is null
and		DATEDIFF(dd, b.Date, CASE c.CookieDoughDeliveryDate WHEN '1995-01-01' THEN c.EndDate ELSE c.CookieDoughDeliveryDate END) > 0
order by ShippingDelay desc