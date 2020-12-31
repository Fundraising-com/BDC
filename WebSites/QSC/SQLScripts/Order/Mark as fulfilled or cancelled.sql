SELECT	b.orderid, b.orderqualifierid, b.statusinstance, b.isinvoiced, 40005
FROM	Batch b
WHERE	b.OrderID NOT IN (
			SELECT	b2.OrderID
			FROM	Batch b2
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b2.ID
						AND	coh.OrderBatchDate = b2.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	cod.statusinstance in (501, 507, 508, 509, 510, 511, 512, 513))
and b.statusinstance IN (40010, 40014)
and date >= '2016-07-01'
and date < '2017-01-11'
order by b.orderid desc

begin tran
update b
set statusinstance = 40005
FROM	Batch b
WHERE	b.OrderID NOT IN (
			SELECT	b2.OrderID
			FROM	Batch b2
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b2.ID
						AND	coh.OrderBatchDate = b2.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	cod.statusinstance in (501, 507, 508, 509, 510, 511, 512, 513))
and b.statusinstance IN (40010, 40014)
and date >= '2016-07-01'
and date < '2017-01-11'
--commit tran

SELECT	b.orderid, b.orderqualifierid, b.statusinstance, b.isinvoiced, 40013
FROM	Batch b
WHERE	b.OrderID IN ( --
			SELECT	b2.OrderID
			FROM	Batch b2
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b2.ID
						AND	coh.OrderBatchDate = b2.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	cod.statusinstance in (501, 507, 508, 512)
)
and		b.OrderID NOT IN (
			SELECT	b3.OrderID
			FROM	Batch b3
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b3.ID
						AND	coh.OrderBatchDate = b3.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	((b3.OrderQualifierID NOT IN (39009) OR cod.IsShippedToAccount = 0)
			OR		(cod.IsShippedToAccount = 1 AND b3.OrderID IN (SELECT DISTINCT OnlineOrderID  
													 FROM OnlineOrderMappingTable)))
			AND	cod.statusinstance in (509, 510, 511))
and b.statusinstance IN (40010, 40014)
and date >= '2016-07-01'
and date < '2017-01-11'
order by b.orderid desc

begin tran
update b
set statusinstance = 40013
FROM	Batch b
WHERE	b.OrderID IN ( --
			SELECT	b2.OrderID
			FROM	Batch b2
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b2.ID
						AND	coh.OrderBatchDate = b2.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	cod.statusinstance in (501, 507, 508, 512)
)
and		b.OrderID NOT IN (
			SELECT	b3.OrderID
			FROM	Batch b3
			JOIN	CustomerOrderHeader coh
						ON	coh.OrderBatchID = b3.ID
						AND	coh.OrderBatchDate = b3.Date
			JOIN	CustomerOrderDetail cod
						ON	cod.CustomerOrderHeaderInstance = coh.Instance
			WHERE	((b3.OrderQualifierID NOT IN (39009) OR cod.IsShippedToAccount = 0)
			OR		(cod.IsShippedToAccount = 1 AND b3.OrderID IN (SELECT DISTINCT OnlineOrderID  
													 FROM OnlineOrderMappingTable)))
			AND	cod.statusinstance in (509, 510, 511))
and b.statusinstance IN (40010, 40014)
and date >= '2016-07-01'
and date < '2017-01-11'
--commit tran

----------
SELECT b.orderid, b.orderqualifierid, b.statusinstance, b.isinvoiced, cod.invoicenumber, cod.isshippedtoaccount, cod.statusinstance,	cod.*, *
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.statusinstance in (40010)
and date >= '2016-07-01'
and date < '2017-01-11'
and cod.statusinstance in (507, 510, 511, 512, 509, 508, 501)
order by cod.statusinstance desc

begin tran
update batch
set statusinstance = 40013
where orderid in (11607965, 11587899, 11579045, 11460510, 11713892, 11643344)

update b
set statusinstance = 40013
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.statusinstance in (40010)
and date >= '2016-07-01'
and date < '2017-01-11'
and cod.statusinstance in (507, 501)

update batch
set statusinstance = 40005
where orderid in (11690531, 11604275, 11491460, 11487384, 11468944, 11465881, 11456301, 11448167, 11427013)
--commit tran