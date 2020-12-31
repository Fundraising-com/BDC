USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetInvoicePaymentTotals]    Script Date: 10/06/2009 17:42:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetInvoicePaymentTotals]
	@InvoiceID 	int
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 6/7/2004 
--   Get Invoice Payment Totals For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON


	-- Get credit card amounts
	SELECT		CASE CD.Description
				WHEN 'Cheque/Cash'  Then 
					(CASE MAX(C.Lang)
						WHEN 'EN' Then 'Cheque/Cash'       --English
						WHEN 'FR' Then 'Chèque/Comptant'   --French
						ELSE 'Cheque/Cash'
					END)
				ELSE CD.Description
				END AS PaymentType,
				SUM(Payment_Amount) AS PaymentAmount
	INTO		#myTempTable
	FROM		QSPCanadaOrderManagement..Batch B 
					INNER JOIN  INVOICE I on B.OrderID = I.Order_ID
					INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
					INNER JOIN Payment P on P.Order_ID = I.Order_ID AND P.Account_ID = I.Account_ID  
					LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = P.payment_method_id
	WHERE		Invoice_ID = @InvoiceID
	GROUP BY	CD.Description


	-- Get language
	DECLARE @Language VARCHAR(MAX)
	SET @Language = (	SELECT	C.Lang
						FROM	QSPCanadaOrderManagement..Batch B 
								INNER JOIN  INVOICE I on B.OrderID = I.Order_ID
								INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
						WHERE	Invoice_ID = @InvoiceID
						)
	
	-- Get Internet amounts
	DECLARE @InternetTotal NUMERIC(10,2)
	SET @InternetTotal = dbo.UDF_GetInvoicePaymentTotalsFromOrderQualifierId(@InvoiceID, 39009)	-- Internet
	
	IF (@InternetTotal > 0)
	BEGIN
		if (@Language = 'EN')
			BEGIN
				INSERT INTO #myTempTable VALUES ('Online profit', @InternetTotal)
			END
		ELSE if (@Language = 'FR')
			BEGIN
				INSERT INTO #myTempTable VALUES ('Bénéfice en ligne', @InternetTotal)
			END
	END


	-- Get Customer Service amounts
	DECLARE @CustomerServiceTotal NUMERIC(10,2)
	DECLARE @CustomerServiceTotal_1 NUMERIC(10,2)
	DECLARE @CustomerServiceTotal_2 NUMERIC(10,2)
	SET @CustomerServiceTotal_1 = dbo.UDF_GetInvoicePaymentTotalsFromOrderQualifierId(@InvoiceID, 39013)	-- Credit Card Reprocess
	SET @CustomerServiceTotal_2 = dbo.UDF_GetInvoicePaymentTotalsFromOrderQualifierId(@InvoiceID, 39015)	-- CC Reprocessed to invoice
	SET @CustomerServiceTotal = @CustomerServiceTotal_1 + @CustomerServiceTotal_2
	
	IF (@CustomerServiceTotal > 0)
	BEGIN
		if (@Language = 'EN')
			BEGIN
				INSERT INTO #myTempTable VALUES ('Customer Service profit', @CustomerServiceTotal)
			END
		ELSE if (@Language = 'FR')
			BEGIN
				INSERT INTO #myTempTable VALUES ('Bénéfice de service à la clientèle', @CustomerServiceTotal)
			END
	END
	

	-- Return results
	SELECT * FROM #myTempTable


SET NOCOUNT OFF