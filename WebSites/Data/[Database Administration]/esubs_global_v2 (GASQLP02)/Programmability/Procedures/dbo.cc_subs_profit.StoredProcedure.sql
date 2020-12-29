USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_subs_profit]    Script Date: 02/14/2014 13:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[cc_subs_profit]
as

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[order_profit_backup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
delete from [dbo].[order_profit_backup]

insert into order_profit_backup
select * from order_profit

declare @checkPeriod int
declare @start_date datetime
declare @end_date datetime

if not exists (select * from payment_period where dateadd(month,-1,getdate()) between start_date and end_date)
begin
	
	SET @checkPeriod = (select max(payment_period_id) from payment_period) + 1
	select 
		@start_date = dateadd(month,1,start_date)
		, @end_date = dateadd(month,1,end_date) 
	from 	
		payment_period 
	where
		 payment_period_id = (@checkPeriod - 1)

 
	insert into payment_period
	values(@checkPeriod,@start_date,@end_date,getdate())
end

else
begin
   select @end_date = max(end_date), @checkPeriod = max(payment_period_id) from payment_period
end


update order_profit 
set order_status_id = -1
from order_profit op
left outer join qspstore.dbo.cc_totals_per_sale cc
on cc.orderid = op.order_id
where cc.orderid is null
and op.order_date > '2005-09-16'






--update chaque jour les vente de la periode passe avec le payment de la periode courante
update
order_profit
set payment_id = pa.payment_id
,update_date = getdate()
from order_profit op
inner join payment_info py
on py.event_id = op.event_id
inner join payment pa
on pa.payment_info_id = py.payment_info_id
and payment_period_id = @checkPeriod
where 
op.payment_id is null
and op.order_date <= @end_date 



/* subs 100% profit (sale before May 16th 2006) */

insert into order_profit(
	event_id
	,event_participation_id
	,order_id,order_date
	,item_price
	,profit
	,total_profit
	,order_item_id
	,order_status_id
)
select  
 	a.event_id 
 	, b.suppid
 	, b.orderid
 	, b.orderdate
	, b.ordertotal
	, 1.00 as profit
	, case when b.ordertotal <= 25  then b.ordertotal  
	       else 25 + (b.ordertotal - 25) *0.4 end as profit
	,b.order_detail_id
	,b.orderstatusid
from 
(       --- va me chercher la premiere date de vente
	select 
		min(order_detail_id) order_detail_id
		,event_id

	from 	
		qspstore.dbo.cc_totals_per_sale tps
		inner join esubs_global_v2.dbo.event_participation ep
		 on ep.event_participation_id = tps.suppid
	
	where 
		not exists(select order_id from order_profit op where op.order_id = tps.orderid)  -- à cause des merges
	and 	tps.orderdate >=  '2004-01-01' 
	group by 
		event_id
) a
inner join (  -- va me chercher le order id pour cette premier date
	select 
		event_id 
		,orderid
		,order_detail_id
		, suppid
 		, orderdate
		, ordertotal
		,order_item_id
		,orderstatusid
	from 	
		qspstore.dbo.cc_totals_per_sale tps
		inner join esubs_global_v2.dbo.event_participation ep
		 on ep.event_participation_id = tps.suppid
	
	where 
		not exists(select order_id from order_profit op where op.order_id = tps.orderid)  -- à cause des merges
	and 	tps.orderdate >=  '2004-01-01' 
)b
on a.event_id = b.event_id
and a.order_detail_id = b.order_detail_id

where not exists
(select event_id from order_profit op where a.event_id = op.event_id)
----
and b.orderdate < '2006-05-16'
-----

order by 3,8
/* les 40% */

insert into order_profit(
event_id,event_participation_id,order_id,order_date,item_price,profit,total_profit,order_item_id,order_status_id)
select  
	 ep.event_id 
	, tps.suppid
	, tps.orderid
	, tps.orderdate
	, tps.ordertotal
	, 0.40 as profit
	, tps.ordertotal * 0.40 as profit
	, tps.order_detail_id
	,tps.orderstatusid
from 
	qspstore.dbo.CC_Totals_per_sale tps
	inner join event_participation ep
	on ep.event_participation_id = tps.suppid
where
not exists
	( select * 
		from order_profit op 
		where op.order_id = tps.orderid and op.order_item_id = tps.order_detail_id 
	)
and tps.orderdate >= '2004-01-01'
GO
