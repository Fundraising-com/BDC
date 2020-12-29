USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_personalization_image]    Script Date: 02/14/2014 13:06:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_personalization_image]
	@personalization_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT image_id
		,personalization_id
		,image_url
		,high_image_url
		,deleted
        ,isCoverAlbum
        ,create_date
	    ,image_approval_status_id
  FROM dbo.personalization_image
  WHERE personalization_id = @personalization_id and deleted = 0
  AND image_approval_status_id <> 4
END
GO
