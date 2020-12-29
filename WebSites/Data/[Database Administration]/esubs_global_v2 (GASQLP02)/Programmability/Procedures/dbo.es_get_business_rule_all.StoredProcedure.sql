USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_business_rule_all]    Script Date: 02/14/2014 13:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_business_rule_all] AS
begin

SELECT     business_rule_id, email_template_id, stored_procedure_call, create_date, active, email_priority, member_type_id, priority_level, 
                      business_rule_name
FROM         dbo.business_rule
end
GO
