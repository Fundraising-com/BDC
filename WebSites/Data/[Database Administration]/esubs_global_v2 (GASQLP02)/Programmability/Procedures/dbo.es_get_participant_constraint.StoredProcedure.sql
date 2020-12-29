USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_participant_constraint]    Script Date: 12/09/2014 13:02:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 15, 2014
-- Update:		December 09, 2014 (by Jiro Hidaka)
--              => was returning more entries than it needs
-- Description:	Finds a redirection for a participant and an event
-- =============================================
ALTER PROCEDURE [dbo].[es_get_participant_constraint]
	@sponsorRedirect nvarchar(200)
	,@participantRedirect nvarchar(200)
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT 
		e.event_id
		,child.event_participation_id
	FROM
		[event] e (NOLOCK)
		JOIN [event_participation] ep (NOLOCK) ON e.event_id = ep.event_id
		JOIN [personalization] p (NOLOCK) ON ep.event_participation_id = p.event_participation_id
		JOIN [member_hierarchy] mh (NOLOCk) ON ep.member_hierarchy_id = mh.member_hierarchy_id
		JOIN	
		(SELECT 
			e.event_id
			,p.event_participation_id
			,mh.parent_member_hierarchy_id
		FROM
			 [event] e (NOLOCK)
		JOIN [event_participation] ep (NOLOCK) ON e.event_id = ep.event_id
		JOIN [personalization] p (NOLOCK) ON ep.event_participation_id = p.event_participation_id
		JOIN [member_hierarchy] mh (NOLOCk) ON ep.member_hierarchy_id = mh.member_hierarchy_id
		WHERE e.active=1 
		  AND ep.participation_channel_id <> 3
		  AND p.redirect = @participantRedirect) child on mh.member_hierarchy_id = child.parent_member_hierarchy_id AND e.event_id = child.event_id
	WHERE
		e.active = 1
		AND ep.participation_channel_id = 3
		AND p.redirect = @sponsorRedirect
		
/*
    SELECT 
		e.event_id
		,pp.event_participation_id
	FROM
		[event] e (NOLOCK)
		JOIN [event_participation] esp (NOLOCK) ON e.event_id = esp.event_id
		JOIN [personalization] sp (NOLOCK) ON esp.event_participation_id = sp.event_participation_id
		JOIN [member_hierarchy] smh (NOLOCk) ON esp.member_hierarchy_id = smh.member_hierarchy_id
		JOIN [member_hierarchy] pmh (NOLOCK) ON smh.member_hierarchy_id = pmh.parent_member_hierarchy_id
		JOIN [event_participation] pep (NOLOCK) ON pmh.member_hierarchy_id = pep.member_hierarchy_id
		JOIN [personalization] pp (NOLOCK) ON pep.event_participation_id = pp.event_participation_id
	WHERE
		e.active = 1
		AND esp.participation_channel_id = 3
		AND sp.redirect = @sponsorRedirect
		AND pp.redirect = @participantRedirect
*/
END
