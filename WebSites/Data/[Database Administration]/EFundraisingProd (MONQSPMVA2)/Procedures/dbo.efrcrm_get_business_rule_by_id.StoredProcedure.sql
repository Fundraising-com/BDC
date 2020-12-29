USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_business_rule_by_id]    Script Date: 02/14/2014 13:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Business_Rule
CREATE PROCEDURE [dbo].[efrcrm_get_business_rule_by_id] @Business_Rule_ID int AS
begin

select Business_Rule_ID, Partner_ID, Rule_Description, Module_Name, Form_Name, Access_Sub_Name from Business_Rule where Business_Rule_ID=@Business_Rule_ID

end
GO
