use qspcanadafinance
go

select adj.Adjustment_ID AdjustmentID, adj.Adjustment_Effective_Date EffectiveDate, adj.Date_Created DateCreated, adj.Adjustment_Amount Amount, camp.ID CampaignID, adj.Internal_Comment Reason
from adjustment adj
join qspcanadacommon..campaign camp on camp.id = adj.campaign_id
where adj.adjustment_type_id in (49076, 49077)
and camp.fmid = '1560'
