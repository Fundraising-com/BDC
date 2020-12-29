USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[agent_get_agent_reach_numbers]    Script Date: 02/14/2014 13:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[agent_get_agent_reach_numbers] (
	@agent_id as int	

)
as 

   select agent_id, reach_type_id, reach_number
   from agent_reach_number
   where agent_id = @agent_id
GO
