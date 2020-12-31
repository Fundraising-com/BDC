USE [QSPCanadaFinance]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBankDepositItem]    Script Date: 06/07/2017 09:17:33 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateBankDepositItem]
             @BankDepositItemID         int ,	 		
             @BankDepositID       int,
             @PaymentId    int
           
AS
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   Created By Muhammad Siddiqui 5/4/2004 
--   Update Bank Deposit Item Record For Canada Finance System
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Update Deposit_Item 
set 	Bank_Deposit_ID = @BankDepositID,
	Payment_Id =   @PaymentId     
Where  	Deposit_Item_ID =  @BankDepositItemID
GO
