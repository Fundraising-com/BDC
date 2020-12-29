USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_daily_numbers]    Script Date: 02/14/2014 13:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* 
get the daily numbers report for esubs call sp in esubs_global_v2
created by: Mel		2006/03/22
*/
CREATE  PROCEDURE [dbo].[es_rpt_daily_numbers]
		@start_date datetime
		, @end_date datetime

AS

select theday
	, Sum(Nb_Leads) as Nb_Leads
	, Sum(Total_Registration) as Total_Registration
	, Sum(kick_off_prev) as kick_off_prev
	, Sum(kick_off) as kick_off
	, Sum(nb_activation_prev) as nb_activation_prev
	, Sum(nb_activation) as nb_activation
	, Sum(total_subs_prev) as total_subs_prev
	, Sum(total_subs) as total_subs
	, Sum(nb_kappa_subs) as nb_kappa_subs
	, Sum(total_amount) as total_amount
from 
(
select 	
	day(m.create_date) as theday
	, 0 as Nb_Leads
	, count(distinct m.member_id) as Total_Registration
	, 0 as kick_off
	, 0 as nb_activation
	, 0 as total_subs
	, 0 as total_subs_prev
	, 0 as nb_kappa_subs
	, 0 as nb_activation_prev
	, 0 as kick_off_prev
	, 0 as total_amount
from member m 
inner join member_hierarchy mh on mh.member_id = m.member_id 
inner join [group] g on g.sponsor_id = mh.member_hierarchy_id
inner join partner p on g.partner_id = p.partner_id
where m.create_date between @start_date and @end_date  and mh.creation_channel_id =1
/*from member m inner join member_hierarchy mh on mh.member_id = m.member_id 
where m.create_date between @StartDate and @EndDate and mh.creation_channel_id =1
*/
group by day(m.create_date) --order by day(m.create_date) asc

union 

select 
	day(l.lead_entry_date) as theday
	, count(distinct l.lead_id) as Nb_Leads
	, 0 as Total_Registration
	, 0 as kick_off
	, 0 as nb_activation
	, 0 as total_subs
	, 0 as total_subs_prev
	, 0 as nb_kappa_subs
	, 0 as nb_activation_prev
	, 0 as kick_off_prev
	, 0 as total_amount
from efundraisingprod.dbo.lead l inner join efundraisingprod.dbo.promotion p on p.promotion_id = l.promotion_id
where p.promotion_type_code = 'ON' and l.lead_entry_date between @start_date and @end_date
group by day(l.lead_entry_date) --order by day(l.lead_entry_date) asc

union 

select 
	day(first_part_date) as theday
	, 0 as Nb_Leads
	, 0 as Total_Registration
	, count(distinct member_hierarchy_id) as kick_off
	, 0 as nb_activation
	, 0 as total_subs
	, 0 as total_subs_prev
	, 0 as nb_kappa_subs
	, 0 as nb_activation_prev
	, 0 as kick_off_prev
	, 0 as total_amount
from (
	select 	min(mh2.create_date) as first_part_date,
		mh.member_hierarchy_id
	
		from member_hierarchy mh 
		inner join event_participation ep on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
		inner join member_hierarchy mh2 on mh2.parent_member_hierarchy_id = mh.member_hierarchy_id
		inner join creation_channel cc2 on cc2.creation_channel_id = mh2.creation_channel_id
	where cc.member_type_id = 1
		and cc2.member_type_id = 2
	
	group by mh.member_hierarchy_id
) a
where first_part_date between @start_date and @end_date
group by day(first_part_date)
--order by day(first_part_date) asc

union 

select 
	day(first_part_date) as theday
	, 0 as Nb_Leads
	, 0 as Total_Registration
	, 0 as kick_off
	, 0 as nb_activation
	, 0 as total_subs
	, 0 as total_subs_prev
	, 0 as nb_kappa_subs
	, 0 as nb_activation_prev
	, count(distinct member_hierarchy_id) as kick_off_prev
	, 0 as total_amount
from (
	select 	min(mh2.create_date) as first_part_date,
		mh.member_hierarchy_id
	
		from member_hierarchy mh 
		inner join event_participation ep on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
		inner join member_hierarchy mh2 on mh2.parent_member_hierarchy_id = mh.member_hierarchy_id
		inner join creation_channel cc2 on cc2.creation_channel_id = mh2.creation_channel_id
	where cc.member_type_id = 1
		and cc2.member_type_id = 2
	
	group by mh.member_hierarchy_id
) a
where first_part_date between dateadd(yyyy, -1, @start_date) and dateadd(yyyy, -1, @end_date)
group by day(first_part_date)
--order by day(first_part_date) asc

--union 

--select 
--	day(activation_date) as theday
--	, 0 as Nb_Leads
--	, 0 as Total_Registration
--	, 0 as kick_off
--	, count(distinct event_id) as nb_activation
--	, 0 as total_subs
--	, 0 as total_subs_prev
--	, 0 as nb_kappa_subs
--	, 0 as nb_activation_prev
--	, 0 as kick_off_prev
--	, 0 as total_amount
--from 	(	
--	select 	ep.event_id,
--		min(tps.orderdate) as activation_date
	
--	from event_participation ep 
--	inner join qspstore.dbo.es_totals_per_sale tps on tps.suppid = ep.event_participation_id
	
--	group by ep.event_id
--	) a
--where activation_date between @start_date and @end_date
--group by day(activation_date)

--union 

--select 
--	day(activation_date) as theday
--	, 0 as Nb_Leads
--	, 0 as Total_Registration
--	, 0 as kick_off
--	, 0 as nb_activation
--	, 0 as total_subs
--	, 0 as total_subs_prev
--	, 0 as nb_kappa_subs
--	, 0 as total_amount
--	, count(distinct event_id) as nb_activation_prev
--	, 0 as kick_off_prev
--from 	(	
--	select 	ep.event_id,
--		min(tps.orderdate) as activation_date
	
--	from event_participation ep 
--	inner join qspstore.dbo.es_totals_per_sale tps on tps.suppid = ep.event_participation_id
	
--	group by ep.event_id
--	) a
--where activation_date between dateadd(yyyy, -1, @start_date) and dateadd(yyyy, -1, @end_date)
--group by day(activation_date)

--union 

--select 
--	day(orderdate) as theday 
--	, 0 as Nb_Leads
--	, 0 as Total_Registration
--	, 0 as kick_off
--	, 0 as nb_activation
--	, sum(totalquantity) as total_subs
--	, 0 as total_subs_prev
--	, 0 as nb_kappa_subs
--	, 0 as nb_activation_prev
--	, 0 as kick_off_prev
--	, sum(ordertotal) as total_amount
--from qspstore.dbo.es_totals_per_sale 
--where orderdate between @start_date and @end_date
--group by day(orderdate) --order by day(orderdate) asc 

--union 

--select 
--	day(orderdate) as theday 
--	, 0 as Nb_Leads
--	, 0 as Total_Registration
--	, 0 as kick_off
--	, 0 as nb_activation
--	, 0 as total_subs
--	, sum(totalquantity) as total_subs_prev
--	, 0 as nb_kappa_subs
--	, 0 as nb_activation_prev
--	, 0 as kick_off_prev
--	, 0 as total_amount
--from qspstore.dbo.es_totals_per_sale 
--where orderdate between dateadd(yyyy, -1, @start_date) and dateadd(yyyy, -1, @end_date)
--group by day(orderdate) --order by day(orderdate) asc 

--union 

--select 
--	day(orderdate) as theday
--	, 0 as Nb_Leads
--	, 0 as Total_Registration
--	, 0 as kick_off
--	, 0 as nb_activation
--	, 0 as total_subs
--	, 0 as total_subs_prev
--	, sum(totalquantity) as nb_kappa_subs
--	, 0 as nb_activation_prev
--	, 0 as kick_off_prev
--	, 0 as total_amount
--from 	[group] g
--	inner join event_group eg on g.group_id = eg.group_id
--	inner join event_participation ep on ep.event_id = eg.event_id
--	inner join qspstore.dbo.es_totals_per_sale tps on tps.suppid = ep.event_participation_id
--where orderdate between @start_date and @end_date 
--and g.partner_id = 143
--group by day(orderdate)
--order by day(orderdate) asc

) daily_report
group by theday
order by 1
GO
