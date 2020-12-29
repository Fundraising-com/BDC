USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_kappadelta_analysis]    Script Date: 02/14/2014 13:06:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from creation_channel where member_type_id = 2
/*
create on 2010.10.18
create by mcote

description : all Kappa Delta Campaigns create between a certain date range

exec [es_rpt_kappadelta_analysis] '2004-07-01', '2005-06-30 23:59:59'
exec [es_rpt_kappadelta_analysis] '2005-07-01', '2006-06-30 23:59:59'
exec [es_rpt_kappadelta_analysis] '2006-07-01', '2007-06-30 23:59:59'
exec [es_rpt_kappadelta_analysis] '2007-07-01', '2008-06-30 23:59:59'
exec [es_rpt_kappadelta_analysis] '2008-07-01', '2009-06-30 23:59:59'
exec [es_rpt_kappadelta_analysis] '2009-07-01', '2010-06-30 23:59:59'
exec [es_rpt_kappadelta_analysis] '2009-07-01', '2009-10-18 23:59:59' -- FY10 TO DATE
exec [es_rpt_kappadelta_analysis] '2010-07-01', '2011-06-30 23:59:59' -- FY11

*/
CREATE proc [dbo].[es_rpt_kappadelta_analysis] 
 @start_date datetime
 ,@end_date datetime
as


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)

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
 
from QSPEcommerce.dbo.EfundraisingTransaction et
inner join qspfulfillment.dbo.[order] o with(nolock) on o.order_id = et.orderid 
inner join qspfulfillment.dbo.[order_detail] od with(nolock) on od.order_id = o.order_id
inner join event_participation ep  with(nolock) on ep.event_participation_id = et.suppid
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

select
	sum(case when Amount >= 75 then 1 else 0 end) as over75
	, sum(case when Amount < 75 and Amount <> 0 then 1 else 0 end) as lower75
	, sum(case when Amount = 0 then 1 else 0 end) as noorders
	, (case when member_type_id = 1 then 'Sponsor' 
		when member_type_id = 2 then 'Participant'
		else 'UNKNOWN'  end) as members
	, member_hierarchy_id

from 
(
select ep.event_id
	,  mhp.member_hierarchy_id 
	,  mhp.creation_channel_id
	, SUM(coalesce(tps.quantity, 0) * coalesce(tps.price, 0)) as Amount
,(case 
	-- sponsor order must be under his name
	when (mp.first_name + ' ' + mp.last_name) is null 
	then 'Sponsor'
	--then mh.member_hierarchy_id
	-- participant orders must be under his name
	when cc.member_type_id = 2
	--then mh.member_hierarchy_id
	then 'Participant'
	else 'Supprter'
	--else mhp.member_hierarchy_id
	end) as member_hierarchy_id

from event_participation ep with(nolock)
	left join #tps tps on tps.suppID = ep.event_participation_id
	-- enfant
	inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join member m with(nolock) on m.member_id = mh.member_id and m.partner_id = 143
    -- parent
	left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	left outer join member mp with(nolock) on mp.member_id = mhp.member_id and m.partner_id = 143
	inner join creation_channel cc on cc.creation_channel_id = mhp.creation_channel_id 
	
where ep.create_date between  @start_date and @end_date
--and mhp.creation_channel_id in(7,8,9,10,17,18,20,22,23,32,35,36,38,40)  
group by  ep.event_id
	,  mhp.member_hierarchy_id 
	,  mhp.creation_channel_id
	, (case 
	-- sponsor order must be under his name
	when (mp.first_name + ' ' + mp.last_name) is null 
	then 'Sponsor'
	--then mh.member_hierarchy_id
	-- participant orders must be under his name
	when cc.member_type_id = 2
	--then mh.member_hierarchy_id
	then 'Participant'
	else 'Supprter'
	--else mhp.member_hierarchy_id
	end)-- as member_hierarchy_id
) kdp
inner join creation_channel cc on cc.creation_channel_id = kdp.creation_channel_id 
group by  (case when member_type_id = 1 then 'Sponsor' 
		when member_type_id = 2 then 'Participant'
		else 'UNKNOWN'  end) 
