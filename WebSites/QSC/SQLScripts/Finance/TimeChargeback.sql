select *
from QSPCanadaFinance..refund
where refund_id = 1065382

select *
from QSPCanadaFinance..GL_ENTRY
where ADJUSTMENT_ID = 113484

select top 99 * from QSPCanadaFinance..Statement where CampaignID = 69053

select *
from QSPCanadaFinance..ADJUSTMENT where adjustment_amount = -412.36

--select *
--from UDF_BusinessUnit_IsTimeOrder(

select distinct c.ID CampaignWithProfitCheque, c.StartDate CampaignWithProfitChequeStartDate, c.BillToAccountID AccountID, a.ADJUSTMENT_ID ProfitChequeAdjustmentID, a.ADJUSTMENT_EFFECTIVE_DATE ProfitChequeDate, a.ADJUSTMENT_AMOUNT ProfitChequeAmount, r.AP_Cheque_ID ReferenceID--, ag2.CampaignID CampaignWithTransactionThatCausedProfitCheque, ag2.TransactionAmount TransactionThatCausedProfitChequeAmount, ag2.TransactionTypeName TransactionThatCausedProfitChequeName
from QSPCanadaCommon..Campaign c
join QSPCanadaFinance..ADJUSTMENT a on a.CAMPAIGN_ID = c.ID
left join dbo.UDF_Statement_GetDetails_WithBusLogic_Aggregated('2014-06-01') ag on ag.campaignid = c.id and ag.TransactionTypeID not in (3) and ag.TransactionDate between dateadd(mm, -1, a.adjustment_effective_date) and a.ADJUSTMENT_EFFECTIVE_DATE
join dbo.UDF_Statement_GetDetails_WithBusLogic_Aggregated('2014-06-01') ag2 on ag2.AccountID = c.BillToAccountID and ag2.campaignid <> c.id and ag2.TransactionTypeID not in (3) and ag2.TransactionDate between dateadd(mm, -1, a.adjustment_effective_date) and a.ADJUSTMENT_EFFECTIVE_DATE
left join Refund r on r.Campaign_ID = c.ID and r.Amount = a.ADJUSTMENT_AMOUNT * -1 and r.Refund_Type_ID = 2 and r.CreateDate between DATEADD(dd,-1,a.ADJUSTMENT_EFFECTIVE_DATE) and DATEADD(dd,1,a.ADJUSTMENT_EFFECTIVE_DATE)
where c.StartDate <= '2012-01-01'
and a.ADJUSTMENT_TYPE_ID = 49024
and a.ADJUSTMENT_EFFECTIVE_DATE >= '2012-01-01'
and c.ID in (select s.CampaignID from Statement s join StatementPrintRequestError e on e.StatementID = s.StatementID)
and ag.CampaignID is null
order by a.ADJUSTMENT_ID

select * from dbo.UDF_Statement_GetDetails_WithBusLogic_Aggregated('2014-06-01') ag where ag.campaignid = 69053

select * from TransactionType

--Final. 17 rows
select distinct c.ID CampaignWithProfitCheque, c.StartDate CampaignWithProfitChequeStartDate, c.BillToAccountID AccountID, a.ADJUSTMENT_ID ProfitChequeAdjustmentID, a.ADJUSTMENT_EFFECTIVE_DATE ProfitChequeDate, a.ADJUSTMENT_AMOUNT ProfitChequeAmount, r.AP_Cheque_ID ReferenceID--, ag2.CampaignID CampaignWithTransactionThatCausedProfitCheque, ag2.TransactionAmount TransactionThatCausedProfitChequeAmount, ag2.TransactionTypeName TransactionThatCausedProfitChequeName
from QSPCanadaCommon..Campaign c
join QSPCanadaFinance..ADJUSTMENT a on a.CAMPAIGN_ID = c.ID
left join dbo.UDF_Statement_GetDetails_WithBusLogic_Aggregated('2014-06-01') ag on ag.campaignid = c.id and ag.TransactionTypeID not in (3) and ag.TransactionDate between dateadd(mm, -1, a.adjustment_effective_date) and a.ADJUSTMENT_EFFECTIVE_DATE
join dbo.UDF_Statement_GetDetails_WithBusLogic_Aggregated('2014-06-01') ag2 on ag2.AccountID = c.BillToAccountID and ag2.campaignid <> c.id and ag2.TransactionTypeID not in (3) and ag2.TransactionDate between dateadd(mm, -1, a.adjustment_effective_date) and a.ADJUSTMENT_EFFECTIVE_DATE
left join Refund r on r.Campaign_ID = c.ID and r.Amount = a.ADJUSTMENT_AMOUNT * -1 and r.Refund_Type_ID = 2 and r.CreateDate between DATEADD(dd,-1,a.ADJUSTMENT_EFFECTIVE_DATE) and DATEADD(dd,1,a.ADJUSTMENT_EFFECTIVE_DATE)
where c.StartDate <= '2012-01-01'
and a.ADJUSTMENT_TYPE_ID = 49024
and a.ADJUSTMENT_EFFECTIVE_DATE >= '2012-03-20'
and c.ID in (select s.CampaignID from Statement s join StatementPrintRequestError e on e.StatementID = s.StatementID)
and ag.CampaignID is null
and a.ADJUSTMENT_ID not in (109636, 109706, 110567, 112053)
order by a.ADJUSTMENT_ID