USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_activation_PurevsFC_byFC_report]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

exec [es_rpt_activation_PurevsFC_detail_report] '2010-07-01', '2010-09-30 23:59:59', 60, 'en-CA'



create on 2010.06.30
create by mcote

description : all group activated for a specific period and sales associate with it.
			  - exclude KD 
			  - exclude canada

exec [es_rpt_activation_PurevsFC_report] '2010-09-15', '2010-09-30 23:59:59', 300, 'en-US'



-- For Marc
-- Sept 2010 14-30
exec [es_rpt_activation_PurevsFC_report] '2010-09-14', '2010-09-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-09-14', '2010-09-30 23:59:59', 30, 'en-US'

exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-30 23:59:59', 15, 'en-CA'
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-30 23:59:59', 30, 'en-CA'
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-30 23:59:59', 60, 'en-CA'

-- Sept 2009 
exec [es_rpt_activation_PurevsFC_report] '2009-09-14', '2009-09-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-09-14', '2009-09-30 23:59:59', 30, 'en-US'

-- Sept 2010 1-14
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-13 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-13 23:59:59', 30, 'en-US'

-- Sept 2009 
exec [es_rpt_activation_PurevsFC_report] '2009-09-01', '2009-09-14 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-09-01', '2009-09-14 23:59:59', 30, 'en-US'

-- NOvembre 2010
exec [es_rpt_activation_PurevsFC_report] '2010-11-01', '2010-11-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-11-01', '2010-11-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-11-01', '2010-11-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-11-01', '2010-11-30 23:59:59', 90, 'en-US'

-- NOvembre 2009
exec [es_rpt_activation_PurevsFC_report] '2009-11-01', '2009-11-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-11-01', '2009-11-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-11-01', '2009-11-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-11-01', '2009-11-30 23:59:59', 90, 'en-US'


-- October 2010
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-10-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-10-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-10-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-10-31 23:59:59', 90, 'en-US'

-- October 2009
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-10-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-10-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-10-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-10-31 23:59:59', 90, 'en-US'


-- Sept 2010
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-09-01', '2010-09-30 23:59:59', 90, 'en-US'

-- Sept 2009
exec [es_rpt_activation_PurevsFC_report] '2009-09-01', '2009-09-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-09-01', '2009-09-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-09-01', '2009-09-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-09-01', '2009-09-30 23:59:59', 90, 'en-US'

-- August 2010
exec [es_rpt_activation_PurevsFC_report] '2010-08-01', '2010-08-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-08-01', '2010-08-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-08-01', '2010-08-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-08-01', '2010-08-31 23:59:59', 90, 'en-US'

-- August 2009
exec [es_rpt_activation_PurevsFC_report] '2009-08-01', '2009-08-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-08-01', '2009-08-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-08-01', '2009-08-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-08-01', '2009-08-31 23:59:59', 90, 'en-US'

-- Q1 2010
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-03-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-03-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-03-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-03-31 23:59:59', 90, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-03-31 23:59:59', 120, 'en-US'

-- Q1 2009
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-03-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-03-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-03-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-03-31 23:59:59', 90, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-03-31 23:59:59', 120, 'en-US'

-- Q2 2010
exec [es_rpt_activation_PurevsFC_report] '2010-04-01', '2010-06-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-04-01', '2010-06-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-04-01', '2010-06-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-04-01', '2010-06-30 23:59:59', 90, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-04-01', '2010-06-30 23:59:59', 120, 'en-US'

-- Q2 2009
exec [es_rpt_activation_PurevsFC_report] '2009-04-01', '2009-06-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-04-01', '2009-06-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-04-01', '2009-06-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-04-01', '2009-06-30 23:59:59', 90, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-04-01', '2009-06-30 23:59:59', 120, 'en-US'


-- Q3 2010
exec [es_rpt_activation_PurevsFC_report] '2010-07-01', '2010-09-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-07-01', '2010-09-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-07-01', '2010-09-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-07-01', '2010-09-30 23:59:59', 90, 'en-US'

-- Q3 2009
exec [es_rpt_activation_PurevsFC_report] '2009-07-01', '2009-09-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-07-01', '2009-09-30 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-07-01', '2009-09-30 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-07-01', '2009-09-30 23:59:59', 90, 'en-US'


-- Q4 2010
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-12-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-12-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-12-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-10-01', '2010-12-31 23:59:59', 90, 'en-US'

-- Q4 2009
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-11-18 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-12-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-12-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-12-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-10-01', '2009-12-31 23:59:59', 90, 'en-US'


-- CY 2010
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-12-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-12-31 23:59:59', 30, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-12-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2010-01-01', '2010-12-31 23:59:59', 90, 'en-US'

