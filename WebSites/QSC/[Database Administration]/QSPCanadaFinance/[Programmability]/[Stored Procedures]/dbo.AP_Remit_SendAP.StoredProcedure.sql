USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AP_Remit_SendAP]    Script Date: 06/07/2017 09:17:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AP_Remit_SendAP]

	@RemitBatchID				INT,
	@GenerateChequeFile			BIT,
	@RemitCode					VARCHAR(20) = NULL

AS

DECLARE	
	@RunDate  			DATETIME,
	@ErrorMessage 		VARCHAR(200),
	@SendEmailToIT		VARCHAR(1000)

SET @RunDate = GETDATE()

CREATE TABLE #AP_Cheque_Remit
(
	AP_Cheque_Remit_ID 	INT IDENTITY,
	RemitBatchID 		INT,
	CreationDate 		DATETIME,
	RemitCode			VARCHAR(20),
	FulfillmentHouseID	INT,
	ProductSortName		VARCHAR(55),
	CurrencyCode		INT, 
	Address1			VARCHAR(50),
	Address2			VARCHAR(50),
	City				VARCHAR(50),   
	Province			VARCHAR(2),
	PostalCode			VARCHAR(10),
	CountryCode			VARCHAR(2),
	Comment				VARCHAR(150)
)

CREATE TABLE #AP_Cheque_Remit_Detail
(
	AP_Cheque_Remit_Detail_ID	INT IDENTITY,
	AP_Cheque_Remit_ID			INT,
	BusinessUnitID				INT,
	StateProvince				VARCHAR(10),
	NetAmount					NUMERIC(14, 6),
	FedTaxAmount				NUMERIC(10, 6),
	ProvTaxAmount				NUMERIC(10, 6),
	PostageAmount				NUMERIC(11, 6)
)

DECLARE 	@ProductSeason 	CHAR(1)
DECLARE		@ProductYear	INT
EXEC		QSPCanadaOrderManagement..pr_RemitTest_GetCurrentSeason @ProductSeason OUTPUT, @ProductYear OUTPUT

INSERT INTO #AP_Cheque_Remit
(	
	RemitBatchID, 
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
)
SELECT	DISTINCT
		rb.RunID AS RemitBatchID,
		GETDATE() AS CreationDate,
		CASE SUBSTRING(prod.RemitCode, 1, 1) WHEN 'X' THEN SUBSTRING(prod.RemitCode, 2, LEN(prod.RemitCode)-1) ELSE prod.RemitCode END AS RemitCode,
		prod.Fulfill_House_Nbr,
		(SELECT		TOP 1
					QSPCanadaFinance.dbo.UDF_RemoveAccent(prod2.Product_Sort_Name, 0)
		 FROM		QSPCanadaProduct..Product prod2
		 WHERE		prod2.RemitCode = CASE SUBSTRING(prod.RemitCode, 1, 1) WHEN 'X' THEN SUBSTRING(prod.RemitCode, 2, LEN(prod.RemitCode)-1) ELSE prod.RemitCode END
		 AND		prod2.Product_Season = @ProductSeason
		 AND		prod2.Product_Year = @ProductYear
		 ORDER BY	prod2.Product_Sort_Name DESC) AS ProductSortName,
		(SELECT		TOP 1
					prod3.Currency
		 FROM		QSPCanadaProduct..Product prod3
		 WHERE		prod3.RemitCode = prod.RemitCode
		 AND		prod3.Product_Season = @ProductSeason
		 AND		prod3.Product_Year = @ProductYear
		 ORDER BY	prod3.Currency DESC) AS CurrencyCode,
		QSPCanadaFinance.dbo.UDF_RemoveAccent(fh.Ful_Addr_1, 0),
		QSPCanadaFinance.dbo.UDF_RemoveAccent(fh.Ful_Addr_2, 0),
		QSPCanadaFinance.dbo.UDF_RemoveAccent(fh.Ful_City, 0),
		QSPCanadaFinance.dbo.UDF_RemoveAccent(fh.Ful_State, 1),
		QSPCanadaFinance.dbo.UDF_RemoveAccent(fh.Ful_Zip, 1),
		QSPCanadaFinance.dbo.UDF_RemoveAccent(fh.CountryCode, 1),
		NULL
