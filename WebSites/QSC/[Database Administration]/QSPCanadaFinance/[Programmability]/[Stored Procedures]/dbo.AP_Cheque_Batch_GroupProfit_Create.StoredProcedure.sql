USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Cheque_Batch_GroupProfit_Create]    Script Date: 06/07/2017 09:17:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AP_Cheque_Batch_GroupProfit_Create]

	@AP_Cheque_ID		INT,
	@AP_Cheque_Batch_ID	INT OUTPUT

AS

SELECT	TOP 1 1
FROM	AP_Cheque apc
JOIN	Refund ref
			ON	ref.AP_Cheque_ID = apc.AP_Cheque_ID
WHERE	apc.AP_Cheque_Batch_ID IS NULL
AND		ref.Refund_Type_ID = 2 --Group Refund
AND		ref.Cancelled = 0
AND		ref.CreateDate > '2009-01-28 15:20' --Started sending Group Refunds directly at this time
AND		apc.AP_Cheque_ID = @AP_Cheque_ID

IF @@ROWCOUNT = 0
BEGIN
	SET	@AP_Cheque_Batch_ID = 0
	RETURN
END

INSERT INTO AP_Cheque_Batch
(
	CreationDate,
	[Type]
)
SELECT	GETDATE(),
		'Group Refund'

SET @AP_Cheque_Batch_ID = SCOPE_IDENTITY()

UPDATE		apc
SET			apc.AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
FROM		AP_Cheque apc
JOIN		Refund ref
				ON	ref.AP_Cheque_ID = apc.AP_Cheque_ID
WHERE		apc.AP_Cheque_Batch_ID IS NULL
AND			ref.Refund_Type_ID = 2 --Group Refund
AND			ref.Cancelled = 0
AND			ref.CreateDate > '2009-01-28 15:20' --Started sending Group Refunds directly at this time
AND			apc.AP_Cheque_ID = @AP_Cheque_ID


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
			'CAD',
			ba.Bank_Account_Number,
			'Group Refund for Cheque Batch ID ' + CONVERT(VARCHAR(10), apcb.AP_Cheque_Batch_ID),
			CONVERT(VARCHAR(17), camp.BillToAccountID),
			apcb.AP_Cheque_Batch_ID,
			CONVERT(VARCHAR(35), acc.Name),
			'02', --Tells TD Bank to send to Resolve
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
WHERE		apcb.AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
AND			camp.FMID <> '0508' --EFR Accounts do an intercompany transfer instead of receiving a physical cheque
AND			apc.AP_Cheque_ID = @AP_Cheque_ID
ORDER BY	apc.ChequeNumber

EXEC AP_CreateChequeFile @AP_Cheque_Batch_ID

DROP TABLE ##AP_Cheque
GO