-- CY 2009
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-11-30 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-12-31 23:59:59', 15, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-12-31 23:59:59', 60, 'en-US'
exec [es_rpt_activation_PurevsFC_report] '2009-01-01', '2009-12-31 23:59:59', 90, 'en-US'

--CY10 and CY09 to date 
declare @date15 datetime 
declare @date30 datetime 
declare @date60 datetime 
declare @date90 datetime 
declare @date120 datetime 

set @date15 = dateadd(day, -15, getdate())
set @date30 = dateadd(day, -30, getdate())
set @date60 = dateadd(day, -60, getdate())
set @date90 = dateadd(day, -90, getdate())
set @date120 = dateadd(day, -90, getdate())

-- CY 2010

--exec [es_rpt_activation_PurevsFC_report] '2010-01-01', @date15, 15, 'en-US'
--exec [es_rpt_activation_PurevsFC_report] '2010-01-01', @date30, 30, 'en-US'
--exec [es_rpt_activation_PurevsFC_report] '2010-01-01', @date60, 60, 'en-US'
--exec [es_rpt_activation_PurevsFC_report] '2010-01-01', @date90, 90, 'en-US'
exec [es_rpt_activation_PurevsFC_byFC_report] '2010-01-01', @date120, 120, 'en-US'
exec [es_rpt_activation_PurevsFC_byFC_report] '2010-01-01', @date120, 120, 'en-CA'


set @date15 = dateadd(year, -1, @date15)
set @date30 = dateadd(year, -1, @date30)
set @date60 = dateadd(year, -1, @date60)
set @date90 = dateadd(year, -1, @date90)
set @date120 = dateadd(year, -1, @date120)

-- CY2009 to date
--exec [es_rpt_activation_PurevsFC_report] '2009-01-01', @date15, 15, 'en-US'
--exec [es_rpt_activation_PurevsFC_report] '2009-01-01', @date30, 30, 'en-US'
--exec [es_rpt_activation_PurevsFC_report] '2009-01-01', @date60, 60, 'en-US'
--exec [es_rpt_activation_PurevsFC_report] '2009-01-01', @date90, 90, 'en-US'
exec [es_rpt_activation_PurevsFC_byFC_report] '2009-01-01', @date120, 120, 'en-US'
exec [es_rpt_activation_PurevsFC_byFC_report] '2009-01-01', @date120, 120, 'en-CA'


*/


CREATE proc [dbo].[es_rpt_activation_PurevsFC_byFC_report] 
 @start_date datetime
 ,@end_date datetime
 ,@nbr_days int
, @culture_code varchar(5)
as


--declare @sale_end_date datetime
--declare @sale_start_date datetime

declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)

--declare @sale_end_date2 varchar(30)
--set @sale_end_date2 = convert(varchar(30), @sale_end_date, 101) + ' 23:59:59'
--set @sale_end_date = convert(datetime, @sale_end_date2)
-- pre-generate the tps
--drop table #tps
create table #tps (
rownum int identity(1,1)
, act int
, orderid int
, quantity int
, price money
, suppID int
, event_id int
, createdate datetime
)


if @culture_code = 'en-US'
BEGIN 

		INSERT INTO #tps (
		orderid
		, quantity
		, price
		, suppID
		, event_id
		, createdate
		)
		select o.order_id as orderid
		, od.quantity
		, od.price
		, et.suppID
		, ep.event_id
		, et.createdate
		 
		from QSPEcommerce.dbo.EfundraisingTransaction et with(nolock)
		inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid 
		inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
		inner join event_participation ep  with(nolock) on ep.event_participation_id = et.suppid
		INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
		INNER JOIN [QSPFulfillment].[dbo].[source] s with(nolock) ON s.source_id = o.source_id and s.source_group_id = 2
		where o.deleted = 0
		  AND od.deleted = 0
		order by et.createdate
		, od.price

		
END 
ELSE 
BEGIN 

	INSERT INTO #tps (
        orderid
	    , quantity
	    , price
        , suppID
		, event_id
	    , createdate
	)
	select o.order_id as orderid
		, 1 as quantity
		--, cod.price
		, cod.price - cod.tax - cod.tax2 as amountNoTax
--		, cod.tax
--		, cod.tax2
		, et.suppID
		, ep.event_id
		, o.order_date as updatedate
--		,hist.status
	from      
		qspCanadaOrderManagement.dbo.CustomerOrderDetail cod with(nolock) LEFT JOIN
		qspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory hist with(nolock) on hist.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and hist.transid = cod.transid INNER JOIN
		qspCanadaOrderManagement.dbo.InternetOrderID ioid with(nolock) ON cod.CustomerOrderHeaderInstance = ioid.CustomerOrderHeaderInstance
		inner join qspEcommerce.dbo.cart c with(nolock) on c.eds_order_id = ioid.internetorderid
		inner join qspFulfillment.dbo.[Order] o with(nolock) on o.order_id = c.x_order_id
		inner join qspecommerce.dbo.efundraisingtransaction et with(nolock) on et.orderid = o.order_id
		inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
		INNER JOIN dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id

        where cod.delflag = 0 
               and (hist.status in (42001,42000,42010) or cod.producttype in (46006, 46007, 460012)) --42000 = needs to be sent, and 42001 = sent, 42010 
        order by et.createdate
		, cod.price - cod.tax - cod.tax2 
	print 'en-CA'    
    
