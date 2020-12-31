USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[AddBankDepositItem]    Script Date: 06/07/2017 09:16:59 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[AddBankDepositItem]
	 		
             @BankDepositID  int,
             @PaymentId    int,
            @DepositItemId  int  output
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   Created By Muhammad Siddiqui 5/4/2004 
--   Insert a new Bank Deposit Item Record For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON

INSERT QSPCanadaFinance..Deposit_Item (
	
	Bank_Deposit_ID,       
             Payment_Id     
)
VALUES(
	  @BankDepositID,
               @PaymentId )

select @DepositItemId = @@Identity
SET NOCOUNT OFF
GO
