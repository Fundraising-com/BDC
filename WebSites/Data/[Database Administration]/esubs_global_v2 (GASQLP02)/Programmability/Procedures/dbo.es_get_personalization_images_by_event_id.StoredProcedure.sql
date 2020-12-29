USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_images_by_event_id]    Script Date: 10/31/2014 14:04:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Javier Arellano
-- Create date: October 14, 2014
-- Description:	Finds the images uploaded for an event
-- UPDATE OCT 31, 2014
--    by Jiro Hidaka: 
--         Found an error in CASE statement and
--         join to personalization_image table
-- =============================================
ALTER PROCEDURE [dbo].[es_get_personalization_images_by_event_id]
	@eventId int
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT
		(CASE WHEN cc.member_type_id = 1 THEN 0 ELSE ep.event_participation_id  END) AS event_participation_id
		,u.first_name
		,u.last_name
		,ep.event_id
		,(CASE WHEN IMG.event_participation_id IS NOT NULL 
		       THEN IMG.image_url
		       WHEN cc.creation_channel_id = 1 
		       THEN '/Content/images/personalization/groupphotoplaceholder.gif' 
		       ELSE '/Content/images/personalization/participant_default.gif' 
		  END) AS image_url
		,ISNULL(IMG.isCoverAlbum, 0) AS isCoverAlbum
	FROM
	   (SELECT
			EP.member_hierarchy_id,
			EP.event_participation_id,
			EP.event_id
		FROM
			[event_participation] EP (NOLOCK)
		WHERE
		ep.event_id = @eventId) EP
		JOIN [member_hierarchy] MH (NOLOCK) ON EP.member_hierarchy_id = MH.member_hierarchy_id
		JOIN [creation_channel] CC (NOLOCK) ON MH.creation_channel_id = CC.creation_channel_id
		JOIN [member] M (NOLOCK) ON MH.member_id = M.member_id
		JOIN [users] U (NOLOCK) ON M.user_id = U.user_id
		LEFT JOIN 
		(SELECT
			P.event_participation_id,
			PII.image_url,
			PII.isCoverAlbum
		 FROM [personalization] P (NOLOCK)
		 JOIN [personalization_image] PII (NOLOCK) ON P.personalization_id = PII.personalization_id
		 WHERE PII.image_url IS NOT NULL AND PII.image_approval_status_id <> 4 AND PII.deleted <> 1
		) IMG ON EP.event_participation_id = IMG.event_participation_id
END
