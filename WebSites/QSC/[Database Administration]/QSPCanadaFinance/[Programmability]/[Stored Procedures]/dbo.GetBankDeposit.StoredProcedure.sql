USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[GetBankDeposit]    Script Date: 06/07/2017 09:17:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[GetBankDeposit] 
	@BankDepositId varchar(10),
             @DepositStatusId varchar(10),
             @DepositDateFrom varchar(20),
             @DepositDateTo varchar(20),
             @DepositAmount   varchar(20),
             @DepositAccountNo Varchar(50),
             @ItemDeposited varchar(5)
          
as

DECLARE @WhereClause varchar(200)
DECLARE @SQL Varchar(2000)
--DECLARE @startDate datetime
--DECLARE @endDate datetime

-- Set parameter values
--EXEC qspCanadaCommon.dbo.GetCurrentFiscalStartAndEnd @startDate  , @endDate 
IF @DepositStatusId  is null
   BEGIN
      SET  @WhereClause = '  WHERE  bd.BANK_DEPOSIT_STATUS_ID > 0 ' 
  END
ELSE
   BEGIN
      SET @WhereClause = '  WHERE  bd.BANK_DEPOSIT_STATUS_ID = ' + @DepositStatusId  
   END


Set @SQL = ' SELECT     bd.BANK_DEPOSIT_ID AS BANK_DEPOSIT_ID, 
                     		 bd.BANK_DEPOSIT_STATUS_ID AS BANK_DEPOSIT_STATUS_ID, 
	                           bd.BANK_ACCOUNT_ID AS BANK_ACCOUNT_ID, 
                                        bd.ITEM_COUNT AS ITEM_COUNT, 
	                           bd.DEPOSIT_AMOUNT AS DEPOSIT_AMOUNT, 
                                        CONVERT(varchar(10), bd.DEPOSIT_DATE, 101) AS DEPOSIT_DATE, 
                                        ba.BANK_ACCOUNT_NUMBER AS BANK_ACCOUNT_NUMBER, 
                          	             ba.BANK_ACCOUNT_NAME AS  BANK_ACCOUNT_NAME  
          		 FROM  dbo.BANK_ACCOUNT ba     INNER JOIN dbo.BANK_DEPOSIT bd ON 
		              ba.BANK_ACCOUNT_ID = bd.BANK_ACCOUNT_ID' 
                          
SET @SQL = @SQL+ @WhereClause

IF @BankDepositId  is not null 
 BEGIN
        Set @SQL = @SQL + ' AND  bd.BANK_DEPOSIT_ID LIKE '''+ @BankDepositId  +'%'''
  END
else
-- If no deposit id is passed it will be zero  (ALL records)
--IF @BankDepositId  = 0 
 BEGIN
        Set @SQL = @SQL + ' AND  bd.BANK_DEPOSIT_ID > 0 '
  END



IF  @DepositDateFrom is not null 
BEGIN
        Set @SQL = @SQL + ' AND  bd.DEPOSIT_DATE >= '''+ @DepositDateFrom+'''' 
END
ELSE 
 
    BEGIN
     Set @SQL = @SQL + ' AND  bd.DEPOSIT_DATE >=  ''01/01/2001'''
   END    

IF   @DepositDateTo  is not null  and @DepositDateTo <> ''
 BEGIN
         Set @SQL = @SQL + ' AND  bd.DEPOSIT_DATE <= '''+ @DepositDateTo+ ''''
  END 
ELSE 

    BEGIN
      Set @SQL = @SQL + ' AND  bd.DEPOSIT_DATE <=  ''12/31/2007'''
     END    




IF @DepositAmount is not null --and cast(@DepositAmount as numeric(10,2))  > 0
 BEGIN
     IF  CHARINDEX('.' ,@DepositAmount) = 0
       BEGIN
          Set @DepositAmount =   cast(@DepositAmount as numeric(10,2))  
       END

     Set  @DepositAmount =  substring(@DepositAmount,1,( CHARINDEX('.' ,@DepositAmount))-1)
     Set @SQL = @SQL + ' AND   bd.DEPOSIT_AMOUNT>=  '+ @DepositAmount  + ' AND   bd.DEPOSIT_AMOUNT<=  ' + cast((cast(@DepositAmount as int) +1) as varchar)
   --    END
  --  ELSE
   --   BEGIN
     --   Set @SQL = @SQL + ' AND   bd.DEPOSIT_AMOUNT =  '+ @DepositAmount    
      --END

  END
ELSE
BEGIN
        Set @SQL = @SQL + ' AND   bd.DEPOSIT_AMOUNT >  0 '
END


IF  @DepositAccountNo  is not null 
  BEGIN
         Set @SQL = @SQL + ' AND  ba.BANK_ACCOUNT_NUMBER LIKE '''+ @DepositAccountNo +'%'''
  END


IF @ItemDeposited  is not null
 BEGIN

      Set @SQL = @SQL + ' AND   bd.ITEM_COUNT = ' + @ItemDeposited 
 END
ELSE
 BEGIN
      Set @SQL = @SQL + ' AND   bd.ITEM_COUNT  >= 0  '
 END

exec (@SQL)
GO
