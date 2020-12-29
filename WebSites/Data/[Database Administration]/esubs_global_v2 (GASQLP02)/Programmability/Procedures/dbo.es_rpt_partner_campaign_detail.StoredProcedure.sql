USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_campaign_detail]    Script Date: 02/14/2014 13:06:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[es_rpt_partner_campaign_detail]
	@start_date datetime,
	@end_date datetime
AS
BEGIN
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
)
	INSERT INTO #tps (
		orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate
	)
	select orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate
	from
	(
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
	where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701, 90, 9401 )
	/*union all
	select oh.ID as orderid
	     , sod.quantity
	     , cid.value as price
	     , et.suppID
	     , ep.event_id
	     , et.createdate
 	from qspecommerce.dbo.efundraisingtransaction et 
		inner join qspstore.dbo.orderheader oh on oh.id = et.orderid
		inner join qspstore.dbo.suborderheader soh on oh.id = soh.orderheaderid 
		inner join qspstore.dbo.suborderdetail sod on soh.id = sod.suborderheaderid
		inner join qspstore.dbo.catalogitemdetail cid on cid.id = sod.catalogitemdetailid
		inner join event_participation ep on ep.event_participation_id = et.suppid
	WHERE oh.OrderTotal IS NOT NULL
	  AND oh.OrderStatusID NOT IN (1, 10, 11)
	  AND soh.ShipToAddressID <> 0
	  and oh.aggregatorid in (7,13)*/
	) t
	--where createdate between @start_date  and @end_date
	order by  createdate
 		, price

create index ix_suppid on #tps (suppid)

-- update tout les activation a 0
update #tps set act = 0
-- trouver toutes les 1rst subs (activations)
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

-- header du rapport
select 	'-Partner Name'
	, 'Event ID'
	, 'Group Name'
	, '+Request'
	, '+Activation'
	, '+Subs'
	, '$Total Amount'
	, '$Profit'
from #tps 
where rownum = 1

-- pre-calculate the result 
create table #rpt (
	partner_name varchar(200)
	, event_id int
	, group_name varchar(200)
	, request int
	, activation int
	, subs int
	, total_amount float
	, profit float
)
insert into #rpt 
(	partner_name
	, ep.event_id
	, g.group_name
	, request
	, activation
	, subs
	, total_amount
	, profit
)

select 	p.partner_name
	, ep.event_id
	, g.group_name
	, 0 AS request
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
		else quantity * price * .4 end) as profit
from  
event_participation ep 
left outer join #tps tps on ep.event_participation_id = tps.suppid
inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
inner join member m on m.member_id = mh.member_id
--left outer join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
inner join event_group eg on eg.event_id = ep.event_id
inner join [group] g on g.group_id = eg.group_id
inner join partner p on p.partner_id = g.partner_id
where 
	(tps.createdate between @start_date  and @end_date or tps.createdate is null )
group by p.partner_name
	, ep.event_id
	, g.group_name



create index ix_partner_name on #rpt (partner_name)


delete from #rpt 
where  
	(request = 0 or request is null)
	and (activation = 0 or activation is null)
	and (subs = 0 or subs is null)
	and (total_amount = 0 or total_amount is null)

-- return the detail of the report
select partner_name
	, event_id
	, group_name
	, sum(request) as request
	, sum(activation) as activation
	, sum(subs) as subs
	, sum(total_amount) as total_amount
	, sum(profit) as profit
from #rpt

group by partner_name
	, event_id
	, group_name
order by partner_name
	, group_name
END
GO
