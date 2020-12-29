USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_member_report]    Script Date: 04/17/2015 16:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Description: procedure qui affiche un rapport sommaire pour les campagnes

Ex: EXEC [dbo].[es_rpt_group_member_report] 11111011

mod fblais 	    2005-12-01 	- pour gérer les unsubscribe

mod mcote 	    2006-02-18 	
                - creation de table temporaire (optimisation)
				- obtention du 100% profit 1rst sub
				- modif relation parent enfant pour reporting
				  la modif consiste a donner le credit des ventes 
				  aux participants meme si ces dernier sont enfant 
				  du sponsor.
				- enlever le select imbriqué.

mod pgirard     2006-12-15
                Changé create date 

mod pgirard     2007-01-16
                Ajouté les creation channel 32 et 33

mod jhidaka     2009-09-03
                Fixed the profit percent hard-coded issue
                
mod jhidaka		2013-03-14
				member_name must com efrom users table

exec [dbo].[es_rpt_group_member_report] 1002418
*/

ALTER PROCEDURE [dbo].[es_rpt_group_member_report]
	@event_id int
AS

BEGIN
    -- participant orders 
    select 
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
		    end)  as member_name
	    , count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		    then m.member_id else NULL end ) as email_sent
        , COALESCE(sum(case 
		  when tps.product_type_id  = 18 and store_id = 1 then 0
		  else quantity end),0) as nb_subs
	    , COALESCE(sum(case 
			when tps.product_type_id  = 18  and store_id = 1 then 0
			else sub_total end),0) as amount
        , COALESCE(sum(case 
			when tps.product_type_id  = 18  and store_id = 1 then
				sub_total
			else 0 end),0) as donation_amount
	    , COALESCE(sum(case 
			-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
			when tps.product_type_id  = 18  and store_id = 1 then
				(case when tps.create_date < '2011-04-01' then 
					sub_total  * 93.5/100.0
				else
					sub_total * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
		    	when tps.item_type_id in (6,24) and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
				sub_total * 25.0/100.0
			else
			-- For all other product percent profit use event profit calculated field (January 6, 2011)
				sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end),0) as profit
    from event_participation ep
		inner join [event] e on e.event_id = ep.event_id
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
        -- get the partner profit percent
        --left outer join partner_payment_config ppc on g.partner_id=ppc.partner_id
	    -- order
            left outer join [es_get_valid_orders_items_by_event_id] (@event_id) tps
	    on tps.supp_id = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    left join users u with (nolock) on m.user_id = u.user_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    left join users up with (nolock) on mp.user_id = up.user_id
	    left join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
        -- get donation profit from efrcommon.dbo.profit
		left outer join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
    where ep.event_id = @event_id
	    and 	mh.active = 1
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
    order by 1, 2

END


