USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_supporters_invited_ca]    Script Date: 09/03/2014 14:22:10 ******/
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
	EXEC [dbo].[es_rpt_supporters_invited_ca] 134548070
*/
ALTER PROCEDURE [dbo].[es_rpt_supporters_invited_ca]
	@event_participation_id int
AS
BEGIN
-- pre-generate the tps
create table #tps (
	rownum int identity(1,1)
	, orderid int
	, quantity int
	, price money
    , amountNoTax money
	, suppID int
    , createdate datetime
)

create table #supp (
    event_participation_id int
)
    INSERT INTO #supp (
        event_participation_id
    )
    select @event_participation_id
    union all
    select ep.event_participation_id
    from event_participation ep (nolock)
		join member_hierarchy mh (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
		join member_hierarchy mhp (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		join event_participation epp (nolock) on epp.member_hierarchy_id = mhp.member_hierarchy_id
    where epp.event_participation_id = @event_participation_id

    create index ix_event_participation_id on #supp (event_participation_id)

	INSERT INTO #tps (
		orderid
	     , quantity
	     , price
         , amountNoTax 
	     , suppID
	     , createdate
	)
	select tps.order_id as orderid
		, 1 as quantity
		, tps.price
		, tps.price - tps.tax - tps.freight - tps.handling_fee as amountNoTax
		, tps.supp_id as suppID
		, tps.create_date as createdate
		from event_participation ep (nolock)
			join event_participation e (nolock) on e.event_id = ep.event_id
			join [es_get_valid_orders_items] () tps on tps.supp_id = ep.event_participation_id
		where e.event_participation_id = @event_participation_id

    create index ix_suppid on #tps (suppid)

-- supporters orders 
select 
	m.first_name
	, m.last_name
	, ep.create_date
	, sum(quantity) as nb_subs
	, sum(quantity * price) as amount
    , sum(quantity * amountNoTax) as amount_gross
	, sum(quantity * amountNoTax * (Isnull(e.profit_calculated, 37.0)/100.0)) as profit
    , (case when ep.event_participation_id = @event_participation_id then 0 else 1 end) as is_supp
from event_participation ep (nolock)
	join [event] e (nolock) on e.event_id = ep.event_id 
    -- filter out the current part
    join #supp supp on supp.event_participation_id = ep.event_participation_id
	-- order
    left join #tps tps on tps.suppid = ep.event_participation_id
    -- current part
	join member_hierarchy mh (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id 
	join member m (nolock) on m.member_id = mh.member_id
--where mh.active = 1
group by 
	 m.first_name
	, m.last_name
	, ep.create_date
    , (case when ltrim(m.first_name + m.last_name) <> '' then m.email_address else NULL end)
	, (case when ep.event_participation_id = @event_participation_id then 0 else 1 end) 
order by 1, 2

RETURN 0
END



