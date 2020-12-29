USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_sponsor_email_product_type]    Script Date: 04/17/2015 16:09:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Pavel Tarassov>
-- Create date: <17-07-2009>
-- Description:	<Provides info for the sponsor how many emails each member sent, what kind of product
-- was purchased, quantities, profit>
-- exec [es_rpt_sponsor_email_product_type] 1554624
-- select * from  [es_get_valid_orders_items_by_event_id] (1554624)
-- =============================================
ALTER PROCEDURE [dbo].[es_rpt_sponsor_email_product_type]
	@event_id INT
AS
BEGIN

	SET NOCOUNT ON;
	
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
		end) as member_name
		, dbo.TitleCase(lower(ISNULL(tps.first_name + ' ' + tps.last_name, m.first_name + ' ' + m.last_name))) as supporter_name 
		, count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
			then m.member_id else NULL end ) as email_sent
		,COALESCE(sum(case 
			when tps.product_type_id  in (18,999) and tps.store_id = 1 then 0
			else quantity end),0) as nb_subs
		,COALESCE(sum(case 
			when tps.product_type_id  in (18,999) and tps.store_id = 1 then 0
			else sub_total end),0) as amount
		,COALESCE(sum(case 
			when tps.product_type_id in (18,999) and tps.store_id = 1 then
				sub_total
			else 0 end),0) as donation_amount
		,COALESCE(sum(case 
			-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
			when tps.product_type_id  in (18,999) and tps.store_id = 1 then
				(case when tps.create_date < '2011-04-01' then 
					sub_total * 93.5/100.0
				else
					sub_total * ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
		    	when tps.item_type_id in (6,24) and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
				sub_total * 25.0/100.0
			else
			-- For all other product percent profit use event profit calculated field (January 6, 2011)
				sub_total * Isnull(e.profit_calculated, 40.0)/100.0 end),0) as profit
		,COALESCE(dpt.display_id, 0) as product_type
    		, dpt.description as product_desc
	from event_participation ep with (nolock)
		inner join event_group eg with (nolock) on eg.event_id = ep.event_id 
		inner join [event] e with (nolock) on e.event_id = eg.event_id
		inner join [group] g with (nolock) on g.group_id = eg.group_id 
		-- get the partner profit percent
		--left outer join partner_payment_config ppc
		--on g.partner_id=ppc.partner_id
		--order
		left outer join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
        	left join display_product_type dpt on dpt.external_product_type_id = tps.product_type_id and dpt.store_id = tps.store_id
		-- enfant
		inner join member_hierarchy mh with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
		inner join member m with (nolock) on m.member_id = mh.member_id
		left join users u with (nolock) on m.user_id = u.user_id
		-- parent
		left outer join member_hierarchy mhp with (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		left outer join member mp with (nolock) on mp.member_id = mhp.member_id
		left join users up with (nolock) on mp.user_id = up.user_id
		left join creation_channel cc with (nolock) on cc.creation_channel_id = mh.creation_channel_id
		-- get donation profit from efrcommon.dbo.profit
		left outer join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11 -- donation ptid =  18
	where ep.event_id = @event_id and mh.active = 1 
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
		, dbo.TitleCase(lower(ISNULL(tps.first_name + ' ' + tps.last_name, m.first_name + ' ' + m.last_name)))
		, dpt.display_id
	    	, dpt.description
		--having count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		-- then m.member_id else NULL end ) > 0
	order by 1, 2

END


