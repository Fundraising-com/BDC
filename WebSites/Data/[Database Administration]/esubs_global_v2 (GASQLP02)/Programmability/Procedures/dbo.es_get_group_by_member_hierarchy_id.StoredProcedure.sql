USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_group_by_member_hierarchy_id]    Script Date: 02/14/2014 13:05:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
    es_get_group_by_member_hierarchy_id

    Created by: Philippe Girard
    Created on: 31/08/2005
    
    Description: Get the group of a member_hierarchy id
*/
CREATE PROCEDURE [dbo].[es_get_group_by_member_hierarchy_id]
    @member_hierarchy_id int
AS
BEGIN
    
    DECLARE @group_id int

    WHILE (SELECT parent_member_hierarchy_id
             FROM member_hierarchy 
            WHERE member_hierarchy_id = @member_hierarchy_id) IS NOT NULL
    BEGIN
        SELECT @member_hierarchy_id = parent_member_hierarchy_id 
          FROM member_hierarchy
         WHERE member_hierarchy_id = @member_hierarchy_id
    END
    
    SELECT @group_id = group_id
      FROM [group] g
     WHERE sponsor_id = @member_hierarchy_id

    SELECT 
	g.group_id
	, g.parent_group_id
	, g.sponsor_id
	, g.partner_id
	, g.lead_id
	, g.external_group_id
	, g.group_name
	, g.test_group
	, g.expected_membership
	, g.redirect
	, g.group_url
	, g.comments
	, g.create_date
    FROM [group] g 
    WHERE (g.group_id = @group_id)
END
GO
