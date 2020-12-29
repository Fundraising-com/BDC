SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 21, 2014
-- Description:	Finds a member by the event participation id received
-- =============================================
ALTER PROCEDURE es_get_member_by_event_participation_id
	@participantId INT
AS
BEGIN
	SET NOCOUNT ON;

    SELECT
		M.member_id, M.user_id, M.first_name, M.last_name, M.password, M.email_address, M.partner_id
	FROM
		event_participation EP (NOLOCK)
		JOIN member_hierarchy MH (NOLOCK) ON EP.member_hierarchy_id = MH.member_hierarchy_id
		JOIN member M (NOLOCK) ON MH.member_id = M.member_id
		WHERE EP.event_participation_id = @participantId
END
GO
--grant exec on es_get_member_by_event_participation_id to db_stored_proc_exec 