USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_registration_summary_report]    Script Date: 02/14/2014 13:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
create on 2010.06.30
create by mcote

description : all group activated for a specific period and sales associate with it.
			  - exclude KD 
			  - exclude canada
CY08
exec [es_rpt_registration_summary_report] '2008-01-01', '2008-12-31 23:59:59'
exec [es_rpt_registration_summary_report] '2008-01-01', '2008-03-31 23:59:59'
exec [es_rpt_registration_summary_report] '2008-04-01', '2008-06-30 23:59:59'
exec [es_rpt_registration_summary_report] '2008-07-01', '2008-09-30 23:59:59'
exec [es_rpt_registration_summary_report] '2008-10-01', '2008-12-31 23:59:59'

CY09
exec [es_rpt_registration_summary_report] '2009-01-01', '2009-12-31 23:59:59'
exec [es_rpt_registration_summary_report] '2009-01-01', '2009-03-31 23:59:59'
exec [es_rpt_registration_summary_report] '2009-04-01', '2009-06-30 23:59:59'
exec [es_rpt_registration_summary_report] '2009-07-01', '2009-09-30 23:59:59'
exec [es_rpt_registration_summary_report] '2009-10-01', '2009-12-31 23:59:59'

CY10
exec [es_rpt_registration_summary_report] '2010-01-01', '2010-12-31 23:59:59'
exec [es_rpt_registration_summary_report] '2010-01-01', '2010-03-31 23:59:59'
exec [es_rpt_registration_summary_report] '2010-04-01', '2010-06-30 23:59:59'
exec [es_rpt_registration_summary_report] '2010-07-01', '2010-09-30 23:59:59'
exec [es_rpt_registration_summary_report] '2010-10-01', '2010-12-31 23:59:59'


--CY09 to date
exec [es_rpt_registration_summary_report] '2009-01-01', '2009-10-05 23:59:59'
exec [es_rpt_registration_summary_report] '2009-01-01', '2009-03-31 23:59:59'
exec [es_rpt_registration_summary_report] '2009-04-01', '2009-06-30 23:59:59'
exec [es_rpt_registration_summary_report] '2009-07-01', '2009-09-30 23:59:59'
exec [es_rpt_registration_summary_report] '2009-10-01', '2009-10-05 23:59:59'


*/
CREATE proc [dbo].[es_rpt_registration_summary_report] 
 @start_date datetime
 ,@end_date datetime

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
from qspecommerce.dbo.efundraisingtransaction et with(nolock)
inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid 
inner join qspfulfillment.dbo.[order_detail] od  with(nolock) on od.order_id = o.order_id
inner join event_participation ep with(nolock) on ep.event_participation_id = et.suppid
INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
INNER JOIN [QSPFulfillment].[dbo].[source] s with(nolock) ON s.source_id = o.source_id and s.source_group_id = 2
where o.deleted = 0
  AND od.deleted = 0
	
order by et.createdate
, od.price

create index ix_suppid on #tps (suppid)
create index ix_createdate on #tps (createdate)
-- update tout les activation a 0
-- fine the 1rst subs (activations) of each group
update #tps set act = 0
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

--select 'ACT', 'ORDER', 'AMOUNT', 'QTY'

select 
	 count(distinct e.event_id) as NumberOfEvent
	, count(distinct act.event_id) as NumberOfAcivation
	, count(distinct tps.orderid) as NumberOfOrders
	, sum(tps.price*tps.quantity) as OrderTotal
	, sum(tps.quantity) as TotalQuantity
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38)and act.event_id is null then e.event_id else Null end) as KOnotAct
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) and act.event_id is not null then act.event_id else Null end) as ActnotKO
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then e.event_id else Null end) as KO
    , count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then m.member_id else NULL end ) as nb_members
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then mh.member_hierarchy_id else null end) as nb_part
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.parent_member_hierarchy_id else null end) as nb_active
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.member_id else null end) as nb_supp

from
	[event] e with(nolock)
	LEFT join (
	select tps.event_id, tps.createdate 
	  from #tps tps
      LEFT JOIN [esubs_global_v2].[dbo].[event_participation] ep with(nolock) ON tps.suppid = ep.event_participation_id
      INNER JOIN [esubs_global_v2].[dbo].[member_hierarchy] mh with(nolock) ON mh.member_hierarchy_id = ep.member_hierarchy_id
      INNER JOIN [esubs_global_v2].[dbo].[member] m with(nolock) ON m.member_id = mh.member_id --and m.partner_id not in (8,58,143,741,809,762,753,784,766,787,764,806,752)	
		where tps.act = 1 
		and tps.event_id not in (1234960, 1235927)
	) act on e.event_id = act.event_id --and act.createdate < '2009-10-06' -- Test Mel
	Inner join event_participation ep with(nolock) on e.event_id = ep.event_id 
	LEFT join #tps tps on tps.suppid = ep.event_participation_id -- on tps.event_id = ep.event_id 
		--and tps.createdate < '2009-10-06' -- Test Mel
	-- enfant
    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
    inner join member m with(nolock) on m.member_id = mh.member_id
    -- parent
    left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
    left outer join member mp with(nolock) on mp.member_id = mhp.member_id 
	inner join event_group eg with(nolock) on e.event_id = eg.event_id 
	inner join [group] g with(nolock) on eg.group_id = g.group_id and  g.partner_id not in (8,58,143,741,809,762,753,784,766,787,764,806,752)	
where e.create_date between  @start_date and @end_date
and e.event_id not in (1234960, 1235927)

--select * from event where  event_id  in (1234960, 1235927)
GO
