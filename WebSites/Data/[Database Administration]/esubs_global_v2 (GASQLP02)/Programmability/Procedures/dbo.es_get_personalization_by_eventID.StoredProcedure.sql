USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_by_eventID]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_get_personalization_by_eventID]
	@eventID INT
as
SELECT [personalization].[personalization_id]
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
  FROM [personalization] inner  join [event_participation] 
	ON [personalization].event_participation_id = [event_participation].event_participation_id
where [event_participation].event_id =@eventID
GO
