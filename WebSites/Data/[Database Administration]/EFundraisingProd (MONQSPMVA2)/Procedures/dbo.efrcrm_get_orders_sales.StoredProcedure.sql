USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_orders_sales]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Orders_sale
CREATE PROCEDURE [dbo].[efrcrm_get_orders_sales] AS
begin

select Order_id, Sales_id, Date_created from Orders_sale

end
GO
