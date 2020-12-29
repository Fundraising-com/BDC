SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 15, 2014
-- Description:	Finds a redirection for a participant and an event
-- =============================================
CREATE PROCEDURE es_get_participant_constraint
	@sponsorRedirect nvarchar(200)
	,@participantRedirect nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;

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
END
GO
--grant exec on es_get_participant_constraint to db_stored_proc_exec 