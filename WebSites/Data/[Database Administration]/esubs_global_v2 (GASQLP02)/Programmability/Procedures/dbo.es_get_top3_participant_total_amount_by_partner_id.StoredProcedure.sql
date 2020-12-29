USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_top3_participant_total_amount_by_partner_id]    Script Date: 2/15/2017 12:46:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- EXEC es_get_top3_participant_total_amount_by_partner_id 143, 5, 588902
ALTER PROCEDURE [dbo].[es_get_top3_participant_total_amount_by_partner_id]
	@partner_id INT,
	@maxResults INT,
	@groupId INT = NULL
AS
BEGIN

SET ROWCOUNT @maxResults

SELECT
	TA.event_participation_id, 
    TA.participant_name,
	TA.items, 
	TA.total_amount, 
	TA.total_supporters, 
	TA.total_donation_amount, 
    TA.total_donors,
    TA.total_profit, 
	TA.create_date,
	ISNULL(P.fundraising_goal, 0) AS goal,
	ISNULL(PI.image_url, NULL) AS image_url,
	E.event_name,
	E.event_id
FROM  dbo.participant_total_amount TA (NOLOCK)
JOIN dbo.event_participation EP (NOLOCK) ON TA.event_participation_id = EP.event_participation_id 
JOIN dbo.event (NOLOCK) E ON EP.event_id = E.event_id 
JOIN dbo.event_group EG (NOLOCK) ON E.event_id = EG.event_id 
JOIN dbo.[group] G (NOLOCK) ON EG.group_id = G.group_id
JOIN personalization P (NOLOCK) ON EP.event_participation_id = P.event_participation_id
LEFT JOIN personalization_image PI (NOLOCK) ON P.personalization_id = PI.personalization_id AND PI.image_approval_status_id <> 4 AND PI.isCoverAlbum = 1 AND PI.deleted <> 1
WHERE 
G.partner_id= @partner_id
AND E.active = 1
AND (@groupId IS NULL OR g.group_id = @groupId)
order by TA.total_amount DESC, TA.total_donation_amount DESC
END