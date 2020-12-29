USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_order_sale_by_id]    Script Date: 02/14/2014 13:05:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Order_sale
CREATE PROCEDURE [dbo].[efrstore_get_order_sale_by_id] @Order_id int AS
begin

select Order_id, Sale_id, Date_created from Order_sale where Order_id=@Order_id

end
GO
