USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[es_get_event_by_order_detail_id]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_event_by_order_detail_id]
	@order_detail_id int
AS
BEGIN


SELECT    
	e.event_id
	, e.event_type_id
	, e.culture_code
	, e.event_name
	, e.start_date
	, e.end_date	
	, e.active
	, e.comments
	, e.create_date
	, et.event_type_name
	, eg.group_id
	, g.partner_id
	, ep.event_participation_id  as sponsor_event_participation_id
	, e.redirect
    , e.group_type_id
	, e.[profit_id]
    , e.[profit_calculated]
    , e.[processing_fee]
FROM         
	event e 
	INNER JOIN event_group eg
	ON e.event_id = eg.event_id 
	INNER JOIN event_type et 
	ON e.event_type_id = et.event_type_id
	inner join [group] g
	on g.group_id = eg.group_id
	inner join event_participation ep
	on ep.event_id = eg.event_id
	inner join qspecommerce.dbo.EfundraisingTransaction on ep.event_participation_id = qspecommerce.dbo.EfundraisingTransaction.SuppID
inner join qspfulfillment.dbo.[order] on qspfulfillment.dbo.[order].order_id = qspecommerce.dbo.EfundraisingTransaction.orderid
inner join qspfulfillment.dbo.[order_detail] on qspfulfillment.dbo.[order_detail].order_id = qspfulfillment.dbo.[order].order_id
WHERE     
	qspfulfillment.dbo.[order_detail].order_detail_id  = @order_detail_id 
END
GO
