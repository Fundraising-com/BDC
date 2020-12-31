USE [QSPCanadaFinance]
GO

/****** Object:  StoredProcedure [dbo].[Invoice_LinkNonPrintedToPrinted]    Script Date: 10/06/2009 17:58:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Invoice_LinkNonPrintedToPrinted]

	@Invoice_ID	INT

AS

DECLARE @IsPrintInvoice	BIT,
		@CampaignID		INT

SELECT	@IsPrintInvoice = CONVERT(BIT, 1),
		@CampaignID = batch.CampaignID
FROM	Invoice inv
JOIN	QSPCanadaOrderManagement..Batch batch
			ON	batch.OrderID = inv.Order_ID
WHERE	inv.Invoice_ID = @Invoice_ID
AND		batch.OrderQualifierID NOT IN (39009, 39013, 39015) --39009: Internet 39013: Credit Card Reprocess 39015: CC Reprocessed to invoice

IF @IsPrintInvoice = 1
BEGIN

	CREATE TABLE #NonPrintInvoicesToLink
	(
		NonPrintInvoiceID	INT
	)

	INSERT INTO #NonPrintInvoicesToLink
	SELECT	invNonPrint.Invoice_ID AS NonPrintInvoiceID
	FROM	Invoice invNonPrint
	JOIN	QSPCanadaOrderManagement..Batch batch
				ON	batch.OrderID = invNonPrint.Order_ID
	WHERE	batch.CampaignID = @CampaignID
	AND		batch.OrderQualifierID IN (39009, 39013, 39015) --39009: Internet 39013: Credit Card Reprocess 39015: CC Reprocessed to invoice
	AND		invNonPrint.Printed_Invoice_ID IS NULL --Hasn't yet been linked to an existing Printed Invoice

	UPDATE	invUpdate
	SET		Printed_Invoice_ID = @Invoice_ID
	FROM	Invoice invUpdate
	JOIN	#NonPrintInvoicesToLink npil
				ON	npil.NonPrintInvoiceID = invUpdate.Invoice_ID

END



GO


