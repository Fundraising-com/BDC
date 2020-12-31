USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetDetails_Aggregated]    Script Date: 06/07/2017 09:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetDetails_Aggregated]
(
	@DateTo	DATETIME
)

RETURNS TABLE

AS

RETURN
(
	SELECT		StatementID,
				AccountID,
				CampaignID,
				StatementDetailTypeID,
				OrderID,
				InvoiceID,
				GroupingTransactionID,
				CONVERT(DATETIME, CONVERT(VARCHAR, GroupingTransactionDate, 112)) AS TransactionDate,
				TransactionTypeID,
				TransactionTypeName,
				Reference,
				SUM(TransactionAmount) AS TransactionAmount
	FROM		dbo.UDF_Statement_GetDetails(@DateTo)
	GROUP BY	StatementID,
				AccountID,
				CampaignID,
				StatementDetailTypeID,
				OrderID,
				InvoiceID,
				GroupingTransactionID,
				CONVERT(DATETIME, CONVERT(VARCHAR, GroupingTransactionDate, 112)),
				TransactionTypeID,
				TransactionTypeName,
				Reference
)
GO
