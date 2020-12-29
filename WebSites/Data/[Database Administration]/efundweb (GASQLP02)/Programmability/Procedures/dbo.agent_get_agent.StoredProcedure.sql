USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[agent_get_agent]    Script Date: 02/14/2014 13:04:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[agent_get_agent] (
	@agent_id as int	

)
as 

   select agent_id, url, company, agent_name, logo, add_timestamp, add_by_user
   from agent
   where agent_id = @agent_id
GO
