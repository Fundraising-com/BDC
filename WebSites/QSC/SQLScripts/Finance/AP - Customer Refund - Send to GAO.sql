USE [QSPCanadaFinance]
GO

SELECT TOP 99 *
from AP_Cheque_Batch
where [type] = 'Customer Refund - GAO'
order by AP_Cheque_Batch_ID desc

DECLARE @AP_Cheque_Batch_ID INT

SET @AP_Cheque_Batch_ID = 830

SELECT	Refund_ID,
		apc.AP_Cheque_ID,
		Amount,
		Address1,
		Address2,
		City,
		Province,
		PostalCode,
		Country,
		CustomerOrderHeaderInstance,
		TransID,
		FirstName,
		LastName,
		Campaign_ID,
		CreateDate,
		CreateUserID,
		UpdateDate,
		UpdateUserID,
		AP_Cheque_Batch_ID
FROM	Refund ref
JOIN	AP_Cheque apc
			ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE	apc.AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
