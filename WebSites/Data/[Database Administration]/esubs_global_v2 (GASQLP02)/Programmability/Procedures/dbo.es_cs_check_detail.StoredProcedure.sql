USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_cs_check_detail]    Script Date: 02/14/2014 13:05:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
procedure qui affiche un rapport sommaire pour les partners
mod mcote 	2006-02-18 	- creation de table temporaire (optimisation)
				- select return the header of the report
				- creation d'une table temporaire pour les resultats du rapports
				- select return the detail of the report
				- select return the footer(sum) of the report

*/
CREATE          procedure [dbo].[es_cs_check_detail]
		@start_date as datetime 
		, @end_date as datetime
as

/*
declare 	@start_date as datetime 
		, @end_date as datetime

set 	@start_date = '2006-06-01'
set 	@end_date ='2006-07-01'

*/

select 
	et.suppID as event_participation_id
	, ep.event_id
	, o.order_id as orderid
	,order_detail_id
        , od.quantity
	, ci.catalog_item_name
	, od.price
        , et.createdate
	from qspecommerce.dbo.efundraisingtransaction et
		inner join qspfulfillment.dbo.[order] o on o.order_id = et.orderid
		inner join qspfulfillment.dbo.[order_detail] od on od.order_id = o.order_id
		inner join event_participation ep on ep.event_participation_id = et.suppid
		inner join qspfulfillment.dbo.catalog_item_detail cdi on cdi.catalog_item_detail_id = od.catalog_item_detail_id
		inner join qspfulfillment.dbo.catalog_item ci on ci.catalog_item_id = cdi.catalog_item_id
	where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701, 90, 9401 )
	and createdate between @start_date and @end_date
	order by  createdate
 		, od.price
GO
