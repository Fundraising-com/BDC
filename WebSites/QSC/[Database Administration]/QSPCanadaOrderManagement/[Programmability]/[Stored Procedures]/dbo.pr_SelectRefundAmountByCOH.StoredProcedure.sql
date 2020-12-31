USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectRefundAmountByCOH]    Script Date: 06/07/2017 09:20:34 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_SelectRefundAmountByCOH]

	@iCustomerOrderHeaderInstance	INT,
	@iTransID						INT

AS

SELECT		ref.Amount AS Amount,
			ref.CreateDate AS CreateDate,
			apcb.CreationDate AS SentDate,
			apc.ChequeNumber
FROM		QSPCanadaFinance..Refund ref
LEFT JOIN	(QSPCanadaFinance..AP_Cheque apc
JOIN			QSPCanadaFinance..AP_Cheque_Batch apcb
					ON	apcb.AP_Cheque_Batch_ID = apc.AP_Cheque_Batch_ID)
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE	ref.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance
AND		ref.TransID = @iTransID
ORDER BY ref.CreateDate DESC
GO
