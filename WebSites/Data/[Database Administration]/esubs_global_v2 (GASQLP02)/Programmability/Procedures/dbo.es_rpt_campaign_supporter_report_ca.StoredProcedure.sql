USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_campaign_supporter_report_ca]    Script Date: 09/03/2014 14:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Description: supporter report dans le campaign manager

Ex: EXEC [dbo].[es_rpt_campaign_supporter_report] 11101011

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
   EXEC [dbo].[es_rpt_campaign_supporter_report_ca] 1479876
*/
ALTER PROCEDURE [dbo].[es_rpt_campaign_supporter_report_ca]
    @event_id int
AS
BEGIN
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
         , tps.price - tps.tax - tps.freight - tps.other_fees as amountNoTax
	     , tps.supp_id as suppID
         , tps.create_date as updatedate
	from  event_participation ep (nolock)
	      join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
    where ep.event_id = @event_id

    create index ix_suppid on #tps (suppid)

    -- supporters orders 
    select (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then (m.first_name + ' ' + m.last_name) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then (m.first_name + ' ' + m.last_name)
		    else (mp.first_name + ' ' + mp.last_name)
		    end)  as part_name
	    , m.first_name + ' ' + m.last_name as supp_name
	    , sum(quantity) as nb_subs
	    , sum(quantity * price) as amount
	    , sum(quantity * amountNoTax) as amount_gross
	    , sum(quantity * amountNoTax*(Isnull(ppc.profit_percentage, 37.0)/100.0)) as profit
	    , min(updatedate) as updatedate
    from event_participation ep (nolock)
	    join event_group eg (nolock) on eg.event_id = ep.event_id 
	    join [group] g (nolock) on g.group_id = eg.group_id 
		-- profit
		join partner prt (nolock) on prt.partner_id = g.partner_id
		left join partner_payment_config ppc (nolock) on ppc.partner_id = prt.partner_id
	    -- order
        left join #tps tps on tps.suppid = ep.event_participation_id
	    -- enfant
	    join member_hierarchy mh (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id 
	    join member m (nolock) on m.member_id = mh.member_id
	    -- parent
	    left join member_hierarchy mhp (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left join member mp (nolock) on mp.member_id = mhp.member_id
	    join creation_channel cc (nolock) on cc.creation_channel_id = mh.creation_channel_id
    where ep.event_id = @event_id
	  --and mh.active = 1
    group by 
	      (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then (m.first_name + ' ' + m.last_name) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then (m.first_name + ' ' + m.last_name)
		    else (mp.first_name + ' ' + mp.last_name)
		    end)
	    , mp.first_name 
	    , mp.last_name
	    , m.first_name
	    , m.last_name
	    , (case when ltrim(m.first_name + m.last_name) <> '' then m.email_address else NULL end)
   order by 1, 2

   select '' as [part_name]
	    , '' as supp_name
	    , sum(quantity) as nb_subs
	    , sum(price) as amount
	    , sum(price) as amount_gross
        , sum(price) * (Isnull(ppc.profit_percentage, 37.0)/100.0) as profit
        , min(m.create_date) as create_date
    from member_hierarchy mh (nolock)
	    join member m (nolock) on m.member_id = mh.member_id
        left join partner_payment_config ppc (nolock) on m.partner_id=ppc.partner_id
	    join event_participation ep (nolock) on ep.member_hierarchy_id=mh.member_hierarchy_id
	    join creation_channel cc (nolock) on cc.creation_channel_id = mh.creation_channel_id
	                                  and cc.member_type_id = 3
        left join #tps tps ON tps.suppid = ep.event_participation_id
	where 1 = 2
    group by ppc.profit_percentage

END

