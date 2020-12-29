USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_campaign_supporter_report]    Script Date: 02/14/2014 13:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 

Description: supporter report dans le campaign manager

Ex: EXEC [dbo].[es_rpt_campaign_supporter_report] 1002418

mod fblais      2005-12-01 	
                - on prend pas les unsubscribe.

mod mcote       2006-02-18
                - creation de table temporaire (optimisation)
				- obtention du 100% profit 1rst sub
				- modif relation parent enfant pour reporting
				  la modif consiste a donner le credit des ventes 
				  aux participants meme si ces dernier sont enfant 
				  du sponsor.
				- enlever le select imbriquer.

mod pgirard     2006-12-13
                changé l'utilisation de createdate pour updatedate
                
mod jhidaka		2013-03-14
				part_name must come from users table

exec [dbo].[es_rpt_campaign_supporter_report] 1002418

*/
CREATE PROCEDURE [dbo].[es_rpt_campaign_supporter_report]
    @event_id int
AS
BEGIN  

    -- supporters orders 
    select (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END
		    else CASE WHEN (up.first_name + ' ' + up.last_name) IS NOT NULL THEN dbo.TitleCase(lower(up.first_name + ' ' + up.last_name))
			          ELSE dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
			     END
		    end)  as part_name
	    , m.first_name + ' ' + m.last_name as supp_name
	    , sum(quantity) as nb_subs
	    , sum(sub_total) as amount
	    , sum(case 
		    -- Fin de 100% Profit sur first subs
		    when tps.rownum = 1 and tps.create_date > '2006-05-16' then 
			    sub_total * (Isnull(ppc.profit_percentage, 40.0)/100.0) 
		    -- 100% premier 25$ 40% reste de l'order
		    when tps.rownum = 1 and tps.create_date > '2005-10-16' then 
			    (case when sub_total > 25  then (((sub_total) - 25) * (Isnull(ppc.profit_percentage, 40.0)/100.0)) + 25
			    else sub_total end)
		    -- 100% maximum 25$
		    when tps.rownum = 1 and tps.create_date < '2005-10-16' then 
			    (case when sub_total > 25  then 25
			    else sub_total end)
		    when tps.item_type_id = 6 and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
				sub_total * 25.0/100.0
		    else sub_total * (Isnull(ppc.profit_percentage, 40.0)/100.0) end) as profit
	    , min(tps.create_date) as updatedate
    from event_participation ep
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
-- profit
inner join partner prt  on prt.partner_id = g.partner_id
left join partner_payment_config ppc on ppc.partner_id = prt.partner_id
	    -- order
        left outer join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id 
	    inner join member m on m.member_id = mh.member_id
	    left join users u with (nolock) on m.user_id = u.user_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    left join users up with (nolock) on mp.user_id = up.user_id
	    inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
    where ep.event_id = @event_id
	  and mh.active = 1
    group by 
	      (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			then CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
			          ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
			     END
		    else CASE WHEN (up.first_name + ' ' + up.last_name) IS NOT NULL THEN dbo.TitleCase(lower(up.first_name + ' ' + up.last_name))
			          ELSE dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
			     END
		    end)
	    , mp.first_name 
	    , mp.last_name
	    , m.first_name
	    , m.last_name
	    , (case when ltrim(m.first_name + m.last_name) <> '' then m.email_address else NULL end)
    order by 1, 2


    --on va chercher les ventes pour les autres supporters 
    -- sans invitations
    -- pas implélmenter dans le code pour l'affichage seulement mais le code attend ce recordset
    select '' as [part_name]
	    , '' as supp_name
	    , sum(quantity) as nb_subs
	    , sum(sub_total) as amount
	    , sum(sub_total) * (Isnull(ppc.profit_percentage, 40.0)/100.0) as profit
	    , min(m.create_date) as create_date
    from member_hierarchy mh
	    inner join member m on m.member_id = mh.member_id
        left outer join partner_payment_config ppc on m.partner_id=ppc.partner_id
	    inner join event_participation ep on ep.member_hierarchy_id=mh.member_hierarchy_id
	    inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
	                                  and cc.member_type_id = 3
	    LEFT OUTER join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	where 1 = 2 
    group by ppc.profit_percentage

END
GO
