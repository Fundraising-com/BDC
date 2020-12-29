USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_supporters_invited_donation_amount]    Script Date: 02/14/2014 13:07:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: August 27, 2011
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_rpt_supporters_invited_donation_amount]
	@event_participation_id int
AS
BEGIN
	SET NOCOUNT ON;

	CREATE table #supp (
		event_participation_id int
	)

    INSERT INTO #supp (
        event_participation_id
    )
    SELECT @event_participation_id
    UNION all
    SELECT ep.event_participation_id
    FROM event_participation ep
            INNER JOIN member_hierarchy mh ON mh.member_hierarchy_id = ep.member_hierarchy_id
            INNER JOIN member_hierarchy mhp ON mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
            INNER JOIN event_participation epp ON epp.member_hierarchy_id = mhp.member_hierarchy_id
    WHERE epp.event_participation_id = @event_participation_id

    CREATE INDEX ix_event_participation_id ON #supp (event_participation_id)

    -- supporters orders 
	SELECT 
		  dbo.TitleCase(lower(m.first_name)) AS first_name
		, dbo.TitleCase(lower(m.last_name)) AS last_name
		, ep.create_date
		, COALESCE(SUM(od.quantity * od.price),0) AS donation_amount
	FROM event_participation ep
		INNER JOIN [event] e WITH (NOLOCK) ON e.event_id = ep.event_id
		-- filter out the current part
		INNER JOIN #supp supp WITH (NOLOCK) ON supp.event_participation_id = ep.event_participation_id
		-- Donation orders from EFRECommerce db
		LEFT JOIN EFRECommerce.dbo.[order] o WITH (NOLOCK) ON o.ext_participation_id = ep.event_participation_id
        LEFT JOIN EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
        LEFT JOIN dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
		-- current part
		INNER JOIN member_hierarchy mh WITH (NOLOCK) ON mh.member_hierarchy_id = ep.member_hierarchy_id 
		INNER JOIN member m WITH (NOLOCK) ON m.member_id = mh.member_id
	WHERE mh.active = 1 AND o.deleted = 0 AND o.source_id = 1
	GROUP BY 
		 m.first_name
		, m.last_name
		, ep.create_date
		, (CASE WHEN LTRIM(m.first_name + m.last_name) <> '' THEN m.email_address ELSE NULL END)
	ORDER BY 1, 2

	RETURN 0
END
GO
