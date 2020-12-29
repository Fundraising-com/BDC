USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_order]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Order
CREATE PROCEDURE [dbo].[efrstore_update_order] @Order_id int, @Shopping_cart_id int, @Online_user_id int, @Credit_card_id int, @Culture_code nvarchar(10), @Random_number int, @Order_number varchar(26), @Order_total numeric, @Shipping_total numeric, @Tax_total numeric, @Order_submitted bit, @Date_created datetime, @Scheduled_delivery_date datetime AS
begin
-- order number is a compted column
update [Order] set Shopping_cart_id=@Shopping_cart_id, Online_user_id=@Online_user_id, Credit_card_id=@Credit_card_id, Culture_code=@Culture_code, Random_number=@Random_number, Order_total=@Order_total, Shipping_total=@Shipping_total, Tax_total=@Tax_total, Order_submitted=@Order_submitted, Date_created=@Date_created, Scheduled_delivery_date=@Scheduled_delivery_date where Order_id=@Order_id

end
GO