FROM	QSPCanadaProduct..Product prod
JOIN	QSPCanadaProduct..Fulfillment_House fh
			ON	fh.Ful_Nbr = prod.Fulfill_House_Nbr
JOIN	QSPCanadaOrderManagement..RemitBatch rb
			ON	rb.FulfillmentHouseNbr = prod.Fulfill_House_Nbr
WHERE	rb.RunID = @RemitBatchID
AND		prod.Product_Season = @ProductSeason
AND		prod.Product_Year = @ProductYear
AND		prod.RemitCode = ISNULL(@RemitCode, prod.RemitCode)
AND		CASE SUBSTRING(prod.RemitCode, 1, 1) WHEN 'X' THEN SUBSTRING(prod.RemitCode, 2, LEN(prod.RemitCode)-1) ELSE prod.RemitCode END IN 
							(SELECT	DISTINCT
									codrh.RemitCode
							FROM	QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
							JOIN	QSPCanadaOrderManagement..RemitBatch rb2
										ON	rb2.ID = codrh.RemitBatchID
							WHERE	rb2.RunID = @RemitBatchID
							AND		codrh.Status IN (42000, 42001)) --42000: Needs to be sent, 42001: Sent
AND		prod.Status IN (30600)
ORDER BY	CurrencyCode,
			ProductSortName

INSERT INTO #AP_Cheque_Remit_Detail
(
	AP_Cheque_Remit_ID,
	BusinessUnitID,
	StateProvince,
	NetAmount,
	FedTaxAmount,
	ProvTaxAmount,
	PostageAmount
)
SELECT		apcr.AP_Cheque_Remit_ID,
			acc.BusinessUnitID,
			crh.State,
			ISNULL(SUM((ISNULL(pd.BasePriceSansPostage, 0) * ISNULL(pd.BaseRemitRate, 0)) + (ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0))), 0) AS NetAmount,
			SUM(ISNULL(codrh.Tax, 0)) AS FedTaxAmount,
			SUM(ISNULL(codrh.Tax2, 0)) AS ProvTaxAmount,
			ISNULL(SUM(ISNULL(pd.PostageAmount, 0) * ISNULL(pd.PostageRemitRate, 0)), 0) AS PostageAmount
FROM		QSPCanadaOrderManagement..CustomerOrderDetailRemitHistory codrh
JOIN		QSPCanadaOrderManagement..CustomerRemitHistory crh
				ON	crh.Instance = codrh.CustomerRemitHistoryInstance
JOIN		QSPCanadaOrderManagement..RemitBatch rb
				ON	rb.ID = codrh.RemitBatchID
				AND	rb.runId = @RemitBatchID
JOIN		QSPCanadaOrderManagement..CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = codrh.CustomerOrderHeaderInstance
				AND	cod.TransID = codrh.TransID
JOIN		QSPCanadaProduct..Pricing_Details pd
				ON	pd.MagPrice_Instance = cod.PricingDetailsID
JOIN		QSPCanadaProduct..Product prod
				ON	prod.Product_Instance = pd.Product_Instance
JOIN		QSPCanadaOrderManagement..CustomerOrderHeader coh
				ON	coh.Instance = cod.CustomerOrderHeaderInstance
JOIN		QSPCanadaCommon..Campaign camp
				ON	camp.ID = coh.CampaignID
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		#AP_Cheque_Remit apcr
				ON	apcr.RemitCode = CASE SUBSTRING(prod.RemitCode, 1, 1) WHEN 'X' THEN SUBSTRING(prod.RemitCode, 2, LEN(prod.RemitCode)-1) ELSE prod.RemitCode END
