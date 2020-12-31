update refund
set ap_cheque_id = null
where refund_type_id = 1
and createdate > '2009-05-07'
