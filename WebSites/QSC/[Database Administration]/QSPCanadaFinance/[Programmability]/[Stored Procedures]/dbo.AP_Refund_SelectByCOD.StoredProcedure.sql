USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Refund_SelectByCOD]    Script Date: 06/07/2017 09:17:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AP_Refund_SelectByCOD]

	@CustomerOrderHeaderInstance	INT,
	@TransID						INT

AS

SELECT		ref.Amount,
			ref.CreateDate,
			apcb.CreationDate AS SentDate,
			apc.ChequeNumber,
			ref.Refund_ID,
			apcs.Description AS ChequeStatus,
			apc.AP_Cheque_Status_ID,
			ref.FirstName,
			ref.LastName,
			ref.Address1,
			ref.Address2,
			ref.Country,
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.Cancelled As RefundCancelled
FROM		Refund ref
LEFT JOIN	(AP_Cheque apc
JOIN			AP_Cheque_Status apcs
					ON	apcs.AP_Cheque_Status_ID = apc.AP_Cheque_Status_ID)
LEFT JOIN		AP_Cheque_Batch apcb
					ON	apcb.AP_Cheque_Batch_ID = apc.AP_Cheque_Batch_ID
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		ref.Cancelled = 0
AND			ref.CustomerOrderHeaderInstance = @CustomerOrderHeaderInstance
AND			ref.TransID = @TransID
ORDER BY	ref.CreateDate DESC
GO
