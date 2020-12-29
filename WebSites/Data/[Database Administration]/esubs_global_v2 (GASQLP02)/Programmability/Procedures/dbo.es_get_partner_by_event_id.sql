SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Javier Arellano
-- Create date: October 23, 2014
-- Description:	Returns the partner id for the event received
-- =============================================
CREATE PROCEDURE es_get_partner_by_event_id
	@eventId INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
	ISNULL(G.partner_id, 0) AS partner_id
	FROM [event_group] EG (NOLOCK)
	JOIN [group] G (NOLOCK) ON EG.group_id = G.group_id
	WHERE EG.event_id = @eventId;
END
GO

--grant exec on es_get_partner_by_event_id to db_stored_proc_exec 