WHERE		codrh.Status IN (42000, 42001) --42000: Needs to be sent, 42001: Sent
GROUP BY	apcr.AP_Cheque_Remit_ID,
			acc.BusinessUnitID,
			crh.State

DELETE	apcr
FROM	#AP_Cheque_Remit apcr
JOIN	#AP_Cheque_Remit_Detail apcrd
			ON	apcrd.AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID
WHERE	apcrd.NetAmount <= 0

DELETE	apcrd
FROM	#AP_Cheque_Remit_Detail apcrd
WHERE	apcrd.NetAmount <= 0

BEGIN TRANSACTION

IF EXISTS (	SELECT 1
			FROM tempdb..sysobjects
			WHERE type = 'U' and NAME = '##AP_Cheque_Remit')
BEGIN
	DROP TABLE [dbo].[##AP_Cheque_Remit]
END

CREATE TABLE ##AP_Cheque_Remit
(
	AP_Cheque_Remit_ID 	INT,
	RemitBatchID 		INT,
	CreationDate 		DATETIME,
	RemitCode			VARCHAR(20),
	FulfillmentHouseID	INT,
	ProductSortName		VARCHAR(55),
	CurrencyCode		INT, 
	Address1			VARCHAR(50),
	Address2			VARCHAR(50),
	City				VARCHAR(50),   
	Province			VARCHAR(2),
	PostalCode			VARCHAR(10),
	CountryCode			VARCHAR(2),
	Comment				VARCHAR(150)
)

IF EXISTS (	SELECT 1
			FROM tempdb..sysobjects
			WHERE type = 'U' and NAME = '##AP_Cheque_Remit_Detail')
BEGIN
	DROP TABLE [dbo].[##AP_Cheque_Remit_Detail]
END

CREATE TABLE ##AP_Cheque_Remit_Detail
(
	AP_Cheque_Remit_Detail_ID	INT,
	AP_Cheque_Remit_ID			INT,
	BusinessUnitID				INT,
	StateProvince				VARCHAR(10),
	NetAmount					NUMERIC(14, 2),
	FedTaxAmount				NUMERIC(10, 2),
	ProvTaxAmount				NUMERIC(10, 2),
	PostageAmount				NUMERIC(10, 2),
)

WHILE EXISTS(SELECT AP_Cheque_Remit_ID FROM #AP_Cheque_Remit)
BEGIN

	INSERT		##AP_Cheque_Remit
	SELECT		TOP 1 *
	FROM		#AP_Cheque_Remit
	ORDER BY	AP_Cheque_Remit_ID

	DELETE	apcr
	FROM	#AP_Cheque_Remit apcr
	JOIN	##AP_Cheque_Remit apcrStaging
				ON	apcrStaging.AP_Cheque_Remit_ID = apcr.AP_Cheque_Remit_ID

	INSERT		##AP_Cheque_Remit_Detail
	SELECT		apcrd.*
	FROM		#AP_Cheque_Remit_Detail apcrd
	JOIN		##AP_Cheque_Remit apcr
					ON	apcr.AP_Cheque_Remit_ID = apcrd.AP_Cheque_Remit_ID

	DELETE	apcrd
	FROM	#AP_Cheque_Remit_Detail apcrd
	JOIN	##AP_Cheque_Remit_Detail apcrdStaging
				ON	apcrdStaging.AP_Cheque_Remit_Detail_ID = apcrd.AP_Cheque_Remit_Detail_ID

	EXEC AP_Remit_CreateCheque @ErrorMessage OUTPUT

	DELETE	##AP_Cheque_Remit
	DELETE	##AP_Cheque_Remit_Detail

END

COMMIT

DROP TABLE #AP_Cheque_Remit
DROP TABLE #AP_Cheque_Remit_Detail

IF @GenerateChequeFile = CONVERT(BIT, 1)
BEGIN
	EXEC AP_Remit_CreateBatch
END
GO
