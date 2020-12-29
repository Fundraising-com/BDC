USE [esubs_global_v2]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_es_get_events_groups_by_payment_id]    Script Date: 02/14/2014 13:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_es_get_events_groups_by_payment_id]
(
    @payment_id  int
)  
RETURNS TABLE
AS
RETURN (

SELECT     
	eg.event_id, ggs.*	
FROM         
	[group] g 
    INNER JOIN event_group eg
        ON eg.group_id = g.group_id
    INNER JOIN group_group_status ggs on ggs.group_id = g.group_id
WHERE eg.event_id in
	(
		select distinct ep.event_id
		    from event_participation ep
		        inner join QSPEcommerce.dbo.efundraisingtransaction et
		            on et.suppID = ep.event_participation_id
		        inner join QSPFulfillment.dbo.[order] o
		            on o.order_id = et.OrderID
		        inner join QSPFulfillment.dbo.[order_detail] od
		            on od.order_id = o.order_id
		        left join payment_item pi
		            on pi.qsp_order_detail_id = od.order_detail_id
		    where 
		--o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701 ) and
		pi.qsp_order_detail_id in 
			(
			select qsp_order_detail_id
			from payment_item
			where payment_id =@payment_id)
	)


)
GO
