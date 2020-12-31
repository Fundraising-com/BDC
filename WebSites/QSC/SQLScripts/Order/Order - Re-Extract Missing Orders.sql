USE GA
GO

--Look at CustomerOrder
SELECT DISTINCT 0 as sortOrder, MAX(s.SQLID) as SQLID, co.CustomerOrderID, co.ToteIDContract       --online order
FROM Core.SalesOrder so with(nolock)
INNER JOIN Core.SAPSQLID s with(nolock) ON s.SourceID = so.SalesOrderID AND s.SQLIDTypeID = 4 /* SalesOrder */
INNER JOIN Core.SalesOrderDetail sod with(nolock) ON sod.SalesOrderID = so.SalesOrderID --AND so.SAPTransmittedDate IS NULL AND so.ArrivalTypeID in (93, 94, 97, 70, 71, 72, 73)
INNER JOIN Core.CustomerOrderDetail cod with(nolock) ON cod.SalesOrderDetailID = sod.SalesOrderDetailID
INNER JOIN Core.CustomerSubOrder  cso with(nolock) ON cso.CustomerSubOrderID = cod.CustomerSubOrderID
INNER JOIN Core.CustomerOrder co with(nolock) ON co.CustomerOrderID = cso.CustomerOrderID  and co.CustomerOrderStateID = 38 /* ReadyToExportFFS */
WHERE co.CustomerOrderID = 77290815
GROUP BY co.CustomerOrderID, co.ToteIDContract

--Check if batch partially exported to FFS
--Run on gasqlp02
SELECT	*
FROM	Batch
WHERE	OrderID = <sqlid>

--Find Unused Batch.OrderID
--Run on gasqlp02
SELECT	OrderID, *
FROM	Batch
WHERE	OrderID BETWEEN 7897900 AND 7897999
order by OrderID

--Set View CustomerOrdersReadyForCanadaExport to only that particular SQLID in where clause, remove AND so.SAPTransmittedDate IS NULL from the where clause, and in select clause
--put the new OrderID as the SQLID

--Check it worked
SELECT	*
FROM	Integration.OrderMapping
WHERE	CustomerOrderID = 77290815

SELECT	TOP 99 *
FROM	Integration.ETLLog
ORDER BY ETLLogID desc

----------

--Look at batch
SELECT DISTINCT 0 as sortOrder, MAX(s.SQLID) as SQLID, co.CustomerOrderID, co.ToteIDContract       --online order
FROM Core.SalesOrder so with(nolock)
INNER JOIN Core.SAPSQLID s with(nolock) ON s.SourceID = so.SalesOrderID AND s.SQLIDTypeID = 4 /* SalesOrder */
INNER JOIN Core.SalesOrderDetail sod with(nolock) ON sod.SalesOrderID = so.SalesOrderID --AND so.SAPTransmittedDate IS NULL AND so.ArrivalTypeID in (93, 94, 97, 70, 71, 72, 73)
INNER JOIN Core.CustomerOrderDetail cod with(nolock) ON cod.SalesOrderDetailID = sod.SalesOrderDetailID
INNER JOIN Core.CustomerSubOrder  cso with(nolock) ON cso.CustomerSubOrderID = cod.CustomerSubOrderID
INNER JOIN Core.CustomerOrder co with(nolock) ON co.CustomerOrderID = cso.CustomerOrderID  and co.CustomerOrderStateID = 38 /* ReadyToExportFFS */
WHERE s.SQLID = 7897922
GROUP BY co.CustomerOrderID, co.ToteIDContract

--Look at CustomerOrder details
SELECT s.SQLID as SQLID, co.CustomerOrderID, co.ToteIDContract       --online order
FROM Core.SalesOrder so with(nolock)
INNER JOIN Core.SAPSQLID s with(nolock) ON s.SourceID = so.SalesOrderID AND s.SQLIDTypeID = 4 /* SalesOrder */
INNER JOIN Core.SalesOrderDetail sod with(nolock) ON sod.SalesOrderID = so.SalesOrderID --AND so.SAPTransmittedDate IS NULL AND so.ArrivalTypeID in (93, 94, 97, 70, 71, 72, 73)
INNER JOIN Core.CustomerOrderDetail cod with(nolock) ON cod.SalesOrderDetailID = sod.SalesOrderDetailID
INNER JOIN Core.CustomerSubOrder  cso with(nolock) ON cso.CustomerSubOrderID = cod.CustomerSubOrderID
INNER JOIN Core.CustomerOrder co with(nolock) ON co.CustomerOrderID = cso.CustomerOrderID  and co.CustomerOrderStateID = 38 /* ReadyToExportFFS */
WHERE s.SQLID = 7897922
