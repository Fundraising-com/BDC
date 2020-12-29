USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_terminate_event_by_event_id]    Script Date: 02/14/2014 13:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[es_terminate_event_by_event_id]
    @event_id int    
AS
BEGIN
    DECLARE @errorCode int

    BEGIN TRAN

    UPDATE event
    SET end_date = GETDATE()
         , active = 0
    WHERE event_id  = @event_id

    SET @errorCode = @@error

    IF @errorCode <> 0
    BEGIN
        ROLLBACK TRAN
        RETURN -1
    END

    COMMIT TRAN
    RETURN 0
END
GO
