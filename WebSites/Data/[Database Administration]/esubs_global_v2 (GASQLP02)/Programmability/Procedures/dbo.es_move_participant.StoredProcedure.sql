USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_move_participant]    Script Date: 02/14/2014 13:06:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Stored procedure
create PROCEDURE [dbo].[es_move_participant]
    @member_hierarchy_id int
    , @group_id_from int
    , @group_id_to int
AS
BEGIN
    DECLARE @sponsor_id_from int
    DECLARE @sponsor_id_to int

    SELECT @sponsor_id_from = sponsor_id
    FROM [group]
    WHERE group_id = @group_id_from

    IF @@error <> 0
    BEGIN
        RETURN -1
    END

    SELECT @sponsor_id_to = sponsor_id
    FROM [group]
    WHERE group_id = @group_id_to

    IF @@error <> 0
    BEGIN
        RETURN -2
    END

    IF @sponsor_id_from IS NOT NULL AND @sponsor_id_to IS NOT NULL
    BEGIN
        BEGIN TRAN

        UPDATE member_hierarchy
        SET parent_member_hierarchy_id = @sponsor_id_to
        WHERE parent_member_hierarchy_id = @sponsor_id_from
          AND member_hierarchy_id = @member_hierarchy_id

        IF @@error <> 0
        BEGIN
            ROLLBACK TRAN
            RETURN -3
        END

        COMMIT TRAN
    END
    ELSE
    BEGIN
        RETURN -4
    END

    RETURN 0
END
GO
