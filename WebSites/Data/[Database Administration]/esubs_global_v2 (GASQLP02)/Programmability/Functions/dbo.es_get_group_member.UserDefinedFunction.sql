USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[es_get_group_member]    Script Date: 02/14/2014 13:08:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[es_get_group_member] (@group_id int)
RETURNS @retGroupMember TABLE (member_hierarchy_id int, member_id int)
AS 
BEGIN
        DECLARE @current int
        DECLARE @nLevel int
        DECLARE @member_stack TABLE (member_hierarchy_id int, nLevel int)
        DECLARE @member_h TABLE (member_hierarchy_id int)
        
        SELECT @nLevel = 1
        
        INSERT INTO @member_stack
        SELECT member_hierarchy_id, @nLevel
        FROM member_hierarchy
        WHERE member_hierarchy_id IN (
        	SELECT sponsor_id
        	FROM [group]
            WHERE group_id = @group_id
        )
        
        -- Get the member hierarchy under each sponsor
        WHILE (@nLevel > 0)
        BEGIN
        	IF EXISTS(SELECT member_hierarchy_id FROM @member_stack WHERE nLevel = @nLevel)
        	BEGIN
        		-- pop a group out of stack
        		SELECT TOP 1 @current = member_hierarchy_id FROM @member_stack WHERE nLevel = @nLevel
        		DELETE FROM @member_stack WHERE member_hierarchy_id = @current AND nLevel = @nLevel
        				
        		-- Insert into the hierarchy table
        		INSERT INTO @member_h (member_hierarchy_id)
        		SELECT member_hierarchy_id
        		FROM member_hierarchy
        		WHERE member_hierarchy_id = @current
        
        		-- insert all kids
        		INSERT INTO @member_stack (member_hierarchy_id, nLevel)
        		SELECT member_hierarchy_id, @nLevel + 1
        		FROM member_hierarchy
        		WHERE parent_member_hierarchy_id = @current
        		
        		IF @@ROWCOUNT > 0
        			SELECT @nLevel = @nLevel + 1
        	END
        	ELSE
        	BEGIN
        		-- Go back to previous level
        		SELECT @nLevel= @nLevel -1
        	END
        END

    INSERT @retGroupMember
    SELECT mh.member_hierarchy_id
    	, mh.member_id
    FROM @member_h as m_h
    	inner join member_hierarchy as mh on mh.member_hierarchy_id = m_h.member_hierarchy_id
    	inner join member as m on m.member_id = mh.member_id

    RETURN
END
GO
