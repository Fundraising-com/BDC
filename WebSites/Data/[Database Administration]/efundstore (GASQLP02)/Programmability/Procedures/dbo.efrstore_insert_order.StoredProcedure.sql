USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_order]    Script Date: 02/14/2014 13:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Order
CREATE PROCEDURE [dbo].[efrstore_insert_order] @Order_id int OUTPUT, @Shopping_cart_id int, @Online_user_id int, @Credit_card_id int, @Culture_code nvarchar(10), @Random_number int, @Order_number varchar(26), @Order_total numeric, @Shipping_total numeric, @Tax_total numeric, @Order_submitted bit, @Date_created datetime, @Scheduled_delivery_date datetime AS
begin
-- order number is a compted column
insert into [Order](Shopping_cart_id, Online_user_id, Credit_card_id, Culture_code, Random_number, Order_total, Shipping_total, Tax_total, Order_submitted, Date_created, Scheduled_delivery_date) values(@Shopping_cart_id, @Online_user_id, @Credit_card_id, @Culture_code, @Random_number, @Order_total, @Shipping_total, @Tax_total, @Order_submitted, @Date_created, @Scheduled_delivery_date)

select @Order_id = SCOPE_IDENTITY()

end
GO
