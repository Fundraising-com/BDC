USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_item_by_event_id_profit_id_to_grandfather]    Script Date: 02/14/2014 13:06:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_get_payment_item_by_event_id_profit_id_to_grandfather] 

@event_id int,

@profit_id int

AS

 

BEGIN

 

SELECT DISTINCT 

         [payment_item].[payment_item_id]

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

inner join [dbo].[event] on [dbo].[event].event_id = [dbo].[event_group].event_id

inner join efrcommon..profit  on efrcommon..profit.profit_id =  [dbo].[payment_item].profit_id

inner join (select EFRCommon..profit.profit_id, sum (EFRCommon..profit_range.profit_range_percentage) as profit_sum

                  from  EFRCommon..profit_range inner join EFRCommon..profit on EFRCommon..profit_range.profit_id = EFRCommon..profit.profit_id

                  group by EFRCommon..profit.profit_id)  t3 on t3.profit_id = efrcommon..profit.profit_id

left join (select t1.qsp_order_detail_id

 from 

(select DISTINCT [payment_item].[payment_item_id]

      ,[payment_item].[payment_id] 

      ,[payment_item].[qsp_order_detail_id] 

      ,[payment_item].[order_detail_amount]

      ,[payment_item].[profit_percentage] 

      ,[payment_item].[profit_amount]

      ,[payment_item].[create_date] 

      ,[payment_item].[profit_id] 

      ,[payment_item].[profit_range_id] 

      

from payment_item

inner join payment on [dbo].[payment_item].payment_id = [dbo].[payment].payment_id

inner join [dbo].[payment_info] on [dbo].[payment].payment_info_id = [dbo].[payment_info].payment_info_id

inner join [dbo].[payment_payment_status] on [dbo].[payment].payment_id = [dbo].[payment_payment_status].payment_id

inner join [dbo].[event_group] on [dbo].[payment_info].group_id = [dbo].[event_group].group_id

inner join [dbo].[event] on [dbo].[event].event_id = [dbo].[event_group].event_id 

WHERE payment.is_processed = 1 and dbo.payment_payment_status.payment_status_id <> 9

and [dbo].[payment_item].[profit_id] = @profit_id 

and [dbo].[event].culture_code = 'en-US'  

and [dbo].[event].event_id = @event_id

)t1 

group by t1.qsp_order_detail_id, t1.order_detail_amount

having count (*) % 2 = 0) t2 on [payment_item].[qsp_order_detail_id] =  t2.qsp_order_detail_id

WHERE payment.is_processed = 1 and dbo.payment_payment_status.payment_status_id <> 9

and [dbo].[event].culture_code = 'en-US' and t2.qsp_order_detail_id is null

and [payment_item].[profit_percentage] < (efrcommon..profit.profit_percentage + t3.profit_sum) /100.00

 

END
GO
