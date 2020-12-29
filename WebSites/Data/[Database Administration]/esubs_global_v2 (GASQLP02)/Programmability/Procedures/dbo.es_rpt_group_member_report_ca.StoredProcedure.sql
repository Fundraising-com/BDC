USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_member_report_ca]    Script Date: 09/03/2014 12:58:46 ******/
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
				- enlever le select imbriquer.
mod pgirard     2006-12-15
                Changé ALTER  date 
mod pgirard     2007-01-16
                Ajouté les creation channel 32 et 33
     EXEC [dbo].[es_rpt_group_member_report_ca] 1499792
*/
ALTER PROCEDURE [dbo].[es_rpt_group_member_report_ca]
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
		from      
			event_participation ep (nolock)
			join [es_get_valid_orders_items_by_event_id] (@event_id) tps on tps.supp_id = ep.event_participation_id
        where ep.event_id = @event_id
	
    create index ix_suppid on #tps (suppid)

    -- participant orders 
    select 
	    (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then (m.first_name + ' ' + m.last_name) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then (m.first_name + ' ' + m.last_name)
		    else (mp.first_name + ' ' + mp.last_name)
		    end)  as member_name
	    , count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		    then m.member_id else NULL end ) as email_sent
	   , sum(quantity) as nb_subs
            ,sum(quantity * price) as amount
	   , sum(quantity * amountNoTax) as amount_gross
	   , sum(quantity * amountNoTax * (Isnull(e.profit_calculated, 37.0)/100.0)) as profit
	    
    from event_participation ep (nolock)
		join [event] e (nolock) on e.event_id = ep.event_id
	    join event_group eg (nolock) on eg.event_id = ep.event_id 
	    join [group] g (nolock) on g.group_id = eg.group_id 
		-- order
            left join #tps tps
	    on tps.suppid = ep.event_participation_id
	    -- enfant
	    join member_hierarchy mh (nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    join member m (nolock) on m.member_id = mh.member_id
	    -- parent
	    left join member_hierarchy mhp (nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left join member mp (nolock) on mp.member_id = mhp.member_id
	    left join creation_channel cc (nolock) on cc.creation_channel_id = mh.creation_channel_id
    where ep.event_id = @event_id
	group by (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then (m.first_name + ' ' + m.last_name) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then (m.first_name + ' ' + m.last_name)
		    else (mp.first_name + ' ' + mp.last_name)
		    end)  
    order by 1, 2

END



