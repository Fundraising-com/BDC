USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[agent_get_agent_from_domain]    Script Date: 02/14/2014 13:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[agent_get_agent_from_domain] (
	@domain_name as varchar(50)

)
as 

   select agent_id, url, company, agent_name, logo, add_timestamp, add_by_user
   from agent
   where url = @domain_name
GO
