USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_touch_get_business_rules]    Script Date: 02/14/2014 13:07:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_touch_get_business_rules]
as

select 
      business_rule_id
      ,email_template_id
      ,stored_procedure_call
      ,email_priority as priority_level
from
      business_rule
where 
      active =1
order by NEWID()
GO
