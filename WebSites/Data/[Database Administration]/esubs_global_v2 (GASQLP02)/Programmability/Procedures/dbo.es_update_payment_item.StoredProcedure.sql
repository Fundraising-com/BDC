USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_item]    Script Date: 02/14/2014 13:07:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment_item
CREATE PROCEDURE [dbo].[es_update_payment_item] @Payment_item_id int, @Payment_id int,@Qsp_order_detail_id int ,@Order_detail_amount money, @Profit_percentage float, @Profit_amount money , @Create_date datetime, @Profit_id int, @Profit_range_id int AS
begin

update Payment_item set Payment_id=@Payment_id, Qsp_order_detail_id =@Qsp_order_detail_id, Order_detail_amount =@Order_detail_amount, Profit_percentage=@Profit_percentage , Profit_amount =@Profit_amount , 
Create_date=@Create_date, profit_id = @profit_id, profit_range_id= @profit_range_id where Payment_item_id=@Payment_item_id

end
GO
