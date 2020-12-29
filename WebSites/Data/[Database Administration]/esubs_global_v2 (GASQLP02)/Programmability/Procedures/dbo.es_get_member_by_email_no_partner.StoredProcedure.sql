USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_by_email_no_partner]    Script Date: 02/14/2014 13:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
	exec [dbo].[es_get_member_by_email_no_partner] 'jiro_hidaka@qsp.com'
*/
CREATE   PROCEDURE [dbo].[es_get_member_by_email_no_partner]
    @email varchar(100)
AS
BEGIN
    SELECT    
    	mh.member_hierarchy_id
    	, mh.parent_member_hierarchy_id
    	, mh.member_id
    	, mh.creation_channel_id
    	, COALESCE(u.culture_code COLLATE SQL_Latin1_General_CP1_CI_AS , m.culture_code) as culture_code
    	, COALESCE(u.opt_status_id, m.opt_status_id) as opt_status_id
    	, COALESCE(u.first_name COLLATE SQL_Latin1_General_CP1_CI_AS, m.first_name) as first_name
    	, m.middle_name
    	, COALESCE(u.last_name COLLATE SQL_Latin1_General_CP1_CI_AS, m.last_name) as last_name
    	, m.gender
    	, COALESCE(u.email_address COLLATE SQL_Latin1_General_CP1_CI_AS, m.email_address) as email_address
    	, COALESCE(u.password COLLATE SQL_Latin1_General_CP1_CI_AS, m.password) as password
    	, m.bounced
		, m.parent_first_name
		, m.parent_last_name
        , m.external_member_id
    	, m.comments
       	, COALESCE(u.partner_id, m.partner_id) as partner_id
		, m.lead_id
		, m.facebook_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
		, mh.unsubscribe
    	, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		, COALESCE(u.create_date, m.create_date) as member_created_date
		, mh.create_date as member_hierarchy_created_date
        , m.[user_id]
    FROM         
    	member_hierarchy mh
    	INNER JOIN member m
    	ON mh.member_id = m.member_id 
    	left outer join creation_Channel cc
    	on cc.creation_channel_id = mh.creation_channel_id
    	left outer join [users] u
    	on m.user_id = u.user_id
    WHERE m.email_address = @email
	and mh.active = 1
END
GO
