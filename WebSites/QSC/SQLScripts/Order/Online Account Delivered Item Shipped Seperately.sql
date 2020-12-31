SELECT	cod.*, *
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
LEFT JOIN BatchDistributionCenter bdc
			ON	bdc.BatchID = b.ID
			AND	bdc.BatchDate = b.Date
WHERE	b.orderid = 12090998

begin tran

update CustomerOrderDetail
set IsShippedToAccount = 0
where CustomerOrderHeaderInstance = 13684479
and TransID = 2

exec pr_Insert_BatchDistributionCenter 12090998

--commit tran

--Requeue batch reports
