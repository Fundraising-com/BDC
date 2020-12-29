USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_sponsor_email_donation_amount]    Script Date: 04/20/2015 10:17:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ==========================================================
-- Author:		Jiro Hidaka
-- Create date: August 26, 2011
-- Description:	<Description,,>
-- UPDATE MARCH 14, 2013 by Jiro Hidaka
--			member_name was not coming from users table
--   EXEC [dbo].[es_rpt_sponsor_email_donation_amount] 1554624 1472242
-- ==========================================================
ALTER PROCEDURE [dbo].[es_rpt_sponsor_email_donation_amount]
	@event_id INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
		(CASE 
			-- sponsor order must be under his name
			WHEN (mp.first_name + ' ' + mp.last_name) is null 
			THEN CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END
			-- participant orders must be under his name
			WHEN cc.member_type_id = 2
			THEN CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END
			ELSE CASE WHEN (up.first_name + ' ' + up.last_name) IS NOT NULL THEN dbo.TitleCase(lower(up.first_name + ' ' + up.last_name))
			          ELSE dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
			     END
		END) AS member_name
		, 
		(CASE
			WHEN (m.first_name + ' ' + m.last_name) is null
				THEN ''
			ELSE dbo.TitleCase(LOWER(m.first_name + ' ' + m.last_name))
		END) AS supporter_name 
		, COUNT ( distinct CASE WHEN mh.creation_channel_id in(12,14,29,33,38, 35)
							    THEN m.member_id ELSE NULL END ) AS email_sent
        ,CAST(SUM(od.quantity * od.price) AS FLOAT) AS donation_amount
	FROM [event_participation] ep WITH (NOLOCK)
		INNER JOIN [event_group] eg WITH (NOLOCK) ON eg.event_id = ep.event_id 
		INNER JOIN [event] e WITH (NOLOCK) ON e.event_id = eg.event_id
		INNER JOIN [group] g WITH (NOLOCK) ON g.group_id = eg.group_id		
		-- enfant
		INNER JOIN [member_hierarchy] mh WITH (NOLOCK) ON mh.member_hierarchy_id = ep.member_hierarchy_id
		INNER JOIN [member] m WITH (NOLOCK) ON m.member_id = mh.member_id
		LEFT OUTER JOIN users u with (nolock) on m.user_id = u.user_id
        INNER JOIN [creation_channel] cc WITH (NOLOCK) ON cc.creation_channel_id = mh.creation_channel_id
		-- parent
		LEFT OUTER JOIN [member_hierarchy] mhp WITH (NOLOCK) ON mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		LEFT OUTER JOIN [member] mp WITH (NOLOCK) ON mp.member_id = mhp.member_id
		LEFT OUTER JOIN users up with (nolock) on mp.user_id = up.user_id
        -- Donation orders from EFRECommerce db
		LEFT JOIN EFRECommerce.dbo.[order] o WITH (NOLOCK) ON o.ext_participation_id = ep.event_participation_id
        LEFT JOIN EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
        LEFT JOIN dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
	WHERE ep.event_id = @event_id 
	  AND mh.active = 1 
	  AND o.deleted = 0 
	  AND o.source_id = 1
	  AND o.status_id = 7
	GROUP BY 
		(CASE 
			WHEN (mp.first_name + ' ' + mp.last_name) is null 
			THEN CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END
			WHEN cc.member_type_id = 2
			THEN CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END
			ELSE CASE WHEN (up.first_name + ' ' + up.last_name) IS NOT NULL THEN dbo.TitleCase(lower(up.first_name + ' ' + up.last_name))
			          ELSE dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
			     END
		END)
		, 
		(CASE
			WHEN (m.first_name + ' ' + m.last_name) is null
				THEN ''
			ELSE dbo.TitleCase(LOWER(m.first_name + ' ' + m.last_name))
		END)
	ORDER BY 1, 2
END

