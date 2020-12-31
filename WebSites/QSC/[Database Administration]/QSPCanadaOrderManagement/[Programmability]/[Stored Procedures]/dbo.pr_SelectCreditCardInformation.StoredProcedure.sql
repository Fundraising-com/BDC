USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectCreditCardInformation]    Script Date: 06/07/2017 09:20:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectCreditCardInformation]
	@iCustomerOrderHeaderInstance int
AS
DECLARE @iCustomerPaymentHeaderInstance 	int,
	@iPaymentMethodID			int, 
	@sPaymentMethod			varchar(125), 
	@sCreditCardNumber			varchar(125), 
	@sExpirationDate			varchar(125),
	@sAuthorizationCode			varchar(125),
	@sCardholderName			varchar(125),
	@sReturnCode				varchar(125),
	@sCreditCardStatus			varchar(64),
	@dAmount					numeric(10,2)

SELECT @iCustomerPaymentHeaderInstance = ccp.CustomerPaymentHeaderInstance,
	 @sCreditCardNumber=ccp.CreditCardNumber, 
	 @sExpirationDate=ccp.ExpirationDate,
	 @sAuthorizationCode=ccp.AuthorizationCode,
	 @sCardholderName=c.FirstName+' '+c.LastName,
	 @sReturnCode=ccp.AVSResponseCode,
	 @sCreditCardStatus=cd.Description,
	 @dAmount=cph.TotalAmount
  FROM	 CreditCardPayment ccp,
	 CustomerPaymentHeader cph,
	 CustomerOrderHeader coh,
	 Customer c,
	 CodeDetail cd
WHERE ccp.CustomerPaymentHeaderInstance = cph.Instance AND
	 cph.CustomerOrderHeaderInstance = @iCustomerOrderHeaderInstance AND
	 cph.CustomerOrderHeaderInstance = coh.Instance AND
	 coh.CustomerBillToInstance = c.Instance AND
	 cd.Instance = ccp.StatusInstance


SELECT @sPaymentMethod = cd.Description,
	 @iPaymentMethodID = coh.PaymentMethodInstance
  FROM CustomerOrderHeader coh,
       CodeDetail cd
 WHERE coh.PaymentMethodInstance = cd.Instance AND
       coh.Instance = @iCustomerOrderHeaderInstance


SELECT  coalesce(@sPaymentMethod,'') AS PaymentMethod,
	  coalesce(@iPaymentMethodID,'') AS PaymentMethodID,
               coalesce(@sCreditCardNumber,'') AS CreditCardNumber, 
	 coalesce(@sExpirationDate,'') AS ExpirationDate,
	 coalesce(@sAuthorizationCode,'') AS AuthorizationCode,
	 coalesce(@sCardholderName,'') AS CardholderName,
	 coalesce(@sReturnCode,'') AS ReturnCode,
	 coalesce(@iCustomerPaymentHeaderInstance,'') AS CustomerPaymentHeaderInstance,
	 coalesce(@sCreditCardStatus, '') AS CreditCardStatus,
	 coalesce(@dAmount,0.00) AS Amount
GO
