USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetDetails_WithBusLogic_Aggregated]    Script Date: 06/07/2017 09:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetDetails_WithBusLogic_Aggregated]
(
	@DateFrom	DATETIME,
	@DateTo		DATETIME
)

RETURNS TABLE

AS

RETURN
(
	SELECT		AccountID,
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
	FROM		dbo.UDF_Statement_GetDetails_WithBusLogic(@DateTo)
	WHERE		GroupingTransactionDate >= @DateFrom
	OR			GroupingTransactionDate IS NULL
	OR			@DateFrom IS NULL
	GROUP BY	AccountID,
				CampaignID,
				StatementDetailTypeID,
				OrderID,
				InvoiceID,
				GroupingTransactionID,
				CONVERT(DATETIME, CONVERT(VARCHAR, GroupingTransactionDate, 112)),
				TransactionTypeID,
				TransactionTypeName,
				Reference
	
	UNION ALL
	
	SELECT		AccountID,
				CampaignID,
				1,--StatementDetailTypeID,
				NULL AS OrderID,
				NULL AS InvoiceID,
				NULL AS GroupingTransactionID,
				@DateFrom AS TransactionDate,
				NULL AS TransactionTypeID,
				'Balance Forward' AS TransactionTypeName,
				NULL AS Reference,
				SUM(TransactionAmount) AS TransactionAmount
	FROM		dbo.UDF_Statement_GetDetails_WithBusLogic(@DateTo)
	WHERE		GroupingTransactionDate < @DateFrom
	GROUP BY	AccountID,
				CampaignID--,
				--StatementDetailTypeID
)
GO
