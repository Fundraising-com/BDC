select coh.campaignid,b.orderqualifierID, b.orderid,s.FirstName, s.Lastname, * from customerorderdetail cod join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
join batch b on b.id = coh.orderbatchid and b.date = orderbatchdate
join student s on s.instance = coh.studentinstance
where coh.campaignid in (53002, 53022)
order by coh.campaignid, s.Lastname

select * from OnlineOrderMappingTable where landedorderid = 800430

BEGIN TRAN t1

INSERT INTO OnlineOrderMappingTable
SELECT	800430,
		b.OrderID,
		cod.CustomerOrderHeaderInstance,
		cod.TransID,
		coh.StudentInstance
FROM	Batch b
JOIN	CustomerorderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.[Date]
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	coh.CampaignID = 53022

COMMIT TRAN t1