USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_deposit]    Script Date: 02/14/2014 13:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Deposit
CREATE PROCEDURE [dbo].[efrcrm_insert_deposit] @Deposit_id int OUTPUT, @Payment_method_id tinyint, @Bank_id int, @Bank_account_no varchar(50), @Deposit_date datetime AS

declare @id int
exec @id = sp_NewID  'Deposit_ID', 'All'

begin

insert into Deposit(Deposit_id, Payment_method_id, Bank_id, Bank_account_no, Deposit_date) values(@id, @Payment_method_id, @Bank_id, @Bank_account_no, @Deposit_date)

end
GO
