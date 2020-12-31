USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetAdjustmentDetails]    Script Date: 06/07/2017 09:17:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetAdjustmentDetails]
(
	@DateTo	DATETIME
)

RETURNS TABLE

AS

RETURN
(
	SELECT			adj.Account_ID AS AccountID,
					adj.Campaign_ID AS CampaignID,
					adj.Order_ID AS OrderID,
					3 AS TransactionTypeID, --3: Adjustment
					3 AS StatementDetailTypeID, --3: Miscellaneous Adjustments
					inv.Invoice_ID AS InvoiceID,
					adj.Adjustment_ID AS GroupingTransactionID,
					adj.Adjustment_Effective_Date AS GroupingTransactionDate,
					CASE camp.Lang
						WHEN 'FR' THEN	COALESCE(adjT.French_Name, COALESCE(adjT.Name, ''))
						ELSE			COALESCE(adjT.Name, '')
					END AS TransactionTypeName,
					adj.Adjustment_ID AS TransactionID,
					adj.Adjustment_Effective_Date AS TransactionDate,
					CASE camp.Lang
						WHEN 'FR' THEN	'Commande #'
						ELSE			'Order #'
					END + CONVERT(VARCHAR, adj.Order_ID) AS Reference,
					CASE adj.Adjustment_Type_ID
						WHEN 49002 THEN ABS(adj.Adjustment_Amount) --NSF Check
						WHEN 49009 THEN ABS(adj.Adjustment_Amount) --Other Debit
						WHEN 49021 THEN ABS(adj.Adjustment_Amount) --Write off debit
						WHEN 49024 THEN ABS(adj.Adjustment_Amount) --Refund Check
						WHEN 49029 THEN	CASE
											WHEN COALESCE((SELECT TOP 1 adj2.Adjustment_Amount FROM Adjustment adj2 WHERE adj2.Campaign_ID = adj.Campaign_ID AND adj2.Adjustment_Type_ID = 49016), 0) > adj.Adjustment_Amount THEN 0
											ELSE																																											adj.Adjustment_Amount - coalesce((SELECT TOP 1 adj3.Adjustment_Amount FROM Adjustment adj3 WHERE adj3.Campaign_ID = adj.Campaign_ID AND adj3.Adjustment_Type_ID = 49016), 0)
										END
						ELSE			(-1 * adj.Adjustment_Amount)
					END AS TransactionAmount
	FROM			Adjustment adj
	JOIN			QSPCanadaCommon..CAccount acc
						ON	acc.ID = adj.Account_ID
	JOIN			QSPCanadaCommon..Campaign camp
						ON	camp.ID = adj.Campaign_ID
	LEFT JOIN		Adjustment_Type adjT
						ON	adjT.Adjustment_Type_ID = adj.Adjustment_Type_ID
	LEFT JOIN		QSPCanadaOrdermanagement..Batch b
						ON	b.OrderID = adj.Order_ID
	LEFT JOIN		Invoice inv
						ON	inv.Order_ID = b.OrderID
	WHERE			(adj.Adjustment_Effective_Date < @DateTo
	OR				@DateTo IS NULL)
	AND				adjT.ExcludeFromInvoicing = 0
)
GO
