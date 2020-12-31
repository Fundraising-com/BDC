USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Remit_CreateCheque]    Script Date: 06/07/2017 09:17:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Remit_CreateCheque]

	@ErrorMessage			VARCHAR(200) OUTPUT

AS

DECLARE @RunDate  					DATETIME,
		@GLDescriptionText			VARCHAR(50),
		@GLEntryID					INT,
		@TotalGrossAmount			NUMERIC(14, 2),
		@GrossAmount				NUMERIC(14, 2),
		@AP_Cheque_Remit_ID			INT,
		@RemitBatchID				INT,
		@RemitCode					VARCHAR(20),
		@CurrencyCode				INT,
		@BusinessUnitID				INT,
		@AP_Cheque_Remit_Detail_ID	INT		

SET @RunDate = GETDATE()

SELECT	@RemitBatchID = RemitBatchID,
		@RemitCode = RemitCode,
		@CurrencyCode = CurrencyCode
FROM	##AP_Cheque_Remit

BEGIN TRANSACTION

INSERT INTO AP_Cheque_Remit
(
	RemitBatchID,
	CreationDate, 
	RemitCode, 
	FulfillmentHouseID, 
	ProductSortName, 
	NetAmount, 
	GSTAmount, 
	HSTAmount, 
	PSTAmount, 
	CurrencyCode, 
	Address1, 
	Address2, 
	City, 
	Province, 
	PostalCode, 
	CountryCode, 
	Comment,
	PostageAmount
)
SELECT		TOP 1
			apcr.RemitBatchID,
			apcr.CreationDate, 
			apcr.RemitCode, 
			apcr.FulfillmentHouseID, 
			apcr.ProductSortName, 
			ROUND(SUM(ISNULL(apcrd.NetAmount, 0)), 2), 
			ROUND(SUM(CASE	WHEN glAccMapFedTax.GLAccountMapID IN (59) THEN	ISNULL(apcrd.FedTaxAmount, 0)
						ELSE											0
				END), 2),
			ROUND(SUM(CASE	WHEN glAccMapFedTax.GLAccountMapID IN (60, 61, 62, 134, 135) THEN	ISNULL(apcrd.FedTaxAmount, 0)
						ELSE													0
				END), 2),
			ROUND(SUM(ISNULL(apcrd.ProvTaxAmount, 0)), 2), 
			CASE apcr.CurrencyCode
				WHEN 801 THEN 'CAD'
				WHEN 802 THEN 'USD'
			END, 
			apcr.Address1, 
			apcr.Address2, 
			apcr.City, 
			apcr.Province, 
			apcr.PostalCode, 
			apcr.CountryCode, 
			apcr.Comment,
			ROUND(SUM(ISNULL(apcrd.PostageAmount, 0)), 2)
FROM		##AP_Cheque_Remit apcr
JOIN		##AP_Cheque_Remit_Detail apcrd
				ON	apcrd.AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID
JOIN		QSPCanadaCommon..TaxProvince taxProv
				ON	taxProv.Province_Code = apcrd.StateProvince
JOIN		QSPCanadaCommon..Tax tax
				ON	tax.Tax_ID = taxProv.Tax_ID
JOIN		GLAccountMap glAccMapFedTax
				ON	glAccMapFedTax.TaxID = tax.Tax_ID
				AND	glAccMapFedTax.GLEntryTypeID = 9 --9: Tax - GST/HST
				AND	glAccMapFedTax.BusinessUnitID = 1 --1: QSP
GROUP BY	RemitBatchID,
			CreationDate, 
			RemitCode, 
			FulfillmentHouseID, 
			ProductSortName, 
			CurrencyCode,
			Address1, 
			Address2, 
			City, 
			Province, 
			PostalCode, 
			CountryCode, 
			Comment

SET @AP_Cheque_Remit_ID = SCOPE_IDENTITY()

IF @@ERROR <> 0 
BEGIN
	ROLLBACK
	SET @ErrorMessage = 'Remit AP - Error - Failed to insert RemitAP log record for remit batch ' + CONVERT(VARCHAR(10), @RemitBatchID)
	RETURN
END

SET @GLDescriptionText = 'RemitAP - ' + CONVERT(VARCHAR(10), @RemitBatchID) + ' ' + CONVERT(VARCHAR(10), @RemitCode)

