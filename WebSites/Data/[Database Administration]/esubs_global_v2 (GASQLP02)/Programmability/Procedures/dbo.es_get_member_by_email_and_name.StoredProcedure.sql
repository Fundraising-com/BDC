USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_by_email_and_name]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
*/
CREATE   PROCEDURE [dbo].[es_get_member_by_email_and_name]
    @email_address varchar(100)
    ,@first_name varchar(100)
    ,@last_name varchar(100)
    ,@partner_id int = 0
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
       	, m.create_date
        , m.lead_id
		, m.facebook_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
	    , mh.unsubscribe
    	, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		, m.create_date as member_created_date
		, mh.create_date as member_hierarchy_created_date
        , m.[user_id]
    FROM member_hierarchy mh
    	INNER JOIN member m
        	ON mh.member_id = m.member_id 
    	left outer join creation_Channel cc
        	on cc.creation_channel_id = mh.creation_channel_id
    WHERE first_name = @first_name
      AND last_name = @last_name
      AND email_address = @email_address
      AND partner_id = @partner_id
      and mh.active = 1

END
GO
