USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_orders_by_group]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_orders_by_group] @group_id int AS

SELECT
	o.order_id as order_id
	, o.order_date as order_date
	, et.SuppID as event_participation_id
	, o.customer_id as customer_id
	, od.quantity
	, od.price
	, ci.catalog_item_name  as catalog_item_title
	, c.first_name as customer_first_name
	, c.last_name as customer_last_name
	, ci.catalog_item_id as order_item_id
	, od.order_detail_id
	, o.order_status_id 
	, cart.eds_order_id as eds_id
from QSPECommerce.dbo.EfundraisingTransaction et
	inner join QSPFulfillment.dbo.[order] o
		on o.order_id = et.OrderID
	inner join QSPFulfillment.dbo.[order_detail] od
		on od.order_id = o.order_id
	inner join QSPFulfillment.dbo.catalog_item_detail cid
		on cid.catalog_item_detail_id = od.catalog_item_detail_id
	inner join QSPFulfillment.dbo.catalog_item ci
		on ci.catalog_item_id = cid.catalog_item_id
	inner join QSPFulfillment.dbo.customer c
		on c.customer_id = o.customer_id
	left join QSPECommerce.dbo.Cart cart
		on et.orderid = cart.x_order_id 
	inner join event_participation ep
		on et.SuppID = ep.event_participation_id
	inner join event e
		on e.event_id = ep.event_id
	inner join event_group eg
		on e.event_id = eg.event_id
where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701,90 )
	--and ep.event_id = @event_id
	and eg.group_id = @group_id
	and o.order_id not in (
		select order_id
		from order_profit op 
		--where op.order_id = tps.orderid 
			-- and op.order_item_id = tps.order_detail_id 
	)


/*
SELECT
	o.order_id as order_id
	, o.order_date as order_date
	, et.SuppID as event_participation_id
	, o.customer_id as customer_id
	, od.quantity
	, od.price
	, ci.catalog_item_name  as catalog_item_title
	, c.first_name as customer_first_name
	, c.last_name as customer_last_name
	, ci.catalog_item_id as order_item_id
	, od.order_detail_id
	, o.order_status_id 
	, cart.eds_order_id as eds_id
from QSPECommerce.dbo.EfundraisingTransaction et
	inner join QSPFulfillment.dbo.[order] o
		on o.order_id = et.OrderID
	inner join QSPFulfillment.dbo.[order_detail] od
		on od.order_id = o.order_id
	inner join QSPFulfillment.dbo.catalog_item_detail cid
		on cid.catalog_item_detail_id = od.catalog_item_detail_id
	inner join QSPFulfillment.dbo.catalog_item ci
		on ci.catalog_item_id = cid.catalog_item_id
	inner join QSPFulfillment.dbo.customer c
		on c.customer_id = o.customer_id
	left join QSPECommerce.dbo.Cart cart
		on et.orderid = cart.x_order_id 
	inner join event_participation ep
		on et.SuppID = ep.event_participation_id
	inner join event_group eg
		on ep.event_id = eg.event_id
where o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701,90 )
	and eg.group_id = @group_id
	and o.order_id not in (
		select order_id
		from order_profit op 
		--where op.order_id = tps.orderid 
			-- and op.order_item_id = tps.order_detail_id 
	)
*/
GO
