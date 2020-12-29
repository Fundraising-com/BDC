USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_bank_account_by_id]    Script Date: 02/14/2014 13:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Bank_Account
CREATE PROCEDURE [dbo].[efrcrm_get_bank_account_by_id] @Bank_ID int AS
begin

select Bank_ID, Bank_Account_No, Currency_Code, GL_Account_No from Bank_Account where Bank_ID=@Bank_ID

end
GO
