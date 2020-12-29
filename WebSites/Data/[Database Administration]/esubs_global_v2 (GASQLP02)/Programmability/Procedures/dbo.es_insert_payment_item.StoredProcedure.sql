USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_item]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_item
CREATE PROCEDURE [dbo].[es_insert_payment_item] @Payment_item_id int OUTPUT, @Payment_id int, 
@Qsp_order_detail_id int ,@Order_detail_amount money, @Profit_percentage float, @Profit_amount money
, @Create_date datetime, @profit_id int, @profit_range_id int AS
begin

insert into Payment_item(Payment_id, qsp_order_detail_id, order_detail_amount, profit_percentage,  profit_amount, Create_date, profit_id, profit_range_id) 
values(@Payment_id, @Qsp_order_detail_id ,@Order_detail_amount , @Profit_percentage , @Profit_amount, @Create_date, @profit_id, @profit_range_id)

select @Payment_item_id = SCOPE_IDENTITY()

end
GO
