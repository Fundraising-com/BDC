USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_consultant_campaign_detail]    Script Date: 11/13/2014 11:38:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
EXEC [dbo].[es_rpt_consultant_campaign_detail] '07/01/2014','10/23/2014',8
*/
ALTER  PROCEDURE [dbo].[es_rpt_consultant_campaign_detail]
	@start_date datetime,
	@end_date datetime, 
	@consultant_id int
AS
BEGIN

-- declare @start_date datetime
-- declare @end_date datetime
-- declare @consultant_id int
-- 
-- 
-- set @start_date = '2007-01-01'
-- set @end_date = '2007-01-31'
-- set @consultant_id = 8
-- 
-- drop table #tps
-- pre-generate the tps
create table #tps (
	rownum int identity(1,1)
	, act int
	, orderid int
	, quantity int
	, price money
	, suppID int
	, event_id int
    , createdate datetime
    ,updatedate datetime
)
	INSERT INTO #tps (
	       act
	     , orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate
	)
	select act
	     , orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate
	from
	(
	select act, 
	       order_id as orderid,
	       quantity,
	       price, 
	       supp_id as suppid,
	       event_id,
	       create_date as createdate
	  from es_get_valid_orders_items() vw
	 where create_date between @start_date  and @end_date) t
	order by  createdate
 		, price

create index ix_suppid on #tps (suppid)

-- header du rapport

select 	'Consultant Name'
	, '-Partner Name'
	, 'Lead ID'
	, 'Group Name'
	, '+Activation'
	, '+Subs'
	, '$Total Amount'
	, '$Profit'

--drop table #rpt
-- pre-calculate the result 
create table #rpt (
	consultant_name varchar(200)
	, partner_name varchar(200)
	, lead_id int
	, group_name varchar(200)
	, activation int
	, subs int
	, total_amount float
	, profit float
)
insert into #rpt 
(	consultant_name
	, partner_name
	, l.lead_id
	, g.group_name
	, activation
	, subs
	, total_amount
	, profit
)

select 	con.name
	, p.partner_name
	, l.lead_id
	, g.group_name
	, SUM( tps.act) AS activation
	, SUM( tps.quantity) AS subs
	, SUM( tps.price) AS total_amount
	, sum(case 
		-- Fin de 100% Profit sur first subs
		when tps.act = 1 and tps.createdate > '2006-05-16' then 
			quantity * price * .4 
		-- 100% premier 25$ 40% reste de l'order
		when tps.act = 1 and tps.createdate > '2005-10-16' then 
			(case when quantity * price > 25  then (((quantity * price) - 25) * 0.4) + 25
			else quantity * price end)
		-- 100% maximum 25$
		when tps.act = 1 and tps.createdate < '2005-10-16' then 
			(case when quantity * price > 25  then 25
			else quantity * price end)
		else 
			(case when tps.event_id = 1 then quantity * price * .5
			else quantity * price *(Isnull(ppc.profit_percentage, 40.0)/100.0) end)
		end) as profit
from  
event_participation ep 
left outer join #tps tps on ep.event_participation_id = tps.suppid
join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
join member m on m.member_id = mh.member_id
--left outer join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
join event_group eg on eg.event_id = ep.event_id
join [group] g on g.group_id = eg.group_id
join partner p on p.partner_id = g.partner_id
join partner_product_offer ppo on p.partner_id = ppo.partner_id and product_offer_id<>5
left join partner_payment_config ppc  on ppc.partner_id = p.partner_id
join efundraisingprod..lead l on g.lead_id = l.lead_id
join efundraisingprod..consultant con on l.consultant_id = con.consultant_id
where 
(	
(tps.createdate between @start_date  and @end_date)
)
	and l.consultant_id = @consultant_id
group by con.name
	, p.partner_name
	, l.lead_id
	, g.group_name



create index ix_partner_name on #rpt (partner_name)


delete from #rpt 
where  
	(activation = 0 or activation is null)
	and (subs = 0 or subs is null)
	and (total_amount = 0 or total_amount is null)

-- return the detail of the report
select consultant_name
	, partner_name
	, lead_id
	, group_name
	, sum(activation) as activation
	, sum(subs) as subs
	, sum(total_amount) as total_amount
	, sum(profit) as profit
from #rpt

group by consultant_name
	, partner_name
	, lead_id
	, group_name
order by consultant_name
	, partner_name
	, lead_id
	, group_name
END

