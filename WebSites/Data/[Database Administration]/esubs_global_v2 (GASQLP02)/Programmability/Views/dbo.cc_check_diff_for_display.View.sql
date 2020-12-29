USE [esubs_global_v2]
GO
/****** Object:  View [dbo].[cc_check_diff_for_display]    Script Date: 02/14/2014 13:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----
--JF lavigne
--view pour afficher le sales summary du custcare (sans se soucier s'il y a un montant a recevoir)
-----
CREATE view [dbo].[cc_check_diff_for_display]
as
select 
	e.event_id
	,eg.group_id
	,e.active
	,py.payment_info_id
	,sum(case when order_status_id <> -1 then total_profit else 0 end) as total_check_profit
	,isnull(old_check_profit,0) as old_check_profit
	,cast((sum(case when order_status_id <> -1 then total_profit else 0 end)-isnull(old_check_profit,0)) as money) as new_check_profit
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
GO
