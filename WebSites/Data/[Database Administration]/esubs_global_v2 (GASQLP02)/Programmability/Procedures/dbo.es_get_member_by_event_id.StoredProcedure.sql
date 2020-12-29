USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_by_event_id]    Script Date: 02/14/2014 13:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--  exec [dbo].[es_get_member_by_event_id] 1002418
CREATE  PROCEDURE [dbo].[es_get_member_by_event_id]
    @event_id int
AS
BEGIN
    SELECT    
    	  mh.member_hierarchy_id
    	, mh.parent_member_hierarchy_id
    	, mh.member_id
    	, mh.creation_channel_id
    	, COALESCE(u.culture_code COLLATE database_default,m.culture_code COLLATE database_default) as culture_code
    	, COALESCE(u.opt_status_id,m.opt_status_id) as opt_status_id
    	, COALESCE(u.first_name COLLATE database_default,m.first_name COLLATE database_default) as first_name
    	, m.middle_name
    	, COALESCE(u.last_name COLLATE database_default,m.last_name COLLATE database_default) as last_name
    	, m.gender
    	, COALESCE(u.email_address COLLATE database_default,m.email_address COLLATE database_default) as email_address
    	, COALESCE(u.password COLLATE database_default,m.password COLLATE database_default) as password
    	, m.bounced
		, m.parent_first_name
		, m.parent_last_name
        , m.external_member_id
    	, m.comments
        , COALESCE(u.partner_id,m.partner_id) as partner_id
		, m.lead_id
		, m.facebook_id
    	, cc.creation_channel_name
    	, cc.description
    	, cc.active
		, mh.unsubscribe
    	, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type
		, COALESCE(u.create_date,m.create_date) as member_created_date
		, mh.create_date as member_hierarchy_created_date
        , m.[user_id]
    FROM         
    	member_hierarchy mh with (nolock)
    	JOIN member m with (nolock)
    	    ON mh.member_id = m.member_id and m.deleted = 0
    	JOIN creation_Channel cc with (nolock)
        	ON cc.creation_channel_id = mh.creation_channel_id
        JOIN event_participation ep with (nolock)
            ON ep.member_hierarchy_id = mh.member_hierarchy_id
        LEFT JOIN [users] u with (nolock)
			ON m.user_id = u.user_id
    WHERE ep.event_id = @event_id and mh.active = 1
       
END
GO
