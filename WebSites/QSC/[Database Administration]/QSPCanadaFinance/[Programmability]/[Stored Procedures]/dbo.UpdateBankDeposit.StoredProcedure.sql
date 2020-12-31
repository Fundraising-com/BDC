USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBankDeposit]    Script Date: 06/07/2017 09:17:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateBankDeposit]
             @BankDepositID         int ,	 		
             @DepositDate	         datetime,
	@ItemCount	          int,
             @DepositAmount         numeric(10,2),
             @DepositStatusID       int,
             @DepositAccountId    int
           
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   Created By Muhammad Siddiqui 5/4/2004 
--   Update Bank Deposit Record For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Update Bank_Deposit 
set 	Bank_Deposit_ID = @BankDepositID,
	Deposit_Date       =  GetDate(),
	Item_Count          = @ItemCount,	     
             Deposit_Amount       =  @DepositAmount ,        
             Bank_Deposit_Status_ID =  @DepositStatusID  ,       
             Bank_Account_Id =   @DepositAccountId     
Where  	Bank_Deposit_ID =  @BankDepositID
GO
