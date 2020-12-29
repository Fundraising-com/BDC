USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_campaign_check_report]    Script Date: 02/14/2014 13:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 

Description:    check report dans le campaign manager

Ex: EXEC es_rpt_campaign_check_report 11101112

mod mcote 	    2006-02-18 	
                - creation de table temporaire (optimisation)
				- obtention du 100% profit 1rst sub
				- modif relation parent enfant pour reporting
				  la modif consiste a donner le credit des ventes 
				  aux participants meme si ces dernier sont enfant 
				  du sponsor.
				- enlever le select imbriquer.

mod krystian	2006-03-03
                - modifie la selection pour les tables des headers

mod pgirard     2006-12-13
                - change le create_date pour le update_date

mod dnghiem 2007-10-01
                - Using end_date in payment period for payment period report.

mod dnghiem 2007-10-05
                - Recalculate profit percentage based on partner_payment_config.
*/

CREATE PROCEDURE [dbo].[es_rpt_campaign_check_report]
	@event_id int
AS
BEGIN

    -- pre-generate the tps
    CREATE TABLE #tps (
	    rownum int identity(1,1)
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
        , charge numeric(18,2)
        , updatedate datetime
    )

	INSERT INTO #tps (
		orderid
	    , quantity
	    , price
	    , suppID
        , charge
	    , updatedate
	)
	select orderid
	     , quantity
	     , price
	     , suppID
         , charge
	     , updatedate
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price as price
	     , et.suppID
         , COALESCE(pt.fulfillment_charge, 0) as charge
         , o.order_date as updatedate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
        inner join qspfulfillment..catalog_item_detail cid on cid.catalog_item_detail_id = od.catalog_item_detail_id
		inner join qspfulfillment..catalog_item ci on ci.catalog_item_id = cid.catalog_item_id
		inner join qspfulfillment..product p on p.product_id = ci.product_id
		inner join qspfulfillment..product_type pt on p.product_type_id = pt.product_type_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
        inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
	where --o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701)
  	  ep.event_id = @event_id
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
	  and ep.event_id = @event_id*/
	) t
	order by updatedate
           , price

    create index ix_suppid on #tps (suppid)

    select  'Member Name', 'Supporter Name', '+Subscriptions', '$Amount Purchased', '$Profit', 'Purchase Date'

    -- supporters orders 
    select 
	    (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then (m.first_name + ' ' + m.last_name) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then (m.first_name + ' ' + m.last_name)
		    else (mp.first_name + ' ' + mp.last_name)
		    end)  as part_name
	    , m.first_name + ' ' + m.last_name as supp_name
	    , quantity as nb_subs
	    , quantity * price as amount
	    ,(case 
		    -- Fin de 100% Profit sur first subs
		    when tps.rownum = 1 and updatedate > '2006-05-16' then 
			    quantity * (price - charge) * (Isnull(ppc.profit_percentage, 40.0)/100.0) 
		    -- 100% premier 25$ 40% reste de l'order
		    when tps.rownum = 1 and updatedate > '2005-10-16' then 
			    (case when quantity * price > 25  then (((quantity * price) - 25) * (Isnull(ppc.profit_percentage, 40.0)/100.0)) + 25
			    else quantity * price end)
		    -- 100% maximum 25$
		    when tps.rownum = 1 and updatedate < '2005-10-16' then 
			    (case when quantity * price > 25  then 25
			    else quantity * price end)
		    else quantity * (price - charge) * (Isnull(ppc.profit_percentage, 40.0)/100.0) end) as profit
	    , convert(varchar(10), (updatedate), 121) as updatedate
    from event_participation ep
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
	    -- profit
	   inner join partner prt
	      on prt.partner_id = g.partner_id
	   left join partner_payment_config ppc
	      on ppc.partner_id = prt.partner_id
	    -- order
        inner join #tps tps on tps.suppid = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id 
	    inner join member m on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
    where ep.event_id = @event_id
	  --and mh.active = 1
    order by updatedate


    select 'Check Number', 'Check Period', '$Check Amount'

    select 
p.cheque_number
--	    , (case when day(p.cheque_date) >= 15 then cast(month(p.cheque_date) as varchar(10)) + '/15/' + cast(year(p.cheque_date) as varchar(10))
--		    else  CAST((month(p.cheque_date) - 1) as varchar(10)) + '/15/' + cast(year(p.cheque_date) as varchar(10)) end) as check_date
, Convert(varchar(10),Isnull(pp.end_date, p.cheque_date), 101)  as check_date
	    , p.paid_amount
    from payment_info pin
        inner join payment p on pin.payment_info_id = p.payment_info_id
	left join
	(
		select pps.payment_id, pps.payment_status_id
		from payment_payment_status pps
	       inner join
	       (
		select payment_id, max(create_date) as create_date
		from payment_payment_status
		group by payment_id
	       ) ppsNew on ppsNew.payment_id = pps.payment_id and ppsNew.create_date = pps.create_date 
	) pcancel
	on pcancel.payment_id = p.payment_id
        left join payment_period pp
             on pp.payment_period_id = p.payment_period_id
    where (event_id = @event_id and isnull(pcancel.payment_status_id, 2) <> 9)
    order by Isnull(pp.end_date, p.cheque_date)
END
GO
