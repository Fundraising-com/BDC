USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_business_rule]    Script Date: 02/14/2014 13:07:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Business_Rule
CREATE PROCEDURE [dbo].[efrcrm_update_business_rule] @Business_Rule_ID int, @Partner_ID int, @Rule_Description varchar(100), @Module_Name varchar(25), @Form_Name varchar(50), @Access_Sub_Name varchar(50) AS
begin

update Business_Rule set Partner_ID=@Partner_ID, Rule_Description=@Rule_Description, Module_Name=@Module_Name, Form_Name=@Form_Name, Access_Sub_Name=@Access_Sub_Name where Business_Rule_ID=@Business_Rule_ID

end
GO
