USE [QSPCanadaOrderManagement]
GO

SELECT		*
INTO		#crh
FROM		CustomerRemitHistory crh
WHERE		crh.Instance IN (
10178881,
10178882,
10178883,
10178884,
10178885,
10178886,
10178887,
10178888,
10178889,
10178890,
10178891,
10178892,
10178893,
10178894,
10178895,
10178896,
10178897,
10178898,
10178899,
10178900,
10178901,
10178902,
10178903,
10178904,
10178905,
10178906,
10178907,
10178908,
10178909,
10178910,
10178911,
10178912,
10178913,
10178914,
10178916,
10178917

)


SELECT		cod.CustomerOrderHeaderInstance, cod.TransID, *
--INTO		#COD
FROM		#crh crh
JOIN		QSPCanadaOrderManagement..Customer c	ON c.Instance = crh.CustomerInstance 
left join	(QSPCanadaOrderManagement..CustomerOrderDetail cod
			join QSPCanadaOrderManagement..customerorderheader coh on coh.Instance = cod.CustomerOrderHeaderInstance
			join QSPCanadaOrderManagement..batch b on b.id = coh.orderbatchid and b.date = coh.orderbatchdate)
			ON c.Instance = CASE ISNULL(cod.CustomerShipToInstance, 0)
								WHEN 0 THEN coh.CustomerBillToInstance
								ELSE		cod.CustomerShipToInstance
							END
			AND cod.ProductType = 46001
			and cod.CreationDate >= DATEADD(dd, -4, GETDATE()) 
			--and cod.StatusInstance = 507
left join	QSPCanadaOrderManagement..customerorderdetailremithistory codrh 
				on	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance 
				and	codrh.TransID = cod.TransID
left join	QSPCanadaOrderManagement..customerremithistory crh2 on crh2.instance = codrh.customerremithistoryinstance
WHERE		codrh.CustomerOrderHeaderInstance IS NULL
AND			cod.CustomerOrderHeaderInstance IS NOT NULL

BEGIN TRAN

DECLARE @COHInstance	INT,
		@TransID		INT

DECLARE cod cursor for 
SELECT	CustomerOrderHeaderInstance,
		TransID
FROM	#COD

OPEN cod
FETCH NEXT FROM cod INTO @COHInstance, @TransID

WHILE @@fetch_status = 0
BEGIN

	UPDATE	cod
	SET		StatusInstance = 502
	FROM	CustomerOrderDetail cod
	WHERE	CustomerOrderHeaderInstance = @COHInstance
	AND		TransID = @TransID

	DECLARE @RemitStatus INT
	EXEC QSPCanadaOrderManagement..spRemitIndividualItem @COHInstance, @TransID, @RemitStatus OUTPUT

	FETCH NEXT FROM cod INTO @COHInstance, @TransID
END

CLOSE cod
DEALLOCATE cod
begin tran
delete crh
from #crh c
join customerremithistory crh on crh.instance = c.instance

DROP TABLE #CRH
DROP TABLE #COD

commit

select top 100 * from customerorderdetailremithistory order by customerremithistoryinstance desc

select * from #cod c
join customerorderdetail cod on cod.customerorderheaderinstance = c.customerorderheaderinstance and cod.transid = c.transid
left join customerorderdetailremithistory h on h.customerorderheaderinstance = c.customerorderheaderinstance and h.transid = c.transid

select *
from customerorderdetail cod
join customerorderheader coh on coh.instance = cod.customerorderheaderinstance
/*JOIN		Customer cust
				ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
										WHEN 0 THEN coh.CustomerBillToInstance
										ELSE		cod.CustomerShipToInstance
									END*/
left join customerorderdetailremithistory codrh 
			on	codrh.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance 
			and	codrh.TransID = cod.TransID
where	codrh.CustomerOrderHeaderInstance IS NULL
and		cod.StatusInstance = 507
and		cod.CreationDate > '2012-07-01'

------------------- Missing customerremithistory records

SELECT		*
INTO		#codrh
FROM		CustomerOrderDetailRemitHistory codrh
WHERE		codrh.CustomerOrderHeaderInstance IN (
11739299,
11739300
)
and transid = 1

SELECT		cod.CustomerOrderHeaderInstance, cod.TransID
INTO		#COD
FROM		#codrh codrh
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod ON cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance AND cod.TransID = codrh.TransID

BEGIN TRAN

DECLARE @COHInstance	INT,
		@TransID		INT

DECLARE cod cursor for 
SELECT	CustomerOrderHeaderInstance,
		TransID
FROM	#COD

OPEN cod
FETCH NEXT FROM cod INTO @COHInstance, @TransID

WHILE @@fetch_status = 0
BEGIN

	UPDATE	cod
	SET		StatusInstance = 502
	FROM	CustomerOrderDetail cod
	WHERE	CustomerOrderHeaderInstance = @COHInstance
	AND		TransID = @TransID

	DECLARE @RemitStatus INT
	EXEC QSPCanadaOrderManagement..spRemitIndividualItem @COHInstance, @TransID, @RemitStatus OUTPUT

	FETCH NEXT FROM cod INTO @COHInstance, @TransID
END

CLOSE cod
DEALLOCATE cod

delete codrh
from #codrh c
join customerorderdetailremithistory codrh on codrh.CustomerOrderHeaderInstance = c.CustomerOrderHeaderInstance and codrh.TransID = c.TransID

DROP TABLE #CRH
DROP TABLE #COD

commit