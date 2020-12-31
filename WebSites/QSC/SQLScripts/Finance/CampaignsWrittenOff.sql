select	acc.Name AccountName, Account_ID, Campaign_ID, Adjustment_id, ADJUSTMENT_EFFECTIVE_DATE WriteoffDate, ADJUSTMENT_AMOUNT WriteOffAmount, INTERNAL_COMMENT WriteoffComment
from ADJUSTMENT a
join QSPCanadaCommon..CACCOUNT acc ON acc.ID = a.ACCOUNT_ID
where adjustment_amount >= 5.00
and adjustment_type_id = 49022
and a.ADJUSTMENT_EFFECTIVE_DATE >= '2011-01-01'
ORDER BY ADJUSTMENT_ID