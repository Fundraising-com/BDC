USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Remit_CreateBatch]    Script Date: 06/07/2017 09:17:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Remit_CreateBatch]

AS

DECLARE @RunDate			DATETIME,
		@BankAccountID		INT,
		@AP_Cheque_ID		INT,
		@AP_Cheque_Batch_ID	INT,
		@ChequeType			VARCHAR(50),
		@ErrorMessage  		VARCHAR(100),
		@SendEmailToIT		VARCHAR(1000),
		@AP_Cheque_Remit_ID	INT,
		@CurrencyCode		VARCHAR(3),
		@ChequeNumber		BIGINT

SET @ChequeType = 'Remit'
SET @RunDate = GETDATE()

BEGIN TRANSACTION

INSERT INTO AP_Cheque_Batch
(
	CreationDate,
	[Type]
)
SELECT	@RunDate,
		@ChequeType
SET @AP_Cheque_Batch_ID = SCOPE_IDENTITY()

DECLARE	Cheque CURSOR FOR
SELECT		apcr.AP_Cheque_Remit_ID,
			apcr.CurrencyCode
FROM		AP_Cheque_Remit apcr
LEFT JOIN	AP_Cheque apc
				ON	apc.AP_Cheque_ID = apcr.AP_Cheque_ID
WHERE		apc.AP_Cheque_ID IS NULLORDER BY	apcr.AP_Cheque_Remit_ID

OPEN Cheque
FETCH NEXT FROM Cheque INTO @AP_Cheque_Remit_ID, @CurrencyCode

WHILE @@FETCH_STATUS = 0
BEGIN

	SELECT	@BankAccountID =	CASE @CurrencyCode
									WHEN 'USD' THEN 5
									ELSE 4
								END

	SELECT	@ChequeNumber = MAX(ISNULL(ChequeNumber, 0)) + 1
	FROM	AP_Cheque
	WHERE	Bank_Account_ID = @BankAccountID

	INSERT INTO AP_Cheque
	(
		AP_Cheque_Batch_ID,
		AP_Cheque_Status_ID,
		ChequeNumber,
		CreationDate,
		Bank_Account_ID,
		ChequePayableDate
	)
	SELECT		@AP_Cheque_Batch_ID,
				2, --2: Outstanding
				@ChequeNumber,
				@RunDate,
				@BankAccountID,
				@RunDate

	SET @AP_Cheque_ID = SCOPE_IDENTITY()

	UPDATE	AP_Cheque_Remit
	SET		AP_Cheque_ID = @AP_Cheque_ID
	WHERE	AP_Cheque_Remit_ID = @AP_Cheque_Remit_ID

	FETCH NEXT FROM Cheque INTO @AP_Cheque_Remit_ID, @CurrencyCode

	END
	CLOSE Cheque
	DEALLOCATE Cheque

COMMIT


--Create cheque file
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
	BankAccount			VARCHAR(12),
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
	Country				VARCHAR(2),
)

INSERT INTO ##AP_Cheque
SELECT		apc.ChequeNumber,
			ISNULL(apcr.NetAmount, 0) + ISNULL(apcr.GSTAmount, 0) + ISNULL(apcr.HSTAmount, 0) + ISNULL(apcr.PSTAmount, 0),
			ISNULL(apcr.NetAmount, 0),
			apcr.CurrencyCode,
			SUBSTRING(ba.Bank_Account_Number, 1, 12),
			'Remit for Batch ID ' +	CONVERT(VARCHAR(10), apcr.RemitBatchID),
			apcr.RemitCode,
			apcr.RemitBatchID,
			SUBSTRING(apcr.ProductSortName, 1, 35),
			'02',
			apcr.Address1,
			apcr.Address2,
			apcr.City,
			apcr.Province,
			apcr.PostalCode,
			apcr.CountryCode
FROM		AP_Cheque_Remit apcr
JOIN		AP_Cheque apc
				ON	apc.AP_Cheque_ID = apcr.AP_Cheque_ID
JOIN		Bank_Account ba
				ON	ba.Bank_Account_ID = apc.Bank_Account_ID
WHERE		apc.AP_Cheque_Batch_ID = @AP_Cheque_Batch_ID
ORDER BY	apc.ChequeNumber

--Temp fix - GAO will manually write cheques for now
--EXEC AP_CreateChequeFile @AP_Cheque_Batch_ID

DROP TABLE ##AP_Cheque
GO
