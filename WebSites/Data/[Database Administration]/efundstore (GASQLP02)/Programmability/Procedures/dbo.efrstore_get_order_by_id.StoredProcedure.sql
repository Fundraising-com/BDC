USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_order_by_id]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Order
CREATE PROCEDURE [dbo].[efrstore_get_order_by_id] @Order_id int AS
begin

select Order_id, Shopping_cart_id, Online_user_id, Credit_card_id, Culture_code, Random_number, Order_number, Order_total, Shipping_total, Tax_total, Order_submitted, Date_created, Scheduled_delivery_date from [Order] where Order_id=@Order_id

end
GO
