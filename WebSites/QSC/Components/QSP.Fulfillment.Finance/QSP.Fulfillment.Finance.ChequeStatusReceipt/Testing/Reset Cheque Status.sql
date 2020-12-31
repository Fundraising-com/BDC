select top 13000 * from AP_Cheque
select top 100 * from ap_cheque_batch
select top 10 * from AP_Cheque_StatusReceipt order by ap_cheque_statusReceipt_ID desc
select * from AP_Cheque_StatusReceipt_Batch
select * from AP_Cheque_StatusReceipt_Batch
select * from AP_Cheque_StatusReceipt order by ap_cheque_statusreceipt_id
select * from AP_Cheque_StatusReceipt_Type
select * from AP_Cheque_StatusReceipt_Status
select * from ap_cheque_status
select * from refund where customerorderheaderinstance = 9941347
select * from ap_cheque where ap_cheque_id in (42757,42756)
select * from ap_cheque where chequenumber = 7000054618 
select * from refund where ap_cheque_ID in (42757,42756)
select * from ap_cheque where ap_cheque_status_id <> 1

delete from AP_Cheque_StatusReceipt
delete from AP_Cheque_StatusReceipt_Batch

update ap_cheque_statusreceipt
set ap_cheque_id = 42787
where ap_cheque_statusreceipt_id = 42677

insert ap_cheque_statusreceipt_status values ('Cheque Status Receipt File for Cheque was already processed')
insert ap_cheque_statusreceipt_type_id values (0, 'Nonexistant')

update ap_cheque_statusreceipt_type
set description = 'Unknown'
where ap_cheque_statusReceipt_Type_ID = 0

insert ap_cheque_status values ('Unknown')
insert ap_cheque_status values ('Outstanding')
insert ap_cheque_status values ('Paid')
insert ap_cheque_status values ('Voided')
insert ap_cheque_status values ('Stopped')

update ap_cheque set ap_cheque_status = 1

select customerorderheaderinstance, transid, count(*) from refund
group by customerorderheaderinstance, transid
having count(*) > 1
order by customerorderheaderinstance desc

select top 100 *
from gl_entry e
left join gl_transaction t on t.gl_entry_id = e.gl_entry_id
order by e.gl_entry_id desc


select top 100 *
from gl_transaction
order by gl_entry_id desc

select * from ap_cheque
order by ap_cheque_id desc