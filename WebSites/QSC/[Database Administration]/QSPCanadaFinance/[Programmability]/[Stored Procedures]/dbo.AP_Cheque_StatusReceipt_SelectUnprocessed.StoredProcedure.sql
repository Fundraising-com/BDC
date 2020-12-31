USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_StatusReceipt_SelectUnprocessed]    Script Date: 06/07/2017 09:17:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_StatusReceipt_SelectUnprocessed]

	@AP_Cheque_StatusReceipt_Batch_ID	INT

AS

SELECT		apcsr.AP_Cheque_StatusReceipt_ID
FROM		AP_Cheque_StatusReceipt apcsr
WHERE		apcsr.AP_Cheque_StatusReceipt_Status_ID IN (1) --1: Unprocessed
AND			apcsr.AP_Cheque_StatusReceipt_Batch_ID = @AP_Cheque_StatusReceipt_Batch_ID
AND			NOT EXISTS (SELECT	1
						FROM	AP_Cheque_StatusReceipt apcsrError
						WHERE	apcsrError.AP_Cheque_StatusReceipt_Batch_ID = apcsr.AP_Cheque_StatusReceipt_Batch_ID
						AND		apcsrError.AP_Cheque_StatusReceipt_Status_ID >= 3) --Another receipt in same file is in Error
ORDER BY	apcsr.AP_Cheque_StatusReceipt_ID
GO
