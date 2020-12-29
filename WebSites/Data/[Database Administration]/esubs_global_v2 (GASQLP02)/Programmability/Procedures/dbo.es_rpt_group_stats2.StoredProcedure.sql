USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_stats2]    Script Date: 02/14/2014 13:06:52 ******/
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
CREATE PROCEDURE [dbo].[es_rpt_group_stats2]
	@event_id int
AS
BEGIN

	DECLARE @event_id1 int

	SET @event_id1 = @event_id

    -- pre-generate the tps
    create table #tps (
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
	from qspecommerce.dbo.efundraisingtransaction et
		inner join event_participation ep on ep.event_participation_id = et.suppid
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
	where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701)
  	  and ep.event_id = @event_id1
	/*union all
	select oh.ID as orderid
	     , sod.quantity
	     , cid.value as price
	     , et.suppID
	     , oh.LastActivityDate as updatedate
 	from qspecommerce.dbo.efundraisingtransaction et 
		inner join qspstore.dbo.orderheader oh on oh.id = et.orderid
		inner join qspstore.dbo.suborderheader soh on oh.id = soh.orderheaderid 
		inner join qspstore.dbo.suborderdetail sod on soh.id = sod.suborderheaderid
		inner join qspstore.dbo.catalogitemdetail cid on cid.id = sod.catalogitemdetailid
		inner join event_participation ep on ep.event_participation_id = et.suppid
	WHERE oh.OrderTotal IS NOT NULL
	  AND oh.OrderStatusID NOT IN (1, 10, 11)
	  AND soh.ShipToAddressID <> 0
	  and oh.aggregatorid in (7,13)
	  and ep.event_id = @event_id
	*/
	) t
	order by updatedate
 		   , price

    create index ix_suppid on #tps (suppid)

    select ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then m.member_id else NULL end ) as nb_members
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then mh.member_hierarchy_id else null end) as nb_part
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.parent_member_hierarchy_id else null end) as nb_active
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.member_id else null end) as nb_supp
	     , sum(quantity) as nb_subs
	     , sum(quantity * price) as amount--_sold
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
	        else quantity * price * .4 end) as profit
         , max(tps.updatedate) as last_activity
    from event_participation ep
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
	    -- order
        left outer join #tps tps on tps.suppid = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
    where ep.event_id = @event_id1
	  and mh.active = 1
    group by ep.event_id
    order by 1, 2

END
GO
