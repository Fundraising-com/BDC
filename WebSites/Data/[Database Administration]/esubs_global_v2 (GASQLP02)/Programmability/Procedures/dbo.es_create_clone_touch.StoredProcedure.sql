USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_clone_touch]    Script Date: 02/14/2014 13:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ======================================================
-- Author:		JF Buist
-- Create date: April 4, 2011
-- Description:	When a resend email action is performed
-- 		by the user, the touch are duplicated.
-- 
-- ======================================================
CREATE PROCEDURE [dbo].[es_create_clone_touch]
	@member_hierarchy_id int,
	@event_participation_id int,
	@touch_info_id int,
	@processed tinyint,
    @touch_id int OUTPUT
AS
BEGIN
    DECLARE @errorCode int    

	BEGIN TRANSACTION

    INSERT INTO touch (
        event_participation_id
        , member_hierarchy_id
        , touch_info_id
        , processed
        , create_date
    ) VALUES (
          @event_participation_id
        , @member_hierarchy_id
        , @touch_info_id
        , @processed
        , GETDATE()
    )

    SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END

	SELECT @touch_id = SCOPE_IDENTITY()

	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -2
	END

    COMMIT TRANSACTION
    RETURN 0    
END
GO
