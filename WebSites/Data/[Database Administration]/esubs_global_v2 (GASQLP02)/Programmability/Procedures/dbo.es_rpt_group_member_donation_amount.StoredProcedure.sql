USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_member_donation_amount]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: August 28, 2011
-- Description:	<Description,,>
-- UPDATE MARCH 13, 2013:
--		member_name must come frmo users table
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_group_member_donation_amount]
	@event_id int
AS
BEGIN
	SET NOCOUNT ON;

     -- participant orders 
    SELECT 
	    (CASE 
		    -- sponsor order must be under his name
		    WHEN (mp.first_name + ' ' + mp.last_name) IS NULL 
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
		    END)  AS member_name
	    , count ( distinct CASE WHEN mh.creation_channel_id in(12,14,29,33,38, 35)
		    THEN m.member_id ELSE NULL END ) AS email_sent
        , COALESCE(SUM(od.quantity * od.price),0) AS donation_amount
    FROM event_participation ep
		INNER JOIN [event] e ON e.event_id = ep.event_id
	    INNER JOIN event_group eg ON eg.event_id = ep.event_id 
	    INNER JOIN [group] g ON g.group_id = eg.group_id
	    -- Donation orders from EFRECommerce db
		LEFT JOIN EFRECommerce.dbo.[order] o WITH (NOLOCK) ON o.ext_participation_id = ep.event_participation_id
        LEFT JOIN EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
        left join dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
	    -- enfant
	    INNER JOIN member_hierarchy mh ON mh.member_hierarchy_id = ep.member_hierarchy_id
	    INNER JOIN member m ON m.member_id = mh.member_id
	    LEFT JOIN users u with (nolock) on m.user_id = u.user_id
	    -- parent
	    LEFT OUTER JOIN member_hierarchy mhp ON mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    LEFT OUTER JOIN member mp ON mp.member_id = mhp.member_id
	    LEFT JOIN users up with (nolock) on mp.user_id = up.user_id
	    LEFT JOIN creation_channel cc ON cc.creation_channel_id = mh.creation_channel_id
    WHERE ep.event_id = @event_id AND mh.active = 1 AND o.deleted = 0 AND o.source_id = 1
    GROUP BY (CASE 
		    -- sponsor order must be under his name
		    WHEN (mp.first_name + ' ' + mp.last_name) IS NULL 
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
		    END)  
    ORDER BY 1, 2
END
GO
