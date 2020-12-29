USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_participant_stats_summary]    Script Date: 02/14/2014 13:06:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [es_get_participant_stats_summary] 1002418

CREATE     PROCEDURE [dbo].[es_get_participant_stats_summary] 
	@event_id int
AS
BEGIN    
    /*
    < 1 mai 2004        tout est 40%
    <16 sept 2004        le 100% n'a pas de maximum
    <16 sept 2005        le 100% a un maximum de 25$
    >16 sept 2005        le 100%  est sur le premier 25$
    >16 mai 2006	fin de 100% profit sur first sub
    */

    select ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35,38) and t.event_participation_id is not null and ti.business_rule_id in (96,115,164) then m.member_id else NULL end) as nb_gm_email
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35,38) then m.member_id else NULL end ) as nb_gm
	     , count(distinct case when mh.creation_channel_id in(12,14,29,33,37,46) and ti.business_rule_id in (104) then mh.parent_member_hierarchy_id else null end) as gm_invited
		 , count(distinct case when mh.creation_channel_id in(7,20,23,32,35,38) and tps.supp_id is not null then  m.member_id else NULL end ) gm_bought
    from event_participation ep with(nolock)
	    inner join event_group eg with(nolock) on eg.event_id = ep.event_id 
	    inner join [group] g with(nolock) on g.group_id = eg.group_id 
		--emails
		left join touch t with(nolock) on ep.event_participation_id = t.event_participation_id
		left join touch_info ti with(nolock) on ti.touch_info_id = t.touch_info_id
	    -- order
        left join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock) on m.member_id = mh.member_id and m.deleted = 0
    where ep.event_id = @event_id
     -- and mh.active = 1
    group by ep.event_id
    order by 1, 2

END
GO
