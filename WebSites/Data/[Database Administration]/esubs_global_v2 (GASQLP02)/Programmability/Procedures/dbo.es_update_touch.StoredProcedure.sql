USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_touch]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[es_update_touch]
	@event_participation_id int = NULL,
	@member_hierarchy_id int = NULL,
	@touch_info_id int = NULL,
	@processed tinyint  = NULL,
	@touch_id int = NULL
AS
BEGIN
	DECLARE @errorCode int    
	
	BEGIN TRANSACTION

	UPDATE [touch]
	SET event_participation_id = @event_participation_id
		, member_hierarchy_id = @member_hierarchy_id
		, touch_info_id = @touch_info_id
		, processed = @processed
	WHERE touch_id = @touch_id

	SET @errorCode = @@error

	IF (@errorCode <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END

    COMMIT TRANSACTION
    RETURN 0 

END
GO
