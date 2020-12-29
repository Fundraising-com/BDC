USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_orders_sale_by_id]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Orders_sale
CREATE PROCEDURE [dbo].[efrcrm_get_orders_sale_by_id] @Order_id int AS
begin

select Order_id, Sales_id, Date_created from Orders_sale where Order_id=@Order_id

end
GO
