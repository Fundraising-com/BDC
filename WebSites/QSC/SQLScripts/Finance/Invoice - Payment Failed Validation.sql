select *
FROM		Batch b
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
where OrderID = 915616	

select CheckPayableToQSPAmount,*
from batch
where orderid = 915616

select *
from qspcanadafinance..payment
where order_id = 915616

select top 100 *
from QSPCanadaFinance..ChequePaymentLog
order by LogId desc
