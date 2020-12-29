USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_member_active_groups_report]    Script Date: 02/14/2014 13:06:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
created by melissa
exec [es_rpt_member_active_groups_report] '2010-01-01', '2010-12-13', 'en-US'
exec [es_rpt_member_active_groups_report] '2009-01-01', '2009-12-13', 'en-US'
exec [es_rpt_member_active_groups_report] '2010-07-01', '2010-12-13', 'en-US'
exec [es_rpt_member_active_groups_report] '2009-07-01', '2009-12-13', 'en-US'

*/

CREATE PROCEDURE [dbo].[es_rpt_member_active_groups_report]
	@start_date datetime 
	, @end_date datetime
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
		, event_id int
    )

	INSERT INTO #tps (
        orderid
	    , quantity
	    , price
	    , suppID
        , charge
	    , updatedate
		, event_id
	)
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
         , COALESCE(pt.fulfillment_charge, 0) as charge
         , o.order_date as updatedate
		 , ep.event_id
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

select 
member_type
, count(members) as part
, sum(case when creation_channel_id in (7,20,23,38,40) then 1 else 0 end) as partinvited
, sum(case when email_sent = 0 then 0 else 1 end) as partemail1
, sum(case when email_sent > 0 and email_sent < 12 and nb_subs > 0 then 1 else 0 end) as partemail1subs
, sum(case when email_sent < 12 then 0 else 1 end) as partemail12
, sum(case when email_sent >= 12 and nb_subs > 0 then 1 else 0 end) as partemail12subs
, sum(case when amount > 0 and amount < 75 then 1 else 0 end) as partless75
, sum(case when amount >= 75 then 1 else 0 end) as partmore75
, sum(nb_subs) as partitems
, sum(amount) as partamount
, avg(amount) as partavgamount
, avg(case when email_sent <> 0 then amount else null end ) as partactavgamount
, sum(case when email_sent = 0 and amount > 0 then 1 else 0 end) as partemail0order
, sum(case when amount > 0 and nb_subs > 0 then 1 else 0 end) as participations

from (
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
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.member_id -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end)  as members
	    , count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		    then m.member_id else NULL end ) as email_sent
	    , COALESCE(sum(quantity),0) as nb_subs
	    , COALESCE(sum(quantity * price),0) as amount
	    , mh.creation_channel_id
    from event_participation ep with(nolock)
		inner join [event] e with(nolock) on e.event_id = ep.event_id
	    inner join event_group eg with(nolock) on eg.event_id = ep.event_id 
	    inner join [group] g with(nolock) on g.group_id = eg.group_id 
        -- get the partner profit percent
        --left outer join partner_payment_config ppc on g.partner_id=ppc.partner_id
	    -- order
            left outer join #tps tps
	    on tps.suppid = ep.event_participation_id
	    -- enfant
	    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock) on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		left outer join event_participation epp with(nolock) on mhp.member_hierarchy_id = epp.event_participation_id and epp.create_date between @start_date and @end_date 
			--and ep.event_participation_id = epp.event_participation_id
	    left outer join member mp with(nolock) on mp.member_id = mhp.member_id
	    left join creation_channel cc with(nolock) on cc.creation_channel_id = mh.creation_channel_id
    where ep.create_date between @start_date and dateadd(mm, 1, @end_date)
		--and epp.create_date between @start_date and @end_date
	    and 	mh.active = 1
		and g.partner_id = 143 -- Kappa Delta
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
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.member_id -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) -- as member_name
		, mh.creation_channel_id
   -- order by 1, 2
) part 
group by member_type


select 
member_type
, count(members) as part
, sum(case when creation_channel_id in (7,20,23,38,40) then 1 else 0 end) as partinvited
, sum(case when email_sent = 0 then 0 else 1 end) as partemail1
, sum(case when email_sent > 0 and email_sent < 12 and nb_subs > 0 then 1 else 0 end) as partemail1subs
, sum(case when email_sent < 12 then 0 else 1 end) as partemail12
, sum(case when email_sent >= 12 and nb_subs > 0 then 1 else 0 end) as partemail12subs
, sum(case when amount > 0 and amount < 75 then 1 else 0 end) as partless75
, sum(case when amount >= 75 then 1 else 0 end) as partmore75
, sum(nb_subs) as partitems
, sum(amount) as partamount
, avg(amount) as partavgamount
, avg(case when email_sent <> 0 then amount else null end ) as partactavgamount
, sum(case when email_sent = 0 and amount > 0 then 1 else 0 end) as partemail0order
, sum(case when amount > 0 and nb_subs > 0 then 1 else 0 end) as participations

