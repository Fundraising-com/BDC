USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_bank_account]    Script Date: 02/14/2014 13:07:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Bank_Account
CREATE PROCEDURE [dbo].[efrcrm_update_bank_account] @Bank_ID int, @Bank_Account_No varchar(50), @Currency_Code varchar(10), @GL_Account_No varchar(10) AS
begin

update Bank_Account set Bank_Account_No=@Bank_Account_No, Currency_Code=@Currency_Code, GL_Account_No=@GL_Account_No where Bank_ID=@Bank_ID

end
GO
