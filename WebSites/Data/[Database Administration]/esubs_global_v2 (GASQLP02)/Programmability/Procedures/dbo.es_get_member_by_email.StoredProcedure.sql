USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_by_email]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
*/
CREATE   PROCEDURE [dbo].[es_get_member_by_email]
    @email varchar(100)
    , @partner_id int = 0
AS
BEGIN
    SELECT    
    	mh.member_hierarchy_id
    	, mh.parent_member_hierarchy_id
    	, mh.member_id
    	, mh.creation_channel_id
    	, u.culture_code
    	, u.opt_status_id
    	, u.first_name
    	, m.middle_name
    	, u.last_name
    	, m.gender
    	, u.email_address
    	, u.password
    	, m.bounced
		, m.parent_first_name
		, m.parent_last_name
        , m.external_member_id
    	, m.comments
       	, u.partner_id
		, m.lead_id
		, m.facebook_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
		, u.unsubscribe
    	, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		, u.create_date as member_created_date
		, mh.create_date as member_hierarchy_created_date
        , m.[user_id]
    FROM         
    	member_hierarchy mh
    	INNER JOIN member m
    	ON mh.member_id = m.member_id 
    	left outer join creation_Channel cc
    	on cc.creation_channel_id = mh.creation_channel_id
    	INNER JOIN users u
		on m.[user_id] = u.[user_id]
    WHERE u.email_address = @email
	and u.partner_id = @partner_id
	and mh.active = 1
END
GO
