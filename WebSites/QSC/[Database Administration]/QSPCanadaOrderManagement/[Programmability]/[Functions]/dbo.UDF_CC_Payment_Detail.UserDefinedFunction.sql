USE [QSPCanadaOrderManagement]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_CC_Payment_Detail]    Script Date: 06/07/2017 09:21:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[UDF_CC_Payment_Detail](@OrderId Int, @DateFrom Datetime, @DateTo Datetime)

RETURNS TABLE
AS
RETURN
(

SELECT   top 10  dbo.CreditCardBatch.ID, dbo.Batch.OrderID, 
dbo.Batch.CampaignID, coh.AccountID, cph.TotalAmount,
(Case Batch.IsStaffOrder 
	When 0	 Then Round(TotalAmount,2)
	When 1	 Then Round(TotalAmount/2,2)
	End) AmountCharged,		
 dbo.Batch.IsStaffOrder, 
                      dbo.CreditCardPayment.AuthorizationCode, 
dbo.CreditCardPayment.AuthorizationDate, coh.PaymentMethodInstance, 
c.LastName, c.FirstName, 
c.Type AS CustomerType, Substring(dbo.CreditCardPayment.CreditCardNumber,1,4)+'********'+Substring(dbo.CreditCardPayment.CreditCardNumber,13,4) as CCNumber, 
dbo.CreditCardPayment.ReasonCode
FROM       dbo.CreditCardBatch INNER JOIN
                      dbo.CreditCardPayment ON dbo.CreditCardBatch.ID = dbo.CreditCardPayment.BatchID INNER JOIN
                      dbo.CustomerPaymentHeader cph ON dbo.CreditCardPayment.CustomerPaymentHeaderInstance = cph.Instance INNER JOIN
                      dbo.CustomerOrderHeader coh ON cph.CustomerOrderHeaderInstance = coh.Instance INNER JOIN
                      dbo.Batch ON coh.OrderBatchDate = dbo.Batch.[Date] AND coh.OrderBatchID = dbo.Batch.ID INNER JOIN
                      dbo.Customer c ON coh.CustomerBillToInstance = c.Instance
WHERE     (dbo.CreditCardBatch.IsGLDone = 1) AND (cph.IsCreditCard = 1) AND (dbo.CreditCardPayment.StatusInstance = 19000) OR
                      (dbo.CreditCardBatch.IsGLDone IS NULL) AND (cph.IsCreditCard = 1) AND (dbo.CreditCardPayment.StatusInstance = 19000)
And Batch.OrderId = IsNull(@OrderId,Batch.OrderId)
And Convert(varchar(10),dbo.CreditCardPayment.AuthorizationDate,101) >= Convert(varchar(10),@DateFrom, 101)
And Convert(varchar(10),dbo.CreditCardPayment.AuthorizationDate, 101) <= Convert(varchar(10),@DateTo,101)
ORDER BY dbo.CreditCardBatch.ID

)
GO
