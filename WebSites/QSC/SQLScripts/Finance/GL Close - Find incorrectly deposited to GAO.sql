--Not really needed, should just use GL Time Summary for GL account 100225 (check) and 172.113104.471 (CC), but use this to validate no profit checks were created
select c.id campaignID, e.payment_id, NULL AS adjustment_id, e.gl_entry_date, e.[description], p.payment_amount, NULL AS adjustment_amount, s.balance StatementAmount, p.order_id, i.invoice_date
from gl_entry e
join payment p on p.payment_id = e.payment_id
join qspcanadacommon..campaign c on c.id = p.campaign_id
join statement s on s.campaignid = c.id and s.statementrunid = 30
left join invoice i on i.order_id = p.order_id
where e.gl_entry_date between '2012-07-01' and '2012-10-01'
and e.businessunitID in (1,2)

/*UNION ALL

select c.id campaignID, e.payment_id, e.adjustment_id, e.gl_entry_date, e.[description], p.payment_amount, a.adjustment_amount, isnull(s.balance, s2.balance) StatementAmount
from gl_entry e
left join adjustment a on a.adjustment_id = e.adjustment_id
left join qspcanadacommon..campaign c2 on c2.id = a.campaign_id
left join statement s2 on s2.campaignid = c2.id and s2.statementrunid = 30
where e.gl_entry_date between '2012-10-01' and '2012-11-01'
and e.businessunitID in (1,2)
and ap_cheque_remit_id is null
and e.refund_id is null
and [description] <> 'cancel refund cheque'
and [description] <> 'Refund Cheque (Debit)'
and (s.refund_id is not null or s2.refund_id is not null)
*/