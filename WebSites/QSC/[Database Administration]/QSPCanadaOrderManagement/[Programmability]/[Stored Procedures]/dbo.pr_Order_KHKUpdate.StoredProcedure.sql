USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Order_KHKUpdate]    Script Date: 06/07/2017 09:20:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_Order_KHKUpdate]

as
/*
update	b
set		statusinstance = 40014
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
where cod.producttype in (46012)
and cod.statusinstance in (510)
and cod.creationdate > '2011-07-01'
and b.orderid in (
	SELECT	OrderID
	FROM	Batch b2
	JOIN	CustomerOrderHeader coh2
				ON	coh2.OrderBatchID = b2.ID
				AND	coh2.OrderBatchDate = b2.Date
	JOIN	CustomerOrderDetail cod2
				ON	cod2.CustomerOrderHeaderInstance = coh2.Instance
	WHERE	b2.OrderID = b.OrderID
	AND		cod2.DistributionCenterID = 2
	AND		cod2.DelFlag <> 1
	AND		cod2.StatusInstance NOT IN (506, 508) --506: Order Detail Voided Due To Error, 508: Order Detail Shipped
)

update	b
set		statusinstance = 40013
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
where cod.producttype in (46012)
and cod.statusinstance in (510)
and cod.creationdate > '2011-07-01'
and b.orderid NOT IN (
	SELECT	OrderID
	FROM	Batch b2
	JOIN	CustomerOrderHeader coh2
				ON	coh2.OrderBatchID = b2.ID
				AND	coh2.OrderBatchDate = b2.Date
	JOIN	CustomerOrderDetail cod2
				ON	cod2.CustomerOrderHeaderInstance = coh2.Instance
	WHERE	b2.OrderID = b.OrderID
	AND		cod2.DistributionCenterID = 2
	AND		cod2.DelFlag <> 1
	AND		cod2.StatusInstance NOT IN (506, 508) --506: Order Detail Voided Due To Error, 508: Order Detail Shipped
)
*/
update customerorderdetail
set statusinstance = 508,
QuantityShipped = Quantity
from customerorderdetail
where producttype in (46012)
and statusinstance in (502,510)
and creationdate > '2011-07-01'
GO
