/*
1. Run QSP.Fulfillment.Finance.Refund
2. Delete the file it creates
3. Run the code below as Remit user to generate the cheque file to go to TD
*/

USE QSPCanadaFinance

select * from refund r
join ap_cheque a on a.ap_cheque_id = r.ap_cheque_id
join ap_cheque_batch b on b.ap_cheque_batch_id = a.ap_cheque_batch_id where refund_type_id in (3,4)

IF EXISTS (	SELECT 1
			FROM tempdb..sysobjects
			WHERE type = 'U' and NAME = '##AP_Cheque')
BEGIN
	DROP TABLE [dbo].[##AP_Cheque]
END

CREATE TABLE ##AP_Cheque
(
	ChequeNumber		BIGINT,
	AmountWithTax		NUMERIC(14, 2),
	AmountWithoutTax	NUMERIC(14, 2),
	Currency			VARCHAR(3),
	BankAccount			VARCHAR(50),
	Description1		VARCHAR(80),
	Description2		VARCHAR(17),
	BatchID				INT,
	RecipientName		VARCHAR(35),
	SendTo				VARCHAR(30),
	Address1			VARCHAR(50),
	Address2			VARCHAR(50),
	City				VARCHAR(50),
	Province			VARCHAR(2),
	PostalCode			VARCHAR(10),
	Country				VARCHAR(2)
)

DECLARE @GenerateChequeBatch INT
SET @GenerateChequeBatch = 159

INSERT INTO ##AP_Cheque
SELECT		apc.ChequeNumber,
			ISNULL(ref.Amount, 0),
			ISNULL(ref.Amount, 0),
			CASE ref.Country
				WHEN 'US' THEN 'USD'
				ELSE 'CAD'
			END,
			ba.Bank_Account_Number,
			'Group Refund for Cheque Batch ID ' + CONVERT(VARCHAR(10), apcb.AP_Cheque_Batch_ID),
			CONVERT(VARCHAR(17), camp.BillToAccountID),
			apcb.AP_Cheque_Batch_ID,
			CONVERT(VARCHAR(35), acc.Name),
			'02',  --Tells TD Bank to send to Resolve
			ref.Address1,
			ref.Address2,
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.Country
FROM		Refund ref
JOIN		QSPCanadaCommon..Campaign camp
				ON	ref.Campaign_ID = camp.ID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
JOIN		AP_Cheque_Batch apcb
				ON	apcb.AP_Cheque_Batch_ID = apc.AP_Cheque_Batch_ID
JOIN		Bank_Account ba
				ON	ba.Bank_Account_ID = apc.Bank_Account_ID
WHERE		apcb.AP_Cheque_Batch_ID = @GenerateChequeBatch
AND			camp.FMID <> '0508' --EFR Accounts do an intercompany transfer instead of receiving a physical cheque
ORDER BY	apc.ChequeNumber

EXEC AP_CreateChequeFile @GenerateChequeBatch

DROP TABLE ##AP_Cheque