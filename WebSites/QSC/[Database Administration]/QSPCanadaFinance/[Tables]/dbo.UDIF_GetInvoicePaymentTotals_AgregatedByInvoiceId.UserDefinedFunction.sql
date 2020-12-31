USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceId]    Script Date: 06/07/2017 09:17:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Martinez
-- Create date: 2010-01-22
-- Description:	Calls UDIF_GetInvoicePaymentTotals and 
--				sums the amount agregating invoice id
-- Remarks:		To filter agregated values, join the table to
--				another table that has the list of invoices that you want
-- =============================================
CREATE FUNCTION [dbo].[UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceId] ()

RETURNS TABLE 

AS

RETURN 
(
	SELECT		InvoiceId, 
				SUM(PaymentAmount) AS PaymentAmount
	FROM		UDIF_GetInvoicePaymentTotals()
	GROUP BY	InvoiceId
)
GO
