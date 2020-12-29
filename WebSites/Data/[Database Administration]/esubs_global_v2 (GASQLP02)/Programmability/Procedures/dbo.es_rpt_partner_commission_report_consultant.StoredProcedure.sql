USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_partner_commission_report_consultant]    Script Date: 02/14/2014 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec es_rpt_partner_commission_report_consultant '2006-09-01', '2006-09-30 23:59:59'

/*
procedure qui affiche un rapport sommaire pour les partners
mod mcote 	2006-02-18 	- creation de table temporaire (optimisation)
				- select return the header of the report
				- creation d'une table temporaire pour les resultats du rapports
				- select return the detail of the report
				- select return the footer(sum) of the report

*/
CREATE     procedure [dbo].[es_rpt_partner_commission_report_consultant]
		@start_date as datetime 
		, @end_date as datetime
as
BEGIN


declare @end_date2 varchar(30)
set @end_date2 = convert(varchar(30), @end_date, 101) + ' 23:59:59'
set @end_date = convert(datetime, @end_date2)


-- pre-generate the tps
create table #tps (
	rownum int identity(1,1)
	, act int
	, orderid int
	, quantity int
	, price money
	, suppID int
	, event_id int
        , createdate datetime,updatedate datetime
)
	INSERT INTO #tps (
		orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate,updatedate
	)
	select orderid
	     , quantity
	     , price
	     , suppID
	     , event_id
	     , createdate,updatedate
	from
	(
	select o.order_id as orderid
	     , od.quantity
	     , od.price
	     , et.suppID
	     , ep.event_id
             , et.createdate,et.updatedate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
	where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701 )
	union all
	select oh.ID as orderid
	     , sod.quantity
	     , cid.value as price
	     , et.suppID
	     , ep.event_id
	     , et.createdate,et.updatedate
 	from qspecommerce.dbo.efundraisingtransaction et 
		inner join qspstore.dbo.orderheader oh on oh.id = et.orderid
		inner join qspstore.dbo.suborderheader soh on oh.id = soh.orderheaderid 
		inner join qspstore.dbo.suborderdetail sod on soh.id = sod.suborderheaderid
		inner join qspstore.dbo.catalogitemdetail cid on cid.id = sod.catalogitemdetailid
		inner join event_participation ep on ep.event_participation_id = et.suppid
	WHERE oh.OrderTotal IS NOT NULL
	  AND oh.OrderStatusID NOT IN (1, 10, 11)
	  AND soh.ShipToAddressID <> 0
	  and oh.aggregatorid in (7,13)
	) t
	order by  createdate
 		, price


create index ix_suppid on #tps (suppid)

-- update tout les activation a 0
update #tps set act = 0
-- trouver toutes les 1rst subs (activations)
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

-- header du rapport
select 	--'-Partner Name'
	--, '+Request'
	 'Consultant'
	, '+Activation'
	, '+Subs'
	, '$Total Amount'
from #tps 
where rownum = 1

-- pre-calculate the result 
create table #rpt (
	--partner_name varchar(200)
--	, request int
	 consultant varchar(200)
	, activation int
	, subs int
	, total_amount float
)
insert into #rpt 
(	--partner_name
--	, request
	 consultant
	, activation
	, subs
	, total_amount
)
/*select 	partner_name
	, sum(request) as request
	, sum(activation) as activation
	, sum(subs) as subs
	, sum(total_amount) as total_amount
from 
(*/
select 	--p.partner_name
--	, 0 AS request
	 con.name as consultant
	, SUM( tps.act) AS activation
	, SUM( tps.quantity) AS subs
	, SUM( tps.price) AS total_amount
from  
event_participation ep 
left outer join #tps tps on ep.event_participation_id = tps.suppid
inner join member_hierarchy mh on mh.member_hierarchy_id = ep.member_hierarchy_id
inner join member m on m.member_id = mh.member_id
--left outer join creation_channel cc on cc.creation_channel_id = mh.creation_channel_id
inner join event_group eg on eg.event_id = ep.event_id
inner join [group] g on g.group_id = eg.group_id
inner join partner p on p.partner_id = g.partner_id
left join efundraisingprod.dbo.lead l on l.lead_id = g.lead_id
left join efundraisingprod.dbo.consultant con on con.consultant_id = l.consultant_id
where 
(
	(tps.createdate between @start_date  and @end_date or tps.createdate is null )
	OR (tps.updatedate between @start_date  and @end_date or tps.updatedate is null )
)
group by --p.partner_name, 
	con.name
/*

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
*/
/*
)rpt
group by partner_name*/

--create index ix_partner_name on #rpt (partner_name)


delete from #rpt 
where  
	--(request = 0 or request is null)
	(activation = 0 or activation is null)
	and (subs = 0 or subs is null)
	and (total_amount = 0 or total_amount is null)

-- return the detail of the report
select -- partner_name
--	, sum(request) as request
	 consultant
	, sum(activation) as activation
	, sum(subs) as subs
	, sum(total_amount) as total_amount
from #rpt
/*where 
	request <> 0 
	and activation <> 0 
	and subs <> 0 
	and total_amount <> 0 */
group by --partner_name, 
	consultant
order by --partner_name, 
	consultant

/*
--return the footer (summary/sum) of the report
select count(partner_name) as partner_name
	, sum(request) as request
	, sum(activation) as activation
	, sum(subs) as subs
	, sum(total_amount) as total_amount
from #rpt
where 
	request <> 0 
	and activation <> 0 
	and subs <> 0 
	and total_amount <> 0 

*/
END
GO
