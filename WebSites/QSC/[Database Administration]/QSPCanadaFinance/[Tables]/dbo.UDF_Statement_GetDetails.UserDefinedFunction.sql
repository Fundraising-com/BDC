USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetDetails]    Script Date: 06/07/2017 09:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetDetails]
(
	@DateTo	DATETIME
)

RETURNS TABLE

AS

RETURN
(
	SELECT	statInv.StatementID,
			statInv.StatementInvoiceID AS StatementDetailID,
			invDet.AccountID,
			invDet.CampaignID,
			invDet.OrderID,
			invDet.TransactionTypeID,
			invDet.StatementDetailTypeID,
			invDet.InvoiceID,
			invDet.GroupingTransactionID,
			invDet.GroupingTransactionDate,
			invDet.TransactionTypeName,
			invDet.TransactionID,
			invDet.TransactionDate,
			invDet.Reference,
			invDet.TransactionAmount
	FROM	dbo.UDF_Statement_GetInvoiceDetails(@DateTo) invDet
	JOIN	StatementInvoice statInv
				ON	statInv.InvoiceID = invDet.TransactionID

	UNION ALL

	SELECT	statPmt.StatementID,
			statPmt.StatementPaymentID AS StatementDetailID,
			pmtDet.AccountID,
			pmtDet.CampaignID,
			pmtDet.OrderID,
			pmtDet.TransactionTypeID,
			pmtDet.StatementDetailTypeID,
			pmtDet.InvoiceID,
			pmtDet.GroupingTransactionID,
			pmtDet.GroupingTransactionDate,
			pmtDet.TransactionTypeName,
			pmtDet.TransactionID,
			pmtDet.TransactionDate,
			pmtDet.Reference,
			pmtDet.TransactionAmount
	FROM	dbo.UDF_Statement_GetPaymentDetails(@DateTo) pmtDet
	JOIN	StatementPayment statPmt
				ON	statPmt.PaymentID = pmtDet.TransactionID

	UNION ALL

	SELECT	statAdj.StatementID,
			statAdj.StatementAdjustmentID AS StatementDetailID,
			adjDet.AccountID,
			adjDet.CampaignID,
			adjDet.OrderID,
			adjDet.TransactionTypeID,
			adjDet.StatementDetailTypeID,
			adjDet.InvoiceID,
			adjDet.GroupingTransactionID,
			adjDet.GroupingTransactionDate,
			adjDet.TransactionTypeName,
			adjDet.TransactionID,
			adjDet.TransactionDate,
			adjDet.Reference,
			adjDet.TransactionAmount
	FROM	dbo.UDF_Statement_GetAdjustmentDetails(@DateTo) adjDet
	JOIN	StatementAdjustment statAdj
				ON	statAdj.AdjustmentID = adjDet.TransactionID

	UNION ALL

	SELECT	statInvOnl.StatementID,
			statInvOnl.StatementInvoiceOnlineID AS StatementDetailID,
			onlDet.AccountID,
			onlDet.CampaignID,
			onlDet.OrderID,
			onlDet.TransactionTypeID,
			onlDet.StatementDetailTypeID,
			onlDet.InvoiceID,
			onlDet.GroupingTransactionID,
			onlDet.GroupingTransactionDate,
			onlDet.TransactionTypeName,
			onlDet.TransactionID,
			onlDet.TransactionDate,
			onlDet.Reference,
			onlDet.TransactionAmount
	FROM	dbo.UDF_Statement_GetOnlineProfitDetails(@DateTo) onlDet
	JOIN	StatementInvoiceOnline statInvOnl
				ON	statInvOnl.InvoiceID = onlDet.TransactionID

	UNION ALL

	SELECT	statInvCustSvc.StatementID,
			statInvCustSvc.StatementInvoiceCustSvcID AS StatementDetailID,
			custSvcDet.AccountID,
			custSvcDet.CampaignID,
			custSvcDet.OrderID,
			custSvcDet.TransactionTypeID,
			custSvcDet.StatementDetailTypeID,
			custSvcDet.InvoiceID,
			custSvcDet.GroupingTransactionID,
			custSvcDet.GroupingTransactionDate,
			custSvcDet.TransactionTypeName,
			custSvcDet.TransactionID,
			custSvcDet.TransactionDate,
			custSvcDet.Reference,
			custSvcDet.TransactionAmount
	FROM	dbo.UDF_Statement_GetCustSvcProfitDetails(@DateTo) custSvcDet
	JOIN	StatementInvoiceCustSvc statInvCustSvc
				ON	statInvCustSvc.InvoiceID = custSvcDet.TransactionID
)
GO
