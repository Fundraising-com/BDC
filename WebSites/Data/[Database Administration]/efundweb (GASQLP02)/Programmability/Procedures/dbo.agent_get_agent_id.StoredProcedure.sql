USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[agent_get_agent_id]    Script Date: 02/14/2014 13:04:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--JF LAvigne
--Permet d'aller chercher l'agent selon le nom de domaine

CREATE procedure [dbo].[agent_get_agent_id] (
	@agent_domain as varchar(50)

)
as 

   select agent_id, domain_id, domain_name
   from agent_domain
   where domain_name = @agent_domain
GO
