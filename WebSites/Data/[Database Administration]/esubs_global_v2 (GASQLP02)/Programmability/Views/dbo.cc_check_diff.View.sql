USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[cc_check_diff]    Script Date: 02/14/2014 13:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from  [cc_check_diff_closed_period_new]

CREATE    view [dbo].[cc_check_diff]
as
select 
	e.event_id
	,eg.group_id
	,e.active
	,py.payment_info_id
	,sum(case when order_status_id <> -1 then total_profit else 0 end) as total_check_profit
	,isnull(old_check_profit,0) as old_check_profit
	,sum(case when order_status_id <> -1 then total_profit else 0 end)-isnull(old_check_profit,0) as new_check_profit
	,count(distinct op.payment_id) as nb_check

from 
	event e
	inner join order_profit op
	on e.event_id = op.event_id
	inner join event_group eg
	on e.event_id = eg.event_id
	inner join payment_info py
	on py.event_id = eg.event_id
	and py.active = 1
	left outer join (
		       select 
		  pi.event_id
	              ,sum(paid_amount) as old_check_profit
	        from 
		   payment p
      		    inner join payment_info pi 
                               on p.payment_info_id = pi.payment_info_id
                       group by pi.event_id
	)a
	on a.event_id = e.event_id

		
group by
	e.event_id
	,e.active
	,py.payment_info_id
	,eg.group_id
	,old_check_profit
having 
	(sum(case when order_status_id <> -1 then total_profit else 0 end)-isnull(old_check_profit,0) >19.99)
or 	( e.active = 0 and sum(case when order_status_id <> -1 then total_profit else 0 end)-isnull(old_check_profit,0) >=4)
or 	(sum(case when a.event_id is null then 0 else 1 end) = 0 and max(order_date) > '2004-05-01' )

--order by e.event_id
GO
