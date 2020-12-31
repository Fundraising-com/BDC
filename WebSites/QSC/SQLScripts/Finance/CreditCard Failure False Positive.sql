select *
from batch
where orderid = 10209750

select *
from qspcanadafinance..InvoiceGenerationLog
where OrderId = 10209750

select *
from QSPCanadaFinance..UDF_GetBillableOrdersFromBatch()
where OrderID = 10209750

SELECT	*
FROM	Batch b
JOIN	CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
join CustomerPaymentHeader cph on cph.CustomerOrderHeaderInstance = coh.Instance
join CreditCardPayment ccp on ccp.CustomerPaymentHeaderInstance = cph.Instance
WHERE	b.OrderID = 10209750

select * from CustomerOrderDetailRemitHistory where CustomerOrderHeaderInstance = 12323101

select top 999 *
from CustomerPaymentHeader cph
join CreditCardPayment ccp on ccp.CustomerPaymentHeaderInstance = cph.Instance
where cph.Instance between 5699919 and 5700099
order by cph.Instance

select *
from NonBatchCreditCardPayment
where CustomerPaymentHeaderInstance = 5699999

begin tran
update CustomerPaymentHeader
set TotalAmount = '27.00', DateChanged = '2013-10-01', UserIDChanged = 'JM', StatusInstance = 600
where CustomerOrderHeaderInstance = 12323101

update CreditCardPayment
set StatusInstance = 19000, ReasonCode=20100,AuthorizationDate = Convert(varchar(10),GetDate(),101)
where CustomerPaymentHeaderInstance = 5699999

update CustomerOrderHeader
set StatusInstance = 400
where Instance = 12323101