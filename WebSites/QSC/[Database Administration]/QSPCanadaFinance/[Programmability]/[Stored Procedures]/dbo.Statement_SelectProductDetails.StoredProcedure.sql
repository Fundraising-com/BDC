USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[Statement_SelectProductDetails]    Script Date: 06/07/2017 09:17:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[Statement_SelectProductDetails]

	@CampaignID		INT,
	@Realtime		BIT,
	@DateTo			DATETIME, 
	@StatementID	INT = NULL

WITH RECOMPILE
	
AS

IF @Realtime = CONVERT(BIT, 1)
	BEGIN

		SELECT		--od.Invoice_ID,
					od.ProductCode,
					od.ProductName,
					od.CatalogPrice,
					od.IsQSPExclusive,
					od.SectionType,
					od.ProductType,
					od.Lang,
					od.OrderQualifierID,
					od.InvoiceGridID,
					od.NumIssues,
					od.ProductTypeName,
					od.Price,					
					SUM(od.QTYOrdered) QTYOrdered,
					SUM(od.QuantityShipped) QuantityShipped,
					SUM(od.TotalPrice) TotalPrice,
					SUM(od.PostageAmount) PostageAmount 
		FROM		UDF_Invoice_GetOrderDetails() od
		WHERE		od.Invoice_ID IN	(SELECT		invNonPrint.Invoice_ID
										FROM		Invoice invNonPrint
										JOIN		QSPCanadaOrderManagement..Batch b
														ON	b.OrderID = invNonPrint.Order_ID
										LEFT JOIN	Invoice invPrint
														ON	invPrint.Invoice_ID = invNonPrint.Printed_Invoice_ID
										WHERE		b.CampaignID = @CampaignID
										AND			b.OrderQualifierID IN (39009, 39013, 39015) --39009: Internet 39013: Credit Card Reprocess 39015: CC Reprocessed to invoice
										AND			(invNonPrint.Printed_Invoice_ID IS NULL --Don't show details on statement if already included in invoice
										OR			invPrint.Invoice_Effective_Date > ISNULL(@DateTo, GETDATE()))) --Even though now applied to an invoice, may not have been the case at time of DateTo
		GROUP BY	od.ProductCode,
					od.ProductName,
					od.CatalogPrice,
					od.IsQSPExclusive,
					od.SectionType,
					od.ProductType,
					od.Lang,
					od.OrderQualifierID,
					od.InvoiceGridID,
					od.NumIssues,
					od.ProductTypeName,
					od.Price			
		ORDER BY	od.OrderQualifierID,
					od.ProductName

	END
ELSE
	BEGIN
		DECLARE	@StatementDate		DATETIME

		IF ISNULL(@StatementID, 0) = 0
			BEGIN
				SELECT	@StatementID = MAX(StatementID)
				FROM	[Statement]
				WHERE	CampaignID = @CampaignID
			END

		SELECT	@StatementDate = CONVERT(VARCHAR(10), DATEADD(DAY, 1, stat.StatementDate), 120) --include transactions until midnight of the statement date
		FROM	[Statement] stat
		WHERE	stat.StatementID = @StatementID

		SELECT		--od.Invoice_ID,
					od.ProductCode,
					od.ProductName,
					od.CatalogPrice,
					od.IsQSPExclusive,
					od.SectionType,
					od.ProductType,
					od.Lang,
					od.OrderQualifierID,
					od.InvoiceGridID,
					od.NumIssues,
					od.ProductTypeName,
					od.Price,
					SUM(od.QTYOrdered) QTYOrdered,
					SUM(od.QuantityShipped) QuantityShipped,
					SUM(od.TotalPrice) TotalPrice,
					SUM(od.PostageAmount) PostageAmount 
		FROM		UDF_Invoice_GetOrderDetails() od
		WHERE		od.Invoice_ID IN	(SELECT		InvoiceID
										FROM		StatementInvoiceOnline statInvO
										JOIN		Invoice invNonPrintOnline
														ON	invNonPrintOnline.Invoice_ID = statInvO.InvoiceID
										LEFT JOIN	Invoice invPrintOnline
														ON	invPrintOnline.Invoice_ID = invNonPrintOnline.Printed_Invoice_ID
										WHERE		statInvO.StatementID = @StatementID
										AND			(invNonPrintOnline.Printed_Invoice_ID IS NULL --Don't show details on statement if already included in invoice
										OR			invPrintOnline.Invoice_Effective_Date >= @StatementDate)  --Even though now applied to an invoice, may not have been the case at time of Statement

										UNION ALL

										(SELECT		InvoiceID
										FROM		StatementInvoiceCustSvc statInvCS
										JOIN		Invoice invNonPrintCustSvc
														ON	invNonPrintCustSvc.Invoice_ID = statInvCS.InvoiceID
										LEFT JOIN	Invoice invPrintCustSvc
														ON	invPrintCustSvc.Invoice_ID = invNonPrintCustSvc.Printed_Invoice_ID
										WHERE		statInvCS.StatementID = @StatementID
										AND			(invNonPrintCustSvc.Printed_Invoice_ID IS NULL --Don't show details on statement if already included in invoice
										OR			invPrintCustSvc.Invoice_Effective_Date >= @StatementDate))) --Even though now applied to an invoice, may not have been the case at time of Statement
		GROUP BY	od.ProductCode,
					od.ProductName,
					od.CatalogPrice,
					od.IsQSPExclusive,
					od.SectionType,
					od.ProductType,
					od.Lang,
					od.OrderQualifierID,
					od.InvoiceGridID,
					od.NumIssues,
					od.ProductTypeName,
					od.Price			
		ORDER BY	od.OrderQualifierID,
					od.ProductName
								
	END
GO
