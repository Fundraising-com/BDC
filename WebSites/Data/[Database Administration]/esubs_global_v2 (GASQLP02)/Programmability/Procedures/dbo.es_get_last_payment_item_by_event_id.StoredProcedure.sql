USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_last_payment_item_by_event_id]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_item
CREATE PROCEDURE [dbo].[es_get_last_payment_item_by_event_id] @event_id int AS
begin

select top 1
	pit.Payment_item_id
	, pit.Payment_id
	, pit.qsp_order_detail_id
	, pit.order_detail_amount
	, pit.profit_percentage
	, pit.profit_amount
	, pit.Create_date
	, pit.profit_id
	, pit.profit_range_id 
	from payment_info pin
	inner join payment p on pin.payment_info_id = p.payment_info_id
	--left join payment_period pp on pp.payment_period_id = p.payment_period_id
    inner join payment_item pit on pit.payment_id = p.payment_id 
	--inner join qspfulfillment.dbo.[order_detail]  od on pit.qsp_order_detail_id = od.order_detail_id 
	inner join payment_payment_status pps on pps.payment_id = p.payment_id
	inner join (
		select payment_id, max(create_date) as create_date
		from payment_payment_status
		group by payment_id
		   ) ppsNew on ppsNew.payment_id = pps.payment_id and ppsNew.create_date = pps.create_date 	and payment_status_id in (2, 10)
	left join dbo.payment_batch pb on pb.payment_batch_id = p.payment_batch_id and confirmation_date is not null
	where event_id = @event_id
	order by pit.profit_percentage desc, pit.create_date desc 
end
GO
