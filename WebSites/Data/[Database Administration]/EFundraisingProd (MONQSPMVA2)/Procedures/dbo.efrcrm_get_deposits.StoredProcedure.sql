USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_deposits]    Script Date: 02/14/2014 13:04:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Deposit
CREATE PROCEDURE [dbo].[efrcrm_get_deposits] AS
begin

select Deposit_id, Payment_method_id, Bank_id, Bank_account_no, Deposit_date from Deposit

end
GO
