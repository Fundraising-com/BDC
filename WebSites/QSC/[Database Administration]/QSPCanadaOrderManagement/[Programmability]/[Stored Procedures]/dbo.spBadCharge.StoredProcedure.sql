USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spBadCharge]    Script Date: 06/07/2017 09:20:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spBadCharge]
	@customerPaymentHeader int
as
	
	Update CreditCardPayment
		Set StatusInstance = 19001, AuthorizationCode = '', AuthorizationDate = '2004/9/21',
		AVSResponseCode = '',DateChanged = '2004/9/21',UserIDChanged = 'ADMI'
		Where customerpaymentheaderinstance = @customerPaymentHeader

	Update CustomerPaymentHeader
		Set StatusInstance = 600, DateChanged = GetDate(), UserIDChanged = 'ADMI'
		Where instance = @customerPaymentHeader

	Update customerorderdetail
		Set StatusInstance = 500, ChangeDate = '2004/9/21', ChangeUserID = 'ADMI',
		PriceA = Price, TaxA = Tax, Tax2A = Tax2
		Where customerorderheaderinstance In
			(select customerorderheaderinstance from CustomerPaymentHeader
				Where instance = @customerPaymentHeader)

	Update customerorderheader
		Set StatusInstance = 400, ChangeDate = '2004/9/21', ChangeUserID = 'ADMI'
		Where instance In
			(select customerorderheaderinstance from CustomerPaymentHeader
				Where instance = @customerPaymentHeader)
GO
