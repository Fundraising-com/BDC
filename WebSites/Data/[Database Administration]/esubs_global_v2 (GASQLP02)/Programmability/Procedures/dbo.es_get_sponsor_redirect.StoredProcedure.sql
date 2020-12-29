USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_sponsor_redirect]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	retourne un group id pour savoir quel event loader
	mod fblais 2005-11-29
*/
CREATE procedure [dbo].[es_get_sponsor_redirect]
	@redirect varchar(255)
as
SELECT top 1
	   [personalization].[personalization_id]
      ,[personalization].[event_participation_id]
      ,[personalization].[header_title1]
      ,[personalization].[header_title2]
      ,[personalization].[body]
      ,[personalization].[fundraising_goal]
      ,[personalization].[site_bgcolor]
      ,[personalization].[header_bgcolor]
      ,[personalization].[header_color]
      ,[personalization].[group_url]
      ,[personalization].[image_url]
      ,[personalization].[create_date]
      ,[personalization].[image_motivator]
      ,[personalization].[redirect]
      ,[personalization].[displayGroupMessage]
      ,[personalization].[remind_later]
  FROM [personalization] inner join [event_participation]
		on [personalization].event_participation_id = [event_participation].event_participation_id inner join [event] on
		[event].event_id = [event_participation].event_id
where
	   [event_participation].participation_channel_id = 3 and [event].active = 1 and [personalization].redirect=@redirect
order by [event_participation].create_date desc
GO
