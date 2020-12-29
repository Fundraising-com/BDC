USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization]    Script Date: 02/14/2014 13:06:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_personalization]
	@event_participation_id int
AS
BEGIN
SELECT 
	personalization_id
    , event_participation_id
	, header_title1
	, header_title2
	, body
	, fundraising_goal
	, site_bgcolor
	, header_bgcolor
	, header_color
	, group_url
	, image_url
    , image_motivator
    , redirect
	, displayGroupMessage
    , remind_later
FROM personalization
WHERE event_participation_id = @event_participation_id
END
GO
