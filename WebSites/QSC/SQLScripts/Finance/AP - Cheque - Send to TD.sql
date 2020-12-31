USE [QSPCanadaFinance]
GO

--change to Remit user

select apc.AP_Cheque_ID, *
from refund ref
join ap_cheque apc on apc.ap_cheque_id = ref.ap_cheque_id
where ref.campaign_id = 79157
order by apc.ap_cheque_id desc

DECLARE	@AP_Cheque_ID int,
		@AP_Cheque_Batch_ID int

SET @AP_Cheque_ID = 103260

EXEC	[dbo].[AP_Cheque_Batch_GroupProfit_Create]
		@AP_Cheque_ID = @AP_Cheque_ID,
		@AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID OUTPUT

--Todo: Look at setting these to Time BusinessUnit and not letting the SwitchToGAO proc override it
SELECT	TOP 5 *
FROM	Adjustment adj
JOIN	GL_entry e
			ON	e.adjustment_ID = adj.Adjustment_ID
JOIN	GL_Transaction t
			ON	t.GL_Entry_ID = e.GL_Entry_ID
WHERE	adj.Campaign_ID = 79157
ORDER BY adj.Adjustment_ID DESC



/*select top 10 *
from ap_cheque_batch
order by ap_cheque_batch_id desc

select *
from ap_cheque
where ap_cheque_batch_id = 379*/