USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Statement_GetPaymentDetails]    Script Date: 06/07/2017 09:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[UDF_Statement_GetPaymentDetails]
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
					2 AS TransactionTypeID, --2: Payment
					1 AS StatementDetailTypeID, --1: Catalog Activity
					inv.Invoice_ID AS InvoiceID,
					inv.Invoice_ID AS GroupingTransactionID,
					pmt.Payment_Effective_Date AS GroupingTransactionDate,
					CASE camp.Lang
						WHEN 'FR' THEN	'Paiement'
						ELSE			'Payment'
					END AS TransactionTypeName,
					pmt.Payment_ID AS TransactionID,
					pmt.Payment_Effective_Date AS TransactionDate,
					CASE pmt.Payment_Method_ID 
						WHEN 50001 THEN '' --Other
						WHEN 50002 THEN CASE ISNULL(cp.CampaignID,0)
											WHEN 0 THEN	CASE camp.Lang
															WHEN 'FR' THEN	'Chèque #'
															ELSE			'Cheque #'
														END + pmt.Cheque_Number
											ELSE		CASE camp.Lang
															WHEN 'FR' THEN	CASE pmt.Cheque_Number WHEN 'CASH' THEN 'Comptant' ELSE 'Chèque' END
															ELSE			CASE pmt.Cheque_Number WHEN 'CASH' THEN 'Cash' ELSE 'Cheque' END
														END
										END
						WHEN 50003 THEN	'Visa'
						WHEN 50004 THEN	'Master Card'
					END AS Reference,
					ISNULL(-1 * pmt.Payment_Amount, 0) AS TransactionAmount
	FROM			Payment Pmt
	JOIN			QSPCanadaOrderManagement..Batch b
						ON	b.OrderID = pmt.Order_ID
	JOIN			QSPCanadaCommon..CAccount acc
						ON	acc.ID = b.AccountID
	JOIN			QSPCanadaCommon..Campaign camp
						ON	camp.ID = b.CampaignID
	LEFT JOIN		Invoice inv
						ON	inv.Order_ID = pmt.Order_ID
	LEFT JOIN		QSPCanadaCommon..CampaignProgram cp
						ON	cp.CampaignID = camp.ID
						AND	cp.DeletedTF = 0
						AND	cp.ProgramID = 47 --Group MagFS payments together
	WHERE			(pmt.Payment_Effective_Date < @DateTo
	OR				@DateTo IS NULL)
)
GO
