USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_business_rule]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Business_Rule
CREATE PROCEDURE [dbo].[efrcrm_insert_business_rule] @Business_Rule_ID int OUTPUT, @Partner_ID int, @Rule_Description varchar(100), @Module_Name varchar(25), @Form_Name varchar(50), @Access_Sub_Name varchar(50) AS
begin

insert into Business_Rule(Partner_ID, Rule_Description, Module_Name, Form_Name, Access_Sub_Name) values(@Partner_ID, @Rule_Description, @Module_Name, @Form_Name, @Access_Sub_Name)

select @Business_Rule_ID = SCOPE_IDENTITY()

end
GO
