USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_stats_ca_dec3_2008]    Script Date: 02/14/2014 13:06:51 ******/
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
CREATE  PROCEDURE [dbo].[es_rpt_group_stats_ca_dec3_2008]
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
	select orderid
	     , quantity
	     , price
             , amountNoTax as money
	     , suppID
	     , updatedate
	from
	(
		select o.order_id as orderid
	     , 1 as quantity
	     , cod.price
             , cod.price - cod.tax - cod.tax2 as amountNoTax
             , cod.tax
             , cod.tax2
	     , et.suppID
             , o.create_date as updatedate
             ,hist.status
	from      
               qspCanadaOrderManagement.dbo.CustomerOrderDetail cod INNER JOIN
               qspCanadaOrderManagement.uspvl3k117.CustomerOrderDetailRemitHistory hist on hist.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and hist.transid = cod.transid INNER JOIN
               qspCanadaOrderManagement.uspvl3k117.InternetOrderID ioid ON cod.CustomerOrderHeaderInstance = ioid.CustomerOrderHeaderInstance
               inner join qspEcommerce.dbo.cart c on c.eds_order_id = ioid.internetorderid
               inner join qspFulfillment.dbo.[Order] o on o.order_id = c.x_order_id
               inner join qspecommerce.dbo.efundraisingtransaction et on et.orderid = o.order_id
               inner join  event_participation ep on ep.event_participation_id = et.suppid
	
        where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701)
               and cod.delflag = 0 and hist.status in (42001,42000) --42000 = needs to be sent, and 42001 = sent
               and ep.event_id = @event_id
	
        )t

    create index ix_suppid on #tps (suppid)

    select ep.event_id
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then m.member_id else NULL end ) as nb_members
	     , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then mh.member_hierarchy_id else null end) as nb_part
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.parent_member_hierarchy_id else null end) as nb_active
	     , count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.member_id else null end) as nb_supp
	     , sum(quantity) as nb_subs
	     , sum(quantity * price) as amount--_sold
             , sum(quantity * amountNoTax) as amount_gross
	     , sum(quantity * amountNoTax*0.37) as profit
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
    where ep.event_id = @event_id
	  and mh.active = 1
    group by ep.event_id
    order by 1, 2

END
GO
