USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_nb_reminder_contact]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created by: Jiro Hidaka
	Date:		11/11/2011

    exec es_get_nb_reminder_contact 5867284, 1336902
*/

CREATE  PROCEDURE [dbo].[es_get_nb_reminder_contact]
	@member_hierarchy_id int , @eventID INT
AS
BEGIN
IF (SELECT parent_member_hierarchy_id FROM member_hierarchy WHERE member_hierarchy_id = @member_hierarchy_id) IS NULL
	BEGIN
		-- the top member is a sponsor
			-- SELECT mh.member_id
		SELECT  COUNT(m.member_id) as total_count
		FROM         
			member_hierarchy mh
			INNER JOIN member m
			ON mh.member_id = m.member_id 
			LEFT OUTER JOIN creation_channel cc
			ON mh.creation_channel_id = cc.creation_channel_id
			INNER JOIN event_participation ep
			ON ep.member_hierarchy_id = mh.member_hierarchy_id
		WHERE mh.parent_member_hierarchy_id = @member_hierarchy_id
		  AND ep.event_id = @eventID
		  AND mh.creation_channel_id in (7, 20, 23, 35, 40)
		  AND mh.active = 1 AND m.opt_status_id = 1
	END
	ELSE
	BEGIN
		-- the top member is a participant
		-- SELECT mh.member_id	
		SELECT COUNT(m.member_id) as total_count
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
		AND mh.creation_channel_id in (12, 14, 37, 41)
		and mh.active = 1
		AND m.opt_status_id = 1
	END
END
GO
