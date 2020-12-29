USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_order_detail_by_event_id_ca]    Script Date: 02/14/2014 13:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
    Name: es_get_order_detail_by_event_id
    Created by: Philippe Girard
    Created on: 2007-07-17
    Description: 


*/
CREATE PROCEDURE [dbo].[es_get_order_detail_by_event_id_ca]
    @event_id int
    , @startdate datetime
    , @enddate datetime
AS
BEGIN

	DECLARE @t_event_id int
	DECLARE @t_startdate datetime
	DECLARE @t_enddate datetime

	SET @t_event_id = @event_id
	SET @t_startdate = @startdate
	SET @t_enddate = @enddate

     select ioid.internetorderid, cod.price - cod.tax - cod.tax2  as order_detail_amount
    from event_participation ep
        inner join QSPEcommerce.dbo.efundraisingtransaction et
            on et.suppID = ep.event_participation_id
        inner join qspFulfillment.dbo.[Order] o
            on  et.orderid = o.order_id
        inner join qspEcommerce.dbo.cart c 
            on  o.order_id = c.x_order_id
        inner join qspCanadaOrderManagement.dbo.InternetOrderID ioid 
            ON c.eds_order_id = ioid.internetorderid
        inner join qspCanadaOrderManagement.dbo.CustomerOrderDetail cod
            on ioid.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance
        left join qspCanadaOrderManagement.dbo.CustomerOrderDetailRemitHistory hist 
          on hist.CustomerOrderHeaderInstance = cod.CustomerOrderHeaderInstance and hist.transid = cod.transid 

        left join ( --order must not have a payment
			select distinct od.order_id
			from payment_item p inner join
                 QSPFulfillment.dbo.[order_detail] od on p.qsp_order_detail_id = od.order_detail_id
			where payment_id in (
				select pps.payment_id
				from payment_payment_status pps
					inner join (
						select payment_id
						     , max(create_date) as create_date
						from payment_payment_status 
						group by payment_id
					) pps2 on pps.payment_id = pps2.payment_id and pps.create_date = pps2.create_date      
				where pps.payment_status_id not in (9) -- Payment Cancelled
				) 
			) pi
            on pi.order_id = o.order_id
         inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
    where --o.order_status_id in ( 101, 110, 201, 301, 401, 501, 601, 701 )
       o.order_date between @t_startdate and @t_enddate 
	   and cod.delflag = 0 
       and (hist.status in (42001,42000,42010) or cod.producttype in (46006, 46007, 460012) )
      and ep.event_id = @t_event_id
	  and pi.order_id is null

END
GO
