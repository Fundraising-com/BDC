USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_group]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by: Philippe 
	Date: 8 August 2005
	
	Description: get the members of a group (including sub_groups)
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
	
*/

CREATE  PROC [dbo].[es_get_member_group]
	@group_id as int
AS
BEGIN
DECLARE @nLevel int, @current int

-- Get all the groups
DECLARE @group_stack TABLE (group_id int,nLevel int)
DECLARE @group_hierarchy TABLE (group_id int,sponsor_id int)

SET @nLevel = 1

-- Insert parent group in stack
INSERT INTO @group_stack
SELECT @group_id, @nLevel

WHILE (@nLevel > 0)
BEGIN
	IF EXISTS (SELECT group_id FROM @group_stack WHERE nLevel =@nLevel) 
	BEGIN
		-- pop a group out of stack
		SELECT TOP 1 @current = group_id FROM @group_stack WHERE nLevel = @nLevel
		DELETE FROM @group_stack WHERE group_id = @current AND nLevel = @nLevel
		
		
		-- Insert into the hierarchy table
		INSERT INTO @group_hierarchy (group_id, sponsor_id)
		SELECT group_id, sponsor_id
		FROM [group]
		WHERE group_id = @current

		-- insert all kids
		INSERT INTO @group_stack (group_id, nLevel)
		SELECT group_id, @nLevel + 1
		FROM [group]
		WHERE parent_group_id = @current

		IF @@ROWCOUNT > 0
			SELECT @nLevel = @nLevel + 1
	END	
	ELSE
	BEGIN
		-- Go back to previous level
		SELECT @nLevel= @nLevel -1
	END
END

-- get the sponsor of each group
DECLARE @member_stack TABLE (member_hierarchy_id int, nLevel int)
DECLARE @member_h TABLE (member_hierarchy_id int)

SELECT @nLevel = 1

INSERT INTO @member_stack
SELECT member_hierarchy_id, @nLevel
FROM member_hierarchy
WHERE member_hierarchy_id IN (
	SELECT sponsor_id
	FROM @group_hierarchy
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
		and active = 1
		
		IF @@ROWCOUNT > 0
			SELECT @nLevel = @nLevel + 1
	END
	ELSE
	BEGIN
		-- Go back to previous level
		SELECT @nLevel= @nLevel -1
	END
END



SELECT  mh.member_hierarchy_id
	, mh.parent_member_hierarchy_id
	, mh.member_id
	, mh.creation_channel_id
	, m.culture_code
	, m.opt_status_id
	, m.first_name
	, m.middle_name
	, m.last_name
	, m.gender
	, m.email_address
	, m.password
	, m.bounced
	, m.parent_first_name
	, m.parent_last_name
	, m.external_member_id
	, m.partner_id
	, m.comments
	, m.lead_id
	, m.facebook_id
	, cc.creation_channel_name
	, cc.description
	, cc.active
	, cc.member_type_id
	, mh.unsubscribe
	, dbo.es_get_user_type(mh.member_hierarchy_id)as user_type
	, m.create_date as member_created_date
	, mh.create_date as member_hierarchy_created_date
    , m.[user_id]
from @member_h as m_h
	inner join member_hierarchy as mh on mh.member_hierarchy_id = m_h.member_hierarchy_id
	inner join member as m on m.member_id = mh.member_id
	left outer join creation_channel cc on mh.creation_channel_id = cc.creation_channel_id
END
GO
