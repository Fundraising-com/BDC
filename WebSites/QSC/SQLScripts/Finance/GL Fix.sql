use qspcanadafinance

select *
from gl_entry e
join gl_transaction t on t.gl_entry_id = e.gl_entry_id
join glaccount a on a.glaccountid = t.glaccountid
where e.invoice_id = 1078940

SELECT cust.state CustomerProvince, ad.stateprovince AccountProvince,	cod.*, cust.*, *
FROM	qspcanadaordermanagement..Batch b
JOIN	qspcanadaordermanagement..CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	qspcanadaordermanagement..CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN	qspcanadaordermanagement..Customer cust
			ON	cust.Instance =	CASE ISNULL(cod.CustomerShipToInstance, 0)
									WHEN 0 THEN coh.CustomerBillToInstance
									ELSE		cod.CustomerShipToInstance
								END
join	QSPCanadaCommon..CAccount acc ON acc.ID = b.AccountID
join	QSPCanadaCommon..address ad on ad.addresslistid = acc.addresslistid and ad.address_type = 54001
where invoicenumber = 1078940

/*select *
from invoice_by_qsp_product
where invoice_id = 1078871*/

/*select * from glaccount
where glaccountsystemid = 2
and description like '%pst%'*/

begin tran
insert gl_transaction values (4742328, 'C', 3.57, 2, 154)

--Gift Account Earnings Increase
update gl_transaction
set amount = amount + 3.57
where gl_transaction_id = 12328440

--commit tran

EXEC [dbo].[GL_Validate_EarliestOpenPeriod]
