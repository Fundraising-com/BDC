USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddBankDeposit]    Script Date: 06/07/2017 09:16:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddBankDeposit]
	 		
	@DepositDate	         Varchar(10),
	@ItemCount	        Varchar(5),
             @DepositAmount       Varchar(20),
             @DepositStatusID      Varchar(10),
             @DepositAccountId    Varchar(50),
            @BankDepositID  Varchar(10) output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   Created By Muhammad Siddiqui 5/4/2004 
--   Insert a new Bank Deposit Record For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

INSERT QSPCanadaFinance..Bank_Deposit (
	
	Deposit_Date,		
	Item_Count,	     
             Deposit_Amount,        
             Bank_Deposit_Status_ID,       
             Bank_Account_Id     
)
VALUES(
	GETDATE(),  
	@ItemCount,
	@DepositAmount,
	@DepositStatusID, 
	@DepositAccountId)

select @BankDepositID = @@Identity
SET NOCOUNT OFF
GO
