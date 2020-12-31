--Number of successful CC transactions
select COUNT(*), SUM(payment_amount)
from qspcanadafinance..PAYMENT
where PAYMENT_METHOD_ID in (50003,50004)
and PAYMENT_EFFECTIVE_DATE >= '2014-01-01'
and PAYMENT_EFFECTIVE_DATE <= '2015-02-05'
and order_id not in (8659994,8694961,8713497,8661357,8708169)

--Details by channel of successfully charge
select COUNT(*), SUM(cph.TotalAmount)
from CreditCardPayment ccp
join CustomerPaymentHeader cph on cph.instance = ccp.customerpaymentheaderinstance
join CustomerOrderHeader coh on coh.Instance = cph.CustomerOrderHeaderInstance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
where ccp.DateCreated >= '2014-01-01'
and ccp.DateCreated <= '2015-02-05'
and cph.StatusInstance = 600
and ccp.StatusInstance = 19000
--and ISNULL(ccp.BatchID,0) > 0
and b.OrderQualifierID IN (39001, 39002)
and b.orderid not in (8659994,8694961,8713497,8661357,8708169)

--Details by channel of unsuccessfully charged that Paypal rejected
select (SELECT SUM(cod.price) FROM CustomerOrderDetail cod WHERE CustomerOrderHeaderInstance = coh.Instance) AS TotalAmount
from CreditCardPayment ccp
join CustomerPaymentHeader cph on cph.instance = ccp.customerpaymentheaderinstance
join CustomerOrderHeader coh on coh.Instance = cph.CustomerOrderHeaderInstance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
where ccp.DateCreated >= '2014-01-01'
and ccp.DateCreated <= '2015-02-05'
and (cph.StatusInstance <> 600
or ccp.StatusInstance <> 19000)
--and ISNULL(ccp.BatchID,0) > 0
and b.OrderQualifierID IN (39001, 39002)
and coh.PaymentMethodInstance <> 50005 -- D+H didn't pass the CC as it failed validation
and b.orderid not in (8659994,8694961,8713497,8661357,8708169)

--Details by channel of unsuccessfully charged that Focus rejected
select (SELECT SUM(cod.price) FROM CustomerOrderDetail cod WHERE CustomerOrderHeaderInstance = coh.Instance)
from CreditCardPayment ccp
join CustomerPaymentHeader cph on cph.instance = ccp.customerpaymentheaderinstance
join CustomerOrderHeader coh on coh.Instance = cph.CustomerOrderHeaderInstance
join Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
where ccp.DateCreated >= '2014-01-01'
and ccp.DateCreated <= '2015-02-05'
and (cph.StatusInstance <> 600
or ccp.StatusInstance <> 19000)
--and ISNULL(ccp.BatchID,0) > 0
and b.OrderQualifierID IN (39001, 39002)
and coh.PaymentMethodInstance = 50005 -- D+H didn't pass the CC as it failed validation
and b.orderid not in (8659994,8694961,8713497,8661357,8708169)

--Landed CC’s that were reprocessed
SELECT		DISTINCT coh.Instance, SUM(cod.Price)
from		CreditCardPayment ccp
join		CustomerPaymentHeader cph on cph.instance = ccp.customerpaymentheaderinstance
join		CustomerOrderHeader coh on coh.Instance = cph.CustomerOrderHeaderInstance
join		Batch b on b.ID = coh.OrderBatchID and b.Date = coh.OrderBatchDate
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		Incident inc
				ON	inc.CustomerOrderHeaderInstance = coh.Instance
				AND	inc.TransID = cod.TransID		
JOIN 		IncidentAction incAct 
				ON	incAct.IncidentInstance = inc.IncidentInstance
where		ccp.DateCreated >= '2014-01-01'
and			ccp.DateCreated <= '2015-02-05'
AND			incAct.ActionInstance IN (18)
and b.orderid not in (8659994,8694961,8713497,8661357,8708169)
GROUP BY	coh.Instance
order by coh.Instance