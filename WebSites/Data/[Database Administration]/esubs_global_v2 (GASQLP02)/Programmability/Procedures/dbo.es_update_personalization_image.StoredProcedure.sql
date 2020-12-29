USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_personalization_image]    Script Date: 02/14/2014 13:07:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: July 26, 2010
-- Description:	Update personalization image
-- =============================================
CREATE PROCEDURE [dbo].[es_update_personalization_image]
	@image_id int
	,@personalization_id int
	,@image_url varchar(255)
	,@deleted bit
    ,@isCoverAlbum bit
    ,@high_image_url varchar(255) = null
AS
BEGIN
	UPDATE dbo.personalization_image
	SET personalization_id = @personalization_id
      ,image_url = @image_url
      ,deleted = @deleted
      ,isCoverAlbum=@isCoverAlbum
      ,high_image_url=@high_image_url
	WHERE image_id = @image_id
END
GO
