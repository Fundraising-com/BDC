USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_summary_report_ca]    Script Date: 09/03/2014 14:22:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	EXEC [dbo].[es_rpt_partner_summary_report_ca] 741
*/
ALTER PROCEDURE [dbo].[es_rpt_partner_summary_report_ca]
	@partner_id int
WITH RECOMPILE
AS
BEGIN
	declare @partner_id1 int

	SET @partner_id1 = @partner_id

	SET NOCOUNT ON 

	-- pre-generate the tps
    CREATE TABLE #tps (
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
		, tps.price - tps.tax - tps.freight - tps.handling_fee as amountNoTax
		, tps.supp_id as suppID
		, tps.create_date as updatedate
	from event_participation ep (nolock)
	join [es_get_valid_orders_items] () tps on tps.supp_id = ep.event_participation_id
	join event_group eg (nolock) on ep.event_id = eg.event_id
	join [group] g (nolock) on eg.group_id = g.group_id
	where g.partner_id = @partner_id1
	
    create index ix_suppid on #tps (suppid)
    
    /*
    < 1 mai 2004        tout est 40%
    <16 sept 2004        le 100% n'a pas de maximum
    <16 sept 2005        le 100% a un maximum de 25$
    >16 sept 2005        le 100%  est sur le premier 25$
    >16 mai 2006	fin de 100% profit sur first sub
    */

select  
		SUM (nb_group_members) as nb_group_members, 
		SUM (nb_part) as nb_part,
		SUM (nb_active)  as nb_active,
		SUM (nb_supporters)  as nb_supporters,
		SUM (nb_subs) as nb_subs,
		SUM (amount_sold) as amount_sold,
		SUM (amount_gross) as amount_gross,
		SUM (profit) as profit,
		MAX (last_activity) as last_activity		
from (
    select TOP 100 percent
		ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35, 38) then m.member_id else NULL end ) as nb_group_members
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35, 38) then mh.member_hierarchy_id else null end) as nb_part
	     , count(distinct case when mh.creation_channel_id in(12,14,29,33, 37) then mh.parent_member_hierarchy_id else null end) as nb_active
	     , count(distinct case when mh.creation_channel_id in(12,14,29,33, 37) then mh.member_id else null end) as nb_supporters
	     , sum(quantity) as nb_subs
	     , sum(quantity * price) as amount_sold
         , sum(round(quantity * amountNoTax, 2,1)) as amount_gross
	     , sum(round(round(quantity * amountNoTax, 2,1)*(Isnull(e.profit_calculated, 37.0)/100.0), 2,1)) as profit
	     , max(tps.updatedate) as last_activity
    from event_participation ep (nolock)
		-- enfant
	    join member_hierarchy mh (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    join member m (nolock) on m.member_id = mh.member_id
		join [event] e (nolock) on e.event_id = ep.event_id
	    join event_group eg (nolock) on eg.event_id = ep.event_id 
	    join [group] g (nolock) on g.group_id = eg.group_id 
		-- profit
		join partner prt (nolock) on prt.partner_id = g.partner_id
		left join partner_payment_config ppc (nolock) on ppc.partner_id = prt.partner_id
	    -- order
        left join #tps tps on tps.suppid = ep.event_participation_id
	    -- parent
	    left join member_hierarchy mhp (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left join member mp (nolock) on mp.member_id = mhp.member_id
    where prt.partner_id = @partner_id1
      and mh.active = 1
    group by ep.event_id
    order by 1, 2 ) T

END


