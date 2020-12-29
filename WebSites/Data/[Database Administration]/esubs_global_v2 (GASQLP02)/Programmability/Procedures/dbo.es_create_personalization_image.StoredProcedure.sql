USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_personalization_image]    Script Date: 02/14/2014 13:05:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        Jiro Hidaka
-- Create date: July 26, 2010
-- Description:    Insert personalization image
-- =============================================
CREATE PROCEDURE [dbo].[es_create_personalization_image]
     @personalization_id int
    ,@image_url varchar(255)
    ,@deleted bit
    ,@image_id int OUTPUT
    ,@isCoverAlbum bit
    ,@image_approval_status_id int= 1
	,@high_image_url varchar(255) = null

WITH RECOMPILE
AS


BEGIN
    DECLARE @errorCode int
    
    BEGIN TRANSACTION
    
    INSERT INTO personalization_image
    (
         personalization_id
        ,image_url
        ,deleted
        ,isCoverAlbum
        ,image_approval_status_id
		,high_image_url
        

    ) VALUES (
          @personalization_id
        , @image_url
        , @deleted
        , @isCoverAlbum
        , @image_approval_status_id
		, @high_image_url
    )
    
    SET @errorCode = @@error
    
    IF (@errorCode <> 0)
    BEGIN
          ROLLBACK TRAN
        RETURN -1
    END

     SELECT @image_id = SCOPE_IDENTITY()
    
    SET @errorCode = @@error

    IF (@errorCode <> 0)
    BEGIN
          ROLLBACK TRAN
        RETURN -2
    END
    
    COMMIT TRANSACTION
    RETURN 0
END
GO
