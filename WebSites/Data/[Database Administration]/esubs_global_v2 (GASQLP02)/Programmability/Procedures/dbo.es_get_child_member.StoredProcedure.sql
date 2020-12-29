USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_child_member]    Script Date: 04/08/2014 14:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by:
	Date:
	Returns only the child of a member
	mod fblais : 2005-12-01 ajout du active pour gérer les unsubscribes
	EXEC [dbo].[es_get_child_member] 8963247,1469826
*/

CREATE PROCEDURE [dbo].[es_get_child_member]
	@member_hierarchy_id int , @eventID INT
AS
BEGIN
IF (SELECT parent_member_hierarchy_id FROM member_hierarchy WHERE member_hierarchy_id = @member_hierarchy_id) IS NULL
	BEGIN
		-- the top member is a sponsor
			-- SELECT mh.member_id
		SELECT DISTINCT
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
			, LTRIM(RTRIM(m.email_address)) AS email_address
			, m.password
			, m.bounced
			, m.parent_first_name
			, m.parent_last_name
			, m.external_member_id
		   	, m.partner_id
			, m.comments
			, m.lead_id
			, m.facebook_id
			, cc.creation_channel_name
			, cc.description
			, cc.active
			, mh.unsubscribe
			, 2 as user_type
			, ep.salutation
		    , m.[user_id]
		FROM         
			member_hierarchy mh
			INNER JOIN member m
			ON mh.member_id = m.member_id 
			LEFT OUTER JOIN creation_channel cc
			ON mh.creation_channel_id = cc.creation_channel_id
			INNER JOIN event_participation ep
			ON ep.member_hierarchy_id = mh.member_hierarchy_id
		WHERE mh.parent_member_hierarchy_id = @member_hierarchy_id
		AND	ep.event_id = @eventID
		  AND mh.creation_channel_id in (7, 20, 23, 35, 40)
		  AND mh.active = 1
		--order by m.first_name + ' ' + m.last_name
	END
	ELSE
	BEGIN
		-- the top member is a participant
		-- SELECT mh.member_id	
		SELECT DISTINCT
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
			, LTRIM(RTRIM(m.email_address)) AS email_address
			, m.password
			, m.bounced
			, m.external_member_id
			, m.partner_id
			, m.comments
			, m.lead_id
			, m.facebook_id
			, cc.creation_channel_name
			, cc.description
			, cc.active
			, mh.unsubscribe
			, 3 as user_type
			, ep.salutation
            , m.[user_id]
		FROM         
			member_hierarchy mh
			INNER JOIN member m
			ON mh.member_id = m.member_id 
			LEFT OUTER JOIN creation_channel cc
			ON mh.creation_channel_id = cc.creation_channel_id
			INNER JOIN event_participation ep
			ON ep.member_hierarchy_id = mh.member_hierarchy_id
		WHERE 
			mh.parent_member_hierarchy_id = @member_hierarchy_id 
		AND	ep.event_id = @eventID
		AND 	mh.creation_channel_id in (12, 14, 37, 41)
		and 	mh.active = 1
		--order by m.first_name + ' ' + m.last_name
	END
END
