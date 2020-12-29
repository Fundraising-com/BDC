USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_supporters_invited]    Script Date: 03/05/2015 15:14:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	rapport des supporters dans le custcare
*/

-- exec cc_get_supporters_invited 783787

ALTER procedure [dbo].[cc_get_supporters_invited]
	 @event_id int,
     @event_participation_id int = null
as

declare @member_hierarchy_id int
select @member_hierarchy_id = member_hierarchy_id
from event_participation with (nolock)
where event_participation_id = @event_participation_id

if @member_hierarchy_id is null
	select
		 m.first_name + ' ' +  m.last_name as supporter_name
	    ,ep.event_participation_id as supporter_id
		,COALESCE(sum(case 
			when tps.product_type_id  = 18 and tps.store_id = 1 then 0
			else tps.quantity end),0) as nb_subs
		,COALESCE(sum(case 
			when tps.product_type_id  = 18 and tps.store_id = 1 then 0
			else tps.sub_total end),0) as amount
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
       	,m.bounced
       	,mh.unsubscribe as unsubscribed
       	,tps.create_date as orderdate
       	,ep.create_date
       	,m.email_address
       	,mh.member_hierarchy_id
       	,mh.parent_member_hierarchy_id
	from
		event_participation ep with (nolock)
		join [event] e with (nolock) on e.event_id = ep.event_id
		join member_hierarchy mh with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
		join member m with (nolock) on m.member_id = mh.member_id
		left join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
		-- get donation profit from efrcommon.dbo.profit
		left join efrcommon.dbo.profit donation_profit with (nolock) 
		on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11 
	where ep.event_id = @event_id
	group by
		 m.first_name
		,m.last_name
		,ep.event_participation_id 
		,m.bounced
       	,mh.unsubscribe
       	,tps.create_date
       	,ep.create_date
       	,m.email_address
       	,mh.creation_channel_id
       	,mh.member_hierarchy_id
       	,mh.parent_member_hierarchy_id
	having mh.creation_channel_id in (12,14,29) or dbo.es_get_user_type(mh.member_hierarchy_id)=3
else
	select
		 m.first_name + ' ' +  m.last_name as supporter_name
	    ,ep.event_participation_id as supporter_id
		,COALESCE(sum(case 
			when tps.product_type_id  = 18 and tps.store_id = 1 then 0
			else tps.quantity end),0) as nb_subs
		,COALESCE(sum(case 
			when tps.product_type_id  = 18 and tps.store_id = 1 then 0
			else tps.sub_total end),0) as amount
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
       	,m.bounced
       	,mh.unsubscribe as unsubscribed
       	,tps.create_date as orderdate
       	,ep.create_date
       	,m.email_address
       	,mh.member_hierarchy_id
       	,mh.parent_member_hierarchy_id
	from
		event_participation ep with (nolock)
		join [event] e with (nolock) on e.event_id = ep.event_id
		join member_hierarchy mh with (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
		join member m with (nolock) on m.member_id = mh.member_id
		left join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
		-- get donation profit from efrcommon.dbo.profit
		left join efrcommon.dbo.profit donation_profit with (nolock) 
		on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11 
	where ep.event_id = @event_id and mh.parent_member_hierarchy_id = @member_hierarchy_id
	group by
		 m.first_name
		,m.last_name
		,ep.event_participation_id 
		,m.bounced
       	,mh.unsubscribe
       	,tps.create_date
       	,ep.create_date
       	,m.email_address
       	,mh.creation_channel_id
       	,mh.member_hierarchy_id
       	,mh.parent_member_hierarchy_id
	having mh.creation_channel_id in (12,14,29) or dbo.es_get_user_type(mh.member_hierarchy_id)=3

