USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_participant_stats]    Script Date: 03/04/2015 23:32:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	procedure qui affiche un rapport sommaire pour les campagnes

	Le premier subselect va chercher les parents(sponsor et participants)
	Le deuxième subselect va chercher les supporters et les associes aux parents
	
	exec [dbo].[cc_get_participant_stats] 1455083
*/

ALTER procedure [dbo].[cc_get_participant_stats]
	@event_id int
as
/*
declare @event_id int
set @event_id = 692221
*/
declare @new_event_id int
set @new_event_id = @event_id

select           
	 CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
		  ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
     END as participant_name
     ,CASE WHEN u.password COLLATE database_default IS NOT NULL THEN u.password COLLATE database_default
		  ELSE m.password COLLATE database_default
     END as password
	,isnull(sum(b.nb_supp),0) as email_sent
	,isnull(sum(a.nb_subs),0) + isnull(sum(b.nb_subs),0) as nb_subs
	,isnull(sum(a.amount_sold),0) + isnull(sum(b.amount_sold),0) as amount_sold
	,isnull(sum(a.profit),0) + isnull(sum(b.profit),0) as profit
    ,ep.event_participation_id as participant_id
    ,COALESCE(u.email_address COLLATE database_default,m.email_address COLLATE database_default) as email_address
    ,ISNULL( sum(b.bounced_count), 0 ) AS bounced_count
    ,ep.create_date
    ,a.movie_ticket 
	,mh.active
	,mh.unsubscribe as unsubscribed
	,m.deleted
	,mh.member_hierarchy_id
from (
	    select 	
	         ep.member_hierarchy_id
			,COALESCE(sum(case 
				when tps.product_type_id  = 18 and tps.store_id = 1 then 0
				else tps.quantity end),0) as nb_subs
			,COALESCE(sum(case 
				when tps.product_type_id  = 18 and tps.store_id = 1 then 0
				else tps.sub_total end),0) as amount_sold
			,COALESCE(sum(case 
				-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
				when tps.product_type_id  = 18 and tps.store_id = 1 then
					(case when tps.create_date < '2011-04-01' then 
						sub_total * 93.5/100.0
					else
						sub_total * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
				when tps.item_type_id = 6 and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
					sub_total * 25.0/100.0
				else
				-- For all other product percent profit use event profit calculated field (January 6, 2011)
					sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end),0) as profit
	        ,max(case when epr.prize_item_id > 0 then 'Yes' else 'No' end) as movie_ticket
			,dbo.es_get_user_type(ep.member_hierarchy_id) as user_type
		from
			event_participation ep with (nolock)
			join [event] e with (nolock) on e.event_id = ep.event_id
			join member_hierarchy mh with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
 			left join(
				select event_participation_id, epr.prize_item_id
				from earned_prize epr with (nolock)
		        		join prize_item pt with (nolock)
				on epr.prize_item_id = pt.prize_item_id
				where pt.prize_id in (2,5)
			) epr on ep.event_participation_id = epr.event_participation_id 
			left join [es_get_valid_orders_items_by_event_id] (@new_event_id) tps on tps.supp_id = ep.event_participation_id
			-- get donation profit from efrcommon.dbo.profit
			left join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11 
		where ep.event_id = @new_event_id
		group by ep.member_hierarchy_id	
		having dbo.es_get_user_type(ep.member_hierarchy_id) in (1,2)
	) a
	left join (
		select 
			parent_member_hierarchy_id
			,sum(nb_supp) nb_supp
			,sum(nb_subs) as nb_subs
			,sum(amount_sold) as amount_sold
			,sum(profit) as profit
            ,sum(bounced_count) as bounced_count
		from(
			select 	
				mh.parent_member_hierarchy_id
				,count(mh.member_hierarchy_id) nb_supp
				,COALESCE(sum(case 
					when tps.product_type_id  = 18 and tps.store_id = 1 then 0
					else tps.quantity end),0) as nb_subs
				,COALESCE(sum(case 
					when tps.product_type_id  = 18 and tps.store_id = 1 then 0
					else tps.sub_total end),0) as amount_sold
				,COALESCE(sum(case 
					-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
					when tps.product_type_id  = 18 and tps.store_id = 1 then
						(case when tps.create_date < '2011-04-01' then 
							sub_total * 93.5/100.0
						else
							sub_total * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
					when tps.item_type_id = 6 and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
						sub_total * 25.0/100.0
					else
					-- For all other product percent profit use event profit calculated field (January 6, 2011)
						sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end),0) as profit
	            ,sum( CASE m.bounced WHEN 1 THEN 1 ELSE 0 END ) AS bounced_count
				,dbo.es_get_user_type(ep.member_hierarchy_id) as user_type	                        
			from
				event_participation ep with (nolock)
				join [event] e with (nolock) on e.event_id = ep.event_id
				join member_hierarchy mh with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	            join member m with (nolock) on m.member_id = mh.member_id
	            left join [es_get_valid_orders_items_by_event_id] (@new_event_id) tps on tps.supp_id = ep.event_participation_id
				-- get donation profit from efrcommon.dbo.profit
				left join efrcommon.dbo.profit donation_profit with (nolock) 
				on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11 
			where ep.event_id = @new_event_id
			group by mh.parent_member_hierarchy_id,ep.member_hierarchy_id
			having dbo.es_get_user_type(ep.member_hierarchy_id) in (3)
		) d
		group by parent_member_hierarchy_id
	) b
	on a.member_hierarchy_id = b.parent_member_hierarchy_id
	join member_hierarchy mh with (nolock) on mh.member_hierarchy_id = a.member_hierarchy_id
	join member m with (nolock) on m.member_id = mh.member_id
	left join users u with (nolock) on m.user_id = u.user_id
    join event_participation ep with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
group by 
	 CASE WHEN (u.first_name + ' ' + u.last_name) IS NOT NULL THEN dbo.TitleCase(lower(u.first_name + ' ' + u.last_name))
		  ELSE dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
     END
    ,CASE WHEN u.password COLLATE database_default IS NOT NULL THEN u.password COLLATE database_default
		  ELSE m.password COLLATE database_default
     END
    ,ep.event_participation_id
    ,COALESCE(u.email_address COLLATE database_default,m.email_address COLLATE database_default)
    ,b.bounced_count
    ,m.opt_status_id
    ,ep.create_date
    ,a.movie_ticket
    ,m.member_id
	,mh.active
	,mh.unsubscribe
	,m.deleted
	,mh.member_hierarchy_id
