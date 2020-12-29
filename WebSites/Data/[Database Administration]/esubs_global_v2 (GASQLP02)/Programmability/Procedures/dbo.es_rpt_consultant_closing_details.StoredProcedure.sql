USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_consultant_closing_details]    Script Date: 02/14/2014 13:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		Philippe Girard
-- Create date: 2006/05/08
-- Description:	Generate the rpt_consultat closing detail information.
--              
--              Use by at least the My group page activation V2
-- =============================================================

CREATE PROCEDURE [dbo].[es_rpt_consultant_closing_details]
AS
BEGIN
SET NOCOUNT ON;

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

create index ix_suppid on #tps (suppid)

BEGIN TRAN

TRUNCATE TABLE reports_tables.dbo.es_rpt_consultant_closing_details

INSERT INTO reports_tables.dbo.es_rpt_consultant_closing_details
SELECT
	g.lead_id --l.lead_id 
	,con.[Name]-- con.[Name]
	,p.partner_name -- part.partner_name
	, e.event_name as group_name --c.Group_Name
	, e.event_id as campaign_id --c.Campaign_ID
	, SUM( tps.Quantity ) AS Total_Units
	, SUM( tps.price ) AS Total_Dollars
	,nb_participants
	,nb_supporters
	, tps.createdate as order_date
	, a.activation_date as activation_date--act.activation_date
from 
	[group] g
	inner join partner p
	on p.partner_id = g.partner_id
	INNER JOIN eFundraisingProd.dbo.Lead l
	ON g.lead_id = l.Lead_ID
	--and g.lead_id = 504850
	INNER JOIN efundraisingProd.dbo.Consultant con
	ON con.Consultant_ID = l.Consultant_ID
	inner join event_group eg
	on eg.group_id = g.group_id
	inner join event e
	on e.event_id = eg.event_id
	inner join event_participation ep
	on ep.event_id = e.event_id
	inner join member_hierarchy mh
	on mh.member_hierarchy_id = ep.member_hierarchy_id
	inner join (
		select 
			ep.event_id
			,min(createdate) as activation_date
		from 
			event_participation ep
			inner join #tps tpsa
			on tpsa.suppid = ep.event_participation_id
		group by 
			ep.event_id
		) a
	on a.event_id = e.event_id
	inner join (
		select
			ep.event_id
			, count (distinct case when mh.creation_channel_id in(7,8,20,22,23)  then event_participation_id else null end)AS nb_participants
			, count (distinct case when mh.creation_channel_id in(12,14,15,29) or tpsb.suppid is not null then event_participation_id else null end) AS nb_supporters
		from 
			event_participation ep
			inner join member_hierarchy mh
			on ep.member_hierarchy_id = mh.member_hierarchy_id
			left outer join #tps tpsb
			on tpsb.suppid = ep.event_participation_id
		group by 
			ep.event_id
	) b
	on b.event_id = e.event_id
	left outer join #tps tps
	on tps.suppid = ep.event_participation_id
group by
	con.[Name]
	, e.event_id
	, g.lead_id
	, e.event_Name
	, p.partner_name
	, tps.createdate
	, a.activation_date
	, ep.event_participation_id
	, tps.suppid
	, mh.creation_channel_id
	, nb_participants
	, nb_supporters
ORDER BY
	con.[Name]
	, e.Campaign_ID

COMMIT TRAN

END
GO
