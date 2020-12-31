select * from StatementRun

select isnull(c.id, c2.id) campaignID, e.payment_id, e.adjustment_id, e.gl_entry_date, e.[description], p.payment_amount, a.adjustment_amount, isnull(s.balance, s2.balance) StatementAmount
from gl_entry e
left join payment p on p.payment_id = e.payment_id
left join qspcanadacommon..campaign c on c.id = p.campaign_id
left join statement s on s.campaignid = c.id and s.statementrunid = 34
left join adjustment a on a.adjustment_id = e.adjustment_id
left join qspcanadacommon..campaign c2 on c2.id = a.campaign_id
left join statement s2 on s2.campaignid = c2.id and s2.statementrunid = 34
where e.gl_entry_date between '2013-02-01' and '2013-03-01'
and e.businessunitID in (1,2)
and ap_cheque_remit_id is null
and e.refund_id is null
and [description] <> 'cancel refund cheque'
and [description] <> 'Refund Cheque (Debit)'
and (s.refund_id is not null or s2.refund_id is not null)

--Now only concerned about adjustments as payments all go to GAO as of July 1 2012
select c.id campaignID, e.adjustment_id, e.gl_entry_date, e.[description], a.adjustment_amount, s.balance StatementAmount
from gl_entry e
join adjustment a on a.adjustment_id = e.adjustment_id
join qspcanadacommon..campaign c on c.id = a.campaign_id
join statement s on s.campaignid = c.id and s.statementrunid = 30
where e.gl_entry_date between '2012-10-01' and '2012-11-01'
and e.businessunitID in (1,2)
and ap_cheque_remit_id is null
and e.refund_id is null
and [description] <> 'cancel refund cheque'
and [description] <> 'Refund Cheque (Debit)'
and s.refund_id is not null

select isnull(c.id, c2.id) campaignID, e.payment_id, e.adjustment_id, e.gl_entry_date, e.[description], p.payment_amount, a.adjustment_amount, isnull(s.balance, s2.balance) StatementAmount
from gl_entry e
left join payment p on p.payment_id = e.payment_id
left join qspcanadacommon..campaign c on c.id = p.campaign_id
left join statement s on s.campaignid = c.id and s.statementrunid = 25
left join adjustment a on a.adjustment_id = e.adjustment_id
left join qspcanadacommon..campaign c2 on c2.id = a.campaign_id
left join statement s2 on s2.campaignid = c2.id and s2.statementrunid = 25
where e.gl_entry_date between '2012-05-21' and '2012-06-11'
and e.businessunitID in (1,2)
and ap_cheque_remit_id is null
and e.refund_id is null
and [description] <> 'cancel refund cheque'
and [description] <> 'Refund Cheque (Debit)'
and (s.refund_id is not null or s2.refund_id is not null)

select isnull(c.id, c2.id) campaignID, e.payment_id, e.adjustment_id, e.gl_entry_date, e.[description], p.payment_amount, a.adjustment_amount, isnull(s.balance, s2.balance) StatementAmount
from gl_entry e
left join payment p on p.payment_id = e.payment_id
left join qspcanadacommon..campaign c on c.id = p.campaign_id
left join statement s on s.campaignid = c.id and s.statementrunid = 25
left join adjustment a on a.adjustment_id = e.adjustment_id
left join qspcanadacommon..campaign c2 on c2.id = a.campaign_id
left join statement s2 on s2.campaignid = c2.id and s2.statementrunid = 25
where e.gl_entry_date between '2012-04-23' and '2012-05-21'
and e.businessunitID in (1,2)
and ap_cheque_remit_id is null
and e.refund_id is null
and [description] <> 'cancel refund cheque'
and [description] <> 'Refund Cheque (Debit)'
and (s.refund_id is not null or s2.refund_id is not null)

select isnull(c.id, c2.id) campaignID, e.payment_id, e.adjustment_id, e.gl_entry_date, e.[description], p.payment_amount, a.adjustment_amount, isnull(s.balance, s2.balance) StatementAmount
from gl_entry e
left join payment p on p.payment_id = e.payment_id
left join qspcanadacommon..campaign c on c.id = p.campaign_id
left join statement s on s.campaignid = c.id and s.statementrunid = 25
left join adjustment a on a.adjustment_id = e.adjustment_id
left join qspcanadacommon..campaign c2 on c2.id = a.campaign_id
left join statement s2 on s2.campaignid = c2.id and s2.statementrunid = 25
where e.gl_entry_date between '2012-03-19' and '2012-04-23'
and e.businessunitID in (1,2)
and ap_cheque_remit_id is null
and e.refund_id is null
and [description] <> 'cancel refund cheque'
and [description] <> 'Refund Cheque (Debit)'
and (s.refund_id is not null or s2.refund_id is not null)

select isnull(c.id, c2.id) campaignID, e.payment_id, e.adjustment_id, e.gl_entry_date, e.[description], p.payment_amount, a.adjustment_amount, isnull(s.balance, s2.balance) StatementAmount
from gl_entry e
left join payment p on p.payment_id = e.payment_id
left join qspcanadacommon..campaign c on c.id = p.campaign_id
left join statement s on s.campaignid = c.id and s.statementrunid = 24
left join adjustment a on a.adjustment_id = e.adjustment_id
left join qspcanadacommon..campaign c2 on c2.id = a.campaign_id
left join statement s2 on s2.campaignid = c2.id and s2.statementrunid = 24
where e.gl_entry_date between '2012-02-20' and '2012-03-19'
and e.businessunitID in (1,2)
and ap_cheque_remit_id is null
and e.refund_id is null
and [description] <> 'cancel refund cheque'
and [description] <> 'Refund Cheque (Debit)'
and (s.refund_id is not null or s2.refund_id is not null)
