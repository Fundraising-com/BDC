USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_sponsor_by_group_id]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_sponsor_by_group_id]
    @group_id int
AS
BEGIN    
    SELECT    
    	mh.member_hierarchy_id
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
    	, m.comments
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
    	, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
    FROM [group] g
        INNER JOIN member_hierarchy mh
            ON mh.member_hierarchy_id = g.sponsor_id
    	INNER JOIN member m
    	    ON mh.member_id = m.member_id 
    	LEFT OUTER JOIN creation_channel cc
    	    ON mh.creation_channel_id = cc.creation_channel_id
    WHERE g.group_id = @group_id
END
GO
