USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_StatusReceipt_SelectError]    Script Date: 06/07/2017 09:17:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_StatusReceipt_SelectError]

AS

SELECT		apcsr.AP_Cheque_StatusReceipt_ID,
			apcsr.AP_Cheque_ID,
			apcsrb.AP_Cheque_StatusReceipt_Batch_ID,
			apcsrb.FileName,
			apcsr.CreateDate AS AP_Cheque_StatusReceipt_Date,
			apcsrs.Description AS Error
FROM		AP_Cheque_StatusReceipt_Batch apcsrb
JOIN		AP_Cheque_StatusReceipt apcsr
				ON	apcsr.AP_Cheque_StatusReceipt_Batch_ID = apcsrb.AP_Cheque_StatusReceipt_Batch_ID
JOIN		AP_Cheque_StatusReceipt_Status apcsrs
				ON	apcsrs.AP_Cheque_StatusReceipt_Status_ID = apcsr.AP_Cheque_StatusReceipt_Status_ID
LEFT JOIN	AP_Cheque_StatusReceipt apcsrNew
				ON	apcsrNew.AP_Cheque_ID = apcsr.AP_Cheque_ID
				AND	apcsrNew.CreateDate > apcsr.CreateDate
WHERE		apcsr.AP_Cheque_StatusReceipt_Status_ID >= 4
AND			apcsrNew.AP_Cheque_StatusReceipt_ID IS NULL --No new Cheque status receipt for this cheque has been received
GO
