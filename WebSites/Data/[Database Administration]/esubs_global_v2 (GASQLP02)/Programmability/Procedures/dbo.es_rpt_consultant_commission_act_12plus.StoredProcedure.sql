USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_consultant_commission_act_12plus]    Script Date: 02/14/2014 13:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
consultant commission report activation 12+
creation  mcote 	2008-02-05 	

*/
CREATE      procedure [dbo].[es_rpt_consultant_commission_act_12plus]
		@start_date as datetime 
		, @end_date as datetime
as
BEGIN


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)


--cast(floor(cast((getdate()) as float)) as datetime)
--drop table #t



--select 

--	fc,
--	count(distinct event_id)

--from 	(	
	select 	ep.event_id,
		e.event_name,
		coalesce(promotion_type_code, 'OTHER') as type,
		pa.partner_name,
		c.name as fc,
		min(tps.orderdate) as activation_date,
		nb_part,
		nb_supp,
		l.lead_assignment_date,
		sum(totalquantity) as nbr_subs
	into #t
	from event_participation ep 
	inner join qspstore.dbo.es_totals_per_sale tps on tps.suppid = ep.event_participation_id
	inner join event e on e.event_id=ep.event_id
	inner join event_group eg on e.event_id = eg.event_id
	inner join [group] g on g.group_id = eg.group_id
	left join efundraisingprod.dbo.lead l on g.lead_id=l.lead_id
	left join efundraisingprod.dbo.promotion p on p.promotion_id = l.promotion_id
	left join partner pa on pa.partner_id = g.partner_id
	left join efundraisingprod.dbo.consultant c on c.consultant_id = l.consultant_id
	left join (SELECT ep2.event_id, count(*) as nb_part
				from event_participation ep2 
					inner join member_hierarchy mh on ep2.member_hierarchy_id = mh.member_hierarchy_id
					inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id and cc.member_type_id=2 group by event_id) part on part.event_id = e.event_id
	left join (SELECT ep2.event_id, count(*) as nb_supp
				from event_participation ep2 
					inner join member_hierarchy mh on ep2.member_hierarchy_id = mh.member_hierarchy_id
					inner join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id and cc.member_type_id=3 group by event_id) supp on supp.event_id = e.event_id
	--where nb_part>=12
	group by ep.event_id,
		e.event_name,
		promotion_type_code,
		partner_name,
		c.name,
		nb_part,
		nb_supp,
		lead_assignment_date
	--) a
--where activation_date between @StartDate and @EndDate
--group by fc
select 'Consultant Name', '+Activation 12+', '$Commission Amount' 

select fc,
count(distinct event_id), 
count(distinct event_id) * 10
from #t

where activation_date between @start_date and @end_date and nb_part>=12 and lead_assignment_date<=activation_date
group by fc

drop table #t

END
GO
