USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[sc_get_didyouknow_by_group_type_id]    Script Date: 02/14/2014 13:06:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sc_get_didyouknow_by_group_type_id]
    @group_type_id int = 0
    ,@culture_code nvarchar(5) = 'en-US'
AS
BEGIN
    DECLARE @count_story int
    DECLARE @story_type_id int
    DECLARE @selected int
    
    -- didyouknow story
    SET @story_type_id = 2

    IF @group_type_id <> 0
    BEGIN
        -- return the selected row number
        SELECT TOP 1 story_text
        FROM story
        WHERE story_type_id = @story_type_id
          AND group_type_id = @group_type_id
        ORDER BY NEWID() -- will only work on win2000

    END
    ELSE
    BEGIN
        -- return the selected row number
        SELECT TOP 1 story_text
        FROM story
        WHERE story_type_id = @story_type_id
        ORDER BY NEWID() -- will only work on win2000        

    END
END
GO
