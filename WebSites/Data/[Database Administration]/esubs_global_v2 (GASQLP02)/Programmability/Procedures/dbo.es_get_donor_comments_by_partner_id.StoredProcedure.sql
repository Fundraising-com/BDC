USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_donor_comments_by_partner_id]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jiro Hidaka
-- Create date: 09/03/2011
-- Description:	<Description,,>
--   exec es_get_donor_comments_by_partner_id 833
-- =============================================
CREATE PROCEDURE [dbo].[es_get_donor_comments_by_partner_id]
	@partner_id int
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT ep.event_participation_id, ep.event_id, mh.member_hierarchy_id
           ,(CASE WHEN (oc.first_name + ' ' + oc.last_name) is null 
				  THEN dbo.TitleCase(LOWER(m.first_name + ' ' + m.last_name))
			      ELSE dbo.TitleCase(LOWER(oc.first_name + ' ' + oc.last_name))
		     END) AS donor_name
		   ,(od.quantity * od.price) as donation_amount
		   , ISNULL(oc.comment, '') as donor_comments
	FROM event_participation ep WITH (NOLOCK)
	INNER JOIN [event] e WITH (NOLOCK) ON e.event_id = ep.event_id
	INNER JOIN member_hierarchy mh WITH (NOLOCK) ON mh.member_hierarchy_id = ep.member_hierarchy_id 
	INNER JOIN member m WITH (NOLOCK) ON m.member_id = mh.member_id

	-- Donation orders from EFRECommerce db:
    INNER JOIN EFRECommerce.dbo.[order] o WITH (NOLOCK) ON ep.event_participation_id = o.ext_participation_id AND o.deleted = 0 AND o.source_id = 1
    INNER JOIN EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
    LEFT JOIN EFRECommerce.dbo.order_comment oc WITH (NOLOCK) ON o.order_comment_id = oc.order_comment_id AND oc.visible = 1 AND oc.deleted = 0
    INNER JOIN dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
	WHERE mh.active = 1 and e.active = 1 and oc.visible =1 and m.partner_id = @partner_id;
END
GO
