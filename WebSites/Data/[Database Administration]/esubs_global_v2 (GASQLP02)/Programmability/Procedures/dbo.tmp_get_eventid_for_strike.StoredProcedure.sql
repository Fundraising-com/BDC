USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[tmp_get_eventid_for_strike]    Script Date: 02/14/2014 13:08:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tmp_get_eventid_for_strike]

AS
BEGIN


--drop table #tps1
-- pre-generate the tps
create table #tps1 (
	rownum int identity(1,1)
	, act int
	, orderid int
	, quantity int
	, price money
	, suppID int
	, event_id int
        , createdate datetime
)
	INSERT INTO #tps1 (
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
	union all
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
	  and oh.aggregatorid in (7,13)
	) t
	order by  createdate
 		, price


create index ix_suppid on #tps1 (suppid)

-- update tout les activation a 0
update #tps1 set act = 0
-- trouver toutes les 1rst subs (activations)
update #tps1 set act = 1 where rownum in (select min(rownum) as rownum from #tps1 group by event_id)

select distinct  eg.event_id, p.partner_name, tps.createdate
from event_participation ep
	inner join event_group eg on eg.event_id = ep.event_id 
	inner join [group] g on g.group_id = eg.group_id 
	-- order
        inner join #tps1 tps on tps.suppid = ep.event_participation_id and act = 1
	inner join partner p on g.partner_id = p.partner_id
where g.lead_id is null 
--and g.create_date>'01-01-06' 
and g.partner_id not in (61,62,65,69,
70,71,86,88,90,91,92,96,99,100,101,102,104,105,106,108,109,110,111,112,113,116,119,120,
121,123,126,127,128,131,135,136,137,138,139,140,143,145,148,149,150,151,156,587,589,590,
593,594,595,596,597,598,599,600,601,604,605,606,607,609,630,631,632,633,637,638,639,648,
653,654,655,656,657)
and g.partner_id = 58
and tps.createdate >'07-01-2006' 


END
GO
