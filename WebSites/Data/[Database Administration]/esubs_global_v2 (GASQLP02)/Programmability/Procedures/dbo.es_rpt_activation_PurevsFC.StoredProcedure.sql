USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_activation_PurevsFC]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
create on 2010.03.03
create by mcote

description : all group activated for a specific period and sales associate with it.
			  - exclude KD 
			  - exclude canada

set @start_date = '09-01-2008'
set @end_date  = '02-28-2009'
set @sales_end_date  = '03-01-2009'
exec [es_rpt_activation_report] '09-01-2008', '02-28-2009','03-01-2009'

*/
CREATE proc [dbo].[es_rpt_activation_PurevsFC] 
 @start_date datetime
 ,@end_date datetime
 ,@sale_end_date datetime
as


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)

declare @sale_end_date2 varchar(30)
set @sale_end_date2 = convert(varchar(30), @sale_end_date, 101) + ' 23:59:59'
set @sale_end_date = convert(datetime, @sale_end_date2)

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
order by et.createdate
, od.price

create index ix_suppid on #tps (suppid)
create index ix_createdate on #tps (createdate)
-- update tout les activation a 0
-- fine the 1rst subs (activations) of each group
update #tps set act = 0
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

select 'MONTH', 'YEAR', 'ACT', 'ORDER', 'AMOUNT', 'QTY'

select 
	EventCreation
	, count(distinct act.event_id) as NumberOfAcivation
	, count(distinct tps.orderid) as NumberOfOrders
	, sum(tps.price*tps.quantity) as OrderTotal
	, sum(tps.quantity) as TotalQuantity
from (
	select tps.event_id, tps.createdate 
		from #tps tps
      LEFT JOIN [esubs_global_v2].[dbo].[event_participation] ep ON tps.suppid = ep.event_participation_id
	  INNER JOIN [esubs_global_v2].[dbo].[event] e ON e.event_id = ep.event_id 
      INNER JOIN [esubs_global_v2].[dbo].[member_hierarchy] mh ON mh.member_hierarchy_id = ep.member_hierarchy_id
      INNER JOIN [esubs_global_v2].[dbo].[member] m ON m.member_id = mh.member_id and m.partner_id <> 143 -- Kappa
		where createdate between @start_date and @end_date and act = 1 and e.discount_site = 0 
	) act
	inner join #tps tps on tps.event_id = act.event_id
	inner join 
	(
		select eg.event_id, 
			(case 
			when fc.consultant_id  = -1 then 'Pure Online' 
			when fc.is_fm = -1 then 'OTHER' 
			when fc.is_active = -1 then 'OTHER' 
			when fc.is_agent = -1 then 'OTHER' 
			when fc.phone_extension is null then 'Pure Online' 
			else 'Non-Pure (Fundraising Consultant)' end) as EventCreation

		from event_group eg 
		inner join [group] g with(nolock) on eg.group_id = g.group_id 
		left join eFundraisingProd.dbo.lead l  with(nolock) on l.lead_id = g.lead_id
		left join eFundraisingProd.dbo.consultant fc with(nolock) on fc.consultant_id = l.consultant_id
		group by eg.event_id
			, (case 
			when fc.consultant_id  = -1 then 'Pure Online' 
			when fc.is_fm = -1 then 'OTHER' 
			when fc.is_active = -1 then 'OTHER' 
			when fc.is_agent = -1 then 'OTHER' 
			when fc.phone_extension is null then 'Pure Online' 
			else 'Non-Pure (Fundraising Consultant)' end)
	) ec on ec.event_id = act.event_id

where tps.createdate between @start_date and @sale_end_date 
group by EventCreation
GO
