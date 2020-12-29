USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_deposit]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Deposit
CREATE PROCEDURE [dbo].[efrcrm_update_deposit] @Deposit_id int, @Payment_method_id tinyint, @Bank_id int, @Bank_account_no varchar(50), @Deposit_date datetime AS
begin

update Deposit set Payment_method_id=@Payment_method_id, Bank_id=@Bank_id, Bank_account_no=@Bank_account_no, Deposit_date=@Deposit_date where Deposit_id=@Deposit_id

end
GO