WHILE EXISTS(SELECT AP_Cheque_Remit_Detail_ID FROM ##AP_Cheque_Remit_Detail)
BEGIN

	SELECT		TOP 1
				@BusinessUnitID = BusinessUnitID
	FROM		##AP_Cheque_Remit_Detail
	ORDER BY	AP_Cheque_Remit_Detail_ID

	DECLARE @CountryCode	VARCHAR(2)
	SET @CountryCode = CASE @CurrencyCode
								WHEN 801 THEN 'CA'
								WHEN 802 THEN 'US'
							END

	EXEC	GL_Entry_Insert
			@Description = @GLDescriptionText,
			@CountryCode = @CountryCode,
			@APChequeRemitID = @AP_Cheque_Remit_ID,
			@BusinessUnitID = @BusinessUnitID,
			@GLEntryID = @GLEntryID OUTPUT

	INSERT	GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				ROUND(SUM(ISNULL(apcrd.NetAmount, 0) - ISNULL(apcrd.PostageAmount, 0)), 2),
				2
	FROM		##AP_Cheque_Remit_Detail apcrd
	JOIN		GLAccountMap glAccMap
					ON	1 = 1
	WHERE		glAccMap.GLEntryTypeID = 7 --7: Expense
	AND			apcrd.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	GROUP BY	glAccMap.GLAccountID,
				glAccMap.Debit

	--Create GL Transaction
	INSERT	GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				ROUND(SUM(ISNULL(apcrd.NetAmount, 0) + ISNULL(apcrd.FedTaxAmount, 0) + ISNULL(apcrd.ProvTaxAmount, 0)), 2),
				2
	FROM		##AP_Cheque_Remit_Detail apcrd
	JOIN		GLAccountMap glAccMap
					ON	1 = 1
	WHERE		glAccMap.GLEntryTypeID = 8 --8: Cash
	AND			glAccMap.CurrencyID = @CurrencyCode
	AND			apcrd.BusinessUnitID = @BusinessUnitID
	GROUP BY	glAccMap.GLAccountID,
				glAccMap.Debit
				
	--Create GL Transaction
	INSERT	GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				ROUND(SUM(apcrd.PostageAmount), 2),
				2
	FROM		##AP_Cheque_Remit_Detail apcrd
	JOIN		GLAccountMap glAccMap
					ON	1 = 1
	WHERE		glAccMap.GLEntryTypeID = 16 --16: Postage Remit Liability
	AND			apcrd.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	AND			apcrd.PostageAmount > 0.00
	GROUP BY	glAccMap.GLAccountID,
				glAccMap.Debit

	INSERT	GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				ROUND(SUM(apcrd.FedTaxAmount), 2),
				2
	FROM		##AP_Cheque_Remit_Detail apcrd
	JOIN		QSPCanadaCommon..TaxProvince taxProv
					ON	taxProv.Province_Code = apcrd.StateProvince
	JOIN		QSPCanadaCommon..Tax tax
					ON	tax.Tax_ID = taxProv.Tax_ID
	JOIN		GLAccountMap glAccMap
					ON	glAccMap.TaxID = tax.Tax_ID
	WHERE		glAccMap.GLEntryTypeID = 9 --9: Tax - GST/HST
	AND			apcrd.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	AND			apcrd.FedTaxAmount > 0.00
	GROUP BY	glAccMap.GLAccountID,
				glAccMap.Debit

	INSERT	GL_Transaction
	(
		GL_Entry_ID,
		GLAccountID,
		Debit_Credit,
		Amount,
		GL_Transaction_Status_ID
	)
	SELECT		@GLEntryID,
				glAccMap.GLAccountID,
				CASE glAccMap.Debit
					WHEN 0 THEN	'C'
					ELSE		'D'
				END,
				ROUND(SUM(apcrd.ProvTaxAmount), 2),
				2
	FROM		##AP_Cheque_Remit_Detail apcrd
	JOIN		QSPCanadaCommon..TaxProvince taxProv
					ON	taxProv.Province_Code = apcrd.StateProvince
	JOIN		QSPCanadaCommon..Tax tax
					ON	tax.Tax_ID = taxProv.Tax_ID
	JOIN		GLAccountMap glAccMap
					ON	glAccMap.TaxID = tax.Tax_ID
	WHERE		glAccMap.GLEntryTypeID = 10 --10: Tax - PST
	AND			apcrd.BusinessUnitID = @BusinessUnitID
	AND			glAccMap.BusinessUnitID = @BusinessUnitID
	AND			apcrd.ProvTaxAmount > 0.00
	GROUP BY	apcrd.BusinessUnitID,
				glAccMap.GLAccountID,
				glAccMap.Debit

	IF @@ERROR <> 0 --Error creating GL Transaction
	BEGIN
		ROLLBACK
		SET @ErrorMessage = 'Remit AP - Error - Failed to insert GL transaction record for remit batch ' + CONVERT(VARCHAR(10), @RemitBatchID)
		RETURN
	END

	DELETE	apcrd
	FROM	##AP_Cheque_Remit_Detail apcrd
	WHERE	apcrd.BusinessUnitID = @BusinessUnitID

END

COMMIT
GO
