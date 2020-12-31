select *
from statement s
join refund r on r.refund_id = s.refund_id
join ap_cheque apc on apc.ap_cheque_id = r.ap_cheque_id
left join ap_cheque_batch b on b.ap_cheque_batch_id = apc.ap_cheque_batch_id
where apc.ap_cheque_batch_id is null
and statementrunid is null
and apc.chequenumber > 0 --exclude cheques that were forced and sent through GAO
order by statementid desc