/*
1. Run QSP.Fulfillment.Finance.Refund
2. Delete the file it creates
3. Run the code below as Remit user to generate the cheque file to go to TD
*/

USE QSPCanadaFinance

DECLARE		@GenerateChequeBatch INT
SELECT		@GenerateChequeBatch = AP_Cheque_Batch_ID
FROM		AP_Cheque_Batch
WHERE		[Type] = 'Customer Refund'
ORDER BY	AP_Cheque_Batch_ID

PRINT @generatechequebatch

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

INSERT INTO ##AP_Cheque
SELECT		apc.ChequeNumber,
			ISNULL(ref.Amount, 0),
			ISNULL(ref.Amount, 0),
			CASE ref.Country
				WHEN 'US' THEN 'USD'
				ELSE 'CAD'
			END,
			ba.Bank_Account_Number,
			'Refund for ' + SUBSTRING(cod.ProductCode, 1, 6) + ' - ' + SUBSTRING(cod.ProductName, 1, 35) + ' - Sub ID ' + CONVERT(VARCHAR(13), ref.CustomerOrderHeaderInstance),
			CONVERT(VARCHAR(17), ref.CustomerOrderHeaderInstance),
			apcb.AP_Cheque_Batch_ID,
			ISNULL(SUBSTRING(ref.FirstName, 1, 17), '') + ' ' + ISNULL(SUBSTRING(ref.LastName, 1, 17), ''),
			'', --Blank means send directly to customer
			ref.Address1,
			ref.Address2,
			ref.City,
			ref.Province,
			ref.PostalCode,
			ref.Country
FROM		Refund ref
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = ref.AP_Cheque_ID
JOIN		AP_Cheque_Batch apcb
				ON	apcb.AP_Cheque_Batch_ID = apc.AP_Cheque_Batch_ID
JOIN		Bank_Account ba
				ON	ba.Bank_Account_ID = apc.Bank_Account_ID
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = ref.CustomerOrderHeaderInstance
				AND	cod.TransID = ref.TransID
WHERE		apcb.AP_Cheque_Batch_ID = @GenerateChequeBatch
ORDER BY	apc.ChequeNumber

EXEC AP_CreateChequeFile @GenerateChequeBatch

DROP TABLE ##AP_Cheque