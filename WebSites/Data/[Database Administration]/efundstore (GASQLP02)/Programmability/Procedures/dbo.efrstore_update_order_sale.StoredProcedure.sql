USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_update_order_sale]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Order_sale
CREATE PROCEDURE [dbo].[efrstore_update_order_sale] @Order_id int, @Sale_id int, @Date_created datetime AS
begin

update Order_sale set Sale_id=@Sale_id, Date_created=@Date_created where Order_id=@Order_id

end
GO
