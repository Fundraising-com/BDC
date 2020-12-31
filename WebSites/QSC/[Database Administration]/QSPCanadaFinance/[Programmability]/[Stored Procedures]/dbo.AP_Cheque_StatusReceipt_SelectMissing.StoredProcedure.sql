USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_StatusReceipt_SelectMissing]    Script Date: 06/07/2017 09:17:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Cheque_StatusReceipt_SelectMissing]

AS
--Must disclude remit cheques somehow
SELECT		apc.AP_Cheque_ID,
			apc.ChequeNumber,
			apc.CreationDate AS ChequeSentDate,
			ba.Bank_Account_Number,
			ba.Bank_Account_Description
FROM		AP_Cheque apc
JOIN		Bank_Account ba
				ON	ba.Bank_Account_ID = apc.Bank_Account_ID
LEFT JOIN	AP_Cheque_StatusReceipt apcsr
				ON	apcsr.AP_Cheque_ID = apc.AP_Cheque_ID
				AND	apcsr.AP_Cheque_StatusReceipt_ID = 2 --2: Processed
WHERE		apcsr.AP_Cheque_StatusReceipt_ID IS NULL
AND			apc.CreationDate < DATEADD(DAY, -2, GETDATE())
AND			apc.CreationDate > '2011-01-01' --*Must change --When we started receiving cheque status receipts
ORDER BY	apc.AP_Cheque_ID
GO
