USE QSPCanadaFinance
GO

SELECT *
FROM	AP_Cheque_Remit
WHERE	RemitBatchID = 1384

SELECT	top 100 *
FROM	Refund ref
WHERE	Refund_Type_ID = 1
AND		AP_Cheque_ID 