USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetInvoiceDetails]    Script Date: 06/07/2017 09:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetInvoiceDetails]
(
	@DateTo	DATETIME
)

RETURNS TABLE

AS

RETURN
(
	SELECT			b.AccountID,
					b.CampaignID,
					b.OrderID,
					1 AS TransactionTypeID, --1: Invoice
					1 AS StatementDetailTypeID, --1: Catalog Activity
					inv.Invoice_ID AS InvoiceID,
					inv.Invoice_ID AS GroupingTransactionID,
					inv.Invoice_Date AS GroupingTransactionDate,
					CASE camp.Lang
						WHEN 'FR' THEN	'Facture'
						ELSE			'Invoice'
					END AS TransactionTypeName,
					inv.Invoice_ID AS TransactionID,
					inv.Invoice_Date AS TransactionDate,
					CONVERT(VARCHAR(MAX), NULL) AS Reference,
					ISNULL(inv.Invoice_Amount, 0) AS TransactionAmount
	FROM			Invoice inv
	JOIN			QSPCanadaOrderManagement..Batch b
						ON	b.OrderID = inv.Order_ID
	JOIN			QSPCanadaCommon..CAccount acc
						ON	acc.ID = b.AccountID
	JOIN			QSPCanadaCommon..Campaign camp
						ON	camp.ID = b.CampaignID
	WHERE			(inv.Invoice_Date < @DateTo
	OR				@DateTo IS NULL)
)
GO
