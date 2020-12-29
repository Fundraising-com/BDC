USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_create_invitation]    Script Date: 02/14/2014 13:05:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_create_invitation]
    @event_participation_id int
    ,@touch_info_id int
    ,@touch_id int OUTPUT
AS
BEGIN

    DECLARE @errorCode int

    if exists (select * 
			from event_participation ep
				inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
			where ep.event_participation_id = @event_participation_id
		      and unsubscribe = 1
    )
    return -3



    BEGIN TRANSACTION

    INSERT INTO touch (
        event_participation_id
        , member_hierarchy_id
        , touch_info_id
        , processed
        , create_date
    ) VALUES (
        @event_participation_id
        , NULL
        , @touch_info_id
        , 0
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
