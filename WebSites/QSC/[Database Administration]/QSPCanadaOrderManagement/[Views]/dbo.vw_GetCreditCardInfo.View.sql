USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_GetCreditCardInfo]    Script Date: 06/07/2017 09:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_GetCreditCardInfo]
AS
SELECT     b.OrderID, coh.Instance AS CustomerOrderHeaderInstance, c.FirstName + '  ' + c.LastName AS CardholderName, 
                      ccp.CreditCardNumber AS CreditCardNumber, ccp.ExpirationDate AS ExpirationDate, ccp.AuthorizationCode AS AuthorizationCode, 
                      ccp.AVSResponseCode AS ReturnCode, c.FirstName,c.LastName, coh.CreationDate AS OrderDate
FROM         dbo.CreditCardPayment ccp INNER JOIN
                      dbo.CustomerPaymentHeader cph ON ccp.CustomerPaymentHeaderInstance = cph.Instance INNER JOIN
                      dbo.CustomerOrderHeader coh ON cph.CustomerOrderHeaderInstance = coh.Instance INNER JOIN
                      dbo.Customer c ON coh.CustomerBillToInstance = c.Instance INNER JOIN
                      dbo.Batch b ON coh.OrderBatchID = b.ID AND coh.OrderBatchDate = b.[Date]
GO
