USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_member_report_ca_dec4_3008]    Script Date: 02/14/2014 13:06:51 ******/
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

*/
CREATE   PROCEDURE [dbo].[es_rpt_group_member_report_ca_dec4_3008]
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
             , amountNoTax
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
             ,ep.event_id
	from      
               qspCanadaOrderManagement.dbo.CustomerOrderDetail cod INNER JOIN
               qspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory hist on hist.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and hist.transid = cod.transid INNER JOIN
               qspCanadaOrderManagement.dbo.InternetOrderID ioid ON cod.CustomerOrderHeaderInstance = ioid.CustomerOrderHeaderInstance
               inner join qspEcommerce.dbo.cart c on c.eds_order_id = ioid.internetorderid
               inner join qspFulfillment.dbo.[Order] o on o.order_id = c.x_order_id
               inner join qspecommerce.dbo.efundraisingtransaction et on et.orderid = o.order_id
               inner join  event_participation ep on ep.event_participation_id = et.suppid
	
        where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701)
               and cod.delflag = 0 and hist.status in (42001,42000) --42000 = needs to be sent, and 42001 = sent
               and ep.event_id = @event_id
	
        )t

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
	   , sum(quantity * amountNoTax) as amount_gross
	   , sum(quantity * amountNoTax*0.37) as profit
	    
    from event_participation ep
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
	    -- order
            left outer join #tps tps
	    on tps.suppid = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    left join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
    where ep.event_id = @event_id
	   -- and 	mh.active = 1
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
GO
