USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_orders_sale]    Script Date: 02/14/2014 13:08:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Orders_sale
CREATE PROCEDURE [dbo].[efrcrm_update_orders_sale] @Order_id int, @Sales_id int, @Date_created datetime AS
begin

update Orders_sale set Sales_id=@Sales_id, Date_created=@Date_created where Order_id=@Order_id

end
GO
