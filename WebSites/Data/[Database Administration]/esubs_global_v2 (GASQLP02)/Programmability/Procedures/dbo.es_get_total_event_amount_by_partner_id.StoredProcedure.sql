USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_total_event_amount_by_partner_id]    Script Date: 02/14/2014 13:06:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: August 29, 2011
-- Description:	<Description,,>
--    exec [es_get_total_event_amount_by_partner_id] 833
-- =============================================
CREATE PROCEDURE [dbo].[es_get_total_event_amount_by_partner_id] 
	@partner_id int
AS
BEGIN
	SET NOCOUNT ON;

    SELECT  SUM(COALESCE(eta.items, 0)) AS items,
			SUM(COALESCE(eta.total_amount, 0)) AS total_amount,
			SUM(COALESCE(eta.total_supporters, 0)) AS total_supporters,
			SUM(COALESCE(eta.total_donation_amount, 0)) AS total_donation_amount,
            SUM(COALESCE(eta.total_donars, 0)) AS total_donars
	FROM event AS e WITH (NOLOCK)
	INNER JOIN event_group AS eg WITH (NOLOCK)
		ON eg.event_id = e.event_id 
	INNER JOIN [group] AS g WITH (NOLOCK)
		ON g.group_id = eg.group_id 
	LEFT JOIN event_total_amount eta WITH (NOLOCK)
		ON e.event_id = eta.event_id
	WHERE 
		(g.partner_id = @partner_id);
END
GO
