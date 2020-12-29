USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_order_detail_canceled_by_event_id]    Script Date: 02/14/2014 13:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    Name: es_get_order_detail_canceled_by_event_id
    Created by: Philippe Girard
    Created on: 2007-07-18
    Description: 

*/
CREATE PROCEDURE [dbo].[es_get_order_detail_canceled_by_event_id]
    @event_id int
AS
BEGIN
    select od.order_detail_id
          ,pi.order_detail_amount * -1 as order_detail_amount
    from event_participation ep
        inner join QSPEcommerce.dbo.efundraisingtransaction et
            on et.suppID = ep.event_participation_id
        inner join QSPFulfillment.dbo.[order] o
            on o.order_id = et.OrderID
        inner join QSPFulfillment.dbo.[order_detail] od
            on od.order_id = o.order_id
        inner join (
            select qsp_order_detail_id, sum(order_detail_amount) as order_detail_amount
            from payment_item
            where payment_id not in (select pps.payment_id
				from payment_payment_status pps
				inner join (
					select payment_id, max(create_date) as create_date
					from payment_payment_status 
					group by payment_id
					) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date      
				where pps.payment_status_id = 9 -- Payment Cancelled
	                                     )
            group by qsp_order_detail_id
        ) pi
            on pi.qsp_order_detail_id = od.order_detail_id
    where (o.order_status_id in ( 9, 109, 209, 309, 801 )
       or od.deleted = 1)
      and pi.order_detail_amount > 0
      and ep.event_id = @event_id
END
GO
