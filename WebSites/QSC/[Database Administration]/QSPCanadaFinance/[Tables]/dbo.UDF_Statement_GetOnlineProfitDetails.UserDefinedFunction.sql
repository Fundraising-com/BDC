USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetOnlineProfitDetails]    Script Date: 06/07/2017 09:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetOnlineProfitDetails]
(
	@DateTo	DATETIME
)

RETURNS TABLE

AS

RETURN
(
	SELECT		b.AccountID,
				b.CampaignID,
				invP.Order_ID AS OrderID,
				4 AS TransactionTypeID, --4: Internet Profit
				2 AS StatementDetailTypeID, --2: Internet Activity
				invP.Invoice_ID AS InvoiceID,
				invP.Invoice_ID AS GroupingTransactionID,
				invP.Invoice_Date AS GroupingTransactionDate,
				CASE camp.Lang
					WHEN 'FR' THEN	'Profit Internet'
					ELSE			'Internet profit'
				END AS TransactionTypeName,
				invO.Invoice_ID AS TransactionID,
				invO.Invoice_Date AS TransactionDate,
				CONVERT(VARCHAR(MAX), NULL) AS Reference,
				(	SELECT	-1 * SUM(invSec.Group_Profit_Amount)
					FROM	Invoice_Section invSec
					WHERE	invSec.Invoice_ID = invO.Invoice_ID) AS TransactionAmount
	FROM		Invoice invO
	LEFT JOIN	Invoice invP
					ON	invP.Invoice_ID = invO.Printed_Invoice_ID
					AND	(invP.Invoice_Date < @DateTo
					OR	@DateTo IS NULL)
	JOIN		QSPCanadaOrderManagement..Batch b
					ON	b.OrderID = invO.Order_ID
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.ID = b.AccountID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	WHERE		(invO.Invoice_Date < @DateTo
	OR			@DateTo IS NULL)
)
GO
