SELECT	cod.*, b.*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
--WHERE	coh.Instance = 12675670
WHERE	b.orderid = 1083636
order by cod.statusinstance

select *
from batch
where orderid = 912681	

begin tran

update customerorderdetail
set delflag = 1
where customerorderheaderinstance in (12675670)
and transid = 1

update qspcanadacommon..systemerrorlog
set IsReviewed = 1, IsFixed = 1
where LogID = 9250

update batch
set statusinstance = 40013
where orderid = 912681

commit tran

--Requeue reports (mark reports as printed if not desired to reprint)