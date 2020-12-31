USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllPaymentsNotDeposited]    Script Date: 06/07/2017 09:17:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllPaymentsNotDeposited]  
			@PaymentMethodId Varchar(10)
AS

Declare @SQL Varchar(2000)
if  @PaymentMethodId  is null
Begin
 set @PaymentMethodId = 50002
end
Set @SQL = ' SELECT p.payment_id as Payment_id,
		           p.cheque_number,CONVERT(varchar(10), p.CHEQUE_DATE, 101) as cheque_date,
	  	           substring(cheque_payer,1,45) as cheque_payer,payment_amount 
   	           FROM dbo.DEPOSIT_ITEM di RIGHT OUTER JOIN dbo.PAYMENT p  ON di.PAYMENT_ID = p.PAYMENT_ID 
                       WHERE (di.BANK_DEPOSIT_ID IS NULL) 
	           AND CONVERT(varchar(10), p.CHEQUE_DATE, 101) >= Cast(''08/01/2004'' as datetime)
	           AND (p.PAYMENT_METHOD_ID)  =  '+ @PaymentMethodId   /* Exclude  Credit Card Payments*/

Exec (@SQL)
GO
