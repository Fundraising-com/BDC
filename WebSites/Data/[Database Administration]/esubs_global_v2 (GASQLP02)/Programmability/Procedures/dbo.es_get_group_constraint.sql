SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 15, 2014
-- Description:	Finds the redirect for the group
-- =============================================
CREATE PROCEDURE es_get_group_constraint
	@sponsorRedirect nvarchar(200)
AS
BEGIN
	SET NOCOUNT ON;

   SELECT
	e.event_id
	FROM [event] e (NOLOCK)
	JOIN [event_participation] ep (NOLOCK) ON e.event_id = ep.event_id
	JOIN [personalization] p (NOLOCK) ON ep.event_participation_id = p.event_participation_id
	WHERE
	e.active = 1
	AND ep.participation_channel_id = 3
	AND p.redirect = @sponsorRedirect
END
GO
--grant exec on es_get_group_constraint to db_stored_proc_exec 