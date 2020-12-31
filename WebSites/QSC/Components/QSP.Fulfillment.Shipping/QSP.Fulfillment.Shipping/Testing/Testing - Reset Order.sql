use [QSPCanadaOrderManagement]

declare @OrderID int
set @OrderID = 704463 --nonbhe
--set @OrderID = 9582160 --bhe
--set @OrderID = 804476 --Magexpress TQ
--set @OrderID = 802546 --Mag Express / Gift with TQ
--set @OrderID = 9716068 --FS

declare @ReportRequired INT
set @ReportRequired = 1 --1: No report needed, 0: Report needed

SELECT b.OrderQualifierID, cod.ProductType, b.StatusInstance, cod.StatusInstance, sel.ProcName, sel.Desc1, sel.IsFixed, rrb.IsQSPPrint, b.DateCreated
from customerorderdetail cod
join customerorderheader coh
	on coh.instance = cod.customerorderheaderinstance
join batch b on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
left join QSPCanadaCommon..SystemErrorLog sel
	on sel.OrderID = b.OrderID
join reportrequestbatch rrb
	on rrb.BatchOrderID = b.OrderID
where b.orderID = @OrderID
and cod.distributioncenterid = 2

update b
set statusinstance = 40010,
	datecreated = GETDATE()
from batch b
--JOIN		BatchDistributionCenter bdc
--				ON	bdc.BatchDate = b.Date
--				AND	bdc.BatchID = b.ID
--				AND	bdc.DistributionCenterID = 2 --2: Unigistix
where orderID = @OrderID

update cod
set statusinstance = 509
from customerorderdetail cod
join customerorderheader coh
	on coh.instance = cod.customerorderheaderinstance
join batch b on b.id = coh.orderbatchid and b.date = coh.orderbatchdate
JOIN		BatchDistributionCenter bdc
				ON	bdc.BatchDate = b.Date
				AND	bdc.BatchID = b.ID
				AND	bdc.DistributionCenterID = 2 --2: Unigistix
where b.orderID = @OrderID
and cod.distributioncenterid = 2

delete QSPCanadaCommon..SystemErrorLog
where OrderID = @OrderID

update rrb
set isqspprint = @ReportRequired
from reportrequestbatch rrb
join batch b
	ON	rrb.BatchOrderID = b.OrderID
where b.orderID = @OrderID
