USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_esubs_activation_vs_assignation_report]    Script Date: 02/14/2014 13:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec es_rpt_esubs_activation_vs_assignation_report '2006-11-01', '2006-11-30' --23:59:59'

/*
procedure qui affiche un rapport sommaire pour les partners
mod mcote 2006-02-18 - creation de table temporaire (optimisation)
- select return the header of the report
- creation d'une table temporaire pour les resultats du rapports
- select return the detail of the report
- select return the footer(sum) of the report

*/
CREATE procedure [dbo].[es_rpt_esubs_activation_vs_assignation_report]
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
order by createdate
, price


create index ix_suppid on #tps (suppid)

-- update tout les activation a 0
update #tps set act = 0
-- trouver toutes les 1rst subs (activations)
update #tps set act = 1 where rownum in (select min(rownum) as rownum from #tps group by event_id)

-- header du rapport
select 'Partner Name'
, 'Lead_ID'
, 'Activation Date'
, 'Lead Assignment Date'
, 'Consultant Name'
select p.partner_name
, l.lead_id
,min(tps.createdate) as activation_date
,min(l.lead_assignment_date) as lead_assignement_Date
, con.name as consultant_name
from
efundraisingprod.dbo.lead l
inner join efundraisingprod.dbo.consultant con on con.consultant_id = l.consultant_id
inner join [group] g
on g.lead_id = l.lead_id
inner join event_group eg
on eg.group_id= g.group_id
inner join event_participation ep
on ep.event_id = eg.event_id
inner join #tps tps
on tps.suppid = ep.event_participation_id
inner join partner p on g.partner_id = p.partner_id
group by
p.partner_name
, l.lead_id
, con.name
having min(l.lead_assignment_date) < min(tps.createdate)
and min(tps.createdate) between @start_date and @end_date
order by 2


END
GO
