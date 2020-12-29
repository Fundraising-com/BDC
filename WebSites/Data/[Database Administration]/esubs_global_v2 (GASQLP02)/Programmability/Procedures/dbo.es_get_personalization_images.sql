SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 14, 2014
-- Description:	Returns the personalization images for the event received
-- =============================================
CREATE PROCEDURE es_get_personalization_images
	@eventId int
AS
BEGIN	
	SET NOCOUNT ON;

    SELECT
		pii.*
	FROM
		event_participation ep (NOLOCK)
		JOIN personalization p (NOLOCK) ON ep.event_participation_id = p.event_participation_id
		JOIN personalization_image pii (NOLOCK)  ON p.personalization_id = pii.personalization_id
	WHERE 
		ep.event_id = @eventId
		AND pii.image_approval_status_id <> 4
		AND pii.deleted = 0
END
GO
