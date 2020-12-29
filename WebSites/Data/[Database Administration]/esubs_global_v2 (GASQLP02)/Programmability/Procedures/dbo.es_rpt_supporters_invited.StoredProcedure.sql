USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_supporters_invited]    Script Date: 04/17/2015 16:08:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 
supporter invited report dans le participant zone
mod mcote 	2006-02-18 	- creation de table temporaire (optimisation)
- obtention du 100% profit 1rst sub
- modif relation parent enfant pour reporting
  la modif consiste a donner le credit des ventes 
  aux participants meme si ces dernier sont enfant 
  du sponsor.
- enlever le select imbriquer.
*/
ALTER  PROCEDURE [dbo].[es_rpt_supporters_invited]
	@event_participation_id int
AS
BEGIN

create table #supp (
    event_participation_id int
)

    INSERT INTO #supp (
        event_participation_id
    )
    select @event_participation_id
    union all
    select ep.event_participation_id
    from event_participation ep
            inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
            inner join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
            inner join event_participation epp on epp.member_hierarchy_id = mhp.member_hierarchy_id
    where epp.event_participation_id = @event_participation_id

    create index ix_event_participation_id on #supp (event_participation_id)


declare @event_id int
select @event_id = event_id from event_participation with (nolock)
where event_participation_id = @event_participation_id

-- supporters orders 
select 
	m.first_name
	, m.last_name
	, ep.create_date
    , m.email_address
    , ISNULL(sum(case 
			when tps.product_type_id IN (18,999) and tps.store_id = 1 then 0
			else tps.quantity end),0) as nb_subs
	, ISNULL(sum(case 
			when tps.product_type_id IN (18,999) and tps.store_id = 1 then 0
			else tps.sub_total end),0) as amount
    , ISNULL(sum(case 
			when tps.product_type_id IN (18,999) and tps.store_id = 1 then
				tps.sub_total
			else 0 end),0) as donation_amount -- THIS IS THE UP-SELL DONATION AMOUNT
    , ISNULL(sum(case 
			-- For Donation use 93.5% profit (January 6, 2011 - April 1, 2011)
			when tps.product_type_id IN (18,999) and tps.store_id = 1 then
				(case when tps.create_date < '2011-04-01' then 
					tps.sub_total * 93.5/100.0
				else
					tps.sub_total* ISNULL(donation_profit.profit_percentage, 0.0)/100.0 end)
			when tps.item_type_id in (6,24) and tps.store_id = 10 then -- Personalize Products on GA store only are 25% profit
				tps.sub_total * 25.0/100.0
			else
			-- For all other product percent profit use event profit calculated field (January 6, 2011)
				tps.sub_total * Isnull(e.profit_calculated, 30.0)/100.0 end),0) as profit
	, (case when ep.event_participation_id = @event_participation_id then 0 else 1 end) as is_supp
	, ISNULL(sum(case 
			when tps.product_type_id IN (18,999) and tps.store_id = 1 then tps.sub_total
			else 0 end),0) as efrecom_donation_amount
from event_participation ep
	inner join [event] e on e.event_id = ep.event_id
    -- filter out the current part
    inner join #supp supp on supp.event_participation_id = ep.event_participation_id
	-- order
    left join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
    -- current part
	inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id 
	inner join member m on m.member_id = mh.member_id
	-- get donation profit from efrcommon.dbo.profit
	left outer join efrcommon.dbo.profit donation_profit with (nolock) 
		on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
where mh.active = 1
group by 
	 m.first_name
	, m.last_name
	, ep.create_date
    , m.email_address
    , (case when ltrim(m.first_name + m.last_name) <> '' then m.email_address else NULL end)
	, (case when ep.event_participation_id = @event_participation_id then 0 else 1 end) 
order by 1, 2

RETURN 0
END
