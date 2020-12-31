/*
1. Run the Webpage on gawebp01 to generate the PDFs on prod website
2. Run C:\Apps\Installation\GA\QSPC\qspfulfca.com\QSPFulfillment\bin\pdftk.exe *.pdf cat output statements.pdf to stitch the PDFs together 
3. Locally, copy from \\gawebp01\C$\Statements\ to \\monqspmvf1.swgao.int\QSPCanada\Finance\Statements
4. Run the code below to create an Excel summary file for Resolve
*/

USE QSPCanadaFinance

SELECT TOP 10 *
FROM StatementPrintRequestBatch
ORDER BY StatementPrintRequestBatchID DESC

DECLARE @StatementPrintRequestBatchID	INT
SET @StatementPrintRequestBatchID = 311

UPDATE		apc
SET			ChequeNumber = 0
FROM		[Statement] stat
JOIN		StatementPrintRequest spr
				ON	spr.StatementID = stat.StatementID
JOIN		Refund ref
				ON	ref.Refund_ID = stat.Refund_ID
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		spr.StatementPrintRequestBatchID = @StatementPrintRequestBatchID

DECLARE @StatementPrintRequestBatchID	INT
SET @StatementPrintRequestBatchID = 311

--GroupProfitCheques.xlsx
SELECT		CONVERT(VARCHAR(35), CASE WHEN LEN(acc.ProfitChequePayee) > 0 THEN acc.ProfitChequePayee ELSE acc.Name END) Name1,
			ref.Address1,
			ref.City,
			ref.Province [State/Province],
			ref.PostalCode,
			ref.Country,
			'CDN' Currency,
			stat.Balance * -1 Amount,
			'QSP-CA' CompanyIdentifier,
			apc.AP_Cheque_ID ReferenceID,
			'' Salutation,
			'' Name2,
			ref.Address2,
			acc.ID CustomerNumber,
			'' ItemText,
			ref.Campaign_ID CampaignID
FROM		[Statement] stat
JOIN		StatementPrintRequest spr
				ON	spr.StatementID = stat.StatementID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = stat.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		Refund ref
				ON	ref.Refund_ID = stat.Refund_ID
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		spr.StatementPrintRequestBatchID = @StatementPrintRequestBatchID
ORDER BY	stat.Balance

--StatementSummary.xlsx
SELECT		acc.ID AS AccountID,
			stat.Balance,
			ref.Refund_ID,
			apc.AP_Cheque_ID,
			Amount,
			ref.Address1,
			ref.Address2,
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.Country,
			ref.Campaign_ID,
			ref.CreateDate
FROM		[Statement] stat
JOIN		StatementPrintRequest spr
				ON	spr.StatementID = stat.StatementID
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = stat.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
LEFT JOIN	Refund ref
				ON	ref.Refund_ID = stat.Refund_ID
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
WHERE		spr.StatementPrintRequestBatchID = @StatementPrintRequestBatchID
ORDER BY	stat.Balance