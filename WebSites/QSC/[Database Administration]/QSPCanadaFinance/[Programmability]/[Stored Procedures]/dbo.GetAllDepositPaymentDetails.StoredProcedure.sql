USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetAllDepositPaymentDetails]    Script Date: 06/07/2017 09:17:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetAllDepositPaymentDetails]
	@BankDepositId varchar(10),
             @PaymentId 	  varchar(10),
             @ChequeNumber varchar(50),
	@PaymentAmount  varchar(20),
	@OrderId 	    varchar(10),
	@CampaignId  varchar(10),
	@ChequeDateFrom varchar(10),
             @ChequeDateTo varchar(10)
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MS May 20, 2004
--   Get Bank Deposit Payment Details List For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Declare @SQL Varchar(2000)
--Declare @WhereClause Varchar1(1000)

Set @SQL = 'SELECT P.PAYMENT_ID as PAYMENT_ID,
      			 P.CHEQUE_NUMBER as  CHEQUE_NUMBER, 
	       		 CONVERT(varchar(10), p.CHEQUE_DATE, 101) as CHEQUE_DATE,
    		 	 SUBSTRING(p.CHEQUE_PAYER, 1, 40) as CHEQUE_PAYER, 
	     		 P.PAYMENT_AMOUNT as PAYMENT_AMOUNT ,
	     		 CONVERT(varchar(10),P.PAYMENT_EFFECTIVE_DATE, 101) as  PAYMENT_EFFECTIVE_DATE,
	      		 P.CAMPAIGN_ID as CAMPAIGN_ID,
	       		 P.ORDER_ID as ORDER_ID
		FROM   dbo.DEPOSIT_ITEM di INNER JOIN dbo.PAYMENT  p   ON di.PAYMENT_ID = p.PAYMENT_ID 
		WHERE    p.PAYMENT_METHOD_ID NOT IN (50003,50004)'
		

If @BankDepositId is not null 
Begin
Set @SQL = @SQL + ' AND Bank_Deposit_Id = ' +@BankDepositId
End

If @PaymentId is not null 
Begin
Set @SQL = @SQL + ' AND p.Payment_Id  Like ''' +@PaymentId+'%'''
End

If @ChequeNumber is not null 
Begin
Set @SQL = @SQL +' AND p.Cheque_Number  Like ''' +@ChequeNumber+'%'''
End

If @OrderId is not null 
Begin
Set @SQL = @SQL + ' AND p.Order_Id  Like ''' +@OrderId+'%'''
End
If @CampaignId is not null 
Begin
Set @SQL = @SQL + ' AND p.Campaign_Id  Like ''' +@CampaignId+'%'''
End


IF  @ChequeDateFrom is not null 
BEGIN
       Set @SQL = @SQL + ' AND  p.Cheque_Date >= '''+ @ChequeDateFrom+'''' 
END
ELSE 
   BEGIN
    Set @SQL = @SQL + ' AND  p.Cheque_Date  >=  '' 01/01/2001'''
    END    

IF   @ChequeDateTo  is not null  and @ChequeDateTo <> ''
 BEGIN
         Set @SQL = @SQL + ' AND  p.Cheque_Date <= '''+ @ChequeDateTo+ ''''
  END 
ELSE 
    BEGIN
      Set @SQL = @SQL + ' AND  p.Cheque_Date <=  ''12/31/2007'''
     END    

IF @PaymentAmount is not null 
 BEGIN
     IF  CHARINDEX('.' ,@PaymentAmount) = 0
       BEGIN
          Set @PaymentAmount =   cast(@PaymentAmount as numeric(10,2))  
       END

     Set  @PaymentAmount =  substring(@PaymentAmount,1,( CHARINDEX('.' ,@PaymentAmount))-1)
     Set @SQL = @SQL + ' AND   p.Payment_Amount >=  '+ @PaymentAmount  + ' AND   p.Payment_Amount <=  ' + cast((cast(@PaymentAmount as int) +1) as varchar)
   --    END
  --  ELSE
   --   BEGIN
     --   Set @SQL = @SQL + ' AND   p.Payment_Amount =  '+ @PaymentAmount    
      --END

  END
ELSE
BEGIN
        Set @SQL = @SQL + ' AND   p.Payment_Amount  >  0 '
END


Exec (@SQL)
GO
