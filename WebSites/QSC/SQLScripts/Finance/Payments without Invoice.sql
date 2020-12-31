select c.ID CampaignID, c.BillToAccountID AccountID, p.Payment_ID, p.Payment_Effective_Date, p.Payment_Amount, b.Date OrderDate, cd.description OrderType, b.StatusInstance
from payment p
left join invoice i on i.order_id = p.order_id
join qspcanadaordermanagement..batch b on b.orderid = p.order_id
join qspcanadacommon..codedetail cd on cd.instance = b.orderqualifierid
join qspcanadacommon..campaign c on c.id = b.campaignid
where i.invoice_id is null
and b.orderqualifierid not in (39009,39015)
and c.startdate >= '2012-01-01'
order by p.payment_id