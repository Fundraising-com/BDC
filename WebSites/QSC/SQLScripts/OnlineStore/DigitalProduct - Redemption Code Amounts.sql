select dp.DigitalProductID, dp.DigitalProductName, 
CASE
	WHEN dpc.CustomerOrderDetailID IS NULL THEN 0 ELSE 1 END as Redeemed,
COUNT(*) 
from Store.Digitalproduct dp 
join Store.DigitalProductCode dpc on dpc.Digitalproductid = dp.DigitalProductID
join Core.CustomerOrderDetail cod on cod.CustomerOrderDetailID = dpc.CustomerOrderDetailID
where dp.DigitalProductID in (1,2)
and dpc.Created > '01-01-2016'
group by dp.DigitalProductID, dp.DigitalProductName, CASE
	WHEN dpc.CustomerOrderDetailID IS NULL THEN 0 ELSE 1 END


SET TRANSACTION ISOLATION LEVEL SNAPSHOT

select dp.DigitalProductID, dp.DigitalProductName, COUNT(*) 
from Store.Digitalproduct dp 
join Store.DigitalProductCode dpc on dpc.Digitalproductid = dp.DigitalProductID
join Core.CustomerOrderDetail cod on cod.CustomerOrderDetailID = dpc.CustomerOrderDetailID
where dp.DigitalProductID in (1,2)
and cod.Created > '01-01-2016'
group by dp.DigitalProductID, dp.DigitalProductName


select * 
from Store.Digitalproduct dp 
join Store.DigitalProductCode dpc on dpc.Digitalproductid = dp.DigitalProductID
where dp.DigitalProductID in (1,2)
AND dpc.CustomerOrderDetailID IS NULL 