, member_hierarchy_id
/*
select ep.event_id
	, mhp.member_hierarchy_id
	, mp.first_name 
	, mp.last_name 
	, mp.email_address
	, SUM(coalesce(tps.quantity, 0) * coalesce(tps.price, 0)) as Amount
	, (case when cc.member_type_id = 1 then 'Sponsor' 
		when cc.member_type_id = 2 then 'Participant'
		else 'UNKNOWN'  end) as members
from event_participation ep with(nolock)
	left join #tps tps on tps.suppID = ep.event_participation_id
	-- enfant
	inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join member m with(nolock) on m.member_id = mh.member_id and m.partner_id = 143
    -- parent
	left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	left outer join member mp with(nolock) on mp.member_id = mhp.member_id and m.partner_id = 143
	
	inner join creation_channel cc on cc.creation_channel_id = kdp.creation_channel_id 

where ep.create_date between  @start_date and @end_date  
	
group by   ep.event_id
	, mhp.member_hierarchy_id
	, mp.first_name 
	, mp.last_name 
	, mp.email_address
, (case when cc.member_type_id = 1 then 'Sponsor' 
		when cc.member_type_id = 2 then 'Participant'
		else 'UNKNOWN'  end)
*/
/*

--select 'ACT', 'ORDER', 'AMOUNT', 'QTY'

select 
	ec.EventCreation
	, e.event_id
	, e.event_name
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
from
	event e  
	(
	select tps.event_id, tps.createdate, tps.suppid
	  from #tps tps
      LEFT JOIN [esubs_global_v2].[dbo].[event_participation] ep ON tps.suppid = ep.event_participation_id
      INNER JOIN [esubs_global_v2].[dbo].[member_hierarchy] mh ON mh.member_hierarchy_id = ep.member_hierarchy_id
      INNER JOIN [esubs_global_v2].[dbo].[member] m ON m.member_id = mh.member_id and m.partner_id = 143
		where 
			--tps.createdate between @start_date and @end_date and 
			tps.act = 1 
		--and tps.event_id not in (1234960, 1235927)
	) act on act.event_id = e.event_id
	inner join #tps tps on tps.event_id = act.event_id
	
select *
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then m.member_id else NULL end ) as nb_members
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38) then mh.member_hierarchy_id else null end) as nb_part
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) then mh.parent_member_hierarchy_id else null end) as nb_active
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35)  then mh.member_id else null end) as nb_supp
	, count(distinct case when mh.creation_channel_id in(7,20,23,33,38)  and tps.orderid is not null then mh.member_hierarchy_id else null end) as nb_part_bought
	, count(distinct case when mh.creation_channel_id in(12,14,29,32, 35) and tps.orderid is not null then mh.member_id else null end) as nb_supp_act
from event e with(nolock)
	inner join event_group eg with(nolock) on eg.event_id = e.event_id 
	inner join [group] g with(nolock) on eg.group_id = g.group_id   and partner_id = 143
	inner join event_participation ep with(nolock) on e.event_id = ep.event_id 
	inner join #tps tps on tps.event_id = e.event_id
	-- enfant
	inner join member_hierarchy mh with(nolock) on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join member m with(nolock) on m.member_id = mh.member_id
    -- parent
	left outer join member_hierarchy mhp with(nolock) on mhp.member_hierarchy_id = mh.parent_member_hierarchy_id
	left outer join member mp with(nolock) on mp.member_id = mhp.member_id 
)
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
			else fc.name end) as EventCreation

		from event_group eg 
		inner join [group] g with(nolock) on eg.group_id = g.group_id 
		left join eFundraisingProd.dbo.lead l  with(nolock) on l.lead_id = g.lead_id
		left join eFundraisingProd.dbo.consultant fc with(nolock) on fc.consultant_id = l.consultant_id
		group by eg.event_id
			, (case 
			when fc.consultant_id is null then 'Pure Online' 
			when fc.consultant_id  in (-1, 0, 936, 3481) then 'Pure Online' 
			when fc.is_fm = -1 then 'OTHER' 
			--when fc.is_active = -1 then 'OTHER' 
			when fc.is_agent = -1 then 'OTHER' 
			--when fc.phone_extension is null then 'Pure Online' 
			else fc.name end)
	) ec on ec.event_id = act.event_id 
	inner join [event] e on e.event_id = ec.event_id
where e.create_date between @start_date and @end_date 
group by ec.EventCreation
	, e.event_id
	, e.event_name
*/
--select * from partner where partner_id  in (8,58,143,741,809,762,753,784,766,787,764,806,752)
GO
