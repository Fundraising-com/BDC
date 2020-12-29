USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_supporter_stats_summary]    Script Date: 02/14/2014 13:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:		JIRO HIDAKA
-- Create date: 08/27/09
-- Description:	Returns Supporter Center stats results filtered by
--              event id and event particpation id
-- ===============================================================
CREATE PROCEDURE [dbo].[es_get_supporter_stats_summary]
	@event_id int,
    @eventParticipationID int
AS
BEGIN    
    select ep.event_id
	     , count( distinct case when mh.creation_channel_id in (12,14,29,33,37) and t.event_participation_id is not null 
			      and ti.business_rule_id in (104) and epp.event_participation_id = @eventParticipationID
			      then m.member_id else NULL end ) as nb_supp_email
	     , count( distinct case when mh.creation_channel_id in (12,14,29,33,37) and tps.supp_id is not null 
                  and epp.event_participation_id = @eventParticipationID
				  then  m.member_id else NULL end ) supp_bought
    from event_participation ep with(nolock)
	    inner join event_group eg with(nolock) on eg.event_id = ep.event_id 
	    inner join [group] g with(nolock) on g.group_id = eg.group_id 
		--emails
		left join touch t with(nolock) on ep.event_participation_id = t.event_participation_id
		left join touch_info ti with(nolock) on ti.touch_info_id = t.touch_info_id
	    -- order
        left outer join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock) on m.member_id = mh.member_id
	    -- parent
	    inner join event_participation epp with(nolock) on mh.parent_member_hierarchy_id = epp.member_hierarchy_id
    where ep.event_id = @event_id
     -- and mh.active = 1
    group by ep.event_id
    order by 1, 2

END
GO
