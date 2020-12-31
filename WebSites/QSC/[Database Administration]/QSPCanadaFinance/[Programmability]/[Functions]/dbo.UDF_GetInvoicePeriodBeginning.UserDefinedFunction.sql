USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_GetInvoicePeriodBeginning]    Script Date: 06/07/2017 09:17:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_GetInvoicePeriodBeginning]
(
	@InvoiceID		INT
)

RETURNS DATETIME

AS

BEGIN

DECLARE @ReturnValue DATETIME,
		@EarliestOrderDateOnInvoice DATETIME,
		@PriorLandedInvoiceDate DATETIME,
		@OrderQualifierID INT,
		@CampaignStartDate DATETIME,
		@InvoiceDate DATETIME

SELECT	@EarliestOrderDateOnInvoice = MIN(inv.Invoice_Date)
FROM	Invoice inv
WHERE	inv.PRINTED_INVOICE_ID = @InvoiceID

SELECT		@PriorLandedInvoiceDate = MAX(invPrior.Invoice_Date)
FROM		Invoice inv
JOIN		QSPCanadaOrderManagement..Batch b ON b.OrderID = inv.Order_ID
JOIN		QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
LEFT JOIN	(QSPCanadaOrderManagement..Batch bPrior 
			JOIN	Invoice invPrior ON invPrior.Order_ID = bPrior.OrderID)
			ON	bPrior.CampaignID = camp.ID
			AND	invPrior.Invoice_ID < inv.Invoice_ID
			AND	bPrior.OrderQualifierID IN (39001, 39002)
WHERE		inv.invoice_id = @InvoiceID

SELECT	@OrderQualifierID = b.OrderQualifierID,
		@CampaignStartDate = camp.StartDate,
		@InvoiceDate = inv.INVOICE_DATE
FROM	Invoice inv
JOIN	QSPCanadaOrderManagement..Batch b ON b.OrderID = inv.Order_ID
JOIN	QSPCanadaCommon..Campaign camp ON camp.ID = b.CampaignID
WHERE	inv.invoice_id = @InvoiceID

SELECT	@ReturnValue = CASE WHEN @OrderQualifierID IN (39001, 39002) THEN ISNULL(@PriorLandedInvoiceDate, CASE WHEN @EarliestOrderDateOnInvoice < @CampaignStartDate THEN @EarliestOrderDateOnInvoice ELSE @CampaignStartDate END) ELSE @InvoiceDate END

RETURN @ReturnValue

END
GO
