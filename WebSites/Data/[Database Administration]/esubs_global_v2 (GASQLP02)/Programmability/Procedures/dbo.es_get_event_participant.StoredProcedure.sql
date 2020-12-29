USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_participant]    Script Date: 02/14/2014 13:05:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_event_participant]
    @event_id int
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
        , m.external_member_id
    	, m.comments
    	, m.facebook_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
    	, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
	, p.personalization_id
	, p.redirect
FROM         
    	member_hierarchy mh
INNER JOIN member m
    	ON mh.member_id = m.member_id 
LEFT OUTER join creation_Channel cc
        ON cc.creation_channel_id = mh.creation_channel_id
INNER JOIN event_participation ep
    	ON ep.member_hierarchy_id = mh.member_hierarchy_id
left JOIN personalization p
	ON ep.event_participation_id = p.event_participation_id
WHERE ep.event_id = @event_id
       
END
GO
