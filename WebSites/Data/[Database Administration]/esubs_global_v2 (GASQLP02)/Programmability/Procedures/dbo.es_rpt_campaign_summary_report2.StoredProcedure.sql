USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_campaign_summary_report2]    Script Date: 02/14/2014 13:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

Description: procedure qui affiche un rapport sommaire pour les campagnes

Ex: EXEC [dbo].[es_rpt_campaign_summary_report] 1110101

mod mcote 	    2006-02-18
                - creation de table temporaire (optimisation)
				- obtention du 100% profit 1rst sub
				- enlever le select imbriquer.

mod pgirard     2006-12-13
                Utilise update_date a la place de create_date

mod pgirard     2006-01-16
                added channel 32 and 33

*/

CREATE PROCEDURE [dbo].[es_rpt_campaign_summary_report2] 
	@event_id int
WITH RECOMPILE
AS
BEGIN

	declare @event_id1 int

	SET @event_id1 = @event_id

	SET NOCOUNT ON 

	-- pre-generate the tps
    CREATE TABLE #tps (
	    rownum int identity(1,1)
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
        , updatedate datetime
    )

	INSERT INTO #tps (
		orderid
	    , quantity
	    , price
	    , suppID
	    , updatedate
	)
	select orderid
	     , quantity
	     , price
	     , suppID
	     , updatedate
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
         , o.create_date as updatedate
	from event_participation ep
		inner join qspecommerce.dbo.efundraisingtransaction et on et.suppid = ep.event_participation_id
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
	where ep.event_id = @event_id1
   	  and o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701 )
	/*
	union all
	select oh.ID as orderid
	     , sod.quantity
	     , cid.value as price
	     , et.suppID
	     , oh.LastActivityDate as updatedate
 	from event_participation ep 
		inner join qspecommerce.dbo.efundraisingtransaction et on et.suppid = ep.event_participation_id
		inner join qspstore.dbo.orderheader oh on oh.id = et.orderid
		inner join qspstore.dbo.suborderheader soh on oh.id = soh.orderheaderid 
		inner join qspstore.dbo.suborderdetail sod on soh.id = sod.suborderheaderid
		inner join qspstore.dbo.catalogitemdetail cid on cid.id = sod.catalogitemdetailid
	WHERE oh.OrderTotal IS NOT NULL
	  AND oh.OrderStatusID NOT IN (1, 10, 11)
	  AND soh.ShipToAddressID <> 0
	  and oh.aggregatorid in (7,13)
	  and ep.event_id = @event_id1
	*/
	) t
    
    create index ix_suppid on #tps (suppid)
    
    /*
    < 1 mai 2004        tout est 40%
    <16 sept 2004        le 100% n'a pas de maximum
    <16 sept 2005        le 100% a un maximum de 25$
    >16 sept 2005        le 100%  est sur le premier 25$
    >16 mai 2006	fin de 100% profit sur first sub
    */

    select ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35, 38) then m.member_id else NULL end ) as nb_group_members
	     , count(distinct case when mh.creation_channel_id in(7,20,23,32,35, 38) then mh.member_hierarchy_id else null end) as nb_part
	     , count(distinct case when mh.creation_channel_id in(12,14,29,33, 37) then mh.parent_member_hierarchy_id else null end) as nb_active
	     , count(distinct case when mh.creation_channel_id in(12,14,29,33, 37) then mh.member_id else null end) as nb_supporters
	     , sum(quantity) as nb_subs
	     , sum(quantity * price) as amount_sold
	     , sum(case 
		    -- Fin de 100% Profit sur first subs
		    when tps.rownum = 1 and updatedate > '2006-05-16' then 
			    quantity * price * .4 
		    -- 100% premier 25$ 40% reste de l'order
		    when tps.rownum = 1 and updatedate > '2005-10-16' then 
			    (case when quantity * price > 25  then (((quantity * price) - 25) * 0.4) + 25
			    else quantity * price end)
		    -- 100% maximum 25$
		    when tps.rownum = 1 and updatedate < '2005-10-16' then 
			    (case when quantity * price > 25  then 25
			    else quantity * price end)
		    when tps.rownum = 1 and updatedate < '2004-10-16' then
			    quantity * price
		    else quantity * price*(Isnull(ppc.profit_percentage, 40.0)/100.0) end) as profit
	     , max(tps.updatedate) as last_activity
    from event_participation ep
		-- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
		
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
		-- profit
		inner join partner prt  on prt.partner_id = g.partner_id
		left join partner_payment_config ppc  on ppc.partner_id = prt.partner_id
	    -- order
        left outer join #tps tps on tps.suppid = ep.event_participation_id
	    
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
    where ep.event_id = @event_id1
      and mh.active = 1
    group by ep.event_id
    order by 1, 2

END
GO
