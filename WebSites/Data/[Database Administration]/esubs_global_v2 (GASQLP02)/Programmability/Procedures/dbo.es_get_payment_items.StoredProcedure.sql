USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_items]    Script Date: 02/14/2014 13:06:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_item
CREATE PROCEDURE [dbo].[es_get_payment_items] AS
begin

select Payment_item_id, Payment_id, qsp_order_detail_id, order_detail_amount, profit_percentage,  profit_amount,
Create_date, profit_id, profit_range_id
from Payment_item

end
GO
