USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_by_redirect]    Script Date: 04/29/2014 13:39:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Jiro Hidaka (April 2014)
Get all instances of personalization redirects that starts with the specified parameter
EXEC [dbo].[es_get_personalization_redirect_startswith] 'braves'
*/
CREATE procedure [dbo].[es_get_personalization_redirect_startswith]
	@redirect varchar(255)
as
SELECT [personalization_id]
      ,p.[event_participation_id]
      ,[header_title1]
      ,[header_title2]
      ,[body]
      ,[fundraising_goal]
      ,[site_bgcolor]
      ,[header_bgcolor]
      ,[header_color]
      ,[group_url]
      ,[image_url]
      ,p.[create_date]
      ,[image_motivator]
      ,p.[redirect]
      ,[displayGroupMessage]
      ,[remind_later]
  FROM [esubs_global_v2].[dbo].[personalization] p (NOLOCK)
  JOIN [esubs_global_v2].[dbo].[event_participation] ep (NOLOCK) ON p.event_participation_id=ep.event_participation_id
  JOIN [esubs_global_v2].[dbo].[event] e (NOLOCK) ON ep.event_id=e.event_id
where e.active=1 and rtrim(p.redirect) like @redirect+'%' and ep.participation_channel_id=3
GO

grant exec on [dbo].[es_get_personalization_redirect_startswith] to db_stored_proc_exec
GO

