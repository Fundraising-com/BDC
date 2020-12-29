USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_stats_ca]    Script Date: 09/03/2014 13:57:31 ******/
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
                    
    mod dpettit     2012-08-07 added 46001 to product type lookup and removed remithist table. Also updated 460012 to 46012 as was typo
	EXEC [dbo].[es_rpt_group_stats_ca] 1479876
*/
ALTER   PROCEDURE [dbo].[es_rpt_group_stats_ca]
	@event_id int
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
        , updatedate datetime
    )

	INSERT INTO #tps (
          orderid
	    , quantity
	    , price
        , amountNoTax
	    , suppID
	    , updatedate
	)
	select tps.order_id as orderid
		, 1 as quantity
		, tps.price
		, tps.price - tps.tax - tps.freight - tps.other_fees as amountNoTax
		, tps.supp_id as suppID
		, tps.create_date as updatedate
	from  event_participation ep (nolock)
	join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
	where ep.event_id = @event_id

    create index ix_suppid on #tps (suppid)

    select ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then m.member_id else NULL end ) as nb_members
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then mh.member_hierarchy_id else null end) as nb_part
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.parent_member_hierarchy_id else null end) as nb_active
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.member_id else null end) as nb_supp
	     , sum(quantity) as nb_subs
	     , sum(quantity * price) as amount--_sold
         , sum(quantity * amountNoTax) as amount_gross
	     , sum(quantity * amountNoTax * (Isnull(e.profit_calculated, 37.0)/100.0)) as profit
         , max(tps.updatedate) as last_activity
    from event_participation ep (nolock)
	    join event_group eg (nolock) on eg.event_id = ep.event_id 
		join [event] e (nolock) on e.event_id = eg.event_id
		join [group] g (nolock) on g.group_id = eg.group_id 
		left join #tps tps on tps.suppid = ep.event_participation_id
	    -- enfant
	    join member_hierarchy mh (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    join member m (nolock) on m.member_id = mh.member_id
	    -- parent
	    left join member_hierarchy mhp (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left join member mp (nolock) on mp.member_id = mhp.member_id
    where ep.event_id = @event_id
	  --and mh.active = 1
    group by ep.event_id
    order by 1, 2

END


