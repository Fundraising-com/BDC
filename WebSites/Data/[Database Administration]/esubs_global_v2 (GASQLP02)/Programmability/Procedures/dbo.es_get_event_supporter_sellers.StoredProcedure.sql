USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_supporter_sellers]    Script Date: 02/14/2014 13:05:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================================
-- Author:		JIRO HIDAKA
-- Create date: 08/27/09
-- Description:	Returns supporter sellers for event filtered by
--              event id and event particpation id
-- ============================================================
CREATE PROCEDURE [dbo].[es_get_event_supporter_sellers]
	@event_id int,
    @eventParticipationID int
AS
select
	ISNULL(tps.first_name, m.first_name) + ' ' + ISNULL(tps.last_name, m.last_name) as [supp_name]
	, sum(case 
		  when tps.product_type_id = 18 and tps.store_id = 1 then 0
		  else tps.quantity end) as quantity
	, sum(case 
	      when tps.product_type_id = 18 and tps.store_id = 1 then 0
		  else tps.sub_total end) as amount
	, min(m.create_date) as create_date
    , COALESCE(sum(od.quantity * od.price), 0) AS donation_amount
from
	member_hierarchy mh WITH (NOLOCK)
	inner join member m WITH (NOLOCK)
	on m.member_id = mh.member_id
	inner join event_participation ep WITH (NOLOCK)
	on ep.member_hierarchy_id=mh.member_hierarchy_id
	inner join [es_get_valid_orders_items_by_event_id] (@event_id) tps
	on tps.supp_id = ep.event_participation_id	
    -- parent
	inner join event_participation epp WITH (NOLOCK) on mh.parent_member_hierarchy_id = epp.member_hierarchy_id

	-- Donation orders from EFRECommerce db:
    left join EFRECommerce.dbo.[order] o WITH (NOLOCK) ON ep.event_participation_id = o.ext_participation_id AND o.deleted = 0 AND o.source_id = 1
    left join EFRECommerce.dbo.order_detail od WITH (NOLOCK) ON o.order_id = od.order_id AND od.deleted = 0 AND od.product_id = 1
    left join dbo.es_get_valid_efrecommerce_order_status() efreos ON o.status_id = efreos.status_id
where
	(mh.creation_channel_id in(12,14,29,33,37)) -- on prend les supporters invited seulement
	and ep.event_id = @event_id and epp.event_participation_id = @eventParticipationID
group by 
	ISNULL(tps.first_name, m.first_name)
	,ISNULL(tps.last_name, m.last_name)
order by 3 desc
GO
