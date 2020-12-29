USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_dm_personalization_image]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: July 26, 2010
-- Description:	Update personalization image
-- =============================================
CREATE PROCEDURE [dbo].[es_update_dm_personalization_image]
	@image_id int
	,@direct_mail_info_id int
	,@image_url varchar(255)
	,@deleted bit
    ,@isCoverAlbum bit
AS
BEGIN
	UPDATE dbo.dm_personalization_image
	SET direct_mail_info_id = @direct_mail_info_id
      ,image_url = @image_url
      ,deleted = @deleted
      ,isCoverAlbum=@isCoverAlbum
	WHERE image_id = @image_id
END
GO
