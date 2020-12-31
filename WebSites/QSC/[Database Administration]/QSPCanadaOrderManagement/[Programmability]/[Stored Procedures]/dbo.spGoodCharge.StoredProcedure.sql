USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[spGoodCharge]    Script Date: 06/07/2017 09:20:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[spGoodCharge]
	@customerPaymentHeader int
as
	
	Update CreditCardPayment
		Set StatusInstance = 19000, AuthorizationCode = '', AuthorizationDate = GetDate(),
		AVSResponseCode = '',DateChanged = GetDate(),UserIDChanged = 'ADMI'
		Where customerpaymentheaderinstance = @customerPaymentHeader

	Update CustomerPaymentHeader
		Set StatusInstance = 600, DateChanged = GetDate(), UserIDChanged = 'ADMI'
		Where instance = @customerPaymentHeader

	Update customerorderdetail
		Set StatusInstance = 502, ChangeDate = '2004/9/21', ChangeUserID = 'ADMI',
		PriceA = Price, TaxA = Tax, Tax2A = Tax2
		Where customerorderheaderinstance In
			(select customerorderheaderinstance from CustomerPaymentHeader
				Where instance = @customerPaymentHeader)

	Update customerorderheader
		Set StatusInstance = 402, ChangeDate = '2004/9/21', ChangeUserID = 'ADMI'
		Where instance In
			(select customerorderheaderinstance from CustomerPaymentHeader
				Where instance = @customerPaymentHeader)
GO
