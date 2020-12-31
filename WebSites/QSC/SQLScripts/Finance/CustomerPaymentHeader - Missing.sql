--Need to make sure payment.payment_method_id gets set correctly

select top 999 *
from customerorderheader coh
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
left join customerpaymentheader cph on cph.CustomerOrderHeaderInstance = coh.Instance
left join CreditCardPayment ccp on ccp.CustomerPaymentHeaderInstance = cph.Instance
where cph.Instance is null
and b.OrderQualifierID in (39009)
order by coh.Instance desc

select top 9999 *
from customerorderheader coh
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
left join customerpaymentheader cph on cph.CustomerOrderHeaderInstance = coh.Instance
left join CreditCardPayment ccp on ccp.CustomerPaymentHeaderInstance = cph.Instance
where ccp.CustomerPaymentHeaderInstance is not null
and b.OrderQualifierID in (39009)
order by coh.Instance desc

select MAX(instance)
from CustomerPaymentHeader

--Set to 600 for CC Reprocess to Invoice
begin tran
insert CustomerPaymentHeader
select ROW_NUMBER() OVER(ORDER BY coh.Instance) + 105782672, coh.Instance, 0, '2013-10-17', 4999, 1, 0, 0.00, GETDATE(), 'JM', GETDATE(), 'JM', 601, 1, NULL
from customerorderheader coh
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
left join customerpaymentheader cph on cph.CustomerOrderHeaderInstance = coh.Instance
left join CreditCardPayment ccp on ccp.CustomerPaymentHeaderInstance = cph.Instance
where cph.Instance is null
and b.OrderQualifierID in (39009)
order by coh.Instance

--Must also update PaymentNext table so future records don't collide
DBCC CHECKIDENT ('PaymentNext', NORESEED)
DBCC CHECKIDENT ('PaymentNext', RESEED, 5783439)

select top 999 *
from CustomerPaymentHeader
order by Instance desc

--Set to 19000 for CC Reprocess to Invoice
begin tran
insert CreditCardPayment
select cph.Instance,'0000000000000000', '0000', 0, NULL, NULL, '1995-01-01 00:00:00.000', NULL, 19001, GETDATE(), 'JM', GETDATE(), NULL, 0, '1'
from customerorderheader coh
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
join customerpaymentheader cph on cph.CustomerOrderHeaderInstance = coh.Instance
left join CreditCardPayment ccp on ccp.CustomerPaymentHeaderInstance = cph.Instance
where ccp.CustomerPaymentHeaderInstance is null
and b.OrderQualifierID in (39009)
order by cph.Instance

select top 999 *
from CreditCardPayment
order by CustomerPaymentHeaderInstance desc

select top 9999 *
from qspcanadafinance..PAYMENT
order by PAYMENT_ID desc
