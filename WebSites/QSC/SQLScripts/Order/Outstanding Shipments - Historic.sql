select COUNT(DISTINCT case when cod.IsShippedToAccount = 1 and b.OrderQualifierID = 39009 then bl.OrderID else b.OrderID end) OrderID --957
--select count(*) --
FROM		Batch b WITH (NOLOCK)
JOIN		CustomerOrderHeader coh WITH (NOLOCK)
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod WITH (NOLOCK)
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
left join shipment s WITH (NOLOCK) on s.id = cod.shipmentid and s.shipmentdate < '2017-11-19'
LEFT JOIN	Batch bL
				ON	bL.CampaignID = b.CampaignID
				AND	bL.OrderQualifierID = 39001
				AND	bL.StatusInstance NOT IN (40005)
				and bL.date < '2017-11-19'
where b.date between '2017-07-01' and '2017-11-19'
and cod.DistributionCenterID = 1
and s.id is null
and cod.DelFlag = 0
and cod.StatusInstance not in (500,501,506)
and b.statusinstance not in (40005)
and	cod.ProductType NOT IN (46023)
and (cod.IsShippedToAccount = 0 OR bL.OrderID IS NOT NULL)

select COUNT(DISTINCT case when cod.IsShippedToAccount = 1 and b.OrderQualifierID = 39009 then bl.OrderID else b.OrderID end) OrderID --1368
--select count(*) --
FROM		Batch b WITH (NOLOCK)
JOIN		CustomerOrderHeader coh WITH (NOLOCK)
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod WITH (NOLOCK)
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
left join shipment s WITH (NOLOCK) on s.id = cod.shipmentid and s.shipmentdate < '2016-11-19'
LEFT JOIN	Batch bL
				ON	bL.CampaignID = b.CampaignID
				AND	bL.OrderQualifierID = 39001
				AND	bL.StatusInstance NOT IN (40005)
				and bL.date < '2016-11-19'
where b.date between '2016-07-01' and '2016-11-19'
and cod.DistributionCenterID = 1
and s.id is null
and cod.DelFlag = 0
and cod.StatusInstance not in (500,501,506)
and b.statusinstance not in (40005)
and	cod.ProductType NOT IN (46023)
and (cod.IsShippedToAccount = 0 OR bL.OrderID IS NOT NULL)

select COUNT(DISTINCT case when cod.IsShippedToAccount = 1 and b.OrderQualifierID = 39009 then bl.OrderID else b.OrderID end) OrderID --208
--select count(*)
FROM		Batch b WITH (NOLOCK)
JOIN		CustomerOrderHeader coh WITH (NOLOCK)
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod WITH (NOLOCK)
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
left join shipment s WITH (NOLOCK) on s.id = cod.shipmentid and s.shipmentdate < '2015-11-19'
LEFT JOIN	Batch bL
				ON	bL.CampaignID = b.CampaignID
				AND	bL.OrderQualifierID = 39001
				AND	bL.StatusInstance NOT IN (40005)
				and bL.date < '2015-11-19'
where b.date between '2015-07-01' and '2015-11-19'
and cod.DistributionCenterID = 1
and s.id is null
and cod.DelFlag = 0
and cod.StatusInstance not in (500,501,506)
and b.statusinstance not in (40005)
and	cod.ProductType NOT IN (46023)
and (cod.IsShippedToAccount = 0 OR bL.OrderID IS NOT NULL)
