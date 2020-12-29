USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_supporter_relaunch]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_get_supporter_relaunch]
	@event_id int
	,@member_hierarchy_id int
as

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
   	, m.partner_id
	, m.comments
    	, m.lead_id
	, cc.creation_channel_name
	, cc.description
	, cc.active
	, mh.unsubscribe
	, 2 as user_type
	, m.create_date as member_created_date
	, mh.create_date as member_hierarchy_created_date
FROM         
	member_hierarchy mh
	INNER JOIN member m
	ON mh.member_id = m.member_id 
	inner JOIN creation_channel cc
	ON mh.creation_channel_id = cc.creation_channel_id
	left outer join event_participation ep
	on ep.member_hierarchy_id = mh.member_hierarchy_id
	and ep.event_id= @event_id
WHERE 
	parent_member_hierarchy_id = @member_hierarchy_id
AND 	mh.creation_channel_id =29
AND 	mh.active = 1
and	ep.event_participation_id is null
GO
