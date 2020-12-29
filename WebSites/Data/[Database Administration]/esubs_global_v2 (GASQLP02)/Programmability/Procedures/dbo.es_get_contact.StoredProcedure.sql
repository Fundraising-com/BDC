USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_contact]    Script Date: 02/14/2014 13:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_contact]
	@member_hierarchy_id int,
	@bounced bit = 0 
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
		   	, m.partner_id
			, m.comments
			, m.lead_id
			, m.facebook_id
			, m.deleted
			, cc.creation_channel_name
			, cc.description
			, cc.active
			, mh.unsubscribe
			, dbo.es_get_user_type(mh.member_hierarchy_id) as user_type		
			, m.user_id
		FROM         
			member_hierarchy mh with (nolock)
			INNER JOIN member m with (nolock)
			ON mh.member_id = m.member_id 
			INNER JOIN creation_channel cc with (nolock)
			ON mh.creation_channel_id = cc.creation_channel_id
            INNER JOIN
            (
				SELECT MIN(mh.member_hierarchy_id) as member_hierarchy_id
				FROM         
					member_hierarchy mh with (nolock)
					INNER JOIN member m with (nolock)
					ON mh.member_id = m.member_id 
					INNER JOIN creation_channel cc with (nolock)
					ON mh.creation_channel_id = cc.creation_channel_id
                WHERE mh.parent_member_hierarchy_id = @member_hierarchy_id		
				  AND cc.is_contact = 1
				  AND mh.active = 1
				  AND m.deleted= 0
				  AND bounced = @bounced
                GROUP BY mh.member_id, cc.member_type_id
            ) t on mh.member_hierarchy_id = t.member_hierarchy_id

		order by m.first_name + ' ' + m.last_name
	
END
GO
