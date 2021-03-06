USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_ResolveCreditCardRefund_SelectAll]    Script Date: 06/07/2017 09:20:31 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_ResolveCreditCardRefund_SelectAll] AS
SELECT	rccr.Price,
		rccr.CreditCardNumber,
		rccr.ExpirationMonth,
		rccr.ExpirationYear,
		rccr.PaymentMethodInstance,
		rccr.FirstName,
		rccr.LastName,
		rccr.SubsCount,
		rccr.RefundStatus
FROM		ResolveCreditCardRefund rccr

-- Only take unprocessed ones
WHERE	rccr.RefundStatus = 0


ORDER BY	rccr.CreditCardNumber
GO
