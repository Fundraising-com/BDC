select apc.chequenumber, apc.creationdate, b.bank_account_number, b.bank_account_description, b.country_code, isnull(r.Amount, (apcr.NetAmount+apcr.GSTAmount+apcr.HSTAmount+PSTAmount)) Amount
from ap_cheque apc
join bank_account b on b.bank_account_id = apc.bank_account_id
left join ap_cheque_remit apcr on apcr.ap_cheque_id = apc.ap_cheque_id
left join refund r on r.ap_cheque_id = apc.ap_cheque_id
where apc.creationdate > '2011-01-01'
order by apc.creationdate
