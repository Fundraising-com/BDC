USE QSPCanadaCommon
GO

select fm.FirstName, fm.LastName, c.ID CampaignID, c.startdate, c.enddate, acc.ID AccountID, acc.Name AccountName, ad.street1, ad.street2, ad.city, ad.postal_code, ad.stateProvince Province, ISNULL(SUM(iSec.Net_Before_Tax - ISNULL(iSec.US_Postage_Amount, 0.00)), 0.00) NetSales
from Campaign c
join FieldManager fm on fm.fmid = c.fmid
join CAccount acc on acc.Id = c.BillToAccountID
left join Address ad on ad.AddressListID = acc.AddressListID and ad.address_type = 54001
left join QSPCanadaOrderManagement..Batch b on b.CampaignID = c.ID and b.OrderTypeCode NOT IN(41006,41007,41011, 41012) and b.OrderQualifierId <> 39006
left join QSPCanadaFinance..INVOICE inv on inv.ORDER_ID = b.OrderID
left join QSPCanadaFinance..INVOICE_SECTION iSec on iSec.INVOICE_ID = inv.INVOICE_ID and iSec.Section_Type_ID  in (1,2,9,10,11)
where c.FMID in ('0511','0037','0041','0070','0064')
and c.StartDate >= '2013-01-01'
group by fm.FirstName, fm.LastName,c.ID, c.startdate, c.enddate, acc.ID, acc.Name, ad.street1, ad.street2, ad.city, ad.postal_code, ad.stateProvince
order by fm.lastname, ad.city, acc.ID, c.ID

/*
select *
from fieldmanager
where lastname like '%Nysetvold%'*/