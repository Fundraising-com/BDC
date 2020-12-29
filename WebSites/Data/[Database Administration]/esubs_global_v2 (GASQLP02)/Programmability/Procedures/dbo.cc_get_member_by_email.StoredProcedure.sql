USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_member_by_email]    Script Date: 03/08/2015 22:51:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--  exec [dbo].[cc_get_member_by_email] 'hidaka7777@yahoo.com'
ALTER   PROCEDURE [dbo].[cc_get_member_by_email]
    @email varchar(100)   
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
		--, m.parent_first_name
		--, m.parent_last_name
        --, m.external_member_id
    	--, m.comments
       	, u.partner_id
		, m.lead_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
		, mh.unsubscribe
		, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		, u.create_date
		--, mh.create_date as member_hierarchy_created_date
		, u.user_id
		, p.partner_name
		FROM         
    		member_hierarchy mh with (nolock)
    		JOIN member m with (nolock)
    		ON mh.member_id = m.member_id and m.deleted = 0
    		JOIN creation_Channel cc with (nolock)
    		on cc.creation_channel_id = mh.creation_channel_id
    		JOIN users u with (nolock)
    		on m.user_id = u.user_id
    		JOIN partner p (nolock)
    		on u.partner_id = p.partner_id
		WHERE (u.email_address = @email) and mh.active = 1
		GROUP BY 
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
		--, m.parent_first_name
		--, m.parent_last_name
  --      , m.external_member_id
  --  	, m.comments
       	, u.partner_id
		, m.lead_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
		, mh.unsubscribe
		, dbo.es_get_user_type(mh.member_hierarchy_id)
		, u.create_date
		--, mh.create_date
		, u.user_id
		, p.partner_name
		ORDER BY u.partner_id
END
