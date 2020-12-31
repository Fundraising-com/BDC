USE QSPCanadaFinance
GO

DECLARE @StatementRunID INT
SET @StatementRunID = 49

DECLARE @StatementPrintRequestBatchID INT
SELECT	@StatementPrintRequestBatchID = spr.StatementPrintRequestBatchID
FROM	StatementPrintRequest spr
JOIN	[Statement] s
			ON	s.StatementID = spr.StatementID
WHERE	StatementRunID = @StatementRunID

BEGIN TRAN

DELETE	spr
FROM	StatementPrintRequest spr
WHERE	spr.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

DELETE	sprb
FROM	StatementPrintRequestBatch sprb
WHERE	sprb.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

DELETE	spre
FROM	StatementPrintRequestError spre
JOIN	[Statement] s
			ON	s.StatementID = spre.StatementID
WHERE	s.StatementRunID = @StatementRunID

DELETE	si
FROM	StatementInvoice si
JOIN	[Statement] s
			ON	s.StatementID = si.StatementID
WHERE	s.StatementRunID = @StatementRunID

DELETE	sp
FROM	StatementPayment sp
JOIN	[Statement] s
			ON	s.StatementID = sp.StatementID
WHERE	s.StatementRunID = @StatementRunID

DELETE	sa
FROM	StatementAdjustment sa
JOIN	[Statement] s
			ON	s.StatementID = sa.StatementID
WHERE	s.StatementRunID = @StatementRunID

DELETE	si
FROM	StatementInvoice si
JOIN	[Statement] s
			ON	s.StatementID = si.StatementID
WHERE	s.StatementRunID = @StatementRunID

DELETE	si
FROM	StatementInvoiceCustSvc si
JOIN	[Statement] s
			ON	s.StatementID = si.StatementID
WHERE	s.StatementRunID = @StatementRunID

DELETE	si
FROM	StatementInvoiceOnline si
JOIN	[Statement] s
			ON	s.StatementID = si.StatementID
WHERE	s.StatementRunID = @StatementRunID

DELETE	glt
FROM	GL_Transaction glt
JOIN	GL_Entry gle
			ON	gle.GL_Entry_ID = glt.GL_Entry_ID
JOIN	Adjustment adj
			ON	adj.ADJUSTMENT_ID = gle.ADJUSTMENT_ID
WHERE	adj.ADJUSTMENT_TYPE_ID = 49024
AND		adj.ADJUSTMENT_EFFECTIVE_DATE between '2014-12-01' and '2014-12-02'

DELETE	gle
FROM	GL_Entry gle
JOIN	Adjustment adj
			ON	adj.ADJUSTMENT_ID = gle.ADJUSTMENT_ID
WHERE	adj.ADJUSTMENT_TYPE_ID = 49024
AND		adj.ADJUSTMENT_EFFECTIVE_DATE between '2014-12-01' and '2014-12-02'

DELETE	adj
FROM	Adjustment adj
WHERE	adj.ADJUSTMENT_TYPE_ID = 49024
AND		adj.ADJUSTMENT_EFFECTIVE_DATE between '2014-12-01' and '2014-12-02'

SELECT	r.Refund_ID, r.AP_Cheque_ID
INTO	#Ref
FROM	Refund r
JOIN	[Statement] s
			ON	s.Refund_ID = r.Refund_ID
WHERE	s.StatementRunID = @StatementRunID

DELETE	[Statement]
WHERE	StatementRunID = @StatementRunID

DELETE	r
FROM	Refund r
JOIN	#Ref ref
			ON	ref.Refund_ID = r.Refund_ID

DELETE	apc
FROM	AP_Cheque apc
JOIN	#Ref r
			ON	r.AP_Cheque_ID = apc.AP_Cheque_ID

update	StatementRun
set		StatementRunClosed = 0
WHERE	StatementRunID = @StatementRunID

DROP TABLE #Ref