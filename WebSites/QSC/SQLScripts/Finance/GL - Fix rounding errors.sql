USE QSPCanadaFinance

select i.ORDER_ID, * from gl_entry e
join gl_transaction t on t.gl_entry_id = e.gl_entry_id
join glaccount a on a.glaccountid = t.glaccountid
left join INVOICE i on i.INVOICE_ID = e.INVOICE_ID
left join QSPCanadaOrderManagement..Batch b on b.OrderID = i.ORDER_ID
where e.INVOICE_ID = 1089701


begin tran
update gl_transaction
set amount = '86.10'
where gl_transaction_id = 7432729

commit tran

EXEC [dbo].[GL_Validate_EarliestOpenPeriod]

/* Invoices with only processing fees
insert GL_TRANSACTION values (4016435, 'D', 1.00, 2, 137)

begin tran
SELECT	cod.*, *
FROM	qspcanadaordermanagement..Batch b
JOIN	qspcanadaordermanagement..CustomerOrderHeader coh
			ON	coh.OrderBatchID = b.ID
			AND	coh.OrderBatchDate = b.Date
JOIN	qspcanadaordermanagement..CustomerOrderDetail cod
			ON	cod.CustomerOrderHeaderInstance = coh.Instance
WHERE	b.orderid = 13194384

update qspcanadaordermanagement..batch
set statusinstance = 40005
where orderid = 12621829
--commit tran
--Then delete the invoice
*/

/* Invoices with QC customer, non-QC school
--update GL_TRANSACTION set amount = 7.82, glaccountid = 155
--where gl_transaction_id = 9924096 --Tax

insert GL_TRANSACTION values (4183886, 'C', 1.82, 2, 155)
update GL_TRANSACTION set amount = 35.90
where gl_transaction_id = 10461151 --AE
*/