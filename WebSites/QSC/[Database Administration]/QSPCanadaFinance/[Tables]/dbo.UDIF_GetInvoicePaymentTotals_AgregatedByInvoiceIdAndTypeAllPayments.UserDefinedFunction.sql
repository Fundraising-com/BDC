USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceIdAndTypeAllPayments]    Script Date: 06/07/2017 09:17:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Martinez
-- Create date: 2010-01-22
-- Description:	Calls UDIF_GetInvoicePaymentTotals and 
--				sums the amount agregating invoice id and type
-- =============================================
CREATE FUNCTION [dbo].[UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceIdAndTypeAllPayments] ()

RETURNS TABLE 

AS

RETURN 
(
	SELECT		InvoiceId, 
				PaymentType, 
				SUM(PaymentAmount) AS PaymentAmount,
				Lang
	FROM		UDIF_GetInvoicePaymentTotalsAllPayments()
	GROUP BY	InvoiceId, PaymentType, Lang
)
GO
