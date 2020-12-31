USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDIF_GetInvoicePaymentTotals]    Script Date: 06/07/2017 09:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Martinez
-- Create date: 2010-01-22
-- Description:	Lists all payments made
-- =============================================
CREATE FUNCTION [dbo].[UDIF_GetInvoicePaymentTotals]()

RETURNS TABLE 

AS

RETURN 
(

	SELECT		I.Invoice_ID AS InvoiceId, 
				CASE CD.Description
				WHEN 'Cheque/Cash'  Then 
					(CASE C.Lang
						WHEN 'EN' Then 'Cheque/Cash'
						WHEN 'FR' Then 'Chèque/Comptant'
						ELSE 'Cheque/Cash'
					END)
					ELSE CD.Description
				END 
				AS PaymentType,
				Payment_Amount AS PaymentAmount, 
				C.StartDate AS CampaignStartDate, 
				C.EndDate AS CampaignEndDate
	FROM		QSPCanadaOrderManagement..Batch B 
					INNER JOIN INVOICE I on B.OrderID = I.Order_ID
					INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
					INNER JOIN Payment P on P.Order_ID = I.Order_ID AND P.Account_ID = I.Account_ID  
					LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = P.payment_method_id

	UNION ALL
	
	SELECT	I.Invoice_ID AS InvoiceId, 
			CASE C.Lang
				WHEN 'EN' THEN
					(CASE B.OrderQualifierID
						WHEN 39009 Then 'Online profit'
						WHEN 39013 Then 'Customer Service profit'
						WHEN 39015 Then 'Customer Service profit'
					END)
				WHEN 'FR' THEN
					(CASE B.OrderQualifierID
						WHEN 39009 Then 'Profit en ligne'
						WHEN 39013 Then 'Profit de service à la clientèle'
						WHEN 39015 Then 'Profit de service à la clientèle'
					END)
			END
			AS PaymentType, 
			ISECTION.group_profit_amount AS PaymentAmount, 
			C.StartDate AS CampaignStartDate, 
			C.EndDate AS CampaignEndDate
	FROM	Invoice I 
			INNER JOIN Invoice invNonPrinted on invNonPrinted.Printed_Invoice_ID = I.Invoice_ID
			INNER JOIN QSPCanadaOrderManagement..Batch B ON B.OrderID = invNonPrinted.Order_ID
			INNER JOIN QSPCanadaCommon..Campaign C on C.ID = B.CampaignID
			INNER JOIN INVOICE_SECTION ISECTION on ISECTION.INVOICE_ID = invNonPrinted.INVOICE_ID
	WHERE	B.OrderQualifierID in (	39009,		-- Online profit
									39013,		-- Customer Service profit - Credit Card Reprocess
									39015 		-- Customer Service profit - CC Reprocessed to invoice
									)
)
GO
