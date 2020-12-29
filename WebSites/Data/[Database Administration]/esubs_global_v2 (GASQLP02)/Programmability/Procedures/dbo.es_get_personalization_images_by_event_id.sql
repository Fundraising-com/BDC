
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 14, 2014
-- Description:	Finds the images uploaded for an event
-- =============================================
ALTER PROCEDURE es_get_personalization_images_by_event_id
	@eventId int
AS
BEGIN
	SET NOCOUNT ON;
SELECT
	(CASE WHEN cc.member_type_id = 1 THEN 0 ELSE ep.event_participation_id  END) AS event_participation_id
	,u.first_name
	,u.last_name
	,ep.event_id
	,(CASE WHEN pii.image_url IS NOT NULL AND pii.image_approval_status_id <> 4 AND pii.deleted <> 1 THEN pii.image_url WHEN cc.creation_channel_id = 1 THEN '/Content/images/personalization/groupphotoplaceholder.gif' ELSE '/Content/images/personalization/participant_default.gif' END) as image_url
	,ISNULL(pii.isCoverAlbum, 0) as isCoverAlbum
FROM
	[users] u (NOLOCK)
	JOIN [member] m (NOLOCK) ON u.user_id = m.user_id
	JOIN [member_hierarchy] mh (NOLOCK) ON m.member_id = mh.member_id
	JOIN [creation_channel] cc (NOLOCK) ON mh.creation_channel_id = cc.creation_channel_id
	JOIN [event_participation] ep (NOLOCK) ON mh.member_hierarchy_id = ep.member_hierarchy_id
	LEFT JOIN [personalization] p (NOLOCK) ON ep.event_participation_id = p.event_participation_id
	LEFT JOIN [personalization_image] pii (NOLOCK) ON p.personalization_id = pii.personalization_id
	WHERE
	ep.event_id = @eventId
END
GO
