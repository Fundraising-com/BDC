USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_top3_event_total_amount_by_partner_id]    Script Date: 2/15/2017 12:53:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: October 14, 2014
-- Description:	Gets top 3 events
-- EXEC [dbo].[es_get_top3_event_total_amount_by_partner_id] 143, 5, 588902
-- =============================================
ALTER PROCEDURE [dbo].[es_get_top3_event_total_amount_by_partner_id]
	@partner_id INT,
	@maxResults INT,
	@groupId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET ROWCOUNT @maxResults

    SELECT
	eta.id, 
    eta.event_id,
	eta.items, 
	eta.total_amount, 
	eta.total_supporters, 
	eta.total_donation_amount, 
    eta.total_donars,
    eta.total_profit, 
	eta.create_date,
	ISNULL(PI.image_url, NULL) AS image_url,
	E.event_name
	FROM dbo.event_total_amount eta (NOLOCK)
	JOIN dbo.event e (NOLOCK) ON eta.event_id = e.event_id 
	JOIN dbo.event_group eg (NOLOCK) ON e.event_id = eg.event_id 
	JOIN dbo.[group] g (NOLOCK) ON eg.group_id = g.group_id
	JOIN event_participation EP (NOLOCK) ON E.event_id = EP.event_id AND EP.participation_channel_id = 3
	JOIN personalization P (NOLOCK) ON EP.event_participation_id = P.event_participation_id
	LEFT JOIN personalization_image PI (NOLOCK) ON P.personalization_id = PI.personalization_id AND PI.image_approval_status_id <> 4 AND PI.isCoverAlbum = 1 AND PI.deleted <> 1
	WHERE 
	g.partner_id= @partner_id
	and e.active = 1
	AND (@groupId IS NULL OR g.group_id = @groupId)
	order by eta.total_amount DESC, eta.total_donation_amount DESC
END
