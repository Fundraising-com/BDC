USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_terminate_group_by_group_id]    Script Date: 02/14/2014 13:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
    Created by: Philippe Girard
    Created on: 24 August 2005

    Description: Used by partner web services
    Close all the group's event.    
*/
CREATE PROCEDURE [dbo].[es_terminate_group_by_group_id]
    @partner_id int -- not necessary, but we want to make sure
    ,@group_id int
AS
BEGIN
    DECLARE @errorCode int
    
    BEGIN TRAN

    
    UPDATE event
        SET end_date = GETDATE()
            , active = 0
    WHERE event_id in (
        SELECT event_id
        FROM event_group as eg
            INNER JOIN [group] as g
                ON g.group_id = eg.group_id
        WHERE g.group_id = @group_id
          AND g.partner_id = @partner_id
    )
    
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
