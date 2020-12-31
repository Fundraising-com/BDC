USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetPaymentAmountForOrder]    Script Date: 06/07/2017 09:17:19 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetPaymentAmountForOrder]
	@OrderID 		int	
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MTC 4/23/2004 
--   Get Payment Amount For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

SELECT ISNULL(SUM(Payment_Amount),0) as PaymentAmount, Description as PaymentMethod
FROM Payment
LEFT JOIN QSPCanadaCommon..CodeDetail CD on CD.Instance = Payment_Method_ID 
WHERE Order_ID = @OrderID
GROUP BY Description

SET NOCOUNT OFF
GO
