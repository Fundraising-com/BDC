USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_group_member_by_date]    Script Date: 02/14/2014 13:06:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Description: procedure qui affiche un rapport sommaire pour les campagnes
select * from event where event_name like 'click 4 hope'
EXEC [dbo].[es_rpt_group_member_by_date]  '2011-01-01', '2011-06-30 23:59:59', 1317516
EXEC [dbo].[es_rpt_group_member_by_date]  '2011-01-01', '2011-06-30 23:59:59', 1316757

EXEC [dbo].[es_rpt_group_member_by_date]  '2011-07-10', '2011-08-15 23:59:59', 1316757
EXEC [dbo].[es_rpt_group_member_by_date]  '2011-07-01', '2011-07-31 23:59:59', 1316757

grant exec on [es_rpt_group_member_by_date] to db_stored_proc_exec
             
*/

CREATE PROCEDURE [dbo].[es_rpt_group_member_by_date]
		@start_date as datetime 
		, @end_date as datetime
		, @event_id int
as
BEGIN


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)


     
    -- pre-generate the tps
    create table #tps (
	    rownum int identity(1,1)
	    , orderid int
	    , quantity int
	    , price money
	    , suppID int
        , product_type int 
        , charge numeric(18,2)
        , updatedate datetime
	--	, catalog_type int
    )

	INSERT INTO #tps (
        orderid
	    , quantity
	    , price
	    , suppID
        , product_type
        , charge
	    , updatedate
       -- , catalog_type
	)
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
         , pt.product_type_id as product_type
         , COALESCE(pt.fulfillment_charge, 0) as charge
         , o.order_date as updatedate
        -- , xc.x_catalog_type_id as catalog_type
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
        inner join qspfulfillment..catalog_item_detail cid on cid.catalog_item_detail_id = od.catalog_item_detail_id
		inner join qspfulfillment..catalog_item ci on ci.catalog_item_id = cid.catalog_item_id
        inner join qspecommerce.dbo.x_catalog xc on ci.catalog_id = xc.x_catalog_id
		inner join qspfulfillment..product p on p.product_id = ci.product_id
		inner join qspfulfillment..product_type pt on p.product_type_id = pt.product_type_id
        inner join event_participation ep on ep.event_participation_id = et.suppid
		inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
    where ep.event_id = @event_id
	and o.order_date between @start_date and @end_date 
	order by updatedate
 		   , od.price

    create index ix_suppid on #tps (suppid)

    -- participant orders 
    select 
	    (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end)  as member_name
		,(case 
		    -- sponsor order must be under his name
		    when mp.email_address is null 
			    then dbo.TitleCase(lower(m.email_address)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then dbo.TitleCase(lower(m.email_address))
		    else dbo.TitleCase(lower(mp.email_address))
		    end)  as member_email
	    , count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		    then m.member_id else NULL end ) as email_sent
        , COALESCE(sum(case 
		  when tps.catalog_type = 11 then 0
		  else quantity end),0) as nb_subs
	    , COALESCE(sum(case 
			when tps.catalog_type = 11 then 0
			else quantity * price end),0) as amount
        , COALESCE(sum(case 
			when tps.catalog_type = 11 then
				quantity * price
			else 0 end),0) as donation_amount
    from event_participation ep
		inner join [event] e on e.event_id = ep.event_id
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
        -- get the partner profit percent
        --left outer join partner_payment_config ppc on g.partner_id=ppc.partner_id
	    -- order
            inner  join #tps tps
	    on tps.suppid = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    left join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
        -- get donation profit from efrcommon.dbo.profit
		left outer join efrcommon.dbo.profit donation_profit with (nolock) 
			on e.profit_group_id = donation_profit.profit_group_id and qsp_catalog_type_id = 11
    where ep.event_id = @event_id
	    and 	mh.active = 1
    group by (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) 
,(case 
		    -- sponsor order must be under his name
		    when mp.email_address is null 
			    then dbo.TitleCase(lower(m.email_address)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then dbo.TitleCase(lower(m.email_address))
		    else dbo.TitleCase(lower(mp.email_address))
		    end) 
    order by 1, 2

END
GO
