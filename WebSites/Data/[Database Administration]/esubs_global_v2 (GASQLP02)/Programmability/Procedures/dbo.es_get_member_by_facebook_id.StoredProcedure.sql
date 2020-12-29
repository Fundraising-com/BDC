USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_by_facebook_id]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	created by: jf buist Dec. 12, 2007
*/
CREATE   PROCEDURE [dbo].[es_get_member_by_facebook_id]
	@facebook_id int = 0
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
       	, m.partner_id
		, m.lead_id
		, m.facebook_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
		, mh.unsubscribe
    	, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		, m.create_date as member_created_date
		, mh.create_date as member_hierarchy_created_date
    FROM         
    	member_hierarchy mh
    	INNER JOIN member m
    	ON mh.member_id = m.member_id 
    	left outer join creation_Channel cc
    	on cc.creation_channel_id = mh.creation_channel_id
    WHERE m.facebook_id = @facebook_id
	and mh.active = 1
END
GO
