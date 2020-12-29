USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_stats_TEMP]    Script Date: 02/14/2014 13:06:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    Description: group stats section in all zone
    Ex: exec es_rpt_group_stats 10100111
    mod pgirard     2006-12-21
                    changé createdate pour update_date
    mod pgirard     2007-01-16
                    ajouté les creation channel 32 et 33
*/
CREATE PROCEDURE [dbo].[es_rpt_group_stats_TEMP]
	@event_id int
AS
BEGIN

	DECLARE @event_id1 int

	SET @event_id1 = @event_id
	

    select ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then m.member_id else NULL end ) as nb_members
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then mh.member_hierarchy_id else null end) as nb_part
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.parent_member_hierarchy_id else null end) as nb_active
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.member_id else null end) as nb_supp
	     , sum(case 
			when tps.product_type_id  = 18 and tps.store_id = 1 then 0
			else quantity end) as nb_subs
	     , sum(case 
			when tps.product_type_id  = 18 and tps.store_id = 1 then 0
			else sub_total end) as amount--_sold
	     , sum(case 
			when tps.product_type_id  = 18 and tps.store_id = 1 then
				sub_total
			else 0 end) as donation_amount
	     , ROUND(sum(case 
			-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
			when tps.product_type_id  = 18 and tps.store_id = 1 then
				(case when tps.create_date < '2011-04-01' then 
					sub_total * 93.5/100.0
				else
					sub_total  * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
		    	when tps.item_type_id = 6 and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
				sub_total * 25.0/100.0
			else
			-- For all other product percent profit use event profit calculated field (January 6, 2011)
				sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end),2) as profit
         , max(tps.create_date) as last_activity
    from event_participation ep
	    inner join event_group eg on eg.event_id = ep.event_id 
		inner join [event] e on e.event_id = eg.event_id
	    inner join [group] g on g.group_id = eg.group_id 
        -- profit
		--inner join partner prt
		--on prt.partner_id = g.partner_id
		--left join partner_payment_config ppc
		--on ppc.partner_id = prt.partner_id
	    -- order
        left outer join [es_get_valid_orders_items_by_event_id] (@event_id1) tps on tps.supp_id = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
		-- get donation profit from efrcommon.dbo.profit
		left outer join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
    where ep.event_id = @event_id1
	  and mh.active = 1
    group by ep.event_id
    order by 1, 2

END
GO
