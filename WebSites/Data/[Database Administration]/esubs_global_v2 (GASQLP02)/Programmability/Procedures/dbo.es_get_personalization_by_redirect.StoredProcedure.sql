USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_by_redirect]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_get_personalization_by_redirect]
	@redirect varchar(255)
as
SELECT [personalization_id]
      ,[event_participation_id]
      ,[header_title1]
      ,[header_title2]
      ,[body]
      ,[fundraising_goal]
      ,[site_bgcolor]
      ,[header_bgcolor]
      ,[header_color]
      ,[group_url]
      ,[image_url]
      ,[create_date]
      ,[image_motivator]
      ,[redirect]
      ,[displayGroupMessage]
      ,[remind_later]
  FROM [esubs_global_v2].[dbo].[personalization]
where redirect=@redirect
GO
