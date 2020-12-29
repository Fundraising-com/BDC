USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_ccommission_report]    Script Date: 02/14/2014 13:06:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--exec es_rpt_partner_ccommission_report '2006-02-01', '2006-03-01'
/*
procedure qui affiche un rapport sommaire pour les partners
mod mcote 	2006-02-18 	- creation de table temporaire (optimisation)
				- select return the header of the report
				- creation d'une table temporaire pour les resultats du rapports
				- select return the detail of the report
				- select return the footer(sum) of the report
*/
CREATE         procedure [dbo].[es_rpt_partner_ccommission_report]
		@start_date as datetime 
		, @end_date as datetime
as

--drop table #tps
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
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
	     , ep.event_id
         , o.order_date as createdate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
		inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
	order by  createdate
 		, od.price


create index ix_suppid on #tps (suppid)

-- update tout les activation a 0
update #tps set act = 0
-- trouver toutes les 1rst subs (activations)
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

-- header du rapport
select 	'Partner Name'
	, 'Request'
	, 'Activation'
	, 'Subs'
	, 'Total Amount'
from #tps 
where rownum = 1

-- pre-calculate the result 
create table #rpt (
	partner_name varchar(200)
	, request int
	, activation int
	, subs int
	, total_amount float
)
insert into #rpt 
(	partner_name
	, request
	, activation
	, subs
	, total_amount
)
select 	p.partner_name
	, 0 AS request
	, SUM( ISNULL( tps.act, 0 ) ) AS activation
	, SUM( ISNULL( tps.quantity, 0 ) ) AS subs
	, SUM( ISNULL( tps.price, 0 ) ) AS total_amount
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


union all 

select 	p.partner_name
	, count(distinct m.member_id) 
	, 0
	, 0
	, 0
from member m 
inner join member_hierarchy mh on mh.member_id = m.member_id 
inner join [group] g on g.sponsor_id = mh.member_hierarchy_id
inner join partner p on g.partner_id = p.partner_id
where m.create_date between @start_date  and @end_date and mh.creation_channel_id =1
group by p.partner_name

-- return the detail of the report
select partner_name
	, sum(request) as request
	, sum(activation) as activation
	, sum(subs) as subs
	, sum(total_amount) as total_amount
from #rpt
group by partner_name
order by partner_name

--return the footer (summary/sum) of the report
select count(partner_name) as partner_name
	, sum(request) as request
	, sum(activation) as activation
	, sum(subs) as subs
	, sum(total_amount) as total_amount
from #rpt
GO
