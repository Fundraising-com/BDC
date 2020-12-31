USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceIdAndType]    Script Date: 06/07/2017 09:17:43 ******/
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
CREATE FUNCTION [dbo].[UDIF_GetInvoicePaymentTotals_AgregatedByInvoiceIdAndType] ()

RETURNS TABLE 

AS

RETURN 
(
	SELECT		InvoiceId, 
				PaymentType, 
				SUM(PaymentAmount) AS PaymentAmount
	FROM		UDIF_GetInvoicePaymentTotals()
	GROUP BY	InvoiceId, PaymentType
)
GO
