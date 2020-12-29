USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_order_detail_by_event_id]    Script Date: 02/14/2014 13:05:37 ******/
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
CREATE PROCEDURE [dbo].[es_get_order_detail_by_event_id]
@event_id INT,
@startdate DATETIME,
@enddate DATETIME
AS
BEGIN

	DECLARE @t_event_id INT
	DECLARE @t_startdate DATETIME
	DECLARE @t_enddate DATETIME

	SET @t_event_id = @event_id
	SET @t_startdate = @startdate
	SET @t_enddate = @enddate

    SELECT od.order_detail_id, od.price * od.quantity AS order_detail_amount, pt.fulfillment_charge	--pt.product_type_id,
    FROM event_participation ep				
        INNER JOIN QSPEcommerce.dbo.efundraisingtransaction et  
            ON et.suppID = ep.event_participation_id  
        INNER JOIN QSPFulfillment.dbo.[order] o 
			INNER JOIN QSPFulfillment.dbo.[order_detail] od
				INNER JOIN QSPFulfillment.dbo.[catalog_item_detail] cid 
					INNER JOIN QSPFulfillment.dbo.[catalog_item] ci 	
						INNER JOIN QSPFulfillment.dbo.[product] p 
							INNER JOIN QSPFulfillment.dbo.[product_type] pt 
								ON pt.product_type_id = P.product_type_id
							ON p.product_id = ci.product_id
						ON ci.catalog_item_id = cid.catalog_item_id
					ON cid.catalog_item_detail_id  = od.catalog_item_detail_id 
				ON od.order_id = o.order_id
			ON o.order_id = et.orderid
        LEFT JOIN 
			(SELECT distinct qsp_order_detail_id 
			FROM payment_item
			WHERE payment_id in (
				SELECT pps.payment_id
				FROM payment_payment_status pps
					INNER JOIN (
						SELECT payment_id
						     , MAX(create_date) AS create_date
						FROM payment_payment_status 
						GROUP BY payment_id
					) pps2 ON pps.payment_id = pps2.payment_id AND pps.create_date = pps2.create_date      
				WHERE pps.payment_status_id NOT IN(9) -- Payment Cancelled
				)) pi
			ON pi.qsp_order_detail_id = od.order_detail_id
        inner join dbo.es_get_valid_order_status() os on o.order_status_id = os.order_status_id
    WHERE (o.order_date between @startdate and @enddate)
		--AND o.order_status_id IN ( 101, 110, 201, 301, 401, 501, 601, 701 )
		AND pi.qsp_order_detail_id IS NULL
		AND ep.event_id = @t_event_id
END
GO
