USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_coveralbum_image]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: July 22, 2011
-- Description:	Gets the cover album
-- =============================================
CREATE PROCEDURE [dbo].[es_get_personalization_coveralbum_image]
	@personalization_id int = NULL,
	@event_id int = NULL --> Gets the sponsors cover image
AS
BEGIN
	SET NOCOUNT ON;

    SELECT pi.image_id
		  ,pi.personalization_id
		  ,pi.image_url
		  ,pi.high_image_url
		  ,pi.deleted
          ,pi.isCoverAlbum
          ,pi.create_date
	      ,pi.image_approval_status_id
    FROM  dbo.personalization_image pi with (nolock) INNER JOIN personalization p with (nolock)
      ON  pi.personalization_id = p.personalization_id INNER JOIN event_participation ep with (nolock)
      ON  p.event_participation_id = ep.event_participation_id
    WHERE (pi.personalization_id = @personalization_id OR @personalization_id IS NULL)
      AND (@event_id IS NULL OR (ep.event_id = @event_id AND ep.participation_channel_id = 3))
      AND deleted = 0 AND image_approval_status_id <> 4 AND isCoverAlbum = 1
END
GO
