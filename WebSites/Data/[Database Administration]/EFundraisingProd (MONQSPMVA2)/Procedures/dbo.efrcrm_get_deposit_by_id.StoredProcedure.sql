USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_deposit_by_id]    Script Date: 02/14/2014 13:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Deposit
CREATE PROCEDURE [dbo].[efrcrm_get_deposit_by_id] @Deposit_id int AS
begin

select Deposit_id, Payment_method_id, Bank_id, Bank_account_no, Deposit_date from Deposit where Deposit_id=@Deposit_id

end
GO
