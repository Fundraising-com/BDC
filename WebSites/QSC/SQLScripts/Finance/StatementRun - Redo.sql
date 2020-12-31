USE QSPCanadaFinance
GO

select *
from statementrun

begin tran

update [statement]
set refund_id = null
where refund_id is not null
and statementrunid = 24

select refund_id, ap_cheque_id
into #ref
from refund
where refund_type_id = 2
and createdate > '2012-03-19'
and createdate < '2012-03-20'

delete r
from refund r
where r.refund_id between 1058395 and 1058934
--join #ref ref on ref.refund_id = r.refund_id

delete apc
from ap_cheque apc
join #ref ref on ref.ap_cheque_id = apc.ap_cheque_id

delete glt
from gl_transaction glt
join gl_entry gle on gle.gl_entry_id = glt.gl_entry_id
join adjustment adj on adj.adjustment_id = gle.adjustment_id
where adj.adjustment_type_id = 49024
and adj.date_created between '2012-03-19' and '2012-03-20'

delete gle
from gl_entry gle
join adjustment adj on adj.adjustment_id = gle.adjustment_id
where adj.adjustment_type_id = 49024
and adj.date_created between '2012-03-19' and '2012-03-20'

delete adjustment
where adjustment_type_id = 49024
and date_created between '2012-03-19' and '2012-03-20'

insert statementrun values ('2012-03-26', 0, 0)