USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetDetails_WithBusLogic]    Script Date: 06/07/2017 09:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetDetails_WithBusLogic]
(
	@DateTo	DATETIME
)

RETURNS TABLE

AS

RETURN
(
	SELECT			invDet.AccountID,
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
					invDet.TransactionAmount,
					b.Date AS OrderDate
	FROM			Invoice inv
	JOIN			QSPCanadaOrderManagement..Batch b
						ON	b.OrderID = inv.Order_ID
	JOIN			dbo.UDF_Statement_GetInvoiceDetails(@DateTo) invDet
						ON	invDet.InvoiceID = inv.Invoice_ID
	WHERE			(b.OrderQualifierID IN (39001, 39002, 39003, 39005, 39006, 39007, 39016, 39017, 39018, 39019, 39020, 39021, 39022, 39023) --39001: Main, 39002: Supplement, 39003: Staff, 39005: Problem Solver, 39020: Customer Service to Invoice
	OR				b.OrderQualifierID IN (39013, 39015) AND inv.Invoice_Date < = '2006-11-09') --39013: CC Reprocess, 39015: CC Reprocessed to invoice
	--AND				b.OrderTypeCode NOT IN (41006, 41007, 41009, 41011) --41006: FM, 41007: FMBULK, 41009: MAGNET, 41011: FMCLOSEOUT

	UNION ALL

	SELECT			pmtDet.AccountID,
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
					pmtDet.TransactionAmount,
					b.Date AS OrderDate
	FROM			Payment pmt
	JOIN			QSPCanadaOrderManagement..Batch b
						ON	b.OrderID = pmt.Order_ID
	JOIN			QSPCanadaCommon..CAccount acc
						ON	acc.ID = b.AccountID
	JOIN			QSPCanadaCommon..Campaign camp
						ON	camp.ID = b.CampaignID
	JOIN			Invoice inv
						ON	inv.Order_ID = pmt.Order_ID
	JOIN			dbo.UDF_Statement_GetPaymentDetails(@DateTo) pmtDet
						ON	pmtDet.TransactionID = pmt.Payment_ID
	WHERE			(b.OrderQualifierID IN (39001, 39002, 39003, 39005, 39006, 39007, 39016, 39017, 39018, 39019, 39020, 39021, 39022, 39023) --39001: Main, 39002: Supplement, 39003: Staff, 39005: Problem Solver, 39020: Customer Service to Invoice
	OR				b.OrderQualifierID IN (39013, 39015) AND inv.Invoice_Date < = '2006-11-09') --39013: CC Reprocess, 39015: CC Reprocessed to invoice
	--AND				b.OrderTypeCode NOT IN (41006, 41007, 41009, 41011) --41006: FM, 41007: FMBULK, 41009: MAGNET, 41011: FMCLOSEOUT

	UNION ALL

	SELECT			adjDet.AccountID,
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
					adjDet.TransactionAmount,
					b.Date AS OrderDate
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
	JOIN			dbo.UDF_Statement_GetAdjustmentDetails(@DateTo) adjDet
						ON	adjDet.TransactionID = adj.Adjustment_ID
	/*WHERE			(ISNULL(b.OrderTypeCode, -1) NOT IN (41006, 41007, 41009, 41011) --41006: FM, 41007: FMBULK, 41009: MAGNET, 41011: FMCLOSEOUT
	OR				adj.Adjustment_Type_ID = 49024) --49024: Refund Cheque (Debit)
	AND				adj.Adjustment_Type_ID NOT IN (49016)*/ --49016: Magnet Postage Cost (Debit)

	UNION ALL

	SELECT		onlDet.AccountID,
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
				onlDet.TransactionAmount,
				b.Date AS OrderDate
	FROM		Invoice invO
	LEFT JOIN	Invoice invP
					ON	invP.Invoice_ID = invO.Printed_Invoice_ID
	JOIN		QSPCanadaOrderManagement..Batch b
					ON	b.OrderID = invO.Order_ID
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.ID = b.AccountID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	JOIN		dbo.UDF_Statement_GetOnlineProfitDetails(@DateTo) onlDet
					ON	onlDet.TransactionID = invO.Invoice_ID
	WHERE		b.OrderQualifierID = 39009 --39009: Online
	AND			b.Date >= '2009-07-01'
	AND			onlDet.TransactionAmount <> 0.00

	UNION ALL

	SELECT		custSvcDet.AccountID,
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
				custSvcDet.TransactionAmount,
				b.Date AS OrderDate
	FROM		Invoice invO
	LEFT JOIN	Invoice invP
					ON	invP.Invoice_ID = invO.Printed_Invoice_ID
	JOIN		QSPCanadaOrderManagement..Batch b
					ON	b.OrderID = invO.Order_ID
	JOIN		QSPCanadaCommon..CAccount acc
					ON	acc.ID = b.AccountID
	JOIN		QSPCanadaCommon..Campaign camp
					ON	camp.ID = b.CampaignID
	JOIN		dbo.UDF_Statement_GetCustSvcProfitDetails(@DateTo) custSvcDet
					ON	custSvcDet.TransactionID = invO.Invoice_ID
	WHERE		b.OrderQualifierID IN (39013, 39015) --39013: CC Reprocess, 39015: CC Reprocessed to invoice
	AND			b.Date >= '2009-07-01'
	AND			custSvcDet.TransactionAmount <> 0.00
)
GO
