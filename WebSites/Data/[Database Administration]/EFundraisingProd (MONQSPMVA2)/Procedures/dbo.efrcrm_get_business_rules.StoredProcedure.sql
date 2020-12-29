USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_business_rules]    Script Date: 02/14/2014 13:03:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Business_Rule
CREATE PROCEDURE [dbo].[efrcrm_get_business_rules] AS
begin

select Business_Rule_ID, Partner_ID, Rule_Description, Module_Name, Form_Name, Access_Sub_Name from Business_Rule

end
GO
