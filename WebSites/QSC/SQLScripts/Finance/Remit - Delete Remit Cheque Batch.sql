
select r.AP_Cheque_Remit_ID, r.AP_Cheque_ID, e.GL_ENTRY_ID, t.GL_TRANSACTION_ID, ch.AP_Cheque_Batch_ID
into #d
from AP_Cheque_Remit r 
join AP_Cheque ch on ch.AP_Cheque_ID = r.AP_Cheque_ID
join GL_ENTRY e on r.ap_cheque_remit_id = e.ap_cheque_remit_id
join GL_TRANSACTION t on t.GL_ENTRY_ID = e.GL_ENTRY_ID
join GLAccount a on a.glaccountid = t.glaccountid
where RemitBatchID = 1416
and r.AP_Cheque_Remit_ID <= 64819
order by r.AP_Cheque_Remit_ID

begin tran
delete GL_TRANSACTION
where GL_TRANSACTION_ID in (
select GL_TRANSACTION_ID
from #d)

delete GL_ENTRY
where GL_ENTRY_ID in (
select GL_ENTRY_ID
from #d)

delete ap_cheque
where AP_Cheque_ID in (
select AP_Cheque_ID
from #d)

delete AP_Cheque_Batch
where AP_Cheque_Batch_ID = 550

delete AP_Cheque_Remit
where AP_Cheque_Remit_ID in (
select AP_Cheque_Remit_ID
from #d)