from (
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
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.member_id -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end)  as members
	    , count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		    then m.member_id else NULL end ) as email_sent
	    , COALESCE(sum(quantity),0) as nb_subs
	    , COALESCE(sum(quantity * price),0) as amount
	     , mh.creation_channel_id
    from event_participation ep with(nolock)
		inner join [event] e with(nolock) on e.event_id = ep.event_id
	    inner join event_group eg with(nolock) on eg.event_id = ep.event_id 
	    inner join [group] g with(nolock) on g.group_id = eg.group_id 
        -- get the partner profit percent
        --left outer join partner_payment_config ppc on g.partner_id=ppc.partner_id
	    -- order
        left outer join #tps tps on tps.suppid = ep.event_participation_id
		inner join (select event_id from #tps tps group by event_id) ae on ae.event_id = e.event_id
	    -- enfant
	    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock) on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		left outer join event_participation epp with(nolock) on mhp.member_hierarchy_id = epp.event_participation_id --and ep.event_participation_id = epp.event_participation_id
	    left outer join member mp with(nolock) on mp.member_id = mhp.member_id
	    left join creation_channel cc with(nolock) on cc.creation_channel_id = mh.creation_channel_id
    where ep.create_date between @start_date and dateadd(mm, 1, @end_date)
		--and epp.create_date between @start_date and @end_date
	    and 	mh.active = 1
		and g.partner_id in (8, 58)
--		and g.partner_id <> 143 -- Kappa Delta
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
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.member_id -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) -- as member_name
		, mh.creation_channel_id
   -- order by 1, 2
) part 
group by member_type

/*
select * from partner where partner_id in (8, 58)
select event_id from #tps tps group by event_id
*/

select 
member_type
, count(members) as part
, sum(case when creation_channel_id in (7,20,23,38,40) then 1 else 0 end) as partinvited
, sum(case when email_sent = 0 then 0 else 1 end) as partemail1
, sum(case when email_sent > 0 and email_sent < 12 and nb_subs > 0 then 1 else 0 end) as partemail1subs
, sum(case when email_sent < 12 then 0 else 1 end) as partemail12
, sum(case when email_sent >= 12 and nb_subs > 0 then 1 else 0 end) as partemail12subs
, sum(case when amount > 0 and amount < 75 then 1 else 0 end) as partless75
, sum(case when amount >= 75 then 1 else 0 end) as partmore75
, sum(nb_subs) as partitems
, sum(amount) as partamount
, avg(amount) as partavgamount
, avg(case when email_sent <> 0 then amount else null end ) as partactavgamount
, sum(case when email_sent = 0 and amount > 0 then 1 else 0 end) as partemail0order
, sum(case when amount > 0 and nb_subs > 0 then 1 else 0 end) as participations

from (
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
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.member_id -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end)  as members
	    , count ( distinct case when mh.creation_channel_id in(12,14,29,33,38, 35)
		    then m.member_id else NULL end ) as email_sent
	    , COALESCE(sum(quantity),0) as nb_subs
	    , COALESCE(sum(quantity * price),0) as amount
	     , mh.creation_channel_id
    from event_participation ep with(nolock)
		inner join [event] e with(nolock) on e.event_id = ep.event_id
	    inner join event_group eg with(nolock) on eg.event_id = ep.event_id 
	    inner join [group] g with(nolock) on g.group_id = eg.group_id 
        -- get the partner profit percent
        --left outer join partner_payment_config ppc on g.partner_id=ppc.partner_id
	    -- order
        left outer join #tps tps on tps.suppid = ep.event_participation_id 
	    --inner join (select event_id from #tps tps group by event_id) ae on ae.event_id = e.event_id
		-- enfant
	    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	    inner join member m with(nolock) on m.member_id = mh.member_id
	    -- parent
	    left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
		left outer join event_participation epp with(nolock) on mhp.member_hierarchy_id = epp.event_participation_id --and ep.event_participation_id = epp.event_participation_id
	    left outer join member mp with(nolock) on mp.member_id = mhp.member_id
	    left join creation_channel cc with(nolock) on cc.creation_channel_id = mh.creation_channel_id
    where ep.create_date between @start_date and dateadd(mm, 1, @end_date)
		--and epp.create_date between @start_date and @end_date
	    and 	mh.active = 1
		and  g.partner_id not in (8,58,143,138,753,807,819,752)
		and e.event_id not in (1234960,1235603,1236606,1236038,1236252,1232386,1238000,1236391,1242166,1196482,1250428)
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
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name)) 
		    -- participant orders must be under his name
		    when cc.member_type_id = 2
			    then m.member_id -- dbo.TitleCase(lower(m.first_name + ' ' + m.last_name))
		    else mp.member_id -- dbo.TitleCase(lower(mp.first_name + ' ' + mp.last_name))
		    end) -- as member_name
		, mh.creation_channel_id	
   -- order by 1, 2
) part 
group by member_type



END
GO
