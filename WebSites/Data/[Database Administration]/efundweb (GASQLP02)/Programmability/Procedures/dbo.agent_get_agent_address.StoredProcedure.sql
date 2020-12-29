USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[agent_get_agent_address]    Script Date: 02/14/2014 13:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[agent_get_agent_address] (
	@agent_id as int	

)
as 

   select address_id, agent_id, address, city, state, country, zip
   from agent_address
   where agent_id = @agent_id
GO
