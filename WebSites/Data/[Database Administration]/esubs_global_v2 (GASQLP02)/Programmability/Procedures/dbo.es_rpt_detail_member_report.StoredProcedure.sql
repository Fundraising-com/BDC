USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_detail_member_report]    Script Date: 02/14/2014 13:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
created by melissa
select * from partner where partner_id = 143
exec [es_rpt_detail_member_report] '2010-07-01', '2010-12-31', 143, 'en-US'
exec [es_rpt_detail_member_report] '2009-01-01', '2009-11-13', 143, 'en-US'
exec [es_rpt_detail_member_report] '2010-07-01', '2010-11-13', 143, 'en-US'
exec [es_rpt_detail_member_report] '2009-07-01', '2009-11-13', 143, 'en-US'

*/

CREATE PROCEDURE [dbo].[es_rpt_detail_member_report]
	@start_date datetime 
	, @end_date datetime
	, @partner_id int
	, @culture_code varchar(5) 
AS

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
	select o.order_id as orderid
	     , od.quantity
	     , od.price
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
    --where ep.event_id = @event_id
	where o.order_date between @start_date and dateadd(mm, 1, @end_date)
	order by updatedate
 		   , od.price

    create index ix_suppid on #tps (suppid)

--select * from creation_channel where member_type_id = 2



    -- participant orders 
    select 
		 (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then 'sponsor' --dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then 'participant' -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else 'participant' -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end)  as member_type
	    ,(case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end)  as members
		, (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then m.email_address -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.email_address -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.email_address -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) as email
		, e.event_name
	    , count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		    then m.member_id else NULL end ) as email_sent
	    , COALESCE(sum(quantity),0) as nb_subs
	    , COALESCE(sum(quantity * price),0) as amount
	     , mh.creation_channel_id
    from event_participation ep
		inner join [event] e on e.event_id = ep.event_id
	    inner join event_group eg on eg.event_id = ep.event_id 
	    inner join [group] g on g.group_id = eg.group_id 
        -- get the partner profit percent
        --left outer join partner_payment_config ppc on g.partner_id=ppc.partner_id
	    -- order
            left outer join #tps tps
	    on tps.suppid = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		left outer join event_participation epp on mhp.member_hierarchy_id = epp.event_participation_id and ep.event_participation_id = epp.event_participation_id
	    left outer join member mp on mp.member_id = mhp.member_id
	    left join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
    where ep.create_date between @start_date and dateadd(mm, 1, @end_date)
		--and epp.create_date between @start_date and @end_date
	    and 	mh.active = 1
		and g.partner_id = @partner_id
		and e.culture_code = @culture_code
    group by (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then 'sponsor' --dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then 'participant' -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else 'participant' -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) -- as member_type
	     ,(case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) -- as members
		, (case 
		    -- sponsor order must be under his name
		    when (mp.first_name + ' ' + mp.last_name) is null 
			    then m.email_address -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.email_address -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.email_address -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) --as email
		, e.event_name
		, mh.creation_channel_id
   -- order by 1, 2


END
GO
