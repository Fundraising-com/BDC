--CC payments that weren't charged because an error with the order, when the type of error we should still charge the card
select	co.CustomerOrderID, co.ToteIDContract ToteID, c.LastName, c.FirstName, coar.Amount, coar.Expiration, coar.MaskedPaymentCardNumber, coar.PaymentCardNumberLastFour, coar.Created,
		f.fieldname Issue, coar.ARTransactionSourceTypeID, co.CustomerOrderStateID
from core.CustomerOrder co
left join core.Customer c on c.CustomerID = co.CustomerID
left join focus.CustomerOrderError e on e.customerorderid = co.CustomerOrderID
left join focus.Field f on f.FieldID = e.FieldID
left join focus.CustomerOrderErrorType t on t.CustomerOrderErrorTypeID = e.CustomerOrderErrorTypeID
join ar.CustomerEnvelopeCustomerOrder ceco on ceco.CustomerOrderID = co.CustomerOrderID
join ar.CustomerOrderAR coar on coar.CustomerEnvelopeID = ceco.CustomerEnvelopeID
where co.FormCode in ('0737','0745') --Canada Order Forms
and coar.CustomerOrderARStateID = 1 --Unauthorized CC

and co.CustomerOrderID NOT IN (SELECT	CustomerOrderID 
								FROM	CustomerOrderError coe
								WHERE	CustomerOrderErrorTypeID IN (19, 20) --Invalid CC, Declined CC
								OR		coe.FieldID IN (7, 14)) --Zip, Price
--and (e.customerordererrortypeid is null or (e.customerordererrortypeid not in (19, 20) and e.FieldID not in (14))) --Invalid CC, Declined CC
and co.CustomerOrderStateID in (38, 39) --Ready for Export, Exported
order by co.CustomerOrderID
