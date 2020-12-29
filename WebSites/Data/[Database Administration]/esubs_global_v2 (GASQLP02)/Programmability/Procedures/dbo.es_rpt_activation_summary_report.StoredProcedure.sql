USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_activation_summary_report]    Script Date: 02/14/2014 13:06:45 ******/
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

exec [es_rpt_activation_summary_report] '2010-01-01', '2010-12-31 23:59:59', 10
exec [es_rpt_activation_summary_report] '2010-01-01', '2010-12-31 23:59:59', 30
exec [es_rpt_activation_summary_report] '2010-01-01', '2010-12-31 23:59:59', 60

exec [es_rpt_activation_summary_report] '2009-06-23', '2009-06-29 23:59:59', 10
CY10
exec [es_rpt_activation_summary_report] '2010-01-01', '2010-12-31 23:59:59'
exec [es_rpt_activation_summary_report] '2010-01-01', '2010-03-31 23:59:59'

exec [es_rpt_activation_summary_report] '2010-04-01', '2010-06-30 23:59:59', 10
exec [es_rpt_activation_summary_report] '2010-04-01', '2010-06-30 23:59:59', 30
exec [es_rpt_activation_summary_report] '2010-04-01', '2010-06-30 23:59:59', 60

exec [es_rpt_activation_summary_report] '2010-07-01', '2010-09-30 23:59:59', 10
exec [es_rpt_activation_summary_report] '2010-07-01', '2010-09-30 23:59:59', 30
exec [es_rpt_activation_summary_report] '2010-07-01', '2010-09-30 23:59:59', 60
exec [es_rpt_activation_summary_report] '2010-10-01', '2010-12-31 23:59:59'


*/
CREATE proc [dbo].[es_rpt_activation_summary_report] 
 @start_date datetime
 ,@end_date datetime
 ,@nbr_days int
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
from qspecommerce.dbo.efundraisingtransaction et
inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid 
inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
inner join event_participation ep on ep.event_participation_id = et.suppid
INNER JOIN dbo.es_get_valid_order_status() os ON os.order_status_id = o.order_status_id
INNER JOIN [QSPFulfillment].[dbo].[source] s ON s.source_id = o.source_id and s.source_group_id = 2
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
	count(distinct act.event_id) as NumberOfAcivation
	, count(distinct tps.orderid) as NumberOfOrders
	, sum(tps.price*tps.quantity) as OrderTotal
	, sum(tps.quantity) as TotalQuantity
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) and mh.create_date <  dateadd(d,@nbr_days,act.createdate) then m.member_id else NULL end ) as nb_members
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) and mh.create_date <  dateadd(d,@nbr_days,act.createdate) then mh.member_hierarchy_id else null end) as nb_part
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35)and mh.create_date <  dateadd(d,@nbr_days,act.createdate) then mh.parent_member_hierarchy_id else null end) as nb_active
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35)  and mh.create_date <  dateadd(d,@nbr_days,act.createdate)then mh.member_id else null end) as nb_supp
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) and mh.create_date <  dateadd(d,@nbr_days,act.createdate) and tps.orderid is not null then mh.member_hierarchy_id else null end) as nb_part_bought
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35)  and mh.create_date <  dateadd(d,@nbr_days,act.createdate)and tps.orderid is not null then mh.member_id else null end) as nb_supp_act
from (
	select tps.event_id, tps.createdate, tps.suppid
	  from #tps tps
      LEFT JOIN [esubs_global_v2].[dbo].[event_participation] ep ON tps.suppid = ep.event_participation_id
      INNER JOIN [esubs_global_v2].[dbo].[member_hierarchy] mh ON mh.member_hierarchy_id = ep.member_hierarchy_id
      INNER JOIN [esubs_global_v2].[dbo].[member] m ON m.member_id = mh.member_id and m.partner_id not in (8,58,143,741,809,762,753,784,766,787,764,806,752)
		where tps.createdate between @start_date and @end_date and tps.act = 1 
		and tps.event_id not in (1234960, 1235927)
	) act
	inner join #tps tps on tps.event_id = act.event_id
	Inner join event_participation ep with(nolock) on tps.suppid = ep.event_participation_id -- on tps.event_id = ep.event_id

	-- enfant
    inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
    inner join member m with(nolock) on m.member_id = mh.member_id
    -- parent
    left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
    left outer join member mp with(nolock) on mp.member_id = mhp.member_id 

where tps.createdate between act.createdate and dateadd(d,@nbr_days,act.createdate)
GO
