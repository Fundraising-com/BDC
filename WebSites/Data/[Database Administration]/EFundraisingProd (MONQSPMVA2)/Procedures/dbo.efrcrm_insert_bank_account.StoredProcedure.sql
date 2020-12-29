USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_bank_account]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Bank_Account
CREATE PROCEDURE [dbo].[efrcrm_insert_bank_account] @Bank_ID int OUTPUT, @Bank_Account_No varchar(50), @Currency_Code varchar(10), @GL_Account_No varchar(10) AS
begin

insert into Bank_Account(Bank_Account_No, Currency_Code, GL_Account_No) values(@Bank_Account_No, @Currency_Code, @GL_Account_No)

select @Bank_ID = SCOPE_IDENTITY()

end
GO
