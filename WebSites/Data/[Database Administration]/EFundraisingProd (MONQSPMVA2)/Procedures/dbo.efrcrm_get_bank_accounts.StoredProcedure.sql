USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_bank_accounts]    Script Date: 02/14/2014 13:03:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Bank_Account
CREATE PROCEDURE [dbo].[efrcrm_get_bank_accounts] AS
begin

select Bank_ID, Bank_Account_No, Currency_Code, GL_Account_No from Bank_Account

end
GO
