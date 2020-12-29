USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_dm_personalization_image]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_dm_personalization_image]
	@direct_mail_info_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT image_id
		,direct_mail_info_id
		,image_url
		,deleted
        ,isCoverAlbum
        ,create_date
	    ,image_approval_status_id
  FROM dbo.dm_personalization_image
  WHERE direct_mail_info_id = @direct_mail_info_id and deleted = 0
  AND image_approval_status_id <> 4
END
GO
