USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[es_get_processed_payment_items_by_event_id]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_processed_payment_items_by_event_id] 
@event_id int
AS
BEGIN
SELECT DISTINCT [payment_item].[payment_item_id]
      ,[payment_item].[payment_id]
      ,[payment_item].[qsp_order_detail_id]
      ,[payment_item].[order_detail_amount]
      ,[payment_item].[profit_percentage]
      ,[payment_item].[profit_amount]
      ,[payment_item].[create_date]
      ,[payment_item].[profit_id]
      ,[payment_item].[profit_range_id]
FROM [dbo].[payment_item] inner join payment on [dbo].[payment_item].payment_id = [dbo].[payment].payment_id
inner join [dbo].[payment_info] on [dbo].[payment].payment_info_id = [dbo].[payment_info].payment_info_id
inner join [dbo].[payment_payment_status] on [dbo].[payment].payment_id = [dbo].[payment_payment_status].payment_id
inner join [dbo].[event_group] on [dbo].[payment_info].group_id = [dbo].[event_group].group_id
WHERE payment.is_processed = 1 and dbo.payment_payment_status.payment_status_id <> 9
and [dbo].[event_group].event_id = @event_id
END
GO
