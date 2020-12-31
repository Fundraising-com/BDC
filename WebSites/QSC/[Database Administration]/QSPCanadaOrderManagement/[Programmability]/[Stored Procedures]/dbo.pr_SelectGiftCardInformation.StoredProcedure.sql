USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_SelectGiftCardInformation]    Script Date: 06/07/2017 09:20:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_SelectGiftCardInformation]
	@iCustomerOrderHeaderInstance int
AS

SELECT	ISNULL(ccp.CreditCardNumber, '') GiftCardCode,
		ISNULL(cph.TotalAmount, '') AmountApplied
FROM	CustomerOrderHeader coh
JOIN	CustomerPaymentHeader cph ON cph.CustomerOrderHeaderInstance = coh.Instance
JOIN	CreditCardPayment ccp ON ccp.CustomerPaymentHeaderInstance = cph.Instance
JOIN	Customer cust ON cust.Instance = coh.CustomerBillToInstance
JOIN	QSPCanadaCommon..CodeDetail cdPM ON cdPM.Instance = coh.PaymentMethodInstance
JOIN	QSPCanadaCommon..CodeDetail cdCCS ON cdCCS.Instance = ccp.StatusInstance
WHERE	coh.Instance = @iCustomerOrderHeaderInstance
AND		SUBSTRING(ccp.CreditCardNumber,1,1) = '9' --Gift Cards
GO