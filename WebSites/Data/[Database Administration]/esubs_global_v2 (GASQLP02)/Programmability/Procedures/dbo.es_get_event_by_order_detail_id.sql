USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_by_order_detail_id]    Script Date: 08/25/2014 16:34:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[es_get_event_by_order_detail_id]
	@order_detail_id int
AS
BEGIN

SELECT    
	e.event_id
	, e.event_status_id
	, e.culture_code
	, e.event_name
	, e.start_date
	, e.end_date	
	, e.active
	, e.comments
	, e.create_date
	, es.event_status_name
	, eg.group_id
	, g.partner_id
	, ep.event_participation_id  as sponsor_event_participation_id
	, e.redirect
    , e.group_type_id
	, e.[profit_group_id]
    , e.[profit_calculated]
    , e.[processing_fee]
    , e.event_type_id
	, e.donation
    , et.event_type_name
    , e.discount_site
FROM         
	event e 
	INNER JOIN event_group eg
	ON e.event_id = eg.event_id 
    INNER JOIN event_status es
    ON es.event_status_id = e.event_status_id 
	INNER JOIN event_type et 
	ON e.event_type_id = et.event_type_id
	inner join [group] g
	on g.group_id = eg.group_id
	inner join event_participation ep
	on ep.event_id = eg.event_id
	join [es_get_valid_orders_items] () tps on tps.supp_id = ep.event_participation_id
WHERE     
	tps.order_item_id  = @order_detail_id 
END
