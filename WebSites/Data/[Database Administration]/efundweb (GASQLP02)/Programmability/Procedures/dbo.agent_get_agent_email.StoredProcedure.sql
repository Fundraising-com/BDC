USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[agent_get_agent_email]    Script Date: 02/14/2014 13:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   procedure [dbo].[agent_get_agent_email] (
	@agent_id as int	

)
as 

   select email_id, agent_id, email_address
   from agent_email
   where agent_id = @agent_id
GO