END 

create index ix_suppid on #tps (suppid)
create index ix_createdate on #tps (createdate)
-- update tout les activation a 0
-- fine the 1rst subs (activations) of each group
update #tps set act = 0
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

--select 'ACT', 'ORDER', 'AMOUNT', 'QTY'

select 
	ec.EventCreation
	, count(distinct act.event_id) as NumberOfAcivation
	, count(distinct tps.orderid) as NumberOfOrders
	, sum(tps.price*tps.quantity) as OrderTotal
	, sum(tps.quantity) as TotalQuantity
	--, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) and mh.create_date <  dateadd(d,@nbr_days,act.createdate) then m.member_id else NULL end ) as nb_members
	--, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) and mh.create_date <  dateadd(d,@nbr_days,act.createdate) then mh.member_hierarchy_id else null end) as nb_part
	--, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35)and mh.create_date <  dateadd(d,@nbr_days,act.createdate) then mh.parent_member_hierarchy_id else null end) as nb_active
	--, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35)  and mh.create_date <  dateadd(d,@nbr_days,act.createdate)then mh.member_id else null end) as nb_supp
	--, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) and mh.create_date <  dateadd(d,@nbr_days,act.createdate) and tps.orderid is not null then mh.member_hierarchy_id else null end) as nb_part_bought
	--, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35)  and mh.create_date <  dateadd(d,@nbr_days,act.createdate)and tps.orderid is not null then mh.member_id else null end) as nb_supp_act
from (
	select tps.event_id, tps.createdate, tps.suppid
	  from #tps tps
      LEFT JOIN [esubs_global_v2].[dbo].[event_participation] ep with(nolock) ON tps.suppid = ep.event_participation_id
      INNER JOIN [esubs_global_v2].[dbo].[member_hierarchy] mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
      INNER JOIN [esubs_global_v2].[dbo].[member] m with(nolock) ON m.member_id = mh.member_id and m.partner_id not in (8,58,143,138,753,807,819,752)
		where tps.createdate between @start_date and @end_date and tps.act = 1 
		and tps.event_id not in (1234960,1235603,1236606,1236038,1236252,1232386,1238000,1236391,1242166,1196482,1250428)
	) act --on act.event_id = ep.event_id
	inner join #tps tps on tps.event_id = act.event_id
	--Inner join  with(nolock) on tps.suppid = ep.event_participation_id -- on tps.event_id = ep.event_id

	-- enfant
   -- inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
   -- inner join member m with(nolock) on m.member_id = mh.member_id
    -- parent
   -- left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
   -- left outer join member mp with(nolock) on mp.member_id = mhp.member_id 
	-- pure vs FC
	inner join 
	(
		select eg.event_id, 
			(case
			when fc.consultant_id is null then 'Pure Online' 
			when fc.consultant_id  in (-1, 0, 936, 3481) then 'Pure Online' 
			when fc.is_fm = -1 then 'OTHER' 
			--when fc.is_active = -1 then 'OTHER' 
			when fc.is_agent = -1 then 'OTHER' 
			--when fc.phone_extension is null then 'Pure Online' 
			else fc.name end) --'Non-Pure (Fundraising Consultant)' end) 
			as EventCreation

		from event_group eg 
		inner join [group] g with(nolock) on eg.group_id = g.group_id 
		left join eFundraisingProd.dbo.lead l  with(nolock) on l.lead_id = g.lead_id
		left join eFundraisingProd.dbo.consultant fc with(nolock) on fc.consultant_id = l.consultant_id
		group by  eg.event_id, (case 
			when fc.consultant_id is null then 'Pure Online' 
			when fc.consultant_id  in (-1, 0, 936, 3481) then 'Pure Online' 
			when fc.is_fm = -1 then 'OTHER' 
			--when fc.is_active = -1 then 'OTHER' 
			when fc.is_agent = -1 then 'OTHER' 
			--when fc.phone_extension is null then 'Pure Online' 
			else fc.name end) --'Non-Pure (Fundraising Consultant)' end)
	) ec on ec.event_id = act.event_id 
	inner join [event] e with(nolock) on act.event_id = e.event_id and e.culture_code = @culture_code

where tps.createdate between act.createdate and dateadd(d,@nbr_days,act.createdate)
group by ec.EventCreation
order by 1 desc
GO
